/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import type { ReactElement } from "react";
import { MemoryRouter } from "react-router-dom";
import { afterEach, describe, expect, it, vi } from "vitest";
import { EmployeeRequestDashboardList } from "./EmployeeRequestDashboardList";

const renderWithRouter = (element: ReactElement) =>
  render(<MemoryRouter>{element}</MemoryRouter>);

describe("EmployeeRequestDashboardList", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows an empty state when there are no employee requests", () => {
    renderWithRouter(<EmployeeRequestDashboardList requests={[]} />);

    expect(screen.getByText("No employee requests to review.")).toBeVisible();
  });

  it("renders employee request row data and details link", () => {
    renderWithRouter(
      <EmployeeRequestDashboardList
        requests={[
          {
            requestId: 42,
            requestType: "Connection",
            status: "InReview",
            applicantDisplayName: "Ivan Petrov",
            objectAddress: "Altai Krai, Zarinsk, Lenina 10",
            createdAt: "2026-01-02T10:30:00Z",
            reviewState: "StartedByAnotherEmployee",
          },
        ]}
      />,
    );

    expect(screen.getByRole("heading", { name: "Request #42" })).toBeVisible();
    expect(screen.getByText("InReview")).toBeVisible();
    expect(screen.getByText("Connection")).toBeVisible();
    expect(screen.getByText("Ivan Petrov")).toBeVisible();
    expect(screen.getByText("Altai Krai, Zarinsk, Lenina 10")).toBeVisible();
    expect(screen.getByText("Started by another Employee")).toBeVisible();
    expect(
      screen.getByRole("link", {
        name: "Open employee details Request #42",
      }),
    ).toHaveAttribute("href", "/employee/requests/42");
  });

  it("renders optional row action slot", () => {
    renderWithRouter(
      <EmployeeRequestDashboardList
        requests={[
          {
            requestId: 42,
            requestType: "Connection",
            status: "InReview",
            applicantDisplayName: "Ivan Petrov",
            objectAddress: "Altai Krai, Zarinsk, Lenina 10",
            createdAt: "2026-01-02T10:30:00Z",
            reviewState: "NotStarted",
          },
        ]}
        renderRowActions={() => <button type="button">Start review</button>}
      />,
    );

    expect(screen.getByRole("button", { name: "Start review" })).toBeVisible();
  });

  it("shows filtered empty state with reset action", () => {
    const onResetFilters = vi.fn();

    renderWithRouter(
      <EmployeeRequestDashboardList
        requests={[]}
        emptyStateVariant="filtered"
        onResetFilters={onResetFilters}
      />,
    );

    expect(
      screen.getByText("No employee requests match selected filters."),
    ).toBeVisible();
    expect(screen.getByRole("button", { name: "Reset filters" })).toBeVisible();
  });
});
