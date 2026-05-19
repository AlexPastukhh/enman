/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import ClientAgreementExchangeDetailsPage from "./ClientAgreementExchangeDetailsPage";

const { mockedUseSession, mockedUseAgreementExchangeDetailsQuery } = vi.hoisted(
  () => ({
    mockedUseSession: vi.fn(),
    mockedUseAgreementExchangeDetailsQuery: vi.fn(),
  }),
);

vi.mock("../../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../../entities/agreement-exchange/model/useAgreementExchangeDetailsQuery",
  () => ({
    useAgreementExchangeDetailsQuery: mockedUseAgreementExchangeDetailsQuery,
    __esModule: true,
  }),
);

vi.mock("../../../shared/ui/layout/Header", () => ({
  Header: () => <header>Header</header>,
  __esModule: true,
}));

vi.mock("../../../shared/ui/layout/Footer", () => ({
  Footer: () => <footer>Footer</footer>,
  __esModule: true,
}));

vi.mock("../../../features/agreement-exchange/accept-proposal/ui/AcceptAgreementProposalButton", () => ({
  AcceptAgreementProposalButton: ({ disabled }: { disabled?: boolean }) => (
    <div data-testid="accept-proposal-button">
      Принять предложение {disabled ? "недоступно" : "доступно"}
    </div>
  ),
  __esModule: true,
}));

vi.mock("../../../features/agreement-exchange/send-proposal/ui/SendAgreementProposalForm", () => ({
  SendAgreementProposalForm: ({
    disabled,
    requestId,
  }: {
    disabled?: boolean;
    requestId: number;
  }) => (
    <div data-testid="send-proposal-form">
      Форма отправки предложения {disabled ? "недоступна" : "доступна"} для заявки {requestId}
    </div>
  ),
  __esModule: true,
}));

const details = {
  exchangeId: 20,
  requestId: 10,
  exchangeStatus: "AwaitingClientConfirmation",
  activeProposalVersion: 1,
  request: {
    requestId: 10,
    requestStatus: "Approved",
    requestDisplayName: "Connection request #10",
    objectAddress: "Altai Krai, Barnaul, Lenina 10",
  },
  activeProposal: {
    proposalId: 200,
    version: 1,
    sender: "Employee",
    senderId: 10,
    state: "AwaitingClientConfirmation",
    document: null,
    comment: null,
    createdAt: "2026-01-02T10:00:00Z",
  },
  proposals: [],
  currentActorSide: "Client",
  createdAt: "2026-01-01T10:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

const renderPage = (path = "/agreements/20") =>
  render(
    <MemoryRouter initialEntries={[path]}>
      <Routes>
        <Route
          path="/agreements/:exchangeId"
          element={<ClientAgreementExchangeDetailsPage />}
        />
      </Routes>
    </MemoryRouter>,
  );

describe("ClientAgreementExchangeDetailsPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 7,
      email: "client@example.com",
      role: "Client",
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

  it("renders client details page shell over shared details view", () => {
    renderPage();

    expect(screen.getByRole("heading", { name: "Детали договора" })).toBeVisible();
    expect(mockedUseAgreementExchangeDetailsQuery).toHaveBeenCalledWith({
      exchangeId: 20,
      enabled: true,
    });
    expect(screen.getByRole("heading", { name: "Обмен #20" })).toBeVisible();
    expect(screen.getByRole("link", { name: "Вернуться к моим договорам" })).toHaveAttribute(
      "href",
      "/agreements",
    );
    expect(screen.getByTestId("accept-proposal-button")).toHaveTextContent(
      "Принять предложение доступно",
    );
    expect(screen.getByTestId("send-proposal-form")).toBeVisible();
    expect(screen.getByTestId("send-proposal-form")).toHaveTextContent(
      "для заявки 10",
    );
  });

  it("blocks non-client sessions from client details page", () => {
    mockedUseSession.mockReturnValue({
      accountId: 9,
      email: "employee@example.com",
      role: "Employee",
      isActive: true,
      isAuthenticated: true,
    });

    renderPage();

    expect(screen.getByRole("alert")).toHaveTextContent(
      "Эта страница доступна клиенту.",
    );
    expect(mockedUseAgreementExchangeDetailsQuery).toHaveBeenCalledWith({
      exchangeId: 20,
      enabled: false,
    });
  });
});
