export const antiforgeryHeaderName = "X-CSRF-TOKEN";
export const antiforgeryFailureCode = "security.antiforgery.validation.failed";

type AntiforgeryTokenResponse = {
  requestToken: string;
};

let requestToken: string | null = null;
let inFlightTokenRequest: Promise<string> | null = null;

export const getCurrentAntiforgeryToken = (): string | null => requestToken;

export const clearAntiforgeryToken = (): void => {
  requestToken = null;
  inFlightTokenRequest = null;
};

export const ensureAntiforgeryToken = async (): Promise<string> => {
  if (requestToken) {
    return requestToken;
  }

  if (!inFlightTokenRequest) {
    inFlightTokenRequest = fetchAntiforgeryToken();
  }

  requestToken = await inFlightTokenRequest;
  inFlightTokenRequest = null;
  return requestToken;
};

export const refreshAntiforgeryToken = async (): Promise<string> => {
  requestToken = null;
  inFlightTokenRequest = fetchAntiforgeryToken();
  requestToken = await inFlightTokenRequest;
  inFlightTokenRequest = null;
  return requestToken;
};

const fetchAntiforgeryToken = async (): Promise<string> => {
  const response = await fetch("/api/antiforgery/token", {
    method: "GET",
    credentials: "include",
    headers: {
      Accept: "application/json",
    },
  });

  if (!response.ok) {
    throw new Error("Could not refresh antiforgery token.");
  }

  const body = (await response.json()) as Partial<AntiforgeryTokenResponse>;
  if (!body.requestToken) {
    throw new Error("Antiforgery token response did not include requestToken.");
  }

  return body.requestToken;
};
