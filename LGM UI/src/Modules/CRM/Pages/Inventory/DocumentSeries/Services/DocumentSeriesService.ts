import apiClient from "../../../../../../API/apiClient";
import { API_ENDPOINTS } from "../../../../../../API/apiConfig";
import type {
  DocumentSeries,
  DocumentSeriesListItem,
} from "./types";

const endpoint = API_ENDPOINTS.DocumentSeries;

export const getGeneratedDocumentSeriesCode =
  async (): Promise<string> => {
    const response = await apiClient.get(`${endpoint}/generate-code`);

    const code =
      response.data?.code ??
      response.data?.data?.code;

    if (!code || typeof code !== "string") {
      throw new Error("The API did not return a valid document series code.");
    }

    return code;
  };

export const getDocumentSeries = async (): Promise<
  DocumentSeriesListItem[]
> => {
  const response = await apiClient.get(endpoint);

  const result = response.data?.data ?? response.data;

  return Array.isArray(result) ? result : [];
};

export const getDocumentSeriesById = async (
  id: number
): Promise<DocumentSeries> => {
  const response = await apiClient.get(`${endpoint}/${id}`);

  return response.data?.data ?? response.data;
};

export const saveDocumentSeries = async (
  data: DocumentSeries
): Promise<void> => {
  const payload = {
    documentSeriesId: data.documentSeriesId || 0,
    code: data.code.trim(),
    documentType: data.documentType.trim(),
    prefix: data.prefix.trim(),
    nextNumber: Number(data.nextNumber),
    financialYear: data.financialYear.trim(),
    branchId: Number(data.branchId),
    isActive: data.isActive ?? true,
    isDeleted: false,
  };

  if (data.documentSeriesId > 0) {
    await apiClient.put(
      `${endpoint}/${data.documentSeriesId}`,
      payload
    );
  } else {
    await apiClient.post(endpoint, payload);
  }
};

export const deleteDocumentSeries = async (
  id: number
): Promise<void> => {
  await apiClient.delete(`${endpoint}/${id}`);
};