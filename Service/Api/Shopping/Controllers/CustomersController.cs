using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;
using Shopping.Util.CustomMessages;
using Swashbuckle.AspNetCore.Annotations;

namespace InteliSystem.InteliMarketPlace.Api.Shopping.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomersAppMaintenance _app;
    private readonly IStringLocalizer<Messages> _localizer;
    public CustomersController(CustomersAppMaintenance app, [FromServices] IStringLocalizer<Messages> localizer)
    {
        this._app = app;
        this._localizer = localizer;
    }
    [HttpPost]
    [Route("add/v1")]
    [SwaggerOperation(summary: "Customer", description: "CRUD of Customers")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(CustomerProfile))]
    [Authorize(Roles = "customer, Manager")]
    public async Task<IActionResult> AddAsync([FromBody] CustomerApp customer)
    {
        try
        {
            var retaux = await this._app.AddAsync(customer);
            if (this._app.ExistNotifications)
                return InteliCustomMessage.MountMessage(new Message(title: this._localizer["CustomerAdd"].Value, notifications: this._app.GetAllNotifications), this._localizer);
            return Ok(retaux);
        }
        catch (System.Exception e)
        {
            return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
        }
    }
    [HttpPut]
    [Route("update/v1/{id}")]
    [Authorize(Roles = "customer")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] CustomerProfile customer)
    {
        try
        {
            var retaux = await this._app.UpdateAsync(id, customer);
            if (this._app.ExistNotifications)
                return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerOps"].Value, notifications: this._app.GetAllNotifications), this._localizer);
            return Ok(retaux);
        }
        catch (System.Exception e)
        {
            return InteliCustomMessage.MountMessage(new Message(title: this._localizer["GerException"].Value, exception: e), this._localizer);
        }
    }

    [HttpGet]
    [Route("get/v1/{id?}")]
    [Authorize(Roles = "customer, Manager")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var retAux = await this._app.GetAsync(id);
            return Ok(retAux);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    [HttpGet]
    [Route("getall/v1/{id?}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var retAux = await this._app.GetAllAsync();
            return Ok(retAux);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
