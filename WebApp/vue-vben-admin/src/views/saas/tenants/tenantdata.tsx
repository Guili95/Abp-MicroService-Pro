import { FormSchema, BasicColumn } from '/@/components/Table';

import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();

export const tenantFormSchema: FormSchema[] = [
  {
    field: 'Id',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
  {
    field: 'Name',
    label: t('AbpTenantManagement.DisplayName:TenantName'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.saas.tenants.tenantNamePlaceholder'),
    },
    rules: [
      {
        required: true,
        message: t('routes.saas.tenants.tenantNamePlaceholder'),
      },
      {
        max: 64,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpTenantManagement.DisplayName:TenantName'),
          64,
        ]),
      },
    ],
  },
  {
    field: 'AdminEmailAddress',
    label: t('AbpTenantManagement.DisplayName:AdminEmailAddress'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.saas.tenants.adminEmailAddressPlaceholder'),
    },
    ifShow: ({ values }) => {
      return !values.Id;
    },
    rules: [
      {
        required: true,
        validator: async (_rule, value) => {
          const reg =
            /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          if (!value) {
            return Promise.reject(t('routes.saas.tenants.adminEmailAddressPlaceholder'));
          } else if (!reg.test(value)) {
            return Promise.reject(t('AbpTenantManagement.ThisFieldIsNotAValidEmailAddress'));
          }
          return Promise.resolve();
        },
        trigger: 'change',
      },
      {
        max: 256,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpTenantManagement.DisplayName:AdminEmailAddress'),
          256,
        ]),
      },
    ],
  },
  {
    field: 'AdminPassword',
    label: t('AbpTenantManagement.DisplayName:AdminPassword'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.saas.tenants.adminPasswordPlaceholder'),
    },
    ifShow: ({ values }) => {
      return !values.Id;
    },
    rules: [
      {
        required: true,
        message: t('routes.saas.tenants.adminPasswordPlaceholder'),
      },
      {
        max: 128,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpTenantManagement.DisplayName:AdminPassword'),
          128,
        ]),
      },
    ],
  },
  {
    field: 'ConcurrencyStamp',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
];

export function getBasicColumns(): BasicColumn[] {
  return [
    {
      title: t('AbpTenantManagement.DisplayName:TenantName'),
      dataIndex: 'Name',
      sorter: true,
    },
  ];
}

export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: t('common.search'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.saas.tenants.searchPlaceholder'),
    },
  },
];
