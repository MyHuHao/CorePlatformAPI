using Application.Commands;
using Application.Queries;
using Application.Services;
using Core.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // 注册基础服务
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped(typeof(IDapperExtensions<>), typeof(DapperExtensions<>));

        // 自动注册
        services.AutoRegisterServices();
    }

    private static void AutoRegisterServices(this IServiceCollection services)
    {
        //  自动注册工具类
        services.Scan(scan => scan
            .FromAssemblyOf<HashHelper>()
            .AddClasses(classes => classes.InNamespaces("Core.Helpers"))
            .AsSelf()
            .WithScopedLifetime());

        // 自动注册业务服务（接口+实现）
        services.Scan(scan => scan
            .FromAssemblyOf<ApiLogService>() // 确保类型在目标程序集中
            .AddClasses(classes => classes.InNamespaces("Application.Services"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // 自动注册CQRS查询
        services.Scan(scan => scan
            .FromAssemblyOf<UserQuery>()
            .AddClasses(classes => classes.InNamespaces("Application.Queries"))
            .AsSelf()
            .WithScopedLifetime());

        // 自动注册命令
        services.Scan(scan => scan
            .FromAssemblyOf<UserCommand>()
            .AddClasses(classes => classes.InNamespaces("Application.Commands"))
            .AsSelf()
            .WithScopedLifetime());

        // 自动注册仓储（接口+实现）
        services.Scan(scan => scan
            .FromAssemblyOf<ApiLogRepository>()
            .AddClasses(classes => classes.InNamespaces("Infrastructure.Data.Repositories"))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
    }
}