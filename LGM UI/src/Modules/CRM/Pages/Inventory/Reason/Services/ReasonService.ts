import apiClient from "../../../../../../API/apiClient";
import { API_ENDPOINTS } from "../../../../../../API/apiConfig";
import type { Reason, ReasonListItem } from "./types";

const endpoint = API_ENDPOINTS.Reason;

export const getReasons = async (): Promise<ReasonListItem[]> => {
  const response = await apiClient.get(endpoint);
  const result = response.data?.data ?? response.data;

  return Array.isArray(result) ? result : [];
};

// Fetch code from GET /api/Reason/generate-code.
export const getGeneratedReasonCode = async (): Promise<string> => {
  const response = await apiClient.get(`${endpoint}/generate-code`);
  const result = response.data?.data ?? response.data;

  const code =
    typeof result === "string"
      ? result
      : result?.code ?? result?.Code;

  if (typeof code !== "string" || !code.trim()) {
    throw new Error("The Reason code API returned an invalid code.");
  }

  return code.trim();
};

export const getReasonById = async (id: number): Promise<Reason> => {
  const response = await apiClient.get(`${endpoint}/${id}`);
  return response.data?.data ?? response.data;
};

export const saveReason = async (data: Reason): Promise<void> => {
  const code =
    data.reasonId > 0
      ? data.code.trim()
      : data.code.trim() || await getGeneratedReasonCode();

  const payload = {
    reasonId: data.reasonId || 0,
    code,
    name: data.name.trim(),
    reasonType: data.reasonType.trim(),
    description: data.description?.trim() || null,
    isActive: data.isActive,
  };

  if (data.reasonId > 0) {
    await apiClient.put(`${endpoint}/${data.reasonId}`, payload);
  } else {
    await apiClient.post(endpoint, payload);
  }
};

export const deleteReason = async (id: number): Promise<void> => {
  await apiClient.delete(`${endpoint}/${id}`);
};