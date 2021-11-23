using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.AddressesCustomer;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.InteliMarketPlace.Repositories.CustomersRepositories;
using SessionsRepositories;

namespace InteliSystem.InteliMarketPlace.Api.Shopping.ServicesDependenceExtensions
{
    public static class ServiceDependenceExtension
    {
        public static void AddDependences(this IServiceCollection services, IConfiguration configuration)
        {
            //ConfigDb
            // services.AddScoped<IConnection>(s => new Connection(configuration.GetConnectionString("AzureDesenv")));
            services.AddScoped<IDbConnection>(s => new SqlConnection(configuration.GetConnectionString("AzureDesenv")));
            // //-------------
            // //Config Applications
            services.AddTransient<CustomersAppMaintenance>();
            services.AddTransient<LoginsAppMaintenance>();
            services.AddTransient<SessionAppMaintenance>();
            services.AddTransient<AddressesCustomerAppMaintenance>();
            // //-------------
            // //Config Repository
            services.AddTransient<ICustomersRepository, CustomersRepositories>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            //-------------
        }
    }
}