
import React from "react";
import type { TabProps } from "../Services/types";
import { SearchableSelect } from "../../Product/Tabs/SearchableSelect";

type Props = TabProps & {
  errors?: Record<string, string[]>;
};
const formatDateTime = (value?: string | null): string => {
  if (!value) return "";

  // Treat timestamps without a timezone suffix as UTC.
  const normalizedValue =
    /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?$/.test(value)
      ? `${value}Z`
      : value;

  const date = new Date(normalizedValue);

  if (Number.isNaN(date.getTime())) return "";

  return (
    new Intl.DateTimeFormat("en-IN", {
      timeZone: "Asia/Kolkata",
      day: "2-digit",
      month: "short",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
      // second: "2-digit",
      hour12: true,
    }).format(date)
      .replace(/\b(am|pm)\b/i, (match) => match.toUpperCase()) + " IST"
  );
};

const OtherInfoTab: React.FC<Props> = ({
  data,
  setData,
  options,
  onAddMaster,
}) => {
  const inputStyle =
    "w-full rounded-xl border border-black/10 bg-white px-4 py-2.5 text-sm text-[#343331] outline-none transition placeholder:text-gray-400 focus:border-[#f07837] focus:ring-4 focus:ring-[#f07837]/10";

  const labelStyle =
    "mb-1.5 block text-[11px] font-extrabold uppercase tracking-[.15em] text-[#77766f]";

  const updateField = (field: string, value: any) => {
    setData((previous: any) => ({ ...previous, [field]: value }));
  };

  const ToggleField = ({
    field,
    label,
    description,
  }: {
    field: string;
    label: string;
    description: string;
  }) => (
    <label className="flex cursor-pointer items-center justify-between gap-4 rounded-xl border border-black/[.07] bg-white p-4">
      <span>
        <span className="block text-sm font-bold text-[#343331]">{label}</span>
        <span className="mt-1 block text-xs text-[#77766f]">
          {description}
        </span>
      </span>
      <input
        type="checkbox"
        checked={Boolean(data[field])}
        onChange={(event) => updateField(field, event.target.checked)}
        className="h-5 w-5 accent-[#f07837]"
      />
    </label>
  );

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="border-b border-black/[.07] pb-4">
        <p className="mb-1 text-[10px] font-extrabold uppercase tracking-[.2em] text-[#c65b27]">
          LGM Sports · Product catalog
        </p>
        <h3 className="text-xl font-black uppercase tracking-tight text-[#252a2e]">
          Other information
        </h3>
        <p className="mt-1 text-sm text-[#77766f]">
          Configure units, calibration and accessory record status.
        </p>
      </div>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Unit information
        </h4>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2">
          <div>
            <label htmlFor="uom" className={labelStyle}>
              UOM
            </label>
            <input
              id="uom"
              className={inputStyle}
              value={data.uom ?? ""}
              onChange={(event) => updateField("uom", event.target.value)}
              placeholder="e.g. Piece, Box, Set"
            />
          </div>

          <SearchableSelect
            label="Unit"
            options={options.units ?? []}
            value={data.unitId ?? 0}
            onChange={(value: any) => updateField("unitId", value)}
            // onAdd={() => onAddMaster("unit")}
          />
        </div>
      </section>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Calibration
        </h4>

        <ToggleField
          field="isCalibration"
          label="Calibration Required"
          description="Enable if this accessory requires periodic calibration."
        />

        <div className="mt-4">
          <label htmlFor="calibration-frequency" className={labelStyle}>
            Calibration Frequency
          </label>
          <input
            id="calibration-frequency"
            className={inputStyle}
            value={data.calibrationFrequency ?? ""}
            onChange={(event) =>
              updateField("calibrationFrequency", event.target.value)
            }
            disabled={!data.isCalibration}
            placeholder={
              data.isCalibration
                ? "e.g. Every 6 months"
                : "Enable calibration first"
            }
          />
        </div>
      </section>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Record status
        </h4>

        <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
          <ToggleField
            field="isActive"
            label="Active"
            description="Allow this accessory to be used."
          />

          {/* <ToggleField
            field="isDeleted"
            label="Deleted"
            description="Mark this record as deleted."
          /> */}
        </div>
      </section>
            {/* Audit Information */}
      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Audit Information
        </h4>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2">
          {/* Created At */}
          <div>
            <label htmlFor="createdAt" className={labelStyle}>
              Created At
            </label>
            <input
              id="createdAt"
              type="text"
              className={`${inputStyle} cursor-not-allowed bg-gray-50`}
              value={formatDateTime(data.createdAt)}
              readOnly
              placeholder="Created date will appear automatically"
            />
          </div>

          {/* Updated At */}
          <div>
            <label htmlFor="updatedAt" className={labelStyle}>
              Updated At
            </label>
            <input
              id="updatedAt"
              type="text"
              className={`${inputStyle} cursor-not-allowed bg-gray-50`}
              value={formatDateTime(data.updatedAt)}
              readOnly
              placeholder="Updated date will appear automatically"
            />
          </div>
        </div>
      </section>
    </div>
  );
};

export default OtherInfoTab;
