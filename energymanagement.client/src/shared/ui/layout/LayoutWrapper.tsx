import { useEffect, type ReactNode } from "react";
import { useLocation } from "react-router-dom";
import { useSessionQuery } from "../../../entities/session/model/useSessionQuery";
import { clientRoutes } from "../../config/clientRoutes";
import { usePageError } from "../../errors/pageErrorContext";
import { ErrorMessage } from "./ErrorMessage";
import { Footer } from "./Footer";
import { Header } from "./Header";
import { headerConst } from "./headerConst";

const publicRoutes = new Set<string>([
  clientRoutes.home,
  clientRoutes.login,
  clientRoutes.register,
  clientRoutes.testUi,
]);

export const LayoutWrapper = ({ children }: { children: ReactNode }) => {
  const { pageError, removePageError } = usePageError();
  const { pathname } = useLocation();
  const sessionQuery = useSessionQuery();
  const shouldShowSessionLoading =
    sessionQuery.isPending && !publicRoutes.has(pathname);

  useEffect(() => {
    removePageError();
  }, [removePageError, pathname]);

  return (
    <div className="appShell">
      <Header />
      {shouldShowSessionLoading ? (
        <main className="content">
          <p className="appShell__session-loading" role="status">
            {headerConst.sessionLoadingText}
          </p>
        </main>
      ) : (
        children
      )}
      {pageError && <ErrorMessage message={pageError} />}
      <Footer />
    </div>
  );
};
