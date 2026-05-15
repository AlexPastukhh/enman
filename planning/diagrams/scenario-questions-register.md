# Scenario Questions Register

Status: active / migrated v1 / applicant current-active policy synchronized  
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
| Q-SC-04-001 | SC-04 / SC-10 | REQ-UCQ-001 | Which ApplicantParty data is used by request creation, and which fields are request-local only? | Affects request creation Client/UI, DTO mapping and user understanding; must not mutate saved ApplicantParty accidentally. | A. request-local applicant copy B. editable request-local applicant fields C. account-level current active ApplicantParty reference | C. Request creation references the account's current active ApplicantParty. Request-local fields are request details/object address. Applicant changes go through SC-10 / future replacement flow before submit. | no longer blocks backend docs; client sidecar must follow this direction | accepted direction |
| Q-SC-10-001 | SC-10 / SC-04 | APPL-CMD-SAVE-001 / REQ-UCQ-001 | Is current active ApplicantParty unique per account or per applicant type? | Determines applicant replacement policy, request creation context and future applicant type behavior. | A. one current active ApplicantParty per account B. one current active ApplicantParty per applicant type | A. One L1 account has one current active ApplicantParty. Applicant types are alternative data shapes for the account-level applicant profile. | future replacement implementation must follow this direction | accepted direction |
| Q-SC-07B-001 | SC-07B / SC-05 | REQ-IBS-002 / REQ-CMD-REJECT-001 | Is rejection explanation required or optional? | Affects reject form, DTO, domain rule, client warning and rejected details. | A. required B. optional + UI warning/confirmation C. optional no warning | B; current domain feedback optional, UI should warn/confirm | reject client/API planning | open |
| Q-SC-13D-001 | SC-13D / SC-13B | AGR-LC-007 / AGR-CMD-EMP-NEW-001 | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | Legacy wording used Rejected for replacement, but the current diagram/source direction is that replacement by counterproposal is not ordinary rejection. | A. Rejected B. Superseded C. Replaced D. SupersededByCounterProposal | Use superseded/replaced by counterproposal in scenario/diagram wording; use SupersededByCounterProposal if a domain state name is needed. Rejected is only for explicit rejection/decline. | no longer blocks diagram generation after source cleanup; exact final enum naming can be revisited during agreement implementation | accepted direction |
| Q-SC-13D-002 | SC-13D / SC-13B | AGR-VI-002 | Are proposal text details/comment required or optional? | Affects proposal DTO, validation, client form and domain value requirement. | A. required B. optional C. required only for specific sender/action | pending | agreement proposal planning | open |

## 4. Resolved / Accepted Direction Notes

### Q-SC-10-001 — Current active ApplicantParty uniqueness

Current accepted direction for scenario and downstream slice/client planning:

```text
One L1 account has one current active ApplicantParty at a time.

Applicant types are alternative data shapes for that account-level applicant profile,
not separate simultaneously-active applicant contexts per type.

When future applicant replacement/edit flow accepts new applicant data,
the newly accepted ApplicantParty becomes current active and the previously current
ApplicantParty is no longer current.
```

Current implementation note:

```text
Current implemented L1 covers narrow individual applicant creation and current active individual lookup.
It does not implement full replacement/versioning across applicant types yet.
```

### Q-SC-04-001 — Applicant context used by request creation

Current accepted direction:

```text
Request creation references the account's current active ApplicantParty.

Request-local fields are request details and object address.

If applicant data is missing or wrong, the user goes through SC-10 Applicant Data
/ future replacement flow before request submission.
```

This replaces the earlier request-local applicant override wording.

### Q-SC-13D-001 — Agreement proposal replacement terminology

Current accepted direction for scenario summaries, DATA summaries and diagram generation:

```text
Do not describe proposal replacement/counterproposal as ordinary Rejected.

Use:
- superseded/replaced by counterproposal;
- SupersededByCounterProposal, if a domain state name is needed.

Rejected remains valid only for explicit rejection/decline.
```

This resolves the diagram/source conflict for current diagram generation.

Remaining future implementation detail:

```text
The final domain enum name can still be reviewed during agreement proposal implementation.
```

## 5. Rule

If a question is resolved and changes sources, update scenario text spec, DATA file, validation/security addendum and affected behavior items.

Do not let implementation planning silently decide scenario behavior.
