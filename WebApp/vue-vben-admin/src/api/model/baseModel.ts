export interface BasicPageParams {
  page: number;
  pageSize: number;
}

export interface BasicFetchResult<T> {
  Items: T[];
  Total: number;
}

export interface ConcurrencyStamp {
  ConcurrencyStamp: string;
}

export interface Id {
  Id: string;
}

export interface ParameterId {
  id: string;
}

export interface BasicListResult<T> {
  Items: T[];
}

export type IdsList = BasicListResult<string>;
