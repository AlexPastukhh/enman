/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { StartAgreementExchangeForm } from "./StartAgreementExchangeForm";

const {
  mockedUseStartAgreementExchangeMutation,
  mockedUploadAgreementProposalDocument,
} = vi.hoisted(() => ({
  mockedUseStartAgreementExchangeMutation: vi.fn(),
  mockedUploadAgreementProposalDocument: vi.fn(),
}));

vi.mock("../model/useStartAgreementExchangeMutation", () => ({
  useStartAgreementExchangeMutation: mockedUseStartAgreementExchangeMutation,
  __esModule: true,
}));

vi.mock("../../upload-document/api/uploadAgreementProposalDocument", () => ({
  uploadAgreementProposalDocument: mockedUploadAgreementProposalDocument,
  __esModule: true,
}));

describe("StartAgreementExchangeForm", () => {
  const mutate = vi.fn();
  const uploadedDocumentRef = {
    storageKey: "agreements/42/initial.pdf",
    originalFileName: "initial.pdf",
    contentType: "application/pdf",
    sizeBytes: 4096,
  };

  beforeEach(() => {
    mockedUseStartAgreementExchangeMutation.mockReturnValue({
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
    mockedUseStartAgreementExchangeMutation.mockReset();
    mockedUploadAgreementProposalDocument.mockReset();
  });

  it("uploads document before submitting start exchange command", async () => {
    const user = userEvent.setup();
    const file = new File(["initial"], "initial.pdf", {
      type: "application/pdf",
    });

    render(<StartAgreementExchangeForm requestId={42} />);

    await user.upload(screen.getByLabelText("Initial proposal document"), file);
    await user.type(screen.getByLabelText("Initial proposal comment"), " First proposal. ");
    await user.click(screen.getByRole("button", { name: "Start agreement exchange" }));

    await waitFor(() => {
      expect(mockedUploadAgreementProposalDocument).toHaveBeenCalledWith({
        document: file,
      });
    });
    expect(mutate).toHaveBeenCalledWith(
      {
        requestId: 42,
        proposal: {
          document: uploadedDocumentRef,
          comment: "First proposal.",
        },
      },
      expect.any(Object),
    );
  });

  it("shows validation feedback when document is missing", async () => {
    const user = userEvent.setup();

    render(<StartAgreementExchangeForm requestId={42} />);

    await user.click(screen.getByRole("button", { name: "Start agreement exchange" }));

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Choose an initial proposal document.",
    );
    expect(mockedUploadAgreementProposalDocument).not.toHaveBeenCalled();
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

    expect(mockedUploadAgreementProposalDocument).not.toHaveBeenCalled();
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
    expect(screen.getByLabelText("Initial proposal document")).toBeDisabled();
    expect(screen.getByRole("alert")).toHaveTextContent(
      "Agreement exchange already exists.",
    );
  });
});
