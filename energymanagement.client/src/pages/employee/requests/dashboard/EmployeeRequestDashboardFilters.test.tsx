/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, it, vi } from "vitest";
import { EmployeeRequestDashboardFilters } from "./EmployeeRequestDashboardFilters";

describe("EmployeeRequestDashboardFilters", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows selected filters", () => {
    render(
      <EmployeeRequestDashboardFilters
        filters={{ status: "InReview", reviewState: "NotStarted" }}
        onChange={() => undefined}
        onReset={() => undefined}
      />,
    );

    expect(screen.getByLabelText("Status")).toHaveValue("InReview");
    expect(screen.getByLabelText("Review state")).toHaveValue("NotStarted");
  });

  it("reports filter changes", async () => {
    const user = userEvent.setup();
    const onChange = vi.fn();

    render(
      <EmployeeRequestDashboardFilters
        filters={{}}
        onChange={onChange}
        onReset={() => undefined}
      />,
    );

    await user.selectOptions(screen.getByLabelText("Review state"), "StartedByAnotherEmployee");

    expect(onChange).toHaveBeenCalledWith({
      reviewState: "StartedByAnotherEmployee",
    });
  });

  it("reports reset action", async () => {
    const user = userEvent.setup();
    const onReset = vi.fn();

    render(
      <EmployeeRequestDashboardFilters
        filters={{ status: "Approved" }}
        onChange={() => undefined}
        onReset={onReset}
      />,
    );

    await user.click(screen.getByRole("button", { name: "Reset filters" }));

    expect(onReset).toHaveBeenCalledTimes(1);
  });
});
