/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { MemoryRouter } from "react-router-dom";
import AccountPage from "./AccountPage";

const {
  mockedUseSession,
  mockedUseAccountApplicantPartiesQuery,
  mockedUseMakeApplicantPartyCurrentDefaultMutation,
} = vi.hoisted(() => ({
  mockedUseSession: vi.fn(),
  mockedUseAccountApplicantPartiesQuery: vi.fn(),
  mockedUseMakeApplicantPartyCurrentDefaultMutation: vi.fn(),
}));

vi.mock("../../entities/session/model/useSession", () => ({
  useSession: mockedUseSession,
  __esModule: true,
}));

vi.mock(
  "../../entities/applicant-party/model/useAccountApplicantPartiesQuery",
  () => ({
    useAccountApplicantPartiesQuery: mockedUseAccountApplicantPartiesQuery,
    __esModule: true,
  }),
);

vi.mock(
  "../../features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation",
  () => ({
    useMakeApplicantPartyCurrentDefaultMutation:
      mockedUseMakeApplicantPartyCurrentDefaultMutation,
    __esModule: true,
  }),
);

vi.mock(
  "../../features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm",
  () => ({
    CreateIndividualApplicantPartyForm: () => (
      <form aria-label="Create applicant party">
        <button type="submit">Save applicant data</button>
      </form>
    ),
    __esModule: true,
  }),
);

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
    mockedUseMakeApplicantPartyCurrentDefaultMutation.mockReturnValue({
      mutate: vi.fn(),
      variables: undefined,
      isPending: false,
      isError: false,
      error: null,
    });
  });

  afterEach(() => {
    cleanup();
    mockedUseSession.mockReset();
    mockedUseAccountApplicantPartiesQuery.mockReset();
    mockedUseMakeApplicantPartyCurrentDefaultMutation.mockReset();
  });

  it("shows signed-out account call to action when client is not signed in", () => {
    mockedUseSession.mockReturnValue(null);
    mockedUseAccountApplicantPartiesQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: undefined,
      refetch: vi.fn(),
    });

    render(
      <MemoryRouter>
        <AccountPage />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("heading", {
        name: "Войдите, чтобы управлять данными заявителя",
      }),
    ).toBeInTheDocument();
    expect(screen.getByRole("link", { name: "Войти" })).toHaveAttribute(
      "href",
      "/login",
    );
    expect(
      screen.getByRole("link", { name: "Зарегистрироваться" }),
    ).toHaveAttribute("href", "/register");
  });

  it("shows empty applicant parties state for signed-in client without saved parties", () => {
    mockedUseAccountApplicantPartiesQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        applicantParties: [],
      },
      refetch: vi.fn(),
    });

    render(<AccountPage />);

    expect(screen.getByText("No saved Applicant Parties yet.")).toBeVisible();
    expect(
      screen.getByRole("form", { name: "Create applicant party" }),
    ).toBeInTheDocument();
  });

  it("shows account applicant parties grouped by current/default state", () => {
    mockedUseAccountApplicantPartiesQuery.mockReturnValue({
      isPending: false,
      isError: false,
      data: {
        applicantParties: [
          {
            applicantPartyId: 1,
            applicantPartyType: "Individual",
            displayName: "John Doe",
            fullName: {
              firstName: "John",
              middleName: "Michael",
              lastName: "Doe",
            },
            email: "applicant.l1@example.com",
            phoneNumber: "79237554726",
            verificationStatus: "Unverified",
            isCurrentDefault: true,
            createdAt: "2026-01-02T10:30:00Z",
          },
          {
            applicantPartyId: 2,
            applicantPartyType: "Individual",
            displayName: "Jane Doe",
            fullName: {
              firstName: "Jane",
              middleName: "Maria",
              lastName: "Doe",
            },
            email: "applicant.two@example.com",
            phoneNumber: "79237554727",
            verificationStatus: "Unverified",
            isCurrentDefault: false,
            createdAt: "2026-01-03T10:30:00Z",
          },
        ],
      },
      refetch: vi.fn(),
    });

    render(<AccountPage />);

    expect(screen.getByText("John Doe")).toBeVisible();
    expect(screen.getByText("Jane Doe")).toBeVisible();
    expect(screen.getByText("Current/default", { exact: true })).toBeVisible();
    expect(screen.getByText("applicant.l1@example.com")).toBeVisible();
    expect(screen.getByText("applicant.two@example.com")).toBeVisible();
    expect(
      screen.getByRole("button", { name: "Make current/default" }),
    ).toBeVisible();
  });
});
