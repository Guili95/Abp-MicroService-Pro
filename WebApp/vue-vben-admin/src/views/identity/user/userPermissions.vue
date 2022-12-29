<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    :title="t('routes.identity.users.permissions')"
    @ok="handleSubmit"
  >
    <ScrollContainer>
      <Checkbox
        :checked="AllCount == AllcheckedKeys.length ? true : false"
        :indeterminate="!!AllcheckedKeys.length && AllcheckedKeys.length < AllCount"
        @change="onCheckAllChange"
        >{{ t('AbpPermissionManagement.SelectAllInAllTabs') }}</Checkbox
      >
      <div ref="wrapperRef" :class="prefixCls">
        <Tabs
          v-model:activeKey="activeKey"
          tab-position="left"
          :tabBarStyle="tabBarStyle"
          @change="heandleChange"
        >
          <TabPane
            v-for="item in permissionList"
            :tab="item.name + `(${item.checkedKeys.length})`"
            :key="item.key"
          >
            <Checkbox
              :checked="item.AllCount == item.checkedKeys.length ? true : false"
              :indeterminate="!!item.checkedKeys.length && item.checkedKeys.length < item.AllCount"
              @change="onCheckChange"
              >{{ t('AbpPermissionManagement.SelectAllInThisTab') }}</Checkbox
            >
            <Tree
              ref="asyncTreeRef"
              :checkedKeys="item.checkedKeys"
              :treeData="item.treeData"
              :checkable="true"
              :clickRowToExpand="false"
              :defaultExpandAll="true"
              :checkStrictly="true"
              :selectable="false"
              @check="handleCheck"
            />
          </TabPane>
        </Tabs>
      </div>
    </ScrollContainer>
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { TreeItem } from '/@/components/Tree/index';
  import { ScrollContainer } from '/@/components/Container/index';
  import { Tabs, Checkbox, Tree } from 'ant-design-vue';

  import { useI18n } from '/@/hooks/web/useI18n';

  import { GetPermission, UpdatePermission } from '/@/api/permission/permission';
  import { GetPermissionModel, permissions } from '/@/api/permission/model/permissionModel';

  interface Permissions {
    key: string;
    name: string;
    treeData: TreeItem[];
    checkedKeys: string[];
    AllCount: number;
  }

  const prefixCls = 'permission';

  const tabBarStyle = {
    width: '150px',
  };

  const TabPane = Tabs.TabPane;

  const { t } = useI18n();

  const activeKey = ref<string>('');
  const AllCount = ref<number>(0);
  const AllcheckedKeys = ref<string[]>([]);
  const rowId = ref('');

  const permissionList = ref<Array<Permissions>>([]);
  let sourceData: GetPermissionModel = {
    EntityDisplayName: '',
    Groups: [],
  };

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    setModalProps({ loading: true, confirmLoading: true });
    permissionList.value = [];
    AllCount.value = 0;
    AllcheckedKeys.value = [];
    rowId.value = data.id;
    GetPermission({ providerName: 'U', providerKey: data.id }).then((res) => {
      sourceData = res;
      activeKey.value = res.Groups[0].Name;
      res.Groups.forEach((item) => {
        const treeData: TreeItem[] = [];
        let checkedKeys: string[] = [];
        item.Permissions.forEach((permissionitem) => {
          if (permissionitem.ParentName == null) {
            treeData.push({
              title: permissionitem.DisplayName,
              key: permissionitem.Name,
              disabled:
                permissionitem.GrantedProviders.length == 0 ||
                (permissionitem.GrantedProviders.length == 1 &&
                  permissionitem.GrantedProviders[0].ProviderName == 'U')
                  ? false
                  : true,
              children: getchildren(permissionitem.Name, item.Permissions),
            });
          }
        });
        checkedKeys = item.Permissions.filter((permissionitem) => {
          if (permissionitem.IsGranted) {
            return permissionitem;
          }
        }).map((permissionitem) => {
          return permissionitem.Name;
        });

        permissionList.value.push({
          key: item.Name,
          name: item.DisplayName,
          treeData: treeData,
          checkedKeys: checkedKeys,
          AllCount: item.Permissions.length,
        });
        AllCount.value = AllCount.value + item.Permissions.length;
        AllcheckedKeys.value.push(...checkedKeys);
      });
      setModalProps({ loading: false, confirmLoading: false });
    });
  });

  function getchildren(Name, data) {
    const childrendata: TreeItem[] = [];
    var child = data.filter((item) => item.ParentName == Name);
    if (child.length == 0) {
      return [];
    } else {
      child.forEach((item) => {
        childrendata.push({
          title: item.DisplayName,
          key: item.Name,
          disabled:
            item.GrantedProviders.length == 0 ||
            (item.GrantedProviders.length == 1 && item.GrantedProviders[0].ProviderName == 'U')
              ? false
              : true,
          children: getchildren(item.Name, data),
        });
      });
      return childrendata;
    }
  }

  function heandleChange(activeKeyStr: string) {
    activeKey.value = activeKeyStr;
  }

  function handleCheck(CheckNode, { node }) {
    const data = permissionList.value.find((item) => {
      return item.key == activeKey.value;
    }) as Permissions;
    data.checkedKeys = CheckNode.checked;
    const sourceDatapermissions = sourceData.Groups.find((item) => {
      return item.Name == activeKey.value;
    })?.Permissions;
    if (CheckNode.checked.findIndex((a) => a == node.eventKey) > -1) {
      if (sourceDatapermissions?.find((a) => a.Name == node.eventKey)?.ParentName != null) {
        if (
          CheckNode.checked.findIndex(
            (a) => a == sourceDatapermissions?.find((a) => a.Name == node.eventKey)?.ParentName,
          ) == -1
        ) {
          sourceDatapermissions?.find((a) => a.Name == node.eventKey)?.ParentName;
          data.checkedKeys.push(
            sourceDatapermissions?.find((a) => a.Name == node.eventKey)?.ParentName as string,
          );
        }
      }
    } else {
      if (sourceDatapermissions?.find((a) => a.Name == node.eventKey)?.ParentName == null) {
        const child = sourceDatapermissions?.filter(
          (item) => item.ParentName == node.eventKey,
        ) as permissions[];
        child.forEach((items) => {
          if (CheckNode.checked.findIndex((a) => a == items.Name) > -1) {
            data.checkedKeys.splice(
              data.checkedKeys.findIndex((a) => a == items.Name),
              1,
            );
          }
        });
      }
    }
    AllcheckedKeys.value = [];
    permissionList.value.forEach((item) => {
      AllcheckedKeys.value.push(...item.checkedKeys);
    });
  }

  function onCheckAllChange(e) {
    permissionList.value.forEach((Allitem) => {
      const sourceDatapermissions = sourceData.Groups.find((item) => {
        return item.Name == Allitem.key;
      })?.Permissions;
      if (e.target.checked) {
        Allitem.checkedKeys = sourceDatapermissions
          ?.filter((permissionitem) => {
            return permissionitem;
          })
          .map((permissionitem) => {
            return permissionitem.Name;
          }) as string[];
      } else {
        const data = sourceDatapermissions
          ?.filter((permissionitem) => {
            if (
              permissionitem.GrantedProviders.length == 0 ||
              (permissionitem.GrantedProviders.length == 1 &&
                permissionitem.GrantedProviders[0].ProviderName == 'U')
            ) {
              return permissionitem;
            }
          })
          .map((permissionitem) => {
            return permissionitem.Name;
          }) as string[];
        if (data.length >= 0) {
          data.forEach((item) => {
            if (Allitem.checkedKeys.findIndex((a) => a == item) > -1) {
              Allitem.checkedKeys.splice(
                Allitem.checkedKeys.findIndex((a) => a == item),
                1,
              );
            }
          });
        }
      }
    });
    AllcheckedKeys.value = [];
    permissionList.value.forEach((item) => {
      AllcheckedKeys.value.push(...item.checkedKeys);
    });
  }

  function onCheckChange(e) {
    console.log(e.target.checked);
    const data = permissionList.value.find((item) => {
      return item.key == activeKey.value;
    }) as Permissions;
    const sourceDatapermissions = sourceData.Groups.find((item) => {
      return item.Name == activeKey.value;
    })?.Permissions;
    if (e.target.checked) {
      data.checkedKeys = sourceDatapermissions
        ?.filter((permissionitem) => {
          return permissionitem;
        })
        .map((permissionitem) => {
          return permissionitem.Name;
        }) as string[];
    } else {
      const dataU = sourceDatapermissions
        ?.filter((permissionitem) => {
          if (
            permissionitem.GrantedProviders.length == 0 ||
            (permissionitem.GrantedProviders.length == 1 &&
              permissionitem.GrantedProviders[0].ProviderName == 'U')
          ) {
            return permissionitem;
          }
        })
        .map((permissionitem) => {
          return permissionitem.Name;
        }) as string[];
      if (dataU.length >= 0) {
        dataU.forEach((item) => {
          if (data.checkedKeys.findIndex((a) => a == item) > -1) {
            data.checkedKeys.splice(
              data.checkedKeys.findIndex((a) => a == item),
              1,
            );
          }
        });
      }
    }
    AllcheckedKeys.value = [];
    permissionList.value.forEach((item) => {
      AllcheckedKeys.value.push(...item.checkedKeys);
    });
  }

  async function handleSubmit() {
    try {
      setModalProps({ confirmLoading: true });
      const data: Array<{
        Name: string;
        IsGranted: boolean;
      }> = [];
      sourceData.Groups.forEach((item) => {
        item.Permissions.forEach((permissionitem) => {
          if (AllcheckedKeys.value.findIndex((a: string) => a == permissionitem.Name) > -1) {
            if (permissionitem.IsGranted == false) {
              data.push({
                Name: permissionitem.Name,
                IsGranted: true,
              });
            }
          } else {
            if (permissionitem.IsGranted) {
              data.push({
                Name: permissionitem.Name,
                IsGranted: false,
              });
            }
          }
        });
      });
      if (data.length > 0) {
        await UpdatePermission(
          { providerName: 'U', providerKey: rowId.value },
          { Permissions: data },
        );
      }
      closeModal();
    } finally {
      setModalProps({ confirmLoading: false });
    }
  }
</script>

<style lang="less">
  .permission {
    margin: 12px;
    background-color: @component-background;

    .base-title {
      padding-left: 0;
    }

    .ant-tabs-tab-active {
      background-color: @item-active-bg;
    }
  }
</style>
