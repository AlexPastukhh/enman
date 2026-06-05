import { describe, expect, it } from "vitest";
import {
  buildCreateConnectionRequestDto,
  validateCreateConnectionRequestValues,
} from "./buildCreateConnectionRequestDto";
import type { CreateConnectionRequestFormValues } from "./createConnectionRequestTypes";

const baseValues = {
  applicantContextType: "Existing",
  applicantPartyType: "Individual",
  existingApplicantPartyId: "42",
  details: "Подключение объекта к электрическим сетям",
  postalCode: "658480",
  region: "Алтайский край",
  city: "Заринск",
  street: "Ленина",
  house: "10",
  building: "",
  apartment: "",
  firstName: "Ivan",
  middleName: "Ivanovich",
  lastName: "Ivanov",
  organizationName: "ООО Энергия",
  inn: "1234567890",
  kpp: "123456789",
  ogrn: "1234567890123",
  ogrnip: "123456789012345",
  email: "ivan@example.com",
  phoneNumber: "+79001234567",
} satisfies CreateConnectionRequestFormValues;

describe("buildCreateConnectionRequestDto", () => {
  it("maps the Existing applicant branch", () => {
    expect(buildCreateConnectionRequestDto(baseValues)).toEqual({
      applicantContextType: "Existing",
      existingApplicantPartyId: 42,
      details: "Подключение объекта к электрическим сетям",
      address: {
        postalCode: "658480",
        region: "Алтайский край",
        city: "Заринск",
        street: "Ленина",
        house: "10",
        building: null,
        apartment: null,
      },
    });
  });

  it("maps the New applicant branch", () => {
    expect(
      buildCreateConnectionRequestDto({
        ...baseValues,
        applicantContextType: "New",
        existingApplicantPartyId: "",
      }),
    ).toEqual({
      applicantContextType: "New",
      existingApplicantPartyId: null,
      newApplicantParty: {
        applicantPartyType: "Individual",
        fullName: {
          firstName: "Ivan",
          middleName: "Ivanovich",
          lastName: "Ivanov",
        },
        organizationName: null,
        inn: null,
        kpp: null,
        ogrn: null,
        ogrnip: null,
        email: "ivan@example.com",
        phoneNumber: "+79001234567",
      },
      details: "Подключение объекта к электрическим сетям",
      address: {
        postalCode: "658480",
        region: "Алтайский край",
        city: "Заринск",
        street: "Ленина",
        house: "10",
        building: null,
        apartment: null,
      },
    });
  });

  it("maps the New individual entrepreneur branch", () => {
    expect(
      buildCreateConnectionRequestDto({
        ...baseValues,
        applicantContextType: "New",
        applicantPartyType: "IndividualEntrepreneur",
        existingApplicantPartyId: "",
      }),
    ).toMatchObject({
      applicantContextType: "New",
      existingApplicantPartyId: null,
      newApplicantParty: {
        applicantPartyType: "IndividualEntrepreneur",
        fullName: {
          firstName: "Ivan",
          middleName: "Ivanovich",
          lastName: "Ivanov",
        },
        organizationName: null,
        inn: "1234567890",
        kpp: null,
        ogrn: null,
        ogrnip: "123456789012345",
        email: "ivan@example.com",
        phoneNumber: "+79001234567",
      },
    });
  });

  it("maps the New legal entity branch", () => {
    expect(
      buildCreateConnectionRequestDto({
        ...baseValues,
        applicantContextType: "New",
        applicantPartyType: "LegalEntity",
        existingApplicantPartyId: "",
      }),
    ).toMatchObject({
      applicantContextType: "New",
      existingApplicantPartyId: null,
      newApplicantParty: {
        applicantPartyType: "LegalEntity",
        fullName: undefined,
        organizationName: "ООО Энергия",
        inn: "1234567890",
        kpp: "123456789",
        ogrn: "1234567890123",
        ogrnip: null,
        email: "ivan@example.com",
        phoneNumber: "+79001234567",
      },
    });
  });

  it("validates required Existing branch fields", () => {
    expect(
      validateCreateConnectionRequestValues({
        ...baseValues,
        existingApplicantPartyId: "",
        details: "",
        postalCode: "",
      }),
    ).toMatchObject({
      existingApplicantPartyId:
        "Выберите сохранённого заявителя или введите новые данные заявителя.",
      details: "Описание заявки: заполните поле.",
      postalCode: "Почтовый индекс: заполните поле.",
    });
  });
});
