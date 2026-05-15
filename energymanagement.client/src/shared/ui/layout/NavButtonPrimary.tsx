import { NavLink, type NavLinkProps } from "react-router-dom";
import { headerConst } from "./headerConst";

type NavButtonPrimaryProps = NavLinkProps & {
  children?: React.ReactNode;
};

export const NavButtonPrimary = ({
  className,
  children,
  ...rest
}: NavButtonPrimaryProps) => (
  <NavLink
    className={`button-primary shrinking-button-primary link-base-clear ${className ?? ""}`.trim()}
    aria-label={headerConst.registerLinkText}
    {...rest}
  >
    {children}
  </NavLink>
);
