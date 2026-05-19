/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { RejectReviewForm } from "./RejectReviewForm";

const { mockedUseRejectRequestReviewMutation } = vi.hoisted(() => ({
  mockedUseRejectRequestReviewMutation: vi.fn(),
}));

vi.mock("../model/useRejectRequestReviewMutation", () => ({
  useRejectRequestReviewMutation: mockedUseRejectRequestReviewMutation,
  __esModule: true,
}));

describe("RejectReviewForm", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseRejectRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseRejectRequestReviewMutation.mockReset();
  });

  it("submits RejectReview command with feedback for the given request", async () => {
    const user = userEvent.setup();

    render(<RejectReviewForm requestId={42} />);

    await user.type(
      screen.getByLabelText("Причина отклонения"),
      "Applicant must provide additional documents.",
    );
    await user.click(screen.getByRole("button", { name: "Отклонить заявку" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        requestId: 42,
        feedback: "Applicant must provide additional documents.",
      },
      expect.any(Object),
    );
  });

  it("does not block submitting without feedback", async () => {
    const user = userEvent.setup();

    render(<RejectReviewForm requestId={42} showEmptyFeedbackWarning />);

    expect(
      screen.getByText(
        "Причина отклонения не указана. Заявку всё равно можно отклонить без комментария.",
      ),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Отклонить заявку" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        requestId: 42,
        feedback: "",
      },
      expect.any(Object),
    );
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseRejectRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(<RejectReviewForm requestId={42} />);

    expect(
      screen.getByRole("button", { name: "Отклоняем заявку..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Причина отклонения")).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <RejectReviewForm
        requestId={42}
        disabled
        unavailableReason="Другой сотрудник уже начал рассмотрение этой заявки."
      />,
    );

    expect(
      screen.getByText("Другой сотрудник уже начал рассмотрение этой заявки."),
    ).toBeVisible();

    await user.click(screen.getByRole("button", { name: "Отклонить заявку" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseRejectRequestReviewMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Review cannot be rejected."),
    });

    render(<RejectReviewForm requestId={42} />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Review cannot be rejected.",
    );
  });
});
