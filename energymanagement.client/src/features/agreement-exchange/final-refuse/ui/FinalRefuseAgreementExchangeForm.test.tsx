/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { FinalRefuseAgreementExchangeForm } from "./FinalRefuseAgreementExchangeForm";

const { mockedUseFinalRefuseAgreementExchangeMutation } = vi.hoisted(() => ({
  mockedUseFinalRefuseAgreementExchangeMutation: vi.fn(),
}));

vi.mock("../model/useFinalRefuseAgreementExchangeMutation", () => ({
  useFinalRefuseAgreementExchangeMutation:
    mockedUseFinalRefuseAgreementExchangeMutation,
  __esModule: true,
}));

describe("FinalRefuseAgreementExchangeForm", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseFinalRefuseAgreementExchangeMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseFinalRefuseAgreementExchangeMutation.mockReset();
  });

  it("submits final refusal without payload when reason is empty", async () => {
    const user = userEvent.setup();

    render(<FinalRefuseAgreementExchangeForm exchangeId={77} requestId={10} />);

    await user.click(screen.getByRole("button", { name: "Final refuse" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        exchangeId: 77,
        requestId: 10,
        payload: undefined,
      },
      expect.any(Object),
    );
  });

  it("submits final refusal with trimmed reason", async () => {
    const user = userEvent.setup();

    render(<FinalRefuseAgreementExchangeForm exchangeId={77} requestId={10} />);

    await user.type(
      screen.getByLabelText("Final refusal reason"),
      "  Cannot agree on terms.  ",
    );
    await user.click(screen.getByRole("button", { name: "Final refuse" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        exchangeId: 77,
        requestId: 10,
        payload: { reason: "Cannot agree on terms." },
      },
      expect.any(Object),
    );
  });

  it("shows validation feedback for whitespace-only reason", async () => {
    const user = userEvent.setup();

    render(<FinalRefuseAgreementExchangeForm exchangeId={77} />);

    await user.type(screen.getByLabelText("Final refusal reason"), "   ");
    await user.click(screen.getByRole("button", { name: "Final refuse" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Enter a reason or leave the field empty.",
    );
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows confirmation step when confirmation is required", async () => {
    const user = userEvent.setup();

    render(
      <FinalRefuseAgreementExchangeForm exchangeId={77} requireConfirmation />,
    );

    await user.click(screen.getByRole("button", { name: "Final refuse" }));

    expect(
      screen.getByRole("group", { name: "Final refusal confirmation" }),
    ).toBeVisible();

    await user.click(
      screen.getByRole("button", { name: "Confirm final refusal" }),
    );

    expect(mutate).toHaveBeenCalledWith(
      {
        exchangeId: 77,
        requestId: undefined,
        payload: undefined,
      },
      expect.any(Object),
    );
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <FinalRefuseAgreementExchangeForm
        exchangeId={77}
        disabled
        unavailableReason="Agreement exchange is already completed."
      />,
    );

    expect(screen.getByText("Agreement exchange is already completed.")).toBeVisible();
    await user.click(screen.getByRole("button", { name: "Final refuse" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows pending state and command error feedback", () => {
    mockedUseFinalRefuseAgreementExchangeMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: true,
      error: new Error("Exchange cannot be finally refused."),
    });

    render(<FinalRefuseAgreementExchangeForm exchangeId={77} />);

    expect(
      screen.getByRole("button", { name: "Refusing exchange..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Final refusal reason")).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent(
      "Exchange cannot be finally refused.",
    );
  });
});
