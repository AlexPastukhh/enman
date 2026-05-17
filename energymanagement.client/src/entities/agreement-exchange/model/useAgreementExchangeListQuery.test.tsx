import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { renderHook, waitFor } from "@testing-library/react";
import type { ReactNode } from "react";
import { afterEach, describe, expect, it, vi } from "vitest";
import { useAgreementExchangeListQuery } from "./useAgreementExchangeListQuery";

const { mockedListAgreementExchanges } = vi.hoisted(() => ({
  mockedListAgreementExchanges: vi.fn(),
}));

vi.mock("../api/listAgreementExchanges", () => ({
  listAgreementExchanges: mockedListAgreementExchanges,
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

describe("useAgreementExchangeListQuery", () => {
  afterEach(() => {
    mockedListAgreementExchanges.mockReset();
  });

  it("maps response exchanges to list state", async () => {
    const exchanges = [
      {
        requestId: 10,
        exchangeId: 20,
        exchangeStatus: "AwaitingClientConfirmation",
        activeProposalVersion: 1,
        activeProposalSender: "Employee",
        activeProposalSenderId: 5,
        requestDisplayName: "Request #10",
        objectAddress: "Lenina 10",
        createdAt: "2026-01-01T10:00:00Z",
        lastActivityAt: "2026-01-02T10:00:00Z",
      },
    ];
    mockedListAgreementExchanges.mockResolvedValueOnce({ exchanges });

    const { result } = renderHook(() => useAgreementExchangeListQuery(), {
      wrapper: createWrapper(),
    });

    await waitFor(() => expect(result.current.isSuccess).toBe(true));

    expect(result.current.data).toEqual(exchanges);
  });

  it("uses an empty list when server omits exchanges", async () => {
    mockedListAgreementExchanges.mockResolvedValueOnce({});

    const { result } = renderHook(() => useAgreementExchangeListQuery(), {
      wrapper: createWrapper(),
    });

    await waitFor(() => expect(result.current.isSuccess).toBe(true));

    expect(result.current.data).toEqual([]);
  });
});
