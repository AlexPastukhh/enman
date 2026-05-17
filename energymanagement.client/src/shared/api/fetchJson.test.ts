import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "./antiforgeryTokenStore";
import { AntiforgeryApiError, fetchJson } from "./fetchJson";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("fetchJson", () => {
  it("does not fetch or send an antiforgery token for GET requests", async () => {
    const response = { ok: true };
    const fetchMock = vi.fn().mockResolvedValue(
      new Response(JSON.stringify(response), {
        status: 200,
        headers: { "content-type": "application/json" },
      }),
    );
    vi.stubGlobal("fetch", fetchMock);

    await expect(fetchJson("/api/example", { method: "GET" })).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledTimes(1);
    expect(fetchMock).toHaveBeenCalledWith(
      "/api/example",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
    const headers = fetchMock.mock.calls[0]?.[1]?.headers as Headers;
    expect(headers.has("X-CSRF-TOKEN")).toBe(false);
    expect(headers.has("Content-Type")).toBe(false);
  });

  it("sets JSON content type and sends an antiforgery token for POST requests with JSON body", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      fetchJson<void>("/api/example", {
        method: "POST",
        body: JSON.stringify({ value: 1 }),
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenCalledTimes(2);
    expect(fetchMock.mock.calls[0]?.[0]).toBe("/api/antiforgery/token");
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
    expect(headers.get("Content-Type")).toBe("application/json");
  });

  it("preserves caller-provided content type for unsafe requests", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      fetchJson<void>("/api/example", {
        method: "POST",
        body: "raw-payload",
        headers: {
          "Content-Type": "text/plain",
        },
      }),
    ).resolves.toBeUndefined();

    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("Content-Type")).toBe("text/plain");
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });

  it("uses the existing token for subsequent unsafe requests", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValue(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await fetchJson<void>("/api/first", { method: "POST" });
    await fetchJson<void>("/api/second", { method: "POST" });

    expect(fetchMock).toHaveBeenCalledTimes(3);
    expect(fetchMock.mock.calls[0]?.[0]).toBe("/api/antiforgery/token");
    expect(fetchMock.mock.calls[1]?.[0]).toBe("/api/first");
    expect(fetchMock.mock.calls[2]?.[0]).toBe("/api/second");
  });

  it("does not recursively require CSRF when skipCsrf is true", async () => {
    const fetchMock = vi.fn().mockResolvedValue(
      new Response(JSON.stringify({ requestToken: "token-1" }), {
        status: 200,
        headers: { "content-type": "application/json" },
      }),
    );
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      fetchJson<{ requestToken: string }>("/api/antiforgery/token", {
        method: "GET",
        skipCsrf: true,
      }),
    ).resolves.toEqual({ requestToken: "token-1" });

    expect(fetchMock).toHaveBeenCalledTimes(1);
  });

  it("refreshes token and throws retryable error without replaying unsafe request on antiforgery failure", async () => {
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

    await expect(fetchJson<void>("/api/example", { method: "POST" })).rejects.toBeInstanceOf(
      AntiforgeryApiError,
    );

    expect(fetchMock).toHaveBeenCalledTimes(3);
    expect(fetchMock.mock.calls[0]?.[0]).toBe("/api/antiforgery/token");
    expect(fetchMock.mock.calls[1]?.[0]).toBe("/api/example");
    expect(fetchMock.mock.calls[2]?.[0]).toBe("/api/antiforgery/token");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });

const problemResponse = (body: object) =>
  new Response(JSON.stringify(body), {
    status: 400,
    headers: { "content-type": "application/problem+json" },
  });
