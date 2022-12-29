<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <BasicTable @register="registerTable">
      <template #toolbar>
        <a-button
          type="primary"
          v-if="hasPermission('AbpTenantManagement.Tenants.Create')"
          @click="handleCreate"
        >
          {{ t('AbpTenantManagement.Permission:Create') }}
        </a-button>
      </template>
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <TableAction
            :dropDownActions="[
              {
                label: t('AbpTenantManagement.Permission:Edit'),
                onClick: handleEdit.bind(null, record),
                auth: 'AbpTenantManagement.Tenants.Update',
              },
              {
                label: t('AbpTenantManagement.Permission:Delete'),
                color: 'error',
                popConfirm: {
                  title: t('component.Confirm.confirmDel'),
                  confirm: handleDelete.bind(null, record),
                },
                auth: 'AbpTenantManagement.Tenants.Delete',
              },
              {
                label: t('AbpTenantManagement.Permission:ManageFeatures'),
                onClick: handleFeatures.bind(null, record),
                auth: 'AbpTenantManagement.Tenants.ManageFeatures',
              },
            ]"
          />
        </template>
      </template>
    </BasicTable>
    <TenantModal @register="registerModal" @success="handleSuccess" />
    <TenantFeatures @register="tenantFeaturesRegisterModal" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '/@/components/Page';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { useModal } from '/@/components/Modal';

  import { useI18n } from '/@/hooks/web/useI18n';
  import { usePermission } from '/@/hooks/web/usePermission';

  import { GetTenantsByPage } from '/@/api/saas/tenant/tenant';

  import TenantModal from './tenantModal.vue';
  import TenantFeatures from './tenantFeatures.vue';
  import { getBasicColumns, searchFormSchema } from './tenantdata';
  import { DelTenant } from '/@/api/saas/tenant/tenant';

  const { t } = useI18n();
  const { hasPermission } = usePermission();

  const [registerTable, { reload }] = useTable({
    title: t('routes.saas.tenants.tenantlist'),
    api: GetTenantsByPage,
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

  const [registerModal, { openModal }] = useModal();
  const [tenantFeaturesRegisterModal, { openModal: tenantFeaturesOpenModal }] = useModal();

  function handleCreate() {
    openModal(true, {
      isUpdate: false,
    });
  }

  function handleEdit(record: Recordable) {
    openModal(true, {
      record,
      isUpdate: true,
    });
  }

  async function handleDelete(record: Recordable) {
    await DelTenant({ Id: record.Id });
    reload();
  }

  function handleFeatures(record: Recordable) {
    tenantFeaturesOpenModal(true, {
      id: record.Id,
    });
  }

  function handleSuccess() {
    reload();
  }
</script>
