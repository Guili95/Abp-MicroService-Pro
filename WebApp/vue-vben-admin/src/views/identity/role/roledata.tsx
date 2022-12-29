import { BasicColumn } from '/@/components/Table/src/types/table';
import { FormSchema } from '/@/components/Table';
import { Tag } from 'ant-design-vue';

import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();

export function getBasicColumns(): BasicColumn[] {
  return [
    {
      title: t('AbpIdentity.DisplayName:RoleName'),
      dataIndex: 'Name',
      sorter: true,
    },
    {
      title: t('AbpIdentity.DisplayName:IsDefault'),
      dataIndex: 'IsDefault',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? <Tag color="#87d068">{t('AbpIdentity.DisplayName:IsDefault')}</Tag> : null;
      },
    },
    {
      title: t('AbpIdentity.DisplayName:IsPublic'),
      dataIndex: 'IsPublic',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? <Tag color="#108ee9">{t('AbpIdentity.DisplayName:IsPublic')}</Tag> : null;
      },
    },
  ];
}

export function getOrganizationUnitBasicColumns(): BasicColumn[] {
  return [
    {
      title: t('AbpIdentity.DisplayName:RoleName'),
      dataIndex: 'Name',
      sorter: true,
    },
  ];
}

export const searchFormSchema: FormSchema[] = [
  {
    field: 'Filter',
    label: t('common.search'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.roles.searchPlaceholder'),
    },
  },
];

export const roleFormSchema: FormSchema[] = [
  {
    field: 'Id',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
  {
    field: 'Name',
    label: t('AbpIdentity.DisplayName:RoleName'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.roles.roleNamePlaceholder'),
    },
    dynamicDisabled: ({ values }) => {
      return !!values.IsStatic;
    },
    rules: [
      {
        required: true,
        message: t('routes.identity.roles.roleNamePlaceholder'),
      },
      {
        max: 256,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:RoleName'),
          256,
        ]),
      },
    ],
  },
  {
    field: 'IsDefault',
    label: '',
    component: 'CheckboxGroup',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: t('AbpIdentity.DisplayName:IsDefault'),
          value: true,
        },
      ],
    },
    defaultValue: [false],
  },
  {
    field: 'IsPublic',
    label: '',
    component: 'CheckboxGroup',
    colProps: { span: 24 },
    componentProps: {
      options: [
        {
          label: t('AbpIdentity.DisplayName:IsPublic'),
          value: true,
        },
      ],
    },
    defaultValue: [false],
  },
  {
    field: 'IsStatic',
    label: '',
    component: 'Checkbox',
    show: false,
    defaultValue: false,
  },
  {
    field: 'ConcurrencyStamp',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
];
