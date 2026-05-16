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

export const Header = () => {
  const session = useSession();

  return (
    <header className="header">
      <div className="header__top">
        <HomeNavLink />

        <div className="header__actions">
          {!session && (
            <NavButtonHollow
              to={clientRoutes.home}
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

          {session && (
            <LogoutButton
              className="header__nav-button"
              label={headerConst.logoutButtonText}
              pendingLabel={headerConst.logoutPendingButtonText}
            />
          )}
        </div>

        <div className="header__contact">
          <NavLink to="/dashboard" className="header__hotline-link">
            <PhoneIcon /> {headerConst.hotlinePhoneNumberText}
          </NavLink>
          {headerConst.hotlinePhoneLabelText}
        </div>
      </div>

      <div className="header__bottom">
        <nav className="header__bottom-nav">
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.aboutCompanyLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.toCustomersLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.disclosureInformationLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.procurementLinkText}
          </HeaderNavLink>
          <HeaderNavLink to={clientRoutes.home}>
            {headerConst.vacanciesLinkText}
          </HeaderNavLink>
        </nav>
      </div>
    </header>
  );
};
