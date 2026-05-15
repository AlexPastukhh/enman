/**
 * @vitest environment jsdom
 */
import { act, cleanup, fireEvent, waitFor } from "@testing-library/react";
import type { UserEvent } from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

vi.mock("*.svg", () => ({
  default: () => null,
}));

afterEach(() => {
  cleanup();
  vi.useRealTimers();
});

const { mockedLogin, mockedRegisterInd } = vi.hoisted(() => ({
  mockedLogin: vi.fn(),
  mockedRegisterInd: vi.fn(),
}));

vi.mock("../../features/auth/login/api/loginClientAccount", () => ({
  loginClientAccount: mockedLogin,
  __esModule: true,
}));

vi.mock("../../features/auth/register/api/registerClientAccount", () => ({
  registerClientAccount: mockedRegisterInd,
  __esModule: true,
}));

beforeEach(() => {
  mockedLogin.mockReset();
  mockedRegisterInd.mockReset();
});

import { LoginForm } from "../../features/auth/login/ui/LoginForm";
import { RegisterForm } from "../../features/auth/register/ui/RegisterForm";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { getMessageFromErrorCode } from "../../shared/errors/clientErrorMessages";
import { InvalidTestData, ValidTestData } from "./TestClasses/TestData";
import { LoginTestComp } from "./TestClasses/LoginTH";
import { RegisterTestComp } from "./TestClasses/RegisterTH";
import {
  createTestRoute,
  renderComponentRoute,
  type UserEventSetupOptions,
} from "./TestClasses/TestSetup";

const debounceValidationDelayMs = 500;

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

const setUpRegisterTest = (
  userOptions?: UserEventSetupOptions,
): {
  user: UserEvent;
  registerHelper: RegisterTestComp;
} => {
  const { user, screen } = renderComponentRoute(
    [
      createTestRoute(
        clientRoutes.register,
        <RegisterForm />,
      ),
      createTestRoute(clientRoutes.login, <div>Login Page</div>),
    ],
    clientRoutes.register,
    userOptions,
  );

  return { user, registerHelper: RegisterTestComp.Create(screen) };
};

const setUpLoginTest = (
  userOptions?: UserEventSetupOptions,
): {
  user: UserEvent;
  loginHelper: LoginTestComp;
} => {
  const { user, screen } = renderComponentRoute(
    [
      createTestRoute(clientRoutes.login, <LoginForm />),
      createTestRoute(clientRoutes.home, <div>Home Page</div>),
    ],
    clientRoutes.login,
    userOptions,
  );

  return { user, loginHelper: LoginTestComp.Create(screen) };
};

const advanceBeforeDebounceAsync = async () => {
  await act(async () => {
    await vi.advanceTimersByTimeAsync(debounceValidationDelayMs - 1);
  });
};

const advanceThroughDebounceAsync = async () => {
  await act(async () => {
    await vi.advanceTimersByTimeAsync(1);
  });
};

const advanceThroughFullDebounceAsync = async () => {
  await act(async () => {
    await vi.advanceTimersByTimeAsync(debounceValidationDelayMs);
  });
};

const expectRegisterDebouncedValidationUxAsync = async (
  registerComp: RegisterTestComp,
  expectedFieldErrors: (string | null)[],
) => {
  expectEquivalentFieldMessages(registerComp.getErrorMessagesNow(), [
    null,
    null,
    null,
  ]);

  await advanceBeforeDebounceAsync();
  expectEquivalentFieldMessages(registerComp.getErrorMessagesNow(), [
    null,
    null,
    null,
  ]);

  await advanceThroughDebounceAsync();
  expectEquivalentFieldMessages(
    registerComp.getErrorMessagesNow(),
    expectedFieldErrors,
  );
};

const expectLoginDebouncedValidationUxAsync = async (
  loginComp: LoginTestComp,
  expectedFieldErrors: (string | null)[],
) => {
  expectEquivalentFieldMessages(loginComp.getErrorMessagesNow(), [null, null]);

  await advanceBeforeDebounceAsync();
  expectEquivalentFieldMessages(loginComp.getErrorMessagesNow(), [null, null]);

  await advanceThroughDebounceAsync();
  expectEquivalentFieldMessages(
    loginComp.getErrorMessagesNow(),
    expectedFieldErrors,
  );
};

describe("Register", () => {
  it("renders without errors", async () => {
    const { registerHelper: registerComp } = setUpRegisterTest();

    expect(registerComp.hasAnyErrorsNow()).toBe(false);
  });

  it.each(ValidTestData.validRegisterData)(
    "does not show validation errors for valid field values",
    async (validData) => {
      vi.useFakeTimers();
      const { registerHelper: registerComp } = setUpRegisterTest();

      registerComp.emailField.SetValue(validData.email);
      registerComp.passwordField.SetValue(validData.password);
      registerComp.passwordConfirmField.SetValue(validData.passwordConfirmation);

      await expectRegisterDebouncedValidationUxAsync(registerComp, [
        null,
        null,
        null,
      ]);
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
      vi.useFakeTimers();
      const { registerHelper: registerComp } = setUpRegisterTest();

      registerComp.emailField.SetValue(mixedData.email);
      registerComp.passwordField.SetValue(mixedData.password);
      registerComp.passwordConfirmField.SetValue(mixedData.passwordConfirmation);

      await expectRegisterDebouncedValidationUxAsync(
        registerComp,
        mixedData.expectedFieldErrors,
      );
    },
  );

  it.each(InvalidTestData.invalidRegisterUxData)(
    "shows exact validation UX for invalid register field values",
    async (invalidData) => {
      vi.useFakeTimers();
      const { registerHelper: registerComp } = setUpRegisterTest();

      registerComp.emailField.SetValue(invalidData.email);
      registerComp.passwordField.SetValue(invalidData.password);
      registerComp.passwordConfirmField.SetValue(
        invalidData.passwordConfirmation,
      );

      await expectRegisterDebouncedValidationUxAsync(
        registerComp,
        invalidData.expectedFieldErrors,
      );
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
      vi.useFakeTimers();
      const { loginHelper: loginComp } = setUpLoginTest();

      loginComp.emailField.SetValue(validData.email);
      loginComp.passwordField.SetValue(validData.password);

      await expectLoginDebouncedValidationUxAsync(loginComp, [null, null]);
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
      vi.useFakeTimers();
      const { loginHelper: loginComp } = setUpLoginTest();

      loginComp.emailField.SetValue(mixedData.email);
      loginComp.passwordField.SetValue(mixedData.password);

      await expectLoginDebouncedValidationUxAsync(
        loginComp,
        mixedData.expectedFieldErrors,
      );
    },
  );

  it.each(InvalidTestData.invalidLoginUxData)(
    "shows exact validation UX for invalid login field values",
    async (invalidData) => {
      vi.useFakeTimers();
      const { loginHelper: loginComp } = setUpLoginTest();

      loginComp.emailField.SetValue(invalidData.email);
      loginComp.passwordField.SetValue(invalidData.password);

      await expectLoginDebouncedValidationUxAsync(
        loginComp,
        invalidData.expectedFieldErrors,
      );
    },
  );

  it.each(ValidTestData.validLoginData)(
    "submits valid login data",
    async (validData) => {
      vi.useFakeTimers();
      const { loginHelper: loginComp } = setUpLoginTest();

      mockedLogin.mockResolvedValueOnce(undefined);

      loginComp.emailField.SetValue(validData.email);
      loginComp.passwordField.SetValue(validData.password);
      await advanceThroughFullDebounceAsync();
      vi.useRealTimers();

      await act(async () => {
        fireEvent.click(loginComp.submitButton);
      });

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
