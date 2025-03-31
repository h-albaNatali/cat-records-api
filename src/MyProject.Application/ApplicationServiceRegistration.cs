using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Interfaces;
using MyProject.Application.Services;

namespace MyProject.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDataService, DataService>();
        return services;
    }
}
