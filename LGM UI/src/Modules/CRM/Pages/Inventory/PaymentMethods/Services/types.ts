
export interface PaymentMethod {
  paymentMethodId: number;
  code: string;
  name: string;
  description?: string | null;
  isActive: boolean;
  isDeleted: boolean;
}

export type PaymentMethodListItem = PaymentMethod;
