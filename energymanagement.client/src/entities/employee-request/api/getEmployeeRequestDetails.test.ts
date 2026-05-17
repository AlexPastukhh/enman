import { afterEach, describe, expect, it, vi } from "vitest";
import { getEmployeeRequestDetails } from "./getEmployeeRequestDetails";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("getEmployeeRequestDetails", () => {
  it("gets employee request details by request id", async () => {
    const response = {
      requestId: 42,
      requestType: "Connection",
      status: "InReview",
      applicant: {
        applicantPartyId: 7,
        applicantPartyType: "Individual",
        displayName: "Ivan Petrov",
        email: "ivan@example.com",
        phoneNumber: "+79001234567",
      },
      objectAddress: "Altai Krai, Zarinsk, Lenina 10",
      details: "Request details",
      createdAt: "2026-01-02T10:30:00Z",
      reviewState: "NotStarted",
    };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await expect(getEmployeeRequestDetails(42)).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/employee/requests/42",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });

  it("encodes the request id in the endpoint path", async () => {
    const response = { requestId: 123 };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await getEmployeeRequestDetails(123);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/employee/requests/123",
      expect.objectContaining({ method: "GET" }),
    );
  });
});

const jsonResponse = (body: unknown) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
