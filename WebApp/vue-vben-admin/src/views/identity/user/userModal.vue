<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <Tabs v-model:activeKey="activeKey">
      <TabPane key="1" :tab="t('AbpIdentity.Users')">
        <BasicForm @register="registerForm" />
      </TabPane>
      <TabPane key="2" :tab="t('AbpIdentity.Roles')">
        <CheckboxGroup v-model:value="checkedList">
          <Row v-for="item in RoleData" :key="item.Id">
            <Col span="24">
              <Checkbox :value="item.Id">{{ item.Name }}</Checkbox>
            </Col>
          </Row>
        </CheckboxGroup>
      </TabPane>
      <TabPane key="3" :tab="t('AbpIdentity.OrganizationUnit')">
        <Tree
          v-model:checkedKeys="checkedKeys"
          :treeData="treeData"
          :autoExpandParent="true"
          :defaultExpandAll="true"
          :search="true"
          checkable
          :clickRowToExpand="false"
          :checkStrictly="true"
        />
      </TabPane>
    </Tabs>
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref, unref, computed, defineEmits } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { Tabs, CheckboxGroup, Checkbox, Row, Col, Tree } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { TreeItem, CheckKeys } from '/@/components/Tree/index';

  import { userFormSchema } from './userdata';
  import {
    CreateUser,
    UpdateUser,
    GetUserAssignableRoles,
    UpdateOrganizationUnits,
    GetUserRole,
    GetUserOrganizationUnits,
  } from '/@/api/identity/user/user';
  import { GetAll } from '/@/api/identity/organization-unit/organization-unit';
  import type { Roles } from '/@/api/identity/role/model/roleModel';
  import type { OrganizationUnitDto } from '/@/api/identity/organization-unit/model/organizationUnitModel';

  const TabPane = Tabs.TabPane;

  const { t } = useI18n();

  const emits = defineEmits(['success']);

  const treeData = ref<TreeItem[]>([]);
  let activeKey = ref('1');
  const checkedList = ref<string[]>([]);
  const isUpdate = ref(true);
  const rowId = ref('');
  let RoleData = ref<Array<Roles>>([]);
  const checkedKeys = ref<CheckKeys>({ checked: [], halfChecked: [] });

  function getchildren(id, data) {
    const childrendata: TreeItem[] = [];
    var child = data.filter((item: OrganizationUnitDto) => item.ParentId == id);
    if (child.length == 0) {
      return [];
    } else {
      child.forEach((item: OrganizationUnitDto) => {
        childrendata.push({
          title: item.DisplayName,
          key: item.Id,
          children: getchildren(item.Id, data),
        });
      });
      return childrendata;
    }
  }

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    setModalProps({ confirmLoading: true });
    activeKey.value = '1';
    resetFields();
    isUpdate.value = !!data?.isUpdate;
    rowId.value = '';
    treeData.value = [];
    checkedList.value = [];
    RoleData.value = [];
    RoleData.value = (await GetUserAssignableRoles()).Items;
    GetAll().then((res) => {
      treeData.value = [];
      checkedKeys.value = { checked: [], halfChecked: [] };
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
    if (unref(isUpdate)) {
      rowId.value = data.record.Id;
      setFieldsValue({
        ...data.record,
        IsActive: [data.record.IsActive],
        LockoutEnabled: [data.record.LockoutEnabled],
      });
      GetUserRole(data.record.Id).then((res) => {
        checkedList.value = res.Items.map((item) => {
          return item.Id;
        });
      });
      GetUserOrganizationUnits(data.record.Id).then((res: OrganizationUnitDto[]) => {
        if (res.length > 0) {
          (checkedKeys.value as { checked: string[]; halfChecked: string[] }).checked = res.map(
            (item: OrganizationUnitDto): string => {
              return item.Id;
            },
          );
        } else {
          (checkedKeys.value as { checked: string[]; halfChecked: string[] }).checked = [];
        }
      });
    }
    setModalProps({ confirmLoading: false });
  });

  const getTitle = computed(() =>
    !unref(isUpdate) ? t('AbpIdentity.NewUser') : t('routes.identity.users.edituser'),
  );

  const [registerForm, { setFieldsValue, resetFields, validate }] = useForm({
    layout: 'vertical',
    labelWidth: 0,
    schemas: userFormSchema,
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
        const user = await CreateUser({
          ...values,
          IsActive: values.IsActive[0],
          LockoutEnabled: values.LockoutEnabled[0],
          RoleNames: RoleData.value
            .filter((a) => {
              return checkedList.value.includes(a.Id);
            })
            .map((item) => {
              return item.Name;
            }),
        });
        await UpdateOrganizationUnits({
          id: user.Id,
          OrganizationUnitIds: (checkedKeys.value as { checked: string[]; halfChecked: string[] })
            .checked,
        });
      } else {
        const user = await UpdateUser({
          ...values,
          IsActive: values.IsActive[0],
          LockoutEnabled: values.LockoutEnabled[0],
          RoleNames: RoleData.value
            .filter((a) => {
              return checkedList.value.includes(a.Id);
            })
            .map((item) => {
              return item.Name;
            }),
        });

        await UpdateOrganizationUnits({
          id: user.Id,
          OrganizationUnitIds: (checkedKeys.value as { checked: string[]; halfChecked: string[] })
            .checked,
        });
      }

      closeModal();
      emits('success');
    } finally {
      setModalProps({ confirmLoading: false });
    }
  }
</script>
