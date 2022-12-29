import { defHttp } from '/@/utils/http/axios';
import { Id } from '/@/api/model/baseModel';

import {
  GetRolesByPageModel,
  GetRoleByPageParams,
  IdentityRoleCreateDto,
  Roles,
  IdentityRoleUpdateDto,
} from './model/roleModel';

enum Api {
  Role = '/identity/roles',
}

export function GetRoleByPage(params?: GetRoleByPageParams) {
  return defHttp.get<GetRolesByPageModel>({
    url: Api.Role,
    params,
  });
}

export function CreateRole(data: IdentityRoleCreateDto) {
  return defHttp.post<Roles>({
    url: Api.Role,
    data,
  });
}

export function UpdateRole(data: IdentityRoleUpdateDto) {
  return defHttp.put<Roles>({
    url: Api.Role + `/${data.Id}`,
    data,
  });
}

export function DelRole(id: Id) {
  return defHttp.delete({
    url: Api.Role + `/${id.Id}`,
  });
}
