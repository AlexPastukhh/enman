# Slice Questions Register

Status: active register / ApplicantParty template-per-type model synchronized  
Scope: shared overview of currently relevant local slice, client sidecar and cross-cutting/helper questions, future-review items and important accepted directions

## 1. Purpose

This register makes important local slice questions visible from one place.

Local `Questions / Decisions` sections keep detailed context.

Implemented slices can still have open questions.

Implementation status does not close future UX/security/cache/API/domain questions.

## 2. What This Register Is Not

This register is not the source for Scenario Flow or Behavior Items.

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

for scenario text/DATA/UI/behavior item sources.

## 3. Questions Register

| ID | Local file(s) | Area | Question status | Question / decision question | Assumption / current direction | Impact / shared target | Blocks current work? |
|---|---|---|---|---|---|---|---|
| SL-APPL-Q-001 | `SL-APPL-001-create-individual-applicant-party.md` | Applicant model | accepted direction | Does creating ApplicantParty replace existing ones? | No. New ApplicantParty is added; existing ones remain unchanged. | scenario/domain behavior | yes for applicant redesign |
| SL-APPL-Q-002 | `SL-APPL-001`, `SL-APPL-002` | Account page | accepted direction | Does Account page show ApplicantParties inline? | Yes. Current direction: top current/default templates by type, below all saved ApplicantParties. | Account page read/UI | no |
| SL-APPL-Q-003 | `SL-APPL-001` | Details page | accepted direction | Is a separate ApplicantParty details page required? | No, not for current direction. Details can be inline. | avoids premature details slice | no |
| SL-APPL-Q-004 | `SL-APPL-001` | API contract | accepted direction | Should create response return ApplicantPartyId? | Yes, for stable identity, cache/refetch, selection and future actions. | API/client contract | no |
| SL-APPL-Q-005 | `SL-APPL-003` | Default template | future review | How does user mark ApplicantParty as current/default when one already exists? | Future explicit slice/action. | default-template API/UI | yes before implementation |
| SL-APPL-Q-006 | `SL-APPL-001`, `SC-10 UI` | UI marker | accepted direction | How are current/default templates shown? | Highlight selected templates with marker/border or equivalent accessible marker. | UI behavior | no |
| SL-APPL-Q-007 | `SL-APPL-004`, `SL-REQ-004` | Shared service | accepted direction | Should shared creation logic be extracted? | Yes. Reuse from standalone create and request creation with new applicant data. | atomicity/code organization | yes before request redesign |
| SL-APPL-Q-008 | `SL-APPL-001`, `SL-APPL-002` | Initial default | accepted direction | What if no current/default exists for this applicant type? | First created ApplicantParty of the type becomes initial current/default. | default/prefill behavior | yes before default implementation |
| SL-APPL-Q-009 | `SL-APPL-001`, `SL-APPL-003` | Existing default | accepted direction | What if current/default already exists for this applicant type? | Creating another same-type ApplicantParty does not silently change existing default. | prevents hidden future prefill changes | yes before default implementation |
| SL-APPL-Q-010 | `SC-10B`, future delete/archive slice | Delete/archive | future review | Does delete mean hard delete, archive, deactivate or hide? | Do not assume hard delete; decide with warning/policy slice. | My Applicant Parties management | no |
| SL-APPL-Q-011 | `SC-10B`, future edit slice | Edit/versioning | future review | Can used ApplicantParty be edited in place? | Future decision; consider immutable profile/version or request snapshot policy. | history correctness | no |
| SL-APPL-Q-012 | `SL-APPL-002` | Read model | accepted direction | Is Account applicant read one current applicant or many saved ApplicantParties? | Many saved ApplicantParties plus current/default per type. | supersedes read-current-only direction | yes before read API |
| SL-REQ-Q-006 | `SC-04`, future `SL-REQ-001.client`, `SL-REQ-004` | Applicant data in request journey | accepted direction | Does request creation include applicant fields/prefill/clear behavior? | Yes. Current/default prefill when available; missing/cleared lead to new applicant data. | request client/API redesign | yes before request UI |
| SL-REQ-Q-007 | `SL-REQ-004` | Applicant context API | accepted direction | Should request creation with new applicant data use two client calls? | No. One server call should create ApplicantParty + Request atomically. | prevents orphan applicant | yes before redesign |
| SL-REQ-Q-018 | `SL-REQ-004` | Existing ApplicantParty | accepted direction | Can request creation use existing saved ApplicantParty? | Yes. Target supports existing ApplicantParty with server-side ownership check. | API/client contract | yes before redesign |
| SL-REQ-Q-019 | `SL-REQ-004` | Snapshot/reference | future review | Should request store ApplicantParty reference, snapshot, or both? | Preserve submitted applicant context; exact model future. | request details/history | no |
| SL-REQ-Q-020 | `SC-04 UI` | Future dropdown | future review | Should request UI allow choosing from all saved ApplicantParties? | Yes later; current/default remains initial selection. | request client UX | no |
| SL-AUTH-Q-007 | `SL-AUTH-003-logout.md` / `.client.md` | CSRF/security | open / future security | When should logout require antiforgery token handling? | Defer to CC-CSRF-001; do not implement custom CSRF inside logout sidecar. | unsafe command security | yes before claiming CSRF |

## 4. Superseded / Removed During Syncs

| Previous register item | Status | Reason |
|---|---|---|
| One current active ApplicantParty per account | superseded for target scenario | Target model is many saved ApplicantParties plus one current/default template per applicant type. |
| Replacement makes previous current non-current | superseded | Creating ApplicantParty is not replacement by default. Explicit default selection is separate. |
| Read-current-individual-only Account page model | superseded for target scenario | Account page needs saved ApplicantParties plus defaults per type. |
| Two client calls for request with new applicant | rejected | Breaks atomic user intent; use one request endpoint with shared creation service. |

## 5. Closed / Resolved Question Policy

Resolved questions may be removed from the active table when they are no longer useful for future work.

If keeping a resolved row, set `Question status = resolved` and make the answer explicit.

Do not leave a resolved question with status `open`.
