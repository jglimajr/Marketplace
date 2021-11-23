using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.Utils.Authentications;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;
using Utils.Globals.Notifications;
using InteliSystem.InteliMarketPlace.Domains.Customers;

namespace InteliSystem.InteliMarketPlace.Applications.LoginsApp
{
    public class LoginsAppMaintenance : InteliNotification
    {

        private readonly ICustomersRepository _customerapp;

        public LoginsAppMaintenance(ICustomersRepository customerapp)
        => this._customerapp = customerapp;


        public Task<Return> Logon(Login login)
        {
            return Task.Run<Return>(() =>
            {
                Customer retAux = this._customerapp.GetByEMailAsync(login.EMail).GetAwaiter().GetResult();
                if (retAux.IsNull())
                {
                    this.AddNotification("Customer", "CustUserOrPass");
                    return new Return(ReturnValues.Failed, null);
                }


                if (login.PassWord != retAux.PassWord)
                {
                    this.AddNotification("Customer", "CustUserOrPass");
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new User()
                {
                    Id = retAux.Id,
                    Name = retAux.Name,
                    EMail = retAux.EMail,
                    Device = login.Device,
                    DateTimeCreate = retAux.DateTimeCreate
                });
            });
        }

    }
}