
import React from "react";
import type { TabProps } from "../Services/types";
import { SearchableSelect } from "../../Product/Tabs/SearchableSelect";

type Props = TabProps & {
  errors?: Record<string, string[]>;
};

const BriefInfoTab: React.FC<Props> = ({
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

  return (
    <div className="space-y-6 animate-in fade-in duration-500">
      <div className="border-b border-black/[.07] pb-4">
        <p className="mb-1 text-[10px] font-extrabold uppercase tracking-[.2em] text-[#c65b27]">
          LGM Sports · Product catalog
        </p>
        <h3 className="text-xl font-black uppercase tracking-tight text-[#252a2e]">
          Brief information
        </h3>
        <p className="mt-1 text-sm text-[#77766f]">
          Describe the accessory and record its specifications, version and warranty.
        </p>
      </div>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <div className="grid grid-cols-1 gap-5 md:grid-cols-2">
          <div>
            <label htmlFor="accessories-description" className={labelStyle}>
              Accessories Description
            </label>
            <textarea
              id="accessories-description"
              rows={5}
              className={inputStyle}
              value={data.accessoriesDescription ?? ""}
              onChange={(event) =>
                updateField("accessoriesDescription", event.target.value)
              }
              placeholder="Describe the accessory..."
            />
          </div>

          <div>
            <label htmlFor="accessories-specification" className={labelStyle}>
              Specification
            </label>
            <textarea
              id="accessories-specification"
              rows={5}
              className={inputStyle}
              value={data.specification ?? ""}
              onChange={(event) =>
                updateField("specification", event.target.value)
              }
              placeholder="Enter technical specifications..."
            />
          </div>
        </div>
      </section>

      <section className="rounded-2xl border border-black/[.06] bg-white p-4 sm:p-5">
        <h4 className="mb-4 text-xs font-extrabold uppercase tracking-[.16em] text-[#a64a1d]">
          Version and warranty
        </h4>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3">
          <SearchableSelect
            label="Version"
            options={options.versions ?? []}
            value={data.versionId ?? 0}
            onChange={(value: any) => updateField("versionId", value)}
            onAdd={() => onAddMaster("version")}
          />

          <div>
            <label htmlFor="warranty-period" className={labelStyle}>
              Warranty Period
            </label>
            <input
              id="warranty-period"
              className={inputStyle}
              value={data.warrantyPeriod ?? ""}
              onChange={(event) =>
                updateField("warrantyPeriod", event.target.value)
              }
              placeholder="e.g. 1 year, 6 months"
            />
          </div>

          <div>
            <label htmlFor="manufacturing-date" className={labelStyle}>
              Manufacturing Date
            </label>
            <input
              id="manufacturing-date"
              type="date"
              className={inputStyle}
              value={data.manufacturingDate?.split("T")[0] ?? ""}
              onChange={(event) =>
                updateField("manufacturingDate", event.target.value || null)
              }
            />
            <p className="mt-1 text-[10px] text-gray-500">
              Requires backend model/DTO support to persist.
            </p>
          </div>
        </div>
      </section>
    </div>
  );
};

export default BriefInfoTab;
