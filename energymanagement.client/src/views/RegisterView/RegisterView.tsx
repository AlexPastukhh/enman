import { useState } from "react";
import { Footer } from "../../Components/Layout/Footer";
import { Header } from "../../Components/Layout/Header";
import { Register } from "./Register";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";

const RegisterView = () => {
    const [rootError, setRootError] = useState<string>("");
    return (
        <>
            <Header/>
            <main className="content register-page">
                <Register setRootError={setRootError}/>
            </main>
            {rootError && <ErrorMessage message={rootError}/>}
            <Footer/>
        </>
    )
}

export default RegisterView
