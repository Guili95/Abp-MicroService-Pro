import { defHttp } from '/@/utils/http/axios';
import { Id } from '/@/api/model/baseModel';

import {
  GetMultiTenancyByPageParams,
  GetMultiTenancyByPageModel,
  CreateMultiTenancyDto,
  MultiTenancy,
  TenancyUpdateDto,
} from './model/multiTenancyModel';

enum Api {
  Tenant = '/multi-tenancy/tenants',
}

export function GetTenantsByPage(params?: GetMultiTenancyByPageParams) {
  return defHttp.get<GetMultiTenancyByPageModel>({
    url: Api.Tenant,
    params,
  });
}

export function CreateTenant(data: CreateMultiTenancyDto) {
  return defHttp.post<MultiTenancy>({
    url: Api.Tenant,
    data,
  });
}

export function UpdateTenant(data: TenancyUpdateDto) {
  return defHttp.put<MultiTenancy>({
    url: Api.Tenant + `/${data.Id}`,
    data,
  });
}

export function DelTenant(params: Id) {
  return defHttp.delete({
    url: Api.Tenant + `/${params.Id}`,
  });
}
