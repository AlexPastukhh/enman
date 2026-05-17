/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { AcceptAgreementProposalButton } from "./AcceptAgreementProposalButton";

const { mockedUseAcceptAgreementProposalMutation } = vi.hoisted(() => ({
  mockedUseAcceptAgreementProposalMutation: vi.fn(),
}));

vi.mock("../model/useAcceptAgreementProposalMutation", () => ({
  useAcceptAgreementProposalMutation: mockedUseAcceptAgreementProposalMutation,
  __esModule: true,
}));

describe("AcceptAgreementProposalButton", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseAcceptAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseAcceptAgreementProposalMutation.mockReset();
  });

  it("submits Accept command for the given exchange", async () => {
    const user = userEvent.setup();

    render(<AcceptAgreementProposalButton exchangeId={77} />);

    await user.click(screen.getByRole("button", { name: "Accept proposal" }));

    expect(mutate).toHaveBeenCalledWith(77, expect.any(Object));
  });

  it("can require confirmation before submitting", async () => {
    const user = userEvent.setup();

    render(<AcceptAgreementProposalButton exchangeId={77} requireConfirmation />);

    await user.click(screen.getByRole("button", { name: "Accept proposal" }));

    expect(mutate).not.toHaveBeenCalled();
    expect(
      screen.getByText(/Accepting is a final positive agreement decision/i),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Confirm acceptance" }));

    expect(mutate).toHaveBeenCalledWith(77, expect.any(Object));
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseAcceptAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(<AcceptAgreementProposalButton exchangeId={77} />);

    expect(
      screen.getByRole("button", { name: "Accepting proposal..." }),
    ).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <AcceptAgreementProposalButton
        exchangeId={77}
        disabled
        unavailableReason="Agreement exchange is not awaiting Client confirmation."
      />,
    );

    expect(
      screen.getByText("Agreement exchange is not awaiting Client confirmation."),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Accept proposal" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseAcceptAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Agreement proposal cannot be accepted."),
    });

    render(<AcceptAgreementProposalButton exchangeId={77} />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Agreement proposal cannot be accepted.",
    );
  });
});
