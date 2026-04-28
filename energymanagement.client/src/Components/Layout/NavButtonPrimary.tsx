import { NavLink, type NavLinkProps } from "react-router-dom"
import { headerConst } from "./headerConst"

type NavButtonPrimaryProps = NavLinkProps & {
    children?: React.ReactNode
}
export const NavButtonPrimary = ({className,children,...rest}:NavButtonPrimaryProps)=>{
    return (
        <NavLink
            className={"button-primary shrinking-button-primary link-base-clear " + className} 
            aria-label={headerConst.registerLinkText}
            {...rest}
          >
            {children}
          </NavLink>
    )

}