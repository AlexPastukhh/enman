import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { createApplicantParty } from "./createApplicantParty";
import type { CreateApplicantPartyFormValues } from "../model/createApplicantPartySchema";

const baseValues: CreateApplicantPartyFormValues = {
  applicantPartyType: "Individual",
  firstName: "Ivan",
  middleName: "Ivanovich",
  lastName: "Petrov",
  organizationName: "OOO Energy Client",
  inn: "123456789012",
  kpp: "123456789",
  ogrn: "1234567890123",
  ogrnip: "123456789012345",
  email: "ivan@example.com",
  phoneNumber: "+79001234567",
};

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("createApplicantParty", () => {
  it("posts individual entrepreneur values to the subtype endpoint", async () => {
    const fetchMock = mockSuccessfulFetch();
    vi.stubGlobal("fetch", fetchMock);

    await createApplicantParty({
      ...baseValues,
      applicantPartyType: "IndividualEntrepreneur",
    });

    expect(fetchMock.mock.calls[1]?.[0]).toBe(
      "/api/applicant-parties/individual-entrepreneur",
    );
    expect(JSON.parse(fetchMock.mock.calls[1]?.[1]?.body as string)).toEqual({
      fullName: {
        firstName: "Ivan",
        middleName: "Ivanovich",
        lastName: "Petrov",
      },
      inn: "123456789012",
      ogrnip: "123456789012345",
      email: "ivan@example.com",
      phoneNumber: "+79001234567",
    });
  });

  it("posts legal entity values to the subtype endpoint", async () => {
    const fetchMock = mockSuccessfulFetch();
    vi.stubGlobal("fetch", fetchMock);

    await createApplicantParty({
      ...baseValues,
      applicantPartyType: "LegalEntity",
      inn: "1234567890",
    });

    expect(fetchMock.mock.calls[1]?.[0]).toBe(
      "/api/applicant-parties/legal-entity",
    );
    expect(JSON.parse(fetchMock.mock.calls[1]?.[1]?.body as string)).toEqual({
      organizationName: "OOO Energy Client",
      inn: "1234567890",
      kpp: "123456789",
      ogrn: "1234567890123",
      email: "ivan@example.com",
      phoneNumber: "+79001234567",
    });
  });
});

const mockSuccessfulFetch = () =>
  vi
    .fn()
    .mockResolvedValueOnce(
      new Response(JSON.stringify({ requestToken: "token-1" }), {
        status: 200,
        headers: { "content-type": "application/json" },
      }),
    )
    .mockResolvedValue(
      new Response(
        JSON.stringify({ applicantPartyId: 1, clientAccountId: 2 }),
        {
          status: 200,
          headers: { "content-type": "application/json" },
        },
      ),
    );
