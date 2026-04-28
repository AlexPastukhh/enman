import { NavLink, type NavLinkProps } from "react-router-dom";
type HeaderNavLinkProps = NavLinkProps & {
  children: React.ReactNode;
};
export const HeaderNavLink = ({ children,...rest}: HeaderNavLinkProps) => {
  return (
    <NavLink {...rest} className="header__nav-link">
        {children}
    </NavLink>
  );
}   