import { expect, test } from "@playwright/test";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
  registerAndLoginL1Client,
} from "../support/l1ClientSetup";
import { waitForApiResponse } from "../support/apiResponse";

test("user sees created request in My Requests", async ({ page, request }) => {
  const { email } = await registerAndLoginL1Client(page, request, "my-requests");
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);

  const myRequestsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/l1/requests",
  );

  await page.goto("/requests");
  const myRequestsResponse = await myRequestsResponsePromise;
  expect(myRequestsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: "Мои заявки", exact: true }),
  ).toBeVisible();
  await expect(page.getByText("InReview", { exact: true })).toBeVisible();
  await expect(
    page.getByText("Подключение объекта к электрическим сетям"),
  ).toBeVisible();
  await expect(page.getByText(/Алтайский край/)).toBeVisible();
  await expect(page.getByText(/Заринск/)).toBeVisible();
  await expect(
    page.getByRole("link", { name: /Открыть детали Заявка/ }),
  ).toBeVisible();
});
