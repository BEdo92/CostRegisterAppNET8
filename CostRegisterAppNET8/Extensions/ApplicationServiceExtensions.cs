using CostRegisterAppNET8.Data;
using CostRegisterAppNET8.Interfaces;
using CostRegisterAppNET8.Repositories;
using CostRegisterAppNET8.Services;
using Microsoft.EntityFrameworkCore;

namespace CostRegisterAppNET8.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("SQL_CONNECTION_STRING environment variable is not set.");
        }

        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddCors();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIncomeCategoryRepository, IncomeCategoryRepository>();
        services.AddScoped<ICostCategoryRepository, CostCategoryRepository>();
        services.AddScoped<ICostRepository, CostRepository>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
