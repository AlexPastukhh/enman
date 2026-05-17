import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { rejectRequestReview } from "./rejectRequestReview";

describe("rejectRequestReview", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts RejectReview command with optional feedback body", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      rejectRequestReview({
        requestId: 42,
        feedback: "Applicant must provide additional documents.",
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/employee/requests/42/review/reject",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({
          feedback: "Applicant must provide additional documents.",
        }),
      }),
    );
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });

  it("allows rejecting without feedback", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      rejectRequestReview({
        requestId: 42,
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/employee/requests/42/review/reject",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({ feedback: "" }),
      }),
    );
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
