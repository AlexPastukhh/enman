/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { SendAgreementProposalForm } from "./SendAgreementProposalForm";

const { mockedUseSendAgreementProposalMutation } = vi.hoisted(() => ({
  mockedUseSendAgreementProposalMutation: vi.fn(),
}));

vi.mock("../model/useSendAgreementProposalMutation", () => ({
  useSendAgreementProposalMutation: mockedUseSendAgreementProposalMutation,
  __esModule: true,
}));

describe("SendAgreementProposalForm", () => {
  const mutate = vi.fn();

  beforeEach(() => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseSendAgreementProposalMutation.mockReset();
  });

  it("submits proposal version command with document reference", async () => {
    const user = userEvent.setup();

    render(<SendAgreementProposalForm exchangeId={77} viewerRole="Client" />);

    await user.type(screen.getByLabelText("Storage key"), "agreements/77/v2.pdf");
    await user.type(screen.getByLabelText("Original file name"), "proposal-v2.pdf");
    await user.clear(screen.getByLabelText("Content type"));
    await user.type(screen.getByLabelText("Content type"), "application/pdf");
    await user.type(screen.getByLabelText("Size bytes"), "2048");
    await user.type(screen.getByLabelText("Comment"), "Updated connection terms.");
    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    expect(mutate).toHaveBeenCalledWith(
      {
        exchangeId: 77,
        proposal: {
          document: {
            storageKey: "agreements/77/v2.pdf",
            originalFileName: "proposal-v2.pdf",
            contentType: "application/pdf",
            sizeBytes: 2048,
          },
          comment: "Updated connection terms.",
        },
      },
      expect.any(Object),
    );
  });

  it("shows validation feedback and does not submit incomplete document reference", async () => {
    const user = userEvent.setup();

    render(<SendAgreementProposalForm exchangeId={77} viewerRole="Employee" />);

    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Fill in document storage key, file name, content type and positive size.",
    );
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(<SendAgreementProposalForm exchangeId={77} viewerRole="Client" />);

    expect(
      screen.getByRole("button", { name: "Sending proposal..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Storage key")).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        viewerRole="Client"
        disabled
        unavailableReason="Waiting for the other party to respond."
      />,
    );

    expect(screen.getByText("Waiting for the other party to respond.")).toBeVisible();
    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Proposal cannot be sent."),
    });

    render(<SendAgreementProposalForm exchangeId={77} viewerRole="Client" />);

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Proposal cannot be sent.",
    );
  });
});
