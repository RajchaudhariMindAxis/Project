
import apiClient from "../../../../../../API/apiClient";
import { API_ENDPOINTS } from "../../../../../../API/apiConfig";
import type { Bundle, BundleListItem } from "./type";

const endpoint = API_ENDPOINTS.Bundle;

export const getBundles = async (): Promise<BundleListItem[]> => {
  const response = await apiClient.get(endpoint);
  const data = response.data?.data ?? response.data;

  return Array.isArray(data) ? data : [];
};

// Generate a Bundle code using GET /api/Bundle/generate-code
export const getGeneratedBundleCode = async (): Promise<string> => {
  const response = await apiClient.get(`${endpoint}/generate-code`);
  const result = response.data?.data ?? response.data;

  const code =
    typeof result === "string"
      ? result
      : result?.code ?? result?.Code;

  if (typeof code !== "string" || !code.trim()) {
    throw new Error("Bundle code API returned an invalid code.");
  }

  return code.trim();
};

export const getBundleById = async (id: number): Promise<Bundle> => {
  const response = await apiClient.get(`${endpoint}/${id}`);
  return response.data?.data ?? response.data;
};

export const saveBundle = async (data: Bundle): Promise<void> => {
  const code =
    data.bundleId > 0
      ? data.code.trim()
      : data.code.trim() || await getGeneratedBundleCode();

  const payload = {
    bundleId: data.bundleId || 0,
    code,
    name: data.name.trim(),
    parentProductId: Number(data.parentProductId),
    packagePrice: Number(data.packagePrice),
    taxMode: data.taxMode.trim(),
    effectiveFrom: data.effectiveFrom,
    effectiveTo: data.effectiveTo || null,
    description: data.description?.trim() || null,
    isActive: data.isActive,
    isDeleted: data.isDeleted ?? false,
  };

  if (data.bundleId > 0) {
    await apiClient.put(`${endpoint}/${data.bundleId}`, payload);
  } else {
    await apiClient.post(endpoint, payload);
  }
};

export const deleteBundle = async (id: number): Promise<void> => {
  await apiClient.delete(`${endpoint}/${id}`);
};
