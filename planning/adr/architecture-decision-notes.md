# Architecture Decision Notes

Status: active accepted-decision registry  
Scope: current accepted architecture decisions that guide planning/implementation and may support diploma text

## 1. Purpose

This file is the current accepted architecture decision registry.

A decision note means:

```text
This direction is accepted/current enough to guide planning and implementation,
but it is not yet a full numbered ADR.
```

## 2. How To Use This File

Before domain/slice/client implementation planning:

```text
1. Search this file for relevant decision notes.
2. Follow accepted directions unless superseded by a full numbered ADR or explicit user decision.
3. If a new important decision is made, add/update a note here.
4. If a note is important enough for formal write-up, add/update `adr-candidates.md`.
```

## 3. Relationship To ADR Candidates

```text
architecture-decision-notes.md
= current accepted guidance.

adr-candidates.md
= backlog of decisions that may later become full ADRs.
```

A decision can appear in both files.

If these files disagree, stop and clarify before implementation.

## 4. Current Accepted Decisions

| ID | Decision | Context | Options considered | Current accepted direction | Rationale / trade-off | Affected artifacts | Diploma value | ADR promotion |
|---|---|---|---|---|---|---|---|---|
| ADN-001 | Domain foundation before full vertical slices | L1 domain behavior needed stable unit tests before API/UI wiring. | Full-stack slice first; API-first; domain foundation first. | Domain foundation first, then scenario/application/client slices. | Reduces ambiguity and makes domain invariants testable before infrastructure. Trade-off: initial slice is not full end-to-end. | `domain-draft-01`, L1 cut, tests, later slices | Explains staged architecture/development method. | Likely ADR |
| ADN-002 | First L1 cut is narrower than domain draft | Domain draft is broader than safe first implementation step. | Implement whole draft; implement narrow foundation cut. | First cut only account/applicant/request review foundation; no persistence/API/UI/agreement. | Keeps implementation safe and testable; avoids mixing too many concerns. | `l1-domain-implementation-cut.md` | Shows staged risk control. | Candidate |
| ADN-003 | Progressive file splitting after green mini-cuts | Large files help initial agent context, but production needs structure. | Split everything upfront; one giant file; progressive split. | Implement mini-cuts, get tests green, then split/normalize files. | Balances agent readability with maintainable structure. | L1 implementation cut, domain files/tests | Useful process explanation. | Candidate |
| ADN-004 | Unit tests first, integration after domain implementation | Domain behavior should be stable before integration/API wiring. | Integration first; unit first; E2E first. | Unit tests first, integration after first green domain implementation. | Fast feedback and stable domain invariants before infrastructure. | `l1-domain-testing-rules.md` | Testing strategy explanation. | Candidate |
| ADN-005 | Test behavior and no-write rules, not implementation details | Domain methods must preserve state on failed commands. | Test internals/call order; test observable behavior/no-write. | Test observable behavior, state transitions, failure results and no-write behavior. | Resists refactoring and catches partial mutation bugs. | L1 test rules, domain tests | Strong testing methodology point. | Candidate |
| ADN-006 | Slice = observable behavior + implementation path + independent testability | Need slice boundaries for workflow and diploma. | Endpoint-based; layer-based; behavior-based. | Behavior-derived slice with implementation path and separate testability. | Prevents technical task slicing and supports scenario-to-implementation traceability. | slice boundary draft, parent slice files, client sidecars | Strong methodology point. | Likely ADR |
| ADN-007 | Scenario question loop before implementation continuation | Scenario ambiguity can invalidate domain/client/API planning. | Continue silently; ask everything; ask relevant questions with assumptions. | Ask relevant blocking questions, include assumptions, update scenario/DATA/UI/validation/items before continuing. | Balances progress with correctness and keeps source artifacts authoritative. | agent protocol, workflow docs, scenario register | Explains disciplined AI-assisted planning. | Candidate |
| ADN-008 | Per-scenario behavior items instead of only compiled baseline | Single compiled baseline was hard to use for scenario/slice/client coverage. | Keep one compiled file; per-scenario files; both. | Per-scenario behavior item files are primary for future planning; compiled baseline remains historical/cross-check. | Improves traceability from scenario to slice/client tests. | scenario-behavior-items, tables baseline | Good traceability explanation. | Candidate |
| ADN-009 | Scenario UI specs as separate layer | Client sidecars need UI-visible requirements, but scenario specs should not become React/component specs. | Put UI details in scenario text; put all in sidecar; create scenario UI specs. | Create scenario UI specs when UI planning starts. | Separates UI-visible requirements from implementation details. | scenario-ui-specs, client sidecars | Useful UI/specification explanation. | Candidate |
| ADN-010 | Parent slice + `.client.md` sidecar split | Client layer needs detailed planning while parent slice owns vertical behavior and API contract. | Keep all in parent; separate sidecar; frontend-only docs. | Parent owns vertical behavior/API; sidecar owns detailed client implementation when client work starts. | Avoids huge parent files and prevents client sidecar from inventing backend API. | slice guides, client sidecars | Explains cross-layer planning boundaries. | Candidate |
| ADN-011 | Do not create `.client.md` in advance | Sidecars should describe concrete client work, not speculative frontend plans. | Create all sidecars upfront; create when work starts. | Create sidecar only when concrete client work starts. | Prevents stale speculative files and keeps planning close to implementation. | slice guide, workflow | Process rationale. | Candidate |
| ADN-012 | Planning slice and frontend feature are not 1:1 | Read behavior is a slice but frontend read behavior usually maps to pages/entities, not features. | 1:1 slice-feature mapping; ad-hoc placement; architecture mapping. | Read slice -> pages + entities; command slice -> pages + features + entities; shared concern -> app/shared. | Keeps frontend architecture clean while preserving planning slice semantics. | client architecture principles, `.client.md` template | Strong client architecture explanation. | Likely ADR |
| ADN-013 | Entity query hooks are allowed only when generic | Entity modules need reusable read access but must not own feature orchestration. | No hooks in entities; hooks everywhere; generic entity query hooks. | Entity hooks may own queryKey/queryFn/enabled/select/options but not feature-specific success/navigation/invalidation. | Keeps read modules reusable and separates command orchestration into features. | client architecture principles | Useful client architecture nuance. | Candidate |
| ADN-014 | Entity components are display-level only | Entities can show business data but should not become screens/actions. | No entity UI; entity display components; entity screens/actions. | Use entity components for small/medium reusable business display only. | Reuse without mixing page context or command behavior. | client architecture principles, component guide | Client architecture explanation. | Candidate |
| ADN-015 | Widgets are optional and introduced only after real reuse | Widgets can become premature architecture if created too early. | No widgets; widgets upfront; widgets after real reuse. | Keep large blocks page-local until reused by multiple pages. | Avoids overengineering while leaving a path to reuse. | client architecture principles | Anti-overengineering example. | Candidate |
| ADN-016 | Client-wide conventions live under `planning/client/cross-cutting` | Accessibility, styling, deferred validation and error mapping apply across many client sidecars. | Put in every sidecar; keep in slices/shared; create planning/client. | `planning/client/cross-cutting/` owns client-wide conventions. | Reduces duplication and makes UI/testing conventions discoverable. | planning/client, client sidecars | Useful UI/test chapter. | Candidate |
| ADN-017 | Plain CSS + CSS Modules + CSS variables/tokens | Styling needs a default architecture. | Tailwind; CSS-in-JS; UI framework; CSS Modules/tokens. | Use plain CSS + CSS Modules + CSS variables/tokens unless separately decided otherwise. | Simple, explicit, framework-light styling; component owns internal style, parent owns layout. | client styling convention | UI implementation rationale. | Candidate |
| ADN-018 | Accessibility / ARIA contract is mandatory for client sidecars | Client tests and UI quality need accessible semantics. | Ignore a11y; optional a11y; required contract. | Prefer native semantic HTML, ARIA only when needed, role/label-first tests, sidecar accessibility contract. | Improves usability and testability through user-visible semantics. | a11y convention, `.client.md` template | Strong client/testing value. | Candidate |
| ADN-019 | Deferred client validation is current convention | Existing client tests validate delayed validation behavior. | Immediate only; server-only; deferred client validation. | Use deferred validation after input and immediate validation on submit. | Better UX while keeping server/domain authoritative. | client validation convention, form sidecars | Client UX/testing explanation. | Candidate |
| ADN-020 | Client/server error mapping is a client-wide convention | Server errors need predictable field/global display. | Ad-hoc mapping per form; central convention. | Map known field errors near fields; unknown/global/domain errors to form/page/action areas. | Consistent UX and testable error handling. | error mapping convention | Useful client/API contract point. | Candidate |
| ADN-021 | FormValues and API DTO may differ | UI forms often have transient/normalized fields. | Use DTO as form state; separate FormValues + mapper. | Use separate FormValues and API DTO mapping when normalization/UI-only state exists. | Prevents UI state leaking into API contract. | implementation principles, client sidecars | Good UI/API boundary explanation. | Candidate |
| ADN-022 | Extension points, change points and extension pressure are explicit planning concepts | Future extensions can affect current design without requiring premature abstraction. | Ignore until later; abstract everything now; document pressure/trade-offs. | Record extension points, change points and extension pressure locally and in cross-slice register. | Avoids accidental coupling and overengineering by documenting trade-offs. | change-extension docs, extension register, slice templates | Strong architecture reasoning/future-proofing discussion. | Likely ADR |
| ADN-023 | Extension pressure register is a workflow gate | Local slice notes are not enough for future agents. | Keep local only; global register; both. | Check `slice-extension-points-register.md` before parent slice/client sidecar work. | Keeps planned extensions and anti-coupling decisions discoverable. | extension register, agent protocol | Process + architecture rationale. | Candidate |
| ADN-024 | ApplicantParty version-like persistence policy | Applicant data may be verified, request-referenced or current active. Mutating records could corrupt history/verification. | Mutate existing; immutable snapshots; version-like records with cleanup. | Create new version-like ApplicantParty; preserve verified/request-referenced/current active; cleanup/archive irrelevant inactive unverified later. | Balances audit/history with avoiding unlimited useless inactive data. | Applicant slice, request creation, domain draft | Important domain modeling decision. | Likely ADR |
| ADN-025 | ApplicantParty status is `Unverified` / `Verified` for now | More statuses are possible but not yet needed. | Many statuses; two-state status; no status. | Use `Unverified` / `Verified` in current core. | Keeps L1 simple; future provider/in-progress states can extend later. | ApplicantParty, verification slices | Shows YAGNI. | Candidate |
| ADN-026 | Keep ApplicantParty inheritance for applicant type variants | Future applicant types need different data shapes. | Single flat type; inheritance; composition/type-specific values. | Keep inheritance for now. | Matches expected variants while preserving current IndividualApplicantParty. | ApplicantParty model | Domain modeling explanation. | Candidate |
| ADN-027 | Account email and ApplicantParty email are different concepts | Login/recovery email may differ from applicant contact email. | Single email; separate account/applicant emails. | `ClientAccount.Email` is auth/login/recovery; `ApplicantParty.Email` is contact email. | Avoids mixing identity credentials with applicant contact data. | domain draft, applicant/account flows | Useful domain language point. | Candidate |
| ADN-028 | Current core registration creates Active account; activation flow is future | Current L1 avoids full email confirmation flow. | PendingActivation now; Active now; no activation state. | Registration creates Active in current core; PendingActivation/email confirmation future. | Keeps current auth scope small while preserving activation marker. | ClientAccount, auth flows | Scope control. | Candidate |
| ADN-029 | Account activation guard stays outside business aggregates | Protected flows require active account, but business aggregates should not duplicate auth policy. | Aggregate checks; app/auth guard; claims only. | Account.EnsureActivated exists; app/auth guard calls it; business aggregates do not duplicate checks. | Keeps auth/framework concern out of business aggregate behavior. | auth, protected slices | Layer boundary explanation. | Candidate |
| ADN-030 | Request creation status is `InReview`, not legacy `Submitted` | Old integration expectation conflicted with target domain direction. | Submitted; InReview; both. | Use `InReview` after request creation. | Clear ubiquitous language: created request is awaiting review. | request domain/API/tests | Example of aligning tests with language. | Candidate |
| ADN-031 | Request stores ApplicantPartyId, not ClientAccountId | Request is applicant-centric and account ownership is reachable through ApplicantParty. | Store ClientAccountId; store ApplicantPartyId; store both. | ConnectionRequest stores ApplicantPartyId; read models join through ApplicantParty to ClientAccount. | Avoids write aggregate duplication; accepts read/query complexity. | request model, read projections | CQRS/read model trade-off. | Likely ADR |
| ADN-032 | Request creation Client/UI is dependent slice, not part of backend slice | Backend/API/persistence request creation is independently testable. | Include UI in same slice; separate dependent client slice. | Keep Client/UI as dependent slice after concrete UI plan. | Allows server behavior to stabilize before client work. | SL-REQ-001 | Slice planning rationale. | Candidate |
| ADN-033 | Request approval verifies ApplicantParty through application orchestration | Approval currently also verifies applicant, but ConnectionRequest should not mutate ApplicantParty internally. | Request aggregate mutates applicant; application service coordinates; domain service coordinates. | Application service coordinates request approval + ApplicantParty verification with no partial writes. | Keeps aggregate boundaries clean and transaction orchestration explicit. | review approve slice, tests | Important aggregate/application service boundary. | Likely ADR |
| ADN-034 | Approval does not create AgreementProposalExchange | Agreement proposal starts by employee action after request is Approved. | Auto-create on approval; separate action. | Approval enables but does not create agreement proposal. | Avoids coupling review to agreement lifecycle and supports future extension slice. | review slice, agreement slice, extension register | Strong extension pressure example. | Candidate |
| ADN-035 | RejectionFeedback optional in domain, warning in UI | Empty rejection feedback may be allowed but undesirable. | Required in domain; optional domain + UI warning; no warning. | Optional in domain; Client/UI warns/confirms empty feedback. | Keeps domain invariant minimal while preserving UX quality. | reject slice, client UI | Domain vs UX validation split. | Candidate |
| ADN-036 | Empty feedback string normalization is not accepted yet | API implementation must decide null vs empty before wiring. | Empty string stored; trim-to-null; validation error. | Open implementation question, not accepted architecture decision. | Prevents hiding undecided API normalization behind domain rule. | reject API/client slice | Shows explicit non-decision. | None until resolved |
| ADN-037 | Failed approve/reject must preserve state | Request review transitions should not partially mutate on failure. | Mutate as attempted; precheck/no-write. | Use CanDo/precheck and no-write tests. | Prevents inconsistent review decision/status. | request review tests | Testing/domain quality. | Candidate |
| ADN-038 | AgreementProposalExchange is separate aggregate and later than first L1 cut | Agreement exchange has independent lifecycle, versions and active proposal. | Embed in request; separate aggregate; implement now. | Separate aggregate later; out of first L1 cut. | Avoids overloading ConnectionRequest and keeps first cut narrow. | agreement slices, domain draft | Aggregate boundary explanation. | Candidate |
| ADN-039 | Agreement proposal replacement uses Superseded/Replaced semantics, not Rejected | Client proposal replaced by counter-proposal is not final refusal. | Mark previous proposal Rejected; SupersededByCounterProposal; Replaced. | Use superseded/replaced semantics for replacement. | Avoids confusing lifecycle replacement with business rejection. | agreement proposal lifecycle | Domain language value. | Candidate |
| ADN-040 | AgreementProposalExchange stores active proposal version, not child id | Child entities live inside aggregate; ids are mainly technical/external. | ActiveProposalId; ActiveProposalVersion; both. | Store active proposal version in aggregate; child IDs remain technical identities. | Keeps aggregate-internal identity separate from DB identity. | agreement aggregate design | Aggregate modeling explanation. | Candidate |
| ADN-041 | Proposal Version generated by aggregate; public Number may come from DB sequence | Version is domain ordering inside exchange; Number is public/business identity. | DB both; domain both; aggregate version + DB number. | Exchange assigns Version; DB sequence may assign public Number. | Separates domain sequence from public number identity. | agreement aggregate/persistence | Strong identity distinction. | Candidate |
| ADN-042 | No final agreement rejection/closed state in current core | Final refusal likely changes request post-approval outcome. | Add Closed/Rejected now; keep proposal states only; defer. | Defer final rejected/closed state until request-level outcome semantics exist. | Avoids premature lifecycle state without scenario semantics. | agreement/request post-approval slices | Good deferral example. | Candidate |
| ADN-043 | RequestNumber and AgreementProposalNumber are future/public numbers | Technical DB Id exists but business/public numbers may be separate. | Id only; public numbers now; future numbers. | Keep request/proposal public numbers future unless needed by scenario/API. | Avoids premature persistence decisions while preserving business identity concept. | request/agreement persistence | Identity modeling. | Candidate |
| ADN-044 | ASP.NET Core cookie auth requires explicit antiforgery client/server support | Cookie-authenticated browser unsafe requests need CSRF protection. | Ignore; custom token; ASP.NET IAntiforgery + client helper. | Use IAntiforgery server support and client token fetch/store/attach/refetch around session changes. | Security correctness; adds shared client/server support. | client cross-cutting, auth/server | Security chapter value. | Candidate |
| ADN-045 | Document responsibility map prevents workflow leakage | Workflow rules were spreading across local files. | Allow local rules; centralize all; define responsibility map. | Centralize global rules and keep local coverage/questions local. | Improves maintainability of planning docs and AI workflow. | planning docs | Process/methodology chapter. | Candidate |
| ADN-046 | ADR workflow uses notes and candidates before full ADRs | Full ADRs are not wanted yet, but important decisions must not be lost. | No ADR until later; full ADRs now; notes + candidates. | Use architecture decision notes as accepted guidance and ADR candidates as promotion backlog. | Captures decisions without over-formalizing too early. | ADR docs, agent protocol | Process/methodology value. | Candidate |

