using MyProject.Worker;
using MyProject.BackgroundJobs;
using MyProject.Application;
using MyProject.Infrastructure;
using Serilog;
using Polly;
using Polly.Extensions.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up");

    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, config) =>
            config.ReadFrom.Configuration(context.Configuration))
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddApplication();
            services.AddInfrastructure(context.Configuration);
            services.AddBackgroundJobs();

            services.AddHttpClient("ExternalApi")
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
        })
        .Build();

    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
