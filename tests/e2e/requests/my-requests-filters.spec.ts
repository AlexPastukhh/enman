import { expect, test } from "@playwright/test";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
  registerAndLoginL1Client,
} from "../support/l1ClientSetup";
import { waitForApiResponse } from "../support/apiResponse";

test("user filters My Requests by status", async ({ page, request }) => {
  const { email } = await registerAndLoginL1Client(
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
    "/api/l1/requests?status=InReview",
  );
  await page.getByLabel("Статус").selectOption("InReview");
  const filteredResponse = await filteredResponsePromise;
  expect(filteredResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests\?status=InReview/);
  await expect(page.getByText("InReview", { exact: true })).toBeVisible();

  const unfilteredResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/requests",
  );
  await page.getByRole("button", { name: "Сбросить фильтры" }).click();
  const unfilteredResponse = await unfilteredResponsePromise;
  expect(unfilteredResponse.ok()).toBeTruthy();

  await expect(page).toHaveURL(/\/requests$/);
});

test("filtered empty state can be reset", async ({ page, request }) => {
  const { email } = await registerAndLoginL1Client(
    page,
    request,
    "my-requests-filters-empty",
  );
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);

  await page.goto("/requests?status=Approved");

  await expect(
    page.getByText("Заявок с выбранным фильтром не найдено."),
  ).toBeVisible();
  await expect(
    page.getByRole("button", { name: "Сбросить фильтры" }),
  ).toBeVisible();
});
