import { describe, expect, it } from "vitest";
import {
  buildCreateConnectionRequestDto,
  validateCreateConnectionRequestValues,
} from "./buildCreateConnectionRequestDto";
import type { CreateConnectionRequestFormValues } from "./createConnectionRequestTypes";

const baseValues = {
  applicantContextType: "Existing",
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
        fullName: {
          firstName: "Ivan",
          middleName: "Ivanovich",
          lastName: "Ivanov",
        },
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
