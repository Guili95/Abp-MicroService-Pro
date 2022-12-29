<template>
  <Loading :loading="compState.loading" :absolute="compState.absolute" :tip="compState.tip" />
</template>

<script lang="ts" setup>
  import { reactive } from 'vue';
  import { router } from '/@/router';
  import { PageEnum } from '/@/enums/pageEnum';

  import { Loading } from '/@/components/Loading';

  import { useAuthStore } from '/@/store/modules/auth';
  import { message } from 'ant-design-vue';

  const compState = reactive({
    absolute: false,
    loading: false,
    tip: '登录中',
  });

  async function openLoading(absolute: boolean) {
    compState.absolute = absolute;
    compState.loading = true;
    try {
      const OidcStore = useAuthStore();

      await OidcStore.signinRedirectCallback().then((data) => {
        if (data) {
          router.push('/dashboard/analysis');
        }
      });
    } catch {
      message.error('登陆失败');
      router.replace(PageEnum.BASE_HOME);
    } finally {
      compState.loading = false;
    }
  }

  openLoading(true);
</script>
