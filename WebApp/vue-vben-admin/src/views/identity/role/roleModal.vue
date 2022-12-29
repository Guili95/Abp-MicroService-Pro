<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref, unref, computed, defineEmits } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { TreeItem } from '/@/components/Tree/index';

  import { roleFormSchema } from './roledata';
  import { CreateRole, UpdateRole } from '/@/api/identity/role/role';

  const { t } = useI18n();

  const emits = defineEmits(['success']);

  const treeData = ref<TreeItem[]>([]);
  let activeKey = ref('1');

  const isUpdate = ref(true);
  const rowId = ref('');

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    activeKey.value = '1';
    resetFields();
    isUpdate.value = !!data?.isUpdate;
    rowId.value = '';
    treeData.value = [];
    if (unref(isUpdate)) {
      rowId.value = data.record.Id;
      setFieldsValue({
        ...data.record,
        IsDefault: [data.record.IsDefault],
        IsPublic: [data.record.IsPublic],
      });
    }
    setModalProps({ confirmLoading: false });
  });

  const getTitle = computed(() =>
    !unref(isUpdate) ? t('AbpIdentity.NewRole') : t('routes.identity.roles.editrole'),
  );

  const [registerForm, { setFieldsValue, resetFields, validate }] = useForm({
    layout: 'vertical',
    labelWidth: 0,
    schemas: roleFormSchema,
    showActionButtonGroup: false,
    actionColOptions: {
      span: 23,
    },
  });

  async function handleSubmit() {
    try {
      const values = await validate();
      setModalProps({ confirmLoading: true });

      if (!rowId.value) {
        await CreateRole({
          Name: values.Name,
          IsDefault: values.IsDefault[0],
          IsPublic: values.IsPublic[0],
        });
      } else {
        await UpdateRole({
          ...values,
          IsDefault: values.IsDefault[0],
          IsPublic: values.IsPublic[0],
        });
      }

      closeModal();
      emits('success');
    } finally {
      setModalProps({ confirmLoading: false });
    }
  }
</script>
