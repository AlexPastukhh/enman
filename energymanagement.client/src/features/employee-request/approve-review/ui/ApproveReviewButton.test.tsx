/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { ApproveReviewButton } from "./ApproveReviewButton";

const { mockedUseApproveRequestReviewMutation } = vi.hoisted(() => ({
  mockedUseApproveRequestReviewMutation: vi.fn(),
}));

vi.mock("../model/useApproveRequestReviewMutation", () => ({
  useApproveRequestReviewMutation: mockedUseApproveRequestReviewMutation,
  __esModule: true,
}));

describe("ApproveReviewButton", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseApproveRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseApproveRequestReviewMutation.mockReset();
  });

  it("submits ApproveReview command for the given request", async () => {
    const user = userEvent.setup();

    render(<ApproveReviewButton requestId={42} />);

    await user.click(screen.getByRole("button", { name: "Одобрить заявку" }));

    expect(mutate).toHaveBeenCalledWith(42, expect.any(Object));
  });

  it("can require confirmation before submitting", async () => {
    const user = userEvent.setup();

    render(<ApproveReviewButton requestId={42} requireConfirmation />);

    await user.click(screen.getByRole("button", { name: "Одобрить заявку" }));

    expect(mutate).not.toHaveBeenCalled();
    expect(screen.getByText(/Одобрение является итоговым положительным решением/i)).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Подтвердить одобрение" }));

    expect(mutate).toHaveBeenCalledWith(42, expect.any(Object));
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseApproveRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(<ApproveReviewButton requestId={42} />);

    expect(
      screen.getByRole("button", { name: "Одобряем заявку..." }),
    ).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <ApproveReviewButton
        requestId={42}
        disabled
        unavailableReason="Другой сотрудник уже начал рассмотрение этой заявки."
      />,
    );

    expect(
      screen.getByText("Другой сотрудник уже начал рассмотрение этой заявки."),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Одобрить заявку" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseApproveRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Review cannot be approved."),
    });

    render(<ApproveReviewButton requestId={42} />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Review cannot be approved.",
    );
  });
});
