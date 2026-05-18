import { NavLink, type NavLinkProps } from "react-router-dom";

export type NavButtonPrimaryProps = NavLinkProps & {
  children?: React.ReactNode;
};

export const NavButtonPrimary = ({
  className,
  children,
  ...rest
}: NavButtonPrimaryProps) => (
  <NavLink
    className={`button-primary link-base-clear ${className ?? ""}`.trim()}
    {...rest}
  >
    {children}
  </NavLink>
);
