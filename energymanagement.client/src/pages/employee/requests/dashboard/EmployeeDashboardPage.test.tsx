/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import EmployeeDashboardPage from "./EmployeeDashboardPage";

const { mockedUseSession, mockedUseEmployeeRequestDashboardQuery } = vi.hoisted(
  () => ({
    mockedUseSession: vi.fn(),
    mockedUseEmployeeRequestDashboardQuery: vi.fn(),
  }),
);

vi.mock("../../../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../../../entities/employee-request/model/useEmployeeRequestDashboardQuery",
  () => ({
    useEmployeeRequestDashboardQuery: mockedUseEmployeeRequestDashboardQuery,
    __esModule: true,
  }),
);

vi.mock("../../../../features/employee-request/start-review/ui/StartReviewButton", () => ({
  StartReviewButton: ({ requestId }: { requestId: number }) => (
    <button type="button">Start review #{requestId}</button>
  ),
  __esModule: true,
}));

vi.mock("../../../../shared/ui/layout/Header", () => ({
  Header: () => <header>Header</header>,
  __esModule: true,
}));

vi.mock("../../../../shared/ui/layout/Footer", () => ({
  Footer: () => <footer>Footer</footer>,
  __esModule: true,
}));

const renderPage = (initialEntry = "/employee/requests") =>
  render(
    <MemoryRouter initialEntries={[initialEntry]}>
      <Routes>
        <Route path="/employee/requests" element={<EmployeeDashboardPage />} />
      </Routes>
    </MemoryRouter>,
  );

describe("EmployeeDashboardPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 10,
      email: "employee@example.com",
      role: "Employee",
      isActive: true,
      isAuthenticated: true,
    });
    mockedUseEmployeeRequestDashboardQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: [
        {
          requestId: 42,
          requestType: "Connection",
          status: "InReview",
          applicantDisplayName: "Ivan Petrov",
          objectAddress: "Altai Krai, Zarinsk, Lenina 10",
          createdAt: "2026-01-02T10:30:00Z",
          reviewState: "NotStarted",
        },
        {
          requestId: 43,
          requestType: "Connection",
          status: "InReview",
          applicantDisplayName: "Petr Ivanov",
          objectAddress: "Altai Krai, Barnaul, Lenina 11",
          createdAt: "2026-01-03T10:30:00Z",
          reviewState: "StartedByAnotherEmployee",
        },
      ],
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseEmployeeRequestDashboardQuery.mockReset();
  });

  it("hosts Start Review action for not-started InReview dashboard rows only", () => {
    renderPage();

    expect(screen.getByRole("heading", { name: "Request #42" })).toBeVisible();
    expect(screen.getByRole("button", { name: "Start review #42" })).toBeVisible();
    expect(
      screen.queryByRole("button", { name: "Start review #43" }),
    ).not.toBeInTheDocument();
  });
});
