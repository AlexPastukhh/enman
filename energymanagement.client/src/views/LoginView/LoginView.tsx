import { useState } from "react";
import { Footer } from "../../Components/Layout/Footer"
import { Header } from "../../Components/Layout/Header"
import { Login } from "./Login"
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";

export const LoginView = ()=>{
    const [rootError, setRootError] = useState<string>("");
    
    return(
        <>
            <Header/>
            <main className="content">
                <Login setRootError={setRootError} />
            </main>
            {rootError && <ErrorMessage message={rootError} />}
            <Footer/>

        </>
        )

}