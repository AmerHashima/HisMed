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

namespace HIS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<HISDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)));

        // ====================================
        // Register repositories - Core System
        // ====================================
        services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // ====================================
        // Register repositories - Lookups
        // ====================================
        services.AddScoped<IAppLookupMasterRepository, AppLookupMasterRepository>();
        services.AddScoped<IAppLookupDetailRepository, AppLookupDetailRepository>();

        // ====================================
        // Register repositories - Hospital Structure
        // ====================================
        services.AddScoped<IHospitalBranchRepository, HospitalBranchRepository>();
        services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

        // ====================================
        // Register repositories - Medical Staff
        // ====================================
        services.AddScoped<IDoctorRepository, DoctorRepository>();

        // ====================================
        // Register repositories - Patients
        // ====================================
        services.AddScoped<IPatientRepository, PatientRepository>();

        // ====================================
        // Register repositories - Appointments & Encounters
        // ====================================
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IEncounterRepository, EncounterRepository>();

        // ====================================
        // Register AutoMapper
        // ====================================
        services.AddAutoMapper(cfg => {
            // Optional: Add custom mapper configurations here
        }, typeof(SystemUserProfile).Assembly);

        // ====================================
        // Register Infrastructure Services
        // ====================================
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IQueryBuilderService, QueryBuilderService>();
        services.AddScoped<IPatientValidationService, PatientValidationService>();

        return services;
    }
}