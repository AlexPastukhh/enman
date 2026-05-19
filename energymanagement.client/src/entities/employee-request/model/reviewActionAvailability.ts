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
      reason: "Рассмотрение уже начато текущим сотрудником.",
    };
  }

  if (reviewState === "StartedByAnotherEmployee") {
    return {
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
      reason: "Другой сотрудник уже начал рассмотрение этой заявки.",
    };
  }

  if (reviewState === "Approved" || reviewState === "Rejected") {
    return {
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
      reason: "Рассмотрение уже завершено.",
    };
  }

  return {
    canStartReview: false,
    canApproveReview: false,
    canRejectReview: false,
    reason: "Действия рассмотрения недоступны для текущего статуса заявки.",
  };
};
