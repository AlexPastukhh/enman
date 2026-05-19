import { expect, test } from "@playwright/test";
import { LoginPage } from "../pages/LoginPage";
import { waitForApiResponse } from "../support/apiResponse";
import { postWithCsrf } from "../support/clientSetup";
import { uniqueEmail, validPassword } from "../support/testData";

test("user logs in through real client-server flow", async ({ page, request }) => {
  const email = uniqueEmail("login");

  const setupResponse = await postWithCsrf(request, "/api/auth/register", {
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
    "/api/auth/login"
  );

  await loginPage.login({
    email,
    password: validPassword,
  });

  const loginResponse = await loginResponsePromise;

  expect(loginResponse.ok()).toBeTruthy();
  await expect(page).toHaveURL(/\/$/);
});
