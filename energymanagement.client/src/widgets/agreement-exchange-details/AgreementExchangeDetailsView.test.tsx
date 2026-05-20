/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it } from "vitest";
import { AgreementExchangeDetailsView } from "./AgreementExchangeDetailsView";
import type { AgreementExchangeDetails } from "../../entities/agreement-exchange/model/agreementExchangeTypes";

const details: AgreementExchangeDetails = {
  exchangeId: 20,
  requestId: 10,
  exchangeStatus: "AwaitingClientConfirmation",
  activeProposalVersion: 2,
  request: {
    requestId: 10,
    requestStatus: "Approved",
    requestDisplayName: "Connection request #10",
    objectAddress: "Altai Krai, Barnaul, Lenina 10",
  },
  activeProposal: {
    proposalId: 200,
    version: 2,
    sender: "Employee",
    senderId: 5,
    state: "Active",
    document: {
      storageKey: "agreements/20/v2.pdf",
      originalFileName: "agreement-v2.pdf",
      contentType: "application/pdf",
      sizeBytes: 2048,
    },
    comment: "Initial agreement proposal",
    createdAt: "2026-01-02T10:00:00Z",
  },
  proposals: [
    {
      proposalId: 100,
      version: 1,
      sender: "Client",
      senderId: 7,
      state: "Superseded",
      document: null,
      comment: "Client draft",
      createdAt: "2026-01-01T10:00:00Z",
    },
  ],
  currentActorSide: "Client",
  createdAt: "2026-01-01T09:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

describe("AgreementExchangeDetailsView", () => {
  afterEach(() => {
    cleanup();
  });

  it("renders shared details, active proposal, proposal history and document refs", () => {
    render(<AgreementExchangeDetailsView details={details} viewerRole="Client" />);

    expect(screen.getByRole("heading", { name: "Согласование #20" })).toBeVisible();
    expect(screen.getByText("Ожидает подтверждения клиента")).toBeVisible();
    expect(screen.getByText("Connection request #10")).toBeVisible();
    expect(screen.getByRole("heading", { name: "Активное предложение" })).toBeVisible();
    expect(screen.getByRole("heading", { name: "Версия 2, автор: Сотрудник" })).toBeVisible();
    expect(screen.getByText("Сотрудник #5")).toBeVisible();
    expect(screen.getByText("agreement-v2.pdf (application/pdf, 2.0 КБ)")).toBeVisible();
    const downloadLink = screen.getByRole("link", {
      name: "Скачать документ: agreement-v2.pdf",
    });
    expect(downloadLink).toHaveAttribute(
      "href",
      "/api/agreement-exchanges/20/proposals/200/document/download",
    );
    expect(downloadLink).toHaveAttribute("download", "agreement-v2.pdf");
    expect(screen.getByRole("heading", { name: "История предложений" })).toBeVisible();
    expect(screen.getByRole("heading", { name: "Версия 1, автор: Клиент" })).toBeVisible();
  });

  it("renders optional action slot without owning command behavior", () => {
    render(
      <AgreementExchangeDetailsView
        details={details}
        viewerRole="Employee"
        renderActions={() => <button type="button">Future action</button>}
      />,
    );

    expect(
      screen.getByRole("heading", { name: "Доступные действия по согласованию" }),
    ).toBeVisible();
    expect(screen.getByRole("button", { name: "Future action" })).toBeVisible();
  });
});
