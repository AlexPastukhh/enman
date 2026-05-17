# L2 Agreement Exchange Slice Numbering / Added Read Slices

Status: current / agreement exchange list-details slice split synchronized  
Scope: canonical slice numbering, server/client placement and old-file supersession

## 1. Canonical Agreement Exchange Slice Family

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

## 2. Client Sidecars Added For Read Slices

```text
L2-AGR-EXCH-LIST-001.client — Agreement Exchange List Pages
L2-AGR-EXCH-DETAILS-001.client — Shared Agreement Exchange Details Pages
```

## 3. File Placement

Server/backend/API slice drafts live in:

```text
planning/slices/
```

Client sidecar drafts live in:

```text
planning/slices/l2/
```

Canonical files:

```text
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/SL-AGR-EXCH-005-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md

planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
```

## 4. Superseded File Names

```text
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

These old names are removed by the apply script to avoid duplicate/conflicting slice numbers.

## 5. Key Read Direction

```text
List:
  shared endpoint/model first pass;
  summaries only;
  Client filtered by AgreementProposalExchange.ClientAccountId;
  Employee first pass uses any-active-Employee visibility.

Details:
  shared endpoint/model first pass;
  query handler + read repository / Dapper projection;
  full proposal version history;
  document refs only, no file bytes;
  no mutation.
```

## 6. Guardrails

```text
- Do not introduce AgreementExchangeActor abstraction for read slices first pass.
- Do not add ResponsibleEmployeeId as exchange authorization guard.
- Client ownership is AgreementProposalExchange.ClientAccountId.
- Employee sender identity is tracked per proposal version.
- UI button visibility is not authorization.
- Command sidecars own lifecycle actions.
```
