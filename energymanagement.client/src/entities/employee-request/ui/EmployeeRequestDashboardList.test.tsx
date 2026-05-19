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

    expect(screen.getByText("Нет заявок для рассмотрения.")).toBeVisible();
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

    expect(screen.getByRole("heading", { name: "Заявка #42" })).toBeVisible();
    expect(screen.getByText("На рассмотрении")).toBeVisible();
    expect(screen.getByText("Подключение")).toBeVisible();
    expect(screen.getByText("Ivan Petrov")).toBeVisible();
    expect(screen.getByText("Altai Krai, Zarinsk, Lenina 10")).toBeVisible();
    expect(screen.getByText("Рассматривается другим сотрудником")).toBeVisible();
    expect(
      screen.getByRole("link", {
        name: "Открыть детали Заявка #42",
      }),
    ).toHaveAttribute("href", "/employee/requests/42");
  });

  it("renders applicant verification badge when verification summary exists", () => {
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
            applicantVerification: {
              required: true,
              status: "Unverified",
              canRun: true,
              message: "Данные не проверены",
            },
          },
        ]}
      />,
    );

    expect(screen.getByText("Данные не проверены")).toBeVisible();
  });

  it("does not guess applicant verification state when summary is missing", () => {
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
      />,
    );

    expect(screen.queryByText("Данные не проверены")).not.toBeInTheDocument();
    expect(screen.queryByText("Данные проверены")).not.toBeInTheDocument();
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
        renderRowActions={() => <button type="button">Начать рассмотрение</button>}
      />,
    );

    expect(screen.getByRole("button", { name: "Начать рассмотрение" })).toBeVisible();
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

    expect(screen.getByText("Нет заявок по выбранным фильтрам.")).toBeVisible();
    expect(screen.getByRole("button", { name: "Сбросить фильтры" })).toBeVisible();
  });
});
