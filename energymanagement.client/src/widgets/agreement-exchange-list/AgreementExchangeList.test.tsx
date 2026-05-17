/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it } from "vitest";
import { MemoryRouter } from "react-router-dom";
import { AgreementExchangeList } from "./AgreementExchangeList";
import type { AgreementExchangeListItem } from "../../entities/agreement-exchange/model/agreementExchangeTypes";

const exchanges: AgreementExchangeListItem[] = [
  {
    requestId: 10,
    exchangeId: 20,
    exchangeStatus: "AwaitingClientConfirmation",
    activeProposalVersion: 2,
    activeProposalSender: "Employee",
    activeProposalSenderId: 5,
    requestDisplayName: "Connection request #10",
    objectAddress: "Altai Krai, Barnaul, Lenina 10",
    createdAt: "2026-01-01T10:00:00Z",
    lastActivityAt: "2026-01-02T10:00:00Z",
  },
];

describe("AgreementExchangeList", () => {
  afterEach(() => {
    cleanup();
  });

  it("renders shared exchange row summary and actor-provided details href", () => {
    render(
      <MemoryRouter>
        <AgreementExchangeList
          exchanges={exchanges}
          viewerRole="Client"
          getDetailsHref={(exchange) => `/agreements/${exchange.exchangeId}`}
        />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("heading", { name: "Connection request #10" }),
    ).toBeVisible();
    expect(screen.getByText("Awaiting client confirmation")).toBeVisible();
    expect(screen.getByText("Version 2 from Employee")).toBeVisible();
    expect(screen.getByText("Altai Krai, Barnaul, Lenina 10")).toBeVisible();
    expect(screen.getByRole("link", { name: "Open exchange details" })).toHaveAttribute(
      "href",
      "/agreements/20",
    );
  });

  it("renders actor-provided empty state", () => {
    render(
      <MemoryRouter>
        <AgreementExchangeList
          exchanges={[]}
          viewerRole="Employee"
          emptyStateTitle="No exchanges need attention."
          emptyStateDescription="Employee-visible exchanges will appear here."
          getDetailsHref={(exchange) => `/employee/agreements/${exchange.exchangeId}`}
        />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("heading", { name: "No exchanges need attention." }),
    ).toBeVisible();
    expect(screen.getByText("Employee-visible exchanges will appear here.")).toBeVisible();
  });
});
