<<<<<<< HEAD
﻿using Guili.Shared.Hosting.AspNetCore;
using Guili.Shared.Hosting.Gateways;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Guili.BackendAdminAppGateway
{
    [DependsOn(
        typeof(GuiliSharedHostingGatewaysModule)
    )]
    public class GuiliBackendAdminAppGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            SwaggerConfigurationHelper.ConfigureWithAuth(
                context: context,
                authority: configuration["AuthServer:Authority"],
                scopes: new
                    Dictionary<string, string>
                    {
                        {"AccountService", "Account Service API"},
                        {"IdentityService", "Identity Service API"},
                        {"AdministrationService", "Administration Service API"},
                        {"SaasService", "Saas Service API"},
                    },
                apiTitle: "BackendAdminApp Gateway"
            );
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCorrelationId();
            app.UseAbpSerilogEnrichers();
            app.UseCors();
            app.UseAbpRequestLocalization();
            app.UseSwaggerUIWithYarp(context);

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^(|\\|\\s+)$", "/swagger"));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy(proxyPipeline => {
                    proxyPipeline.Use((context, next) =>
                    {
                        if (env.IsDevelopment())
                        {
                            const string CookieHeaderName = "Cookie";
                            var authCookieName = "Identity.Application";
                            var antiForgeryCookieName = "XSRF-TOKEN";
                            if (context.Request.Headers.TryGetValue(CookieHeaderName, out var cookies))
                            {
                                var newCookies = cookies.ToList();

                                newCookies.RemoveAll(x =>
                                    !string.IsNullOrWhiteSpace(authCookieName) && x.Contains(authCookieName) ||
                                    !string.IsNullOrWhiteSpace(antiForgeryCookieName) && x.Contains(antiForgeryCookieName));

                                context.Request.Headers.Remove(CookieHeaderName);
                            }
                        }
                        return next();
                    });
                });
            });
        }
    }
}
=======
﻿using Guili.Shared.Hosting.AspNetCore;
using Guili.Shared.Hosting.Gateways;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Guili.BackendAdminAppGateway
{
    [DependsOn(
        typeof(GuiliSharedHostingGatewaysModule)
    )]
    public class GuiliBackendAdminAppGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            SwaggerConfigurationHelper.ConfigureWithAuth(
                context: context,
                authority: configuration["AuthServer:Authority"],
                scopes: new
                    Dictionary<string, string>
                    {
                        {"AccountService", "Account Service API"},
                        {"IdentityService", "Identity Service API"},
                        {"AdministrationService", "Administration Service API"},
                        {"SaasService", "Saas Service API"},
                    },
                apiTitle: "BackendAdminApp Gateway"
            );
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCorrelationId();
            app.UseAbpSerilogEnrichers();
            app.UseCors();
            app.UseAbpRequestLocalization();
            app.UseSwaggerUIWithYarp(context);

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^(|\\|\\s+)$", "/swagger"));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy(proxyPipeline => {
                    proxyPipeline.Use((context, next) =>
                    {
                        if (env.IsDevelopment())
                        {
                            const string CookieHeaderName = "Cookie";
                            var authCookieName = "Identity.Application";
                            var antiForgeryCookieName = "XSRF-TOKEN";
                            if (context.Request.Headers.TryGetValue(CookieHeaderName, out var cookies))
                            {
                                var newCookies = cookies.ToList();

                                newCookies.RemoveAll(x =>
                                    !string.IsNullOrWhiteSpace(authCookieName) && x.Contains(authCookieName) ||
                                    !string.IsNullOrWhiteSpace(antiForgeryCookieName) && x.Contains(antiForgeryCookieName));

                                context.Request.Headers.Remove(CookieHeaderName);
                            }
                        }
                        return next();
                    });
                });
            });
        }
    }
}
>>>>>>> git/ids4
