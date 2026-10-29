
import React from "react";
import type { Accessories } from "./Services/types";
import { API_ENDPOINTS } from "../../../../API/apiConfig";
import {
  FaTimes,
  FaTools,
  FaTags,
  FaInfoCircle,
  FaRupeeSign,
  FaWarehouse,
  FaCogs,
  FaCalendarAlt,
  FaShieldAlt,
  FaBarcode,
  FaCheckCircle,
} from "react-icons/fa";

const FILE_BASE_URL = API_ENDPOINTS.BaseUrl.replace(/\/api\/?$/, "");

interface ViewModalProps {
  data: Accessories;
  onClose: () => void;
  options: any;
}

const AccessoriesViewModal: React.FC<ViewModalProps> = ({
  data,
  onClose,
  options,
}) => {
  const getLabel = (list: any[] | undefined, id?: number | null) => {
    if (id === null || id === undefined || id === 0 || !list) {
      return "N/A";
    }

    const found = list.find(
      (item) => Number(item.value) === Number(id)
    );

    return found?.label ?? "N/A";
  };

  const formatDate = (value?: string | Date | null) => {
    if (!value) return "N/A";

    const date = new Date(value);

    return Number.isNaN(date.getTime())
      ? "N/A"
      : date.toLocaleDateString();
  };

  const formatMoney = (value?: number | null) => {
    if (value === null || value === undefined || value === ("" as any)) {
      return "N/A";
    }

    return Number(value).toLocaleString("en-IN", {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    });
  };

  const imagePath =
    (data as any).accessoriesImage ||
    (data as any).accessoriesImagePath;

  const imageUrl = imagePath
    ? imagePath.startsWith("http")
      ? imagePath
      : `${FILE_BASE_URL}${imagePath.startsWith("/") ? "" : "/"}${imagePath}`
    : null;

  const DataItem = ({
    label,
    value,
    alert = false,
  }: {
    label: string;
    value?: React.ReactNode;
    alert?: boolean;
  }) => (
    <div className="flex flex-col border-b border-black/[.06] py-3">
      <span className="text-[10px] font-extrabold uppercase tracking-[.15em] text-[#8a8984]">
        {label}
      </span>

      <span
        className={`break-words text-sm font-bold ${
          alert ? "text-red-600" : "text-[#343331]"
        }`}
      >
        {value === null || value === undefined || value === ""
          ? "N/A"
          : value}
      </span>
    </div>
  );

  const SectionHeader = ({
    icon: Icon,
    title,
  }: {
    icon: React.ElementType;
    title: string;
  }) => (
    <div className="mb-4 mt-6 flex items-center gap-2 first:mt-0">
      <Icon className="text-xl text-[#f07837]" />
      <h3 className="grow border-b-2 border-[#f07837]/20 pb-1 text-sm font-black uppercase tracking-tight text-[#252a2e]">
        {title}
      </h3>
    </div>
  );

  const StatusPill = ({
    active,
    label,
  }: {
    active: boolean;
    label: string;
  }) => (
    <span
      className={`inline-flex items-center gap-1 rounded-lg px-2.5 py-1 text-xs font-bold ${
        active
          ? "bg-green-100 text-green-700"
          : "bg-gray-100 text-gray-600"
      }`}
    >
      <FaCheckCircle size={11} />
      {label}
    </span>
  );

  const isLowStock =
    Boolean(data.trackStock) &&
    Number(data.availableQuantity ?? 0) <=
      Number(data.outOfStockWarning ?? 0);

  return (
    <div className="fixed inset-0 z-[1000] flex items-center justify-center bg-black/40 p-4 text-left backdrop-blur-sm">
      <div className="flex max-h-[95vh] w-full max-w-7xl animate-in flex-col overflow-hidden rounded-[24px] bg-white shadow-2xl zoom-in duration-300">
        {/* Header */}
        <div className="flex items-center justify-between border-b-4 border-[#f07837] bg-[#252a2e] p-4 text-white shadow-lg sm:p-6">
          <div className="flex min-w-0 items-center gap-4 sm:gap-6">
            <div className="flex h-16 w-16 shrink-0 items-center justify-center overflow-hidden rounded-2xl bg-white p-1 shadow-inner sm:h-24 sm:w-24">
              {imageUrl ? (
                <img
                  src={imageUrl}
                  alt={data.accessoriesName || "Accessory"}
                  className="h-full w-full object-contain"
                />
              ) : (
                <FaTools className="text-4xl text-[#d6d2ca] sm:text-5xl" />
              )}
            </div>

            <div className="min-w-0">
              <h2 className="truncate text-xl font-black sm:text-3xl">
                {data.accessoriesName || "Accessory details"}
              </h2>

              <div className="mt-3 flex flex-wrap gap-2">
                <span className="rounded-lg border border-white/15 bg-white/10 px-3 py-1 text-[10px] font-black uppercase tracking-wider text-white/80">
                  CODE: {data.accessoriesCode || "N/A"}
                </span>

                <span className="rounded-lg bg-[#f07837] px-3 py-1 text-[10px] font-black uppercase tracking-wider text-white">
                  SOURCE: {data.productSource || "N/A"}
                </span>

                {(data as any).accessorySKU && (
                  <span className="rounded-lg border border-white/15 bg-white/10 px-3 py-1 text-[10px] font-black uppercase tracking-wider text-white/80">
                    SKU: {(data as any).accessorySKU}
                  </span>
                )}
              </div>
            </div>
          </div>

          <button
            type="button"
            onClick={onClose}
            aria-label="Close details"
            className="shrink-0 rounded-full bg-white/10 p-2 text-white transition hover:rotate-90 hover:bg-white/20 sm:p-3"
          >
            <FaTimes size={24} />
          </button>
        </div>

        {/* Body */}
        <div className="overflow-y-auto bg-[#f7f6f2] p-5 sm:p-8">
          <div className="mb-6 flex flex-wrap gap-3">
            <StatusPill
              active={Boolean(data.isActive)}
              label={data.isActive ? "Active" : "Inactive"}
            />

            <StatusPill
              active={!data.isDeleted}
              label={data.isDeleted ? "Deleted" : "Not Deleted"}
            />

            <StatusPill
              active={Boolean(data.trackStock)}
              label={data.trackStock ? "Stock Tracked" : "Stock Not Tracked"}
            />
          </div>

          <div className="grid grid-cols-1 gap-x-8 gap-y-8 md:grid-cols-2 lg:grid-cols-4">
            {/* Basic information */}
            <section>
              <SectionHeader icon={FaTags} title="Basic Information" />

              <DataItem
                label="Accessories Name"
                value={data.accessoriesName}
              />

              <DataItem
                label="Accessories Code"
                value={data.accessoriesCode}
              />

              <DataItem
                label="Accessory SKU"
                value={(data as any).accessorySKU}
              />

              <DataItem
                label="Part Number"
                value={(data as any).partNo}
              />

              <DataItem
                label="Product Source"
                value={data.productSource}
              />

              <DataItem
                label="Vendor"
                value={(data as any).vendorName}
              />

              <DataItem
                label="Product Group"
                value={getLabel(options?.groups, data.productGroupId)}
              />

              <DataItem
                label="Category"
                value={getLabel(options?.categories, data.categoryId)}
              />

              <DataItem
                label="Brand"
                value={getLabel(options?.brands, (data as any).brandId)}
              />
            </section>

            {/* Technical information */}
            <section>
              <SectionHeader icon={FaCogs} title="Technical Details" />

              <DataItem
                label="Version"
                value={getLabel(options?.versions, data.versionId)}
              />

              <DataItem
                label="Unit"
                value={getLabel(options?.units, (data as any).unitId)}
              />

              <DataItem label="UOM" value={data.uom} />

              <DataItem
                label="Warranty Period"
                value={data.warrantyPeriod}
              />

              <DataItem
                label="Calibration Required"
                value={data.isCalibration ? "Yes" : "No"}
              />

              {data.isCalibration && (
                <DataItem
                  label="Calibration Frequency"
                  value={data.calibrationFrequency}
                />
              )}
            </section>

            {/* Codes and dates */}
            <section>
              <SectionHeader icon={FaBarcode} title="Codes & Timeline" />

              <DataItem
                label="Manufacturing Date"
                value={formatDate((data as any).manufacturingDate)}
              />

              <DataItem
                label="Accessories ID"
                value={data.accessoriesId}
              />

              <DataItem
                label="Image Path"
                value={imagePath}
              />

              <DataItem
                label="Record Status"
                value={data.isDeleted ? "Deleted" : "Available"}
              />
            </section>

            {/* Pricing */}
            <section>
              <SectionHeader icon={FaRupeeSign} title="Pricing & Tax" />

              <DataItem
                label="Purchase Price"
                value={formatMoney(data.purchasePrice)}
              />

              <DataItem
                label="Sale Price"
                value={formatMoney(data.salePrice)}
              />

              <DataItem
                label="Currency"
                value={getLabel(options?.currencies, (data as any).currencyId)}
              />

              <DataItem
                label="Tax"
                value={getLabel(options?.taxes, (data as any).taxId)}
              />
            </section>

            {/* Inventory */}
            <section>
              <SectionHeader icon={FaWarehouse} title="Inventory" />

              <DataItem
                label="Stock Tracking"
                value={data.trackStock ? "Enabled" : "Disabled"}
              />

              <DataItem
                label="Available Quantity"
                value={data.availableQuantity}
                alert={isLowStock}
              />

              <DataItem
                label="Minimum Stock"
                value={(data as any).minStock}
              />

              <DataItem
                label="Out-of-Stock Warning"
                value={data.outOfStockWarning}
                alert={isLowStock}
              />

              {isLowStock && (
                <p className="mt-3 rounded-xl border border-red-200 bg-red-50 p-3 text-xs font-semibold text-red-600">
                  Stock has reached or fallen below the warning threshold.
                </p>
              )}
            </section>

            {/* Assurance */}
            <section>
              <SectionHeader icon={FaShieldAlt} title="Record & Assurance" />

              <DataItem
                label="Active Status"
                value={data.isActive ? "Active" : "Inactive"}
              />

              <DataItem
                label="Deleted Status"
                value={data.isDeleted ? "Deleted" : "Not Deleted"}
              />

              <DataItem
                label="Calibration"
                value={data.isCalibration ? "Required" : "Not Required"}
              />

              <DataItem
                label="Warranty"
                value={data.warrantyPeriod}
              />
            </section>

            {/* Description */}
            <section className="md:col-span-2 lg:col-span-2">
              <SectionHeader icon={FaInfoCircle} title="Description" />

              <p className="min-h-24 whitespace-pre-wrap break-words rounded-xl border border-black/[.06] bg-white p-4 text-sm leading-6 text-[#343331]">
                {data.accessoriesDescription || "No description available."}
              </p>
            </section>

            {/* Specification */}
            <section className="md:col-span-2 lg:col-span-2">
              <SectionHeader icon={FaCalendarAlt} title="Specification" />

              <p className="min-h-24 whitespace-pre-wrap break-words rounded-xl border border-black/[.06] bg-white p-4 text-sm leading-6 text-[#343331]">
                {data.specification || "No specification available."}
              </p>
            </section>
          </div>
        </div>

        {/* Footer */}
        <div className="flex items-center justify-end gap-4 border-t border-black/[.07] bg-white p-4 sm:p-6">
          <p className="mr-auto text-[9px] font-black uppercase tracking-[.16em] text-[#8a8984] sm:text-[10px]">
            LGM Sports · Accessories profile
          </p>

          <button
            type="button"
            onClick={onClose}
            className="rounded-xl bg-[#f07837] px-5 py-2.5 text-sm font-bold text-white transition hover:bg-[#dd6328] focus:outline-none focus:ring-4 focus:ring-[#f07837]/20"
          >
            Close Details
          </button>
        </div>
      </div>
    </div>
  );
};

export default AccessoriesViewModal;
