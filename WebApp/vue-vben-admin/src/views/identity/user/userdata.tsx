import { BasicColumn } from '/@/components/Table/src/types/table';
import { FormSchema } from '/@/components/Table';
import dayjs from 'dayjs';
import { Icon } from '/@/components/Icon';

import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();

export function getBasicColumns(): BasicColumn[] {
  return [
    {
      title: t('AbpIdentity.DisplayName:UserName'),
      dataIndex: 'UserName',
      customRender: ({ record }: { record: any }) => {
        return (
          <div>
            {!record.IsActive ? <Icon icon="el:ban-circle" color="red" /> : ''}
            {record.LockoutEnabled &&
            record.LockoutEnd != null &&
            dayjs(record.LockoutEnd) > dayjs() ? (
              <Icon icon="ant-design:lock-outlined" color="red" />
            ) : (
              ''
            )}
            {record.UserName}
          </div>
        );
      },
      sorter: true,
    },
    {
      title: t('AbpIdentity.DisplayName:Surname') + t('AbpIdentity.DisplayName:Name'),
      dataIndex: 'Name',
      sorter: true,
    },
    {
      title: t('AbpIdentity.EmailAddress'),
      dataIndex: 'Email',
    },
    {
      title: t('AbpIdentity.PhoneNumber'),
      dataIndex: 'PhoneNumber',
    },
    {
      title: t('AbpIdentity.DisplayName:IsActive'),
      dataIndex: 'IsActive',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? (
          <Icon icon="ant-design:check-outlined" color="green" />
        ) : (
          <Icon icon="charm:cross" color="red" />
        );
      },
    },
    {
      title: t('AbpIdentity.DisplayName:LockoutEnabled'),
      dataIndex: 'LockoutEnabled',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? (
          <Icon icon="ant-design:check-outlined" color="green" />
        ) : (
          <Icon icon="charm:cross" color="red" />
        );
      },
    },
    {
      title: t('routes.identity.users.EmailConfirmed'),
      dataIndex: 'EmailConfirmed',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? (
          <Icon icon="ant-design:check-outlined" color="green" />
        ) : (
          <Icon icon="charm:cross" color="red" />
        );
      },
    },
    {
      title: t('routes.identity.users.PhoneNumberConfirmed'),
      dataIndex: 'PhoneNumberConfirmed',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? (
          <Icon icon="ant-design:check-outlined" color="green" />
        ) : (
          <Icon icon="charm:cross" color="red" />
        );
      },
    },
  ];
}

export function getBasicColumnsForOrganizationUnit(): BasicColumn[] {
  return [
    {
      title: t('AbpIdentity.DisplayName:UserName'),
      dataIndex: 'UserName',
      customRender: ({ record }: { record: any }) => {
        return (
          <div>
            {!record.IsActive ? <Icon icon="el:ban-circle" color="red" /> : ''}
            {record.LockoutEnabled &&
            record.LockoutEnd != null &&
            dayjs(record.LockoutEnd) > dayjs() ? (
              <Icon icon="ant-design:lock-outlined" color="red" />
            ) : (
              ''
            )}
            {record.UserName}
          </div>
        );
      },
      sorter: true,
    },
    {
      title: t('AbpIdentity.DisplayName:Surname') + t('AbpIdentity.DisplayName:Name'),
      dataIndex: 'Name',
      sorter: true,
    },
    {
      title: t('AbpIdentity.EmailAddress'),
      dataIndex: 'Email',
    },
    {
      title: t('AbpIdentity.PhoneNumber'),
      dataIndex: 'PhoneNumber',
    },
    {
      title: t('AbpIdentity.DisplayName:IsActive'),
      dataIndex: 'IsActive',
      customRender: ({ text }: { text: boolean; index: number }) => {
        return text ? (
          <Icon icon="ant-design:check-outlined" color="green" />
        ) : (
          <Icon icon="charm:cross" color="red" />
        );
      },
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
      placeholder: t('routes.identity.users.searchPlaceholder'),
    },
  },
];

