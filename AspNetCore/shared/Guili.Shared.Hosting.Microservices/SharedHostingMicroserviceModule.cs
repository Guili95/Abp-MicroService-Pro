using Guili.AdministrationService.EntityFrameworkCore;
using Guili.Shared.Hosting.AspNetCore;
using Guili.Shared.Hosting.Microservices.AntiForgery;
using Guili.Shared.Hosting.Microservices.WrapResult;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Guili.Shared.Hosting.Microservices
{
    [DependsOn(
        typeof(GuiliSharedHostingAspNetCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AdministrationServiceEntityFrameworkCoreModule),
        typeof(AbpDistributedLockingModule),
        typeof(GuiliSharedLocalizationModule)
    )]
    public class SharedHostingMicroserviceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = Convert.ToBoolean(configuration["IsMultiTenancyEnabled"]);
            });

            //netCore 防伪选项
            Configure<AntiforgeryOptions>(options =>
            {
                //HTTP 标头。默认命名：RequestVerificationToke（如果更改，前端也要改）
                options.HeaderName = "RequestVerificationToken";
                options.Cookie.Domain = "threebody.shop";
            });

            //abp 防伪选项
            Configure<AbpAntiForgeryOptions>(options =>
            {
                // //用于在客户端存储防伪令牌值
                options.TokenCookie.Name = "XSRF-TOKEN";
                options.TokenCookie.SameSite = SameSiteMode.Lax;
                //将防伪令牌到期时间设置为 1 年。默认10年
                options.TokenCookie.Expiration = TimeSpan.FromDays(365);
                //应用程序使用的身份验证 cookie 的名称。默认值为 Identity.Application
                options.AuthCookieSchemaName = "Identity.Application";
                options.TokenCookie.Domain = "threebody.shop";
                ////忽略指定命名空间中的控制器类型
                //options.AutoValidateFilter = type => !type.Namespace.StartsWith("MyProject.MyIgnoredNamespace");
                ////默认“GET”、“HEAD”、“TRACE”、“OPTIONS”
                //options.AutoValidateIgnoredHttpMethods.Remove("GET");
            });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "GuiliAbpMicroServicePro:";
            });

            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "GuiliAbpMicroServicePro-Protection-Keys");

            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            });

            //时区配置
            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });

            //添加统一返回格式过滤器
            context.Services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ResultFilter));
                options.Filters.Add(typeof(ResultExceptionFilter));
                options.Filters.Add(typeof(AbpAntiforgeryTokenActionFilter));
            });
        }
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            PostConfigure<JsonOptions>(options =>
            {
                //原样返回JSON，首字母不小写
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            PostConfigure<AbpJsonOptions>(options =>
            {
                //设置时间格式
                options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            });
        }
    }
}
