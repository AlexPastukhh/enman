# Scenario Questions Register

Status: active / applicant template per type policy synchronized  
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
| Q-SC-04-001 | SC-04 / SC-10 / SC-10B | SC-04-BI-003 / SC-04-BI-008 / SC-10-BI-004 | Which ApplicantParty context is used by request creation? | Affects request creation UX, DTO/API direction, historical request context and applicant management. | A. one global current active ApplicantParty B. request-local applicant copy C. one account-owned ApplicantParty selected/created for the request | C. Request uses one account-owned ApplicantParty. Existing current/default per type can prefill. New applicant data creates a new ApplicantParty and uses it for this request. | yes before request client/API redesign | accepted direction |
| Q-SC-04-002 | SC-04 UI | SC-04-UI-004 / SC-04-UI-007 | What happens when no current/default template exists for selected applicant type? | Determines request form initial state. | A. show error B. route to applicant management C. show empty applicant fields | C. Fields are empty; user enters new applicant data directly. Clear action is only needed when prefill exists. | yes before request UI | accepted direction |
| Q-SC-04-003 | SC-04 UI / SC-10 | SC-04-UI-009 / SC-10-UI-008 | Should newly created ApplicantParty become current/default template? | Affects future prefill and user expectation. | A. automatic always B. never C. offer/ask user D. automatic only if no template exists | Offer to make new ApplicantParty current/default for its type. Exact default-on behavior remains client/API slice decision. | yes before final request UI | assumption |
| Q-SC-04-004 | SC-04 / SC-10B | SC-04-UI-013 / SC-10B-BI-001 | Should request creation allow selecting from all saved ApplicantParties? | Affects future request UI and API. | A. only current/default prefill B. dropdown/list of all saved profiles C. manage on separate page only | Future UI may show dropdown/list of all saved ApplicantParties; current/default remains initial prefill/default selection. | future request UI | future review |
| Q-SC-10-001 | SC-10 / SC-04 / SC-10B | SC-10-BI-002 / SC-10-BI-003 | Is current/default ApplicantParty unique per account or per applicant type? | Determines applicant template policy and request creation prefill. | A. one global current active ApplicantParty B. one current/default per applicant type C. no current/default concept | B. At most one current/default ApplicantParty per applicant type. Account may store many ApplicantParties. | yes for future applicant work | accepted direction |
| Q-SC-10-002 | SC-10 UI / DATA | SC-10-UI-002 / SC-10-UI-003 | What read model should Account page use for applicant templates? | Without a read model, Account page cannot stably show per-type templates after reload. | A. no read model B. current individual only C. per-type applicant template read model | Current implementation may stay narrow; target model needs per-type templates later. | future applicant read/template slice | open |
| Q-SC-10-003 | SC-10 UI / SC-04 | SC-10-UI-008 / SC-04-UI-011 | Should applicant data create UI introduce a create-request entry point? | Affects navigation and ownership of request flow. | A. show create-request entry before applicant exists B. show after applicant save C. do not introduce entry here; request slice decides | C. Applicant data UI does not own request creation entry. | future request creation client sidecar | accepted direction |
| Q-SC-10-004 | SC-10 UI | SC-10-UI-006 | Should Account page show applicant verification status? | Affects read model, UI status states and future verification flow. | A. no B. yes as future read/verification state C. only in employee/review screens | Prefer B when backend/read model exposes verification state; new ApplicantParty is NotVerified. | future applicant read / verification work | future review |
| Q-SC-10-005 | SC-10 UI / Auth | SC-10-UI-002 | Should applicant data creation refetch or change current-user/session state? | Affects client cache invalidation and auth/applicant coupling. | A. invalidate current-user B. no session refetch C. include applicant status in current-user later | Prefer B. Session/current-user is account auth state; applicant state belongs to applicant read/model queries. | future auth/client contract if current-user grows applicant summary | accepted direction |
| Q-SC-10B-001 | SC-10B | SC-10B-BI-006 / SC-10B-UI-007 | Delete, archive, hide or deactivate ApplicantParty? | Dangerous if profile is used by requests or approved requests. | A. hard delete B. archive/hide C. deactivate D. depends on usage | Future policy; prefer warning-driven archive/hide unless hard delete is proven safe. | future My Applicant Parties management | future review |
| Q-SC-10B-002 | SC-10B / request history | SC-10-BI-007 | Should ApplicantParty edit mutate in place or create a new version? | In-place edit can change historical request meaning. | A. edit in place B. edit only if unused C. create version D. request snapshot makes edit safe | Future decision; do not break historical request context. | future edit slice | future review |
| Q-SC-07B-001 | SC-07B / SC-05 | REQ-IBS-002 / REQ-CMD-REJECT-001 | Is rejection explanation required or optional? | Affects reject form, DTO, domain rule, client warning and rejected details. | A. required B. optional + UI warning/confirmation C. optional no warning | B; current domain feedback optional, UI should warn/confirm | reject client/API planning | open |
| Q-SC-13D-001 | SC-13D / SC-13B | AGR-LC-007 / AGR-CMD-EMP-NEW-001 | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | Legacy wording used Rejected for replacement, but current direction says replacement by counterproposal is not ordinary rejection. | A. Rejected B. Superseded C. Replaced D. SupersededByCounterProposal | Use superseded/replaced by counterproposal; use SupersededByCounterProposal if a domain state name is needed. Rejected only for explicit rejection/decline. | no longer blocks diagram generation; final enum can be revisited | accepted direction |
| Q-SC-13D-002 | SC-13D / SC-13B | AGR-VI-002 | Are proposal text details/comment required or optional? | Affects proposal DTO, validation, client form and domain value requirement. | A. required B. optional C. required only for specific sender/action | pending | agreement proposal planning | open |

## 4. Resolved / Accepted Direction Notes

### Q-SC-10-001 — ApplicantParty current/default template per type

Current accepted direction for scenario and downstream slice/client planning:

```text
A client account may store multiple saved ApplicantParty profiles over time.

At most one ApplicantParty per applicant type may be current/default template.

Current/default affects future prefill/default selection only.

Adding a new ApplicantParty does not overwrite, delete or deactivate older ApplicantParties.

Existing requests keep their submitted applicant context.
```

Current implementation note:

```text
Current implemented L1 covers narrower individual applicant creation/current read behavior.
It does not implement the full per-type template/all-saved ApplicantParty model yet.
```

### Q-SC-04-001 — Applicant context used by request creation

Current accepted direction:

```text
Request creation uses one account-owned ApplicantParty context.

If current/default template exists for selected applicant type, fields are prefilled.

If user keeps prefilled data, request uses that existing ApplicantParty.

If user clears fields or if no prefill exists, accepted new applicant data creates a new ApplicantParty and uses it for the request.

After creating the new ApplicantParty, UI offers to make it current/default template for the applicant type.
```

### Q-SC-10-003 — Applicant Data UI does not own create-request entry

Current accepted direction:

```text
The applicant data create UI does not introduce a create-request entry point.

Applicant data can become available for future request creation flow,
but the exact global create-request entry location remains a future
request creation client-slice decision.
```

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

## 5. Superseded Direction

```text
Superseded:
One L1 account has one current active ApplicantParty at a time.

Superseded:
Applicant types are alternative forms of one account-level applicant profile,
not independent saved profiles.

Superseded:
When future replacement/edit flow accepts new applicant data,
the newly accepted ApplicantParty becomes current active and the previously current
ApplicantParty is no longer current globally.
```

## 6. Rule

If a question is resolved and changes sources, update scenario text spec, DATA file, validation/security addendum and affected behavior items.

Do not let implementation planning silently decide scenario behavior.
