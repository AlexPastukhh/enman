import { useState, type FormEvent } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import type { StartAgreementExchangeResponse } from "../api/startAgreementExchangeApiTypes";
import { useStartAgreementExchangeMutation } from "../model/useStartAgreementExchangeMutation";
import { startAgreementExchangeFormConst } from "./startAgreementExchangeFormConst";
import "./startAgreementExchangeForm.css";

type StartAgreementExchangeFormProps = {
  requestId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  onStarted?: (response: StartAgreementExchangeResponse) => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return startAgreementExchangeFormConst.defaultErrorMessage;
};

const isPositiveInteger = (value: string): boolean => {
  const parsed = Number(value);
  return Number.isInteger(parsed) && parsed > 0;
};

export const StartAgreementExchangeForm = ({
  requestId,
  disabled = false,
  unavailableReason = null,
  onStarted,
}: StartAgreementExchangeFormProps) => {
  const [storageKey, setStorageKey] = useState("");
  const [originalFileName, setOriginalFileName] = useState("");
  const [contentType, setContentType] = useState("application/pdf");
  const [sizeBytes, setSizeBytes] = useState("");
  const [comment, setComment] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);
  const mutation = useStartAgreementExchangeMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isDisabled) {
      return;
    }

    if (
      storageKey.trim().length === 0 ||
      originalFileName.trim().length === 0 ||
      contentType.trim().length === 0 ||
      !isPositiveInteger(sizeBytes)
    ) {
      setValidationError(startAgreementExchangeFormConst.validationErrorMessage);
      return;
    }

    setValidationError(null);

    mutation.mutate(
      {
        requestId,
        initialProposal: {
          document: {
            storageKey: storageKey.trim(),
            originalFileName: originalFileName.trim(),
            contentType: contentType.trim(),
            sizeBytes: Number(sizeBytes),
          },
          comment: comment.trim() || null,
        },
      },
      {
        onSuccess: (response) => {
          onStarted?.(response);
        },
      },
    );
  };

  return (
    <form
      className="startAgreementExchangeAction"
      aria-live="polite"
      onSubmit={handleSubmit}
    >
      <h4 className="startAgreementExchangeAction__title">
        {startAgreementExchangeFormConst.title}
      </h4>
      <div
        className="startAgreementExchangeAction__grid"
        role="group"
        aria-label={startAgreementExchangeFormConst.documentSectionTitle}
      >
        <div className="startAgreementExchangeAction__field">
          <label
            className="startAgreementExchangeAction__label"
            htmlFor={`start-exchange-storage-key-${requestId}`}
          >
            {startAgreementExchangeFormConst.storageKeyLabel}
          </label>
          <input
            id={`start-exchange-storage-key-${requestId}`}
            className="startAgreementExchangeAction__input"
            disabled={isDisabled}
            value={storageKey}
            onChange={(event) => setStorageKey(event.target.value)}
          />
        </div>
        <div className="startAgreementExchangeAction__field">
          <label
            className="startAgreementExchangeAction__label"
            htmlFor={`start-exchange-file-name-${requestId}`}
          >
            {startAgreementExchangeFormConst.originalFileNameLabel}
          </label>
          <input
            id={`start-exchange-file-name-${requestId}`}
            className="startAgreementExchangeAction__input"
            disabled={isDisabled}
            value={originalFileName}
            onChange={(event) => setOriginalFileName(event.target.value)}
          />
        </div>
        <div className="startAgreementExchangeAction__field">
          <label
            className="startAgreementExchangeAction__label"
            htmlFor={`start-exchange-content-type-${requestId}`}
          >
            {startAgreementExchangeFormConst.contentTypeLabel}
          </label>
          <input
            id={`start-exchange-content-type-${requestId}`}
            className="startAgreementExchangeAction__input"
            disabled={isDisabled}
            value={contentType}
            onChange={(event) => setContentType(event.target.value)}
          />
        </div>
        <div className="startAgreementExchangeAction__field">
          <label
            className="startAgreementExchangeAction__label"
            htmlFor={`start-exchange-size-${requestId}`}
          >
            {startAgreementExchangeFormConst.sizeBytesLabel}
          </label>
          <input
            id={`start-exchange-size-${requestId}`}
            className="startAgreementExchangeAction__input"
            disabled={isDisabled}
            inputMode="numeric"
            value={sizeBytes}
            onChange={(event) => setSizeBytes(event.target.value)}
          />
        </div>
      </div>
      <div className="startAgreementExchangeAction__field">
        <label
          className="startAgreementExchangeAction__label"
          htmlFor={`start-exchange-comment-${requestId}`}
        >
          {startAgreementExchangeFormConst.commentLabel}
        </label>
        <textarea
          id={`start-exchange-comment-${requestId}`}
          className="startAgreementExchangeAction__textarea"
          disabled={isDisabled}
          value={comment}
          placeholder={startAgreementExchangeFormConst.commentPlaceholder}
          onChange={(event) => setComment(event.target.value)}
        />
      </div>
      <button
        type="submit"
        className="startAgreementExchangeAction__button"
        disabled={isDisabled}
      >
        {mutation.isPending
          ? startAgreementExchangeFormConst.pendingLabel
          : startAgreementExchangeFormConst.actionLabel}
      </button>
      {shouldShowUnavailableReason && (
        <p className="startAgreementExchangeAction__hint">{unavailableReason}</p>
      )}
      {validationError && (
        <p className="startAgreementExchangeAction__error" role="alert">
          {validationError}
        </p>
      )}
      {mutation.isError && (
        <p className="startAgreementExchangeAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </form>
  );
};
