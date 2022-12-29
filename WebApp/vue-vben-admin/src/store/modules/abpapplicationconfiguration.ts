import { defineStore } from 'pinia';
import { store } from '/@/store';

import { getApplicationConfiguration } from '/@/api/abp/applicationconfiguration';
import type { GetApplicationConfigurationModel } from '/@/api/abp/model/ApplicationConfigurationModel';

export const useAbpApplicationConfigurationStore = defineStore({
  id: 'app',
  getters: {},
  actions: {
    async getApplicationConfiguration(): Promise<GetApplicationConfigurationModel> {
      const data = await getApplicationConfiguration();
      return data;
    },
  },
});

export function useAbpApplicationConfigurationStoreWithOut() {
  return useAbpApplicationConfigurationStore(store);
}
