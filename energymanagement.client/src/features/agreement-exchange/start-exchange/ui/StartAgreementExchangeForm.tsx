import { useState, type FormEvent } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import { uploadAgreementProposalDocument } from "../../upload-document/api/uploadAgreementProposalDocument";
import { useStartAgreementExchangeMutation } from "../model/useStartAgreementExchangeMutation";
import { startAgreementExchangeFormConst } from "./startAgreementExchangeFormConst";
import "./startAgreementExchangeForm.css";

type StartAgreementExchangeFormProps = {
  requestId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  onStarted?: () => void;
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

export const StartAgreementExchangeForm = ({
  requestId,
  disabled = false,
  unavailableReason = null,
  onStarted,
}: StartAgreementExchangeFormProps) => {
  const [document, setDocument] = useState<File | null>(null);
  const [comment, setComment] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);
  const [uploadError, setUploadError] = useState<string | null>(null);
  const [isUploading, setIsUploading] = useState(false);
  const mutation = useStartAgreementExchangeMutation();
  const isDisabled = disabled || isUploading || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isDisabled) {
      return;
    }

    if (document === null) {
      setValidationError(startAgreementExchangeFormConst.validationErrorMessage);
      return;
    }

    setValidationError(null);
    setUploadError(null);

    try {
      setIsUploading(true);
      const documentRef = await uploadAgreementProposalDocument({ document });

      mutation.mutate(
        {
          requestId,
          proposal: {
            document: documentRef,
            comment: comment.trim() || null,
          },
        },
        {
          onSuccess: () => {
            onStarted?.();
          },
        },
      );
    } catch (error) {
      setUploadError(getErrorMessage(error));
    } finally {
      setIsUploading(false);
    }
  };

  const pendingLabel = isUploading
    ? startAgreementExchangeFormConst.uploadingLabel
    : startAgreementExchangeFormConst.pendingLabel;

  return (
    <form
      className="startAgreementExchangeAction"
      aria-live="polite"
      onSubmit={handleSubmit}
    >
      <h4 className="startAgreementExchangeAction__title">
        {startAgreementExchangeFormConst.title}
      </h4>
      <div className="startAgreementExchangeAction__field">
        <label
          className="startAgreementExchangeAction__label"
          htmlFor={`start-exchange-document-${requestId}`}
        >
          {startAgreementExchangeFormConst.documentLabel}
        </label>
        <input
          id={`start-exchange-document-${requestId}`}
          className="startAgreementExchangeAction__input"
          type="file"
          disabled={isDisabled}
          onChange={(event) => {
            setDocument(event.target.files?.[0] ?? null);
          }}
        />
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
        {isUploading || mutation.isPending
          ? pendingLabel
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
      {uploadError && (
        <p className="startAgreementExchangeAction__error" role="alert">
          {uploadError}
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