## 5. Open Questions That Must Not Be Treated As Accepted Decisions

| ID | Question | Current assumption / status | Where to resolve |
|---|---|---|---|
| ADQ-001 | Exactly one current active ApplicantParty per account, or one per applicant role/type? | Open. Current L1 only has simple current-active marker. | ApplicantParty persistence slice |
| ADQ-002 | Inactive unverified ApplicantParty cleanup: hard delete or archive? | Open. Current direction allows remove/archive later. | ApplicantParty persistence/retention decision |
| ADQ-003 | Should notifications go to Account.Email, ApplicantParty.Email, or both if they differ? | Open. Notification slice deferred. | notification slice |
| ADQ-004 | RequestNumber lifecycle and assignment timing? | Open/future. | request numbering slice/persistence |
| ADQ-005 | PendingActivation login behavior and stale activation claim refresh? | Open/future. | auth activation slice |
| ADQ-006 | Does activation policy apply identically to client and employee accounts? | Open/future. | auth/employee account slice |
| ADQ-007 | Rejection feedback empty string normalization? | Open before rejection API/client implementation. | SL-REVIEW-002 API/client sidecar |
| ADQ-008 | Should request creation require current active ApplicantParty? | Open/medium. | SL-REQ-001 hardening |
| ADQ-009 | AgreementProposalNumber assignment timing? | Open/future. | agreement proposal persistence |
| ADQ-010 | Proposal text/comment required or optional? | Open/future. | agreement proposal scenario/API planning |
| ADQ-011 | Does entering review create server-side lock/session/assignment? | Assumption: no for now; open for future. | review read/client planning |
| ADQ-012 | Are dashboard filters persisted user preferences? | Assumption: no for now; open for future. | employee dashboard read/client planning |
| ADQ-013 | Is external verification provider needed in current L1? | Assumption: no; avoid provider coupling. | external verification slice |

## 6. Maintenance Rule

When an accepted decision is added to a local slice/domain/client file:

```text
1. If it guides future planning, add/update a note here.
2. If it may deserve formal ADR later, add/update `adr-candidates.md`.
3. If it creates extension pressure, add/update `slice-extension-points-register.md`.
```
