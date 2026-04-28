import { useState } from "react";
import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { Register } from "./Register";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";

const RegisterView = () => {
    const [rootError, setRootError] = useState<string>("");
    return (
        <div className="register-page">

        </div>
    )
}

export default RegisterView