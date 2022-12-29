<template>
  <template v-for="item in childData" :key="item.Name">
    <template v-if="item.Features.ValueType.Name == 'ToggleStringValueType'">
      <div class="flex-col form-check">
        <Checkbox
          :checked="item.Features.Value == 'true' ? true : false"
          class="w-full"
          @change="onCheckChange(item.Features)"
          >{{ item.Features.DisplayName }}
        </Checkbox>
        <small class="w-full ml-6 text-muted">{{ item.Features.Description }}</small>
      </div>
      <div class="ml-6">
        <FeatureTree :childData="item.Child" />
      </div>
    </template>
  </template>
</template>

<script lang="ts">
  export default {
    name: 'FeatureTree',
  };
</script>

<script lang="ts" setup>
  import { Checkbox } from 'ant-design-vue';
  import { features } from '/@/api/feature/model/featureModel';

  interface FeatureTree {
    Features: features;
    Child: FeatureTree[];
  }

  defineProps({
    childData: {
      type: Object as PropType<Array<FeatureTree>>,
    },
  });

  function onCheckChange(data) {
    console.log(data.Value);
    data.Value = data.Value == 'true' ? 'false' : 'true';
  }
</script>
