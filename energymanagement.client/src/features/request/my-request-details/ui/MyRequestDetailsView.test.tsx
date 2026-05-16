/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import type { ReactElement } from "react";
import { MemoryRouter } from "react-router-dom";
import { afterEach, describe, expect, it } from "vitest";
import { MyRequestDetailsNotFound } from "./MyRequestDetailsNotFound";
import { MyRequestDetailsView } from "./MyRequestDetailsView";

const request = {
  requestId: 14,
  requestType: "Connection",
  status: "Rejected",
  createdAt: "2026-01-02T10:30:00Z",
  submittedRequest: {
    details: "Подключение объекта",
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
  reviewResult: {
    decision: "Rejected",
    decidedAt: "2026-01-03T09:30:00Z",
    rejection: {
      reason: "Недостаточно данных по объекту.",
    },
  },
};

const renderWithRouter = (element: ReactElement) =>
  render(<MemoryRouter>{element}</MemoryRouter>);

describe("MyRequestDetailsView", () => {
  afterEach(() => {
    cleanup();
  });

  it("renders submitted request data and review result", () => {
    renderWithRouter(<MyRequestDetailsView request={request} />);

    expect(screen.getByRole("heading", { name: "Заявка #14" })).toBeVisible();
    expect(screen.getAllByText("Rejected")).toHaveLength(2);
    expect(screen.getByText("Connection")).toBeVisible();
    expect(screen.getByText("Подключение объекта")).toBeVisible();
    expect(screen.getByText(/Алтайский край/)).toBeVisible();
    expect(screen.getByText("Недостаточно данных по объекту.")).toBeVisible();
    expect(
      screen.getByRole("link", { name: "Вернуться к моим заявкам" }),
    ).toHaveAttribute("href", "/requests");
  });

  it("renders pending review message when review result is missing", () => {
    renderWithRouter(
      <MyRequestDetailsView request={{ ...request, reviewResult: undefined }} />,
    );

    expect(screen.getByText("Решение по заявке пока не вынесено.")).toBeVisible();
  });

  it("renders not-found state", () => {
    renderWithRouter(<MyRequestDetailsNotFound />);

    expect(screen.getByText("Заявка не найдена.")).toBeVisible();
    expect(
      screen.getByRole("link", { name: "Вернуться к моим заявкам" }),
    ).toHaveAttribute("href", "/requests");
  });
});
