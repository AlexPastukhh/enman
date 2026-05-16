/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import type { ReactElement } from "react";
import { MemoryRouter } from "react-router-dom";
import { afterEach, describe, expect, it } from "vitest";
import { MyRequestsList } from "./MyRequestsList";

const renderWithRouter = (element: ReactElement) =>
  render(<MemoryRouter>{element}</MemoryRouter>);

describe("MyRequestsList", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows an empty state when there are no requests", () => {
    renderWithRouter(<MyRequestsList requests={[]} />);

    expect(screen.getByText("У вас пока нет заявок.")).toBeVisible();
  });

  it("renders request summary data", () => {
    renderWithRouter(
      <MyRequestsList
        requests={[
          {
            requestId: 12,
            requestType: "Connection",
            status: "InReview",
            createdAt: "2026-01-02T10:30:00Z",
            summary: "Подключение объекта",
            objectAddress: {
              postalCode: "658480",
              region: "Алтайский край",
              city: "Заринск",
              street: "Ленина",
              house: "10",
              building: null,
              apartment: null,
            },
          },
        ]}
      />,
    );

    expect(screen.getByRole("heading", { name: "Заявка #12" })).toBeVisible();
    expect(screen.getByText("InReview")).toBeVisible();
    expect(screen.getByText("Подключение объекта")).toBeVisible();
    expect(screen.getByText(/Алтайский край/)).toBeVisible();
    expect(screen.getByText(/Заринск/)).toBeVisible();
    expect(
      screen.getByRole("link", { name: "Открыть детали Заявка #12" }),
    ).toHaveAttribute("href", "/requests/12");
  });

  it("shows filtered empty state with reset action", () => {
    renderWithRouter(
      <MyRequestsList
        requests={[]}
        emptyStateVariant="filtered"
        onResetFilters={() => undefined}
      />,
    );

    expect(screen.getByText("Заявок с выбранным фильтром не найдено.")).toBeVisible();
    expect(
      screen.getByRole("button", { name: "Сбросить фильтры" }),
    ).toBeVisible();
  });
});
