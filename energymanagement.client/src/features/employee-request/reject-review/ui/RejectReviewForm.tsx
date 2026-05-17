import { useState, type FormEvent } from "react";
import { ApiError } from "../../../../shared/api/fetchJson";
import { useRejectRequestReviewMutation } from "../model/useRejectRequestReviewMutation";
import { rejectReviewFormConst } from "./rejectReviewFormConst";
import "./rejectReviewForm.css";

type RejectReviewFormProps = {
  requestId: number;
  disabled?: boolean;
  unavailableReason?: string | null;
  showEmptyFeedbackWarning?: boolean;
  onRejected?: () => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return rejectReviewFormConst.defaultErrorMessage;
};

export const RejectReviewForm = ({
  requestId,
  disabled = false,
  unavailableReason = null,
  showEmptyFeedbackWarning = false,
  onRejected,
}: RejectReviewFormProps) => {
  const [feedback, setFeedback] = useState("");
  const mutation = useRejectRequestReviewMutation();
  const isDisabled = disabled || mutation.isPending;
  const shouldShowUnavailableReason = disabled && unavailableReason;
  const shouldShowNoFeedbackWarning =
    showEmptyFeedbackWarning && !disabled && feedback.trim().length === 0;

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (isDisabled) {
      return;
    }

    mutation.mutate(
      {
        requestId,
        feedback: feedback.trim(),
      },
      {
        onSuccess: () => {
          onRejected?.();
        },
      },
    );
  };

  return (
    <form className="rejectReviewAction" aria-live="polite" onSubmit={handleSubmit}>
      <div className="rejectReviewAction__field">
        <label className="rejectReviewAction__label" htmlFor={`reject-review-feedback-${requestId}`}>
          {rejectReviewFormConst.feedbackLabel}
        </label>
        <textarea
          id={`reject-review-feedback-${requestId}`}
          className="rejectReviewAction__textarea"
          disabled={isDisabled}
          value={feedback}
          placeholder={rejectReviewFormConst.feedbackPlaceholder}
          onChange={(event) => setFeedback(event.target.value)}
        />
      </div>

      {shouldShowNoFeedbackWarning && (
        <p className="rejectReviewAction__warning">
          {rejectReviewFormConst.noFeedbackWarning}
        </p>
      )}

      <button
        type="submit"
        className="rejectReviewAction__button"
        disabled={isDisabled}
      >
        {mutation.isPending
          ? rejectReviewFormConst.pendingLabel
          : rejectReviewFormConst.actionLabel}
      </button>

      {shouldShowUnavailableReason && (
        <p className="rejectReviewAction__hint">{unavailableReason}</p>
      )}
      {mutation.isError && (
        <p className="rejectReviewAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
    </form>
  );
};
