# Slice Questions Register

Status: active register  
Scope: shared overview of currently relevant local slice, client sidecar and cross-cutting/helper questions

## 1. Purpose

This register makes important local slice questions visible from one place.

Local `Questions / Decisions` sections keep detailed context.

This register answers:

```text
Where are the still-open, future-review or otherwise important questions discovered by local slice/client/cross-cutting docs?
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

The goal is not to duplicate every paragraph from local docs.

The goal is to make unresolved, future-review and important accepted-direction questions discoverable from one shared place.

## 3. What Belongs Here

Mirror a local question here when it is:

```text
- open;
- blocked;
- an assumption waiting for confirmation;
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

## 4. Question Status And Assumption Rule

Every row must make the question state explicit.

Required fields:

```text
ID
Local file(s)
Area
Question status
Question
Assumption / current direction
Impact / shared target
Blocks current work?
```

Status values:

| Status | Meaning |
|---|---|
| `open` | Needs an answer before related behavior/contract/design can be finalized. |
| `blocked` | Cannot be answered until another decision/source/implementation is available. |
| `assumption` | A working answer is being used so draft work can continue; user confirmation/refinement is expected. |
| `accepted direction` | Direction is accepted enough for planning and should be remembered by future work. |
| `future review` | Not a current blocker; revisit when the related future slice/hardening/client work starts. |
| `resolved` | Answered and no longer open. Keep only if useful for traceability. |
| `superseded` | Replaced by a newer question/decision/source. |
| `local only` | Intentionally not mirrored globally; local file must say why. |

Assumptions must be explicit.

For draft work, use:

```text
Draft assumes ... until user confirms/refines.
```

For implemented slices, use:

```text
Current implementation evidence says ...
```

Do not mark an assumption as resolved.

## 5. Relationship To Other Registers

| Register | Use for |
|---|---|
| `slice-questions-register.md` | Shared overview of currently relevant local slice questions |
| `slice-extension-points-register.md` | Extension points, change pressure, anti-coupling decisions and extension-related cross-slice questions |
| `slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes not yet assigned to an active file |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change source scenario behavior or diagrams |
| `planning/adr/adr-candidates.md` | Possible future architecture decisions needing full ADR |

A question can appear here and also have a related EP/NOTE/ADR entry when appropriate.

## 6. Intake Rule

Before starting or updating a slice/client sidecar:

```text
1. Search this register by slice id, scenario id, area and status.
2. Check whether any question affects the current draft.
3. Keep or update local question context in the slice/client file.
4. Update this register when the local status or assumption changes.
5. If the question is extension/change pressure, also update `slice-extension-points-register.md`.
6. If the question is a concrete future implementation/client/testing note, also update `slice-implementation-notes-register.md`.
7. If the question changes scenario meaning, use the scenario question/clarification workflow.
```

## 7. Questions Register

| ID | Local file(s) | Area | Question status | Question | Assumption / current direction | Impact / shared target | Blocks current work? |
|---|---|---|---|---|---|---|---|
| Q-SL-ACC-001 | `SL-ACC-001-register-client-account.md` | Registration uniqueness | future review | Should duplicate email be enforced by application precheck, DB unique constraint, or both? | Current implemented slice uses application precheck. Draft assumes DB uniqueness is future hardening unless explicitly pulled into current persistence work. | possible future hardening / DB constraint decision | no |
| Q-SL-ACC-002 | `SL-ACC-001-register-client-account.md` | Account lifecycle | future review | Should PendingActivation be introduced after registration? | Current L1 keeps registration Active. Draft assumes PendingActivation/email confirmation is a separate future slice. | extension/future slice | no |
| Q-SL-ACC-003 | `SL-ACC-001-register-client-account.md` | Auth guard | open | Where should active-account guard be enforced? | Draft assumes shared application/auth boundary, not duplicated inside every aggregate. Needs confirmation before protected future slices depend on this policy. | cross-slice auth/security planning | yes for protected future slices |
| Q-SL-APPL-001 | `SL-APPL-001-create-individual-applicant-party.md` | Applicant versioning | open | One current active ApplicantParty per account or per applicant type? | Draft assumes current active individual applicant party for current implemented request flow; final replacement policy remains unresolved. | extension/change pressure | no for current create slice |
| Q-SL-APPL-002 | `SL-APPL-001-create-individual-applicant-party.md` | API error semantics | future review | Should missing account return validation, unauthorized, forbidden, or not found? | Current implementation returns validation problem for missing account after authenticated context. Draft assumes this stays until API/security semantics change. | API/security semantics | no |
| Q-SL-APPL-003 | `SL-APPL-001-create-individual-applicant-party.md` | Data semantics | accepted direction | Should applicant contact email duplicate account email? | Applicant email may differ from account email. Keep separate unless scenario/DATA changes. | scenario/DATA consistency | no |
| Q-SL-REQ-001 | `SL-REQ-001-create-connection-request.md` | My Requests read model | open | What exact My Requests list/detail response does the client need after navigation? | Draft assumes request creation command does not need returned request data; read/list slice defines My Requests shape later. | future read/client slice | no |
| Q-SL-REQ-002 | `SL-REQ-001-create-connection-request.md` | Applicant party versions | future review | How are older applicant party versions made inactive when replacement/edit flow is implemented? | Current request flow uses current active applicant lookup. Draft assumes version management is handled by future replacement slice. | applicant replacement slice | no |
| Q-SL-REQ-003 | `SL-REQ-001-create-connection-request.md` | UI/client | open | What concrete page/form implements request creation? | Draft assumes no `.client.md` is created until concrete client work starts. | client sidecar | no |
| Q-SL-REQ-004 | `SL-REQ-001-create-connection-request.md` | Security | open | When should unsafe browser commands enforce CSRF? | Draft assumes CC-CSRF-001 governs concrete unsafe browser request handling; current backend slice docs do not implement CSRF. | CSRF cross-cutting slice | no for current docs |

## 8. Closed / Resolved Question Policy

Resolved questions may be removed from the active table when they are no longer useful for future work.

If keeping a resolved row, set `Question status = resolved` and make the answer explicit in `Assumption / current direction`.

Do not leave a resolved question with status `open`.

Do not leave an open question without an assumption/current direction.
