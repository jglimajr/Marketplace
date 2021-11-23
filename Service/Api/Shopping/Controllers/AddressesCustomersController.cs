using System.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.AddressesCustomer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;
using Shopping.Util.CustomMessages;

namespace InteliSystem.InteliMarketPlace.Api.Shopping.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressesCustomersController : ControllerBase
{
    private readonly AddressesCustomerAppMaintenance _app;
    private readonly IStringLocalizer<Messages> _localizer;

    public AddressesCustomersController(AddressesCustomerAppMaintenance app, IStringLocalizer<Messages> localizer)
    {
        this._app = app;
        this._localizer = localizer;
    }

    [HttpPost]
    [Route("add/v1")]
    [Authorize(Roles = "customer")]
    public async Task<IActionResult> AddAsync([FromBody] AddressApp address)
    {
        try
        {
            var retaux = await this._app.AddAsync(address);
            if (this._app.ExistNotifications)
                return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerOps"].Value, notifications: this._app.GetAllNotifications), this._localizer);
            return Ok(retaux);
        }
        catch (System.Exception e)
        {
            return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
        }
    }
    [HttpPut]
    [Route("update/v1")]
    [Authorize(Roles = "customer")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] AddressApp address)
    {
        try
        {
            var retaux = await this._app.UpdateAsync(id, address);
            if (this._app.ExistNotifications)
                return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerOps"].Value, notifications: this._app.GetAllNotifications), this._localizer);
            return Ok(retaux);
        }
        catch (System.Exception e)
        {
            return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
        }
    }
}