import { expect, test, type Page } from "@playwright/test";
import path from "node:path";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import {
  demoCredentials,
  demoRequestIds,
  demoText,
  seedE2eDemoData,
} from "../support/demoSeed";
import { L } from "../support/locators";

const fixtureDocumentPath = path.join(
  process.cwd(),
  "tests/e2e/fixtures/demo-agreement.pdf",
);

async function login(page: Page, email: string) {
  const loginPage = new LoginPage(page);
  await loginPage.open();
  await expect(loginPage.heading()).toBeVisible();
  await loginPage.login({
    email,
    password: demoCredentials.password,
  });
  await expect(page).toHaveURL(/\/$/);
}

test.beforeEach(() => {
  seedE2eDemoData();
});

test("employee starts exchange and client sends counter-proposal with document download", async ({
  page,
}) => {
  await login(page, demoCredentials.employeeEmail);

  const employeeDetailsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/employee/requests/",
  );
  await page.goto("/employee/requests/9005");
  const employeeDetailsResponse = await employeeDetailsResponsePromise;
  expect(employeeDetailsResponse.ok()).toBeTruthy();

  await expect(page.getByText(demoText.agreementRequestDetails)).toBeVisible();

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
    page.getByRole("heading", { name: L.headings.employeeAgreementExchanges }),
  ).toBeVisible();
  const employeeExchangeCard = page.locator("article").filter({
    hasText: `#${demoRequestIds.agreement}`,
  });
  await expect(employeeExchangeCard).toBeVisible();
  await expect(
    employeeExchangeCard.getByRole("link", {
      name: L.agreementExchange.openDetailsLink,
    }),
  ).toBeVisible();

  await page.getByRole("button", { name: "Выйти" }).click();
  await expect(
    page
      .getByLabel("Основная навигация")
      .getByRole("link", { name: "Войти" }),
  ).toBeVisible();

  await login(page, demoCredentials.clientEmail);

  const clientAgreementsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/agreement-exchanges",
  );
  await page.goto("/agreements");
  const clientAgreementsResponse = await clientAgreementsResponsePromise;
  expect(clientAgreementsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: L.headings.clientAgreementExchanges }),
  ).toBeVisible();
  const clientExchangeCard = page.locator("article").filter({
    hasText: `#${demoRequestIds.agreement}`,
  });
  await expect(clientExchangeCard).toBeVisible();

  const detailsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/agreement-exchanges/",
  );
  await clientExchangeCard
    .getByRole("link", { name: L.agreementExchange.openDetailsLink })
    .click();
  const detailsResponse = await detailsResponsePromise;
  expect(detailsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("link", { name: L.agreementExchange.downloadDocumentLink }).first(),
  ).toBeVisible();

  const downloadPromise = page.waitForEvent("download");
  await page.getByRole("link", { name: L.agreementExchange.downloadDocumentLink }).first().click();
  const download = await downloadPromise;
  expect(download.suggestedFilename()).toBeTruthy();

  await page
    .locator('input[type="file"][id^="send-proposal-document-"]')
    .setInputFiles(fixtureDocumentPath);
  await page.getByLabel(L.agreementExchange.commentLabel).fill("Client E2E counter-proposal.");

  const sendProposalResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/agreement-exchange/proposals",
  );
  await page.getByRole("button", { name: L.buttons.sendProposalVersion }).click();
  const sendProposalResponse = await sendProposalResponsePromise;
  expect(sendProposalResponse.ok()).toBeTruthy();

  await expect(page.getByText("Client E2E counter-proposal.")).toBeVisible();
});
