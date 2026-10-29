import React, { useEffect, useRef, useState } from "react";
import type { Reason } from "./Services/types";
import {
  getGeneratedReasonCode,
  saveReason,
} from "./Services/ReasonService";
import { toast } from "sonner";
import { buttonStyles } from "../../../../../Components/Common/buttonStyles";

interface WizardProps {
  editData: Reason | null;
  onSuccess: () => void;
  onCancel: () => void;
}

const emptyReason: Reason = {
  reasonId: 0,
  code: "",
  name: "",
  reasonType: "",
  description: "",
  isActive: true,
  isDeleted: false,
};

const ReasonWizard: React.FC<WizardProps> = ({
  editData,
  onSuccess,
  onCancel,
}) => {
  const [data, setData] = useState<Reason>({ ...emptyReason });
  const [isSaving, setIsSaving] = useState(false);
  const [isGeneratingCode, setIsGeneratingCode] = useState(false);
  const [errors, setErrors] = useState<Record<string, string>>({});

  const codeRequestRef = useRef(0);

  // Initialize form and automatically generate code for new records.
  useEffect(() => {
    const requestId = ++codeRequestRef.current;
    let cancelled = false;

    setData(
      editData
        ? {
            ...emptyReason,
            ...editData,
            description: editData.description ?? "",
          }
        : { ...emptyReason },
    );

    setErrors({});
    setIsGeneratingCode(false);

    // Preserve the original code when editing.
    if (editData) {
      return () => {
        cancelled = true;
      };
    }

    setIsGeneratingCode(true);

    void getGeneratedReasonCode()
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

        console.error("Failed to generate reason code:", error);
        toast.error("Could not generate reason code. Please try again.");
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

  // Update form fields without regenerating the code.
  const update = (
    field: keyof Reason,
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

    if (!data.reasonType.trim()) {
      next.reasonType = "Reason Type is required.";
    }

    if (data.reasonType.length > 50) {
      next.reasonType = "Reason Type must be 50 characters or fewer.";
    }

    if ((data.description?.length ?? 0) > 500) {
      next.description = "Description must be 500 characters or fewer.";
    }

    setErrors(next);
    return Object.keys(next).length === 0;
  };

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
      await saveReason(data);

      toast.success(
        data.reasonId > 0
          ? "Reason updated successfully!"
          : "Reason created successfully!",
      );

      onSuccess();
    } catch (error: unknown) {
      console.error("Failed to save reason:", error);

      toast.error(
        error instanceof Error
          ? error.message
          : "Something went wrong while saving the reason.",
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
      <p className="mt-1 text-xs text-red-600">{errors[key]}</p>
    ) : null;

  return (
    <div className="mb-8 overflow-hidden rounded-xl border border-gray-200 bg-white shadow-xl">
      {/* Header */}
      <header className="bg-[#f07837] px-6 py-4 text-white">
        <h2 className="text-lg font-bold uppercase tracking-wide">
          {data.reasonId ? "Update Reason" : "Add Reason"}
        </h2>

        <p className="mt-1 text-xs text-white/80">
          Enter the reason information below.
        </p>
      </header>

      {/* Form Content */}
      <div className="p-5 sm:p-8">
        <h3 className="mb-5 border-b border-gray-200 pb-3 text-sm font-extrabold uppercase tracking-wide text-[#154068]">
          Reason Information
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
              placeholder="LGM-RSON-0001"
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
              onChange={(event) => update("name", event.target.value)}
              className={inputClass}
              placeholder="Enter reason name"
            />

            {errorText("name")}
          </div>

          {/* Reason Type */}
          <div>
            <label htmlFor="reasonType" className={labelClass}>
              Reason Type *
            </label>

            <input
              id="reasonType"
              maxLength={50}
              value={data.reasonType}
              onChange={(event) => update("reasonType", event.target.value)}
              className={inputClass}
              placeholder="Enter reason type"
            />

            {errorText("reasonType")}
          </div>

          {/* Active Status */}
          <div className="flex items-end pb-2">
            <label className="flex cursor-pointer items-center gap-3">
              <input
                type="checkbox"
                checked={data.isActive}
                onChange={(event) => update("isActive", event.target.checked)}
                className="h-4 w-4 accent-[#f07837]"
              />

              <span className="text-sm font-semibold text-gray-700">
                Active reason
              </span>
            </label>
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
              placeholder="Enter reason description"
            />

            <div className="mt-1 flex justify-between">
              {errorText("description")}

              <span className="ml-auto text-xs text-gray-400">
                {(data.description ?? "").length}/500
              </span>
            </div>
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
            : data.reasonId
              ? "Update"
              : "Save"}
        </button>
      </footer>
    </div>
  );
};

export default ReasonWizard;