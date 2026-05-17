import { ApiError } from "../../../../shared/api/fetchJson";
import { useStartRequestReviewMutation } from "../model/useStartRequestReviewMutation";
import { startReviewButtonConst } from "./startReviewButtonConst";
import "./startReviewButton.css";

type StartReviewButtonSurface = "dashboard" | "details";

type StartReviewButtonProps = {
  requestId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  surface?: StartReviewButtonSurface;
  onStarted?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return startReviewButtonConst.defaultErrorMessage;
};

export const StartReviewButton = ({
  requestId,
  disabled = false,
  unavailableReason = null,
  surface = "details",
  onStarted,
}: StartReviewButtonProps) => {
  const mutation = useStartRequestReviewMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;

  const handleStartReview = () => {
    if (isDisabled) {
      return;
    }

    mutation.mutate(requestId, {
      onSuccess: () => {
        onStarted?.();
      },
    });
  };

  return (
    <div
      className="startReviewAction"
      data-surface={surface}
      aria-live="polite"
    >
      <button
        type="button"
        className="startReviewAction__button"
        disabled={isDisabled}
        onClick={handleStartReview}
      >
        {mutation.isPending
          ? startReviewButtonConst.pendingLabel
          : startReviewButtonConst.actionLabel}
      </button>
      {shouldShowUnavailableReason && (
        <p className="startReviewAction__hint">{unavailableReason}</p>
      )}
      {mutation.isError && (
        <p className="startReviewAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </div>
  );
};
