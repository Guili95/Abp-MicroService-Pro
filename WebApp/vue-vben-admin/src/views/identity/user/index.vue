<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button
          type="primary"
          v-if="hasPermission('AbpIdentity.Users.Create')"
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
                auth: 'AbpIdentity.Users.Update',
              },
              {
                label: t('AbpIdentity.Permission:Delete'),
                color: 'error',
                popConfirm: {
                  title: t('component.Confirm.confirmDel'),
                  confirm: handleDelete.bind(null, record),
                },
                auth: 'AbpIdentity.Users.Delete',
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
    <UserModal @register="userRegisterModal" @success="handleSuccess" />
    <UserPermissions @register="userPermissionsRegisterModal" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '/@/components/Page';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { useModal } from '/@/components/Modal';

  import { useI18n } from '/@/hooks/web/useI18n';
  import { usePermission } from '/@/hooks/web/usePermission';

  import { getBasicColumns, searchFormSchema } from './userdata';
  import { GetUserByPage, DelUser } from '/@/api/identity/user/user';
  import UserModal from './userModal.vue';
  import UserPermissions from './userPermissions.vue';

  const { t } = useI18n();
  const { hasPermission } = usePermission();

  const [registerTable, { reload }] = useTable({
    title: t('routes.identity.users.userlist'),
    api: GetUserByPage,
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

  const [userRegisterModal, { openModal: userOpenModal }] = useModal();
  const [userPermissionsRegisterModal, { openModal: userPermissionsOpenModal }] = useModal();

  function handleCreate() {
    userOpenModal(true, {
      isUpdate: false,
    });
  }

  function handleEdit(record: Recordable) {
    userOpenModal(true, {
      record,
      isUpdate: true,
    });
  }

  async function handleDelete(record: Recordable) {
    await DelUser({ Id: record.Id });
    reload();
  }

  function handleSuccess() {
    reload();
  }

  function handlePermissions(record: Recordable) {
    userPermissionsOpenModal(true, {
      id: record.Id,
    });
  }
</script>
