import type { Locator, Page } from "@playwright/test";
import { ClientRoute, ClientRoutes } from "../../energymanagement.client/src/globConstants";
import { loginConst } from "../../energymanagement.client/src/views/LoginView/loginConst";
import { SUTFormField, TestBase, type TestPage } from "./TestBase";


export default class LoginPage extends TestBase {
  private loginRoute: ClientRoute = ClientRoutes.Login;
  private password;
  private email;

  emailField:SUTFormField
  passwordField:SUTFormField
  private submitButton: Locator;

  loginPath = this.loginRoute.Path;

  private constructor(
    page: Page,
    email: string,
    password: string,
    emailField:SUTFormField,
    passwordField:SUTFormField,
    submitButton: Locator
  ) {
    super(page);
    this.email = email;
    this.password = password;
    this.emailField = emailField;
    this.passwordField = passwordField;
    this.submitButton = submitButton;
  }
  static CreateAsync = async (page:TestPage,email: string, password: string) => {
    const emailField = await SUTFormField.Create(page,loginConst.emailLabel)
    const passwordField = await SUTFormField.Create(page,loginConst.passwordLabel)
    const submitButton = LoginPage.findSubmitButton(page);
    return new LoginPage(
      page,
      email,
      password,
      emailField,
      passwordField,
      submitButton
    );
  };
  
  private static findSubmitButton =(page:TestPage)=>{
    return page.getByRoleFullName("button",{name:loginConst.submitButtonText})
  }

  get Route(): string {
    return this.loginRoute.Path;
  }
  FillForm = async () => {
    await this.emailField.Fill(this.email);
    await this.passwordField.Fill(this.password);
  };
  HasEmailErrorsAsync = async () => {
    return await this.emailField.HasErrorsAsync();
  };
  HasPasswordErrorsAsync = async () => {
    return await this.passwordField.HasErrorsAsync();
  };
  
  ClickSubmitButton = async () => {
    await this.submitButton.click();
  };
}
