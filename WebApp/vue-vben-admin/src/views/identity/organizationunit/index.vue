<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <Card :title="t('AbpIdentity.OrganizationTree')" class="w-1/3">
      <template #extra v-if="hasPermission('AbpIdentity.OrganizationUnits.ManageOU')">
        <Button type="primary" @click="handleCreate">
          <template #icon><plus-outlined /></template>
          {{ t('AbpIdentity.AddRootUnit') }}
        </Button>
      </template>
      <BasicTree
        ref="asyncTreeRef"
        :treeData="treeData"
        :search="true"
        :clickRowToExpand="false"
        :draggable="hasPermission('AbpIdentity.OrganizationUnits.ManageOU') ? true : false"
        :beforeRightClick="handleBeforeRightClick"
        @select="handleSelect"
        @drop="onDrop"
      />
    </Card>
    <Card class="w-2/3">
      <Tabs v-model:activeKey="activeKey" @change="TabsChange">
        <TabPane key="1" :tab="t('routes.identity.organizationunits.member')">
          <div v-show="!selectedKeys">{{
            t('routes.identity.organizationunits.Selectmember')
          }}</div>
          <BasicTable v-show="selectedKeys" @register="registerUserTable">
            <template #toolbar>
              <a-button
                v-if="hasPermission('AbpIdentity.OrganizationUnits.ManageMembers')"
                type="primary"
                @click="handleUserCreate"
              >
                {{ t('AbpIdentity.CreateMember') }}
              </a-button>
            </template>
            <template #bodyCell="{ column, record }">
              <template v-if="column.key === 'action'">
                <TableAction
                  :actions="[
                    {
                      label: t('common.delText'),
                      color: 'error',
                      popConfirm: {
                        title: t('component.Confirm.confirmDel'),
                        confirm: handleUserDelete.bind(null, record),
                      },
                      // auth: 'XyjIdentity.OrganizationUnits.ManageMembers',
                    },
                  ]"
                />
              </template>
            </template>
          </BasicTable>
        </TabPane>
        <TabPane key="2" :tab="t('AbpIdentity.Roles')">
          <div v-show="!selectedKeys">{{ t('routes.identity.organizationunits.Selectrole') }}</div>
          <BasicTable v-show="selectedKeys" @register="registerRoleTable">
            <template #toolbar>
              <a-button
                v-if="hasPermission('AbpIdentity.OrganizationUnits.ManageRoles')"
                type="primary"
                @click="handleRoleCreate"
              >
                {{ t('AbpIdentity.CreateRole') }}
              </a-button>
            </template>
            <template #bodyCell="{ column, record }">
              <template v-if="column.key === 'action'">
                <TableAction
                  :actions="[
                    {
                      label: t('common.delText'),
                      color: 'error',
                      popConfirm: {
                        title: t('component.Confirm.confirmDel'),
                        confirm: handleRoleDelete.bind(null, record),
                      },
                      // auth: 'XyjIdentity.OrganizationUnits.ManageMembers',
                    },
                  ]"
                />
              </template>
            </template>
          </BasicTable>
        </TabPane>
      </Tabs>
    </Card>
    <OrganizationunitModal @register="registerModal" @success="handleSuccess" />
    <OrganizationunitUserModal @register="userRegisterModal" @success="handleAddUserSuccess" />
    <OrganizationunitRoleModal @register="roleRegisterModal" @success="handleAddRoleSuccess" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { ref, unref, createVNode } from 'vue';
  import { PageWrapper } from '/@/components/Page';
  import { Card, Button, Modal, Tabs, TabPane } from 'ant-design-vue';
  import { BasicTree, TreeItem, TreeActionType, ContextMenuItem } from '/@/components/Tree/index';
  import { useModal } from '/@/components/Modal';
  import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';

  import { useI18n } from '/@/hooks/web/useI18n';
  import { usePermission } from '/@/hooks/web/usePermission';

  import {
    GetAll,
    MoveOrganization,
    DelOrganizationUnit,
    GetOrganizationUnitUsers,
    GetOrganizationUnitRoles,
    GetOrganizationUnitUsersIdAll,
    RemoveUser,
    GetOrganizationUnitRolesIdAll,
    RemoveRole,
  } from '/@/api/identity/organization-unit/organization-unit';
  import { OrganizationUnitDto } from '/@/api/identity/organization-unit/model/organizationUnitModel';
  import OrganizationunitModal from './organizationunitModal.vue';
  import OrganizationunitUserModal from './organizationunitUserModal.vue';
  import OrganizationunitRoleModal from './organizationunitRoleModal.vue';
  import { getOrganizationUnitBasicColumns as userBasicColumns } from '/@/views/identity/user/userdata';
  import { getOrganizationUnitBasicColumns as roleBasicColumns } from '/@/views/identity/role/roledata';
  import { GetRoleByPageParams } from '/@/api/identity/role/model/roleModel';

  const { t } = useI18n();
  const { hasPermission } = usePermission();

  const [registerModal, { openModal }] = useModal();
  const [userRegisterModal, { openModal: userOpenModal }] = useModal();
  const [roleRegisterModal, { openModal: roleOpenModal }] = useModal();

  const treeData = ref<TreeItem[]>([]);
  const originalData = ref<Array<OrganizationUnitDto>>([]);
  const asyncTreeRef = ref<Nullable<TreeActionType>>(null);
  let selectedKeys = ref<string>('');

  let activeKey = ref('1');

  const [registerUserTable, { reload: userReload }] = useTable({
    title: t('routes.identity.users.userlist'),
    api: GetOrganizationUnitUsers,
    rowKey: 'Id',
    columns: userBasicColumns(),
    useSearchForm: false,
    showTableSetting: true,
    bordered: true,
    actionColumn: {
      width: 100,
      title: t('common.operation'),
      dataIndex: 'action',
    },
    beforeFetch: (par: { id: string }) => {
      par.id = selectedKeys.value;
    },
  });

  const [registerRoleTable, { reload: roleReload }] = useTable({
    title: t('routes.identity.roles.rolelist'),
    api: GetOrganizationUnitRoles,
    rowKey: 'Id',
    columns: roleBasicColumns(),
    useSearchForm: false,
    showTableSetting: true,
    bordered: true,
    actionColumn: {
      width: 100,
      title: t('common.operation'),
      dataIndex: 'action',
    },
    beforeFetch: (par: GetRoleByPageParams & { id: string }) => {
      par.id = selectedKeys.value;
    },
  });

  GetAll().then((res) => {
    originalData.value = res.Items;
    res.Items.forEach((item: OrganizationUnitDto) => {
      if (item.ParentId == null) {
        treeData.value.push({
          title: item.DisplayName,
          key: item.Id,
          children: getchildren(item.Id, res.Items),
        });
      }
    });
  });

  function getchildren(id, data) {
    const childrendata: TreeItem[] = [];
    var child = data.filter((item: OrganizationUnitDto) => item.ParentId == id);
    if (child.length == 0) {
      return [];
    } else {
      child.forEach((item) => {
        childrendata.push({
          title: item.DisplayName,
          key: item.Id,
          children: getchildren(item.Id, data),
        });
      });
      return childrendata;
    }
  }

  function handleCreate() {
    openModal(true, {
      isUpdate: false,
    });
  }

  function handleSuccess(data) {
    if (data.isUpdate) {
      originalData.value = originalData.value.map((t) => {
        if (t.Id == data.Id) {
          t.DisplayName = data.DisplayName;
          t.ConcurrencyStamp = data.ConcurrencyStamp;
        }
        return t;
      });

      const asyncTreeAction: TreeActionType | null = unref(asyncTreeRef);
      if (asyncTreeAction) {
        asyncTreeAction.updateNodeByKey(data.Id, { title: data.DisplayName });
      }
    } else {
      if (data.ParentId == null) {
        treeData.value.push({
          title: data.DisplayName,
          key: data.Id,
        });
        originalData.value.push(data);
      } else {
        const asyncTreeAction: TreeActionType | null = unref(asyncTreeRef);
        if (asyncTreeAction) {
          let nodeChildren: TreeItem[] = [];
          originalData.value
            .filter((a) => a.ParentId == data.ParentId)
            .forEach((item: OrganizationUnitDto) => {
              nodeChildren.push({
                title: item.DisplayName,
                key: item.Id,
              });
            });
          nodeChildren.push({
            title: data.DisplayName,
            key: data.Id,
          });
          originalData.value.push(data);
          asyncTreeAction.updateNodeByKey(data.ParentId, { children: nodeChildren });
          asyncTreeAction.setExpandedKeys([data.ParentId, ...asyncTreeAction.getExpandedKeys()]);
        }
      }
    }
  }

  function handleBeforeRightClick(node) {
    let ContextMenuItem: ContextMenuItem[] = [];
    if (hasPermission('AbpIdentity.OrganizationUnits.ManageOU')) {
      ContextMenuItem.push({
        label: t('common.editText'),
        handler: () => {
          openModal(true, {
            isUpdate: true,
            record: originalData.value.find((a) => a.Id == node.eventKey),
          });
        },
      });
      ContextMenuItem.push({
        label: t('AbpIdentity.AddChildOrganizationUnit'),
        handler: () => {
          openModal(true, {
            isUpdate: false,
            ParentId: node.eventKey,
          });
        },
      });
      ContextMenuItem.push({
        label: t('common.delText'),
        handler: () => {
          Modal.confirm({
            title: t('component.Confirm.confirmDel'),
            icon: createVNode(ExclamationCircleOutlined),
            content: createVNode(
              'div',
              {
                style: 'color:red;',
              },
              t('routes.identity.organizationunits.delorganizationunit', [
                originalData.value.find((a) => a.Id == node.eventKey)?.DisplayName,
              ]),
            ),
            onOk() {
              DelOrganizationUnit({ id: node.eventKey }).then(() => {
                originalData.value.splice(
                  originalData.value.findIndex((a) => a.Id == node.eventKey),
                  1,
                );
                const asyncTreeAction: TreeActionType | null = unref(asyncTreeRef);
                if (asyncTreeAction) {
                  asyncTreeAction?.deleteNodeByKey(node.eventKey);
                }
              });
            },
          });
        },
      });
    }
    return ContextMenuItem;
  }

  function handleSelect(SelectNode) {
    selectedKeys.value = SelectNode[0];
    if (SelectNode[0]) {
      if (activeKey.value == '1') {
        userReload();
      } else if (activeKey.value == '2') {
        roleReload();
      }
    }
  }

  async function handleUserCreate() {
    const OrganizationUnitUsersIdAll = await GetOrganizationUnitUsersIdAll({
      id: selectedKeys.value,
    });
    userOpenModal(true, {
      Usersid: OrganizationUnitUsersIdAll.Items,
      id: selectedKeys.value,
    });
  }

  function onDrop(data) {
    MoveOrganization({ id: data.dragNode.eventKey, NewParentId: data.node.eventKey }).then(() => {
      treeData.value = [];
      originalData.value = originalData.value.map((t) => {
        if (t.Id == data.dragNode.eventKey) {
          t.ParentId = data.node.eventKey;
        }
        return t;
      });

      originalData.value.forEach((item: OrganizationUnitDto) => {
        if (item.ParentId == null) {
          treeData.value.push({
            title: item.DisplayName,
            key: item.Id,
            children: getchildren(item.Id, originalData.value),
          });
        }
      });
    });
  }

  function TabsChange(activeKey: string) {
    if (selectedKeys.value) {
      if (activeKey == '1') {
        userReload();
      } else if (activeKey == '2') {
        roleReload();
      }
    }
  }

  function handleAddUserSuccess() {
    userReload();
  }

  async function handleUserDelete(record: Recordable) {
    await RemoveUser({ id: selectedKeys.value, userId: record.Id });
    userReload();
  }

  async function handleRoleCreate() {
    const OrganizationUnitRolesIdAll = await GetOrganizationUnitRolesIdAll({
      id: selectedKeys.value,
    });
    roleOpenModal(true, {
      Rolesid: OrganizationUnitRolesIdAll.Items,
      id: selectedKeys.value,
    });
  }

  function handleAddRoleSuccess() {
    roleReload();
  }

  async function handleRoleDelete(record: Recordable) {
    await RemoveRole({ id: selectedKeys.value, roleId: record.Id });
    roleReload();
  }
</script>
