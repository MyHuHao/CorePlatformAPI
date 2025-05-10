using Application.Commands;
using Application.Queries;
using Application.Services;
using Core.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddCoreHelpers();
        services.AddApplicationServices();
        services.AddQueries();
        services.AddCommands();
    }

    private static void AddCoreHelpers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<HashHelper>()
            .AddClasses(classes => classes.InNamespaces("Core.Helpers"))
            .AsSelf()
            .WithScopedLifetime());
    }

    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<ApiLogService>()
            .AddClasses(classes => classes.InNamespaces("Application.Services"))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static void AddQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<UserQuery>()
            .AddClasses(classes => classes.InNamespaces("Application.Queries"))
            .AsSelf()
            .WithScopedLifetime());
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<UserCommand>()
            .AddClasses(classes => classes.InNamespaces("Application.Commands"))
            .AsSelf()
            .WithScopedLifetime());
    }
}