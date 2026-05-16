/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import AccountPage from "./AccountPage";

const { mockedUseSession, mockedUseCurrentApplicantQuery } = vi.hoisted(() => ({
  mockedUseSession: vi.fn(),
  mockedUseCurrentApplicantQuery: vi.fn(),
}));

vi.mock("../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../entities/applicant-party/model/useCurrentIndividualApplicantPartyQuery",
  () => ({
    useCurrentIndividualApplicantPartyQuery: mockedUseCurrentApplicantQuery,
    __esModule: true,
  }),
);

vi.mock("../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm", () => ({
  CreateIndividualApplicantPartyForm: () => (
    <form aria-label="Create applicant party">
      <button type="submit">Save applicant data</button>
    </form>
  ),
  __esModule: true,
}));

vi.mock("../../features/auth/register/ui/RegisterForm", () => ({
  RegisterForm: () => <form aria-label="Register"></form>,
  __esModule: true,
}));

vi.mock("../../shared/ui/layout/Header", () => ({
  Header: () => <header>Header</header>,
  __esModule: true,
}));

vi.mock("../../shared/ui/layout/Footer", () => ({
  Footer: () => <footer>Footer</footer>,
  __esModule: true,
}));

describe("AccountPage", () => {
  beforeEach(() => {
    mockedUseSession.mockReturnValue({
      accountId: 1,
      email: "client@example.com",
      role: "Client",
      isActive: true,
      isAuthenticated: true,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseCurrentApplicantQuery.mockReset();
  });

  it("shows create applicant form when current applicant is missing", () => {
    mockedUseCurrentApplicantQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        exists: false,
        applicantParty: null,
      },
    });

    render(<AccountPage />);

    expect(
      screen.getByRole("form", { name: "Create applicant party" }),
    ).toBeInTheDocument();
  });

  it("shows read-only applicant data when current applicant exists", () => {
    mockedUseCurrentApplicantQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        exists: true,
        applicantParty: {
          fullName: {
            firstName: "John",
            middleName: "Michael",
            lastName: "Doe",
          },
          email: "applicant.l1@example.com",
          phoneNumber: "79237554726",
          verificationStatus: "Unverified",
        },
      },
    });

    render(<AccountPage />);

    expect(screen.getByText("John")).toBeInTheDocument();
    expect(screen.getByText("Michael")).toBeInTheDocument();
    expect(screen.getByText("Doe")).toBeInTheDocument();
    expect(screen.getByText("applicant.l1@example.com")).toBeInTheDocument();
    expect(screen.getByText("79237554726")).toBeInTheDocument();
    expect(screen.getByText("Unverified")).toBeInTheDocument();
    expect(
      screen.queryByRole("form", { name: "Create applicant party" }),
    ).not.toBeInTheDocument();
  });
});