export const userFormSchema: FormSchema[] = [
  {
    field: 'Id',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
  {
    field: 'UserName',
    label: t('AbpIdentity.DisplayName:UserName'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.users.userNamePlaceholder'),
    },
    rules: [
      {
        required: true,
        message: t('routes.identity.users.userNamePlaceholder'),
      },
      {
        max: 256,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:UserName'),
          256,
        ]),
      },
    ],
  },
  {
    field: 'Password',
    label: t('AbpIdentity.DisplayName:Password'),
    component: 'InputPassword',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.users.passwordPlaceholder'),
    },
    ifShow: ({ values }) => {
      return !values.id;
    },
    rules: [
      {
        max: 128,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:Password'),
          128,
        ]),
      },
    ],
    dynamicRules: ({ values }) => {
      return [
        {
          required: values.Id ? false : true,
          validator: (_, value) => {
            if (!values.Id && !value) {
              console.log(values.Id);
              return Promise.reject(t('routes.identity.users.passwordPlaceholder'));
            }
            return Promise.resolve();
          },
        },
      ];
    },
  },
  {
    field: 'Name',
    label: t('AbpIdentity.DisplayName:Surname') + t('AbpIdentity.DisplayName:Name'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.users.namePlaceholder'),
    },
    rules: [
      {
        max: 64,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:Surname') + t('AbpIdentity.DisplayName:Name'),
          64,
        ]),
      },
    ],
  },
  {
    field: 'Email',
    label: t('AbpIdentity.DisplayName:Email'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.users.emailPlaceholder'),
    },
    rules: [
      {
        required: true,
        message: t('routes.identity.users.emailPlaceholder'),
      },
      {
        max: 256,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:Email'),
          256,
        ]),
      },
      {
        validator: async (_rule, value) => {
          const email =
            /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
          if (!email.test(value)) {
            return Promise.reject(
              t('AbpValidation.The {0} field is not a valid e-mail address', [
                t('AbpIdentity.DisplayName:Email'),
              ]),
            );
          }
          return Promise.resolve();
        },
      },
    ],
  },
  {
    field: 'PhoneNumber',
    label: t('AbpIdentity.DisplayName:PhoneNumber'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.users.phoneNumberPlaceholder'),
    },
    rules: [
      {
        max: 16,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:PhoneNumber'),
          16,
        ]),
      },
      {
        validator: async (_rule, value) => {
          if (value) {
            const phone = /^1(3[0-9]|4[01456879]|5[0-35-9]|6[2567]|7[0-8]|8[0-9]|9[0-35-9])\d{8}$/;
            if (!phone.test(value)) {
              return Promise.reject(
                t('AbpValidation.The {0} field is not a valid phone number', [
                  t('AbpIdentity.DisplayName:PhoneNumber'),
                ]),
              );
            }
            return Promise.resolve();
          } else {
            return Promise.resolve();
          }
        },
      },
    ],
  },
  {
    field: 'IsActive',
    label: '',
    component: 'CheckboxGroup',
    componentProps: {
      options: [
        {
          label: t('AbpIdentity.DisplayName:IsActive'),
          value: true,
        },
      ],
    },
    defaultValue: [true],
  },
  {
    field: 'LockoutEnabled',
    label: '',
    component: 'CheckboxGroup',
    componentProps: {
      options: [
        {
          label: t('AbpIdentity.DisplayName:LockoutEnabled'),
          value: true,
        },
      ],
    },
    defaultValue: [true],
  },
  {
    field: 'ConcurrencyStamp',
    label: '',
    component: 'Input',
    show: false,
    defaultValue: '',
  },
];

export function getOrganizationUnitBasicColumns(): BasicColumn[] {
  return [
    {
      title: t('AbpIdentity.DisplayName:UserName'),
      dataIndex: 'UserName',
    },
    {
      title: t('AbpIdentity.DisplayName:Surname') + t('AbpIdentity.DisplayName:Name'),
      dataIndex: 'Name',
    },
    {
      title: t('AbpIdentity.EmailAddress'),
      dataIndex: 'Email',
    },
    {
      title: t('AbpIdentity.PhoneNumber'),
      dataIndex: 'PhoneNumber',
    },
  ];
}
