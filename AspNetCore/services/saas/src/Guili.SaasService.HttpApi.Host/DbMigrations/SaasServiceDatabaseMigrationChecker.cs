using Guili.SaasService.EntityFramework;
using Guili.Shared.Hosting.Microservices.DbMigrations.EfCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Guili.SaasService.DbMigrations
{
    public class SaasServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<SaasServiceDbContext>
    {
        public SaasServiceDatabaseMigrationChecker(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IAbpDistributedLock abpDistributedLock)
            : base(
                unitOfWorkManager,
                serviceProvider,
                currentTenant,
                distributedEventBus,
                abpDistributedLock,
                SaasServiceDbProperties.ConnectionStringName)
        {

        }
    }
}
