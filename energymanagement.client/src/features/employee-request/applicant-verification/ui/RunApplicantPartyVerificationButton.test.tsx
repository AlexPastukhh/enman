/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { RunApplicantPartyVerificationButton } from "./RunApplicantPartyVerificationButton";

const { mockedUseRunApplicantPartyVerificationMutation } = vi.hoisted(() => ({
  mockedUseRunApplicantPartyVerificationMutation: vi.fn(),
}));

vi.mock("../model/useRunApplicantPartyVerificationMutation", () => ({
  useRunApplicantPartyVerificationMutation:
    mockedUseRunApplicantPartyVerificationMutation,
  __esModule: true,
}));

describe("RunApplicantPartyVerificationButton", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseRunApplicantPartyVerificationMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      isSuccess: false,
      error: null,
      data: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseRunApplicantPartyVerificationMutation.mockReset();
  });

  it("submits verification command with requestId", async () => {
    const user = userEvent.setup();

    render(<RunApplicantPartyVerificationButton requestId={42} />);

    await user.click(screen.getByRole("button", { name: "Проверить данные" }));

    expect(mutate).toHaveBeenCalledWith(42, expect.any(Object));
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseRunApplicantPartyVerificationMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      isSuccess: false,
      error: null,
      data: null,
    });

    render(<RunApplicantPartyVerificationButton requestId={42} />);

    expect(screen.getByRole("button", { name: "Проверяем..." })).toBeDisabled();
  });

  it("shows command error feedback", () => {
    mockedUseRunApplicantPartyVerificationMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      isSuccess: false,
      error: new Error("Verification failed."),
      data: null,
    });

    render(<RunApplicantPartyVerificationButton requestId={42} />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Verification failed.",
    );
  });
});
