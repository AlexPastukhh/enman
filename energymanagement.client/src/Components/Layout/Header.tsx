
import { headerConst } from "./headerConst";
import { NavLink } from "react-router-dom";
import { EnterIcon, EyeIcon, MessagesIcon, PhoneIcon } from "../../assets/icons/svgr_barrel";
import { HomeNavLink } from "./HomeNavLink";
import { NavButtonHollow } from "./NavButtonHollow";
import { NavButtonPrimary } from "./NavButtonPrimary";
import { HeaderNavLink } from "./HeaderNavLink";
import { useSession } from "../../hooks/useSession";

export const Header = () =>
{
  
  const {session}= useSession();
  return (
    <header className="header">
      <div className="header__top">
        <HomeNavLink />


  <div className="header__actions">


          {!session && (
            <NavButtonHollow to={"/"} className="header__nav-button">
              <MessagesIcon />
              {headerConst.internetReceptionLinkText}
            </NavButtonHollow>
          )}
          {!session && (
            <NavButtonPrimary to={"/register"} className="header__nav-button">
              <EnterIcon />

              {headerConst.accountLinkText}
            </NavButtonPrimary>
          )}
        </div>
        <div className="header__contact">
          <NavLink to="/dashboard" className="header__hotline-link">
            <PhoneIcon/> {headerConst.hotlinePhoneNumberText}

          </NavLink>
          {headerConst.hotlinePhoneLabelText}
        </div>
      </div>
      <div className="header__bottom">
        <nav className="header__bottom-nav">
          <HeaderNavLink to="/">
            {headerConst.aboutCompanyLinkText}
          </HeaderNavLink>
          <HeaderNavLink to="/">
            {headerConst.toCustomersLinkText}
          </HeaderNavLink><HeaderNavLink to="/">
            {headerConst.disclosureInformationLinkText}
          </HeaderNavLink>
          <HeaderNavLink to="/">
            {headerConst.procurementLinkText}
          </HeaderNavLink>
          <HeaderNavLink to="/">
            {headerConst.vacanciesLinkText}
          </HeaderNavLink>
        </nav>
      </div>
    </header>
  );
};
