export interface Reason {
  reasonId: number;
  code: string;
  name: string;
  reasonType: string;
  description?: string | null;
  isActive: boolean;
  isDeleted?: boolean;
}

export type ReasonListItem = Reason;