import { expect, test } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import { uniqueEmail, validPassword } from "../support/testData";

test("user creates individual applicant party through real client-server flow", async ({
  page,
  request,
}) => {
  const email = uniqueEmail("applicant-party");

  const setupResponse = await request.post("/api/l1/auth/register", {
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
    "/api/l1/auth/login",
  );

  await loginPage.login({
    email,
    password: validPassword,
  });

  const loginResponse = await loginResponsePromise;
  expect(loginResponse.ok()).toBeTruthy();

  const initialCurrentApplicantPromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties/current-individual",
  );

  await page.goto("/account");
  const initialCurrentApplicantResponse = await initialCurrentApplicantPromise;
  expect(initialCurrentApplicantResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: "Applicant data", exact: true }),
  ).toBeVisible();

  const applicantPartyResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/l1/applicant-parties/individual",
  );
  const currentApplicantAfterCreatePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties/current-individual",
  );

  await page.getByLabel("First name").fill("Ivan");
  await page.getByLabel("Middle name").fill("Ivanovich");
  await page.getByLabel("Last name").fill("Ivanov");
  await page.getByLabel("Applicant email").fill(email);
  await page.getByLabel("Phone number").fill("+79001234567");
  await page.getByRole("button", { name: "Save applicant data" }).click();

  const applicantPartyResponse = await applicantPartyResponsePromise;
  expect(applicantPartyResponse.ok()).toBeTruthy();
  const currentApplicantAfterCreate =
    await currentApplicantAfterCreatePromise;
  expect(currentApplicantAfterCreate.ok()).toBeTruthy();

  await expect(page.getByText("Applicant data saved.")).toBeVisible();
  await expect(page.getByText("Ivan", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanovich", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanov", { exact: true })).toBeVisible();
  await expect(page.getByText("Unverified", { exact: true })).toBeVisible();

  const currentApplicantAfterReloadPromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/applicant-parties/current-individual",
  );
  await page.reload();
  const currentApplicantAfterReload =
    await currentApplicantAfterReloadPromise;
  expect(currentApplicantAfterReload.ok()).toBeTruthy();

  await expect(page.getByText("Ivan", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanovich", { exact: true })).toBeVisible();
  await expect(page.getByText("Ivanov", { exact: true })).toBeVisible();
});
