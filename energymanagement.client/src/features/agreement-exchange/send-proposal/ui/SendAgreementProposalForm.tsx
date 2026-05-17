import { useState, type FormEvent } from "react";
import type { AgreementExchangeViewerRole } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";
import { ApiError } from "../../../../shared/api/fetchJson";
import { useSendAgreementProposalMutation } from "../model/useSendAgreementProposalMutation";
import { sendAgreementProposalFormConst } from "./sendAgreementProposalFormConst";
import "./sendAgreementProposalForm.css";

type SendAgreementProposalFormProps = {
  exchangeId: number;
  viewerRole: AgreementExchangeViewerRole;
  disabled?: boolean;
  unavailableReason?: string | null;
  onSent?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return sendAgreementProposalFormConst.defaultErrorMessage;
};

const isPositiveInteger = (value: string): boolean => {
  const parsed = Number(value);
  return Number.isInteger(parsed) && parsed > 0;
};

export const SendAgreementProposalForm = ({
  exchangeId,
  viewerRole,
  disabled = false,
  unavailableReason = null,
  onSent,
}: SendAgreementProposalFormProps) => {
  const [storageKey, setStorageKey] = useState("");
  const [originalFileName, setOriginalFileName] = useState("");
  const [contentType, setContentType] = useState("application/pdf");
  const [sizeBytes, setSizeBytes] = useState("");
  const [comment, setComment] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);
  const mutation = useSendAgreementProposalMutation();
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
      setValidationError(sendAgreementProposalFormConst.validationErrorMessage);
      return;
    }

    setValidationError(null);

    mutation.mutate(
      {
        exchangeId,
        proposal: {
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
        onSuccess: () => {
          onSent?.();
        },
      },
    );
  };

  const title =
    viewerRole === "Client"
      ? sendAgreementProposalFormConst.clientTitle
      : sendAgreementProposalFormConst.employeeTitle;

  return (
    <form
      className="sendAgreementProposalAction"
      aria-live="polite"
      onSubmit={handleSubmit}
    >
      <h4 className="sendAgreementProposalAction__title">{title}</h4>
      <div className="sendAgreementProposalAction__grid" role="group" aria-label={sendAgreementProposalFormConst.documentSectionTitle}>
        <div className="sendAgreementProposalAction__field">
          <label className="sendAgreementProposalAction__label" htmlFor={`send-proposal-storage-key-${exchangeId}`}>
            {sendAgreementProposalFormConst.storageKeyLabel}
          </label>
          <input
            id={`send-proposal-storage-key-${exchangeId}`}
            className="sendAgreementProposalAction__input"
            disabled={isDisabled}
            value={storageKey}
            onChange={(event) => setStorageKey(event.target.value)}
          />
        </div>
        <div className="sendAgreementProposalAction__field">
          <label className="sendAgreementProposalAction__label" htmlFor={`send-proposal-file-name-${exchangeId}`}>
            {sendAgreementProposalFormConst.originalFileNameLabel}
          </label>
          <input
            id={`send-proposal-file-name-${exchangeId}`}
            className="sendAgreementProposalAction__input"
            disabled={isDisabled}
            value={originalFileName}
            onChange={(event) => setOriginalFileName(event.target.value)}
          />
        </div>
        <div className="sendAgreementProposalAction__field">
          <label className="sendAgreementProposalAction__label" htmlFor={`send-proposal-content-type-${exchangeId}`}>
            {sendAgreementProposalFormConst.contentTypeLabel}
          </label>
          <input
            id={`send-proposal-content-type-${exchangeId}`}
            className="sendAgreementProposalAction__input"
            disabled={isDisabled}
            value={contentType}
            onChange={(event) => setContentType(event.target.value)}
          />
        </div>
        <div className="sendAgreementProposalAction__field">
          <label className="sendAgreementProposalAction__label" htmlFor={`send-proposal-size-${exchangeId}`}>
            {sendAgreementProposalFormConst.sizeBytesLabel}
          </label>
          <input
            id={`send-proposal-size-${exchangeId}`}
            className="sendAgreementProposalAction__input"
            disabled={isDisabled}
            inputMode="numeric"
            value={sizeBytes}
            onChange={(event) => setSizeBytes(event.target.value)}
          />
        </div>
      </div>
      <div className="sendAgreementProposalAction__field">
        <label className="sendAgreementProposalAction__label" htmlFor={`send-proposal-comment-${exchangeId}`}>
          {sendAgreementProposalFormConst.commentLabel}
        </label>
        <textarea
          id={`send-proposal-comment-${exchangeId}`}
          className="sendAgreementProposalAction__textarea"
          disabled={isDisabled}
          value={comment}
          placeholder={sendAgreementProposalFormConst.commentPlaceholder}
          onChange={(event) => setComment(event.target.value)}
        />
      </div>
      <button
        type="submit"
        className="sendAgreementProposalAction__button"
        disabled={isDisabled}
      >
        {mutation.isPending
          ? sendAgreementProposalFormConst.pendingLabel
          : sendAgreementProposalFormConst.actionLabel}
      </button>
      {shouldShowUnavailableReason && (
        <p className="sendAgreementProposalAction__hint">{unavailableReason}</p>
      )}
      {validationError && (
        <p className="sendAgreementProposalAction__error" role="alert">
          {validationError}
        </p>
      )}
      {mutation.isError && (
        <p className="sendAgreementProposalAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </form>
  );
};
