/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import EmployeeAgreementExchangeDetailsPage from "./EmployeeAgreementExchangeDetailsPage";

const { mockedUseSession, mockedUseAgreementExchangeDetailsQuery } = vi.hoisted(
  () => ({
    mockedUseSession: vi.fn(),
    mockedUseAgreementExchangeDetailsQuery: vi.fn(),
  }),
);

vi.mock("../../../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../../../entities/agreement-exchange/model/useAgreementExchangeDetailsQuery",
  () => ({
    useAgreementExchangeDetailsQuery: mockedUseAgreementExchangeDetailsQuery,
    __esModule: true,
  }),
);

vi.mock("../../../../shared/ui/layout/Header", () => ({
  Header: () => <header>Header</header>,
  __esModule: true,
}));

vi.mock("../../../../shared/ui/layout/Footer", () => ({
  Footer: () => <footer>Footer</footer>,
  __esModule: true,
}));

vi.mock("../../../../features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm", () => ({
  SendAgreementProposalForm: ({ disabled }: { disabled?: boolean }) => (
    <div data-testid="send-proposal-form">
      Send proposal form {disabled ? "disabled" : "enabled"}
    </div>
  ),
  __esModule: true,
}));

vi.mock("../../../../features/agreement-exchange/final-refuse/ui/FinalRefuseAgreementExchangeForm", () => ({
  FinalRefuseAgreementExchangeForm: ({ disabled }: { disabled?: boolean }) => (
    <div data-testid="final-refuse-form">
      Final refuse form {disabled ? "disabled" : "enabled"}
    </div>
  ),
  __esModule: true,
}));

const details = {
  exchangeId: 20,
  requestId: 10,
  exchangeStatus: "AwaitingEmployeeResponse",
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
    sender: "Client",
    senderId: 7,
    state: "Active",
    document: null,
    comment: null,
    createdAt: "2026-01-02T10:00:00Z",
  },
  proposals: [],
  currentActorSide: "Employee",
  createdAt: "2026-01-01T10:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

const renderPage = (path = "/employee/agreements/20") =>
  render(
    <MemoryRouter initialEntries={[path]}>
      <Routes>
        <Route
          path="/employee/agreements/:exchangeId"
          element={<EmployeeAgreementExchangeDetailsPage />}
        />
      </Routes>
    </MemoryRouter>,
  );

describe("EmployeeAgreementExchangeDetailsPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 10,
      email: "employee@example.com",
      role: "Employee",
      isActive: true,
      isAuthenticated: true,
    });
    mockedUseAgreementExchangeDetailsQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: details,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseAgreementExchangeDetailsQuery.mockReset();
  });

  it("renders employee details page shell over shared details view", () => {
    renderPage();

    expect(
      screen.getByRole("heading", { name: "Agreement exchange details" }),
    ).toBeVisible();
    expect(mockedUseAgreementExchangeDetailsQuery).toHaveBeenCalledWith({
      exchangeId: 20,
      enabled: true,
    });
    expect(screen.getByRole("heading", { name: "Exchange #20" })).toBeVisible();
    expect(screen.getByRole("heading", { name: "Version 2 from Client" })).toBeVisible();
    expect(screen.getByRole("link", { name: "Back to agreement exchanges" })).toHaveAttribute(
      "href",
      "/employee/agreements",
    );
    expect(screen.getByTestId("send-proposal-form")).toBeVisible();
    expect(screen.getByTestId("final-refuse-form")).toHaveTextContent(
      "Final refuse form enabled",
    );
  });

  it("blocks non-employee sessions from employee details page", () => {
    mockedUseSession.mockReturnValue({
      accountId: 7,
      email: "client@example.com",
      role: "Client",
      isActive: true,
      isAuthenticated: true,
    });

    renderPage();

    expect(screen.getByRole("alert")).toHaveTextContent(
      "This page is available only to Employees.",
    );
    expect(mockedUseAgreementExchangeDetailsQuery).toHaveBeenCalledWith({
      exchangeId: 20,
      enabled: false,
    });
  });
});
