import { expect, test, type Page } from "@playwright/test";
import { waitForApiResponse } from "../support/apiResponse";
import {
  postWithCsrf,
  registerAndLoginClient,
} from "../support/clientSetup";
import { L, cardWithText, statusInCard } from "../support/locators";

const validAddress = {
  postalCode: "658480",
  region: "Алтайский край",
  city: "Заринск",
  street: "Ленина",
  house: "10",
} as const;

async function createIndividualApplicantParty(
  page: Page,
  values: {
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
  },
) {
  const response = await postWithCsrf(
    page.request,
    "/api/applicant-parties/individual",
    {
      data: {
        fullName: {
          firstName: values.firstName,
          middleName: values.middleName,
          lastName: values.lastName,
        },
        email: values.email,
        phoneNumber: values.phoneNumber,
      },
    },
  );
  expect(response.ok()).toBeTruthy();

  const applicantParty = (await response.json()) as {
    applicantPartyId: number;
  };
  expect(applicantParty.applicantPartyId).toBeGreaterThan(0);

  return applicantParty.applicantPartyId;
}

async function openCreateRequestPage(page: Page) {
  const applicantPartiesResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/applicant-parties",
  );
  await page.goto("/requests/create");
  const applicantPartiesResponse = await applicantPartiesResponsePromise;
  expect(applicantPartiesResponse.ok()).toBeTruthy();
  await expect(
    page.getByRole("heading", { name: L.headings.createRequest, level: 2 }),
  ).toBeVisible();
}

async function fillRequestDetailsAndAddress(page: Page) {
  await page
    .getByLabel("Описание заявки")
    .fill("Подключение объекта к электрическим сетям");
  await page.getByLabel("Почтовый индекс").fill(validAddress.postalCode);
  await page.getByLabel("Регион").fill(validAddress.region);
  await page.getByLabel("Город").fill(validAddress.city);
  await page.getByLabel("Улица").fill(validAddress.street);
  await page.getByLabel("Дом").fill(validAddress.house);
}

async function submitAndExpectMyRequestsHandoff(page: Page) {
  const createResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/requests",
  );
  const myRequestsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/requests",
  );

  await page.getByRole("button", { name: L.buttons.createRequest }).click();

  const createResponse = await createResponsePromise;
  expect(createResponse.ok()).toBeTruthy();
  const myRequestsResponse = await myRequestsResponsePromise;
  expect(myRequestsResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests$/);
  const createdRequestCard = cardWithText(
    page,
    "Подключение объекта к электрическим сетям",
  );
  await expect(createdRequestCard).toBeVisible();
  await expect(statusInCard(createdRequestCard, L.status.inReview)).toBeVisible();
}

test("user creates request with current/default saved ApplicantParty", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginClient(
    page,
    request,
    "create-request-existing-default",
  );
  await createIndividualApplicantParty(page, {
    firstName: "Ivan",
    middleName: "Ivanovich",
    lastName: "Ivanov",
    email,
    phoneNumber: "+79001234567",
  });

  await openCreateRequestPage(page);
  await expect(
    page.getByLabel(L.applicantParties.savedApplicantLabel),
  ).toHaveValue(/\d+/);
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("user creates request with non-default saved ApplicantParty", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginClient(
    page,
    request,
    "create-request-existing-non-default",
  );
  await createIndividualApplicantParty(page, {
    firstName: "Ivan",
    middleName: "Ivanovich",
    lastName: "Ivanov",
    email,
    phoneNumber: "+79001234567",
  });
  const nonDefaultApplicantPartyId = await createIndividualApplicantParty(page, {
    firstName: "Petr",
    middleName: "Petrovich",
    lastName: "Petrov",
    email: `petr-${email}`,
    phoneNumber: "+79007654321",
  });

  await openCreateRequestPage(page);
  await page
    .getByLabel(L.applicantParties.savedApplicantLabel)
    .selectOption(String(nonDefaultApplicantPartyId));
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("user creates request with new applicant data", async ({ page, request }) => {
  const { email } = await registerAndLoginClient(
    page,
    request,
    "create-request-new-applicant",
  );

  await openCreateRequestPage(page);
  await expect(page.getByLabel("Ввести новые данные заявителя")).toBeChecked();
  await page.getByLabel("Имя").fill("Ivan");
  await page.getByLabel("Отчество").fill("Ivanovich");
  await page.getByLabel("Фамилия").fill("Ivanov");
  await page.getByLabel("Email заявителя").fill(`applicant-${email}`);
  await page.getByLabel("Телефон").fill("+79001234567");
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("request creation validation feedback is visible", async ({
  page,
  request,
}) => {
  await registerAndLoginClient(
    page,
    request,
    "create-request-validation",
  );

  await openCreateRequestPage(page);
  await page.getByRole("button", { name: L.buttons.createRequest }).click();

  await expect(page.getByText("Описание заявки: заполните поле.")).toBeVisible();
  await expect(page.getByText("Почтовый индекс: заполните поле.")).toBeVisible();
});
