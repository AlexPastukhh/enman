import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { renderHook, waitFor } from "@testing-library/react";
import type { ReactNode } from "react";
import { afterEach, describe, expect, it, vi } from "vitest";
import { useAgreementExchangeDetailsQuery } from "./useAgreementExchangeDetailsQuery";

const { mockedGetAgreementExchangeDetails } = vi.hoisted(() => ({
  mockedGetAgreementExchangeDetails: vi.fn(),
}));

vi.mock("../api/getAgreementExchangeDetails", () => ({
  getAgreementExchangeDetails: mockedGetAgreementExchangeDetails,
  __esModule: true,
}));

const createWrapper = () => {
  const queryClient = new QueryClient({
    defaultOptions: { queries: { retry: false } },
  });

  return ({ children }: { children: ReactNode }) => (
    <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
  );
};

describe("useAgreementExchangeDetailsQuery", () => {
  afterEach(() => {
    mockedGetAgreementExchangeDetails.mockReset();
  });

  it("maps details response to query data", async () => {
    const details = {
      exchangeId: 20,
      requestId: 10,
      exchangeStatus: "AwaitingClientConfirmation",
      activeProposalVersion: 1,
      request: {
        requestId: 10,
        requestStatus: "Approved",
        requestDisplayName: "Connection request #10",
        objectAddress: "Lenina 10",
      },
      activeProposal: null,
      proposals: [],
      currentActorSide: "Client",
      createdAt: "2026-01-01T10:00:00Z",
      lastActivityAt: "2026-01-02T10:00:00Z",
    };
    mockedGetAgreementExchangeDetails.mockResolvedValueOnce(details);

    const { result } = renderHook(
      () => useAgreementExchangeDetailsQuery({ exchangeId: 20 }),
      { wrapper: createWrapper() },
    );

    await waitFor(() => expect(result.current.isSuccess).toBe(true));

    expect(mockedGetAgreementExchangeDetails).toHaveBeenCalledWith(20);
    expect(result.current.data).toEqual(details);
  });

  it("does not call API for invalid exchange id", () => {
    renderHook(
      () => useAgreementExchangeDetailsQuery({ exchangeId: Number.NaN }),
      { wrapper: createWrapper() },
    );

    expect(mockedGetAgreementExchangeDetails).not.toHaveBeenCalled();
  });
});
