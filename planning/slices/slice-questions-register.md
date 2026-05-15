# Slice Questions Register

Status: active register  
Scope: shared overview of currently relevant local slice, client sidecar and cross-cutting/helper questions

## 1. Purpose

This register makes important local slice questions visible from one place.

Local `Questions / Decisions` sections keep detailed context.

This register answers:

```text
What questions did local slice/client/cross-cutting docs discover that another future chat must not miss?
```

It should be checked before starting or reviewing a parent slice, `.client.md` sidecar or cross-cutting/helper slice.

## 2. Relationship To Local Files

Local files are still required to contain their own `Questions / Decisions` section.

This register does not replace local questions.

Instead:

```text
local slice/client/cross-cutting question
        ↓
keep local context in the local file
        ↓
mirror currently relevant question here
        ↓
link back from local file to register ID when practical
```

## 3. What Belongs Here

Mirror a local question here when it is:

```text
- open;
- future review;
- accepted direction but important for future work;
- unresolved risk;
- cross-slice relevant;
- client sidecar relevant;
- API/security/testing/architecture relevant;
- likely to affect another slice or later implementation.
```

Tiny resolved local drafting questions do not need to remain here forever.

If a question remains local only, the local file should say why.

## 4. Relationship To Other Registers

| Register | Use for |
|---|---|
| `slice-questions-register.md` | Shared overview of currently relevant local slice questions |
| `slice-extension-points-register.md` | Extension points, change pressure, anti-coupling decisions and extension-related cross-slice questions |
| `slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes not yet assigned to an active file |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change source scenario behavior or diagrams |
| `planning/adr/adr-candidates.md` | Possible future architecture decisions needing full ADR |

A question can appear here and also have a related EP/NOTE/ADR entry when appropriate.

## 5. Intake Rule

Before starting or updating a slice/client sidecar:

```text
1. Search this register by slice id, scenario id, area and status.
2. Check whether any question affects the current draft.
3. Keep or update local question context in the slice/client file.
4. Update this register when the local status changes.
5. If the question is extension/change pressure, also update `slice-extension-points-register.md`.
6. If the question is a concrete future implementation/client/testing note, also update `slice-implementation-notes-register.md`.
7. If the question changes scenario meaning, use the scenario question/clarification workflow.
```

## 6. Questions Register

| ID | Local file(s) | Area | Question | Current direction / assumption | Shared target | Blocks current work? | Status |
|---|---|---|---|---|---|---|---|
| Q-SL-ACC-001 | `SL-ACC-001-register-client-account.md` | Registration uniqueness | Should duplicate email be enforced by application precheck, DB unique constraint, or both? | Prefer both eventually; current slice documents validation behavior. | possible future hardening / DB constraint decision | no | future review |
| Q-SL-ACC-002 | `SL-ACC-001-register-client-account.md` | Account lifecycle | Should PendingActivation be introduced after registration? | No for current L1; separate future email confirmation / activation slice. | extension/future slice | no | future review |
| Q-SL-ACC-003 | `SL-ACC-001-register-client-account.md` | Auth guard | Where should active-account guard be enforced? | Application service / auth framework, not duplicated inside each aggregate. | cross-slice auth/security planning | yes for protected future slices | open |
| Q-SL-APPL-001 | `SL-APPL-001-create-individual-applicant-party.md` | Applicant versioning | One current active ApplicantParty per account or per applicant type? | Unresolved; replacement/current-active slice should decide. | extension/change pressure | no for current create slice | open for future |
| Q-SL-APPL-002 | `SL-APPL-001-create-individual-applicant-party.md` | API error semantics | Should missing account return validation, unauthorized, forbidden, or not found? | Current integration expects validation problem; revisit if auth semantics change. | API/security semantics | no | future review |
| Q-SL-APPL-003 | `SL-APPL-001-create-individual-applicant-party.md` | Data semantics | Should applicant contact email duplicate account email? | No; applicant email may differ from account email. | scenario/DATA consistency | no | accepted direction |
| Q-SL-REQ-001 | `SL-REQ-001-create-connection-request.md` | My Requests read model | What exact My Requests list/detail response does the client need after navigation? | Separate read/list slice should decide. | future read/client slice | no | open |
| Q-SL-REQ-002 | `SL-REQ-001-create-connection-request.md` | Applicant party versions | How are older applicant party versions made inactive when replacement/edit flow is implemented? | Keep current active lookup now; version management later. | applicant replacement slice | no | future review |
| Q-SL-REQ-003 | `SL-REQ-001-create-connection-request.md` | UI/client | What concrete page/form implements request creation? | Dependent client sidecar when concrete client work starts. | client sidecar | no | open |
| Q-SL-REQ-004 | `SL-REQ-001-create-connection-request.md` | Security | When should unsafe browser commands enforce CSRF? | CC-CSRF-001 before broad client unsafe requests; do not implement here. | CSRF cross-cutting slice | no for current docs | open |

## 7. Status Values

```text
open
open for future
future review
accepted direction
resolved
superseded
local only
promoted-to-extension-register
promoted-to-implementation-notes
promoted-to-scenario-question
promoted-to-ADR
```
