import { defHttp } from '/@/utils/http/axios';
import { Id } from '/@/api/model/baseModel';

import {
  GetUsersByPageModel,
  IdentityUserCreateDto,
  Users,
  IdentityUserUpdateDto,
  GetUserByPageParams,
} from './model/userModel';
import { GetRolesListModel } from '/@/api/identity/role/model/roleModel';
import { OrganizationUnitDto } from '/@/api/identity/organization-unit/model/organizationUnitModel';

enum Api {
  User = '/identity/users',
}

export function GetUserByPage(params?: GetUserByPageParams) {
  return defHttp.get<GetUsersByPageModel>({
    url: Api.User,
    params,
  });
}

export function CreateUser(data: IdentityUserCreateDto) {
  return defHttp.post<Users>({
    url: Api.User,
    data,
  });
}

export function UpdateUser(data: IdentityUserUpdateDto) {
  return defHttp.put<Users>({
    url: Api.User + `/${data.Id}`,
    data,
  });
}

export function DelUser(id: Id) {
  return defHttp.delete({
    url: Api.User + `/${id.Id}`,
  });
}

export function GetUserAssignableRoles() {
  return defHttp.get<GetRolesListModel>({
    url: Api.User + '/assignable-roles',
  });
}

export function UpdateOrganizationUnits(data: { id: string; OrganizationUnitIds: string[] }) {
  return defHttp.put({
    url: Api.User + `/${data.id}/organization-units`,
    data,
  });
}

export function GetUserRole(id: string) {
  return defHttp.get<GetRolesListModel>({
    url: Api.User + `/${id}/roles`,
  });
}

export function GetUserOrganizationUnits(id: string) {
  return defHttp.get<Array<OrganizationUnitDto>>({
    url: Api.User + `/${id}/organization-units`,
  });
}
