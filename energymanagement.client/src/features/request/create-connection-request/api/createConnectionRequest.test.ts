import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { createConnectionRequest } from "./createConnectionRequest";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("createConnectionRequest", () => {
  it("posts create connection request to the requests endpoint", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 200 }));
    vi.stubGlobal("fetch", fetchMock);

    const request = {
      applicantContextType: "Existing",
      existingApplicantPartyId: 1,
      details: "Connection request",
      address: {
        postalCode: "658480",
        region: "Altai Krai",
        city: "Zarinsk",
        street: "Lenina",
        house: "10",
        building: null,
        apartment: null,
      },
    };

    await expect(createConnectionRequest(request)).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/requests",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify(request),
      }),
    );
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
