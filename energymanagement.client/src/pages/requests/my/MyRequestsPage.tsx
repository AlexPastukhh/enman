import { Link } from "react-router-dom";
import { useSession } from "../../../entities/session/model/useSession";
import { useMyRequestsQuery } from "../../../entities/request/model/useMyRequestsQuery";
import { MyRequestsList } from "../../../features/request/my-requests-list/ui/MyRequestsList";
import { myRequestsConst } from "../../../features/request/my-requests-list/ui/myRequestsConst";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { Footer } from "../../../shared/ui/layout/Footer";
import { Header } from "../../../shared/ui/layout/Header";
import "./myRequestsPage.css";

const MyRequestsPage = () => {
  const session = useSession();
  const myRequestsQuery = useMyRequestsQuery(Boolean(session));

  return (
    <>
      <Header />
      <main className="content">
        <section className="myRequestsPage" aria-labelledby="my-requests-heading">
          <h1 id="my-requests-heading">{myRequestsConst.pageTitle}</h1>

          {!session && (
            <div className="myRequestsPage__state">
              <h2>{myRequestsConst.signInRequiredTitle}</h2>
              <p>{myRequestsConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>{myRequestsConst.signInLinkText}</Link>
            </div>
          )}

          {session && myRequestsQuery.isPending && (
            <p className="myRequestsPage__state">{myRequestsConst.loadingText}</p>
          )}

          {session && myRequestsQuery.isError && (
            <p className="myRequestsPage__state" role="alert">
              {myRequestsConst.errorText}
            </p>
          )}

          {session && myRequestsQuery.data && (
            <MyRequestsList requests={myRequestsQuery.data} />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default MyRequestsPage;
