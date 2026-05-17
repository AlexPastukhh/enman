# SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details

Archive: `sl-agr-exch-004-agreement-exchange-details-read-v23.zip`

Scope:
- shared server details endpoint for Client and Employee;
- query handler + read repository / Dapper projection;
- Client access filtering by `AgreementProposalExchange.ClientAccountId`;
- Employee first-pass visibility through active Employee validation;
- details DTO with request summary, active proposal, proposal history and document refs;
- focused integration tests;
- test DB schema ensure for `ClientAccountId` on `L1AgreementProposalExchanges`.

Not included:
- docs/planning changes;
- client UI;
- generated OpenAPI/types;
- migrations;
- agreement exchange command slices.

Generated artifacts must be produced locally through repo commands after applying this archive.
