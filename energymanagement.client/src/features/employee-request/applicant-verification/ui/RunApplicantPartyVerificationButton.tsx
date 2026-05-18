import { ApiError } from "../../../../shared/api/fetchJson";
import type { RunApplicantPartyVerificationResponse } from "../api/applicantVerificationApiTypes";
import { useRunApplicantPartyVerificationMutation } from "../model/useRunApplicantPartyVerificationMutation";
import { applicantPartyVerificationConst } from "./applicantPartyVerificationConst";
import "./applicantPartyVerification.css";

type RunApplicantPartyVerificationButtonSurface = "dashboard" | "details";

type RunApplicantPartyVerificationButtonProps = {
  requestId: number;
  surface?: RunApplicantPartyVerificationButtonSurface;
  onVerified?: (response: RunApplicantPartyVerificationResponse) => void;
};

const getErrorMessage = (error: unknown): string => {
  if (error instanceof ApiError) {
    return error.message;
  }

  if (error instanceof Error && error.message) {
    return error.message;
  }

  return applicantPartyVerificationConst.defaultErrorMessage;
};

export const RunApplicantPartyVerificationButton = ({
  requestId,
  surface = "details",
  onVerified,
}: RunApplicantPartyVerificationButtonProps) => {
  const mutation = useRunApplicantPartyVerificationMutation();

  const handleRunVerification = () => {
    if (mutation.isPending) {
      return;
    }

    mutation.mutate(requestId, {
      onSuccess: (response) => {
        onVerified?.(response);
      },
    });
  };

  return (
    <div
      className="applicantVerificationAction"
      data-surface={surface}
      aria-live="polite"
    >
      <button
        type="button"
        className="applicantVerificationAction__button"
        disabled={mutation.isPending}
        onClick={handleRunVerification}
      >
        {mutation.isPending
          ? applicantPartyVerificationConst.pendingLabel
          : applicantPartyVerificationConst.actionLabel}
      </button>
      {mutation.isError && (
        <p className="applicantVerificationAction__error" role="alert">
          {getErrorMessage(mutation.error)}
        </p>
      )}
      {mutation.isSuccess && mutation.data?.message && (
        <p className="applicantVerificationAction__success">
          {mutation.data.message}
        </p>
      )}
    </div>
  );
};
