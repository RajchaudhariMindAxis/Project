
export interface AccessoryCurrencyPrice {
  id?: number;
  currencyId: number;
  price: number;
  currencyName?: string;
  currencySymbol?: string;
}

export interface Accessories {
  accessoriesId: number;
  accessoriesCode?: string;
  accessorySKU?: string;
  partNo?: string;
  uom?: string;
  accessoriesName: string;

  // Image upload and display
  accessoriesImageFile?: File | null;
  accessoriesImage?: string;
  accessoriesImagePath?: string;

  // Vendor
  vendorName?: string;
  vendorId?: number;

  // Master IDs
  productGroupId?: number;
  categoryId?: number;
  brandId?: number;
  unitId?: number;
  taxId?: number;
  versionId?: number;
  currencyId?: number;

  // Description and specifications
  accessoriesDescription?: string;
  specification?: string;
  warrantyPeriod?: string;
  manufacturingDate?: string;

  // Calibration
  isCalibration: boolean;
  calibrationFrequency?: string;

  // Pricing
  purchasePrice: number;
  salePrice: number;
  currencyPrices?: AccessoryCurrencyPrice[];

  // Inventory
  trackStock: boolean;
  availableQuantity: number;
  minStock: number;
  outOfStockWarning: number;

  // Source and status
  productSource: "Local" | "Imported" | string;
  isActive: boolean;
  isDeleted: boolean;
}

export interface AccessoriesListItem {
  accessoriesId: number;
  accessoriesCode?: string;
  accessorySKU?: string;
  partNo?: string;
  accessoriesName: string;
  accessoriesDescription?: string;

  // Image fields returned by the API
  accessoriesImage?: string;
  accessoriesImagePath?: string;

  // Vendor
  vendorName?: string;
  vendorId?: number;

  // Master IDs
  productGroupId?: number;
  categoryId?: number;
  brandId?: number;
  unitId?: number;
  taxId?: number;
  versionId?: number;
  currencyId?: number;

  // Details
  uom?: string;
  specification?: string;
  warrantyPeriod?: string;
  manufacturingDate?: string;
  isCalibration?: boolean;
  calibrationFrequency?: string;

  // Pricing and stock
  purchasePrice?: number;
  salePrice: number;
  trackStock?: boolean;
  availableQuantity: number;
  minStock?: number;
  outOfStockWarning?: number;

  // Status
  productSource?: string;
  isActive?: boolean;
  isDeleted?: boolean;

  currencyPrices?: AccessoryCurrencyPrice[];
}

export interface TabProps {
  data: Accessories;
  setData: React.Dispatch<React.SetStateAction<Accessories>>;
  options: any;
  onAddMaster: (type: string) => void;
  errors?: { [key: string]: string[] };
}
