import { afterEach, describe, expect, it, vi } from "vitest";
import { createConnectionRequest } from "./l1RequestApi";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("l1RequestApi", () => {
  it("posts create connection request to the L1 requests endpoint", async () => {
    const fetchMock = vi.fn().mockResolvedValue(
      new Response(null, {
        status: 200,
      }),
    );
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

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/l1/requests",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify(request),
      }),
    );
  });
});
