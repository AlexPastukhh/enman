/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen, within } from "@testing-library/react";
import { afterEach, describe, expect, it } from "vitest";
import type { ApplicantPartySummary } from "../model/applicantPartyTypes";
import { ApplicantPartiesList } from "./ApplicantPartiesList";

const applicantParty = (
  applicantPartyId: number,
  displayName: string,
  isCurrentDefault: boolean,
): ApplicantPartySummary => ({
  applicantPartyId,
  applicantPartyType: "Individual",
  displayName,
  fullName: {
    firstName: displayName,
    middleName: "Ivanovich",
    lastName: "Ivanov",
  },
  email: `${displayName.toLowerCase()}@example.com`,
  phoneNumber: "+79001234567",
  verificationStatus: "Unverified",
  isCurrentDefault,
  createdAt: "2026-01-02T10:30:00Z",
});

describe("ApplicantPartiesList", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows an empty read state when no applicant parties exist", () => {
    render(<ApplicantPartiesList applicantParties={[]} />);

    expect(screen.getByText("No saved Applicant Parties yet.")).toBeVisible();
    expect(
      screen.getByText(
        "Saved Applicant Parties will appear here after they are added to the account.",
      ),
    ).toBeVisible();
  });

  it("renders current/default and other saved applicant parties separately", () => {
    render(
      <ApplicantPartiesList
        applicantParties={[
          applicantParty(1, "Ivan", true),
          applicantParty(2, "Petr", false),
        ]}
      />,
    );

    const currentDefaults = screen.getByRole("region", {
      name: "Current/default templates",
    });
    const otherSaved = screen.getByRole("region", {
      name: "Other saved Applicant Parties",
    });

    expect(
      within(currentDefaults).getByRole("heading", {
        name: "Applicant Party #1: Ivan",
      }),
    ).toBeVisible();
    expect(within(currentDefaults).getByText("Current/default")).toBeVisible();
    expect(
      within(otherSaved).getByRole("heading", {
        name: "Applicant Party #2: Petr",
      }),
    ).toBeVisible();
    expect(within(otherSaved).queryByText("Current/default")).not.toBeInTheDocument();
  });
});
