/**
 * @vitest environment jsdom
 */
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { cleanup, render, screen, within } from "@testing-library/react";
import type { ReactNode } from "react";
import { MemoryRouter } from "react-router-dom";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { SessionProvider } from "../../../entities/session/model/SessionProvider";
import { PageErrorProvider } from "../../errors/pageErrorContext";
import { Header } from "./Header";
import { headerConst } from "./headerConst";
import { LayoutWrapper } from "./LayoutWrapper";

const { mockedGetCurrentSession } = vi.hoisted(() => ({
  mockedGetCurrentSession: vi.fn(),
}));

vi.mock("../../../entities/session/api/getCurrentSession", () => ({
  getCurrentSession: mockedGetCurrentSession,
}));

vi.mock("../../../assets/icons/svgr_barrel", () => ({
  LogoIcon: () => null,
  PhoneIcon: () => null,
}));

const clientSession = {
  accountId: 1,
  email: "client@example.com",
  role: "Client",
  isActive: true,
  isAuthenticated: true,
};

const employeeSession = {
  accountId: 2,
  email: "employee@example.com",
  role: "Employee",
  isActive: true,
  isAuthenticated: true,
};

const adminSession = {
  accountId: 3,
  email: "admin@example.com",
  role: "Admin",
  isActive: true,
  isAuthenticated: true,
};

const renderWithProviders = (ui: ReactNode, initialPath = "/") => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false },
      mutations: { retry: false },
    },
  });

  render(
    <QueryClientProvider client={queryClient}>
      <PageErrorProvider>
        <SessionProvider>
          <MemoryRouter initialEntries={[initialPath]}>{ui}</MemoryRouter>
        </SessionProvider>
      </PageErrorProvider>
    </QueryClientProvider>,
  );
};

const getNavigation = () => screen.getByRole("navigation");

describe("Header", () => {
  beforeEach(() => {
    mockedGetCurrentSession.mockReset();
  });

  afterEach(() => {
    cleanup();
  });

  it("shows only guest navigation for guest users", async () => {
    mockedGetCurrentSession.mockResolvedValueOnce(null);

    renderWithProviders(<Header />);

    const navigation = getNavigation();

    expect(
      await within(navigation).findByRole("link", {
        name: headerConst.loginLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("link", {
        name: headerConst.registerLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.createRequestLinkText,
      }),
    ).not.toBeInTheDocument();
    expect(
      within(navigation).queryByRole("button", {
        name: headerConst.logoutButtonText,
      }),
    ).not.toBeInTheDocument();
  });

  it("shows client navigation for client users", async () => {
    mockedGetCurrentSession.mockResolvedValueOnce(clientSession);

    renderWithProviders(<Header />);

    const navigation = getNavigation();

    expect(
      await within(navigation).findByRole("link", {
        name: headerConst.createRequestLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("link", {
        name: headerConst.myRequestsLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("link", {
        name: headerConst.agreementExchangesLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("link", {
        name: headerConst.accountLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("button", {
        name: headerConst.logoutButtonText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.loginLinkText,
      }),
    ).not.toBeInTheDocument();
  });

  it("shows employee navigation for employee users", async () => {
    mockedGetCurrentSession.mockResolvedValueOnce(employeeSession);

    renderWithProviders(<Header />);

    const navigation = getNavigation();

    expect(
      await within(navigation).findByRole("link", {
        name: headerConst.employeeRequestsLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).getByRole("link", {
        name: headerConst.employeeAgreementExchangesLinkText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.accountLinkText,
      }),
    ).not.toBeInTheDocument();
    expect(
      within(navigation).getByRole("button", {
        name: headerConst.logoutButtonText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.createRequestLinkText,
      }),
    ).not.toBeInTheDocument();
  });

  it("shows only logout navigation for admin users", async () => {
    mockedGetCurrentSession.mockResolvedValueOnce(adminSession);

    renderWithProviders(<Header />);

    const navigation = getNavigation();

    expect(
      await within(navigation).findByRole("button", {
        name: headerConst.logoutButtonText,
      }),
    ).toBeVisible();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.accountLinkText,
      }),
    ).not.toBeInTheDocument();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.createRequestLinkText,
      }),
    ).not.toBeInTheDocument();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.employeeRequestsLinkText,
      }),
    ).not.toBeInTheDocument();
  });

  it("does not show guest navigation while the session is loading", () => {
    mockedGetCurrentSession.mockImplementation(() => new Promise(() => {}));

    renderWithProviders(<Header />);

    const navigation = getNavigation();

    expect(within(navigation).getByRole("status")).toHaveTextContent(
      headerConst.sessionLoadingText,
    );
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.loginLinkText,
      }),
    ).not.toBeInTheDocument();
    expect(
      within(navigation).queryByRole("link", {
        name: headerConst.registerLinkText,
      }),
    ).not.toBeInTheDocument();
  });

  it("renders shell page content once", async () => {
    mockedGetCurrentSession.mockResolvedValueOnce(null);

    renderWithProviders(
      <LayoutWrapper>
        <main>
          <h1>Shell page content</h1>
        </main>
      </LayoutWrapper>,
    );

    expect(
      await screen.findByRole("heading", { name: "Shell page content" }),
    ).toBeVisible();
    expect(
      screen.getAllByRole("heading", { name: "Shell page content" }),
    ).toHaveLength(1);
    expect(screen.getByRole("contentinfo")).toBeVisible();
  });

  it("holds protected route content while the session is loading", () => {
    mockedGetCurrentSession.mockImplementation(() => new Promise(() => {}));

    renderWithProviders(
      <LayoutWrapper>
        <main>
          <h1>Protected account content</h1>
        </main>
      </LayoutWrapper>,
      "/account",
    );

    expect(
      screen.queryByRole("heading", { name: "Protected account content" }),
    ).not.toBeInTheDocument();
    expect(
      screen
        .getAllByRole("status")
        .some((status) =>
          status.textContent?.includes(headerConst.sessionLoadingText),
        ),
    ).toBe(true);
  });
});
