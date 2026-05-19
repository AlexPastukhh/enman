/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen, within } from "@testing-library/react";
import { afterEach, describe, expect, it, vi } from "vitest";
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

    expect(screen.getByText("Сохранённых заявителей пока нет.")).toBeVisible();
    expect(
      screen.getByText(
        "Сохранённые заявители появятся здесь после добавления к аккаунту.",
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
      name: "Текущий заявитель",
    });
    const otherSaved = screen.getByRole("region", {
      name: "Другие сохранённые заявители",
    });

    expect(
      within(currentDefaults).getByRole("heading", {
        name: "Заявитель #1: Ivan",
      }),
    ).toBeVisible();
    expect(
      within(currentDefaults).getByText("Текущий", { exact: true }),
    ).toBeVisible();
    expect(
      within(otherSaved).getByRole("heading", {
        name: "Заявитель #2: Petr",
      }),
    ).toBeVisible();
    expect(
      within(otherSaved).queryByText("Текущий", { exact: true }),
    ).not.toBeInTheDocument();
  });

  it("places optional actions for applicant party cards", () => {
    const renderAction = vi.fn((party: ApplicantPartySummary) => (
      <button type="button">Action for {party.displayName}</button>
    ));

    render(
      <ApplicantPartiesList
        applicantParties={[applicantParty(1, "Ivan", false)]}
        renderApplicantPartyActions={renderAction}
      />,
    );

    expect(screen.getByRole("button", { name: "Action for Ivan" })).toBeVisible();
    expect(renderAction).toHaveBeenCalledWith(
      expect.objectContaining({ applicantPartyId: 1 }),
      { highlighted: false },
    );
  });
});
