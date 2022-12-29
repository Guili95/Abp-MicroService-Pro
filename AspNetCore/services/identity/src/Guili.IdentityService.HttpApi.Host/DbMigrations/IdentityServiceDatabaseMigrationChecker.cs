<<<<<<< HEAD
﻿using Guili.IdentityService.EntityFrameworkCore;
using Guili.Shared.Hosting.Microservices.DbMigrations.EfCore;
using System;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Guili.IdentityService.DbMigrations
{
    public class IdentityServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<IdentityServiceDbContext>
    {
        public IdentityServiceDatabaseMigrationChecker(
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
            IdentityServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
=======
﻿using Guili.IdentityService.EntityFrameworkCore;
using Guili.Shared.Hosting.Microservices.DbMigrations.EfCore;
using System;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Guili.IdentityService.DbMigrations
{
    public class IdentityServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<IdentityServiceDbContext>
    {
        public IdentityServiceDatabaseMigrationChecker(
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
            IdentityServiceDbProperties.ConnectionStringName)
        {
        }
    }
}
>>>>>>> git/ids4
