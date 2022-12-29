import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const identity: AppRouteModule = {
  path: '/identity',
  name: 'Identity',
  component: LAYOUT,
  redirect: '/identity/user',
  meta: {
    orderNo: 101,
    icon: 'ic:baseline-perm-identity',
    title: t('AbpIdentity.Permission:IdentityManagement'),
    permission: ['AbpIdentity.OrganizationUnits', 'AbpIdentity.Users', 'AbpIdentity.Roles'],
  },
  children: [
    {
      path: 'organizationunit',
      name: 'OrganizationUnit',
      component: () => import('/@/views/identity/organizationunit/index.vue'),
      meta: {
        title: t('AbpIdentity.Permission:OrganizationUnitManagement'),
        permission: ['AbpIdentity.OrganizationUnits'],
      },
    },
    {
      path: 'user',
      name: 'User',
      component: () => import('/@/views/identity/user/index.vue'),
      meta: {
        title: t('AbpIdentity.Permission:UserManagement'),
        permission: ['AbpIdentity.Users'],
      },
    },
    {
      path: 'role',
      name: 'Role',
      component: () => import('/@/views/identity/role/index.vue'),
      meta: {
        title: t('AbpIdentity.Permission:RoleManagement'),
        permission: ['AbpIdentity.Roles'],
      },
    },
    {
      path: 'securitylogs',
      name: 'SecurityLogs',
      component: () => import('/@/views/identity/role/index.vue'),
      meta: {
        title: '安全日志',
        // permission: ['AbpIdentity.Roles'],
      },
    },
  ],
};

export default identity;
