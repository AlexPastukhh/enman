import { expect, test, type Page } from "@playwright/test";
import { waitForApiResponse } from "../support/apiResponse";
import {
  postWithCsrf,
  registerAndLoginL1Client,
} from "../support/l1ClientSetup";

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
}

async function getApplicantPartyIdByEmail(page: Page, email: string) {
  const response = await page.request.get("/api/l1/applicant-parties");
  expect(response.ok()).toBeTruthy();

  const body = (await response.json()) as {
    applicantParties?: Array<{
      applicantPartyId?: number;
      email?: string | null;
    }> | null;
  };

  const applicantParty = body.applicantParties?.find(
    (item) => item.email === email,
  );

  expect(applicantParty?.applicantPartyId).toBeTruthy();
  return applicantParty!.applicantPartyId!;
}

test("user makes another saved Applicant Party current/default", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginL1Client(
    page,
    request,
    "applicant-parties-make-default",
  );
  const otherEmail = `default-${email}`;

  await createIndividualApplicantParty(page, {
    firstName: "Ivan",
    middleName: "Ivanovich",
    lastName: "Ivanov",
    email,
    phoneNumber: "+79001234567",
  });
  await createIndividualApplicantParty(page, {
    firstName: "Petr",
    middleName: "Petrovich",
    lastName: "Petrov",
    email: otherEmail,
    phoneNumber: "+79007654321",
  });

  const nonDefaultApplicantPartyId = await getApplicantPartyIdByEmail(
    page,
    otherEmail,
  );

  const applicantPartiesResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties",
  );
  await page.goto("/account");
  expect((await applicantPartiesResponsePromise).ok()).toBeTruthy();

  const currentDefaults = page.getByRole("region", {
    name: "Current/default templates",
  });
  const otherSaved = page.getByRole("region", {
    name: "Other saved Applicant Parties",
  });

  await expect(
    currentDefaults.locator("article").filter({ hasText: "Ivan" }),
  ).toBeVisible();

  const petrCard = otherSaved.locator("article").filter({ hasText: "Petr" });
  await expect(petrCard).toBeVisible();

  const makeCurrentDefaultResponsePromise = waitForApiResponse(
    page,
    "POST",
    `/api/l1/applicant-parties/${nonDefaultApplicantPartyId}/make-current-default`,
  );
  await petrCard.getByRole("button", { name: "Make current/default" }).click();
  expect((await makeCurrentDefaultResponsePromise).ok()).toBeTruthy();

  await expect(
    currentDefaults.locator("article").filter({ hasText: "Petr" }),
  ).toBeVisible();
  await expect(
    otherSaved.locator("article").filter({ hasText: "Ivan" }),
  ).toBeVisible();
});
