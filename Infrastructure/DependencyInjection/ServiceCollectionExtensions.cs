using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddRepositories();
    }

    private static void AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped(typeof(IDapperExtensions<>), typeof(DapperExtensions<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<ApiLogRepository>()
            .AddClasses(classes => classes.InNamespaces("Infrastructure.Data.Repositories"))
            .AsMatchingInterface()
            .WithScopedLifetime());
    }
}