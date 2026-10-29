
export interface ProductAccessoryRule {
  id: number;
  productId: number;
  productName?: string | null;
  accessoriesId: number;
  accessoryName?: string | null;
  quantity: number;
  mandatoryOptional: string;
  pricingType: string;
  defaultPrice: number;
  description: string;
  effectiveFrom: string;
  effectiveTo: string | null;
  active: boolean;
  isDeleted?: boolean;
}

export interface ProductAccessoryRuleListItem
  extends ProductAccessoryRule {
  productCode?: string | null;
  accessoryCode?: string | null;
}

export interface ProductAccessoryRuleLookupOption {
  id: number;
  code: string;
  name: string;
}
