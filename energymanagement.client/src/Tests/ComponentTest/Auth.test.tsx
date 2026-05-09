/**
 * @vitest environment jsdom
 */
import { cleanup, waitFor } from "@testing-library/react";
import type { UserEvent } from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

vi.mock("*.svg", () => ({
  default: () => null,
}));

afterEach(() => {
  cleanup();
});

const { mockedLogin, mockedRegisterInd } = vi.hoisted(() => ({
  mockedLogin: vi.fn(),
  mockedRegisterInd: vi.fn(),
}));

vi.mock("../../MutationFns/login", () => ({
  login: mockedLogin,
  __esModule: true,
}));

vi.mock("../../MutationFns/registerIndClient", () => ({
  registerIndClient: mockedRegisterInd,
  __esModule: true,
}));

beforeEach(() => {
  mockedLogin.mockReset();
  mockedRegisterInd.mockReset();
});

import { Login } from "../../views/LoginView/Login";
import { Register } from "../../views/RegisterView/Register";
import { ClientRoutes, getMessageFromErrorCode } from "../../globConstants";
import { InvalidTestData, ValidTestData } from "./TestClasses/TestData";
import { LoginTestComp } from "./TestClasses/LoginTH";
import { RegisterTestComp } from "./TestClasses/RegisterTH";
import { createTestRoute, renderComponentRoute } from "./TestClasses/TestSetup";

const expectEquivalentMessages = (
  actualMessages: string[],
  expectedErrorCodes: string[],
) => {
  const expectedMessages = expectedErrorCodes.map(getMessageFromErrorCode);
  expect(actualMessages).toHaveLength(expectedMessages.length);
  expect(actualMessages).toEqual(expect.arrayContaining(expectedMessages));
};

const expectEquivalentFieldMessages = (
  actualMessages: (string | null)[],
  expectedErrorCodes: (string | null)[],
) => {
  const expectedMessages = expectedErrorCodes.map((errorCode) =>
    errorCode ? getMessageFromErrorCode(errorCode) : null,
  );
  expect(actualMessages).toEqual(expectedMessages);
};

const setUpRegisterTest = (): {
  user: UserEvent;
  registerHelper: RegisterTestComp;
} => {
  const { user, screen } = renderComponentRoute(
    [
      createTestRoute(
        ClientRoutes.Register.Path,
        <Register setRootError={vi.fn()} />,
      ),
      createTestRoute(ClientRoutes.Login.Path, <div>Login Page</div>),
    ],
    ClientRoutes.Register.Path,
  );

  return { user, registerHelper: RegisterTestComp.Create(screen) };
};

const setUpLoginTest = (): {
  user: UserEvent;
  loginHelper: LoginTestComp;
} => {
  const { user, screen } = renderComponentRoute(
    [
      createTestRoute(ClientRoutes.Login.Path, <Login setRootError={vi.fn()} />),
      createTestRoute(ClientRoutes.Home.Path, <div>Home Page</div>),
    ],
    ClientRoutes.Login.Path,
  );

  return { user, loginHelper: LoginTestComp.Create(screen) };
};

describe("Register", () => {
  it("renders without errors", async () => {
    const { registerHelper: registerComp } = setUpRegisterTest();

    expect(registerComp.hasAnyErrorsNow()).toBe(false);
  });

  it.each(ValidTestData.validRegisterData)(
    "does not show validation errors for valid field values",
    async (validData) => {
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      await registerComp.emailField.FillAsync(user, validData.email);
      const emailErrors = registerComp.emailField.TryGetErrorMessageDebounced();

      await registerComp.passwordField.FillAsync(user, validData.password);
      const passwordErrors = registerComp.passwordField.TryGetErrorMessageDebounced();

      await registerComp.passwordConfirmField.FillAsync(
        user,
        validData.passwordConfirmation,
      );
      const passwordConfirmErrors =
        registerComp.passwordConfirmField.TryGetErrorMessageDebounced();

      const actualErrors = await Promise.all([
        emailErrors,
        passwordErrors,
        passwordConfirmErrors,
      ]);

      expectEquivalentFieldMessages(actualErrors, [null, null, null]);
    },
  );

  it.each(InvalidTestData.invalidRegisterData)(
    "shows exact validation errors after invalid submit",
    async (invalidData) => {
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      await registerComp.emailField.FillAsync(user, invalidData.email);
      await registerComp.passwordField.FillAsync(user, invalidData.password);
      await registerComp.passwordConfirmField.FillAsync(
        user,
        invalidData.passwordConfirmation,
      );
      await user.click(registerComp.submitButton);

      expectEquivalentMessages(
        registerComp.getVisibleErrorMessagesNow(),
        invalidData.expectedErrors,
      );
    },
  );

  it.each(InvalidTestData.mixedRegisterData)(
    "shows exact validation UX for mixed register field values",
    async (mixedData) => {
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      await registerComp.emailField.FillAsync(user, mixedData.email);
      const emailErrors = registerComp.emailField.TryGetErrorMessageDebounced();

      await registerComp.passwordField.FillAsync(user, mixedData.password);
      const passwordErrors = registerComp.passwordField.TryGetErrorMessageDebounced();

      await registerComp.passwordConfirmField.FillAsync(
        user,
        mixedData.passwordConfirmation,
      );
      const passwordConfirmErrors =
        registerComp.passwordConfirmField.TryGetErrorMessageDebounced();

      const actualErrors = await Promise.all([
        emailErrors,
        passwordErrors,
        passwordConfirmErrors,
      ]);

      expectEquivalentFieldMessages(actualErrors, mixedData.expectedFieldErrors);
    },
  );

  it.each(ValidTestData.validRegisterData)(
    "submits valid register data",
    async (validData) => {
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      mockedRegisterInd.mockResolvedValueOnce(
        new Response(null, { status: 200 }),
      );

      await registerComp.emailField.FillAsync(user, validData.email);
      await registerComp.passwordField.FillAsync(user, validData.password);
      await registerComp.passwordConfirmField.FillAsync(
        user,
        validData.passwordConfirmation,
      );
      await user.click(registerComp.submitButton);

      await waitFor(() => {
        expect(mockedRegisterInd).toHaveBeenCalledTimes(1);
      });
      expect(mockedRegisterInd.mock.calls[0][0]).toEqual(validData);
    },
  );

  it.each(InvalidTestData.invalidRegisterData)(
    "does not submit invalid register data",
    async (invalidData) => {
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      mockedRegisterInd.mockResolvedValueOnce(
        new Response(null, { status: 200 }),
      );

      await registerComp.emailField.FillAsync(user, invalidData.email);
      await registerComp.passwordField.FillAsync(user, invalidData.password);
      await registerComp.passwordConfirmField.FillAsync(
        user,
        invalidData.passwordConfirmation,
      );
      await user.click(registerComp.submitButton);

      expect(mockedRegisterInd).not.toHaveBeenCalled();
    },
  );
});

