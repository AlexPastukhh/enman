/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen } from "@testing-library/react";
import userEvent, { type UserEvent } from "@testing-library/user-event";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

vi.mock("*.svg", () => ({
  default: () => null,
}));

afterEach(() => {
  cleanup();
});

const { mockedRegisterInd } = vi.hoisted(() => ({
  mockedRegisterInd: vi.fn(),
}));

vi.mock("../../MutationFns/registerIndClient", () => ({
  registerIndClient: mockedRegisterInd,
  __esModule: true,
}));

beforeEach(() => {
  mockedRegisterInd.mockReset();
});

import { Register } from "../../views/RegisterView/Register";
import { RegisterTH } from "./TestClasses/RegisterTH";
import { InvalidTestData, ValidTestData } from "./TestClasses/TestData";
import { ClientRoutes } from "../../globConstants";
import {
  createMemoryRouter,
  RouterProvider,
  useRouteError,
} from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

type routerError = {
  message?: string;
};
const RouteError = () => {
  const err = useRouteError() as routerError;
  return (
    <div data-testid="route-error">
      {String(err.message ?? JSON.stringify(err))}
    </div>
  );
};

const setUpRegisterTest = (): {
  user: UserEvent;
  registerHelper: RegisterTH;
} => {

  const routes =  [
      {
        path: ClientRoutes.Register.Path,
        element: <Register setRootError={vi.fn()} />,
        errorElement: <RouteError />,
      },
      {
        path: ClientRoutes.Login.Path,
        element: <div>Login Page</div>,
        errorElement: <RouteError />,
      },
    ];

  const memoryRouter = createMemoryRouter(
   routes,
    { initialEntries: [ClientRoutes.Register.Path] },
  );

  const queryClient = new QueryClient(); // create fresh client per test render
  render(
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={memoryRouter} />
    </QueryClientProvider>,
  );
  const user = userEvent.setup();
  const registerComp = RegisterTH.Create(screen);
  return { user, registerHelper: registerComp };
};

describe("Register", () => {
  it.each(ValidTestData.validRegisterData)(
    "registers successfully",
    async (validData) => {
      //Arrange

      const { user, registerHelper: registerComp } = setUpRegisterTest();

      mockedRegisterInd.mockResolvedValueOnce(
        new Response(null, { status: 200 }),
      );

      //Act
      const hasErrorsOnRender = await registerComp.hasAnyErrorsAsync();

      await registerComp.emailField.FillAsync(user, validData.email);
      const hasEmailErrors = await registerComp.emailField.HasError();

      await registerComp.passwordField.FillAsync(user, validData.password);
      const hasPasswordErrors = await registerComp.passwordField.HasError();

      await registerComp.passwordConfirmField.FillAsync(
        user,
        validData.passwordConfirmation,
      );
      const hasPasswordConfirmErrors =
        await registerComp.passwordConfirmField.HasError();

      await user.click(registerComp.submitButton);

      //Assert
      expect(
        hasEmailErrors ||
          hasPasswordErrors ||
          hasPasswordConfirmErrors ||
          hasErrorsOnRender,
      ).toBe(false);

      expect(mockedRegisterInd).toHaveBeenCalledTimes(1);
      expect(mockedRegisterInd).toHaveBeenCalledExactlyOnceWith(
        ClientRoutes.Login.Path,
      );
    },
  );
  it.each(InvalidTestData.invalidRegisterData)(
    "cant register",
    async (invalidData) => {
      //Arrange
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      mockedRegisterInd.mockResolvedValueOnce(
        new Response(null, { status: 200 }),
      );

      //Act
      const hasErrorsOnRender = await registerComp.hasAnyErrorsAsync();

      await registerComp.emailField.FillAsync(user, invalidData.email);
      const hasEmailErrors = await registerComp.emailField.HasError();

      await registerComp.passwordField.FillAsync(user, invalidData.password);
      const hasPasswordErrors = await registerComp.passwordField.HasError();

      await registerComp.passwordConfirmField.FillAsync(
        user,
        invalidData.passwordConfirmation,
      );
      const hasPasswordConfirmErrors =
        await registerComp.passwordConfirmField.HasError();

      await user.click(registerComp.submitButton);

      //Assert
      expect(hasErrorsOnRender).toBe(false);

      expect(mockedRegisterInd).toHaveBeenCalledTimes(1);

      expect(await registerComp.hasOnlyErrorMessages(invalidData.err)).toBe(
        true,
      );

      expect(
        hasEmailErrors || hasPasswordErrors || hasPasswordConfirmErrors,
      ).toBe(true);
    },
  );
});
