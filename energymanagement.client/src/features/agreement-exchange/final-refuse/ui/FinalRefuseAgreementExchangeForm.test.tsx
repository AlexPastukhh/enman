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

    await user.click(screen.getByRole("button", { name: "Финально отказаться" }));

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
      screen.getByLabelText("Причина финального отказа"),
      "  Cannot agree on terms.  ",
    );
    await user.click(screen.getByRole("button", { name: "Финально отказаться" }));

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

    await user.type(screen.getByLabelText("Причина финального отказа"), "   ");
    await user.click(screen.getByRole("button", { name: "Финально отказаться" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Введите причину или оставьте поле пустым.",
    );
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows confirmation step when confirmation is required", async () => {
    const user = userEvent.setup();

    render(
      <FinalRefuseAgreementExchangeForm exchangeId={77} requireConfirmation />,
    );

    await user.click(screen.getByRole("button", { name: "Финально отказаться" }));

    expect(
      screen.getByRole("group", { name: "Подтверждение финального отказа" }),
    ).toBeVisible();

    await user.click(
      screen.getByRole("button", { name: "Подтвердить финальный отказ" }),
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
        unavailableReason="Договорной обмен уже завершён."
      />,
    );

    expect(screen.getByText("Договорной обмен уже завершён.")).toBeVisible();
    await user.click(screen.getByRole("button", { name: "Финально отказаться" }));

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
      screen.getByRole("button", { name: "Отказываемся от обмена..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Причина финального отказа")).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent(
      "Exchange cannot be finally refused.",
    );
  });
});
