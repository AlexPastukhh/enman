import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "./antiforgeryTokenStore";
import { AntiforgeryApiError } from "./fetchJson";
import { fetchFormData } from "./fetchFormData";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("fetchFormData", () => {
  it("sends FormData with CSRF token and does not set Content-Type manually", async () => {
    const formData = new FormData();
    formData.append("document", new Blob(["content"], { type: "application/pdf" }), "file.pdf");

    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(jsonResponse({ storageKey: "agreement-proposals/file.pdf" }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(fetchFormData("/api/upload", formData)).resolves.toEqual({
      storageKey: "agreement-proposals/file.pdf",
    });

    expect(fetchMock).toHaveBeenCalledTimes(2);
    expect(fetchMock.mock.calls[0]?.[0]).toBe("/api/antiforgery/token");
    expect(fetchMock.mock.calls[1]?.[0]).toBe("/api/upload");
    expect(fetchMock.mock.calls[1]?.[1]).toEqual(
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: formData,
      }),
    );

    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
    expect(headers.has("Content-Type")).toBe(false);
  });

  it("refreshes token and throws retryable error on antiforgery failure", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(
        problemResponse({
          type: "https://enman.local/problems/security/antiforgery-validation-failed",
          title: "Antiforgery validation failed",
          status: 400,
          detail: "Antiforgery token is missing, expired, or invalid.",
          code: "security.antiforgery.validation.failed",
        }),
      )
      .mockResolvedValueOnce(tokenResponse("token-2"));
    vi.stubGlobal("fetch", fetchMock);

    await expect(fetchFormData("/api/upload", new FormData())).rejects.toBeInstanceOf(
      AntiforgeryApiError,
    );

    expect(fetchMock).toHaveBeenCalledTimes(3);
    expect(fetchMock.mock.calls[2]?.[0]).toBe("/api/antiforgery/token");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });

const jsonResponse = (body: object) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });

const problemResponse = (body: object) =>
  new Response(JSON.stringify(body), {
    status: 400,
    headers: { "content-type": "application/problem+json" },
  });
