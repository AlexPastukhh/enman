import { expect, test } from "@playwright/test";
import { RegisterPage } from "../pages/RegisterPage";
import { waitForApiResponse } from "../support/apiResponse";
import { uniqueEmail, validPassword } from "../support/testData";

test("user registers through real client-server flow", async ({ page }) => {
  const registerPage = new RegisterPage(page);
  const email = uniqueEmail("register");

  await registerPage.open();
  await expect(registerPage.heading()).toBeVisible();

  const registerResponsePromise = waitForApiResponse(
    page,
    "POST",
    "/api/auth/registerIndividual"
  );

  await registerPage.register({
    email,
    password: validPassword,
    passwordConfirmation: validPassword,
  });

  const registerResponse = await registerResponsePromise;

  expect(registerResponse.ok()).toBeTruthy();
  await expect(page).toHaveURL(/\/login$/i);
  await expect(page.getByRole("heading", { name: "Login", exact: true })).toBeVisible();
});
