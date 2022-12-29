using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Guili.AdministrationService.EntityFrameworkCore
{
    [ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
    public class AdministrationServiceDbContext
        : AbpDbContext<AdministrationServiceDbContext>,
        IPermissionManagementDbContext,
        ISettingManagementDbContext,
        IAuditLoggingDbContext,
        IFeatureManagementDbContext
    {
        public AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options)
            : base(options)
        {

        }
        public DbSet<PermissionGrant> PermissionGrants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<FeatureValue> FeatureValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureAuditLogging();
        }
    }
}
