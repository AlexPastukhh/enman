import { useState } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import { useApproveRequestReviewMutation } from "../model/useApproveRequestReviewMutation";
import { approveReviewButtonConst } from "./approveReviewButtonConst";
import "./approveReviewButton.css";

type ApproveReviewButtonProps = {
  requestId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  requireConfirmation?: boolean;
  onApproved?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return approveReviewButtonConst.defaultErrorMessage;
};

export const ApproveReviewButton = ({
  requestId,
  disabled = false,
  unavailableReason = null,
  requireConfirmation = false,
  onApproved,
}: ApproveReviewButtonProps) => {
  const [isConfirming, setIsConfirming] = useState(false);
  const mutation = useApproveRequestReviewMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const submitApprove = () => {
    if (isDisabled) {
      return;
    }

    mutation.mutate(requestId, {
      onSuccess: () => {
        setIsConfirming(false);
        onApproved?.();
      },
    });
  };

  const handleApproveClick = () => {
    if (isDisabled) {
      return;
    }

    if (requireConfirmation) {
      setIsConfirming(true);
      return;
    }

    submitApprove();
  };

  const handleCancelConfirmation = () => {
    if (!mutation.isPending) {
      setIsConfirming(false);
    }
  };

  return (
    <div className="approveReviewAction" aria-live="polite">
      <button
        type="button"
        className="approveReviewAction__button"
        disabled={isDisabled}
        onClick={handleApproveClick}
      >
        {mutation.isPending
          ? approveReviewButtonConst.pendingLabel
          : approveReviewButtonConst.actionLabel}
      </button>

      {isConfirming && !disabled && (
        <div
          className="approveReviewAction__confirmation"
          role="group"
          aria-label="Подтверждение одобрения заявки"
        >
          <p>{approveReviewButtonConst.confirmationText}</p>
          <div className="approveReviewAction__confirmationActions">
            <button
              type="button"
              className="approveReviewAction__button"
              disabled={mutation.isPending}
              onClick={submitApprove}
            >
              {mutation.isPending
                ? approveReviewButtonConst.pendingLabel
                : approveReviewButtonConst.confirmationLabel}
            </button>
            <button
              type="button"
              className="approveReviewAction__secondaryButton"
              disabled={mutation.isPending}
              onClick={handleCancelConfirmation}
            >
              {approveReviewButtonConst.cancelConfirmationLabel}
            </button>
          </div>
        </div>
      )}

      {shouldShowUnavailableReason && (
        <p className="approveReviewAction__hint">{unavailableReason}</p>
      )}
      {mutation.isError && (
        <p className="approveReviewAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </div>
  );
};
