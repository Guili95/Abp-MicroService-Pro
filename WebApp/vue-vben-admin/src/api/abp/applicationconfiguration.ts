import { defHttp } from '/@/utils/http/axios';

import { GetApplicationConfigurationModel } from './model/ApplicationConfigurationModel';

enum Api {
  ApplicationConfiguration = '/abp/application-configuration',
}

export function getApplicationConfiguration() {
  return defHttp.get<GetApplicationConfigurationModel>({
    url: Api.ApplicationConfiguration,
  });
}
