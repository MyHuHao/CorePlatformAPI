using System.Globalization;
using API.Auth;
using API.Middlewares;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Redis;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// 添加配置和服务
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));
builder.Services.AddScoped<DictionaryService>();
builder.Services.AddScoped<AuthService>();

// 日志记录相关
builder.Services.AddScoped<ILoginLogRepository, LoginLogRepository>();
builder.Services.AddScoped<IOperationLogRepository, OperationLogRepository>();
// 国际化
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// 认证
builder.Services.AddAuthentication("MyScheme")
    .AddScheme<AuthenticationSchemeOptions, MyAuthenticationHandler>("MyScheme", null);
// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Configuration"];
});
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"] ??
                                  throw new InvalidOperationException()));
builder.Services.AddScoped<IOnlineUserService, OnlineUserService>();
// 通知服务
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHttpClient<IDingTalkService, DingTalkService>();
builder.Services.AddHostedService<NotificationScheduler>();
// 本地化
var supportedCultures = new[] { new CultureInfo("zh-CN"), new CultureInfo("en-US") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("zh-CN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalLoggingMiddleware>();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();