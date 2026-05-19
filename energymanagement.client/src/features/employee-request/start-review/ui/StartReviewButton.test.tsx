/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { StartReviewButton } from "./StartReviewButton";

const { mockedUseStartRequestReviewMutation } = vi.hoisted(() => ({
  mockedUseStartRequestReviewMutation: vi.fn(),
}));

vi.mock("../model/useStartRequestReviewMutation", () => ({
  useStartRequestReviewMutation: mockedUseStartRequestReviewMutation,
  __esModule: true,
}));

describe("StartReviewButton", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseStartRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseStartRequestReviewMutation.mockReset();
  });

  it("submits StartReview command for the given request", async () => {
    const user = userEvent.setup();

    render(<StartReviewButton requestId={42} surface="details" />);

    await user.click(screen.getByRole("button", { name: "Начать рассмотрение" }));

    expect(mutate).toHaveBeenCalledWith(42, expect.any(Object));
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseStartRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(<StartReviewButton requestId={42} />);

    expect(
      screen.getByRole("button", { name: "Начинаем рассмотрение..." }),
    ).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <StartReviewButton
        requestId={42}
        disabled
        unavailableReason="Другой сотрудник уже начал рассмотрение этой заявки."
      />,
    );

    expect(
      screen.getByText("Другой сотрудник уже начал рассмотрение этой заявки."),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Начать рассмотрение" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseStartRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Review was already started."),
    });

    render(<StartReviewButton requestId={42} />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Review was already started.",
    );
  });
});
