import type {
  AgreementProposal,
  RequestRecord,
  UiState,
} from "./mockData";

export type MockApiResult<T> =
  | { kind: "success"; data: T; message: string }
  | { kind: "validationError"; message: string; fieldErrors: Record<string, string> }
  | { kind: "serverRejection"; message: string }
  | { kind: "accessDenied"; message: string }
  | { kind: "empty"; message: string }
  | { kind: "notFound"; message: string };

const pause = async () => {
  await new Promise((resolve) => window.setTimeout(resolve, 450));
};

const stateResult = <T>(uiState: UiState, data: T, successMessage: string): MockApiResult<T> => {
  if (uiState === "validationError") {
    return {
      kind: "validationError",
      message: "Mock validation error: required fields or document references are missing.",
      fieldErrors: {
        objectAddress: "Object address is required for request creation.",
        document: "Attach a document reference before sending this version.",
      },
    };
  }

  if (uiState === "serverRejection") {
    return {
      kind: "serverRejection",
      message: "Mock server/domain rejection: the operation is not allowed in the selected state.",
    };
  }

  if (uiState === "accessDenied") {
    return {
      kind: "accessDenied",
      message: "Mock access denied: current actor cannot access this resource.",
    };
  }

  if (uiState === "empty") {
    return {
      kind: "empty",
      message: "Mock empty state: no records are available for this view.",
    };
  }

  if (uiState === "notFound") {
    return {
      kind: "notFound",
      message: "Mock not found: requested resource does not exist or is hidden.",
    };
  }

  return { kind: "success", data, message: successMessage };
};

export const mockApi = {
  async createRequest(uiState: UiState, request: RequestRecord): Promise<MockApiResult<RequestRecord>> {
    await pause();
    return stateResult(uiState, request, "Request created in mock flow with InReview status.");
  },

  async saveApplicantData<T>(uiState: UiState, applicantData: T): Promise<MockApiResult<T>> {
    await pause();
    return stateResult(uiState, applicantData, "Applicant data saved as standalone demo data. No verification started.");
  },

  async submitReview(uiState: UiState, request: RequestRecord): Promise<MockApiResult<RequestRecord>> {
    await pause();
    return stateResult(uiState, request, "Request review saved. Agreement proposal was not created automatically.");
  },

  async sendAgreementVersion(
    uiState: UiState,
    agreement: AgreementProposal,
  ): Promise<MockApiResult<AgreementProposal>> {
    await pause();
    return stateResult(uiState, agreement, "Agreement proposal version sent in mock flow.");
  },

  async acceptAgreement(uiState: UiState, agreement: AgreementProposal): Promise<MockApiResult<AgreementProposal>> {
    await pause();
    return stateResult(uiState, agreement, "Proposal accepted in mock flow. It is not shown as legally Signed.");
  },
};
