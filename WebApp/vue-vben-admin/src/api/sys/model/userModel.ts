import { BasicFetchResult } from '/@/api/model/baseModel';

/**
 * @description: 登录接口参数
 */
export interface LoginParams {
  userNameOrEmailAddress: string;
  password: string;
  rememberMe: boolean;
}

export interface RoleInfo {
  roleName: string;
  value: string;
}

/**
 * @description: 登录接口原生返回值
 */
export interface LoginResultModel {
  Result: number;
  Description: string;
}

/**
 * @description: 登录接口返回值
 */
export type WrapLoginResultModel = BasicFetchResult<LoginResultModel>;

/**
 * @description: Get user information return value
 */
export interface GetUserInfoModel {
  roles: RoleInfo[];
  // 用户id
  userId: string | number;
  // 用户名
  username: string;
  // 真实名字
  realName: string;
  // 头像
  avatar: string;
  // 介绍
  desc?: string;
}
