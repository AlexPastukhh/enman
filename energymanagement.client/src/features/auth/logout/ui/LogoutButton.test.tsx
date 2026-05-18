/**
 * @vitest environment jsdom
 */
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { cleanup, render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import {
  createMemoryRouter,
  RouterProvider,
  type RouteObject,
} from "react-router-dom";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { ApiError } from "../../../../shared/api/fetchJson";
import { Header } from "../../../../shared/ui/layout/Header";
import { SessionProvider } from "../../../../entities/session/model/SessionProvider";
import { headerConst } from "../../../../shared/ui/layout/headerConst";

vi.mock("*.svg?react", () => ({
  default: () => null,
}));

const { mockedGetCurrentUser, mockedLogoutClientAccount } = vi.hoisted(() => ({
  mockedGetCurrentUser: vi.fn(),
  mockedLogoutClientAccount: vi.fn(),
}));

vi.mock("../../../../shared/api/l1AuthApi", () => ({
  getCurrentUser: mockedGetCurrentUser,
  logoutClientAccount: mockedLogoutClientAccount,
  __esModule: true,
}));

const authenticatedUser = {
  accountId: 1,
  email: "client@example.com",
  role: "Client",
  isActive: true,
  isAuthenticated: true,
};

const routes: RouteObject[] = [
  {
    path: "/",
    element: (
      <>
        <Header />
        <main>Home public</main>
      </>
    ),
  },
  {
    path: "/account",
    element: (
      <>
        <Header />
        <main>Authenticated account</main>
      </>
    ),
  },
];

const renderApp = (initialPath = "/account") => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false },
      mutations: { retry: false },
    },
  });
  const router = createMemoryRouter(routes, {
    initialEntries: [initialPath],
  });

  render(
    <QueryClientProvider client={queryClient}>
      <SessionProvider>
        <RouterProvider router={router} />
      </SessionProvider>
    </QueryClientProvider>,
  );

  return { router };
};

describe("LogoutButton", () => {
  beforeEach(() => {
    mockedGetCurrentUser.mockReset();
    mockedLogoutClientAccount.mockReset();
  });

  afterEach(() => {
    cleanup();
  });

  it("is visible only for authenticated users", async () => {
    mockedGetCurrentUser.mockResolvedValueOnce(authenticatedUser);

    renderApp();

    expect(
      await screen.findByRole("button", { name: headerConst.logoutButtonText }),
    ).toBeVisible();
  });

  it("is hidden for guest state", async () => {
    mockedGetCurrentUser.mockRejectedValueOnce(new ApiError(401, null));

    renderApp("/");

    await waitFor(() => {
      expect(
        screen.queryByRole("button", { name: headerConst.logoutButtonText }),
      ).not.toBeInTheDocument();
    });
  });

  it("clears authenticated UI and navigates home after successful logout", async () => {
    mockedGetCurrentUser.mockResolvedValueOnce(authenticatedUser);
    mockedLogoutClientAccount.mockResolvedValueOnce(undefined);
    const { router } = renderApp();

    await userEvent.click(
      await screen.findByRole("button", { name: headerConst.logoutButtonText }),
    );

    await waitFor(() => {
      expect(router.state.location.pathname).toBe("/");
    });
    expect(await screen.findByText("Home public")).toBeVisible();
    expect(
      screen.queryByRole("button", { name: headerConst.logoutButtonText }),
    ).not.toBeInTheDocument();
  });

  it("treats logout 401 as already unauthenticated and navigates home", async () => {
    mockedGetCurrentUser.mockResolvedValueOnce(authenticatedUser);
    mockedLogoutClientAccount.mockRejectedValueOnce(new ApiError(401, null));
    const { router } = renderApp();

    await userEvent.click(
      await screen.findByRole("button", { name: headerConst.logoutButtonText }),
    );

    await waitFor(() => {
      expect(router.state.location.pathname).toBe("/");
    });
    expect(await screen.findByText("Home public")).toBeVisible();
    expect(
      screen.queryByRole("button", { name: headerConst.logoutButtonText }),
    ).not.toBeInTheDocument();
  });

  it("shows visible feedback and does not navigate on unexpected logout failure", async () => {
    mockedGetCurrentUser.mockResolvedValueOnce(authenticatedUser);
    mockedLogoutClientAccount.mockRejectedValueOnce(new Error("network"));
    const { router } = renderApp();

    await userEvent.click(
      await screen.findByRole("button", { name: headerConst.logoutButtonText }),
    );

    expect(await screen.findByRole("alert")).toHaveTextContent(
      "Something went wrong",
    );
    expect(router.state.location.pathname).toBe("/account");
    expect(screen.getByText("Authenticated account")).toBeVisible();
    expect(
      screen.getByRole("button", { name: headerConst.logoutButtonText }),
    ).toBeVisible();
  });
});
