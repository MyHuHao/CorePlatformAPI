using System.Text;
using System.Text.Json;
using API.Filters;
using API.Middlewares;
using Application.DependencyInjection;
using Application.Mappings;
using Dapper;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 启用 Dapper ORM 的自动列名映射功能
DefaultTypeMap.MatchNamesWithUnderscores = true;

// 获取JWT配置
var configuration = builder.Configuration;
var jwtSettings = configuration.GetSection("JWT");

// 日志记录相关
builder.Host.UseSerilog((ctx, config) => config
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4194304;
    logging.CombineLogs = true;
});

// 添加配置
builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); }).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

// 注册服务
builder.Services.AddScoped<GlobalExceptionFilter>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ApiLogProfile).Assembly);

// 认证
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = jwtSettings["ValidIssuer"],
            ValidateAudience = false,
            ValidAudience = jwtSettings["ValidAudience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["IssuerSigningKey"] ?? ""))
        };
    });

// 跨域
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Content-Disposition")));

// Redis

// 通知服务

// 国际化

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger/index.html", true);
            return;
        }
        await next();
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ApiLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CorePlatformAPI");
    c.RoutePrefix = "swagger";
});
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TokenValidationMiddleware>();
app.MapControllers();
app.Run();