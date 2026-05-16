import { expect, test, type Page } from "@playwright/test";
import { registerAndLoginL1Client } from "../support/l1ClientSetup";
import { waitForApiResponse } from "../support/apiResponse";

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
}

test("user sees current default and other saved Applicant Parties", async ({
  page,
  request,
}) => {
  const { email } = await registerAndLoginL1Client(
    page,
    request,
    "applicant-parties-read",
  );

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
    email: `saved-${email}`,
    phoneNumber: "+79007654321",
  });

  const applicantPartiesResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties",
  );
  await page.goto("/account");
  const applicantPartiesResponse = await applicantPartiesResponsePromise;
  expect(applicantPartiesResponse.ok()).toBeTruthy();

  const currentDefaults = page.getByRole("region", {
    name: "Current/default templates",
  });
  const otherSaved = page.getByRole("region", {
    name: "Other saved Applicant Parties",
  });

  await expect(currentDefaults.getByText("Ivan", { exact: true })).toBeVisible();
  await expect(currentDefaults.getByText("Current/default")).toBeVisible();
  await expect(otherSaved.getByText("Petr", { exact: true })).toBeVisible();
  await expect(otherSaved.getByText("Current/default")).not.toBeVisible();
});

test("user sees empty Applicant Parties read state", async ({ page, request }) => {
  await registerAndLoginL1Client(page, request, "applicant-parties-empty");

  const applicantPartiesResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties",
  );
  await page.goto("/account");
  const applicantPartiesResponse = await applicantPartiesResponsePromise;
  expect(applicantPartiesResponse.ok()).toBeTruthy();

  await expect(page.getByText("No saved Applicant Parties yet.")).toBeVisible();
  await expect(page.getByText("Applicant Party #")).not.toBeVisible();
});
