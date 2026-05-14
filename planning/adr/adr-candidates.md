# ADR Candidates

Status: current candidate list  
Scope: architecture decisions discovered during domain and slice planning

## 1. Purpose

This file collects ADR candidates before they are promoted into full ADRs.

A candidate is not a final ADR.

A candidate means:

```text
This decision may matter enough to document formally later.
```

## 2. Candidate Table

| ADR candidate | Decision needed | Context | Options | Current direction | Affected scenarios/slices | Urgency | Notes |
|---|---|---|---|---|---|---|---|
| Domain foundation before application slices | Whether to implement a narrow domain foundation before full application/API/UI slices. | Current workflow uses L1 domain classes + unit tests before application/API/persistence/UI slice planning. | Full-stack slice first; domain foundation first; API-first. | Domain foundation first, then scenario/application slices. | L1 domain cut; all later L1 slices. | High | Supports stable domain behavior and unit tests before wiring. |
| Slice definition and discovery criteria | How to define implementation slices. | Slice planning must be scenario-derived and independently testable. | Endpoint-based slices; layer-based tasks; behavior-based slices. | Slice = observable behavior + implementation path + independent testability. | All slice drafts. | High | Important for diploma explanation and implementation planning. |
| ApplicantParty version-like persistence policy | How to handle applicant data edits. | ApplicantParty may be verified, request-referenced, or current active. | Mutate existing record; immutable snapshots; version-like records with cleanup. | Create new version-like ApplicantParty; preserve verified/request-referenced/current active; cleanup irrelevant inactive unverified later. | Applicant data slice; request creation slice. | High | May become full ADR before persistence implementation. |
| Approval verifies ApplicantParty via application service | Whether request approval should also verify ApplicantParty and where orchestration belongs. | Current implementation decision: approval verifies ApplicantParty, but ConnectionRequest should not mutate ApplicantParty internally. | Request aggregate mutates applicant; application service coordinates; domain service coordinates. | Application service coordinates request approval + applicant verification using CanDo checks. | Employee approve request slice. | High | Transaction boundary and no-partial-write tests matter. |
| Read projection instead of write aggregate duplication | Whether to store ClientAccountId in ConnectionRequest for read convenience. | Request is applicant-centric and stores ApplicantPartyId. My Requests needs account visibility. | Add ClientAccountId to request; use read projection join; denormalized read model. | Use Dapper/read projection through Request -> ApplicantParty -> ClientAccount. | My Requests read slice; employee dashboard/read slices. | Medium | Good diploma tradeoff: write model purity vs read model convenience. |
| Rejection feedback optional in domain | Whether rejection feedback is a domain invariant. | Domain allows reject without feedback; UI should warn. | Required in domain; optional in domain + UI warning; application warning only. | Optional in domain; UI warning/confirmation. | Employee reject request slice; review UI slice. | Medium | Could become full ADR if business changes. |
| Account activation guard placement | Where to enforce active account requirement. | Current core registration creates Active account; protected use cases require active account. | Domain aggregates check account; application service guard; auth policy/claim. | Account.EnsureActivated exists; application/auth guard calls it; business aggregates do not duplicate activation checks. | Applicant creation; request creation; protected actions. | Medium | Future claim/policy design may need full ADR. |
| External verification provider as plugin slice | How to model applicant verification through third-party API. | Verification is deferred and likely replaceable external dependency. | Inline service; port/adapter plugin; provider-specific implementation. | Treat as plugin/external-integration slice later. | SC-14 verification; future verification slice. | Medium | Requires fake provider and possible contract tests. |
| AgreementProposalExchange as separate aggregate | Whether agreement proposal exchange is separate from ConnectionRequest. | Agreement exchange has own lifecycle, versions and active proposal state. | Embed in request; separate aggregate; simple ContractDraft first. | Separate aggregate later, out of first L1 cut. | Agreement proposal slices. | Low now, higher later | Not implemented in first L1 cut. |
| Final agreement refusal requires request-level outcome | How to model final refusal of agreement exchange. | Final refusal is not current core and likely affects Approved request outcome. | Exchange Rejected only; request post-approval outcome; Closed with reason. | Defer; introduce only with request-level post-approval semantics. | Agreement proposal / agreement result slices. | Low now, higher later | Add when scenarios explicitly require final refusal. |
| Testing strategy: unit first, integration later | How to sequence tests. | L1 domain foundation should be fast and stable before integration wiring. | Integration first; unit first; full E2E first. | Unit tests first; integration after green domain implementation. | L1 domain implementation; later slices. | High | Already documented in testing rules. |

## 3. Promotion Notes

Likely first full ADRs later:

```text
ADR-0001 Domain foundation before application slices
ADR-0002 Slice definition: observable behavior + implementation path + independent testability
ADR-0003 ApplicantParty version-like persistence policy
ADR-0004 Request approval verifies ApplicantParty via application service
ADR-0005 Read projections instead of write aggregate duplication
```

Do not create these full ADRs until explicitly requested.
