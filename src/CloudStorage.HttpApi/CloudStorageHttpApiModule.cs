using System.Text;
using CloudStorage.Application.Helpers;
using CloudStorage.Application.Modules;
using CloudStorage.HttpApi.filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using CloudStorage.Domain.Shared;
using MessagePack;
using NSwag;
using NSwag.Generation.Processors.Security;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace CloudStorage.HttpApi;

public class CloudStorageHttpApiModule : AbpModule
{
    public override async Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        if (!Directory.Exists(Constants.CloudStorageRoot))
        {
            Directory.CreateDirectory(Constants.CloudStorageRoot);
        }

        ConfigureAuthentication(context, context.Services.GetConfiguration());
        ConfigureCors(context, context.Services.GetConfiguration());
        await ConfigureRedis(context);
        ConfigureSwaggerServices(context);
        ConfigurationMvc(context);
        ConfigureSignalr(context.Services);
    }

    private void ConfigurationMvc(ServiceConfigurationContext context)
    {
        context.Services.AddMvc(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter), 1);
                o.Filters.Add(typeof(GlobalResponseFilter), 1);
                o.Filters.Add(typeof(GlobalModelStateValidationFilter), 1);
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
    }

    private void ConfigureSignalr(IServiceCollection services)
    {
        services
            .AddSignalR(x =>
            {
                // 传输缓存
                x.StreamBufferCapacity = (64 * 1024);
                x.MaximumReceiveMessageSize = (64 * 1024);
            })
            .AddRedis(services.GetConfiguration()["Redis:Configuration"])
            .AddJsonProtocol()
            .AddMessagePackProtocol(x =>
            {
                x.SerializerOptions = MessagePackSerializerOptions.Standard
                    .WithSecurity(MessagePackSecurity.UntrustedData);
            });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
    {
        context.Services.AddSwaggerDocument(config =>
        {
            config.PostProcess = document =>
            {
                document.BasePath = "cloud-storage";
                document.Info.Title = "Cloud Api";
                document.Info.Description = "云盘api";
            };

            config.OperationProcessors.Add(new OperationSecurityScopeProcessor("cloud-storage"));
            config.DocumentProcessors.Add(new SecurityDefinitionAppender("cloud-storage",
                new NSwag.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Bearer Token",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey
                }));
        });
    }

    private static async Task ConfigureRedis(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration()["Redis:Configuration"];
        RedisHelper.Initialization(new CSRedis.CSRedisClient(configuration));
        await NameSuffix.SetIconAsync();
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(nameof(TokenOptions));
        var tokenOptions = configurationSection.Get<TokenOptions>();
        if (string.IsNullOrEmpty(tokenOptions.Issuer))
            throw new Exception("未设置JWT权限配置");
        context.Services.Configure<TokenOptions>(configurationSection);
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //是否在令牌期间验证签发者
                    ValidateAudience = true, //是否验证接收者
                    ValidateLifetime = true, //是否验证失效时间
                    ValidateIssuerSigningKey = true, //是否验证签名
                    ValidAudience = tokenOptions.Audience, //接收者
                    ValidIssuer = tokenOptions.Issuer, //签发者，签发的Token的人
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        // 添加signalr的token 因为signalr的token在请求头上所以需要设置
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(SignalRConstants.FileStream))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsBuilder =>
            {
                corsBuilder.SetIsOriginAllowed((string _) => true).AllowAnyMethod().AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        app.UseCorrelationId();

        app.UseRouting();
        app.UseCors("CorsPolicy"); //CORS strategy
        app.UseAuditing();

        app.UseAuthentication();
    }
}