import { PhoneIcon } from "../../../assets/icons/svgr_barrel";
import { useSessionQuery } from "../../../entities/session/model/useSessionQuery";
import { useSession } from "../../../entities/session/model/useSession";
import { LogoutButton } from "../../../features/auth/logout/ui/LogoutButton";
import { clientRoutes } from "../../config/clientRoutes";
import { HeaderNavLink } from "./HeaderNavLink";
import { headerConst } from "./headerConst";
import { HomeNavLink } from "./HomeNavLink";

const isEmployee = (role?: string | null) => role === "Employee";
const isClient = (role?: string | null) => role === "Client";

export const Header = () => {
  const session = useSession();
  const sessionQuery = useSessionQuery();
  const employeeSession = isEmployee(session?.role);
  const clientSession = isClient(session?.role);
  const isSessionLoading = sessionQuery.isPending;

  return (
    <header className="header">
      <div className="header__top">
        <HomeNavLink />

        <nav className="header__actions" aria-label="Основная навигация">
          {isSessionLoading && (
            <span className="header__session-loading" role="status">
              {headerConst.sessionLoadingText}
            </span>
          )}

          {!isSessionLoading && !session && (
            <>
              <HeaderNavLink to={clientRoutes.home}>
                {headerConst.homeLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.login}>
                {headerConst.loginLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.register}>
                {headerConst.registerLinkText}
              </HeaderNavLink>
            </>
          )}

          {clientSession && (
            <>
              <HeaderNavLink to={clientRoutes.createRequest}>
                {headerConst.createRequestLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.requests}>
                {headerConst.myRequestsLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.agreementExchanges}>
                {headerConst.agreementExchangesLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.account}>
                {headerConst.accountLinkText}
              </HeaderNavLink>
            </>
          )}

          {employeeSession && (
            <>
              <HeaderNavLink to={clientRoutes.employeeRequests}>
                {headerConst.employeeRequestsLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.employeeAgreementExchanges}>
                {headerConst.employeeAgreementExchangesLinkText}
              </HeaderNavLink>
            </>
          )}

          {!isSessionLoading && session && (
            <LogoutButton
              className="header__nav-button"
              label={headerConst.logoutButtonText}
              pendingLabel={headerConst.logoutPendingButtonText}
            />
          )}
        </nav>

        <div className="header__contact" aria-label="Контакты поддержки">
          <a href="tel:80000000000" className="header__hotline-link">
            <PhoneIcon /> {headerConst.hotlinePhoneNumberText}
          </a>
          <span>{headerConst.hotlinePhoneLabelText}</span>
        </div>
      </div>
    </header>
  );
};
