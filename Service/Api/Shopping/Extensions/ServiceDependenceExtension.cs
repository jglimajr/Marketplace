using System.Data;
using InteliSystem.Connection;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Repositories.CustomersRepositories;
using InteliSystem.Utils.Globals.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Extensions
{
    public static class ServiceDependenceExtension
    {
        public static void AddDependences(this IServiceCollection services, IConfiguration configuration)
        {
            //ConfigDb
            services.AddScoped<IConnection>(s => new Connection(configuration.GetConnectionString("AzureDesenv")));
            services.AddScoped<IDbConnection>(s => new SqlConnection(configuration.GetConnectionString("AzureDesenv")));
            //-------------
            //Config Applications
            services.AddTransient<CustomersAppMaintenance>();
            //-------------
            //Config Repository
            services.AddTransient<ICustomersRepository, CustomersRepositories>();
            //-------------
        }
    }
}