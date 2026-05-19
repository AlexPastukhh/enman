import { expect, test } from "@playwright/test";
import {
  createConnectionRequestForExistingApplicant,
  createIndividualApplicantParty,
  registerAndLoginClient,
} from "../support/clientSetup";
import { waitForApiResponse } from "../support/apiResponse";
import { cardWithText, L, statusInCard } from "../support/locators";

const objectAddressText = "658480, Алтайский край, Заринск, Ленина, 10";

test("user sees created request in My Requests", async ({ page, request }) => {
  const { email } = await registerAndLoginClient(page, request, "my-requests");
  const applicantPartyId = await createIndividualApplicantParty(page, email);
  await createConnectionRequestForExistingApplicant(page, applicantPartyId);

  const myRequestsResponsePromise = waitForApiResponse(
    page,
    "GET",
    "/api/requests",
  );

  await page.goto("/requests");
  const myRequestsResponse = await myRequestsResponsePromise;
  expect(myRequestsResponse.ok()).toBeTruthy();

  await expect(
    page.getByRole("heading", { name: "Мои заявки", exact: true }),
  ).toBeVisible();
  const createdRequestCard = cardWithText(
    page,
    "Подключение объекта к электрическим сетям",
  );
  await expect(createdRequestCard).toBeVisible();
  await expect(statusInCard(createdRequestCard, L.status.inReview)).toBeVisible();
  await expect(page.getByText(objectAddressText, { exact: true })).toBeVisible();
  await expect(
    page.getByRole("link", { name: /Открыть детали Заявка/ }),
  ).toBeVisible();
});
