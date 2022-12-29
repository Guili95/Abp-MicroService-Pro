import { defHttp } from '/@/utils/http/axios';

import { FeatureParams, GetFeatureModel, FeaturesParams } from './model/featureModel';

enum Api {
  Feature = '/feature-management/features',
}

export function GetFeature(params: FeatureParams) {
  return defHttp.get<GetFeatureModel>({
    url: Api.Feature,
    params,
  });
}

export function SetFeature(providerName: string, providerKey: string, data: FeaturesParams) {
  return defHttp.put({
    url: Api.Feature + `?providerName=${providerName}&providerKey=${providerKey}`,
    data,
  });
}
