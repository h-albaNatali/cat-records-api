using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Interfaces;


namespace MyProject.BackgroundJobs;

public static class BackgroundJobsServiceRegistration
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<MyProject.Application.Interfaces.IDataUpsertJob, DataUpsertJob>();
        return services;
    }
}
