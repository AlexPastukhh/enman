import { NavLink, type NavLinkProps } from "react-router-dom"
import { headerConst } from "./headerConst"

type NavButtonHollowProps = NavLinkProps & {
    children: React.ReactNode
}

export const NavButtonHollow = ({className,children, ...rest }: NavButtonHollowProps) =>
{
    return (
        <NavLink className={"button-hollow " + (className ?? "")}
            aria-label={headerConst.internetReceptionLinkText} 
            {...rest}
            >
                {children}
        </NavLink>
    )

}