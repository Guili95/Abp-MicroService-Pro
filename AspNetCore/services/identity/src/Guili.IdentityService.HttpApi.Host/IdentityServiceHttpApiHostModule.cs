using Guili.IdentityService.DbMigrations;
using Guili.IdentityService.EntityFrameworkCore;
using Guili.Shared.Hosting.AspNetCore;
using Guili.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Guili.IdentityService
{
    [DependsOn(
        typeof(SharedHostingMicroserviceModule),
        typeof(IdentityServiceEntityFrameworkCoreModule),
        typeof(IdentityServiceApplicationModule),
        typeof(IdentityServiceHttpApiModule)
    )]
    public class IdentityServiceHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            JwtBearerConfigurationHelper.Configure(context, "IdentityService");

            SwaggerConfigurationHelper.ConfigureWithAuth(
                context: context,
                authority: configuration["AuthServer:Authority"],
                scopes: new
                    Dictionary<string, string>
                    {
                        {"IdentityService", "Identity Service API"}
                    },
                apiTitle: "Identity Service API"
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
            app.UseCors();
            app.UseAbpRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            if (Convert.ToBoolean(configuration["IsMultiTenancyEnabled"]))
            {
                app.UseMultiTenancy();
            }
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service API");
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            });
            app.UseAbpSerilogEnrichers();
            app.UseAuditing();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
            });
        }
        public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            await context.ServiceProvider
                .GetRequiredService<IdentityServiceDatabaseMigrationChecker>()
                .CheckAndApplyDatabaseMigrationsAsync();
        }
    }
}
