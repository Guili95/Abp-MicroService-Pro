import { Id, ParameterId, IdsList } from '/@/api/model/baseModel';
import {
  GetUserByPageParams,
  GetUsersByPageModel,
  GetRolesByPageParams,
  GetRolesByPageModel,
} from '/@/api/identity/user/model/userModel';

import { defHttp } from '/@/utils/http/axios';

import {
  GetOrganizationUnitListModel,
  OrganizationUnitDto,
  OrganizationUnitCreateDto,
  OrganizationUnitUpdateDto,
  OrganizationUnitMoveInput,
  AddMemberToOrganizationUnitInput,
  AddRoleToOrganizationUnitInput,
} from './model/organizationUnitModel';

enum Api {
  OrganizationUnit = '/identity/organization-units',
}

export function GetAll() {
  return defHttp.get<GetOrganizationUnitListModel>({
    url: Api.OrganizationUnit + '/all',
  });
}

export function GetById(id: Id) {
  return defHttp.get<OrganizationUnitDto>({
    url: Api.OrganizationUnit + `/${id.Id}`,
  });
}

export function CreateOrganizationUnit(data: OrganizationUnitCreateDto) {
  return defHttp.post<OrganizationUnitDto>({
    url: Api.OrganizationUnit,
    data,
  });
}

export function UpdateOrganizationUnit(data: Id & OrganizationUnitUpdateDto) {
  return defHttp.put<OrganizationUnitDto>({
    url: Api.OrganizationUnit + `/${data.Id}`,
    data,
  });
}

export function MoveOrganization(data: ParameterId & OrganizationUnitMoveInput) {
  return defHttp.put({
    url: Api.OrganizationUnit + `/${data.id}/move`,
    data,
  });
}

export function DelOrganizationUnit(params: ParameterId) {
  return defHttp.delete({
    url: Api.OrganizationUnit + `?id=${params.id}`,
    params,
  });
}

export function GetOrganizationUnitUsers(params: GetUserByPageParams & { id: string | null }) {
  if (!params.id) {
    return new Promise((resolve, _reject) => {
      resolve({ items: [], total: 0 });
    });
  }
  return defHttp.get<GetUsersByPageModel>({
    url: Api.OrganizationUnit + `/${params.id}/members`,
    params,
  });
}

export function GetOrganizationUnitUsersIdAll(params: ParameterId) {
  return defHttp.get<IdsList>({
    url: Api.OrganizationUnit + `/${params.id}/membersidall`,
  });
}

export function OrganizationAddMembers(data: ParameterId & AddMemberToOrganizationUnitInput) {
  return defHttp.put({
    url: Api.OrganizationUnit + `/${data.id}/members`,
    data,
  });
}

export function RemoveUser(params: { id: string; userId: string }) {
  return defHttp.delete({
    url: Api.OrganizationUnit + `/${params.id}/members/${params.userId}`,
  });
}

export function GetOrganizationUnitRoles(params: GetRolesByPageParams & { id: string | null }) {
  if (!params.id) {
    return new Promise((resolve, _reject) => {
      resolve({ items: [], total: 0 });
    });
  }
  return defHttp.get<GetRolesByPageModel>({
    url: Api.OrganizationUnit + `/${params.id}/roles`,
    params,
  });
}

export function GetOrganizationUnitRolesIdAll(params: ParameterId) {
  return defHttp.get<IdsList>({
    url: Api.OrganizationUnit + `/${params.id}/rolesidall`,
  });
}

export function OrganizationAddRoles(data: ParameterId & AddRoleToOrganizationUnitInput) {
  return defHttp.put({
    url: Api.OrganizationUnit + `/${data.id}/roles`,
    data,
  });
}

export function RemoveRole(params: { id: string; roleId: string }) {
  return defHttp.delete({
    url: Api.OrganizationUnit + `/${params.id}/roles/${params.roleId}`,
  });
}
