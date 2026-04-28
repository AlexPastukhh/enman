import { Link } from "react-router-dom";
import { routes } from "../Utils/routes";

export default function AccountActivatedPage() {
    return (
        <div>
            <h1>
                Account activated
            </h1>
            Please login to your account
            <Link to={routes.login}></Link>
        </div>
    )
}