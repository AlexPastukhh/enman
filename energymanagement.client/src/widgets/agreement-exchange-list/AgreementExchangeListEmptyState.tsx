type AgreementExchangeListEmptyStateProps = {
  title: string;
  description: string;
};

export const AgreementExchangeListEmptyState = ({
  title,
  description,
}: AgreementExchangeListEmptyStateProps) => (
  <section className="agreementExchangeListEmpty" aria-labelledby="agreement-exchange-list-empty-title">
    <h2 id="agreement-exchange-list-empty-title">{title}</h2>
    <p>{description}</p>
  </section>
);
