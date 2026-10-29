
import apiClient from "../../../../../API/apiClient";
import { API_ENDPOINTS } from "../../../../../API/apiConfig";
import type { Accessories, AccessoriesListItem } from "./types";

export const getAccessories = async (): Promise<AccessoriesListItem[]> => {
  const res = await apiClient.get(API_ENDPOINTS.Accessories);

  console.log("Accessories API response:", res.data);
  console.log("First accessory:", res.data?.[0]);

  return Array.isArray(res.data) ? res.data : [];
};

export const getAccessoryById = async (
  id: number
): Promise<Accessories> => {
  const res = await apiClient.get(
    `${API_ENDPOINTS.Accessories}/${id}`
  );
  return res.data;
};

export const getGeneratedAccessoryCode = async (
  vendorId: number,
  productSource: "Local" | "Imported"
): Promise<string> => {
  if (!vendorId) return "";

  const typeCode = 2;
  const originCode = productSource === "Imported" ? 1 : 2;

  const res = await apiClient.get(
    `${API_ENDPOINTS.Accessories}/generate-part-number`,
    {
      params: {
        vendorId,
        typeCode,
        originCode,
      },
    }
  );

  // Supports APIs returning either a string or an object.
  if (typeof res.data === "string") {
    return res.data;
  }

  return (
    res.data?.accessoriesCode ??
    res.data?.accessorySKU ??
    res.data?.partNo ??
    res.data?.partNumber ??
    res.data?.code ??
    ""
  );
};

const appendIfDefined = (
  formData: FormData,
  key: string,
  value: string | number | boolean | null | undefined
) => {
  if (value === null || value === undefined) return;

  formData.append(key, String(value));
};

export const saveAccessory = async (
  data: Accessories
): Promise<void> => {
  const formData = new FormData();

  // Basic information
  formData.append("AccessoriesId", String(data.accessoriesId || 0));
  formData.append("AccessoriesName", data.accessoriesName || "");

  appendIfDefined(formData, "AccessoriesCode", data.accessoriesCode);
  appendIfDefined(formData, "AccessorySKU", data.accessorySKU);
  appendIfDefined(formData, "PartNo", data.partNo);
  appendIfDefined(formData, "UOM", data.uom);

  // Vendor: use the field supported by the updated API.
  appendIfDefined(formData, "VendorName", data.vendorName);

  // Master IDs
  appendIfDefined(formData, "ProductGroupId", data.productGroupId);
  appendIfDefined(formData, "CategoryId", data.categoryId);
  appendIfDefined(formData, "BrandId", data.brandId);
  appendIfDefined(formData, "UnitId", data.unitId);
  appendIfDefined(formData, "TaxId", data.taxId);
  appendIfDefined(formData, "VersionId", data.versionId);
  appendIfDefined(formData, "CurrencyId", data.currencyId);

  // Descriptions and specifications
  appendIfDefined(
    formData,
    "AccessoriesDescription",
    data.accessoriesDescription
  );
  appendIfDefined(formData, "Specification", data.specification);
  appendIfDefined(formData, "WarrantyPeriod", data.warrantyPeriod);

  // Manufacturing date
  if (data.manufacturingDate) {
    formData.append("ManufacturingDate", data.manufacturingDate);
  }

  // Pricing
  appendIfDefined(formData, "PurchasePrice", data.purchasePrice ?? 0);
  appendIfDefined(formData, "SalePrice", data.salePrice ?? 0);

  // Inventory
  appendIfDefined(formData, "TrackStock", data.trackStock ?? false);
  appendIfDefined(
    formData,
    "AvailableQuantity",
    data.availableQuantity ?? 0
  );
  appendIfDefined(formData, "MinStock", data.minStock ?? 0);
  appendIfDefined(
    formData,
    "OutOfStockWarning",
    data.outOfStockWarning ?? 5
  );

  // Calibration
  appendIfDefined(formData, "IsCalibration", data.isCalibration ?? false);
  appendIfDefined(
    formData,
    "CalibrationFrequency",
    data.calibrationFrequency
  );

  // Product source
  appendIfDefined(formData, "ProductSource", data.productSource || "Local");

  // Record status
  appendIfDefined(formData, "IsActive", data.isActive ?? true);
  appendIfDefined(formData, "IsDeleted", data.isDeleted ?? false);

  // Upload a new image only when the user selected one.
  if (data.accessoriesImageFile instanceof File) {
    formData.append(
      "AccessoriesImageFile",
      data.accessoriesImageFile
    );
  }

  // Preserve an existing image path when supplied.
  // The backend must support AccessoriesImage for this field to be persisted.
  if (data.accessoriesImage) {
    formData.append("AccessoriesImage", data.accessoriesImage);
  } else if (data.accessoriesImagePath) {
    formData.append("AccessoriesImage", data.accessoriesImagePath);
  }

  if (data.accessoriesId > 0) {
    await apiClient.put(
      `${API_ENDPOINTS.Accessories}/${data.accessoriesId}`,
      formData
    );
  } else {
    await apiClient.post(
      API_ENDPOINTS.Accessories,
      formData
    );
  }
};

export const deleteAccessory = async (
  id: number
): Promise<void> => {
  await apiClient.delete(`${API_ENDPOINTS.Accessories}/${id}`);
};

export const importAccessoriesExcel = async (
  file: File
): Promise<any> => {
  const formData = new FormData();
  formData.append("file", file);

  const response = await apiClient.post(
    `${API_ENDPOINTS.Accessories}/upload-excel`,
    formData
  );

  return response.data;
};

export const downloadAccessoriesExcel = async (): Promise<Blob> => {
  const response = await apiClient.get(
    `${API_ENDPOINTS.Accessories}/export-excel`,
    {
      responseType: "blob",
    }
  );

  return response.data;
};
