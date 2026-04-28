import type { Screen } from "@testing-library/react";
import { SUTFormField } from "./BaseTest";
import { registerConst } from "../../../views/RegisterView/registerConst";

export class RegisterTH {
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
    return new RegisterTH(
      emailField,
      passwordField,
      passwordConfirmField,
      submitButton,
    );
  };
  hasAnyErrorsAsync = async (): Promise<boolean> => {
    const hasEmailErr = await this._emailField.HasError();
    const hasPasswordErr = await this._passwordField.HasError();
    const hasPasswordConfirmErr = await this._passwordConfirmField.HasError();
    return hasEmailErr || hasPasswordErr || hasPasswordConfirmErr;
  };

  hasOnlyErrorMessages = async (expectedMessages: string[]): Promise<boolean> => {
    const currentMessages = [
      await this._emailField.TryGetErrorMessage(),
      await this._passwordField.TryGetErrorMessage(),
      await this._passwordConfirmField.TryGetErrorMessage(),
    ].filter((msg) => msg !== null);
    return currentMessages.every((msg)=> expectedMessages.includes(msg));

  };
}
