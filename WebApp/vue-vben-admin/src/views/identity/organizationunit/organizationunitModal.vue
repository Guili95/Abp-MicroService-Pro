<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <h2 v-if="parentId">{{ t('AbpIdentity.ParentOrganizationUnit') + parentName }}</h2>
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref, unref, computed } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';

  import { useI18n } from '/@/hooks/web/useI18n';

  import {
    GetById,
    CreateOrganizationUnit,
    UpdateOrganizationUnit,
  } from '/@/api/identity/organization-unit/organization-unit';
  import { organizationunitFormSchema } from './organizationunitdata';

  const { t } = useI18n();
  const emits = defineEmits(['success']);

  const isUpdate = ref(true);
  const parentId = ref<Nullable<string>>(null);
  const parentName = ref('');
  const rowId = ref('');

  const [registerForm, { setFieldsValue, resetFields, validate }] = useForm({
    layout: 'vertical',
    labelWidth: 0,
    schemas: organizationunitFormSchema,
    showActionButtonGroup: false,
    actionColOptions: {
      span: 23,
    },
  });

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    resetFields();
    rowId.value = '';
    parentId.value = data.ParentId ?? null;
    isUpdate.value = !!data?.isUpdate;
    if (parentId.value != null) {
      GetById({ Id: parentId.value }).then((res) => {
        parentName.value = res.DisplayName;
      });
    }

    if (unref(isUpdate)) {
      rowId.value = data.record.Id;
      setFieldsValue({ ...data.record });
    }
    setModalProps({ confirmLoading: false });
  });

  const getTitle = computed(() =>
    !unref(isUpdate) ? t('AbpIdentity.NewOrganizationUnit') : t('AbpIdentity.EditOrganizationUnit'),
  );

  async function handleSubmit() {
    try {
      const values = await validate();
      if (!rowId.value) {
        const data = await CreateOrganizationUnit({ ...values, ParentId: parentId.value });
        closeModal();
        emits('success', { ...data, isUpdate: isUpdate.value });
      } else {
        const data = await UpdateOrganizationUnit({
          DisplayName: values.DisplayName,
          Id: rowId.value,
          ConcurrencyStamp: values.ConcurrencyStamp,
        });
        closeModal();
        emits('success', { ...data, isUpdate: isUpdate.value });
      }
    } finally {
      setModalProps({ confirmLoading: false });
    }
  }
</script>
