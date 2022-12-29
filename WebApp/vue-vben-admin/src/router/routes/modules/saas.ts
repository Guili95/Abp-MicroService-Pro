import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const saas: AppRouteModule = {
  path: '/saas',
  name: 'Saas',
  component: LAYOUT,
  redirect: '/saas/tenants',
  meta: {
    orderNo: 100,
    icon: 'akar-icons:people-group',
    title: t('AbpTenantManagement.Menu:TenantManagement'),
    permission: ['AbpTenantManagement.Tenants'],
  },
  children: [
    {
      path: 'tenants',
      name: 'Tenants',
      component: () => import('/@/views/saas/tenants/index.vue'),
      meta: {
        title: t('AbpTenantManagement.Permission:TenantManagement'),
        permission: ['AbpTenantManagement.Tenants'],
      },
    },
  ],
};

export default saas;
