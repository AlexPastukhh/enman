/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it } from "vitest";
import { ApplicantVerificationStatusBadge } from "./ApplicantVerificationStatusBadge";

describe("ApplicantVerificationStatusBadge", () => {
  afterEach(() => {
    cleanup();
  });

  it("renders not-required verification state", () => {
    render(
      <ApplicantVerificationStatusBadge
        verification={{ required: false, status: "NotRequired", canRun: false }}
      />,
    );

    expect(screen.getByText("Проверка не требуется")).toBeVisible();
  });

  it("renders unverified verification state", () => {
    render(
      <ApplicantVerificationStatusBadge
        verification={{ required: true, status: "Unverified", canRun: true }}
      />,
    );

    expect(screen.getByText("Данные не проверены")).toBeVisible();
  });

  it("renders verified verification state", () => {
    render(
      <ApplicantVerificationStatusBadge
        verification={{ required: true, status: "Verified", canRun: false }}
      />,
    );

    expect(screen.getByText("Данные проверены")).toBeVisible();
  });
});
