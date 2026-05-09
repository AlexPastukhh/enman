import { Footer } from "../../Components/Layout/Footer"
import { Header } from "../../Components/Layout/Header"
import { useState } from "react";
import {useSession} from "../../hooks/useSession"
import { Register } from "../RegisterView/Register";
import { ErrorMessage } from "../../Components/Layout/ErrorMessage";


function AccountView() {
  const session = useSession();
  const [rootError, setRootError] = useState<string>("");
  return (
    <>
      <Header/>
      <main className="content">
      {!session && (
        <Register setRootError={setRootError}/>
        )}
      </main>
      {rootError && <ErrorMessage message={rootError}/>}
      <Footer/>
    </>
  )
}

export default AccountView