describe("Login", () => {
  it("renders without errors", async () => {
    const { loginHelper: loginComp } = setUpLoginTest();

    expect(loginComp.hasAnyErrorsNow()).toBe(false);
  });

  it.each(ValidTestData.validLoginData)(
    "does not show validation errors for valid field values",
    async (validData) => {
      const { user, loginHelper: loginComp } = setUpLoginTest();

      await loginComp.emailField.FillAsync(user, validData.email);
      const emailErrors = loginComp.emailField.TryGetErrorMessageDebounced();

      await loginComp.passwordField.FillAsync(user, validData.password);
      const passwordErrors = loginComp.passwordField.TryGetErrorMessageDebounced();

      const actualErrors = await Promise.all([emailErrors, passwordErrors]);

      expectEquivalentFieldMessages(actualErrors, [null, null]);
    },
  );

  it.each(InvalidTestData.invalidLoginData)(
    "shows exact validation errors after invalid submit",
    async (invalidData) => {
      const { user, loginHelper: loginComp } = setUpLoginTest();

      await loginComp.emailField.FillAsync(user, invalidData.email);
      await loginComp.passwordField.FillAsync(user, invalidData.password);
      await user.click(loginComp.submitButton);

      expectEquivalentMessages(
        loginComp.getVisibleErrorMessagesNow(),
        invalidData.expectedErrors,
      );
    },
  );

  it.each(InvalidTestData.mixedLoginData)(
    "shows exact validation UX for mixed login field values",
    async (mixedData) => {
      const { user, loginHelper: loginComp } = setUpLoginTest();

      await loginComp.emailField.FillAsync(user, mixedData.email);
      const emailErrors = loginComp.emailField.TryGetErrorMessageDebounced();

      await loginComp.passwordField.FillAsync(user, mixedData.password);
      const passwordErrors = loginComp.passwordField.TryGetErrorMessageDebounced();

      const actualErrors = await Promise.all([emailErrors, passwordErrors]);

      expectEquivalentFieldMessages(actualErrors, mixedData.expectedFieldErrors);
    },
  );

  it.each(ValidTestData.validLoginData)(
    "submits valid login data",
    async (validData) => {
      const { user, loginHelper: loginComp } = setUpLoginTest();

      mockedLogin.mockResolvedValueOnce(undefined);

      await loginComp.emailField.FillAsync(user, validData.email);
      await loginComp.emailField.WaitForDebouncedValidationAsync();
      await loginComp.passwordField.FillAsync(user, validData.password);
      await loginComp.passwordField.WaitForDebouncedValidationAsync();
      await user.click(loginComp.submitButton);

      await waitFor(() => {
        expect(mockedLogin).toHaveBeenCalledTimes(1);
      });
      expect(mockedLogin.mock.calls[0][0]).toEqual(validData);
    },
  );

  it.each(InvalidTestData.invalidLoginData)(
    "does not submit invalid login data",
    async (invalidData) => {
      const { user, loginHelper: loginComp } = setUpLoginTest();

      mockedLogin.mockResolvedValueOnce(undefined);

      await loginComp.emailField.FillAsync(user, invalidData.email);
      await loginComp.passwordField.FillAsync(user, invalidData.password);
      await user.click(loginComp.submitButton);

      expect(mockedLogin).not.toHaveBeenCalled();
    },
  );
});
