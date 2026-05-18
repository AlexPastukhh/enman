import { NavLink, type NavLinkProps } from "react-router-dom";

type NavButtonHollowProps = NavLinkProps & {
  children: React.ReactNode;
};

export const NavButtonHollow = ({
  className,
  children,
  ...rest
}: NavButtonHollowProps) => (
  <NavLink className={`button-hollow ${className ?? ""}`.trim()} {...rest}>
    {children}
  </NavLink>
);
