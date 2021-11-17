using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp.Classes;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;
using Shopping.Util.Authentications;
using Shopping.Util.CustomMessages;
using Swashbuckle.AspNetCore.Annotations;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("Customers")]
    public class LoginsController : ControllerBase
    {
        private readonly IStringLocalizer<Messages> _localizer;
        private readonly LoginsAppMaintenance _app;
        private readonly SessionAppMaintenance _sessionapp;

        public LoginsController(IStringLocalizer<Messages> localizer, LoginsAppMaintenance app, SessionAppMaintenance sessionapp)
        {
            _localizer = localizer;
            _app = app;
            _sessionapp = sessionapp;
        }



        [HttpPut]
        [Route("login/v1")]
        [SwaggerOperation(summary: "Customer", description: "CRUD of Customers")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> LogonAsync([FromBody] Login user)
        {
            try
            {
                var retaux = await this._app.Logon(user);
                if (this._app.ExistNotifications)
                    return InteliCustomMessage.MountMessage(new Message(title: this._localizer["Customer"].Value, notifications: this._app.GetAllNotifications), this._localizer);

                var useraux = retaux.Value as User;

                var token = JwtToken.GetToken(useraux);
                var refreshtoken = JwtToken.GetRefreshToken();

                retaux = await this._sessionapp.AddAsync(new SessionApp() { IdCustomer = new Guid(useraux.Id), Device = user.Device, Token = token, RefreshToken = refreshtoken });
                return Ok(new { Id = useraux.Id, Device = useraux.Device, Token = token, RefreshToken = refreshtoken });
            }
            catch (System.Exception e)
            {
                return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
            }
        }
        [HttpPut]
        [Route("refresh/v1/{token}/{refreshtoken}")]
        [SwaggerOperation(summary: "RefreshToken", description: "Get new Token and RefreshToken")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(SessionApp))]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> RefreshAsync(string token, string refreshtoken)
        {
            var principal = JwtToken.GetClaimsPrincipal(token);


            var idcustomer = principal.FindFirstValue(ClaimTypes.Actor);
            var device = principal.FindFirstValue("Device");
            var retaux = await this._sessionapp.GetSessionAsync(idcustomer, device);

            var session = retaux.Value as SessionApp;

            if (refreshtoken != session.RefreshToken || token != session.Token)
                return InteliCustomMessage.MountMessage(new Message(title: "Teste", simpleMessage: new SimpleMessage("Token", "")), this._localizer);

            if (!JwtToken.IsExpiredToken(token))
                return Ok(new Return(ReturnValues.Success, new { Token = token, RefreshToken = refreshtoken }));

            var newtoken = JwtToken.GetToken(principal.Claims);
            var newrefreshtoken = JwtToken.GetRefreshToken();
            session.Token = newtoken;
            session.RefreshToken = newrefreshtoken;
            retaux = await this._sessionapp.UpdateAsync(session.Id, session);

            return Ok(new Return(ReturnValues.Success, new { Token = newtoken, RefreshToken = newrefreshtoken }));
        }

    }
}