import { fallbackErrorMessage } from "../errors/clientErrorMessages";
import { isProblemDetails, type ProblemDetails } from "./problemDetails";

export class ApiError extends Error {
  public readonly status: number;
  public readonly problemDetails: ProblemDetails | null;

  constructor(status: number, problemDetails: ProblemDetails | null) {
    super(problemDetails?.detail ?? problemDetails?.title ?? fallbackErrorMessage);
    this.status = status;
    this.problemDetails = problemDetails;
  }
}

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


export const fetchJson = async <TResponse>(
  path: string,
  init: RequestInit,
): Promise<TResponse> => {
  const response = await fetch(path, {
    credentials: "include",
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...init.headers,
    },
  });

  if (!response.ok) {
    throw new ApiError(response.status, await tryReadProblemDetails(response));
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
