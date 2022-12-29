import { FormSchema } from '/@/components/Table';

import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();

export const organizationunitFormSchema: FormSchema[] = [
  {
    field: 'DisplayName',
    label: t('AbpIdentity.DisplayName:OrganizationUnit'),
    component: 'Input',
    colProps: { span: 24 },
    componentProps: {
      placeholder: t('routes.identity.organizationunits.displayNamePlaceholder'),
    },
    dynamicDisabled: ({ values }) => {
      return !!values.isStatic;
    },
    rules: [
      {
        required: true,
        message: t('routes.identity.organizationunits.displayNamePlaceholder'),
      },
      {
        max: 128,
        message: t('AbpValidation.The field {0} must be a string with a maximum length of {1}', [
          t('AbpIdentity.DisplayName:OrganizationUnit'),
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
