export interface PermissionParams {
  providerName: string;
  providerKey: string;
}

export interface GetPermissionModel {
  EntityDisplayName: string;
  Groups: Array<groups>;
}

interface groups {
  Name: string;
  DisplayName: string;
  Permissions: Array<permissions>;
}

export interface permissions {
  Name: string;
  DisplayName: string;
  ParentName: string;
  IsGranted: boolean;
  AllowedProviders: Array<string>;
  GrantedProviders: Array<Providers>;
}

interface Providers {
  ProviderName: string;
  ProviderKey: string;
}

export interface putpermissions {
  Permissions: Array<{
    Name: string;
    IsGranted: boolean;
  }>;
}
