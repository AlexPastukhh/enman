import { expect, test } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import { uniqueEmail, validPassword } from "../support/testData";

test("user sees created request in My Requests", async ({ page, request }) => {
  const email = uniqueEmail("my-requests");

  const setupResponse = await request.post("/api/l1/auth/register", {
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

  const applicantResponse = await page.request.post(
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

  const createRequestResponse = await page.request.post("/api/l1/requests", {
    data: {
      details: "Подключение объекта к электрическим сетям",
      address: {
        postalCode: "658480",
        region: "Алтайский край",
        city: "Заринск",
        street: "Ленина",
        house: "10",
        building: null,
        apartment: null,
      },
    },
  });
  expect(createRequestResponse.ok()).toBeTruthy();

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
});
