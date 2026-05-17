/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it } from "vitest";
import type { EmployeeRequestDetails } from "../model/employeeRequestTypes";
import { EmployeeRequestDetailsView } from "./EmployeeRequestDetailsView";

const baseDetails: EmployeeRequestDetails = {
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
};

describe("EmployeeRequestDetailsView", () => {
  afterEach(() => {
    cleanup();
  });

  it("renders employee request details and applicant review data", () => {
    render(<EmployeeRequestDetailsView details={baseDetails} />);

    expect(screen.getByRole("heading", { name: "Request #42" })).toBeVisible();
    expect(screen.getByText("InReview")).toBeVisible();
    expect(screen.getByText("Connection")).toBeVisible();
    expect(screen.getByText("Ivan Petrov")).toBeVisible();
    expect(screen.getByText("ivan@example.com")).toBeVisible();
    expect(screen.getByText("+79001234567")).toBeVisible();
    expect(screen.getByText("Altai Krai, Zarinsk, Lenina 10")).toBeVisible();
    expect(screen.getByText("Please connect the object to the grid.")).toBeVisible();
    expect(screen.getByText("Not started")).toBeVisible();
    expect(screen.getByText("Start review is available.")).toBeVisible();
  });

  it("renders blocked action state when another Employee started review", () => {
    render(
      <EmployeeRequestDetailsView
        details={{
          ...baseDetails,
          reviewState: "StartedByAnotherEmployee",
        }}
      />,
    );

    expect(screen.getByText("Started by another Employee")).toBeVisible();
    expect(screen.getByText("Start review is not available.")).toBeVisible();
    expect(
      screen.getByText("Another Employee has already started this review."),
    ).toBeVisible();
  });

  it("renders optional future review action slot", () => {
    render(
      <EmployeeRequestDetailsView
        details={baseDetails}
        renderReviewActions={() => <button type="button">Future action</button>}
      />,
    );

    expect(screen.getByRole("button", { name: "Future action" })).toBeVisible();
  });
});
