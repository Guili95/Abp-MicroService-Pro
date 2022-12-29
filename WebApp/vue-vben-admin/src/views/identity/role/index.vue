<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button
          type="primary"
          v-if="hasPermission('AbpIdentity.Roles.Create')"
          @click="handleCreate"
        >
          {{ t('AbpIdentity.Permission:Create') }}
        </a-button>
      </template>
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <TableAction
            :dropDownActions="[
              {
                label: t('AbpIdentity.Permission:Edit'),
                onClick: handleEdit.bind(null, record),
                auth: 'AbpIdentity.Roles.Update',
              },
              {
                label: t('AbpIdentity.Permission:Delete'),
                color: 'error',
                popConfirm: {
                  title: t('component.Confirm.confirmDel'),
                  confirm: handleDelete.bind(null, record),
                },
                auth: 'AbpIdentity.Roles.Delete',
                ifShow: () => {
                  return !record.IsStatic;
                },
              },
              {
                label: t('AbpPermissionManagement.Permissions'),
                auth: 'AbpIdentity.Users.ManagePermissions',
                onClick: handlePermissions.bind(null, record),
              },
            ]"
          />
        </template>
      </template>
    </BasicTable>
    <RoleModal @register="roleRegisterModal" @success="handleSuccess" />
    <RolePermissions @register="rolePermissionsRegisterModal" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '/@/components/Page';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { useModal } from '/@/components/Modal';

  import { useI18n } from '/@/hooks/web/useI18n';
  import { usePermission } from '/@/hooks/web/usePermission';

  import { getBasicColumns, searchFormSchema } from './roledata';
  import { GetRoleByPage, DelRole } from '/@/api/identity/role/role';
  import RoleModal from './roleModal.vue';
  import RolePermissions from './rolePermissions.vue';

  const { t } = useI18n();
  const { hasPermission } = usePermission();

  const [registerTable, { reload }] = useTable({
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
    actionColumn: {
      width: 160,
      title: t('common.operation'),
      dataIndex: 'action',
    },
  });

  const [roleRegisterModal, { openModal: roleOpenModal }] = useModal();
  const [rolePermissionsRegisterModal, { openModal: rolePermissionsOpenModal }] = useModal();

  function handleCreate() {
    roleOpenModal(true, {
      isUpdate: false,
    });
  }

  function handleEdit(record: Recordable) {
    roleOpenModal(true, {
      record,
      isUpdate: true,
    });
  }

  async function handleDelete(record: Recordable) {
    await DelRole({ Id: record.Id });
    reload();
  }

  function handleSuccess() {
    reload();
  }

  function handlePermissions(record: Recordable) {
    rolePermissionsOpenModal(true, {
      name: record.Name,
    });
  }
</script>
