import { myRequestsConst } from "./myRequestsConst";

export const MyRequestsEmptyState = () => (
  <section className="myRequestsEmpty" aria-labelledby="my-requests-empty-title">
    <h2 id="my-requests-empty-title">{myRequestsConst.emptyTitle}</h2>
    <p>{myRequestsConst.emptyDescription}</p>
  </section>
);
