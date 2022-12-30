using Autofac.Core;
using Guili;
using Guili.AdministrationService.EntityFrameworkCore;
using Guili.IdentityService.EntityFrameworkCore;
using Guili.Shared.Hosting.AspNetCore;
using Guili.Shared.Hosting.Microservices;
using IdentityServer4.Configuration;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.Emailing;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Jwt;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using IEmailSender = Volo.Abp.Emailing.IEmailSender;

namespace AuthServer
{
    [DependsOn(
        //typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        //typeof(AbpEventBusRabbitMqModule),
        //typeof(AbpBackgroundJobsRabbitMqModule),
        //typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        //typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpAccountApplicationModule),
        typeof(SharedHostingMicroserviceModule),
        typeof(GuiliSharedLocalizationModule),
        typeof(AdministrationServiceEntityFrameworkCoreModule),
        typeof(IdentityServiceEntityFrameworkCoreModule)
    )]
    public class AuthServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (!hostingEnvironment.IsDevelopment())
            {
                var configuration = context.Services.GetConfiguration();

                PreConfigure<AbpIdentityServerBuilderOptions>(options =>
                {
                    options.AddDeveloperSigningCredential = false;
                });

                PreConfigure<IIdentityServerBuilder>(builder =>
                {
                    builder.AddSigningCredential(GetSigningCertificate(hostingEnvironment));
                });
            }
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            context.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            ConfigureSameSiteCookiePolicy(context);
            ConfigureSwagger(context, configuration);

            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "AccountService";
                });

            Configure<IdentityServerOptions>(options => { options.IssuerUri = configuration["App:SelfUrl"]; });

            Configure<AbpAuditingOptions>(options => { options.ApplicationName = "AuthServer"; });

            Configure<IdentityServerOptions>(options =>
            {
                options.UserInteraction.LoginUrl = configuration["IdentityServerOptions:LoginUrl"];
                options.UserInteraction.ConsentUrl = "/Consent";
                options.UserInteraction.ErrorUrl = configuration["IdentityServerOptions:ErrorUrl"];
                options.UserInteraction.LogoutUrl = configuration["IdentityServerOptions:LogoutUrl"];
            });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["AuthServer"].RootUrl = configuration["App:SelfUrl"];
            });

            #if DEBUG
                context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
            #endif
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Headers.ContainsKey("from-ingress"))
                {
                    ctx.SetIdentityServerOrigin(configuration["App:SelfUrl"]);
                }

                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }
            app.UseForwardedHeaders();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();
            app.UseAbpSerilogEnrichers();
            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Service API");
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            });
            app.UseAuditing();
            app.UseConfiguredEndpoints();
        }
        private X509Certificate2 GetSigningCertificate(IWebHostEnvironment hostingEnv)
        {
            const string fileName = "guili-authserver.pfx";
            const string passPhrase = "www.guili.co";
            var file = Path.Combine(hostingEnv.ContentRootPath, fileName);

            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"Signing Certificate couldn't found: {file}");
            }

            return new X509Certificate2(file, passPhrase);
        }
        private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
        {
            SwaggerConfigurationHelper.ConfigureWithAuth(
                context: context,
                authority: configuration["AuthServer:Authority"],
                scopes: new Dictionary<string, string>
                {
                    {
                        "AccountService",
                        "Account Service API"
                    },
                },
                apiTitle: "Account Service API"
            );
        }
        private void ConfigureSameSiteCookiePolicy(ServiceConfigurationContext context)
        {
            context.Services.AddSameSiteCookiePolicy();
        }
    }
}
