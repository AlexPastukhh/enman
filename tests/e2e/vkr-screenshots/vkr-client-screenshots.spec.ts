import { expect, test, type Page } from "@playwright/test";
import { mkdir } from "node:fs/promises";
import path from "node:path";
import { LoginPage } from "../pages/LoginPage";
import { RegisterPage } from "../pages/RegisterPage";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
} from "../support/clientSetup";
import { waitForApiResponse } from "../support/apiResponse";
import { demoCredentials, demoRequestIds, seedE2eDemoData } from "../support/demoSeed";
import { L } from "../support/locators";
import { uniqueEmail, validPassword } from "../support/testData";

const outputDir = "planning/thesis/assets/screenshots";

const fixtureDocumentPath = path.join(
  process.cwd(),
  "tests/e2e/fixtures/demo-agreement.pdf",
);

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
      page.getByRole("heading", { name: /Создание заявки|Заявка на подключение/, level: 2 }),
    ).toBeVisible();
    await capture(page, "04-create-request.png");

    await createConnectionRequestForExistingApplicant(
      page,
      Number(
        await page
          .getByLabel(/Saved Applicant Party|Сохранённый заявитель|Сохраненный заявитель/)
          .inputValue(),
      ),
      "VKR screenshot request.",
    );

    await page.goto("/requests");
    await expect(page.getByRole("heading", { name: "Мои заявки" })).toBeVisible();
    await expect(page.getByText("VKR screenshot request.")).toBeVisible();
    await capture(page, "05-my-requests-list.png");

    const myRequestsResponse = await page.request.get("/api/requests");
    expect(myRequestsResponse.ok()).toBeTruthy();
    const requests = (await myRequestsResponse.json()) as Array<{ requestId: number }>;
    expect(requests.length).toBeGreaterThan(0);

    await page.goto(`/requests/${requests[0].requestId}`);
    await expect(page.getByRole("heading", { name: "Детали заявки" })).toBeVisible();
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
      page.getByRole("heading", { name: "Заявки на рассмотрение" }),
    ).toBeVisible();
    await capture(page, "07-employee-request-dashboard.png");

    await page.goto("/employee/requests/9004");
    await expect(
      page.getByRole("heading", { name: "Рассмотрение заявки" }),
    ).toBeVisible();
    await capture(page, "08-employee-request-details-review-actions.png");

    // Start review and capture applicant verification before/after
    const startReviewResponsePromise = waitForApiResponse(
      page,
      "POST",
      "/review/start",
    );
    await page.getByRole("button", { name: L.buttons.startReview }).click();
    const startReviewResponse = await startReviewResponsePromise;
    expect(startReviewResponse.ok()).toBeTruthy();

    const verificationButton = page.getByRole("button", { name: /Проверить данные/ });
    if (!(await verificationButton.isVisible())) {
      throw new Error("Applicant verification button is not visible in seeded demo data");
    }

    await capture(page, "11-applicant-verification-before.png");

    const verificationResponsePromise = waitForApiResponse(
      page,
      "POST",
      "/applicant-party/verification/run",
    );
    await verificationButton.click();
    const verificationResponse = await verificationResponsePromise;
    expect(verificationResponse.ok()).toBeTruthy();
    await expect(page.getByText("Данные проверены").first()).toBeVisible();
    await capture(page, "12-applicant-verification-after.png");

    // Start an agreement exchange for deterministic details capture
    await page.goto("/employee/requests/9005");
    const employeeDetailsResponsePromise = waitForApiResponse(
      page,
      "GET",
      "/api/employee/requests/",
    );
    await page.goto("/employee/requests/9005");
    const employeeDetailsResponse = await employeeDetailsResponsePromise;
    expect(employeeDetailsResponse.ok()).toBeTruthy();

    await page
      .locator('input[type="file"][id^="start-exchange-document-"]')
      .setInputFiles(fixtureDocumentPath);
    await page
      .getByLabel(L.agreementExchange.initialCommentLabel)
      .fill("Initial E2E agreement proposal.");

    const startExchangeResponsePromise = waitForApiResponse(
      page,
      "POST",
      "/agreement-exchange/start",
    );
    await page.getByRole("button", { name: L.buttons.startAgreementExchange }).click();
    const startExchangeResponse = await startExchangeResponsePromise;
    expect(startExchangeResponse.ok()).toBeTruthy();

    await page.goto("/employee/agreements");
    await expect(
      page.getByRole("heading", { name: /Согласование договоров|Мои договоры/ }),
    ).toBeVisible();
    await capture(page, "09-agreement-exchange-list.png");

    const employeeExchangeCard = page.locator("article").filter({
      hasText: `#${demoRequestIds.agreement}`,
    });
    await expect(employeeExchangeCard).toBeVisible();
    const detailsResponsePromise = waitForApiResponse(
      page,
      "GET",
      "/api/agreement-exchanges/",
    );
    await employeeExchangeCard
      .getByRole("link", { name: L.agreementExchange.openDetailsLink })
      .click();
    const detailsResponse = await detailsResponsePromise;
    expect(detailsResponse.ok()).toBeTruthy();

    await expect(
      page.getByRole("link", { name: L.agreementExchange.downloadDocumentLink }).first(),
    ).toBeVisible();
    await capture(page, "10-agreement-exchange-details.png");
  });
});
