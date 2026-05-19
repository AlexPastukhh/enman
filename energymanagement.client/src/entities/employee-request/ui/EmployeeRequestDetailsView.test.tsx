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

    expect(screen.getByRole("heading", { name: "Заявка #42" })).toBeVisible();
    expect(screen.getByText("На рассмотрении")).toBeVisible();
    expect(screen.getByText("Подключение")).toBeVisible();
    expect(screen.getByText("Ivan Petrov")).toBeVisible();
    expect(screen.getByText("ivan@example.com")).toBeVisible();
    expect(screen.getByText("+79001234567")).toBeVisible();
    expect(screen.getByText("Altai Krai, Zarinsk, Lenina 10")).toBeVisible();
    expect(screen.getByText("Please connect the object to the grid.")).toBeVisible();
    expect(screen.getByText("Не начато")).toBeVisible();
    expect(screen.getByText("Можно начать рассмотрение.")).toBeVisible();
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

    expect(screen.getByText("Рассматривается другим сотрудником")).toBeVisible();
    expect(screen.getByText("Начать рассмотрение сейчас нельзя.")).toBeVisible();
    expect(
      screen.getByText("Другой сотрудник уже начал рассмотрение этой заявки."),
    ).toBeVisible();
  });


  it("renders optional applicant verification slot before review actions", () => {
    render(
      <EmployeeRequestDetailsView
        details={baseDetails}
        renderApplicantVerification={() => (
          <section aria-label="Applicant verification">Verification panel</section>
        )}
        renderReviewActions={() => <button type="button">Future action</button>}
      />,
    );

    expect(screen.getByLabelText("Applicant verification")).toHaveTextContent(
      "Verification panel",
    );
    expect(screen.getByRole("button", { name: "Future action" })).toBeVisible();
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
