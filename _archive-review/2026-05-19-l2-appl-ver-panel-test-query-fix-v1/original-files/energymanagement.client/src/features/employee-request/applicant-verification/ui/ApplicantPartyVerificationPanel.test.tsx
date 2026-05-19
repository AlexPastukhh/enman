/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it, vi } from "vitest";
import { ApplicantPartyVerificationPanel } from "./ApplicantPartyVerificationPanel";

vi.mock("./RunApplicantPartyVerificationButton", () => ({
  RunApplicantPartyVerificationButton: ({ requestId }: { requestId: number }) => (
    <button type="button">Проверить данные {requestId}</button>
  ),
  __esModule: true,
}));

describe("ApplicantPartyVerificationPanel", () => {
  afterEach(() => {
    cleanup();
  });

  it("shows status, message and run action when verification can run", () => {
    render(
      <ApplicantPartyVerificationPanel
        requestId={42}
        verification={{
          required: true,
          status: "Unverified",
          canRun: true,
          message: "Данные не проверены",
        }}
      />,
    );

    expect(
      screen.getByRole("heading", { name: "Проверка данных заявителя" }),
    ).toBeVisible();
    expect(screen.getByText("Данные не проверены")).toBeVisible();
    expect(screen.getByRole("button", { name: "Проверить данные 42" })).toBeVisible();
  });

  it("hides run action when verification is not required", () => {
    render(
      <ApplicantPartyVerificationPanel
        requestId={42}
        verification={{
          required: false,
          status: "NotRequired",
          canRun: false,
          message: "Проверка не требуется",
        }}
      />,
    );

    expect(screen.getByText("Проверка не требуется")).toBeVisible();
    expect(
      screen.queryByRole("button", { name: "Проверить данные 42" }),
    ).not.toBeInTheDocument();
  });

  it("hides run action when verification is already verified", () => {
    render(
      <ApplicantPartyVerificationPanel
        requestId={42}
        verification={{
          required: true,
          status: "Verified",
          canRun: false,
          message: "Данные проверены",
        }}
      />,
    );

    expect(screen.getByText("Данные проверены")).toBeVisible();
    expect(
      screen.queryByRole("button", { name: "Проверить данные 42" }),
    ).not.toBeInTheDocument();
  });
});
