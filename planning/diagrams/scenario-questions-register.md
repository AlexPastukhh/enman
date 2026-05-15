# Scenario Questions Register

Status: active  
Scope: unresolved scenario-stage questions affecting behavior, DATA, validation/security or visible outcomes

## 1. Purpose

This file records unresolved questions discovered while writing or using scenario text specs, DATA files, validation/security addenda, behavior items, domain drafts, slice files, client sidecars and implementation planning.

Implementation-only questions should stay in slice files, `.client.md` files or `planning/slices/slice-implementation-notes-register.md`.

## 2. Scenario Question Loop

```text
question appears
-> classify as scenario-level or implementation-only
-> if scenario-level, record/update here
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update validation/security addendum if needed
-> update behavior items if required behavior changed
-> continue implementation planning
```

## 3. What Belongs Here

Add a question here if it affects:

```text
scenario behavior
DATA
validation/security
user-visible outcome
scenario semantics
downstream API contract because scenario meaning is unclear
```

Do not add questions here if they only affect route/component/hook naming, cache invalidation detail or internal implementation structure.

## 4. Questions

| ID | Source | Affected behavior items | Question | Why it matters | Options | Current preference | Blocks | Status |
|---|---|---|---|---|---|---|---|---|
| Q-SC-04-001 | SC-04 / request creation DATA | REQ-CMD-CREATE-001, REQ-UCQ-001 | Which ApplicantParty data is copied/prefilled into request creation form, and which fields are request-local only? | Affects request creation Client/UI, DTO mapping and user understanding. | A. show ApplicantParty summary only B. prefill editable request-local fields C. read-only copied fields | B for request-local input; no mutation of saved ApplicantParty | Client/UI planning | open |
| Q-SC-05-001 | SC-05 / My Requests DATA | REQ-READ-001 | What exact request summary is enough for the client to identify a request? | Affects My Requests read DTO and UI. | A. object address + status B. add created date C. add applicant summary D. add request type | A/B likely for L1 | read/client slice | open |
| Q-SC-06-001 | SC-06 / Employee Dashboard DATA | EMP-READ-001 | What exact request summary is enough for employee dashboard? | Affects employee dashboard read DTO and UI. | A. object address + applicant + status B. add created date C. add request type | A for L1; add date if available | employee read/client slice | open |
| Q-SC-07B-001 | SC-07B / rejection feedback | REQ-CMD-REJECT-001, REQ-IBS-002 | Is rejection explanation required or optional? | Affects reject form, DTO, domain rule, client warning and rejected details. | A. required B. optional + UI warning/confirmation C. optional no warning | B | reject client/API/domain | open |
| Q-SC-07B-002 | SC-07B / review action availability | REQ-LC-004, REQ-LC-006 | Should processed requests show disabled review actions with explanation or hide actions? | Affects user-visible review details behavior. | A. hide B. disabled with explanation | B if explanation is useful; implementation-only if scenario only requires no action | client sidecar | open |
| Q-SC-13D-001 | SC-13D / agreement proposal start | AGR-UCQ-002 | Where should employee start agreement proposal after approval: approved request details or separate agreements area? | Affects post-approval navigation and agreement slice entry point. | A. approved request details B. employee agreements area C. both | pending | agreement/client slice | open |
