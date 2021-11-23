using System.Net;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.Utils.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;
using Shopping.Util.CustomMessages;
using Swashbuckle.AspNetCore.Annotations;

namespace InteliSystem.InteliMarketPlace.Api.Shopping.Controllers;

[ApiController]
[Route("[controller]")]
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

            var useraux = retaux.Value as InteliSystem.InteliMarketPlace.Applications.LoginsApp.User;

            retaux = await this._sessionapp.AddAsync(new SessionApp() { IdCustomer = useraux.Id, Device = user.Device });

            var session = retaux.Value as SessionApp;

            return Ok(session);
        }
        catch (System.Exception e)
        {
            return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
        }
    }
    [HttpPut]
    [Route("refresh/v1/")]
    [SwaggerOperation(summary: "RefreshToken", description: "Get new Token and RefreshToken")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(SessionApp))]
    [Authorize(Roles = "customer")]
    public async Task<IActionResult> RefreshAsync([FromBody] Keys keys)
    {

        try
        {
            var retaux = await this._sessionapp.RefreshTokenAsync(keys.Token, keys.RefreshToken);

            return Ok(retaux);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
