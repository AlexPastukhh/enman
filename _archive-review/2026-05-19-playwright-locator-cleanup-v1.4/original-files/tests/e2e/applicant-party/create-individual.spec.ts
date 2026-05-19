import { expect, test } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import { postWithCsrf } from "../support/clientSetup";
import { uniqueEmail, validPassword } from "../support/testData";
import { L } from "../support/locators";

test("user creates individual applicant party through real client-server flow", async ({
  page,
  request,
}) => {
  const email = uniqueEmail("applicant-party");

  const setupResponse = await postWithCsrf(request, "/api/auth/register", {
    data: {
      email,
      password: validPassword,
    },
  });
  expect(setupResponse.ok()).toBeTruthy();

  const loginPage = new LoginPage(page);
  await loginPage.open();
  await expect(loginPage.heading()).toBeVisible();

  const loginResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/auth/login",
  );

  await loginPage.login({
    email,
    password: validPassword,
  });

  const loginResponse = await loginResponsePromise;
  expect(loginResponse.ok()).toBeTruthy();

  const initialApplicantPartiesPromise = waitForApiResponse(
    page,
    "GET",
    "/api/applicant-parties",
  );

  await page.goto("/account");
  const initialApplicantPartiesResponse = await initialApplicantPartiesPromise;
  expect(initialApplicantPartiesResponse.ok()).toBeTruthy();

  await expect(page.getByText(L.applicantParties.noSaved)).toBeVisible();
  await expect(
    page.getByRole("heading", { name: "Applicant data", exact: true }),
  ).toBeVisible();

  const applicantPartyResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/applicant-parties/individual",
  );
  const applicantPartiesAfterCreatePromise = waitForApiResponse(
    page,
    "GET",
    "/api/applicant-parties",
  );

  await page.getByLabel("First name").fill("Ivan");
  await page.getByLabel("Middle name").fill("Ivanovich");
  await page.getByLabel("Last name").fill("Ivanov");
  await page.getByLabel("Applicant email").fill(email);
  await page.getByLabel("Phone number").fill("+79001234567");
  await page.getByRole("button", { name: "Save applicant data" }).click();

  const applicantPartyResponse = await applicantPartyResponsePromise;
  expect(applicantPartyResponse.ok()).toBeTruthy();
  const applicantPartiesAfterCreate = await applicantPartiesAfterCreatePromise;
  expect(applicantPartiesAfterCreate.ok()).toBeTruthy();

  await expect(page.getByText("Applicant data saved.")).toBeVisible();
  await expect(page.getByText("Ivan", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanovich", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanov", { exact: true })).toBeVisible();
  await expect(page.getByText("Unverified", { exact: true })).toBeVisible();
  await expect(page.getByText(L.applicantParties.currentDefaultRegion, { exact: true })).toBeVisible();

  const applicantPartiesAfterReloadPromise = waitForApiResponse(
    page,
    "GET",
    "/api/applicant-parties",
  );
  await page.reload();
  const applicantPartiesAfterReload = await applicantPartiesAfterReloadPromise;
  expect(applicantPartiesAfterReload.ok()).toBeTruthy();

  await expect(page.getByText("Ivan", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanovich", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanov", { exact: true })).toBeVisible();
});
