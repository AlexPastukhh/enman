import type { Page } from "@playwright/test";
import { exactTextIgnoreCase } from "../support/locators";

type LoginValues = {
  email: string;
  password: string;
};

export class LoginPage {
  constructor(private readonly page: Page) {}

  async open() {
    await this.page.goto("/login");
  }

  async login(values: LoginValues) {
    await this.page.getByLabel(exactTextIgnoreCase("Email")).fill(values.email);
    await this.page
      .getByLabel(exactTextIgnoreCase("Password"))
      .fill(values.password);
    await this.page
      .getByRole("button", { name: "Login", exact: true })
      .click();
  }

  heading() {
    return this.page.getByRole("heading", { name: "Login", exact: true });
  }
}
