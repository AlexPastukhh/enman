import { test, expect } from "@playwright/test";
import { ClientRoutes } from "../energymanagement.client/src/globConstants";
import LoginPage from "./TestPages/LoginPage";
import { RegisterPage } from "./TestPages/RegisterPage";
import { headerConst } from "../energymanagement.client/src/components/Layout/headerConst";
import {  getTestPage } from "./TestPages/TestBase";
const validData = {
  email: "email@gmail.com",
  password: "ValidPassword111!",
};


test("user registers", async ({ page }) => {

  if (process.env.PW_TEST_DEBUG) {
    debugger;
  }

  const tpage = getTestPage(page);
  await page.goto(ClientRoutes.Home.Path);

  const registerLink =await tpage.getByRoleFullName("link", { name: headerConst.registerLinkText} );
  await registerLink.click()

  await expect(page).toHaveURL(ClientRoutes.Register.Path);
  const registerPage = await RegisterPage.CreateAsync(
    tpage,
    validData.email,
    validData.password,
    validData.password
  );
  registerPage.FillForm();
  const hasEmailErrors = await registerPage.emailField.HasErrorsAsync;
  const hasPasswordErrors = await registerPage.passwordField.HasErrorsAsync;
  const hasPasswordConfirmErrors = await registerPage.passwordConfirmField
    .HasErrorsAsync;

  expect(hasEmailErrors).toBeFalsy();
  expect(hasPasswordErrors).toBeFalsy();
  expect(hasPasswordConfirmErrors).toBeFalsy();

  await registerPage.ClickSubmitButton();

  const hasEmailServerErrors = await registerPage.emailField.HasErrorsAsync;
  const hasPasswordServerErrors = await registerPage.passwordField.HasErrorsAsync;
  await expect(hasEmailServerErrors).toBeFalsy();
  await expect(hasPasswordServerErrors).toBeFalsy();

  await expect(page).toHaveURL(ClientRoutes.Login.Path);
});

test("user logins", async ({ page }) => {
  if (process.env.PW_TEST_DEBUG) {
    debugger;
  }
  const tpage = getTestPage(page);
  await page.goto(ClientRoutes.Home.Path);

  await page.getByRole("link", { name: "Login" }).click();

  await expect(page).toHaveURL(ClientRoutes.Login.Path);
  const loginPage = await LoginPage.CreateAsync(
    tpage,
    validData.email,
    validData.password
  );
  await loginPage.FillForm();
  const hasEmailErrors = await loginPage.HasEmailErrorsAsync();
  const hasPasswordErrors = await loginPage.HasPasswordErrorsAsync();
  expect(hasEmailErrors).toBeFalsy();
  expect(hasPasswordErrors).toBeFalsy();

  await loginPage.ClickSubmitButton();
 
  const hasEmailServerErrors = await loginPage.HasEmailErrorsAsync();
  const hasPasswordServerErrors = await loginPage.HasPasswordErrorsAsync();
  expect(hasEmailServerErrors).toBeFalsy();
  expect(hasPasswordServerErrors).toBeFalsy();

  await expect(page).toHaveURL(ClientRoutes.Home.Path); 
  
});
