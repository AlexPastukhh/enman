/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, it, vi } from "vitest";
import { MyRequestsFilters } from "./MyRequestsFilters";

describe("MyRequestsFilters", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows the selected status", () => {
    render(
      <MyRequestsFilters
        filters={{ status: "Approved" }}
        onChange={() => undefined}
        onReset={() => undefined}
      />,
    );

    expect(screen.getByLabelText("Статус")).toHaveValue("Approved");
  });

  it("reports status changes to the page", async () => {
    const user = userEvent.setup();
    const onChange = vi.fn();

    render(
      <MyRequestsFilters
        filters={{}}
        onChange={onChange}
        onReset={() => undefined}
      />,
    );

    await user.selectOptions(screen.getByLabelText("Статус"), "Rejected");

    expect(onChange).toHaveBeenCalledWith({ status: "Rejected" });
  });

  it("reports reset action to the page", async () => {
    const user = userEvent.setup();
    const onReset = vi.fn();

    render(
      <MyRequestsFilters
        filters={{ status: "InReview" }}
        onChange={() => undefined}
        onReset={onReset}
      />,
    );

    await user.click(screen.getByRole("button", { name: "Сбросить фильтры" }));

    expect(onReset).toHaveBeenCalledTimes(1);
  });
});
