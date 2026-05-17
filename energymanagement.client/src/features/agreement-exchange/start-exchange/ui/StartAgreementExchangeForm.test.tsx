/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { StartAgreementExchangeForm } from "./StartAgreementExchangeForm";

const { mockedUseStartAgreementExchangeMutation } = vi.hoisted(() => ({
  mockedUseStartAgreementExchangeMutation: vi.fn(),
}));

vi.mock("../model/useStartAgreementExchangeMutation", () => ({
  useStartAgreementExchangeMutation: mockedUseStartAgreementExchangeMutation,
  __esModule: true,
}));

describe("StartAgreementExchangeForm", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseStartAgreementExchangeMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseStartAgreementExchangeMutation.mockReset();
  });

  it("submits start exchange command with initial proposal payload", async () => {
    const user = userEvent.setup();

    render(<StartAgreementExchangeForm requestId={42} />);

    await user.type(screen.getByLabelText("Document storage key"), " agreements/42/initial.pdf ");
    await user.type(screen.getByLabelText("Original file name"), " initial.pdf ");
    await user.clear(screen.getByLabelText("Content type"));
    await user.type(screen.getByLabelText("Content type"), " application/pdf ");
    await user.type(screen.getByLabelText("Size in bytes"), "4096");
    await user.type(screen.getByLabelText("Initial proposal comment"), " First proposal. ");
    await user.click(screen.getByRole("button", { name: "Start agreement exchange" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        requestId: 42,
        initialProposal: {
          document: {
            storageKey: "agreements/42/initial.pdf",
            originalFileName: "initial.pdf",
            contentType: "application/pdf",
            sizeBytes: 4096,
          },
          comment: "First proposal.",
        },
      },
      expect.any(Object),
    );
  });

  it("shows validation feedback when required document fields are missing", async () => {
    const user = userEvent.setup();

    render(<StartAgreementExchangeForm requestId={42} />);

    await user.click(screen.getByRole("button", { name: "Start agreement exchange" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Provide document storage key, original file name, content type and positive size.",
    );
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <StartAgreementExchangeForm
        requestId={42}
        disabled
        unavailableReason="Agreement exchange can be started only after approval."
      />,
    );

    expect(
      screen.getByText("Agreement exchange can be started only after approval."),
    ).toBeVisible();
    await user.click(screen.getByRole("button", { name: "Start agreement exchange" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows pending state and command error feedback", () => {
    mockedUseStartAgreementExchangeMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: true,
      error: new Error("Agreement exchange already exists."),
    });

    render(<StartAgreementExchangeForm requestId={42} />);

    expect(
      screen.getByRole("button", { name: "Starting exchange..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Document storage key")).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent(
      "Agreement exchange already exists.",
    );
  });
});
