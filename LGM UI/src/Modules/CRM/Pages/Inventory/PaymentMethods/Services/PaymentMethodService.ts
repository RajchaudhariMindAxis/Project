import apiClient from "../../../../../../API/apiClient";
import { API_ENDPOINTS } from "../../../../../../API/apiConfig";
import type {
  PaymentMethod,
  PaymentMethodListItem,
} from "./types";

const endpoint = API_ENDPOINTS.PaymentMethod;

export const getPaymentMethods = async (): Promise<
  PaymentMethodListItem[]
> => {
  const response = await apiClient.get(endpoint);
  const result = response.data?.data ?? response.data;

  return Array.isArray(result) ? result : [];
};

// Fetch generated code from the backend API.
export const getGeneratedPaymentMethodCode = async (): Promise<string> => {
  const response = await apiClient.get(`${endpoint}/generate-code`);
  const result = response.data?.data ?? response.data;

  const code =
    typeof result === "string"
      ? result
      : result?.code ?? result?.Code;

  if (typeof code !== "string" || !code.trim()) {
    throw new Error("The Payment Method code API returned an invalid code.");
  }

  return code.trim();
};

export const getPaymentMethodById = async (
  id: number,
): Promise<PaymentMethod> => {
  const response = await apiClient.get(`${endpoint}/${id}`);
  return response.data?.data ?? response.data;
};

export const savePaymentMethod = async (
  data: PaymentMethod,
): Promise<void> => {
  const code =
    data.paymentMethodId > 0
      ? data.code.trim()
      : data.code.trim() || await getGeneratedPaymentMethodCode();

  const payload = {
    paymentMethodId: data.paymentMethodId || 0,
    code,
    name: data.name.trim(),
    description: data.description?.trim() || null,
    isActive: data.isActive,
  };

  if (data.paymentMethodId > 0) {
    await apiClient.put(
      `${endpoint}/${data.paymentMethodId}`,
      payload,
    );
  } else {
    await apiClient.post(endpoint, payload);
  }
};

export const deletePaymentMethod = async (
  id: number,
): Promise<void> => {
  await apiClient.delete(`${endpoint}/${id}`);
};