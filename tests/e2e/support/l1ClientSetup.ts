import { expect, type APIRequestContext, type Page } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "./apiResponse";
import { uniqueEmail, validPassword } from "./testData";

type RequestPostOptions = Parameters<APIRequestContext["post"]>[1];

export const validRequestAddress = {
  postalCode: "658480",
  region: "Алтайский край",
  city: "Заринск",
  street: "Ленина",
  house: "10",
  building: null,
  apartment: null,
} as const;

export async function getAntiforgeryToken(request: APIRequestContext) {
  const response = await request.get("/api/antiforgery/token");
  expect(response.ok()).toBeTruthy();

  const body = (await response.json()) as { requestToken?: string };
  expect(body.requestToken).toBeTruthy();
  return body.requestToken!;
}

export async function postWithCsrf(
  request: APIRequestContext,
  url: string,
  options: RequestPostOptions = {},
) {
  const requestToken = await getAntiforgeryToken(request);
  return request.post(url, {
    ...options,
    headers: {
      ...options.headers,
      "X-CSRF-TOKEN": requestToken,
    },
  });
}

export async function registerAndLoginL1Client(
  page: Page,
  request: APIRequestContext,
  emailPrefix: string,
) {
  const email = uniqueEmail(emailPrefix);

  const setupResponse = await postWithCsrf(request, "/api/l1/auth/register", {
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

  return { email };
}

export async function createIndividualApplicantParty(page: Page, email: string) {
  const applicantResponse = await postWithCsrf(
    page.request,
    "/api/l1/applicant-parties/individual",
    {
      data: {
        fullName: {
          firstName: "Ivan",
          middleName: "Ivanovich",
          lastName: "Ivanov",
        },
        email,
        phoneNumber: "+79001234567",
      },
    },
  );
  expect(applicantResponse.ok()).toBeTruthy();

  const applicant = (await applicantResponse.json()) as {
    applicantPartyId: number;
  };
  expect(applicant.applicantPartyId).toBeGreaterThan(0);

  return applicant.applicantPartyId;
}

export async function createConnectionRequestForExistingApplicant(
  page: Page,
  applicantPartyId: number,
  details = "Подключение объекта к электрическим сетям",
) {
  const createRequestResponse = await postWithCsrf(
    page.request,
    "/api/l1/requests",
    {
      data: {
        applicantContextType: "Existing",
        existingApplicantPartyId: applicantPartyId,
        newApplicantParty: null,
        details,
        address: validRequestAddress,
      },
    },
  );
  expect(createRequestResponse.ok()).toBeTruthy();
}

export async function getLatestMyRequestId(page: Page) {
  const myRequestsResponse = await page.request.get("/api/l1/requests");
  expect(myRequestsResponse.ok()).toBeTruthy();

  const requests = (await myRequestsResponse.json()) as Array<{
    requestId?: number;
  }>;
  expect(requests.length).toBeGreaterThan(0);
  expect(requests[0].requestId).toBeGreaterThan(0);

  return requests[0].requestId!;
}
