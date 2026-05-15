# Slice Questions Register

Status: active register / synchronized with current implemented backend L1 slice docs and applicant current-active scenario decision  
Scope: shared overview of currently relevant local slice, client sidecar and cross-cutting/helper questions, future-review items and important accepted directions

## 1. Purpose

This register makes important local slice questions visible from one place.

Local `Questions / Decisions` sections keep detailed context.

This register answers:

```text
Where are the still-open, future-review, deferred or otherwise important questions discovered by local slice/client/cross-cutting docs?
```

It should be checked before starting or reviewing a parent slice, `.client.md` sidecar or cross-cutting/helper slice.

This file is synchronized with:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
```

## 2. Relationship To Local Files

Local files are still required to contain their own `Questions / Decisions` section.

This register does not replace local questions.

Instead:

```text
local slice/client/cross-cutting question or important accepted direction
        ↓
keep detailed context in the local file
        ↓
mirror currently relevant question or accepted direction here
        ↓
use the local file as the source of detailed scope/status
```

The goal is not to duplicate every paragraph from local docs.

The goal is to make unresolved, future-review, deferred and important accepted-direction items discoverable from one shared place.

## 3. What Belongs Here

Mirror a local item here when it is:

```text
- open;
- blocked;
- an assumption waiting for confirmation;
- future review;
- deferred;
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
Question / decision question
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
| `deferred` | Intentionally outside the current implemented slice; belongs to a later slice or planning pass. |
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
| `slice-questions-register.md` | Shared overview of currently relevant local slice questions and important accepted directions |
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

