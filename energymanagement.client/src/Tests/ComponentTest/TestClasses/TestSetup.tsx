import { render, screen } from "@testing-library/react";
import userEvent, { type UserEvent } from "@testing-library/user-event";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import {
  createMemoryRouter,
  RouterProvider,
  useRouteError,
  type RouteObject,
} from "react-router-dom";

type RouterError = {
  message?: string;
};

export type UserEventSetupOptions = Parameters<typeof userEvent.setup>[0];

const RouteError = () => {
  const err = useRouteError() as RouterError;
  return (
    <div data-testid="route-error">
      {String(err.message ?? JSON.stringify(err))}
    </div>
  );
};

export const createTestRoute = (
  path: string,
  element: React.ReactNode,
): RouteObject => ({
  path,
  element,
  errorElement: <RouteError />,
});

export const renderComponentRoute = (
  routes: RouteObject[],
  initialPath: string,
  userOptions?: UserEventSetupOptions,
): { user: UserEvent; screen: typeof screen } => {
  const memoryRouter = createMemoryRouter(routes, {
    initialEntries: [initialPath],
  });

  const queryClient = new QueryClient();
  render(
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={memoryRouter} />
    </QueryClientProvider>,
  );

  return { user: userEvent.setup(userOptions), screen };
};
