using API.Data;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();

        var connection = string.Empty;
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
            connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        }
        else
        {
            connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
        }

        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connection));

        // Add Swagger services
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  },
                  Scheme = "oauth2",
                  Name = "Bearer",
                  In = ParameterLocation.Header,

                },
                new List<string>()
              }
            });
        });

        services.AddCors();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIncomeCategoryRepository, IncomeCategoryRepository>();
        services.AddScoped<ICostCategoryRepository, CostCategoryRepository>();
        services.AddScoped<ICostRepository, CostRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ICostplanRepository, CostplanRepository>();
        services.AddScoped<IUserCurrencyRepository, UserCurrencyRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddHttpLogging(o =>
        {
            o.CombineLogs = true;
            o.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
        });

        return services;
    }
}
