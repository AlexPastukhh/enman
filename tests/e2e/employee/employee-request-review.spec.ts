import { expect, test } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import {
  demoCredentials,
  demoRequestIds,
  demoText,
  seedE2eDemoData,
} from "../support/demoSeed";
import { L } from "../support/locators";

test.beforeEach(() => {
  seedE2eDemoData();
});

test("employee reviews and approves a seeded request", async ({ page }) => {
  const loginPage = new LoginPage(page);
  await loginPage.open();
  await expect(loginPage.heading()).toBeVisible();

  await loginPage.login({
    email: demoCredentials.employeeEmail,
    password: demoCredentials.password,
  });

  await expect(page).toHaveURL(/\/$/);

  const dashboardResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/employee/requests",
  );
  await page.goto("/employee/requests");
  const dashboardResponse = await dashboardResponsePromise;
  expect(dashboardResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: L.headings.employeeDashboard }),
  ).toBeVisible();

  const reviewRequestCard = page.locator("article").filter({
    has: page.getByRole("heading", {
      name: `Заявка #${demoRequestIds.review}`,
    }),
  });
  await expect(reviewRequestCard).toBeVisible();
  await expect(reviewRequestCard.getByText(L.status.inReview)).toBeVisible();

  const detailsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/employee/requests/",
  );
  await reviewRequestCard
    .getByRole("link", { name: /Открыть детали/ })
    .click();
  const detailsResponse = await detailsResponsePromise;
  expect(detailsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: L.headings.employeeRequestDetails }),
  ).toBeVisible();
  await expect(page.getByText(demoText.reviewRequestDetails)).toBeVisible();

  const startReviewResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/review/start",
  );
  await page.getByRole("button", { name: L.buttons.startReview }).click();
  const startReviewResponse = await startReviewResponsePromise;
  expect(startReviewResponse.ok()).toBeTruthy();

  const verificationButton = page.getByRole("button", {
    name: /Проверить данные/,
  });
  if (await verificationButton.isVisible()) {
    const verificationResponsePromise = waitForApiResponse(
      page,
      "POST",
      "/applicant-party/verification/run",
    );
    await verificationButton.click();
    const verificationResponse = await verificationResponsePromise;
    expect(verificationResponse.ok()).toBeTruthy();
    await expect(page.getByText("Данные проверены").first()).toBeVisible();
  }

  const approveResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/review/approve",
  );
  await page.getByRole("button", { name: L.buttons.approveReview }).click();
  const approveResponse = await approveResponsePromise;
  expect(approveResponse.ok()).toBeTruthy();

  await expect(page.getByText(L.status.approved).first()).toBeVisible();
});
