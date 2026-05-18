import { NavLink } from "react-router-dom";
import {
  EnterIcon,
  MessagesIcon,
  PhoneIcon,
} from "../../../assets/icons/svgr_barrel";
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

        <div className="header__actions">
          {!session && (
            <NavButtonHollow
              to={clientRoutes.createRequest}
              className="header__nav-button"
            >
              <MessagesIcon />
              {headerConst.internetReceptionLinkText}
            </NavButtonHollow>
          )}

          {!session && (
            <NavButtonPrimary
              to={clientRoutes.register}
              className="header__nav-button"
            >
              <EnterIcon />
              {headerConst.accountLinkText}
            </NavButtonPrimary>
          )}

          {session && (
            <HeaderNavLink to={clientRoutes.account}>
              {headerConst.accountLinkText}
            </HeaderNavLink>
          )}

          {session && isClient && (
            <HeaderNavLink to={clientRoutes.createRequest}>
              {headerConst.createRequestLinkText}
            </HeaderNavLink>
          )}

          {session && isClient && (
            <HeaderNavLink to={clientRoutes.requests}>
              {headerConst.myRequestsLinkText}
            </HeaderNavLink>
          )}

          {session && isClient && (
            <HeaderNavLink to={clientRoutes.agreementExchanges}>
              {headerConst.agreementExchangesLinkText}
            </HeaderNavLink>
          )}

          {session && isEmployee && (
            <HeaderNavLink to={clientRoutes.employeeRequests}>
              {headerConst.employeeRequestsLinkText}
            </HeaderNavLink>
          )}

          {session && isEmployee && (
            <HeaderNavLink to={clientRoutes.employeeAgreementExchanges}>
              {headerConst.employeeAgreementExchangesLinkText}
            </HeaderNavLink>
          )}

          {session && (
            <LogoutButton
              className="header__nav-button"
              label={headerConst.logoutButtonText}
              pendingLabel={headerConst.logoutPendingButtonText}
            />
          )}
        </div>

        <div className="header__contact">
          <NavLink to={clientRoutes.home} className="header__hotline-link">
            <PhoneIcon /> {headerConst.hotlinePhoneNumberText}
          </NavLink>
          {headerConst.hotlinePhoneLabelText}
        </div>
      </div>

      <div className="header__bottom">
        <nav className="header__bottom-nav" aria-label="Основная навигация">
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.aboutCompanyLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.createRequest}>
            {headerConst.toCustomersLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.requests}>
            {headerConst.disclosureInformationLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.agreementExchanges}>
            {headerConst.procurementLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.employeeRequests}>
            {headerConst.vacanciesLinkText}
          </HeaderNavLink>
        </nav>
      </div>
    </header>
  );
};
