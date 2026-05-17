# Slice Extension Points Register

Status: active / client API placement, L2 review sidecars and AgreementProposalExchange slice boundaries synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-CLIENT-API-PLACEMENT-001` | client API placement | New read endpoint wrappers live in `entities/*/api`; new command wrappers live in `features/*/api`; `shared/api` stays transport/generated only. | accepted |
| `CP-CLIENT-API-PLACEMENT-002` | migration | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility; migrate only when a concrete slice touches that API area. | future cleanup |
| `CP-CLIENT-API-PLACEMENT-003` | generated types | Entities/features can import generated OpenAPI types from shared generated artifact and define business aliases locally. | accepted |
| `CP-APPL-DEFAULT-004` | explicit default action client | client same-page action belongs to `SL-APPL-003.client`; command API wrapper belongs in `features/applicant-party/make-current-default/api`. | full sidecar draft |
| `CP-L2-EMP-VIS-001` | employee request visibility | temporary first-pass policy: all active Employees can see all review-relevant requests. | accepted temporary |
| `CP-L2-EMP-VIS-002` | employee request visibility | department/region/assignment-based filtering is future. | future |
| `CP-L2-EMP-DETAILS-001` | details action slot | `L2-EMP-DETAILS-001.client` exposes optional action slot; feature sidecars render Start/Approve/Reject controls later. | accepted |
| `CP-L2-EMP-DETAILS-002` | details read API placement | Details read wrapper and DTO aliases live in `entities/employee-request/api`. | accepted |
| `CP-L2-EMP-DETAILS-003` | review command API placement | Start/Approve/Reject wrappers live in `features/employee-request/<action>/api`. | accepted |
| `CP-L2-EMP-DETAILS-004` | future details enrichment | documents, verification result, review history and assignment/lock stay future details refinements. | future |

## L2 Account / Employee Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-L2-EMP-ACCOUNT-001` | Employee identity | `Employee : Account`; Employee sessions use Account.Id as Employee.Id. | accepted |
| `CP-L2-EMP-ACCOUNT-002` | persistence | TPH in `L1Accounts` with AccountType/Role discriminator. | accepted direction |
| `CP-L2-EMP-ACCOUNT-003` | compatibility cleanup | If current code has separate Employee profile with AccountId, migrate/cleanup under a scoped persistence/domain slice. | future cleanup |
| `CP-L2-EMP-ACCOUNT-004` | permissions | Department/region/assignment/permission model remains future; do not confuse it with Employee account identity. | future |

## L2 AgreementProposalExchange Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-L2-AGR-EXCH-001` | initial exchange | `SL-AGR-EXCH-001` creates exchange and proposal version 1 from approved request. | planned boundary |
| `CP-L2-AGR-EXCH-002` | counter-proposals | `SL-AGR-EXCH-002` owns both client and employee counter-proposal branches. Split later only if they diverge materially. | planned boundary |
| `CP-L2-AGR-EXCH-003` | exchange read | `SL-AGR-EXCH-003` owns exchange read model, active proposal and version history shape. | planned boundary |
| `CP-L2-AGR-EXCH-004` | acceptance | `SL-AGR-EXCH-004` owns accepting active proposal; it does not create a new proposal version. | planned boundary |
| `CP-L2-AGR-EXCH-005` | final refusal | `SL-AGR-EXCH-005` owns final refusal; exchange and request are separate aggregates orchestrated by application service. | planned boundary |
| `CP-L2-AGR-DOC-001` | document metadata | AgreementDocumentRef metadata is part of proposal commands/read models; bytes/storage adapter remains future document/storage slice. | future |
| `CP-L2-AGR-PERM-001` | actor permissions | Actor-specific route/auth rules can be separate in implementation but do not force separate counter-proposal slices yet. | future refinement |

## Agreement Exchange Read Extension Points

Marker: AGR-EXCH-READ-SLICE-DECISIONS-2026-05

| ID | Area | Current direction | Status |
|---|---|---|---|
| CP-AGR-EXCH-READ-001 | read endpoint split | Shared list/details endpoints first pass; split actor endpoints only if behavior diverges. | future if needed |
| CP-AGR-EXCH-READ-002 | employee assignment | No ResponsibleEmployeeId guard first pass; department/assignment visibility is future. | future |
| CP-AGR-EXCH-READ-003 | actions in read DTO | AvailableActions may be added later if client derivation becomes too complex. | future |
| CP-AGR-EXCH-READ-004 | document bytes/download | List/details return document references only; download/storage remains future document slice. | future |


<!-- L2-AGR-EXCH-COMMAND-SLICES-SYNC -->
## Agreement Exchange Command Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| CP-L2-AGR-CMD-001 | route shape | Counter-proposal uses request-scoped route first pass; generated OpenAPI remains source during implementation. | accepted |
| CP-L2-AGR-CMD-002 | actor abstraction | AgreementExchangeActor may be introduced later if role branching repeats or grows. | future cleanup |
| CP-L2-AGR-CMD-003 | employee ownership | ResponsibleEmployeeId is not a guard; assignment/ownership can be a future visibility slice. | future |
| CP-L2-AGR-CMD-004 | accept | Employee accept is out of scope unless future scenario/domain explicitly adds it. | future |
| CP-L2-AGR-CMD-005 | final refusal | Final refusal remains canonical SL-AGR-EXCH-006; full draft still pending. | planned |

