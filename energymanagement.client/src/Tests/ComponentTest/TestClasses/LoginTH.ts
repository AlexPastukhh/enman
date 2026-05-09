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

  hasAnyErrorsDebounced = async (): Promise<boolean> => {
    const hasEmailErr = await this._emailField.HasErrorDebounced();
    const hasPasswordErr = await this._passwordField.HasErrorDebounced();
    return hasEmailErr || hasPasswordErr;
  };

  getErrorMessagesNow = (): (string | null)[] => {
    const emailErr = this._emailField.TryGetErrorMessageNow();
    const passwordErr = this._passwordField.TryGetErrorMessageNow();
    return [emailErr, passwordErr];
  };

  getErrorMessagesDebounced = async (): Promise<(string | null)[]> => {
    return Promise.all([
      this._emailField.TryGetErrorMessageDebounced(),
      this._passwordField.TryGetErrorMessageDebounced(),
    ]);
  };

  getVisibleErrorMessagesNow = (): string[] => {
    return this.getErrorMessagesNow().filter(
      (msg): msg is string => msg !== null
    );
  };

  getVisibleErrorMessagesDebounced = async (): Promise<string[]> => {
    return (await this.getErrorMessagesDebounced()).filter(
      (msg): msg is string => msg !== null
    );
  };
}
