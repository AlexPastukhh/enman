import { afterEach, describe, expect, it, vi } from "vitest";
import { listEmployeeDashboardRequests } from "./listEmployeeDashboardRequests";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("listEmployeeDashboardRequests", () => {
  it("gets employee requests without filters", async () => {
    const response = { requests: [] };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await expect(listEmployeeDashboardRequests()).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/employee/requests",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });

  it("maps status and review state filters to query string", async () => {
    const response = { requests: [] };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await listEmployeeDashboardRequests({
      status: "InReview",
      reviewState: "StartedByAnotherEmployee",
    });

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/employee/requests?status=InReview&reviewState=StartedByAnotherEmployee",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });
});

const jsonResponse = (body: unknown) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
