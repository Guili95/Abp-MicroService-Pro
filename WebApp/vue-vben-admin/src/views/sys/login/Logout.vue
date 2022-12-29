<template>
  <span>您已成功退出</span>
  <input id="ReturnUrl" hidden />
</template>

<script setup lang="ts">
  import { useUserStore } from '/@/store/modules/user';
  // import { useRoute } from 'vue-router';

  const userStore = useUserStore();

  // const route = useRoute();

  userStore.logout();
</script>
