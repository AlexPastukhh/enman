import type { Locator, Page } from "@playwright/test";
import { SUTFormField, TestBase, type TestPage } from "./TestBase";
import { registerConst } from "../../energymanagement.client/src/views/RegisterView/registerConst";

export class RegisterPage extends TestBase {
  private email: string;
  private password: string;
  private passwordConfirm: string;

  emailField: SUTFormField;
  passwordField: SUTFormField;
  passwordConfirmField: SUTFormField;
  private submitButton: Locator;
  private constructor(
    page: Page,
    email: string,
    password: string,
    passwordConfirm: string,
    emailField: SUTFormField,
    passwordField: SUTFormField,
    passwordConfirmField: SUTFormField,
    submitButton: Locator
  ) {
    super(page);
    this.email = email;
    this.password = password;
    this.passwordConfirm = passwordConfirm;
    this.emailField = emailField;
    this.passwordField = passwordField;
    this.passwordConfirmField = passwordConfirmField;
    this.submitButton = submitButton;
  }
  static CreateAsync = async (page: TestPage, email: string, password: string, passwordConfirm: string) => {
    const emailField = await SUTFormField.Create(
      page,
      registerConst.emailLabel
    );
    const passwordField = await SUTFormField.Create(
      page,
      registerConst.passwordLabel
    );
    const passwordConfirmField = await SUTFormField.Create(
      page,
      registerConst.passwordConfirmLabel
    );
    const submitButton = RegisterPage.findSubmitButton(page);
    return new RegisterPage(
      page,
      email,
      password,
      passwordConfirm,
      emailField,
      passwordField,
      passwordConfirmField,
      submitButton
    );
  };

  static findSubmitButton = (page: TestPage) => {
    return page.getByRoleFullName("button", { name: registerConst.submitButtonText });
  };
  
  FillForm() {
    this.emailField.Fill(this.email);
    this.passwordField.Fill(this.password);
    this.passwordConfirmField.Fill(this.passwordConfirm);
  }
  ClickSubmitButton=async()=>{
    await this.submitButton.click();
  }
}
