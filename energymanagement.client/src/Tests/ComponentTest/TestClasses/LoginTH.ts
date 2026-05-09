import type { Screen } from "@testing-library/react";
import { loginConst } from "../../../views/LoginView/loginConst";
import { SUTFormField } from "./BaseTest";

export class LoginTestComp {
  private _emailField: SUTFormField;
  private _passwordField: SUTFormField;
  private _submitButton: HTMLButtonElement;

  get emailField() {
    return this._emailField;
  }

  get passwordField() {
    return this._passwordField;
  }

  get submitButton() {
    return this._submitButton;
  }

  private constructor(
    emailField: SUTFormField,
    passwordField: SUTFormField,
    submitButton: HTMLButtonElement,
  ) {
    this._emailField = emailField;
    this._passwordField = passwordField;
    this._submitButton = submitButton;
  }

  static Create = (screen: Screen) => {
    const emailField = SUTFormField.Create(screen, loginConst.emailLabel);
    const passwordField = SUTFormField.Create(screen, loginConst.passwordLabel);
    const submitButton = screen.getByRole("button", {
      name: loginConst.submitButtonText,
    }) as HTMLButtonElement;

    return new LoginTestComp(emailField, passwordField, submitButton);
  };

  hasAnyErrorsNow = (): boolean => {
    return this._emailField.HasErrorNow() || this._passwordField.HasErrorNow();
  };

  getErrorMessagesNow = (): (string | null)[] => {
    const emailErr = this._emailField.TryGetErrorMessageNow();
    const passwordErr = this._passwordField.TryGetErrorMessageNow();
    return [emailErr, passwordErr];
  };

  getVisibleErrorMessagesNow = (): string[] => {
    return this.getErrorMessagesNow().filter(
      (msg): msg is string => msg !== null
    );
  };
}
