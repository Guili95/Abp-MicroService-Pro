import { defHttp } from '/@/utils/http/axios';

import { PermissionParams, GetPermissionModel, putpermissions } from './model/permissionModel';

enum Api {
  Permission = '/permission-management/permissions',
}

export function GetPermission(params: PermissionParams) {
  return defHttp.get<GetPermissionModel>({
    url: Api.Permission,
    params,
  });
}

export function UpdatePermission(params: PermissionParams, data: putpermissions) {
  return defHttp.put({
    url: Api.Permission + `?providerName=${params.providerName}&providerKey=${params.providerKey}`,
    data,
  });
}
