/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { SendAgreementProposalForm } from "./SendAgreementProposalForm";

const {
  mockedUseSendAgreementProposalMutation,
  mockedUploadAgreementProposalDocument,
} = vi.hoisted(() => ({
  mockedUseSendAgreementProposalMutation: vi.fn(),
  mockedUploadAgreementProposalDocument: vi.fn(),
}));

vi.mock("../model/useSendAgreementProposalMutation", () => ({
  useSendAgreementProposalMutation: mockedUseSendAgreementProposalMutation,
  __esModule: true,
}));

vi.mock("../../upload-document/api/uploadAgreementProposalDocument", () => ({
  uploadAgreementProposalDocument: mockedUploadAgreementProposalDocument,
  __esModule: true,
}));

describe("SendAgreementProposalForm", () => {
  const mutate = vi.fn();
  const uploadedDocumentRef = {
    storageKey: "agreements/77/v2.pdf",
    originalFileName: "proposal-v2.pdf",
    contentType: "application/pdf",
    sizeBytes: 2048,
  };

  beforeEach(() => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: false,
      error: null,
    });
    mockedUploadAgreementProposalDocument.mockResolvedValue(uploadedDocumentRef);
  });

  afterEach(() => {
    cleanup();
    mutate.mockReset();
    mockedUseSendAgreementProposalMutation.mockReset();
    mockedUploadAgreementProposalDocument.mockReset();
  });

  it("uploads document before submitting proposal version command", async () => {
    const user = userEvent.setup();
    const file = new File(["proposal"], "proposal-v2.pdf", {
      type: "application/pdf",
    });

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        requestId={42}
        viewerRole="Client"
      />,
    );

    await user.upload(screen.getByLabelText("Proposal document"), file);
    await user.type(screen.getByLabelText("Comment"), "Updated connection terms.");
    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    await waitFor(() => {
      expect(mockedUploadAgreementProposalDocument).toHaveBeenCalledWith({
        document: file,
      });
    });
    expect(mutate).toHaveBeenCalledWith(
      {
        exchangeId: 77,
        requestId: 42,
        proposal: {
          document: uploadedDocumentRef,
          comment: "Updated connection terms.",
        },
      },
      expect.any(Object),
    );
  });

  it("shows validation feedback and does not submit when document is missing", async () => {
    const user = userEvent.setup();

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        requestId={42}
        viewerRole="Employee"
      />,
    );

    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Choose a proposal document.",
    );
    expect(mockedUploadAgreementProposalDocument).not.toHaveBeenCalled();
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows pending state and disables duplicate submit", () => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: true,
      isError: false,
      error: null,
    });

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        requestId={42}
        viewerRole="Client"
      />,
    );

    expect(
      screen.getByRole("button", { name: "Sending proposal..." }),
    ).toBeDisabled();
    expect(screen.getByLabelText("Proposal document")).toBeDisabled();
  });

  it("shows unavailable reason and does not submit when disabled", async () => {
    const user = userEvent.setup();

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        requestId={42}
        viewerRole="Client"
        disabled
        unavailableReason="Waiting for the other party to respond."
      />,
    );

    expect(screen.getByText("Waiting for the other party to respond.")).toBeVisible();
    await user.click(screen.getByRole("button", { name: "Send proposal version" }));

    expect(mockedUploadAgreementProposalDocument).not.toHaveBeenCalled();
    expect(mutate).not.toHaveBeenCalled();
  });

  it("shows command error feedback", () => {
    mockedUseSendAgreementProposalMutation.mockReturnValue({
      mutate,
      isPending: false,
      isError: true,
      error: new Error("Proposal cannot be sent."),
    });

    render(
      <SendAgreementProposalForm
        exchangeId={77}
        requestId={42}
        viewerRole="Client"
      />,
    );

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Proposal cannot be sent.",
    );
  });
});
