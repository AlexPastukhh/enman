import { NavLink } from "react-router-dom"
import { LogoIcon } from "../../assets/icons/svgr_barrel"
import { headerConst } from "./headerConst"

export const HomeNavLink = () =>
{
    return (
        <NavLink className="header__home-link" to="/">
            <LogoIcon className="header__home-logo"/>
            <div className="header__home-text">
                <span>{headerConst.homeLinkTextPart1}</span>
                <span>{headerConst.homeLinkTextPart2}</span>
                <span className="header__home-slogan">{headerConst.homeLinkSloganText}</span>
            </div>
        </NavLink>
    )

}