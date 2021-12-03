using System.Data;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Applications.LoginsApp;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.InteliMarketPlace.Repositories.CustomersRepositories;
using Microsoft.Data.SqlClient;
using SessionsRepositories;

namespace Shopping.Extensions
{
    public static class ServiceDependenceExtension
    {
        public static void AddDependences(this IServiceCollection services, IConfiguration configuration)
        {
            //ConfigDb
            // services.AddScoped<IConnection>(s => new Connection(configuration.GetConnectionString("AzureDesenv")));
            services.AddScoped<IDbConnection>(s => new SqlConnection(configuration.GetConnectionString("AzureDesenv")));
            //-------------
            //Config Applications
            services.AddTransient<CustomersAppMaintenance>();
            services.AddTransient<LoginsAppMaintenance>();
            services.AddTransient<SessionAppMaintenance>();
            //-------------
            //Config Repository
            services.AddTransient<ICustomersRepository, CustomersRepositories>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            //-------------
        }
    }
}