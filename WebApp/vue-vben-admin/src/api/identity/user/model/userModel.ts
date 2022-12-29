import { BasicFetchResult, ConcurrencyStamp, Id } from '/@/api/model/baseModel';
import { Roles } from '/@/api/identity/role/model/roleModel';

export interface Users extends Id, ConcurrencyStamp {
  CreationTime: string;
  CreatorId: string;
  LastModificationTime: string;
  LastModifierId: string;
  IsDeleted: boolean;
  DeleterId: boolean;
  DeletionTime: string;
  TenantId: string;
  UserName: string;
  Email: string;
  Name: string;
  Surname: string;
  EmailConfirmed: boolean;
  PhoneNumber: string;
  PhoneNumberConfirmed: boolean;
  IsActive: boolean;
  LockoutEnabled: boolean;
  LockoutEnd: string;
}

export interface GetUserByPageParams {
  Sorting?: string;
  Filter?: string;
}

export type GetUsersByPageModel = BasicFetchResult<Users>;

export interface IdentityUserCreateDto extends IdentityUserCreateOrUpdateDtoBase {
  Password: string;
}

export interface IdentityUserCreateOrUpdateDtoBase {
  UserName: string;
  Name: string;
  Surname: string;
  Email: string;
  PhoneNumber: string;
  IsActive: boolean;
  LockoutEnabled: string;
  RoleNames: string[];
}

export interface IdentityUserUpdateDto
  extends IdentityUserCreateOrUpdateDtoBase,
    ConcurrencyStamp,
    Id {}

export interface GetRolesByPageParams {
  Sorting?: string;
  Filter?: string;
}

export type GetRolesByPageModel = BasicFetchResult<Roles>;
