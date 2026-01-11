using HIS.Application.Interfaces;
using HIS.Application.Mappings;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using HIS.Infrastructure.Repositories;
using HIS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HIS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<HISDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register repositories
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppLookupMasterRepository, AppLookupMasterRepository>();
        services.AddScoped<IAppLookupDetailRepository, AppLookupDetailRepository>();
        
        services.AddAutoMapper(cfg => {
            // optional: extra config
        }, typeof(SystemUserProfile).Assembly);
        
        // Register services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IQueryBuilderService, QueryBuilderService>(); // This line is important

        return services;
    }
}