| ID | Local file(s) | Area | Question status | Question / decision question | Assumption / current direction | Impact / shared target | Blocks current work? |
|---|---|---|---|---|---|---|---|
| SL-ACC-Q-001 | `SL-ACC-001-register-client-account.md` | DB hardening | accepted direction | Should duplicate email also be enforced by a DB unique constraint? | User decision: yes. Current implementation evidence says duplicate email is rejected by application precheck. DB unique constraint is accepted future hardening and is not implemented by documentation-only work. | future persistence hardening / DB constraint implementation | no for current docs; yes before claiming DB-level uniqueness |
| SL-ACC-Q-002 | `SL-ACC-001-register-client-account.md` | Activation lifecycle | deferred | Should PendingActivation/email confirmation be introduced? | Current L1 registration creates an Active ClientAccount. Draft assumes PendingActivation/email confirmation is a separate L2 extension slice. | auth lifecycle extension | no |
| SL-ACC-Q-003 | `SL-ACC-001-register-client-account.md` | Auth UX | open | Should registration automatically sign the user in? | Current backend registration returns `AccountId` + `Email` and does not issue a session in this slice. Draft assumes auto-login is a separate auth/client flow decision. | dependent auth/client UI flow | no for current backend docs; yes before final auth UX/client implementation |
| SL-ACC-D-001 | `SL-ACC-001-register-client-account.md` | API contract | accepted direction | Does the current backend registration API accept `passwordConfirm`? | Current implementation evidence says the backend DTO contains `email` + `password` only. Password confirmation is a dependent client/UI validation concern unless a future backend contract change adds it. | API/client contract and registration UI sidecar | no |
| SL-APPL-Q-001 | `SL-APPL-001-create-individual-applicant-party.md` / `SC-10` | Replacement/versioning | accepted direction | Is current active ApplicantParty unique per account or per applicant type? | User/scenario decision: one current active ApplicantParty per account. Applicant types are alternative data shapes for the account-level applicant profile. Future replacement flow should make the new accepted ApplicantParty current and the previous one non-current. | applicant replacement/current-active policy; request creation context | no for current implemented save slice; yes for future replacement/applicant type work |
| SL-APPL-Q-002 | `SL-APPL-001-create-individual-applicant-party.md` | Missing account semantics | future review | Should a missing account under authenticated claim return validation, unauthorized, forbidden or not found? | Current implementation evidence says missing account returns validation ProblemDetails after authenticated context. Keep this unless API/security policy changes. | API/security semantics | no |
| SL-APPL-Q-003 | `SL-APPL-001-create-individual-applicant-party.md` | Applicant verification | deferred | When is external verification required before request creation/review? | Current save command creates applicant data as Unverified and does not require external provider integration. Draft assumes verification/provider behavior is a separate slice/policy. | verification/provider slice and review/request policy | no |
| SL-APPL-Q-004 | `SL-APPL-001-create-individual-applicant-party.md` | Non-individual applicants | deferred | When do entrepreneur/legal-entity applicant shapes enter L1? | Current implemented slice covers IndividualApplicantParty only. Draft assumes entrepreneur/legal entity shapes are separate applicant-type extension slices. | future applicant type slices | no |
| SL-APPL-D-001 | `SL-APPL-001-create-individual-applicant-party.md` / `SC-10` | Data semantics | accepted direction | Should applicant contact email duplicate account email? | Applicant contact email may differ from account email. Keep them separate unless scenario/DATA changes. | scenario/DATA consistency and client form mapping | no |
| SL-APPL-CLIENT-Q-001 | `SC-10-applicant-data-ui.md` / future `SL-APPL-001...client.md` | Account page state | open | How should Account page know whether current applicant party exists after refresh? | Draft assumes a future current-applicant read slice, preferably auth-derived `GET /api/l1/applicant-parties/current-individual` or equivalent. Initial create UI may use local post-submit read-only state only. | future `L1-APPLICANT-PARTY-READ-CURRENT` / Account page client state | no for create command; yes before stable Account page refresh behavior |
| SL-APPL-CLIENT-Q-002 | `SC-10-applicant-data-ui.md` / future `SL-APPL-001...client.md` | Request creation entry | accepted direction | Should applicant-party create UI introduce a create-request entry? | No. Do not show/create request entry in this slice. Exact global entry point is a future request creation client decision. | future `L1-CONNECTION-REQUEST-CREATE.client` | no for applicant create client |
| SL-APPL-CLIENT-Q-003 | `SC-10-applicant-data-ui.md` | Verification status | future review | Should Account page display applicant verification status? | Yes when current-applicant read model exposes it. Not required for first create command. | future read/verification UI | no |
| SL-APPL-CLIENT-Q-004 | `SC-10-applicant-data-ui.md` / auth client baseline | Session coupling | accepted direction | Should applicant party creation refetch current-user/session? | No for current direction. Session/current-user is account-auth state; applicant state belongs to applicant read model unless current-user contract changes. | auth/session client and applicant state ownership | no |
| SL-APPL-CLIENT-Q-005 | `SC-10-applicant-data-ui.md` | Edit / replacement | future review | What does the Edit action do after applicant data is saved? | Edit action may be visible, but actual edit/replacement behavior is a future slice that must follow current-active replacement policy. | future applicant replacement/edit slice | no |
| SL-REQ-Q-001 | `SL-REQ-001-create-connection-request.md` | My Requests read model | open | What exact My Requests list/detail response does the client need after navigation? | Draft assumes request creation command does not need returned request data; the read/list/detail slice defines My Requests shape later. | future read/client slice | no for current backend docs |
| SL-REQ-Q-002 | `SL-REQ-001-create-connection-request.md` / `SC-10` | Applicant versions | accepted direction | How are older applicant party versions made inactive when replacement/edit flow exists? | Scenario direction says one current active ApplicantParty per account. The future replacement flow should make older applicant versions non-current when a replacement is accepted. Exact implementation remains future work. | applicant replacement slice | no for current implemented request slice; yes for replacement/versioning work |
| SL-REQ-Q-003 | `SL-REQ-001-create-connection-request.md` | Client UI | open | What concrete page/form implements request creation? | Draft assumes no `.client.md` is created until concrete client work starts. | request creation client sidecar | no for backend docs |
| SL-REQ-Q-004 | `SL-REQ-001-create-connection-request.md` | Security | open | When should unsafe browser commands enforce CSRF? | Draft assumes CC-CSRF-001 governs concrete unsafe browser request handling. Current backend slice docs do not implement CSRF. | CSRF cross-cutting slice and future client/security work | no for current docs |
| SL-REQ-Q-005 | `SL-REQ-001-create-connection-request.md` | Verification | future review | Must applicant party be verified before request creation? | Current backend implementation does not require applicant verification before request creation. Draft assumes verification requirement is a separate future policy/slice if adopted. | verification/provider policy and request creation rules | no |
| SL-REQ-D-001 | `SL-REQ-001-create-connection-request.md` | Command response | accepted direction | Does create request command require returned request data? | Current implementation returns HTTP success without required response body. Client success handling can show a success message and navigate to My Requests without request id/status/body. | request creation client sidecar and API contract | no |
| SL-REQ-D-002 | `SL-REQ-001-create-connection-request.md` / `SC-04` | Applicant context | accepted direction | Does request creation use request-local applicant data or the account's current active ApplicantParty? | Scenario direction says request creation uses the account's current active ApplicantParty. Request-local fields are request details and object address. Applicant changes go through SC-10 / future replacement flow before submit. | request creation client sidecar, API DTO mapping and scenario behavior | no for current backend docs; yes for request creation UI planning |

## 8. Superseded / Removed During Syncs

| Previous register item | Status | Reason |
|---|---|---|
| `Q-SL-ACC-003` — active-account guard placement | superseded / removed from active register | The current local `SL-ACC-001` `Questions / Decisions` section no longer contains this question. The current local `SL-ACC-Q-003` is the auth UX auto-login question. Reintroduce active-account guard placement as a local question in an auth/security slice if future protected-slice planning needs it. |
| Earlier request-local applicant prefill assumption | superseded | Scenario direction now says request creation references the account's current active ApplicantParty. Applicant data changes go through SC-10 / replacement flow before submit. |

## 9. Closed / Resolved Question Policy

Resolved questions may be removed from the active table when they are no longer useful for future work.

If keeping a resolved row, set `Question status = resolved` and make the answer explicit in `Assumption / current direction`.

Do not leave a resolved question with status `open`.

Do not leave an open question without an assumption/current direction.
