export interface FeatureParams {
  providerName: string;
  providerKey: string;
}

export interface GetFeatureModel {
  Groups: Array<groups>;
}

export interface groups {
  Name: string;
  DisplayName: string;
  Features: Array<features>;
}

export interface features {
  Name: string;
  DisplayName: string;
  Value: string;
  Provider: provider;
  Description: string;
  ValueType: valueType;
  Depth: number;
  ParentName: string | null;
}

interface provider {
  Name: string;
  key: string | null;
}

interface valueType {
  Name: 'ToggleStringValueType';
  Properties: any;
  Validator: validator;
}

interface validator {
  Name: string;
  Properties: any;
}

interface Feature {
  Name: string;
  Value: string;
}

export interface FeaturesParams {
  Features: Feature[];
}
