import { useState, type FormEvent } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import { useFinalRefuseAgreementExchangeMutation } from "../model/useFinalRefuseAgreementExchangeMutation";
import {
  finalRefusalReasonMaxLength,
  finalRefuseAgreementExchangeFormConst,
} from "./finalRefuseAgreementExchangeFormConst";
import "./finalRefuseAgreementExchangeForm.css";

type FinalRefuseAgreementExchangeFormProps = {
  exchangeId: number;
  requestId?: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  requireConfirmation?: boolean;
  onFinalRefused?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return finalRefuseAgreementExchangeFormConst.defaultErrorMessage;
};

const validateReason = (reason: string): string | null => {
  if (reason.length > 0 && reason.trim().length === 0) {
    return finalRefuseAgreementExchangeFormConst.blankReasonError;
  }

  if (reason.trim().length > finalRefusalReasonMaxLength) {
    return finalRefuseAgreementExchangeFormConst.reasonTooLongError;
  }

  return null;
};

export const FinalRefuseAgreementExchangeForm = ({
  exchangeId,
  requestId,
  disabled = false,
  unavailableReason = null,
  requireConfirmation = false,
  onFinalRefused,
}: FinalRefuseAgreementExchangeFormProps) => {
  const [reason, setReason] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);
  const [isConfirming, setIsConfirming] = useState(false);
  const mutation = useFinalRefuseAgreementExchangeMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const submitFinalRefusal = () => {
    if (isDisabled) {
      return;
    }

    const reasonError = validateReason(reason);
    if (reasonError) {
      setValidationError(reasonError);
      setIsConfirming(false);
      return;
    }

    const trimmedReason = reason.trim();
    setValidationError(null);

    mutation.mutate(
      {
        exchangeId,
        requestId,
        payload: trimmedReason.length > 0 ? { reason: trimmedReason } : undefined,
      },
      {
        onSuccess: () => {
          setIsConfirming(false);
          onFinalRefused?.();
        },
      },
    );
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isDisabled) {
      return;
    }

    const reasonError = validateReason(reason);
    if (reasonError) {
      setValidationError(reasonError);
      return;
    }

    if (requireConfirmation) {
      setValidationError(null);
      setIsConfirming(true);
      return;
    }

    submitFinalRefusal();
  };

  const handleCancelConfirmation = () => {
    if (!mutation.isPending) {
      setIsConfirming(false);
    }
  };

  return (
    <form
      className="finalRefuseAgreementExchangeAction"
      aria-live="polite"
      onSubmit={handleSubmit}
    >
      <h4 className="finalRefuseAgreementExchangeAction__title">
        {finalRefuseAgreementExchangeFormConst.title}
      </h4>
      <p className="finalRefuseAgreementExchangeAction__description">
        {finalRefuseAgreementExchangeFormConst.description}
      </p>
      <div className="finalRefuseAgreementExchangeAction__field">
        <label
          className="finalRefuseAgreementExchangeAction__label"
          htmlFor={`final-refuse-reason-${exchangeId}`}
        >
          {finalRefuseAgreementExchangeFormConst.reasonLabel}
        </label>
        <textarea
          id={`final-refuse-reason-${exchangeId}`}
          className="finalRefuseAgreementExchangeAction__textarea"
          disabled={isDisabled}
          value={reason}
          placeholder={finalRefuseAgreementExchangeFormConst.reasonPlaceholder}
          onChange={(event) => setReason(event.target.value)}
        />
        <p className="finalRefuseAgreementExchangeAction__hint">
          {finalRefuseAgreementExchangeFormConst.reasonHelpText}
        </p>
      </div>
      <button
        type="submit"
        className="finalRefuseAgreementExchangeAction__button"
        disabled={isDisabled}
      >
        {mutation.isPending
          ? finalRefuseAgreementExchangeFormConst.pendingLabel
          : finalRefuseAgreementExchangeFormConst.actionLabel}
      </button>

      {isConfirming && !disabled && (
        <div
          className="finalRefuseAgreementExchangeAction__confirmation"
          role="group"
          aria-label="Подтверждение финального отказа"
        >
          <p>{finalRefuseAgreementExchangeFormConst.confirmationText}</p>
          <div className="finalRefuseAgreementExchangeAction__confirmationActions">
            <button
              type="button"
              className="finalRefuseAgreementExchangeAction__button"
              disabled={mutation.isPending}
              onClick={submitFinalRefusal}
            >
              {mutation.isPending
                ? finalRefuseAgreementExchangeFormConst.pendingLabel
                : finalRefuseAgreementExchangeFormConst.confirmationLabel}
            </button>
            <button
              type="button"
              className="finalRefuseAgreementExchangeAction__secondaryButton"
              disabled={mutation.isPending}
              onClick={handleCancelConfirmation}
            >
              {finalRefuseAgreementExchangeFormConst.cancelConfirmationLabel}
            </button>
          </div>
        </div>
      )}

      {shouldShowUnavailableReason && (
        <p className="finalRefuseAgreementExchangeAction__hint">
          {unavailableReason}
        </p>
      )}
      {validationError && (
        <p className="finalRefuseAgreementExchangeAction__error" role="alert">
          {validationError}
        </p>
      )}
      {mutation.isError && (
        <p className="finalRefuseAgreementExchangeAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </form>
  );
};
