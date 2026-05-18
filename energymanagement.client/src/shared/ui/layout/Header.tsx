import { NavLink } from "react-router-dom";
import { EnterIcon, PhoneIcon } from "../../../assets/icons/svgr_barrel";
import { useSession } from "../../../entities/session/model/useSession";
import { LogoutButton } from "../../../features/auth/logout/ui/LogoutButton";
import { clientRoutes } from "../../config/clientRoutes";
import { HeaderNavLink } from "./HeaderNavLink";
import { headerConst } from "./headerConst";
import { HomeNavLink } from "./HomeNavLink";
import { NavButtonHollow } from "./NavButtonHollow";
import { NavButtonPrimary } from "./NavButtonPrimary";

const isClientSession = (role?: string | null) => role === "Client";
const isEmployeeSession = (role?: string | null) => role === "Employee";

export const Header = () => {
  const session = useSession();
  const isClient = isClientSession(session?.role);
  const isEmployee = isEmployeeSession(session?.role);

  return (
    <header className="header">
      <div className="header__top">
        <HomeNavLink />

        <div className="header__actions" aria-label="Быстрые действия">
          {!session && (
            <>
              <NavButtonHollow
                to={clientRoutes.login}
                className="header__nav-button"
                aria-label={headerConst.loginLinkText}
              >
                <EnterIcon />
                {headerConst.loginLinkText}
              </NavButtonHollow>
              <NavButtonPrimary
                to={clientRoutes.register}
                className="header__nav-button"
                aria-label={headerConst.registerLinkText}
              >
                {headerConst.registerLinkText}
              </NavButtonPrimary>
            </>
          )}

          {session && (
            <>
              <HeaderNavLink to={clientRoutes.account}>
                {headerConst.accountLinkText}
              </HeaderNavLink>
              <LogoutButton
                className="header__nav-button"
                label={headerConst.logoutButtonText}
                pendingLabel={headerConst.logoutPendingButtonText}
              />
            </>
          )}
        </div>

        <div className="header__contact">
          <NavLink to={clientRoutes.home} className="header__hotline-link">
            <PhoneIcon /> {headerConst.hotlinePhoneNumberText}
          </NavLink>
          <span>{headerConst.hotlinePhoneLabelText}</span>
        </div>
      </div>

      <div className="header__bottom">
        <nav className="header__bottom-nav" aria-label="Основная навигация">
          <HeaderNavLink to={clientRoutes.home}>{headerConst.homeLinkText}</HeaderNavLink>

          {!session && (
            <>
              <HeaderNavLink to={clientRoutes.login}>{headerConst.loginLinkText}</HeaderNavLink>
              <HeaderNavLink to={clientRoutes.register}>{headerConst.registerLinkText}</HeaderNavLink>
            </>
          )}

          {isClient && (
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
            </>
          )}

          {isEmployee && (
            <>
              <HeaderNavLink to={clientRoutes.employeeRequests}>
                {headerConst.employeeRequestsLinkText}
              </HeaderNavLink>
              <HeaderNavLink to={clientRoutes.employeeAgreementExchanges}>
                {headerConst.employeeAgreementExchangesLinkText}
              </HeaderNavLink>
            </>
          )}
        </nav>
      </div>
    </header>
  );
};
