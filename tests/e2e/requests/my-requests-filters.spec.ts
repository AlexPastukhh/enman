import { expect, test } from "@playwright/test";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
  registerAndLoginClient,
} from "../support/clientSetup";
import { waitForApiResponse } from "../support/apiResponse";
import { cardWithText, L, statusInCard } from "../support/locators";

test("user filters My Requests by status", async ({ page, request }) => {
  const { email } = await registerAndLoginClient(
    page,
    request,
    "my-requests-filters",
  );
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);

  await page.goto("/requests");
  await expect(
    page.getByText("Подключение объекта к электрическим сетям"),
  ).toBeVisible();

  const filteredResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/requests?status=InReview",
  );
  await page.getByLabel("Статус").selectOption("InReview");
  const filteredResponse = await filteredResponsePromise;
  expect(filteredResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests\?status=InReview/);
  const filteredRequestCard = cardWithText(
    page,
    "Подключение объекта к электрическим сетям",
  );
  await expect(filteredRequestCard).toBeVisible();
  await expect(statusInCard(filteredRequestCard, L.status.inReview)).toBeVisible();

  const unfilteredResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/requests",
  );
  await page
    .getByRole("region", { name: "Фильтры" })
    .getByRole("button", { name: "Сбросить фильтры" })
    .click();
  const unfilteredResponse = await unfilteredResponsePromise;
  expect(unfilteredResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests$/);
});

test("filtered empty state can be reset", async ({ page, request }) => {
  const { email } = await registerAndLoginClient(
    page,
    request,
    "my-requests-filters-empty",
  );
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);

  await page.goto("/requests?status=Approved");

  const filteredEmptyState = page.getByRole("region", {
    name: /Заявок с выбранным фильтром не найдено/i,
  });

  await expect(filteredEmptyState).toBeVisible();

  const unfilteredResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/requests",
  );
  await filteredEmptyState
    .getByRole("button", { name: "Сбросить фильтры" })
    .click();

  const unfilteredResponse = await unfilteredResponsePromise;
  expect(unfilteredResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests$/);
});
