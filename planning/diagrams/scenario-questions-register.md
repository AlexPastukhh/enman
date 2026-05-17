# Scenario Questions Register

Status: active / L1 applicant + L2 review/agreement validation decisions synchronized

| ID | Source | Question | Current preference | Status |
|---|---|---|---|---|
| `Q-SC-04-001` | SC-04 / SC-10 | Which applicant context does request creation use? | Explicit Existing selected ApplicantPartyId or New applicant data. | accepted |
| `Q-SC-04-002` | SC-04 | Can Existing branch use non-default ApplicantParty? | Yes. Current/default is only initial prefill/default. | accepted |
| `Q-SC-04-003` | SC-04 | Should New applicant + request be atomic? | Yes. One server command. | accepted |
| `Q-SC-10-001` | SC-10 | Is default global or per type? | Per applicant type. | accepted |
| `Q-SC-10-002` | SC-10 | What happens on create? | First of type may initialize default; additional same-type does not switch. | accepted |
| `Q-SC-07B-001` | SC-07B / RejectReview | Is rejection feedback required or optional? | Optional in current accepted direction. Missing/null/blank feedback does not block RejectReview unless the endpoint/domain is intentionally changed later. | accepted |
| `Q-SC-L2-AGR-001` | SC-13B / SC-13D | Does counter-proposal replacement mean Rejected? | No. Use `SupersededByCounterProposal` or “superseded/replaced by counterproposal”. `Rejected` is only explicit rejection/decline. | accepted |
| `Q-SC-L2-AGR-002` | SC-13A..E / AgreementProposalExchange | How is client ownership protected? | `AgreementProposalExchange.ClientAccountId`; client actions check `client.Id == exchange.ClientAccountId`. | accepted |
| `Q-SC-L2-AGR-003` | SC-13A..E / AgreementProposalExchange | Is there a fixed responsible Employee guard? | No first pass. Do not add `ResponsibleEmployeeId` as authorization guard. Any active Employee can service the same exchange. | accepted |
| `Q-SC-L2-AGR-004` | SC-13D / proposal versions | Where is Employee sender identity stored? | Per proposal version through `AgreementProposal.Author.Sender = Employee` and `AgreementProposal.Author.SenderId = employee.Id`. | accepted |
| `Q-SC-L2-AGR-005` | SC-13D / route direction | What route is used for shared counter-proposal send by request id? | `POST /api/requests/{requestId}/agreement-exchange/proposals`; do not document `/api/agreement-exchanges/{requestId}/proposals` because that suggests `exchangeId`. | accepted |
| `Q-SC-L2-AGR-006` | SC-13A..E / command results | Should commands use per-command status enums? | No. Use existing Result/UnitResult + Error/ProblemDetails mapping. Domain status is persisted state, not command execution result. | accepted |
| `Q-SC-L2-ID-001` | Employee identity | Is Employee a separate profile linked by AccountId? | No current accepted direction. `Account -> ClientAccount / Employee`, TPH in `L1Accounts`, `Account.Id == Employee.Id` for Employee sessions. | accepted |
| `Q-SC-14-001` | SC-14 | What does SC-14 currently mean? | Current agreement context uses SC-14 as Agreement Documents / AgreementDocumentRef. Old Client Data Verification wording under SC-14 is stale/deferred unless explicitly reintroduced under another scenario id. | accepted |

Superseded:

```text
one current active ApplicantParty per account;
request creation target uses server-selected single current active applicant;
ApplicantParty creation is replacement;
RejectReview feedback required by API by default;
counter-proposal replacement modeled as ordinary Rejected;
EmployeeRef / ReviewerRef in L2 review/agreement diagrams;
DocumentFileRef / ProposalAttachment in L2 agreement docs;
EmployeeProfile(AccountId) as current target identity model;
ResponsibleEmployeeId as agreement exchange authorization guard;
per-command status enums for HTTP mapping;
```

## L2 source/read rule

For L2 review/agreement diagram or docs work, do not rely only on this register.

Also read:

```text
planning/diagrams/scenario-clarifications/README.md
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/slices/README.md
planning/slices/l2/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
```
