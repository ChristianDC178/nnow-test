using Microsoft.Extensions.DependencyInjection;
using Nnow.Application.Events.Clients;
using Nnow.Database;
using System.Reflection;

namespace Nnow.Application;

public static class RegisterServices
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<UnitOfWork>();
        services.AddScoped<NnowContext>();
        services.AddTransient<ElasticSearchClientWrapper>();
        services.AddTransient<KafkaClientWrapper>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

}