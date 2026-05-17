import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "./antiforgeryTokenStore";
import { createConnectionRequest } from "./l1RequestApi";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("l1RequestApi", () => {
  it("posts create connection request to the L1 requests endpoint", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 200 }));
    vi.stubGlobal("fetch", fetchMock);

    const request = {
      applicantContextType: "Existing",
      existingApplicantPartyId: 1,
      details: "Подключение объекта к электрическим сетям",
      address: {
        postalCode: "658480",
        region: "Алтайский край",
        city: "Заринск",
        street: "Ленина",
        house: "10",
        building: null,
        apartment: null,
      },
    };

    await expect(createConnectionRequest(request)).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/l1/requests",
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
