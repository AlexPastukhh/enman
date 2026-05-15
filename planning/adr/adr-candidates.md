# ADR Candidates

Status: current candidate list  
Scope: promotion backlog for decisions that may later become full numbered ADRs

## 1. Purpose

This file collects possible future full ADRs.

A candidate is not the primary guiding source.

Guiding accepted decisions live in:

```text
planning/adr/architecture-decision-notes.md
```

A candidate means:

```text
This decision may matter enough to document formally later.
```

## 2. Source Priority

```text
1. Full numbered ADR, if present.
2. architecture-decision-notes.md.
3. Local slice/domain/client decision sections.
4. adr-candidates.md as promotion backlog.
```

If this file conflicts with `architecture-decision-notes.md`, stop and clarify.

## 3. Candidate Table

| Candidate ID | Candidate ADR | Accepted decision note(s) | Why it may need full ADR | Current direction | Promotion priority | Open questions |
|---|---|---|---|---|---|---|
| ADR-CAND-001 | Domain foundation before application slices | ADN-001, ADN-002, ADN-003 | Establishes staged implementation method. | Domain foundation first, then slices. | High | None blocking |
| ADR-CAND-002 | Slice definition and discovery criteria | ADN-006 | Central methodology for implementation and diploma. | Slice = observable behavior + implementation path + independent testability. | High | None blocking |
| ADR-CAND-003 | Testing strategy: unit first, no-write behavior, integration later | ADN-004, ADN-005 | Defines quality strategy and test pyramid for L1. | Unit tests first, no-write behavior required. | Medium/High | None blocking |
| ADR-CAND-004 | Scenario question loop and AI planning protocol | ADN-007 | Important AI-assisted planning methodology. | Relevant questions + assumptions + source update loop. | Medium | None blocking |
| ADR-CAND-005 | Per-scenario behavior items and UI specs | ADN-008, ADN-009 | Traceability from scenario to domain/slice/client tests. | Per-scenario behavior items; UI specs when UI planning starts. | Medium | Concrete SC-XX UI specs created later |
| ADR-CAND-006 | Parent slice + `.client.md` sidecar split | ADN-010, ADN-011 | Defines server/client planning boundary. | Parent owns API/vertical behavior; sidecar owns concrete client. | Medium/High | None blocking |
| ADR-CAND-007 | Client architecture mapping for slice sidecars | ADN-012, ADN-013, ADN-014, ADN-015 | Explains why planning slices do not map 1:1 to frontend features. | Read -> pages/entities; command -> pages/features/entities; widgets only after reuse. | High | None blocking |
| ADR-CAND-008 | Client-wide UI conventions | ADN-016, ADN-017, ADN-018, ADN-019, ADN-020, ADN-021 | Cross-cutting client architecture/testing conventions. | planning/client/cross-cutting owns conventions. | Medium | Future UI framework decision if any |
| ADR-CAND-009 | Extension points, change points and extension pressure | ADN-022, ADN-023, ADN-034 | Important future-proofing vs overengineering trade-off. | Record locally + register; anti-coupling where needed. | High | Concrete extension slices later |
| ADR-CAND-010 | ApplicantParty version-like persistence policy | ADN-024, ADN-025, ADN-026, ADN-027 | Important domain modeling and history/verification decision. | Version-like records; preserve verified/request-referenced/current active. | High | cleanup archive/delete; active per account/type |
| ADR-CAND-011 | Account activation guard placement | ADN-028, ADN-029 | Auth/framework vs domain boundary. | Current core Active; app/auth guard calls EnsureActivated. | Medium | future PendingActivation behavior |
| ADR-CAND-012 | Request creation and read projection model | ADN-030, ADN-031, ADN-032 | Write model vs read model trade-off. | Request stores ApplicantPartyId; read projections join through ApplicantParty. | High | current active ApplicantParty requirement |
| ADR-CAND-013 | Request approval verifies ApplicantParty via application service | ADN-033, ADN-034, ADN-037 | Cross-aggregate orchestration and transaction boundary. | App service coordinates request approval + applicant verification; no agreement proposal auto-create. | High | transaction implementation details |
| ADR-CAND-014 | Rejection feedback optional in domain + UI warning | ADN-035, ADN-036 | Domain invariant vs UX validation distinction. | Optional domain feedback; UI warning/confirmation. | Medium | empty string normalization |
| ADR-CAND-015 | AgreementProposalExchange aggregate and proposal lifecycle | ADN-038, ADN-039, ADN-040, ADN-041, ADN-042, ADN-043 | Aggregate boundary, versioning, replacement semantics and final refusal deferral. | Separate aggregate later; superseded/replaced semantics; active version; final refusal deferred. | Medium later | proposal number timing; text/comment requirement |
| ADR-CAND-016 | CSRF / antiforgery support with cookie auth | ADN-044 | Security architecture with browser/cookie auth. | ASP.NET IAntiforgery + client token helper/refetch. | Medium | exact client helper implementation |
| ADR-CAND-017 | Planning document responsibility map | ADN-045 | AI/doc workflow maintainability. | Central responsibility map + local content exception. | Medium | future centralization audit |
| ADR-CAND-018 | ADR workflow without full ADRs yet | ADN-046 | Process decision for capturing architecture decisions without premature formalization. | Architecture decision notes guide; candidates are promotion backlog. | Medium | when to promote first full ADR |

## 4. Likely First Full ADRs Later

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
