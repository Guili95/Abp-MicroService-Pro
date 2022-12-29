import { BasicFetchResult, ConcurrencyStamp, Id, BasicListResult } from '/@/api/model/baseModel';

export interface Roles extends Id, ConcurrencyStamp {
  Name: string;
  IsDefault: boolean;
  IsStatic: boolean;
  IsPublic: boolean;
}

export type GetRolesByPageModel = BasicFetchResult<Roles>;

export interface GetRoleByPageParams {
  Filter?: string;
}

export type IdentityRoleCreateDto = IdentityRoleCreateOrUpdateDtoBase;

interface IdentityRoleCreateOrUpdateDtoBase {
  Name: string;
  IsDefault: boolean;
  IsPublic: boolean;
}

export interface IdentityRoleUpdateDto
  extends IdentityRoleCreateOrUpdateDtoBase,
    ConcurrencyStamp,
    Id {}

export type GetRolesListModel = BasicListResult<Roles>;
