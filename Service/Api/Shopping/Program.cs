using System.Globalization;
using System.Text;
using InteliSystem.Utils.Authentications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("DefaultCache", new Microsoft.AspNetCore.Mvc.CacheProfile() { Duration = 3600, NoStore = false });
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
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
});

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(j =>
{
    j.RequireHttpsMetadata = true;
    j.SaveToken = true;
    j.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtKey.Get)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

var cultures = new List<CultureInfo> { new CultureInfo("pt-BR"), new CultureInfo("en-US") };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultures[0]);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddLocalization();

builder.Services.AddResponseCompression();

builder.Services.AddResponseCaching();

// builder.Services.AddDependences(configuration);

builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Ta Na Hora - Delivery",
        Version = "v1",
        Description = ""
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("swagger/v1/swagger.json", "shopping");
    s.RoutePrefix = string.Empty;
});
// }

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCaching();

app.UseResponseCompression();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapControllers();

app.Run();
