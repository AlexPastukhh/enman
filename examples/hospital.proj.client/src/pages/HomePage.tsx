import { Link } from "react-router-dom";
import { routes } from "../Utils/routes";

export default function HomePage() {
    return (
        <div className="d-flex flex-column">
        Home Page
            <Link id="loginLink" to={routes.login}>Login</Link>
            <Link id="registerLink" to={routes.register}>Register</Link>
        </div>
    )
        
    
}