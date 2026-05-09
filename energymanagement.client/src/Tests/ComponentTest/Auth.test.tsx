/**
 * @vitest environment jsdom
 */
import { cleanup, render, screen, waitFor } from "@testing-library/react";
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
import { RegisterTestComp } from "./TestClasses/RegisterTH";
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
  registerHelper: RegisterTestComp;
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
  const registerComp = RegisterTestComp.Create(screen);
  return { user, registerHelper: registerComp };
};

describe("Register", () => {
  it("renders without errors", async() => {
    //Arrange & Act
    const { registerHelper: registerComp } = setUpRegisterTest();

    //Assert
    const hasErrorsOnRender = await registerComp.hasAnyErrorsAsync();
    expect(hasErrorsOnRender).toBe(false);    

  });
  it.each(ValidTestData.validRegisterData)(
    "registers successfully",
    async (validData) => {
      //Arrange
      const { user, registerHelper: registerComp } = setUpRegisterTest();

      mockedRegisterInd.mockResolvedValueOnce(
        new Response(null, { status: 200 }),
      );

      //Act

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
          hasPasswordConfirmErrors
      ).toBe(false);

      await waitFor(() => {
        expect(mockedRegisterInd).toHaveBeenCalledTimes(1);
      });
      expect(mockedRegisterInd.mock.calls[0][0]).toEqual(validData);
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
      await registerComp.emailField.FillAsync(user, invalidData.email);
      await registerComp.passwordField.FillAsync(user, invalidData.password);
      await registerComp.passwordConfirmField.FillAsync(
        user,
        invalidData.passwordConfirmation,
      );
      await user.click(registerComp.submitButton);

      //Assert
      expect(mockedRegisterInd).not.toHaveBeenCalled();

      expect(await registerComp.hasOnlyErrorMessages(invalidData.err)).toBe(
        true,
      );

      expect(await registerComp.hasAnyErrorsAsync()).toBe(true);
    },
  );
});
