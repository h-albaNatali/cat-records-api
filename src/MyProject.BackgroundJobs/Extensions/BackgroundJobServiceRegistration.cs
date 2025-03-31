using Microsoft.Extensions.DependencyInjection;
using MyProject.BackgroundJobs.Jobs;
using MyProject.Application.Interfaces;

namespace MyProject.BackgroundJobs.Extensions;

public static class BackgroundJobServiceRegistration
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<IDataUpsertJob, DataUpsertJob>();
        return services;
    }
}
