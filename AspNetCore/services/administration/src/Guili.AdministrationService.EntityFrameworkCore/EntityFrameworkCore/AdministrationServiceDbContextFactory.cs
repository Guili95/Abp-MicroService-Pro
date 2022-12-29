﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Guili.AdministrationService.EntityFrameworkCore
{
    public class AdministrationServiceDbContextFactory : IDesignTimeDbContextFactory<AdministrationServiceDbContext>
    {
        public AdministrationServiceDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AdministrationServiceDbContext>()
                .UseMySql(GetConnectionStringFromConfiguration(), ServerVersion.AutoDetect(GetConnectionStringFromConfiguration()), b =>
                {
                    b.MigrationsHistoryTable("__AdministrationService_Migrations");
                });

            return new AdministrationServiceDbContext(builder.Options);
        }

        private static string GetConnectionStringFromConfiguration()
        {
            return BuildConfiguration()
                .GetConnectionString(AdministrationServiceDbProperties.ConnectionStringName);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        $"..{Path.DirectorySeparatorChar}Guili.AdministrationService.HttpApi.Host"
                    )
                )
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
