export interface GetApplicationConfigurationModel {
  Localization: localization;
  Auth: {
    Policies: any | {};
    GrantedPolicies: any | {};
  };
  Setting: {
    Values: any | {};
  };
  CurrentUser: {
    IsAuthenticated: boolean;
    Id: null | string;
    TenantId: null | string;
    ImpersonatorUserId: null | string;
    ImpersonatorTenantId: null | string;
    ImpersonatorUserName: null | string;
    ImpersonatorTenantName: null | string;
    UserName: null | string;
    Name: null | string;
    SurName: null | string;
    Email: null | string;
    EmailVerified: boolean;
    PhoneNumber: null | string;
    PhoneNumberVerified: boolean;
    Roles: Array<string>;
  };
  Features: {
    Values: any | {};
  };
  MultiTenancy: {
    IsEnabled: boolean;
  };
  CurrentTenant: {
    Id: null | string;
    Name: null | string;
    IsAvailable: boolean;
  };
  Timing: {
    TimeZone: {
      Iana: {
        TimeZoneName: string;
      };
      Windows: {
        TimeZoneId: string;
      };
    };
  };
  Clock: {
    Kind: string;
  };
  ObjectExtensions: {
    Modules: any | {};
    Enums: any | {};
  };
}

interface localization {
  Values: any | {};
  Languages: Array<languages>;
  CurrentCulture: {
    DisplayName: string;
    EnglishName: string;
    ThreeLetterIsoLanguageName: string;
    TwoLetterIsoLanguageName: string;
    IsRightToLeft: boolean;
    CultureName: string;
    Name: string;
    NativeName: string;
    DateTimeFormat: {
      DalendarAlgorithmType: string;
      DateTimeFormatLong: string;
      ShortDatePattern: string;
      FullDateTimePattern: string;
      DateSeparator: string;
      ShortTimePattern: string;
      LongTimePattern: string;
    };
    DefaultResourceName: string;
    LanguagesMap: any | {};
    LanguageFilesMap: any | {};
  };
}

interface languages {
  CultureName: string;
  UiCultureName: string;
  DisplayName: string;
  FlagIcon: string;
}
