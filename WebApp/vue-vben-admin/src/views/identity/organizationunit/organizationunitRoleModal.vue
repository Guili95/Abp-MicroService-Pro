<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    :title="t('routes.identity.roles.selectRole')"
    @ok="handleSubmit"
  >
    <BasicTable @register="registerTable" />
  </BasicModal>
</template>

<script lang="ts" setup>
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicTable, useTable } from '/@/components/Table';

  import { useI18n } from '/@/hooks/web/useI18n';

  import { GetRoleByPage } from '/@/api/identity/role/role';
  import { getBasicColumns, searchFormSchema } from '/@/views/identity/role/roledata';
  import { OrganizationAddRoles } from '/@/api/identity/organization-unit/organization-unit';

  const { t } = useI18n();

  const emits = defineEmits(['success']);

  let areadyKeys = [];
  let id = '';

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    setModalProps({ confirmLoading: true, width: 1000 });
    getForm().resetFields();
    reload();
    areadyKeys = data.Rolesid;
    id = data.id;
    setSelectedRowKeys(areadyKeys);
    setModalProps({ confirmLoading: false, width: 1000 });
  });

  const [registerTable, { setSelectedRowKeys, getSelectRowKeys, reload, getForm }] = useTable({
    title: t('routes.identity.roles.rolelist'),
    api: GetRoleByPage,
    rowKey: 'Id',
    columns: getBasicColumns(),
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
        const isDisabled = areadyKeys.some((item) => item === record.Id);
        return {
          disabled: isDisabled,
        };
      },
    },
    pagination: {
      pageSize: 5,
    },
  });

  function handleSubmit() {
    const data = getSelectRowKeys();
    OrganizationAddRoles({ id: id, RoleIds: data }).then(() => {
      closeModal();
      emits('success');
    });
  }
</script>
