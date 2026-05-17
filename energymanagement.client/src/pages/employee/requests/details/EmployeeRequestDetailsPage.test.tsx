/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import { ApiError } from "../../../../shared/api/fetchJson";
import EmployeeRequestDetailsPage from "./EmployeeRequestDetailsPage";

const { mockedUseSession, mockedUseEmployeeRequestDetailsQuery } = vi.hoisted(
  () => ({
    mockedUseSession: vi.fn(),
    mockedUseEmployeeRequestDetailsQuery: vi.fn(),
  }),
);

vi.mock("../../../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../../../entities/employee-request/model/useEmployeeRequestDetailsQuery",
  () => ({
    useEmployeeRequestDetailsQuery: mockedUseEmployeeRequestDetailsQuery,
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

vi.mock("../../../../features/employee-request/start-review/ui/StartReviewButton", () => ({
  StartReviewButton: ({ disabled }: { disabled?: boolean }) => (
    <button type="button" disabled={disabled}>
      Start review
    </button>
  ),
  __esModule: true,
}));

vi.mock("../../../../features/employee-request/approve-review/ui/ApproveReviewButton", () => ({
  ApproveReviewButton: ({ disabled }: { disabled?: boolean }) => (
    <button type="button" disabled={disabled}>
      Approve review
    </button>
  ),
  __esModule: true,
}));

const renderPage = (initialEntry = "/employee/requests/42") =>
  render(
    <MemoryRouter initialEntries={[initialEntry]}>
      <Routes>
        <Route
          path="/employee/requests/:requestId"
          element={<EmployeeRequestDetailsPage />}
        />
      </Routes>
    </MemoryRouter>,
  );

describe("EmployeeRequestDetailsPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 10,
      email: "employee@example.com",
      role: "Employee",
      isActive: true,
      isAuthenticated: true,
    });
    mockedUseEmployeeRequestDetailsQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        requestId: 42,
        requestType: "Connection",
        status: "InReview",
        applicant: {
          applicantPartyId: 7,
          applicantPartyType: "Individual",
          displayName: "Ivan Petrov",
          email: "ivan@example.com",
          phoneNumber: "+79001234567",
        },
        objectAddress: "Altai Krai, Zarinsk, Lenina 10",
        details: "Please connect the object to the grid.",
        createdAt: "2026-01-02T10:30:00Z",
        reviewState: "NotStarted",
      },
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseEmployeeRequestDetailsQuery.mockReset();
  });

  it("loads and renders Employee request details for Employee session", () => {
    renderPage();

    expect(mockedUseEmployeeRequestDetailsQuery).toHaveBeenCalledWith({
      requestId: 42,
      enabled: true,
    });
    expect(
      screen.getByRole("heading", { name: "Employee Request Details" }),
    ).toBeVisible();
    expect(screen.getByRole("heading", { name: "Request #42" })).toBeVisible();
    expect(screen.getByText("Start review is available.")).toBeVisible();
    expect(screen.getByRole("button", { name: "Start review" })).toBeVisible();
    expect(screen.getByRole("button", { name: "Approve review" })).toBeDisabled();
  });


  it("renders Approve action when review is started by current Employee", () => {
    mockedUseEmployeeRequestDetailsQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        requestId: 42,
        requestType: "Connection",
        status: "InReview",
        applicant: {
          applicantPartyId: 7,
          applicantPartyType: "Individual",
          displayName: "Ivan Petrov",
          email: "ivan@example.com",
          phoneNumber: "+79001234567",
        },
        objectAddress: "Altai Krai, Zarinsk, Lenina 10",
        details: "Please connect the object to the grid.",
        createdAt: "2026-01-02T10:30:00Z",
        reviewState: "StartedByCurrentEmployee",
      },
      error: null,
    });

    renderPage();

    expect(screen.getByText("Approve is available after review is started by current Employee.")).toBeVisible();
    expect(screen.getByRole("button", { name: "Approve review" })).toBeEnabled();
    expect(screen.getByRole("button", { name: "Start review" })).toBeDisabled();
  });

  it("shows sign-in state without session", () => {
    mockedUseSession.mockReturnValue(null);

    renderPage();

    expect(
      screen.getByText("Sign in as Employee to view request details"),
    ).toBeVisible();
    expect(mockedUseEmployeeRequestDetailsQuery).toHaveBeenCalledWith({
      requestId: 42,
      enabled: false,
    });
  });

  it("shows access state for non-Employee session", () => {
    mockedUseSession.mockReturnValue({
      accountId: 11,
      email: "client@example.com",
      role: "Client",
      isActive: true,
      isAuthenticated: true,
    });

    renderPage();

    expect(screen.getByText("Employee access required")).toBeVisible();
    expect(mockedUseEmployeeRequestDetailsQuery).toHaveBeenCalledWith({
      requestId: 42,
      enabled: false,
    });
  });

  it("shows not-found state for invalid route id", () => {
    renderPage("/employee/requests/not-a-number");

    expect(screen.getByText("Employee request details not found.")).toBeVisible();
    expect(mockedUseEmployeeRequestDetailsQuery).toHaveBeenCalledWith({
      requestId: 0,
      enabled: false,
    });
  });

  it("shows not-found state for 404 response", () => {
    mockedUseEmployeeRequestDetailsQuery.mockReturnValue({
      isPending: false,
      isError: true,
      data: undefined,
      error: new ApiError(404, null),
    });

    renderPage();

    expect(screen.getByText("Employee request details not found.")).toBeVisible();
  });
});
