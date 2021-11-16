using System.Net;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Shopping.Resources;
using Shopping.Util.CustomMessages;
using Swashbuckle.AspNetCore.Annotations;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly CustomersAppMaintenance _app;
        public CustomersController(CustomersAppMaintenance app)
        {
            this._app = app;
        }
        [HttpPost]
        [Route("add/v1")]
        [SwaggerOperation(summary: "Customer", description: "CRUD of Customers")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(CustomerProfile))]
        public async Task<IActionResult> AddAsync([FromBody] CustomerApp customer, [FromServices] IStringLocalizer<Messages> localizer)
        {
            try
            {
                var retaux = await this._app.AddAsync(customer);
                if (this._app.ExistNotifications)
                    return InteliCustomMessage.MountMessage(new Message(title: localizer["CustomerAdd"].Value, notifications: this._app.GetAllNotifications), localizer);
                return Ok(retaux);
            }
            catch (System.Exception e)
            {
                return InteliCustomMessage.MountMessage(new Message(title: localizer["GerException"].Value, exception: e), localizer);
            }
        }
        [HttpPut]
        [Route("update/v1/{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] CustomerProfile customer, [FromServices] IStringLocalizer<Messages> localizer)
        {
            try
            {
                var retaux = await this._app.UpdateAsync(id, customer);
                if (this._app.ExistNotifications)
                    return InteliCustomMessage.MountMessage(new Message(title: localizer["GerOps"].Value, notifications: this._app.GetAllNotifications), localizer);
                return Ok(retaux);
            }
            catch (System.Exception e)
            {
                return InteliCustomMessage.MountMessage(new Message(title: localizer["GerException"].Value, exception: e), localizer);
            }
        }

        [HttpGet]
        [Route("get/v1/{id?}")]
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
}