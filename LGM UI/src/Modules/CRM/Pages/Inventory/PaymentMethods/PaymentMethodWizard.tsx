import React, { useEffect, useRef, useState } from "react";
import type { PaymentMethod } from "./Services/types";
import {
  getGeneratedPaymentMethodCode,
  savePaymentMethod,
} from "./Services/PaymentMethodService";
import { toast } from "sonner";
import { buttonStyles } from "../../../../../Components/Common/buttonStyles";

interface WizardProps {
  editData: PaymentMethod | null;
  onSuccess: () => void;
  onCancel: () => void;
}

const emptyMethod: PaymentMethod = {
  paymentMethodId: 0,
  code: "",
  name: "",
  description: "",
  isActive: true,
  isDeleted: false,
};

const PaymentMethodWizard: React.FC<WizardProps> = ({
  editData,
  onSuccess,
  onCancel,
}) => {
  const [data, setData] = useState<PaymentMethod>({
    ...emptyMethod,
  });

  const [isSaving, setIsSaving] = useState(false);
  const [isGeneratingCode, setIsGeneratingCode] = useState(false);
  const [errors, setErrors] = useState<Record<string, string>>({});

  const codeRequestRef = useRef(0);

  // Initialize form and generate a code for new records.
  useEffect(() => {
    const requestId = ++codeRequestRef.current;
    let cancelled = false;

    setData(
      editData
        ? {
            ...emptyMethod,
            ...editData,
            description: editData.description ?? "",
          }
        : { ...emptyMethod },
    );

    setErrors({});
    setIsGeneratingCode(false);

    // Preserve existing code when editing.
    if (editData) {
      return () => {
        cancelled = true;
      };
    }

    // Automatically generate a code for a new payment method.
    setIsGeneratingCode(true);

    void getGeneratedPaymentMethodCode()
      .then((generatedCode) => {
        if (cancelled || requestId !== codeRequestRef.current) {
          return;
        }

        setData((previous) => ({
          ...previous,
          code: generatedCode,
        }));
      })
      .catch((error: unknown) => {
        if (cancelled || requestId !== codeRequestRef.current) {
          return;
        }

        console.error(
          "Failed to generate payment method code:",
          error,
        );

        toast.error(
          "Could not generate payment method code. Please try again.",
        );
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

  // Update form fields.
  const update = (
    field: keyof PaymentMethod,
    value: string | boolean,
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

  // Validate form fields.
  const validate = () => {
    const next: Record<string, string> = {};

    if (!data.code.trim()) {
      next.code = "Code is required.";
    }

    if (data.code.length > 30) {
      next.code = "Code must be 30 characters or fewer.";
    }

    if (!data.name.trim()) {
      next.name = "Name is required.";
    }

    if (data.name.length > 100) {
      next.name = "Name must be 100 characters or fewer.";
    }

    if ((data.description?.length ?? 0) > 500) {
      next.description = "Description must be 500 characters or fewer.";
    }

    setErrors(next);

    return Object.keys(next).length === 0;
  };

  // Save payment method.
  const handleSave = async () => {
    if (isSaving || isGeneratingCode) {
      return;
    }

    if (!validate()) {
      toast.error("Please correct the highlighted fields.");
      return;
    }

    setIsSaving(true);

    try {
      await savePaymentMethod(data);

      toast.success(
        data.paymentMethodId > 0
          ? "Payment method updated successfully!"
          : "Payment method created successfully!",
      );

      onSuccess();
    } catch (error: unknown) {
      console.error("Failed to save payment method:", error);

      toast.error(
        error instanceof Error
          ? error.message
          : "Something went wrong while saving the payment method.",
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

  return (
    <div className="mb-8 overflow-hidden rounded-xl border border-gray-200 bg-white shadow-xl">
      {/* Header */}
      <header className="bg-[#f07837] px-6 py-4 text-white">
        <h2 className="text-lg font-bold uppercase tracking-wide">
          {data.paymentMethodId
            ? "Update Payment Method"
            : "Add Payment Method"}
        </h2>

        <p className="mt-1 text-xs text-white/80">
          Enter payment method details below.
        </p>
      </header>

      {/* Form Content */}
      <div className="p-5 sm:p-8">
        <h3 className="mb-5 border-b border-gray-200 pb-3 text-sm font-extrabold uppercase tracking-wide text-[#154068]">
          Payment Method Information
        </h3>

        <div className="grid grid-cols-1 gap-5 md:grid-cols-2">
          {/* Code */}
          <div>
            <label htmlFor="code" className={labelClass}>
              Code *
            </label>

            <input
              id="code"
              maxLength={30}
              value={
                isGeneratingCode ? "Generating..." : data.code
              }
              readOnly
              className={`${inputClass} bg-gray-100`}
              placeholder="LGM-PYMD-0001"
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
              maxLength={100}
              value={data.name}
              onChange={(event) =>
                update("name", event.target.value)
              }
              className={inputClass}
              placeholder="Enter payment method name"
            />

            {errorText("name")}
          </div>

          {/* Description */}
          <div className="md:col-span-2">
            <label htmlFor="description" className={labelClass}>
              Description
            </label>

            <textarea
              id="description"
              rows={4}
              maxLength={500}
              value={data.description ?? ""}
              onChange={(event) =>
                update("description", event.target.value)
              }
              className={inputClass}
              placeholder="Enter payment method description"
            />

            <div className="mt-1 flex justify-between">
              {errorText("description")}
              <span className="ml-auto text-xs text-gray-400">
                {(data.description ?? "").length}/500
              </span>
            </div>
          </div>

          {/* Active Status */}
          <div className="md:col-span-2">
            <label className="flex cursor-pointer items-center gap-3">
              <input
                type="checkbox"
                checked={data.isActive}
                onChange={(event) =>
                  update("isActive", event.target.checked)
                }
                className="h-4 w-4 accent-[#f07837]"
              />

              <span className="text-sm font-semibold text-gray-700">
                Active payment method
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
            : data.paymentMethodId
              ? "Update"
              : "Save"}
        </button>
      </footer>
    </div>
  );
};

export default PaymentMethodWizard;