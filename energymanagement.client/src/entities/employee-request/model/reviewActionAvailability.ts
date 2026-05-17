import type {
  EmployeeRequestDetails,
  EmployeeReviewActionAvailability,
} from "./employeeRequestTypes";

export const getEmployeeReviewActionAvailability = (
  details: EmployeeRequestDetails,
): EmployeeReviewActionAvailability => {
  const status = details.status;
  const reviewState = details.reviewState;

  if (status === "InReview" && reviewState === "NotStarted") {
    return {
      canStartReview: true,
      canApproveReview: false,
      canRejectReview: false,
      reason: null,
    };
  }

  if (reviewState === "StartedByCurrentEmployee") {
    return {
      canStartReview: false,
      canApproveReview: true,
      canRejectReview: true,
      reason: "Review is already started by the current Employee.",
    };
  }

  if (reviewState === "StartedByAnotherEmployee") {
    return {
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
      reason: "Another Employee has already started this review.",
    };
  }

  if (reviewState === "Approved" || reviewState === "Rejected") {
    return {
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
      reason: "Review is already completed.",
    };
  }

  return {
    canStartReview: false,
    canApproveReview: false,
    canRejectReview: false,
    reason: "Review actions are unavailable for this request status.",
  };
};
