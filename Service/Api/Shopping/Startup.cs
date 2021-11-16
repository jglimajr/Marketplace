using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shopping.Extensions;

namespace Shopping
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.CacheProfiles.Add("DefaultCache", new CacheProfile() { Duration = 3600, NoStore = false });
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });



            var cultures = new List<CultureInfo> { new CultureInfo("pt-BR"), new CultureInfo("en-US"), new CultureInfo("pt-PT"),
                                                  new CultureInfo("es-MX"), new CultureInfo("es-AR"), new CultureInfo("es-CL") };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultures[0]);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.AddLocalization();
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddDependences(configuration: Configuration);
            services.AddCors();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "InteliMarketplace",
                    Version = "v1",
                    Description = ""
                });
                s.EnableAnnotations();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // var suportedCultures = new[] { "pt-BR", "en-US", "it", "pt-PT", "es-MX", "es-AR", "es-CL" };

            // var localizationOptions = new RequestLocalizationOptions()
            //         .SetDefaultCulture(suportedCultures[0])
            //         .AddSupportedCultures(suportedCultures)
            //         .AddSupportedUICultures(suportedCultures);

            // app.UseRequestLocalization(localizationOptions);

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseResponseCompression();

            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "InteliMarketplace");
                s.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
