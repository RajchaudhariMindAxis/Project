
import React from "react";
import type { TabProps } from "../Services/types";
import { SearchableSelect } from "./SearchableSelect";

type Props = TabProps & {
  errors?: Record<string, string[]>;
};

const RateTab: React.FC<Props> = ({
  data,
  setData,
  options,
}) => {
  const inputStyle =
    "w-full rounded-xl border border-black/10 bg-white px-4 py-2.5 text-sm text-[#343331] outline-none transition placeholder:text-gray-400 focus:border-[#f07837] focus:ring-4 focus:ring-[#f07837]/10";

  const labelStyle =
    "mb-1.5 block text-[11px] font-extrabold uppercase tracking-[.15em] text-[#77766f]";

  const updateField = (field: string, value: any) => {
    setData((previous: any) => ({ ...previous, [field]: value }));
  };

  const updateNumber = (field: string, rawValue: string) => {
    updateField(field, rawValue === "" ? "" : Number(rawValue));
  };

  const NumberField = ({
    field,
    label,
    min = 0,
    step = "1",
    placeholder = "0",
  }: {
    field: string;
    label: string;
    min?: number;
    step?: string;
    placeholder?: string;
  }) => (
    <div>
      <label htmlFor={field} className={labelStyle}>
        {label}
      </label>
      <input
        id={field}
        type="number"
        min={min}
        step={step}
        className={inputStyle}
        value={data[field] ?? ""}
        placeholder={placeholder}
        onChange={(event) => updateNumber(field, event.target.value)}
        onBlur={() => {
          if (data[field] === "" || data[field] == null) {
            updateField(field, 0);
          }
        }}
      />
    </div>
  );

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="border-b border-black/[.07] pb-4">
        <p className="mb-1 text-[10px] font-extrabold uppercase tracking-[.2em] text-[#c65b27]">
          LGM Sports · Product catalog
        </p>
        <h3 className="text-xl font-black uppercase tracking-tight text-[#252a2e]">
          Pricing and inventory
        </h3>
        <p className="mt-1 text-sm text-[#77766f]">
          Set purchase and sale prices, tax, currency and stock thresholds.
        </p>
      </div>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Pricing
        </h4>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3">
          <NumberField
            field="purchasePrice"
            label="Purchase Price"
            step="0.01"
            placeholder="0.00"
          />

          <NumberField
            field="salePrice"
            label="Sale Price"
            step="0.01"
            placeholder="0.00"
          />

          <SearchableSelect
            label="Currency"
            options={options.currencies ?? []}
            value={data.currencyId ?? 0}
            onChange={(value: any) => updateField("currencyId", value)}
            onAdd={() => {}}
          />

          <SearchableSelect
            label="Tax"
            options={options.taxes ?? []}
            value={data.taxId ?? 0}
            onChange={(value: any) => updateField("taxId", value)}
            onAdd={() => {}}
          />
        </div>
      </section>

      <section className="rounded-2xl border border-black/[.06] bg-white p-4 sm:p-5">
        <div className="mb-4">
          <h4 className="text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
            Inventory management
          </h4>
          <p className="mt-1 text-xs text-[#77766f]">
            Configure stock tracking and quantity warning levels.
          </p>
        </div>

        <label className="mb-5 flex cursor-pointer items-center justify-between gap-4 rounded-xl border border-black/[.07] bg-[#fbfaf8] p-4">
          <span>
            <span className="block text-sm font-bold text-[#343331]">
              Track Stock
            </span>
            <span className="mt-1 block text-xs text-[#77766f]">
              Enable inventory quantity tracking for this accessory.
            </span>
          </span>
          <input
            type="checkbox"
            checked={Boolean(data.trackStock)}
            onChange={(event) =>
              updateField("trackStock", event.target.checked)
            }
            className="h-5 w-5 accent-[#f07837]"
          />
        </label>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3">
          <NumberField
            field="availableQuantity"
            label="Available Quantity"
          />

          <NumberField
            field="minStock"
            label="Minimum Stock"
          />

          <NumberField
            field="outOfStockWarning"
            label="Out-of-Stock Warning"
          />
        </div>

        {!data.trackStock && (
          <p className="mt-4 rounded-lg bg-[#fff7f1] p-3 text-xs text-[#a64a1d]">
            Stock tracking is disabled. Quantity fields are retained in the form
            but will not automatically manage inventory.
          </p>
        )}
      </section>
    </div>
  );
};

export default RateTab;
