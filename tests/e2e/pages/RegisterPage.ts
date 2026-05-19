import type { Page } from "@playwright/test";
import { exactTextIgnoreCase } from "../support/locators";

type RegisterValues = {
  email: string;
  password: string;
  passwordConfirmation: string;
};

export class RegisterPage {
  constructor(private readonly page: Page) {}

  async open() {
    await this.page.goto("/register");
  }

  async register(values: RegisterValues) {
    await this.page.getByLabel(exactTextIgnoreCase("Email")).fill(values.email);
    await this.page
      .getByLabel(exactTextIgnoreCase("Пароль"))
      .fill(values.password);
    await this.page
      .getByLabel(exactTextIgnoreCase("Подтвердите пароль"))
      .fill(values.passwordConfirmation);
    await this.page
      .getByRole("button", { name: "Зарегистрироваться", exact: true })
      .click();
  }

  heading() {
    return this.page.getByRole("heading", { name: /Регистрация/, level: 2 });
  }
}
