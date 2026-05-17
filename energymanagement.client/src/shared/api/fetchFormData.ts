import {
  antiforgeryFailureCode,
  antiforgeryHeaderName,
  ensureAntiforgeryToken,
  refreshAntiforgeryToken,
} from "./antiforgeryTokenStore";
import { AntiforgeryApiError, ApiError } from "./fetchJson";
import { isProblemDetails, type ProblemDetails } from "./problemDetails";

type FetchFormDataInit = Omit<RequestInit, "body" | "method"> & {
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

const isAntiforgeryProblem = (
  status: number,
  problemDetails: ProblemDetails | null,
): boolean =>
  status === 400 && problemDetails?.code === antiforgeryFailureCode;

export const fetchFormData = async <TResponse>(
  path: string,
  formData: FormData,
  init: FetchFormDataInit = {},
): Promise<TResponse> => {
  const { skipCsrf = false, headers: initHeaders, ...requestInit } = init;
  const headers = new Headers(initHeaders);

  if (!skipCsrf) {
    headers.set(antiforgeryHeaderName, await ensureAntiforgeryToken());
  }

  const response = await fetch(path, {
    credentials: "include",
    ...requestInit,
    method: "POST",
    headers,
    body: formData,
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
