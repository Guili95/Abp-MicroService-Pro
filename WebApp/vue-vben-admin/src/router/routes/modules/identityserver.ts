import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const identityserver: AppRouteModule = {
  path: '/identityserver',
  name: 'IdentityServer',
  component: LAYOUT,
  redirect: '/identityserver/clients',
  meta: {
    orderNo: 102,
    icon: 'ion:grid-outline',
    title: t('XyjIdentityServer.Permission:IdentityServer'),
    permission: [
      'IdentityServer.Client',
      'IdentityServer.IdentityResource',
      'IdentityServer.ApiResource',
      'IdentityServer.ApiScope',
    ],
  },
  children: [
    {
      path: 'clients',
      name: 'Clients',
      // component: () => import('/@/views/identityserver/clients/index.vue'),
      meta: {
        title: t('XyjIdentityServer.Permission:Clients'),
        permission: 'IdentityServer.Client',
      },
    },
    {
      path: 'identityresources',
      name: 'IdentityResources',
      // component: () => import('/@/views/identityserver/identityresources/index.vue'),
      meta: {
        title: t('XyjIdentityServer.Permission:IdentityResources'),
        permission: 'IdentityServer.IdentityResource',
      },
    },
    {
      path: 'apiresources',
      name: 'ApiResources',
      // component: () => import('/@/views/identityserver/apiresources/index.vue'),
      meta: {
        title: t('XyjIdentityServer.Permission:ApiResources'),
        permission: 'IdentityServer.ApiResource',
      },
    },
    {
      path: 'apiscope',
      name: 'ApiScope',
      // component: () => import('/@/views/identityserver/apiscope/index.vue'),
      meta: {
        title: t('XyjIdentityServer.Permission:ApiScope'),
        permission: 'IdentityServer.ApiScope',
      },
    },
  ],
};

export default identityserver;
