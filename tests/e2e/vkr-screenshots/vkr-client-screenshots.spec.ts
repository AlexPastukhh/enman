import { expect, test, type Page } from "@playwright/test";
import { mkdir } from "node:fs/promises";
import { LoginPage } from "../pages/LoginPage";
import { RegisterPage } from "../pages/RegisterPage";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
} from "../support/clientSetup";
import { demoCredentials, seedE2eDemoData } from "../support/demoSeed";
import { uniqueEmail, validPassword } from "../support/testData";

const outputDir = "planning/thesis/assets/screenshots";

async function capture(page: Page, fileName: string) {
  await page.screenshot({
    path: `${outputDir}/${fileName}`,
    fullPage: true,
  });
}

test.describe("VKR documentation screenshots @screenshots", () => {
  test.beforeEach(async () => {
    await mkdir(outputDir, { recursive: true });
  });

  test("capture stable client request flow screens", async ({ page, request }) => {
    const email = uniqueEmail("vkr-screenshots");

    const registerPage = new RegisterPage(page);
    await registerPage.open();
    await expect(registerPage.heading()).toBeVisible();
    await capture(page, "01-register-client.png");

    await registerPage.register({
      email,
      password: validPassword,
      passwordConfirmation: validPassword,
    });
    await expect(page).toHaveURL(/\/login$/);

    const loginPage = new LoginPage(page);
    await expect(loginPage.heading()).toBeVisible();
    await capture(page, "02-login-client.png");

    await loginPage.login({ email, password: validPassword });
    await expect(page).toHaveURL(/\/$/);

    await createIndividualApplicantParty(page, email);

    await page.goto("/account");
    await expect(page.getByRole("heading", { name: "Данные аккаунта" })).toBeVisible();
    await expect(page.getByText("Ivan", { exact: true }).first()).toBeVisible();
    await capture(page, "03-account-applicant.png");

    await page.goto("/requests/create");
    await expect(
      page.getByRole("heading", { name: "Create connection request" }),
    ).toBeVisible();
    await capture(page, "04-create-request.png");

    await createConnectionRequestForExistingApplicant(
      page,
      Number(await page.getByLabel("Saved Applicant Party", { exact: true }).inputValue()),
      "VKR screenshot request.",
    );

    await page.goto("/requests");
    await expect(page.getByRole("heading", { name: "My Requests" })).toBeVisible();
    await expect(page.getByText("VKR screenshot request.")).toBeVisible();
    await capture(page, "05-my-requests-list.png");

    const myRequestsResponse = await page.request.get("/api/requests");
    expect(myRequestsResponse.ok()).toBeTruthy();
    const requests = (await myRequestsResponse.json()) as Array<{ requestId: number }>;
    expect(requests.length).toBeGreaterThan(0);

    await page.goto(`/requests/${requests[0].requestId}`);
    await expect(page.getByRole("heading", { name: "Request Details" })).toBeVisible();
    await expect(page.getByText("VKR screenshot request.")).toBeVisible();
    await capture(page, "06-my-request-details.png");
  });

  test("capture stable employee and agreement exchange screens from seeded demo data", async ({
    page,
  }) => {
    seedE2eDemoData();

    const loginPage = new LoginPage(page);
    await loginPage.open();
    await loginPage.login({
      email: demoCredentials.employeeEmail,
      password: demoCredentials.password,
    });
    await expect(page).toHaveURL(/\/$/);

    await page.goto("/employee/requests");
    await expect(
      page.getByRole("heading", { name: "Employee Request Dashboard" }),
    ).toBeVisible();
    await capture(page, "07-employee-request-dashboard.png");

    await page.goto("/employee/requests/9004");
    await expect(
      page.getByRole("heading", { name: "Employee Request Details" }),
    ).toBeVisible();
    await capture(page, "08-employee-request-details-review-actions.png");

    await page.goto("/employee/agreements");
    await expect(
      page.getByRole("heading", { name: "Employee Agreement Exchanges" }),
    ).toBeVisible();
    await capture(page, "09-agreement-exchange-list.png");
  });
});
