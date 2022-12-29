using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Guili.IdentityService.EntityFrameworkCore
{
    public class IdentityServiceDbContextFactory : IDesignTimeDbContextFactory<IdentityServiceDbContext>
    {
        public IdentityServiceDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<IdentityServiceDbContext>()
                .UseMySql(GetConnectionStringFromConfiguration(), ServerVersion.AutoDetect(GetConnectionStringFromConfiguration()), b =>
                {
                    b.MigrationsHistoryTable("__IdentityService_Migrations");
                });

            return new IdentityServiceDbContext(builder.Options);
        }

        private static string GetConnectionStringFromConfiguration()
        {
            return BuildConfiguration()
                .GetConnectionString(IdentityServiceDbProperties.ConnectionStringName);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        $"..{Path.DirectorySeparatorChar}Guili.IdentityService.HttpApi.Host"
                    )
                )
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
