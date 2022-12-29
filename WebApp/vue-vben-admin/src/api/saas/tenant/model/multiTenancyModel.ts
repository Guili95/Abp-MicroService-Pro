import { BasicPageParams, BasicFetchResult, ConcurrencyStamp, Id } from '/@/api/model/baseModel';

export interface GetMultiTenancyByPageParams extends BasicPageParams {
  Filter?: string;
}

export type GetMultiTenancyByPageModel = BasicFetchResult<MultiTenancy>;

export interface CreateMultiTenancyDto {
  Name: string;
  AdminEmailAddress: string;
  AdminPassword: string;
}

export interface MultiTenancy extends ConcurrencyStamp, Id {
  Name: string;
}

export interface TenancyUpdateDto extends ConcurrencyStamp, Id {
  Name: string;
}
