
import React, { useEffect, useState } from "react";
import Select from "react-select";
import { SearchableSelect } from "../../Product/Tabs/SearchableSelect";
import { API_ENDPOINTS } from "../../../../../API/apiConfig";
import { getVendors } from "../../Product/Services/ProductService";
import { getGeneratedAccessoryCode } from "../Services/AccessoriesService";
import type { TabProps } from "../Services/types";

type Props = TabProps & {
  errors?: Record<string, string[]>;
};

const FILE_BASE_URL = API_ENDPOINTS.BaseUrl.replace(/\/api\/?$/, "");

const AccessoriesInfoTab: React.FC<Props> = ({
  data,
  setData,
  options,
  onAddMaster,
  errors = {},
}) => {
  const [vendors, setVendors] = useState<any[]>([]);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);

  const inputStyle = (hasError = false) =>
    `w-full rounded-xl border ${hasError ? "border-red-500 bg-red-50" : "border-black/10 bg-white"
    } px-4 py-2.5 text-sm outline-none transition focus:border-[#f07837] focus:ring-4 focus:ring-[#f07837]/10`;

  const labelStyle =
    "mb-1.5 block text-[11px] font-extrabold uppercase tracking-[.15em] text-[#77766f]";

  useEffect(() => {
    let mounted = true;

    const loadVendors = async () => {
      try {
        const response: any = await getVendors();
        const list =
          Array.isArray(response) ? response :
            Array.isArray(response?.data) ? response.data :
              Array.isArray(response?.$values) ? response.$values :
                Array.isArray(response?.data?.$values) ? response.data.$values :
                  [];

        if (mounted) setVendors(list);
      } catch (error) {
        console.error("Error loading vendors:", error);
      }
    };

    loadVendors();

    return () => {
      mounted = false;
    };
  }, []);

  useEffect(() => {
    let mounted = true;

    const generateCode = async () => {
      const vendorId = Number(data.vendorId ?? 0);
      const accessoryId = Number(data.accessoriesId ?? 0);

      if (accessoryId !== 0 || vendorId <= 0) return;

      try {
        const source: "Local" | "Imported" =
          data.productSource === "Imported" ? "Imported" : "Local";

        const code = await getGeneratedAccessoryCode(vendorId, source);

        if (mounted && code) {
          setData((previous: any) => ({
            ...previous,
            accessoriesCode: code,
          }));
        }
      } catch (error) {
        console.error("Error generating accessory code:", error);
      }
    };

    generateCode();

    return () => {
      mounted = false;
    };
  }, [
    data.accessoriesId,
    data.vendorId,
    data.productSource,
    setData,
  ]);

  useEffect(() => {
    if (data.accessoriesImageFile instanceof File) {
      const url = URL.createObjectURL(data.accessoriesImageFile);
      setPreviewUrl(url);

      return () => URL.revokeObjectURL(url);
    }

    const imagePath = data.accessoriesImage || data.accessoriesImagePath;

    if (imagePath) {
      setPreviewUrl(
        imagePath.startsWith("http")
          ? imagePath
          : `${FILE_BASE_URL}${imagePath.startsWith("/") ? "" : "/"}${imagePath}`
      );
    } else {
      setPreviewUrl(null);
    }
  }, [
    data.accessoriesImageFile,
    data.accessoriesImage,
    data.accessoriesImagePath,
  ]);

  const vendorOptions = vendors.map((vendor: any) => ({
    value: Number(
      vendor.id ??
      vendor.Id ??
      vendor.vendorId ??
      vendor.VendorId ??
      0
    ),
    label: `${vendor.vendorName ??
      vendor.VendorName ??
      vendor.name ??
      "Unnamed Vendor"
      }${vendor.vendorCode ?? vendor.VendorCode
        ? ` (${vendor.vendorCode ?? vendor.VendorCode})`
        : ""
      }`,
    vendorName:
      vendor.vendorName ?? vendor.VendorName ?? vendor.name ?? "",
  }));

  const currentVendorId = Number(
    data.vendorId ?? data.VendorId ?? 0
  );

  const currentVendor = vendorOptions.find(
    (vendor) => vendor.value === currentVendorId
  ) ?? (
      data.vendorName || data.VendorName
        ? {
          value: currentVendorId,
          label: data.vendorName ?? data.VendorName,
          vendorName: data.vendorName ?? data.VendorName,
        }
        : null
    );


  const updateField = (field: string, value: any) => {
    setData((previous: any) => ({ ...previous, [field]: value }));
  };

  return (
    <div className="space-y-8 animate-in fade-in duration-500">
      <div className="border-b border-black/[.07] pb-4">
        <p className="mb-1 text-[10px] font-extrabold uppercase tracking-[.2em] text-[#c65b27]">
          LGM Sports · Product catalog
        </p>
        <h3 className="text-xl font-black uppercase tracking-tight text-[#252a2e]">
          Accessories information
        </h3>
        <p className="mt-1 text-sm text-[#77766f]">
          Enter the accessory identity, source, vendor and classification.
        </p>
      </div>

      <section className="rounded-2xl border border-black/[.06] bg-[#fbfaf8] p-4 sm:p-5">
        <div className="grid grid-cols-1 gap-5 md:grid-cols-2 lg:grid-cols-3">
          <div>
            <label className={labelStyle}>Accessories Code</label>
            <input
              className={inputStyle()}
              value={data.accessoriesCode ?? ""}
              readOnly
              placeholder="Select a vendor to generate code"
            />
          </div>

          <div>
            <label className={labelStyle}>
              Vendor <span className="text-red-500">*</span>
            </label>
            <Select
              options={vendorOptions}
              placeholder="Search vendor..."
              value={currentVendor}
              isClearable
              onChange={(selected: any) => {
                setData((previous: any) => ({
                  ...previous,
                  vendorId: selected?.value ?? null,
                  vendorName: selected?.vendorName ?? "",
                }));
              }}
              styles={{
                control: (base, state) => ({
                  ...base,
                  minHeight: "42px",
                  borderRadius: "0.75rem",
                  borderColor: errors.vendorId
                    ? "#ef4444"
                    : state.isFocused
                      ? "#f07837"
                      : "#dedbd5",
                  boxShadow: "none",
                  "&:hover": { borderColor: "#f07837" },
                }),
                menu: (base) => ({ ...base, zIndex: 9999 }),
              }}
            />
            {errors.vendorId && (
              <p className="mt-1 text-xs text-red-500">
                {errors.vendorId[0]}
              </p>
            )}
          </div>

          {/* <div>
            <label className={labelStyle}>Product Source</label>
            <Select
              options={[
                { value: "Local", label: "Local" },
                { value: "Imported", label: "Imported" },
              ]}
              value={{
                value: data.productSource || "Local",
                label: data.productSource || "Local",
              }}
              onChange={(selected: any) =>
                updateField("productSource", selected?.value ?? "Local")
              }
              styles={{
                control: (base) => ({
                  ...base,
                  minHeight: "42px",
                  borderRadius: "0.75rem",
                  borderColor: "#dedbd5",
                  boxShadow: "none",
                  "&:hover": { borderColor: "#f07837" },
                }),
                menu: (base) => ({ ...base, zIndex: 9999 }),
              }}
            />
          </div> */}
          <div>
            <label className={labelStyle}>
              Accessories Name <span className="text-red-500">*</span>
            </label>
            <input
              className={inputStyle(!!errors.accessoriesName)}
              value={data.accessoriesName ?? ""}
              onChange={(event) =>
                updateField("accessoriesName", event.target.value)
              }
              placeholder="Enter accessories name"
            />
            {errors.accessoriesName && (
              <p className="mt-1 text-xs text-red-500">
                {errors.accessoriesName[0]}
              </p>
            )}
          </div>

          <div>
            <label className={labelStyle}>Accessories Image</label>
            <div className="flex items-center gap-3 rounded-xl border border-dashed border-[#f07837]/40 bg-white p-3">
              <div className="flex h-16 w-16 shrink-0 items-center justify-center overflow-hidden rounded-lg border border-black/10 bg-[#fbfaf8]">
                {previewUrl ? (
                  <img
                    src={previewUrl}
                    alt="Accessory preview"
                    className="h-full w-full object-contain"
                  />
                ) : (
                  <span className="text-[9px] font-bold text-gray-400">
                    NO IMAGE
                  </span>
                )}
              </div>

              <div className="min-w-0">
                <input
                  id="accessories-image"
                  type="file"
                  accept="image/*"
                  className="hidden"
                  onChange={(event) => {
                    const file = event.target.files?.[0];
                    if (file) updateField("accessoriesImageFile", file);
                  }}
                />
                <label
                  htmlFor="accessories-image"
                  className="inline-flex cursor-pointer rounded-lg bg-[#f07837] px-3 py-2 text-xs font-bold text-white hover:bg-[#dd6328]"
                >
                  Browse image
                </label>
                <p className="mt-1 text-[10px] text-gray-500">
                  Choose an image file
                </p>
              </div>
            </div>
          </div>

          <SearchableSelect
            label="Product Group"
            options={options.groups ?? []}
            value={data.productGroupId ?? 0}
            onChange={(value: any) => updateField("productGroupId", value)}
            onAdd={() => onAddMaster("group")}
          />

          <SearchableSelect
            label="Category"
            options={options.categories ?? []}
            value={data.categoryId ?? 0}
            onChange={(value: any) => updateField("categoryId", value)}
          // onAdd={() => onAddMaster("category")}
          />



          <div>
            <label className={labelStyle}>Accessory SKU</label>
            <input
              className={inputStyle()}
              value={data.accessorySKU ?? ""}
              onChange={(event) =>
                updateField("accessorySKU", event.target.value)
              }
              placeholder="Enter SKU"
            />
          </div>

          <div>
            <label className={labelStyle}>Part Number</label>
            <input
              className={inputStyle()}
              value={data.partNo ?? ""}
              onChange={(event) => updateField("partNo", event.target.value)}
              placeholder="Enter part number"
            />
          </div>

          <SearchableSelect
            label="Brand"
            options={options.brands ?? []}
            value={data.brandId ?? 0}
            onChange={(value: any) => updateField("brandId", value)}
          // onAdd={() => onAddMaster("brand")}
          />
        </div>
      </section>
    </div>
  );
};

export default AccessoriesInfoTab;
