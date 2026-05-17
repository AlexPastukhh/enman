# L2 AgreementProposalExchange Slice Family

Status: slice-family decision / planned slice boundaries fixed / not implementation drafts  
Scope: AgreementProposalExchange, proposal versions, accept flow, final refusal and exchange read model

## 1. Decision

Use five AgreementProposalExchange slices:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Read Agreement Exchange
SL-AGR-EXCH-004 — Accept Active Agreement Proposal
SL-AGR-EXCH-005 — Final Refuse Agreement Exchange
```

This replaces the loose `SL-AGR-*` placeholder in navigation/registers.

## 2. Split Rule

Do not create one generic “Send Proposal” implementation slice for both initial creation and later proposal response.

Reason:

```text
Initial employee proposal:
  no exchange exists yet;
  creates AgreementProposalExchange;
  creates proposal version 1;
  requires Approved request;
  requires no existing exchange.

Counter-proposal:
  exchange already exists;
  supersedes active proposal;
  creates next version;
  requires correct actor turn/status.
```

If mixed into one handler, implementation becomes an `if no exchange -> create else -> respond` lifecycle branch. Keep those paths separate.

## 3. Counter-Proposal Rule

Do not split Client counter-proposal and Employee counter-proposal into separate implementation slices yet.

Use one slice with two actor branches:

```text
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
```

Branches:

```text
Client counter-proposal:
  AwaitingClientConfirmation
  active proposal author = Employee
  -> ClientSendOwnVersion(...)
  -> AwaitingEmployeeResponse

Employee counter-proposal:
  AwaitingEmployeeResponse
  active proposal author = Client
  -> EmployeeSendNewVersion(...)
  -> AwaitingClientConfirmation
```

Reason:

```text
Both branches implement the same scenario pattern:
current actor received active proposal and sends own document/comment as the next version.
```

Split later only if UI, permission model, document handling or validation diverges materially.

## 4. Slice Map

| Slice | Responsibility | Status |
|---|---|---|
| `SL-AGR-EXCH-001` | Employee starts exchange from Approved request and sends initial proposal document. | planned boundary |
| `SL-AGR-EXCH-002` | Client/Employee sends next counter-proposal version inside existing active exchange. | planned boundary |
| `SL-AGR-EXCH-003` | Read active/historical exchange state and proposal versions. | planned boundary |
| `SL-AGR-EXCH-004` | Accept active agreement proposal. | planned boundary |
| `SL-AGR-EXCH-005` | Employee final refusal of exchange and request failure marking. | planned boundary |

## 5. Scenario Source Family

Scenario flow and behavior coverage still come from scenario source artifacts, especially:

```text
planning/diagrams/scenario-text-specs/SC-13A-client-agreements.md
planning/diagrams/scenario-text-specs/SC-13B-client-agreement-proposal-details-response.md
planning/diagrams/scenario-text-specs/SC-13C-employee-agreements.md
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md
planning/diagrams/scenario-text-specs/SC-13E-agreement-final-refusal.md
planning/diagrams/scenario-text-specs/SC-14-agreement-documents.md
```

Domain draft remains domain-design input, not scenario replacement.

## 6. Guardrails

```text
- AgreementProposalExchange and Request are separate aggregates.
- AgreementProposal is child entity of exchange, not aggregate.
- AgreementProposalVersion is local per-exchange value object.
- Version starts at 1.
- Next version = max existing version + 1.
- API/client does not choose proposal version.
- ActiveProposalVersion points by domain version, not DB id.
- AgreementDocumentRef is metadata reference, not bytes/storage adapter.
- ProposalComment is optional; if present, it is non-empty and length-limited.
- Final refusal is exchange direct state, not separate AgreementFinalRefusal entity and not new proposal version.
```

## 7. Next Drafting Order

```text
1. SL-AGR-EXCH-001
2. SL-AGR-EXCH-003
3. SL-AGR-EXCH-002
4. SL-AGR-EXCH-004
5. SL-AGR-EXCH-005
```

Reading before command implementation can be drafted first if UI/client planning needs exchange state earlier, but the slice family boundary remains the same.
