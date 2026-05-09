import type { Screen } from "@testing-library/react";
import { SUTFormField } from "./BaseTest";
import { registerConst } from "../../../views/RegisterView/registerConst";

export class RegisterTestComp {
  private _emailField: SUTFormField;
  private _passwordField: SUTFormField;
  private _passwordConfirmField: SUTFormField;
  private _submitButton: HTMLButtonElement;

  get emailField() {
    return this._emailField;
  }
  get passwordField() {
    return this._passwordField;
  }
  get passwordConfirmField() {
    return this._passwordConfirmField;
  }
  get submitButton() {
    return this._submitButton;
  }

  private constructor(
    emailField: SUTFormField,
    passwordField: SUTFormField,
    passwordConfirmField: SUTFormField,
    submitButton: HTMLButtonElement,
  ) {
    this._emailField = emailField;
    this._passwordField = passwordField;
    this._passwordConfirmField = passwordConfirmField;
    this._submitButton = submitButton;
  }
  static Create = (screen: Screen) => {
    const emailField = SUTFormField.Create(screen, registerConst.emailLabel);
    const passwordField = SUTFormField.Create(
      screen,
      registerConst.passwordLabel,
    );
    const passwordConfirmField = SUTFormField.Create(
      screen,
      registerConst.passwordConfirmLabel,
    );
    const submitButton = screen.getByRole("button", {
      name: registerConst.submitButtonAriaLabel,
    }) as HTMLButtonElement;
    return new RegisterTestComp(
      emailField,
      passwordField,
      passwordConfirmField,
      submitButton,
    );
  };
  hasAnyErrorsNow = (): boolean => {
    return (
      this._emailField.HasErrorNow() ||
      this._passwordField.HasErrorNow() ||
      this._passwordConfirmField.HasErrorNow()
    );
  };

  getErrorMessagesNow = (): (string | null)[] => {
    const emailErr = this._emailField.TryGetErrorMessageNow();
    const passwordErr = this._passwordField.TryGetErrorMessageNow();
    const passwordConfirmErr = this._passwordConfirmField.TryGetErrorMessageNow();
    return [emailErr, passwordErr, passwordConfirmErr];
  };

  getVisibleErrorMessagesNow = (): string[] => {
    return this.getErrorMessagesNow().filter(
      (msg): msg is string => msg !== null
    );
  };
}
