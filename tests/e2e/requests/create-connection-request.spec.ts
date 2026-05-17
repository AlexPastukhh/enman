import { expect, test, type Page } from "@playwright/test";
import { waitForApiResponse } from "../support/apiResponse";
import { registerAndLoginL1Client } from "../support/l1ClientSetup";

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
  const response = await page.request.post(
    "/api/l1/applicant-parties/individual",
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
    "/api/l1/applicant-parties",
  );
  await page.goto("/requests/create");
  const applicantPartiesResponse = await applicantPartiesResponsePromise;
  expect(applicantPartiesResponse.ok()).toBeTruthy();
  await expect(
    page.getByRole("heading", { name: "Create connection request" }),
  ).toBeVisible();
}

async function fillRequestDetailsAndAddress(page: Page) {
  await page
    .getByLabel("Request details")
    .fill("Подключение объекта к электрическим сетям");
  await page.getByLabel("Postal code").fill(validAddress.postalCode);
  await page.getByLabel("Region").fill(validAddress.region);
  await page.getByLabel("City").fill(validAddress.city);
  await page.getByLabel("Street").fill(validAddress.street);
  await page.getByLabel("House").fill(validAddress.house);
}

async function submitAndExpectMyRequestsHandoff(page: Page) {
  const createResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/l1/requests",
  );
  const myRequestsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/requests",
  );

  await page.getByRole("button", { name: "Create request" }).click();

  const createResponse = await createResponsePromise;
  expect(createResponse.ok()).toBeTruthy();
  const myRequestsResponse = await myRequestsResponsePromise;
  expect(myRequestsResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests$/);
  await expect(page.getByText("InReview", { exact: true })).toBeVisible();
  await expect(
    page.getByText("Подключение объекта к электрическим сетям"),
  ).toBeVisible();
}

test("user creates request with current/default saved ApplicantParty", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginL1Client(
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
  await expect(page.getByLabel("Saved Applicant Party")).toHaveValue(/\d+/);
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("user creates request with non-default saved ApplicantParty", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginL1Client(
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
    .getByLabel("Saved Applicant Party")
    .selectOption(String(nonDefaultApplicantPartyId));
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("user creates request with new applicant data", async ({ page, request }) => {
  const { email } = await registerAndLoginL1Client(
    page,
    request,
    "create-request-new-applicant",
  );

  await openCreateRequestPage(page);
  await expect(page.getByLabel("Enter new applicant data")).toBeChecked();
  await page.getByLabel("First name").fill("Ivan");
  await page.getByLabel("Middle name").fill("Ivanovich");
  await page.getByLabel("Last name").fill("Ivanov");
  await page.getByLabel("Applicant email").fill(`applicant-${email}`);
  await page.getByLabel("Phone number").fill("+79001234567");
  await fillRequestDetailsAndAddress(page);
  await submitAndExpectMyRequestsHandoff(page);
});

test("request creation validation feedback is visible", async ({
  page,
  request,
}) => {
  await registerAndLoginL1Client(
    page,
    request,
    "create-request-validation",
  );

  await openCreateRequestPage(page);
  await page.getByRole("button", { name: "Create request" }).click();

  await expect(page.getByText("Request details is required.")).toBeVisible();
  await expect(page.getByText("Postal code is required.")).toBeVisible();
});
