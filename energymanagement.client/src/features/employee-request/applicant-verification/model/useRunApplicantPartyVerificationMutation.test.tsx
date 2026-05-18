/**
 * @vitest environment jsdom
 */
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { act, renderHook, waitFor } from "@testing-library/react";
import type { ReactNode } from "react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import { useRunApplicantPartyVerificationMutation } from "./useRunApplicantPartyVerificationMutation";

const { mockedRunApplicantPartyVerification } = vi.hoisted(() => ({
  mockedRunApplicantPartyVerification: vi.fn(),
}));

vi.mock("../api/runApplicantPartyVerification", () => ({
  runApplicantPartyVerification: mockedRunApplicantPartyVerification,
  __esModule: true,
}));

describe("useRunApplicantPartyVerificationMutation", () => {
  let queryClient: QueryClient;

  beforeEach(() => {
    queryClient = new QueryClient({
      defaultOptions: { queries: { retry: false }, mutations: { retry: false } },
    });
    vi.spyOn(queryClient, "invalidateQueries");
    mockedRunApplicantPartyVerification.mockResolvedValue({
      requestId: 42,
      applicantPartyId: 7,
      verificationStatus: "Verified",
      mockResult: "Passed",
      message: "Mock verification passed.",
    });
  });

  afterEach(() => {
    queryClient.clear();
    mockedRunApplicantPartyVerification.mockReset();
    vi.restoreAllMocks();
  });

  it("invalidates employee request details and dashboard reads after success", async () => {
    const wrapper = ({ children }: { children: ReactNode }) => (
      <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
    );

    const { result } = renderHook(
      () => useRunApplicantPartyVerificationMutation(),
      { wrapper },
    );

    await act(async () => {
      await result.current.mutateAsync(42);
    });

    await waitFor(() => {
      expect(queryClient.invalidateQueries).toHaveBeenCalledWith({
        queryKey: employeeRequestQueryKeys.details(42),
      });
      expect(queryClient.invalidateQueries).toHaveBeenCalledWith({
        queryKey: employeeRequestQueryKeys.all,
      });
    });
  });
});
