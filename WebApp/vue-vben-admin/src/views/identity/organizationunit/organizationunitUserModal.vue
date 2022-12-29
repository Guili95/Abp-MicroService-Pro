<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    :title="t('routes.identity.users.selectUser')"
    @ok="handleSubmit"
  >
    <BasicTable @register="registerTable" />
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicTable, useTable } from '/@/components/Table';

  import { useI18n } from '/@/hooks/web/useI18n';

  import { GetUserByPage } from '/@/api/identity/user/user';
  import { OrganizationAddMembers } from '/@/api/identity/organization-unit/organization-unit';
  import {
    getBasicColumnsForOrganizationUnit,
    searchFormSchema,
  } from '/@/views/identity/user/userdata';

  const { t } = useI18n();

  const emits = defineEmits(['success']);

  const areadyKeys = ref<Array<string>>([]);
  const id = ref<string>('');

  const [registerTable, { setSelectedRowKeys, getSelectRowKeys, reload, getForm }] = useTable({
    title: t('routes.identity.users.userlist'),
    api: GetUserByPage,
    rowKey: 'Id',
    columns: getBasicColumnsForOrganizationUnit(),
    formConfig: {
      labelWidth: 120,
      schemas: searchFormSchema,
      autoSubmitOnEnter: true,
    },
    useSearchForm: true,
    showTableSetting: true,
    bordered: true,
    maxHeight: 300,
    rowSelection: {
      //对于不可取消的值进行禁选
      getCheckboxProps: (record: { Id: string }) => {
        const isDisabled = areadyKeys.value.some((item) => item === record.Id);
        return {
          disabled: isDisabled,
        };
      },
    },
    pagination: {
      pageSize: 5,
    },
  });

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    setModalProps({ confirmLoading: true, width: 1300 });
    getForm().resetFields();
    reload();
    areadyKeys.value = data.Usersid;
    id.value = data.id;
    setSelectedRowKeys(areadyKeys.value);
    setModalProps({ confirmLoading: false, width: 1300 });
  });

  function handleSubmit() {
    const data = getSelectRowKeys();
    OrganizationAddMembers({ id: id.value, UserIds: data }).then(() => {
      closeModal();
      emits('success');
    });
  }
</script>
