import { useState } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import { useAcceptAgreementProposalMutation } from "../model/useAcceptAgreementProposalMutation";
import { acceptAgreementProposalButtonConst } from "./acceptAgreementProposalButtonConst";
import "./acceptAgreementProposalButton.css";

type AcceptAgreementProposalButtonProps = {
  exchangeId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  requireConfirmation?: boolean;
  onAccepted?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return acceptAgreementProposalButtonConst.defaultErrorMessage;
};

export const AcceptAgreementProposalButton = ({
  exchangeId,
  disabled = false,
  unavailableReason = null,
  requireConfirmation = false,
  onAccepted,
}: AcceptAgreementProposalButtonProps) => {
  const [isConfirming, setIsConfirming] = useState(false);
  const mutation = useAcceptAgreementProposalMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const submitAccept = () => {
    if (isDisabled) {
      return;
    }

    mutation.mutate(exchangeId, {
      onSuccess: () => {
        setIsConfirming(false);
        onAccepted?.();
      },
    });
  };

  const handleAcceptClick = () => {
    if (isDisabled) {
      return;
    }

    if (requireConfirmation) {
      setIsConfirming(true);
      return;
    }

    submitAccept();
  };

  const handleCancelConfirmation = () => {
    if (!mutation.isPending) {
      setIsConfirming(false);
    }
  };

  return (
    <div className="acceptAgreementProposalAction" aria-live="polite">
      <button
        type="button"
        className="acceptAgreementProposalAction__button"
        disabled={isDisabled}
        onClick={handleAcceptClick}
      >
        {mutation.isPending
          ? acceptAgreementProposalButtonConst.pendingLabel
          : acceptAgreementProposalButtonConst.actionLabel}
      </button>

      {isConfirming && !disabled && (
        <div
          className="acceptAgreementProposalAction__confirmation"
          role="group"
          aria-label="Accept proposal confirmation"
        >
          <p>{acceptAgreementProposalButtonConst.confirmationText}</p>
          <div className="acceptAgreementProposalAction__confirmationActions">
            <button
              type="button"
              className="acceptAgreementProposalAction__button"
              disabled={mutation.isPending}
              onClick={submitAccept}
            >
              {mutation.isPending
                ? acceptAgreementProposalButtonConst.pendingLabel
                : acceptAgreementProposalButtonConst.confirmationLabel}
            </button>
            <button
              type="button"
              className="acceptAgreementProposalAction__secondaryButton"
              disabled={mutation.isPending}
              onClick={handleCancelConfirmation}
            >
              {acceptAgreementProposalButtonConst.cancelConfirmationLabel}
            </button>
          </div>
        </div>
      )}

      {shouldShowUnavailableReason && (
        <p className="acceptAgreementProposalAction__hint">
          {unavailableReason}
        </p>
      )}
      {mutation.isError && (
        <p className="acceptAgreementProposalAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </div>
  );
};
