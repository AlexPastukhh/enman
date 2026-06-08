import {
  antiforgeryFailureCode,
  antiforgeryHeaderName,
  ensureAntiforgeryToken,
  refreshAntiforgeryToken,
} from "./antiforgeryTokenStore";
import {
  isProblemDetails,
  problemDetailsToErrorMessage,
  type ProblemDetails,
} from "./problemDetails";

export class ApiError extends Error {
  public readonly status: number;
  public readonly problemDetails: ProblemDetails | null;

  constructor(status: number, problemDetails: ProblemDetails | null) {
    super(problemDetailsToErrorMessage(problemDetails));
    this.status = status;
    this.problemDetails = problemDetails;
  }
}

export class AntiforgeryApiError extends ApiError {
  public readonly isRetryableSessionSecurityError = true;
}

type FetchJsonInit = RequestInit & {
  skipCsrf?: boolean;
};

const tryReadProblemDetails = async (
  response: Response,
): Promise<ProblemDetails | null> => {
  const contentType = response.headers.get("content-type") ?? "";
  if (!contentType.includes("json")) {
    return null;
  }

  const text = await response.text();
  if (!text) {
    return null;
  }

  const body = JSON.parse(text) as unknown;
  return isProblemDetails(body) ? body : null;
};

const unsafeMethods = new Set(["POST", "PUT", "PATCH", "DELETE"]);

const isUnsafeMethod = (method: string | undefined): boolean =>
  unsafeMethods.has((method ?? "GET").toUpperCase());

const isAntiforgeryProblem = (
  status: number,
  problemDetails: ProblemDetails | null,
): boolean =>
  status === 400 && problemDetails?.code === antiforgeryFailureCode;

export const fetchJson = async <TResponse>(
  path: string,
  init: FetchJsonInit,
): Promise<TResponse> => {
  const { skipCsrf = false, headers: initHeaders, ...requestInit } = init;
  const headers = new Headers(initHeaders);
  if (requestInit.body !== undefined && !headers.has("Content-Type")) {
    headers.set("Content-Type", "application/json");
  }

  if (isUnsafeMethod(requestInit.method) && !skipCsrf) {
    headers.set(antiforgeryHeaderName, await ensureAntiforgeryToken());
  }

  const response = await fetch(path, {
    credentials: "include",
    ...requestInit,
    headers,
  });

  if (!response.ok) {
    const problemDetails = await tryReadProblemDetails(response);
    if (isAntiforgeryProblem(response.status, problemDetails)) {
      await refreshAntiforgeryToken();
      throw new AntiforgeryApiError(response.status, problemDetails);
    }

    throw new ApiError(response.status, problemDetails);
  }

  if (response.status === 204) {
    return undefined as TResponse;
  }

  const contentType = response.headers.get("content-type") ?? "";
  if (!contentType.includes("json")) {
    return undefined as TResponse;
  }

  return (await response.json()) as TResponse;
};
