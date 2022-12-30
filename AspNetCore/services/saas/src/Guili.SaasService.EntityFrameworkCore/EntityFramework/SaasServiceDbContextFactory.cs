using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Guili.SaasService.EntityFramework
{
    public class SaasServiceDbContextFactory : IDesignTimeDbContextFactory<SaasServiceDbContext>
    {
        public SaasServiceDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<SaasServiceDbContext>()
                .UseMySql(GetConnectionStringFromConfiguration(), ServerVersion.AutoDetect(GetConnectionStringFromConfiguration()), b =>
                {
                    b.MigrationsHistoryTable("__SaasService_Migrations");
                });

            return new SaasServiceDbContext(builder.Options);
        }

        private static string GetConnectionStringFromConfiguration()
        {
            return BuildConfiguration()
                .GetConnectionString(SaasServiceDbProperties.ConnectionStringName);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        $"..{Path.DirectorySeparatorChar}Guili.SaasService.HttpApi.Host"
                    )
                )
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
