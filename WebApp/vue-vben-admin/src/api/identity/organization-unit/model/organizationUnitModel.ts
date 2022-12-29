import { BasicListResult, ConcurrencyStamp } from '/@/api/model/baseModel';

export type GetOrganizationUnitListModel = BasicListResult<OrganizationUnitDto>;

export interface OrganizationUnitDto {
  Id: string;
  CreationTime: string;
  CreatorId: string;
  LastModificationTime: string;
  LastModifierId: string;
  IsDeleted: boolean;
  DeleterId: string;
  DeletionTime: string;
  ParentId: string | null;
  Code: string;
  DisplayName: string;
  ConcurrencyStamp: string;
}

export interface OrganizationUnitCreateDto extends OrganizationUnitCreateOrUpdateDtoBase {
  ParentId: string | null;
}

export interface OrganizationUnitCreateOrUpdateDtoBase {
  DisplayName: string;
}

export interface OrganizationUnitUpdateDto
  extends OrganizationUnitCreateOrUpdateDtoBase,
    ConcurrencyStamp {}

export interface OrganizationUnitMoveInput {
  NewParentId?: string;
}

export interface AddMemberToOrganizationUnitInput {
  UserIds: Array<string>;
}

export interface AddRoleToOrganizationUnitInput {
  RoleIds: Array<string>;
}
