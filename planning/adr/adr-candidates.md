# ADR Candidates

Status: current candidate list  
Scope: architecture decisions discovered during domain, slice, client and workflow planning

## 1. Purpose

This file collects ADR candidates before they are promoted into full numbered ADRs.

A candidate is not a final ADR.

A candidate means:

```text
This decision may matter enough to document formally later.
```

Accepted/current directions with detailed rationale are also summarized in:

```text
planning/adr/architecture-decision-notes.md
```

## 2. Candidate Table

| ADR candidate | Decision needed | Context | Options | Current direction | Affected scenarios/slices | Urgency | Notes |
|---|---|---|---|---|---|---|---|
| Domain foundation before application slices | Whether to implement a narrow domain foundation before full application/API/UI slices. | Current workflow uses L1 domain classes + unit tests before application/API/persistence/UI slice planning. | Full-stack slice first; domain foundation first; API-first. | Domain foundation first, then scenario/application slices. | L1 domain cut; all later L1 slices. | High | Supports stable domain behavior and unit tests before wiring. |
| Slice definition and discovery criteria | How to define implementation slices. | Slice planning must be scenario-derived and independently testable. | Endpoint-based slices; layer-based tasks; behavior-based slices. | Slice = observable behavior + implementation path + independent testability. | All slice drafts. | High | Important for diploma explanation and implementation planning. |
| Scenario question loop and AI assumptions protocol | How planning agents should handle ambiguities. | AI-assisted planning can silently invent behavior. | Continue with assumptions; ask everything; ask relevant questions with assumptions. | Ask relevant questions, include assumption, update source artifacts before continuing. | All planning docs and agents. | High | Good methodology/diploma process decision. |
| Per-scenario behavior items | How to structure scenario-derived behavior items. | One compiled baseline became hard to use for slice/client coverage. | Single compiled file; per-scenario files; both. | Per-scenario behavior item files primary; compiled baseline remains historical/cross-check. | Scenario behavior items; slices; client sidecars. | High | Traceability decision. |
| Scenario UI specs and UI behavior items | Whether UI-visible requirements need separate source files. | Client sidecars need UI behavior coverage, but scenario specs should not become React/component specs. | Put in scenario text specs; put in `.client.md`; create scenario UI specs. | Scenario UI specs are created when UI planning starts. | Client slices. | Medium | Useful for UI/test/diploma coverage. |
| Parent slice + `.client.md` sidecar split | How to document client implementation without bloating parent slice. | Parent slice owns vertical behavior and API, client needs detailed UI/client planning. | All in parent; separate sidecar; frontend docs only. | Parent slice + client sidecar. | All client slices. | High | Layer responsibility decision. |
| Client architecture mapping for sidecars | How planning slices map to frontend architecture. | Planning slice is not necessarily frontend feature. | 1:1 slice-feature; ad-hoc; read->pages/entities and command->features/pages/entities. | Read slice -> pages/entities; command slice -> pages/features/entities. | Client sidecars. | High | Strong client architecture explanation. |
| Client-wide cross-cutting conventions | Where to keep UI conventions like styling/a11y/errors/validation. | Some conventions are broader than one slice. | Put in every sidecar; slices/shared; planning/client. | `planning/client/cross-cutting/`. | Client sidecars. | Medium | Avoids duplication and supports tests. |
| Change points / extension points / extension pressure | How to handle future extension pressure without overengineering. | Future slices can affect current design. | Ignore future; abstract everything; explicit pressure/anti-coupling decisions. | Record change/extension/pressure locally and in register. | Parent slices and client sidecars. | High | Important architecture trade-off. |
| ApplicantParty version-like persistence policy | How to handle applicant data edits. | ApplicantParty may be verified, request-referenced, or current active. | Mutate existing record; immutable snapshots; version-like records with cleanup. | Create new version-like ApplicantParty; preserve verified/request-referenced/current active; cleanup irrelevant inactive unverified later. | Applicant data slice; request creation slice. | High | May become full ADR before persistence implementation. |
| ApplicantParty inheritance strategy | Whether inheritance remains appropriate for individual/entrepreneur/legal entity. | Applicant types have different data shapes. | Single type; inheritance; composition/type-specific values. | Keep inheritance for now. | ApplicantParty model. | Medium | Revisit when entrepreneur/legal entity are implemented. |
| Approval verifies ApplicantParty via application service | Whether request approval should also verify ApplicantParty and where orchestration belongs. | Current implementation decision: approval verifies ApplicantParty, but ConnectionRequest should not mutate ApplicantParty internally. | Request aggregate mutates applicant; application service coordinates; domain service coordinates. | Application service coordinates request approval + applicant verification using CanDo checks. | Employee approve request slice. | High | Transaction boundary and no-partial-write tests matter. |
| Read projection instead of write aggregate duplication | Whether to store ClientAccountId in ConnectionRequest for read convenience. | Request is applicant-centric and stores ApplicantPartyId. My Requests needs account visibility. | Add ClientAccountId to request; use read projection join; denormalized read model. | Use Dapper/read projection through Request -> ApplicantParty -> ClientAccount. | My Requests read slice; employee dashboard/read slices. | Medium | Good diploma tradeoff: write model purity vs read model convenience. |
| Rejection feedback optional in domain | Whether rejection feedback is a domain invariant. | Domain allows reject without feedback; UI should warn. | Required in domain; optional in domain + UI warning; application warning only. | Optional in domain; UI warning/confirmation. | Employee reject request slice; review UI slice. | Medium | Could become full ADR if business changes. |
| Account activation guard placement | Where to enforce active account requirement. | Current core registration creates Active account; protected use cases require active account. | Domain aggregates check account; application service guard; auth policy/claim. | Account.EnsureActivated exists; application/auth guard calls it; business aggregates do not duplicate activation checks. | Applicant creation; request creation; protected actions. | Medium | Future claim/policy design may need full ADR. |
| CSRF / antiforgery support with cookie auth | How to protect unsafe browser requests with ASP.NET Core cookie auth. | Cookie-authenticated browser requests need CSRF protection. | Ignore; custom token; ASP.NET IAntiforgery + client token helper. | Use IAntiforgery and client token fetch/store/attach/refetch around session changes. | Client/server cross-cutting auth. | Medium | Security/diploma explanation value. |
| External verification provider as plugin slice | How to model applicant verification through third-party API. | Verification is deferred and likely replaceable external dependency. | Inline service; port/adapter plugin; provider-specific implementation. | Treat as plugin/external-integration slice later. | SC-14 verification; future verification slice. | Medium | Requires fake provider and possible contract tests. |
| AgreementProposalExchange as separate aggregate | Whether agreement proposal exchange is separate from ConnectionRequest. | Agreement exchange has own lifecycle, versions and active proposal state. | Embed in request; separate aggregate; simple ContractDraft first. | Separate aggregate later, out of first L1 cut. | Agreement proposal slices. | Low now, higher later | Not implemented in first L1 cut. |
| Agreement proposal replacement semantics | Whether replacement should mark previous proposal as Rejected. | Counter-proposal replacement is not the same as final refusal. | Rejected; SupersededByCounterProposal; Replaced. | Superseded/replaced semantics. | Agreement proposal lifecycle. | Medium later | Fixes baseline contradiction. |
| Agreement proposal version/number identity split | How to generate proposal version identity and public number. | Version belongs to exchange ordering; Number may be public unique value. | DB both; domain both; aggregate version + DB sequence number. | Aggregate generates Version; DB sequence may generate public Number. | AgreementProposalExchange. | Medium later | Important persistence/domain split. |
| Active proposal reference by version | Whether AgreementProposalExchange stores active proposal ID or version. | Child entities are aggregate-internal; ID is mainly for aggregate references. | ActiveProposalId; ActiveProposalVersion; both. | Store active proposal version. | AgreementProposalExchange. | Medium later | Aggregate modeling decision. |
| Final agreement refusal requires request-level outcome | How to model final refusal of agreement exchange. | Final refusal is not current core and likely affects Approved request outcome. | Exchange Rejected only; request post-approval outcome; Closed with reason. | Defer; introduce only with request-level post-approval semantics. | Agreement proposal / agreement result slices. | Low now, higher later | Add when scenarios explicitly require final refusal. |
| Testing strategy: unit first, integration later | How to sequence tests. | L1 domain foundation should be fast and stable before integration wiring. | Integration first; unit first; full E2E first. | Unit tests first; integration after green domain implementation. | L1 domain implementation; later slices. | High | Already documented in testing rules. |
| Planning document responsibility map | How to prevent workflow rules from leaking into local files. | Workflow and file responsibilities were spreading. | No central map; central responsibility map; strict centralized only. | Responsibility map + local content exception. | Planning docs. | High | Supports long-term AI planning maintainability. |

## 3. Promotion Notes

Likely first full ADRs later:

```text
ADR-0001 Domain foundation before application slices
ADR-0002 Slice definition: observable behavior + implementation path + independent testability
ADR-0003 ApplicantParty version-like persistence policy
ADR-0004 Request approval verifies ApplicantParty via application service
ADR-0005 Read projections instead of write aggregate duplication
ADR-0006 Extension pressure and anti-coupling decisions
ADR-0007 Client sidecar architecture mapping
```

Do not create full numbered ADRs until explicitly requested.
