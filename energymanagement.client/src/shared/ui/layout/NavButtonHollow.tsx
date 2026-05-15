import { NavLink, type NavLinkProps } from "react-router-dom";
import { headerConst } from "./headerConst";

type NavButtonHollowProps = NavLinkProps & {
  children: React.ReactNode;
};

export const NavButtonHollow = ({
  className,
  children,
  ...rest
}: NavButtonHollowProps) => (
  <NavLink
    className={`button-hollow ${className ?? ""}`.trim()}
    aria-label={headerConst.internetReceptionLinkText}
    {...rest}
  >
    {children}
  </NavLink>
);
