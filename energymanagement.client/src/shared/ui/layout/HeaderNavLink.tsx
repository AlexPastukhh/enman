import { NavLink, type NavLinkProps } from "react-router-dom";

type HeaderNavLinkProps = NavLinkProps & {
  children: React.ReactNode;
};

export const HeaderNavLink = ({ children, className, ...rest }: HeaderNavLinkProps) => (
  <NavLink
    {...rest}
    className={({ isActive }) =>
      ["header__nav-link", isActive ? "active" : "", className ?? ""]
        .filter(Boolean)
        .join(" ")
    }
  >
    {children}
  </NavLink>
);
