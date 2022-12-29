<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    :title="t('AbpTenantManagement.Permission:ManageFeatures')"
    @ok="handleSubmit"
  >
    <ScrollContainer>
      <div ref="wrapperRef" :class="prefixCls">
        <Tabs
          v-model:activeKey="activeKey"
          tab-position="left"
          :tabBarStyle="tabBarStyle"
          @change="heandleChange"
        >
          <TabPane v-for="item in featureList" :tab="item.DisplayName" :key="item.Name">
            <FeatureTree :childData="item.Features" />
          </TabPane>
        </Tabs>
      </div>
    </ScrollContainer>
  </BasicModal>
</template>

<script lang="ts" setup>
  import { ref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { ScrollContainer } from '/@/components/Container/index';
  import { Tabs } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';

  import { features, FeaturesParams } from '/@/api/feature/model/featureModel';
  import { GetFeature, SetFeature } from '/@/api/feature/feature';
  import FeatureTree from './featureTree.vue';

  const TabPane = Tabs.TabPane;

  interface groups {
    Name: string;
    DisplayName: string;
    Features: Array<FeatureTree>;
  }

  interface FeatureTree {
    Features: features;
    Child: FeatureTree[];
  }

  const { t } = useI18n();
  const prefixCls = 'feature';

  const rowId = ref('');
  const activeKey = ref<string>('');
  const tabBarStyle = {
    width: '150px',
  };

  const featureList = ref<Array<groups>>([]);
  let Features: FeaturesParams = { Features: [] };

  const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
    setModalProps({ loading: true, confirmLoading: true });
    featureList.value = [];
    rowId.value = data.id;
    GetFeature({ providerName: 'T', providerKey: data.id }).then((res) => {
      activeKey.value = res.Groups[0].Name;
      res.Groups.forEach((item) => {
        var featuredata: groups = {
          Name: item.Name,
          DisplayName: item.DisplayName,
          Features: [],
        };
        item.Features.forEach((feature) => {
          if (feature.ParentName == null) {
            featuredata.Features.push({
              Features: {
                Name: feature.Name,
                DisplayName: feature.DisplayName,
                Value: feature.Value,
                Provider: feature.Provider,
                Description: feature.Description,
                ValueType: feature.ValueType,
                Depth: feature.Depth,
                ParentName: feature.ParentName,
              },
              Child: getchild(feature.Name, item.Features),
            });
          }
        });
        featureList.value.push(featuredata);
      });
      setModalProps({ loading: false, confirmLoading: false });
    });
  });

  function heandleChange(activeKeyStr: string) {
    activeKey.value = activeKeyStr;
  }

  function getchild(name: string, data: features[]) {
    const childrendata: FeatureTree[] = [];
    var child = data.filter((item) => item.ParentName == name);
    if (child.length == 0) {
      return [];
    } else {
      child.forEach((item) => {
        childrendata.push({
          Features: {
            Name: item.Name,
            DisplayName: item.DisplayName,
            Value: item.Value,
            Provider: item.Provider,
            Description: item.Description,
            ValueType: item.ValueType,
            Depth: item.Depth,
            ParentName: item.ParentName,
          },
          Child: getchild(item.Name, data),
        });
      });
      return childrendata;
    }
  }

  function getfeaturechild(Child: FeatureTree[]) {
    Child.forEach((item) => {
      Features.Features.push({
        Name: item.Features.Name,
        Value: item.Features.Value.toString(),
      });
      if (item.Child.length >= 1) {
        getfeaturechild(item.Child);
      }
    });
  }

  async function handleSubmit() {
    setModalProps({ loading: true, confirmLoading: true });
    Features.Features = [];
    featureList.value.forEach((item) => {
      item.Features.forEach((feature) => {
        Features.Features.push({
          Name: feature.Features.Name,
          Value: feature.Features.Value.toString(),
        });
        if (feature.Child.length >= 1) {
          getfeaturechild(feature.Child);
        }
      });
    });

    await SetFeature('T', rowId.value, Features);
    closeModal();
  }
</script>

<style lang="less">
  .form-check {
    margin-bottom: 1rem;
  }

  .text-muted {
    --bs-text-opacity: 1;
    color: #000;
    opacity: 0.675;
  }
</style>
