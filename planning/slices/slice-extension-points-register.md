# Slice Extension Points Register

Status: active register / ApplicantParty template-per-type model synchronized  
Scope: cross-slice extension points, change points, extension pressure, anti-coupling decisions and related extension questions

## 1. Purpose

This register makes planned extension points and extension pressure visible across slices.

It must be checked before starting a parent slice or `.client.md` sidecar.

It does not replace `slice-questions-register.md` or `slice-scenario-flow-behavior-register.md`.

## 2. Extension Points Coverage

| ID | Parent slice | Future extension slice | Layer | Current seam | Certainty | Time horizon | Status |
|---|---|---|---|---|---|---|---|
| EP-APPL-READ-LIST-001 | SL-APPL-001 | SL-APPL-002 Account Applicant Parties Read | Server + Client | saved list + defaults per type | high | near | planned |
| EP-APPL-DEFAULT-001 | SL-APPL-001 / SL-APPL-002 | SL-APPL-003 Select Current/Default Template | Server + Client | explicit set-default action | high | near/future | planned |
| EP-APPL-CREATE-SERVICE-001 | SL-APPL-001 | SL-APPL-004 ApplicantParty Creation Application Service | Server/Application | service with no SaveChanges | high | near | planned |
| EP-REQ-APPLICANT-CONTEXT-001 | SL-REQ-001 | SL-REQ-004 Create Request With Explicit Applicant Context | Server + Client | applicantContext Existing/New | high | future request redesign | planned |
| EP-APPL-MANAGE-001 | SC-10B | future My Applicant Parties management slices | Client + Server | saved ApplicantParties list/details/edit/delete/archive | medium | later | planned |
| EP-APPL-VERIFY-001 | Request review | Applicant verification workflow/provider | Server + Client | NotVerified state and review context | medium | later | planned |
| EP-REQ-DETAILS-SNAPSHOT-001 | SL-REQ-003 / SL-REQ-004 | request applicant snapshot/reference policy | Server + Client | submitted applicant context preservation | medium | later | future review |

## 3. Extension Pressure / Anti-Coupling Decisions

| ID | Related extension point | Affected current slice | Layer | Decision | Anti-coupling constraint | Revisit when | Status |
|---|---|---|---|---|---|---|---|
| EPRESS-APPL-NO-REPLACE-001 | EP-APPL-DEFAULT-001 | SL-APPL-001 | Server + Client | Creating ApplicantParty does not replace existing defaults when one exists. | Do not hide default mutation inside create. | default selection slice | accepted direction |
| EPRESS-APPL-FIRST-DEFAULT-001 | EP-APPL-DEFAULT-001 | SL-APPL-001 | Server + Client | First ApplicantParty of a type may initialize default. | Do not model default as global per account. | account applicant read/default implementation | accepted direction |
| EPRESS-APPL-SERVICE-001 | EP-APPL-CREATE-SERVICE-001 | SL-APPL-001 / SL-REQ-004 | Server/Application | Use service for creation logic but outer handler owns commit. | Do not call command handler from command handler. | request applicant-context redesign | accepted direction |
| EPRESS-REQ-ATOMIC-001 | EP-REQ-APPLICANT-CONTEXT-001 | SL-REQ-004 | Server/API | New applicant + request should be one atomic server use case. | Do not force two client calls. | request redesign implementation | accepted direction |
| EPRESS-APPL-DELETE-001 | EP-APPL-MANAGE-001 | future My Applicant Parties | Server + Client | Do not assume hard delete. | Use archive/hide/delete policy with warnings. | delete/archive slice | future review |
| EPRESS-REQ-SNAPSHOT-001 | EP-REQ-DETAILS-SNAPSHOT-001 | SL-REQ-003 / SL-REQ-004 | Server + Client | Preserve submitted applicant context. | Do not let later ApplicantParty edits silently rewrite historical request details. | request details/history implementation | future review |

## 4. Change Points Coverage

| ID | Affected slice | Layer | Behavior aspect | Current decision | Status |
|---|---|---|---|---|---|
| CP-APPL-MULTI-001 | SL-APPL-001 / SL-APPL-002 | Server + Client | many saved ApplicantParties | Account may store many ApplicantParties. | accepted direction |
| CP-APPL-DEFAULT-PER-TYPE-001 | SL-APPL-001 / SL-APPL-003 | Server + Client | one default per type | Default/current template is per applicant type. | accepted direction |
| CP-APPL-FIRST-DEFAULT-001 | SL-APPL-001 | Server + Client | first default | First ApplicantParty of type initializes default. | accepted direction |
| CP-APPL-NO-SILENT-SWITCH-001 | SL-APPL-001 / SL-APPL-003 | Server + Client | default switch | Creating another same-type ApplicantParty does not silently switch default. | accepted direction |
| CP-REQ-NEW-APPLICANT-ATOMIC-001 | SL-REQ-004 | Server/API | create request with new applicant | ApplicantParty + Request are created atomically. | accepted direction |
| CP-REQ-SAVED-DROPDOWN-001 | future request client sidecar | Client/UI | choose saved applicant | Future dropdown/list can show all saved ApplicantParties; default remains initial selection. | future review |

## 5. Status Values

```text
planned
candidate
active
accepted direction
assumption
future review
open for future
resolved
superseded
ignored
```
