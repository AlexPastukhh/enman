import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import type { ReactNode } from "react";
import { SessionProvider } from "../../entities/session/model/SessionProvider";
import { PageErrorProvider } from "../../shared/errors/pageErrorContext";

const queryClient = new QueryClient();

export const AppProviders = ({ children }: { children: ReactNode }) => (
  <QueryClientProvider client={queryClient}>
    <PageErrorProvider>
      <SessionProvider>{children}</SessionProvider>
    </PageErrorProvider>
  </QueryClientProvider>
);
