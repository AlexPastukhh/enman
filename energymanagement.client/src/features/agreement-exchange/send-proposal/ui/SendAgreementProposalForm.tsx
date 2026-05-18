import { useState, type FormEvent } from "react";
import type { AgreementExchangeViewerRole } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";
import { ApiError } from "../../../../shared/api/fetchJson";
import { uploadAgreementProposalDocument } from "../../upload-document/api/uploadAgreementProposalDocument";
import { useSendAgreementProposalMutation } from "../model/useSendAgreementProposalMutation";
import { sendAgreementProposalFormConst } from "./sendAgreementProposalFormConst";
import "./sendAgreementProposalForm.css";

type SendAgreementProposalFormProps = {
  exchangeId: number;
  requestId: number;
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

export const SendAgreementProposalForm = ({
  exchangeId,
  requestId,
  viewerRole,
  disabled = false,
  unavailableReason = null,
  onSent,
}: SendAgreementProposalFormProps) => {
  const [document, setDocument] = useState<File | null>(null);
  const [comment, setComment] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);
  const [uploadError, setUploadError] = useState<string | null>(null);
  const [isUploading, setIsUploading] = useState(false);
  const mutation = useSendAgreementProposalMutation();
  const isDisabled = disabled || isUploading || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isDisabled) {
      return;
    }

    if (document === null) {
      setValidationError(sendAgreementProposalFormConst.validationErrorMessage);
      return;
    }

    setValidationError(null);
    setUploadError(null);

    try {
      setIsUploading(true);
      const documentRef = await uploadAgreementProposalDocument({ document });

      mutation.mutate(
        {
          exchangeId,
          requestId,
          proposal: {
            document: documentRef,
            comment: comment.trim() || null,
          },
        },
        {
          onSuccess: () => {
            onSent?.();
          },
        },
      );
    } catch (error) {
      setUploadError(getErrorMessage(error));
    } finally {
      setIsUploading(false);
    }
  };

  const title =
    viewerRole === "Client"
      ? sendAgreementProposalFormConst.clientTitle
      : sendAgreementProposalFormConst.employeeTitle;
  const pendingLabel = isUploading
    ? sendAgreementProposalFormConst.uploadingLabel
    : sendAgreementProposalFormConst.pendingLabel;

  return (
    <form
      className="sendAgreementProposalAction"
      aria-live="polite"
      onSubmit={handleSubmit}
    >
      <h4 className="sendAgreementProposalAction__title">{title}</h4>
      <div className="sendAgreementProposalAction__field">
        <label
          className="sendAgreementProposalAction__label"
          htmlFor={`send-proposal-document-${exchangeId}`}
        >
          {sendAgreementProposalFormConst.documentLabel}
        </label>
        <input
          id={`send-proposal-document-${exchangeId}`}
          className="sendAgreementProposalAction__input"
          type="file"
          disabled={isDisabled}
          onChange={(event) => {
            setDocument(event.target.files?.[0] ?? null);
          }}
        />
      </div>
      <div className="sendAgreementProposalAction__field">
        <label
          className="sendAgreementProposalAction__label"
          htmlFor={`send-proposal-comment-${exchangeId}`}
        >
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
        {isUploading || mutation.isPending
          ? pendingLabel
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
      {uploadError && (
        <p className="sendAgreementProposalAction__error" role="alert">
          {uploadError}
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
