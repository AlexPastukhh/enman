import React from "react";
import { NavLink, type NavLinkProps } from "react-router-dom";
type NLinkProps = Omit<NavLinkProps, "className"> & {
  className?: string;
};

export const NLink = React.forwardRef<HTMLAnchorElement, NLinkProps>(
  ({ children, className, ...rest }, ref) => {
    return (
      <>
        <NavLink className={`navLink ${className}`} {...rest} ref={ref}>
          {children}
        </NavLink>
      </>
    );
  }
);
