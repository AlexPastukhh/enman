# Test Object Patterns

Status: current test object guidance  
Scope: E2E Page Objects, client Component Objects, locator/data ownership

## 1. Purpose

This file defines how to use Page Object and Component Object patterns without hiding important scenario behavior.

## 2. Terms

### E2E Page Object

A helper for Playwright E2E tests.

It hides locator mechanics and repeated user actions across browser pages/routes.

### Component Object

A helper for client/component tests.

It hides repeated component interactions and exposes detailed UI state assertions.

## 3. E2E Page Object Responsibility

E2E Page Objects may:

```text
- open a page/route;
- locate main fields/buttons/links by role/label;
- perform repeated user actions;
- hide locator mechanics;
- expose repeated high-level page assertions.
```

E2E Page Objects should not:

```text
- own detailed field-validation matrix;
- own ARIA error mapping assertions;
- hide the key expected outcome of the test;
- duplicate business logic;
- wrap one-line actions just for abstraction;
- store scenario data as default state in ordinary cases.
```

## 4. Component Object Responsibility

Component Objects may:

```text
- keep render/userEvent context;
- fill fields;
- blur/touch fields;
- assert field errors;
- assert deferred validation;
- assert aria-describedby / aria-invalid / role alert;
- assert disabled/enabled state;
- assert server error mapping;
- expose field-level locators and UI-state helpers.
```

Component Objects are allowed to be more stateful than E2E Page Objects because component tests own detailed client-visible UI behavior.

## 5. Data Ownership Rule

Distinguish:

```text
locator/config constants
scenario input data
```

### Locator/config constants

Locator/config constants may live inside Page Object or Component Object.

Example:

```ts
submitButton() {
  return this.page.getByRole("button", {
    name: "Register",
    exact: true,
  });
}
```

These values define how the object finds UI elements.

### Scenario input data

Scenario input data should usually be passed to action methods.

Preferred:

```ts
await registerPage.register({
  email,
  password,
  passwordConfirmation: password,
});
```

Less preferred:

```ts
const registerPage = new RegisterPage(page, email, password, password);
await registerPage.fillForm();
```

Reason:

```text
- Page Object represents the page, not one scenario data set;
- test method shows scenario data clearly;
- one Page Object can run several attempts;
- less object state reduces async/race mistakes;
- important E2E story remains readable.
```

## 6. Page Object May Store Data Only As Exception

Page Object may store data only when it is a deliberate fixture-style abstraction used by several tests.

Example:

```text
Default registration fixture object for repeated setup.
```

Even then, the test should remain clear about the scenario and expected outcome.

## 7. What To Keep In The Test Method

Keep in the test method:

```text
- unique scenario path;
- important setup;
- scenario input data;
- key assertions;
- waitForResponse / server communication checks;
- one-off actions;
- the final expected outcome.
```

Move to Page Object/helper:

```text
- repeated navigation;
- repeated form actions;
- stable page locators;
- test data generation;
- authentication/session setup;
- repeated low-level wait mechanics.
```

Do not hide the core of the test behind a method like:

```text
performFullSuccessfulRegistrationAndCheckEverything()
```

## 8. Current E2E Object Problems To Fix

Current E2E objects should be refactored because:

```text
SUTFormField:
- behaves more like Component/Form Object than E2E helper;
- checks field errors in E2E happy path;
- uses role textbox for all inputs, problematic for password input;
- has isVisible reference bug.

RegisterPage:
- stores email/password/passwordConfirm;
- FillForm is not async while calling async field fill methods;
- contains field-error helpers;
- behaves more like form/component object than thin E2E Page Object.

LoginPage:
- FillForm is async, but it still stores data and exposes field-error helpers.
```

## 9. Locator Helper Rule

Do not create a global locator wrapper unless it solves a real repeated problem.

If exact accessible-name matching is needed:

```ts
page.getByRole("link", { name: "Login", exact: true });
```

If exact case-insensitive matching is needed:

```ts
page.getByRole("link", { name: exactName("Login") });
```

where `exactName()` escapes regex special characters.

Do not build raw regex from UI text without escaping.
