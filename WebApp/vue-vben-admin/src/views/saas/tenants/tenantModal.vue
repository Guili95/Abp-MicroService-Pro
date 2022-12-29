<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref, computed, unref } from 'vue';

  import { useI18n } from '/@/hooks/web/useI18n';

  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';

  import { tenantFormSchema } from './tenantdata';
  import { CreateTenant, UpdateTenant } from '/@/api/saas/tenant/tenant';

  const emits = defineEmits(['success']);

  const isUpdate = ref(true);
  const rowId = ref('');

  const { t } = useI18n();

  const [registerForm, { setFieldsValue, resetFields, validate }] = useForm({
    layout: 'vertical',
    labelWidth: 0,
    schemas: tenantFormSchema,
    showActionButtonGroup: false,
    actionColOptions: {
      span: 23,
    },
  });

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    resetFields();
    setModalProps({ confirmLoading: false });
    isUpdate.value = !!data?.isUpdate;
    rowId.value = '';
    if (unref(isUpdate)) {
      rowId.value = data.record.Id;
      setFieldsValue({
        ...data.record,
      });
    }
  });

  const getTitle = computed(() =>
    !unref(isUpdate) ? t('AbpTenantManagement.NewTenant') : t('routes.saas.tenants.editTenant'),
  );

  async function handleSubmit() {
    try {
      const values = await validate();
      setModalProps({ confirmLoading: true });

      if (!rowId.value) {
        await CreateTenant(values);
      } else {
        await UpdateTenant(values);
      }

      emits('success');
      closeModal();
    } finally {
      setModalProps({ confirmLoading: false });
    }
  }
</script>
