# Scenario Questions Register

Status: active / migrated v1  
Scope: unresolved scenario-stage questions affecting behavior, DATA, validation/security or visible outcomes

## 1. Purpose

This file records unresolved questions discovered while writing or using scenario specs, DATA files, validation/security addenda, behavior items, domain drafts, slice files, client sidecars and implementation planning.

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

## 3. Questions

| ID | Source | Affected behavior items | Question | Why it matters | Options | Current preference | Blocks | Status |
|---|---|---|---|---|---|---|---|---|
| Q-SC-02-001 | SC-02 / SC-15 | ACC-SQ-001 | If user created account but did not activate it and then tries to log in, what should happen? | Changes auth/session UX and authorization placement. | A. reject login B. issue limited session + activation-required page C. allow session but block protected actions | Current core avoids branch because registration creates Active account. | future auth activation work | open |
| Q-SC-15-001 | SC-02 / SC-15 | ACC-SQ-002 | If future AccountActivated policy uses account_activated claim, how is stale claim refreshed after activation state changes? | Claim avoids DB lookup but can become stale. | A. refresh sign-in after activation B. short-lived claims C. DB check on protected paths | future implementation question | future auth activation work | open |
| Q-SC-15-002 | SC-15 | ACC-SQ-003 | Does account activation apply identically to client and employee accounts? | Employee flows may require same policy or separate employee-status policy. | A. same policy B. client only C. separate employee policy | avoid assuming silently | future employee/auth work | open |
| Q-SC-04-001 | SC-04 / SC-10 | REQ-UCQ-001 | Which ApplicantParty data is copied/prefilled into request creation form, and which fields are request-local only? | Affects request creation Client/UI, DTO mapping and user understanding; must not mutate saved ApplicantParty accidentally. | A. summary only B. prefill editable request-local fields C. read-only copied fields | B for fields that belong to request-local input; no mutation of saved ApplicantParty | SL-REQ-001 client sidecar | open |
| Q-SC-07B-001 | SC-07B / SC-05 | REQ-IBS-002 / REQ-CMD-REJECT-001 | Is rejection explanation required or optional? | Affects reject form, DTO, domain rule, client warning and rejected details. | A. required B. optional + UI warning/confirmation C. optional no warning | B; current domain feedback optional, UI should warn/confirm | reject client/API planning | open |
| Q-SC-13D-001 | SC-13D / SC-13B | AGR-LC-007 / AGR-CMD-EMP-NEW-001 | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | Legacy baseline used Rejected for replacement, but current domain decision prefers SupersededByCounterProposal/Replaced semantics. | A. Rejected B. Superseded C. Replaced D. RejectedByCounterProposal | B/C preferred; domain draft uses superseded/replaced meaning | agreement proposal implementation | open |
| Q-SC-13D-002 | SC-13D / SC-13B | AGR-VI-002 | Are proposal text details/comment required or optional? | Affects proposal DTO, validation, client form and domain value requirement. | A. required B. optional C. required only for specific sender/action | pending | agreement proposal planning | open |

## 4. Rule

If a question is resolved and changes sources, update scenario text spec, DATA file, validation/security addendum and affected behavior items.

Do not let implementation planning silently decide scenario behavior.
