import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const setting: AppRouteModule = {
  path: '/setting',
  name: 'Setting',
  component: LAYOUT,
  redirect: '/setting/management',
  meta: {
    orderNo: 110,
    icon: 'ep:setting',
    title: t('AbpSettingManagement.Settings'),
    hideChildrenInMenu: true,
    // permission: ['AbpTenantManagement.Tenants'],
  },
  children: [
    {
      path: 'management',
      name: 'ManageMent',
      component: () => import('/@/views/identity/organizationunit/index.vue'),
      meta: {
        title: t('AbpSettingManagement.Settings'),
        // permission: ['AbpIdentity.OrganizationUnits'],
      },
    },
  ],
};

export default setting;
