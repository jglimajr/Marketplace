using System;
using System.Net;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using Microsoft.AspNetCore.Mvc;
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
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(CustomerApp))]
        public async Task<IActionResult> AddAsync([FromBody] CustomerApp customer)
        {
            try
            {
                var retaux = await this._app.AddAsync(customer);
                return Ok(retaux);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("update/v1/{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] CustomerProfile customer)
        {
            try
            {
                var retaux = await this._app.UpdateAsync(id, customer);
                return Ok(retaux);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("get/v1/{id?}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var retAux = await this._app.GetAsyn(id);
                return Ok(retAux);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}