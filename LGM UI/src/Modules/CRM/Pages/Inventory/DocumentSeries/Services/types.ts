export interface DocumentSeries {
  documentSeriesId: number;
  code: string;
  documentType: string;
  prefix: string;
  nextNumber: number;
  financialYear: string;
  branchId: number;
  isActive: boolean;
  isDeleted: boolean;
}

export type DocumentSeriesListItem = DocumentSeries;