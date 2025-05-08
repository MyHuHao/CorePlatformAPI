using Application.Commands;
using Application.Interfaces;
using Application.Queries;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // 注册基础服务
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped(typeof(IDapperExtensions<>), typeof(DapperExtensions<>));

        // 注册业务服务
        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserService, UserService>();
        
        // 注册CQRS 
        services.AddScoped<UserQuery>();
        
        // 注册命令
        services.AddScoped<UserCommand>();
        
        // 注册数据仓储
        services.AddScoped<IApiLogRepository, ApiLogRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        
        
        
        
        
    }
}