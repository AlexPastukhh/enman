import { expect, test } from "@playwright/test";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
  getLatestMyRequestId,
  registerAndLoginL1Client,
} from "../support/l1ClientSetup";
import { waitForApiResponse } from "../support/apiResponse";

const objectAddressText = "658480, Алтайский край, Заринск, Ленина, 10";

test("user sees own request details", async ({ page, request }) => {
  const { email } = await registerAndLoginL1Client(
    page,
    request,
    "my-request-details",
  );
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);
  const requestId = await getLatestMyRequestId(page);

  const detailsResponsePromise = waitForApiResponse(
    page,
    "GET",
    `/api/l1/requests/${requestId}`,
  );

  await page.goto(`/requests/${requestId}`);
  const detailsResponse = await detailsResponsePromise;
  expect(detailsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: "Детали заявки", exact: true }),
  ).toBeVisible();
  await expect(
    page.getByRole("heading", { name: `Заявка #${requestId}` }),
  ).toBeVisible();
  await expect(page.getByText("InReview", { exact: true })).toBeVisible();
  await expect(
    page.getByText("Подключение объекта к электрическим сетям"),
  ).toBeVisible();
  await expect(page.getByText(objectAddressText, { exact: true })).toBeVisible();
  await expect(page.getByText("Решение по заявке пока не вынесено.")).toBeVisible();
});

test("missing request details show not found state", async ({ page, request }) => {
  await registerAndLoginL1Client(page, request, "my-request-details-missing");

  await page.goto("/requests/999999999");

  await expect(page.getByText("Заявка не найдена.")).toBeVisible();
  await expect(
    page.getByRole("link", { name: "Вернуться к моим заявкам" }),
  ).toBeVisible();
});
