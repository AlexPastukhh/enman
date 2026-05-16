# Scenario Questions Register

Status: active / ApplicantParty template-per-type model synchronized  
Scope: unresolved scenario-stage questions affecting behavior, DATA, validation/security or visible outcomes

## 1. Purpose

This file records unresolved questions discovered while writing or using scenario specs, DATA files, validation/security addenda, behavior items, domain drafts, slice files, client sidecars and implementation planning.

## 2. Scenario Question Loop

```text
question appears
-> classify as scenario-level or implementation-only
-> if scenario-level, record/update here
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update behavior items if required behavior changed
-> continue implementation planning
```

## 3. Active Questions / Decisions

| ID | Source | Affected behavior items | Question | Current preference / assumption | Blocks | Status |
|---|---|---|---|---|---|---|
| Q-SC-04-001 | SC-04 / SC-10 | SC-04-BI-003..010 | Which ApplicantParty data is used by request creation? | Request uses one accepted applicant context: existing saved ApplicantParty or newly created ApplicantParty from entered data. | future request redesign | accepted direction |
| Q-SC-04-002 | SC-04 | SC-04-BI-008 | Should request creation with new applicant use two client calls? | No. One server call should create ApplicantParty + Request atomically. | future request redesign | accepted direction |
| Q-SC-04-003 | SC-04 UI | SC-04-UI-010 | If new ApplicantParty is created while a default already exists, should UI offer to make it default? | Yes as future UI direction; no silent switch. | request client/default slice | future review |
| Q-SC-04-004 | SC-04 UI | SC-04-UI-011 | Should future UI allow choosing from all saved ApplicantParties? | Yes as future extension; current/default remains initial selection. | future request client | future review |
| Q-SC-10-001 | SC-10 / SC-10B | SC-10-BI-002..008 | Is ApplicantParty single current per account or many saved with default per type? | Many saved ApplicantParties; one current/default template per applicant type. | scenario/slice planning | accepted direction |
| Q-SC-10-002 | SC-10 | SC-10-BI-006 | What happens when first ApplicantParty of a type is created? | It may initialize current/default template for that type. | default policy | accepted direction |
| Q-SC-10-003 | SC-10 | SC-10-BI-007 | What happens when another ApplicantParty of same type is created? | Existing default remains unchanged; explicit future action changes default. | default policy | accepted direction |
| Q-SC-10-004 | SC-10B | SC-10B-BI-006 | What is delete semantics? | Future decision; do not assume hard delete. Use delete/archive/hide wording until policy exists. | future management slice | future review |
| Q-SC-10-005 | SC-10 / SC-05 | request details future | Should requests store ApplicantParty reference, snapshot or both? | Preserve submitted applicant context; exact implementation future. | request details/history | future review |
| Q-SC-10-006 | SC-10B | edit behavior | Can used ApplicantParties be edited in place? | Future decision; consider immutable profile/version or snapshot policy. | edit slice | future review |
| Q-SC-13D-001 | SC-13D / SC-13B | agreement proposal behavior | Should replaced proposal version be named Superseded/Replaced instead of Rejected? | Use superseded/replaced by counterproposal; Rejected only for explicit rejection/decline. | agreement implementation naming | accepted direction |

## 4. Superseded ApplicantParty Direction

Superseded:

```text
One L1 account has one current active ApplicantParty.
Future applicant replacement makes new data current and old data non-current.
```

Replacement direction:

```text
A client account may store many ApplicantParties.
One current/default template may exist per applicant type.
Creating new ApplicantParty does not replace older ones.
Changing default when one exists is a separate explicit behavior.
```

## 5. Rule

If a question is resolved and changes sources, update scenario text spec, DATA file, UI spec and affected behavior items.

Do not let implementation planning silently decide scenario behavior.
