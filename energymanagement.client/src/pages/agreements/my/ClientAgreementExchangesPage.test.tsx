/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import ClientAgreementExchangesPage from "./ClientAgreementExchangesPage";

const { mockedUseSession, mockedUseAgreementExchangeListQuery } = vi.hoisted(
  () => ({
    mockedUseSession: vi.fn(),
    mockedUseAgreementExchangeListQuery: vi.fn(),
  }),
);

vi.mock("../../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../../entities/agreement-exchange/model/useAgreementExchangeListQuery",
  () => ({
    useAgreementExchangeListQuery: mockedUseAgreementExchangeListQuery,
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

const renderPage = () =>
  render(
    <MemoryRouter initialEntries={["/agreements"]}>
      <Routes>
        <Route path="/agreements" element={<ClientAgreementExchangesPage />} />
      </Routes>
    </MemoryRouter>,
  );

describe("ClientAgreementExchangesPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 7,
      email: "client@example.com",
      role: "Client",
      isActive: true,
      isAuthenticated: true,
    });
    mockedUseAgreementExchangeListQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: [
        {
          requestId: 10,
          exchangeId: 20,
          exchangeStatus: "AwaitingClientConfirmation",
          activeProposalVersion: 1,
          activeProposalSender: "Employee",
          activeProposalSenderId: 5,
          requestDisplayName: "Connection request #10",
          objectAddress: "Altai Krai, Barnaul, Lenina 10",
          createdAt: "2026-01-01T10:00:00Z",
          lastActivityAt: "2026-01-02T10:00:00Z",
        },
      ],
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseAgreementExchangeListQuery.mockReset();
  });

  it("renders client page shell over shared agreement exchange list", () => {
    renderPage();

    expect(screen.getByRole("heading", { name: "Мои договоры" })).toBeVisible();
    expect(
      mockedUseAgreementExchangeListQuery,
    ).toHaveBeenCalledWith({ enabled: true });
    expect(
      screen.getByRole("heading", { name: "Connection request #10" }),
    ).toBeVisible();
    expect(screen.getByRole("link", { name: "Open exchange details" })).toHaveAttribute(
      "href",
      "/agreements/20",
    );
  });

  it("blocks non-client sessions from client agreements page", () => {
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
    expect(
      mockedUseAgreementExchangeListQuery,
    ).toHaveBeenCalledWith({ enabled: false });
  });
});
