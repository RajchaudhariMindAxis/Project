
import React, { useEffect, useRef, useState } from "react";

import {
  getGeneratedPriceListCode,
  getCurrencies,
  savePriceList,
} from "./Services/PriceListService";

import type { PriceList, CurrencyOption } from "./Services/types";
import { toast } from "sonner";
import { buttonStyles } from "../../../../../Components/Common/buttonStyles";



interface WizardProps {
  editData: PriceList | null;
  onSuccess: () => void;
  onCancel: () => void;
}

const emptyPriceList: PriceList = {
  priceListId: 0,
  code: "",
  name: "",
  customerChannelType: "",
  id: 0,
  effectiveFrom: new Date().toISOString().slice(0, 10),
  effectiveTo: null,
  isActive: true,
  isDeleted: false,
};

const dateValue = (value?: string | null) =>
  value ? value.slice(0, 10) : "";

const PriceListWizard: React.FC<WizardProps> = ({
  editData,
  onSuccess,
  onCancel,
}) => {
  const [data, setData] = useState<PriceList>({
    ...emptyPriceList,
  });
  const [isSaving, setIsSaving] = useState(false);
  const [isGeneratingCode, setIsGeneratingCode] = useState(false);
  const codeRequestRef = useRef(0);
  const [errors, setErrors] = useState<Record<string, string>>({});

  // Initialize form data
  useEffect(() => {
    const requestId = ++codeRequestRef.current;
    let cancelled = false;

    setIsGeneratingCode(false);

    setData(
      editData
        ? {
          ...emptyPriceList,
          ...editData,
          effectiveFrom: dateValue(editData.effectiveFrom),
          effectiveTo: dateValue(editData.effectiveTo) || null,
        }
        : { ...emptyPriceList }
    );

    setErrors({});

    if (editData) {
      return () => {
        cancelled = true;
      };
    }

    setIsGeneratingCode(true);

    void getGeneratedPriceListCode()
      .then((generatedCode) => {
        if (cancelled || requestId !== codeRequestRef.current) return;

        setData((previous) => ({
          ...previous,
          code: generatedCode,
        }));
      })
      .catch((error: unknown) => {
        if (cancelled || requestId !== codeRequestRef.current) return;

        console.error("Failed to generate Price List code:", error);
        toast.error("Could not generate Price List code. Please try again.");
      })
      .finally(() => {
        if (!cancelled && requestId === codeRequestRef.current) {
          setIsGeneratingCode(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, [editData]);

  // Update form fields and generate code
  const update = (
    field: keyof PriceList,
    value: string | number | boolean | null
  ) => {
    setData((previous) => ({
      ...previous,
      [field]: value,
    }));

    setErrors((previous) => ({
      ...previous,
      [field]: "",
    }));
  };

  // Validate form fields
  const validate = () => {
    const next: Record<string, string> = {};

    if (!data.code.trim()) {
      next.code = "Code is required.";
    }

    if (data.code.length > 50) {
      next.code = "Code must be 50 characters or fewer.";
    }

    if (!data.name.trim()) {
      next.name = "Name is required.";
    }

    if (data.name.length > 150) {
      next.name = "Name must be 150 characters or fewer.";
    }

    if (!data.customerChannelType.trim()) {
      next.customerChannelType =
        "Customer Channel Type is required.";
    }

    if (data.customerChannelType.length > 50) {
      next.customerChannelType =
        "Customer Channel Type must be 50 characters or fewer.";
    }

    if (
      !Number.isInteger(Number(data.id)) ||
      Number(data.id) <= 0
    ) {
      next.id = "Enter a valid Currency ID.";
    }

    if (!data.effectiveFrom) {
      next.effectiveFrom = "Effective From is required.";
    }

    if (
      data.effectiveTo &&
      data.effectiveFrom &&
      data.effectiveTo < data.effectiveFrom
    ) {
      next.effectiveTo =
        "Effective To must be on or after Effective From.";
    }

    setErrors(next);

    return Object.keys(next).length === 0;
  };

  // Save price list
  const handleSave = async () => {
    if (isSaving || isGeneratingCode) return;

    if (!validate()) {
      toast.error("Please correct the highlighted fields.");
      return;
    }

    setIsSaving(true);

    try {
      await savePriceList({
        ...data,
        id: Number(data.id),
        effectiveFrom: `${dateValue(data.effectiveFrom)}T00:00:00`,
        effectiveTo: data.effectiveTo
          ? `${dateValue(data.effectiveTo)}T00:00:00`
          : null,
      });

      toast.success(
        data.priceListId > 0
          ? "Price list updated successfully!"
          : "Price list created successfully!"
      );

      onSuccess();
    } catch (error: unknown) {
      toast.error(
        error instanceof Error
          ? error.message
          : "Something went wrong while saving the price list."
      );
    } finally {
      setIsSaving(false);
    }
  };

  const inputClass =
    "w-full rounded-lg border border-gray-300 px-3 py-2.5 text-sm outline-none transition focus:border-[#f07837] focus:ring-2 focus:ring-[#f07837]/20";

  const labelClass =
    "mb-1 block text-xs font-bold uppercase tracking-wide text-gray-600";

  const errorText = (key: string) =>
    errors[key] ? (
      <p className="mt-1 text-xs text-red-600">
        {errors[key]}
      </p>
    ) : null;
  const [currencies, setCurrencies] = useState<CurrencyOption[]>([]);
  const [isLoadingCurrencies, setIsLoadingCurrencies] = useState(false);

  useEffect(() => {
    let cancelled = false;

    const loadCurrencies = async () => {
      setIsLoadingCurrencies(true);

      try {
        const result = await getCurrencies();

        if (!cancelled) {
          setCurrencies(result);
        }
      } catch (error) {
        console.error("Failed to load currencies:", error);
        if (!cancelled) {
          toast.error("Could not load currencies.");
        }
      } finally {
        if (!cancelled) {
          setIsLoadingCurrencies(false);
        }
      }
    };

    void loadCurrencies();

    return () => {
      cancelled = true;
    };
  }, []);
  const [currencySearch, setCurrencySearch] = useState("");
  const [showCurrencyDropdown, setShowCurrencyDropdown] = useState(false);
  const currencyDropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleOutsideClick = (event: MouseEvent) => {
      if (
        currencyDropdownRef.current &&
        !currencyDropdownRef.current.contains(event.target as Node)
      ) {
        setShowCurrencyDropdown(false);
        setCurrencySearch("");
      }
    };

    document.addEventListener("mousedown", handleOutsideClick);

    return () => {
      document.removeEventListener("mousedown", handleOutsideClick);
    };
  }, []);
  const selectedCurrency = currencies.find(
    (currency) => currency.id === Number(data.id)
  );

  const filteredCurrencies = currencies.filter((currency) => {
    const search = currencySearch.trim().toLowerCase();

    return (
      currency.currencyName.toLowerCase().includes(search) ||
      currency.currencySymbol.toLowerCase().includes(search) ||
      String(currency.id).includes(search)
    );
  });

  return (
    <div className="mb-8 overflow-hidden rounded-xl border border-gray-200 bg-white shadow-xl">
      {/* Header */}
      <header className="bg-[#f07837] px-6 py-4 text-white">
        <h2 className="text-lg font-bold uppercase tracking-wide">
          {data.priceListId
            ? "Update Price List"
            : "Add Price List"}
        </h2>

        <p className="mt-1 text-xs text-white/80">
          Configure pricing and the effective period.
        </p>
      </header>

      {/* Form Content */}
      <div className="p-5 sm:p-8">
        <h3 className="mb-5 border-b border-gray-200 pb-3 text-sm font-extrabold uppercase tracking-wide text-[#154068]">
          Price List Information
        </h3>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2">
          {/* Code */}
          <div>
            <label htmlFor="code" className={labelClass}>
              Code *
            </label>

            <input
              id="code"
              maxLength={50}
              value={isGeneratingCode ? "Generating..." : data.code}
              readOnly
              className={`${inputClass} bg-gray-100`}
              placeholder="LGM-PRL-0001"
            />

            {errorText("code")}
          </div>

          {/* Name */}
          <div>
            <label htmlFor="name" className={labelClass}>
              Name *
            </label>

            <input
              id="name"
              maxLength={150}
              value={data.name}
              onChange={(e) => update("name", e.target.value)}
              className={inputClass}
              placeholder="Enter price list name"
            />

            {errorText("name")}
          </div>

          {/* Customer Channel Type */}
          <div>
            <label
              htmlFor="customerChannelType"
              className={labelClass}
            >
              Customer Channel Type *
            </label>

            <input
              id="customerChannelType"
              maxLength={50}
              value={data.customerChannelType}
              onChange={(e) =>
                update("customerChannelType", e.target.value)
              }
              className={inputClass}
              placeholder="e.g. Retail, Wholesale"
            />

            {errorText("customerChannelType")}
          </div>


          {/* Searchable Currency Dropdown */}
          <div className="relative">
            <label htmlFor="currencySearch" className={labelClass}>
              Currency *
            </label>

            <input
              id="currencySearch"
              type="text"
              value={
                showCurrencyDropdown
                  ? currencySearch
                  : selectedCurrency
                    ? `${selectedCurrency.currencyName}${selectedCurrency.currencySymbol
                      ? ` (${selectedCurrency.currencySymbol})`
                      : ""
                    }`
                    : ""
              }
              onFocus={() => {
                setCurrencySearch("");
                setShowCurrencyDropdown(true);
              }}
              onChange={(e) => {
                setCurrencySearch(e.target.value);
                setShowCurrencyDropdown(true);
              }}
              onKeyDown={(e) => {
                if (e.key === "Escape") {
                  setShowCurrencyDropdown(false);
                  setCurrencySearch("");
                }
              }}
              placeholder={
                isLoadingCurrencies
                  ? "Loading currencies..."
                  : "Search currency by name, symbol or ID..."
              }
              disabled={isLoadingCurrencies}
              autoComplete="off"
              className={inputClass}
              aria-expanded={showCurrencyDropdown}
              aria-controls="currency-options"
              role="combobox"
              aria-autocomplete="list"
            />

            {showCurrencyDropdown && !isLoadingCurrencies && (
              <div
                id="currency-options"
                role="listbox"
                className="absolute z-50 mt-1 max-h-60 w-full overflow-y-auto rounded-lg border border-gray-200 bg-white shadow-lg"
              >
                {filteredCurrencies.length > 0 ? (
                  filteredCurrencies.map((currency) => (
                    <button
                      key={currency.id}
                      type="button"
                      role="option"
                      aria-selected={Number(data.id) === currency.id}
                      onClick={() => {
                        update("id", currency.id);
                        setCurrencySearch("");
                        setShowCurrencyDropdown(false);
                      }}
                      className={`w-full border-b border-gray-100 px-3 py-2.5 text-left text-sm last:border-b-0 hover:bg-orange-50 ${Number(data.id) === currency.id
                        ? "bg-orange-50 font-semibold text-[#f07837]"
                        : "text-gray-700"
                        }`}
                    >
                      <span className="block">
                        {currency.currencyName}
                        {currency.currencySymbol
                          ? ` (${currency.currencySymbol})`
                          : ""}
                      </span>
                      <span className="text-xs text-gray-400">
                        Currency ID: {currency.id}
                      </span>
                    </button>
                  ))
                ) : (
                  <p className="px-3 py-4 text-sm text-gray-500">
                    No currencies found.
                  </p>
                )}
              </div>
            )}

            {errorText("id")}
          </div>


          {/* Effective From */}
          <div>
            <label htmlFor="effectiveFrom" className={labelClass}>
              Effective From *
            </label>

            <input
              id="effectiveFrom"
              type="date"
              value={dateValue(data.effectiveFrom)}
              onChange={(e) =>
                update("effectiveFrom", e.target.value)
              }
              className={inputClass}
            />

            {errorText("effectiveFrom")}
          </div>

          {/* Effective To */}
          <div>
            <label htmlFor="effectiveTo" className={labelClass}>
              Effective To
            </label>

            <input
              id="effectiveTo"
              type="date"
              value={dateValue(data.effectiveTo)}
              onChange={(e) =>
                update("effectiveTo", e.target.value || null)
              }
              className={inputClass}
            />

            {errorText("effectiveTo")}
          </div>

          {/* Active Status */}
          <div className="md:col-span-2">
            <label className="flex cursor-pointer items-center gap-3">
              <input
                type="checkbox"
                checked={data.isActive}
                onChange={(e) =>
                  update("isActive", e.target.checked)
                }
                className="h-4 w-4 accent-[#f07837]"
              />

              <span className="text-sm font-semibold text-gray-700">
                Active price list
              </span>
            </label>
          </div>
        </div>
      </div>

      {/* Footer */}
      <footer className="flex flex-wrap justify-end gap-3 border-t bg-gray-50 p-5">
        <button
          type="button"
          onClick={onCancel}
          disabled={isSaving}
          className={buttonStyles.cancel}
        >
          Cancel
        </button>

        <button
          type="button"
          onClick={() => void handleSave()}
          disabled={isSaving || isGeneratingCode}
          className={buttonStyles.save}
        >
          {isSaving
            ? "Saving..."
            : data.priceListId
              ? "Update"
              : "Save"}
        </button>
      </footer>
    </div>
  );
};

export default PriceListWizard;
