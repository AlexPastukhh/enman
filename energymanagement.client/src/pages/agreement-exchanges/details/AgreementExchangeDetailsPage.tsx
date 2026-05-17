import { Link, useParams } from "react-router-dom";
import { clientRoutes } from "../../../shared/config/clientRoutes";
import { Footer } from "../../../shared/ui/layout/Footer";
import { Header } from "../../../shared/ui/layout/Header";

const AgreementExchangeDetailsPage = () => {
  const { requestId } = useParams();

  return (
    <>
      <Header />
      <main className="content">
        <section aria-labelledby="agreement-exchange-details-heading">
          <h1 id="agreement-exchange-details-heading">
            Договорный обмен по заявке #{requestId ?? "—"}
          </h1>
          <p>
            Детальная история обмена будет реализована отдельным slice. Пока используйте список
            договорных обменов для просмотра сводки.
          </p>
          <Link to={clientRoutes.agreementExchanges}>Вернуться к списку обменов</Link>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default AgreementExchangeDetailsPage;
