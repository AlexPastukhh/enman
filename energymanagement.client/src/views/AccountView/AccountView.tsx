import { Footer } from "../../Components/Layout/Footer"
import { Header } from "../../Components/Layout/Header"
import {useSession} from "../../hooks/useSession"
import { Register } from "../RegisterView/Register";


function AccountView() {
  const session = useSession();
  return (
    <>
      <Header/>
      <main className="content">
      {!session && (
        <Register />
        )}
      </main>
      <Footer/>
    </>
  )
}

export default AccountView
