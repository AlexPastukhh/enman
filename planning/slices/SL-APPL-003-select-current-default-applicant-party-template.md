# SL-APPL-003 — Select Current/Default ApplicantParty Template

Status: planned / future explicit same-page action  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Slice type: backend/API + client future command slice

## 1. Slice Overview

Target behavior:

```text
Client explicitly selects one saved ApplicantParty as current/default template for its applicant type.

This action lives on the same Applicant Parties page / section.

Previous current/default for that type is unset.

Future request creation uses the selected ApplicantParty as initial prefill/default.

Existing requests remain unchanged.
```

This is not implicit create behavior.

Creating a second ApplicantParty of the same type must not silently switch current/default.

## 2. Visual Scenario Flow

```text
Client opens Applicant Parties page / section
        ↓
Client sees default/current templates highlighted
        ↓
Client sees other saved ApplicantParties below
        ↓
Client chooses "make default/current" on a saved ApplicantParty
        ↓
System verifies ownership
        ↓
System sets selected party default/current for its type
        ↓
Previous same-type default/current is unset
        ↓
Applicant Parties page highlights the selected ApplicantParty
        ↓
Existing requests remain unchanged
```

## 3. API Direction

Future endpoint shape is not finalized.

Possible direction:

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

Rules:

```text
- authenticated account only;
- selected ApplicantParty must belong to current account;
- action affects only selected ApplicantParty type;
- previous same-type default/current is unset;
- existing requests are not relinked or rewritten;
- no delete/archive behavior in this slice.
```

## 4. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `SL-APPL-003-Q-001` | accepted direction | Is make default/current implicit on create? | No. It is explicit user action after first-of-type initialization rule. | Behavior boundary. | Mirrored by `SL-APPL-Q-002`. |
| `SL-APPL-003-Q-002` | accepted direction | Is this on separate page? | No. It is same Applicant Parties page behavior. | Client flow. | Mirrored by `SL-APPL-Q-006`. |
| `SL-APPL-003-Q-003` | accepted direction | Do existing requests change? | No. Existing requests remain unchanged. | Data safety. | Mirrored by `SC-10-BI-009`. |
| `SL-APPL-003-Q-004` | future review | Exact endpoint shape? | Defer until implementation starts. | API shape. | Local to this future slice until API design starts. |

## 5. Behavior Coverage

| Source behavior item | How draft covers it | Status |
|---|---|---|
| `SC-10-BI-005` One current/default per applicant type | Action sets one selected same-type default/current. | covered as draft |
| `SC-10-BI-007` Additional same-type create does not switch default implicitly | This slice is explicit user action, not create side effect. | covered as draft |
| `SC-10-BI-008` Explicit current/default selection is future same-page behavior | Slice owns the future explicit action. | covered as draft |
| `SC-10-BI-009` Existing requests are unchanged | Action does not relink/rewrite requests. | covered as draft |
| `SC-10-UI-010` Explicit make default/current action is future same-page behavior | Client action belongs on Applicant Parties page. | covered as draft |

## 6. Test / Verification Plan

```text
- owned ApplicantParty can become default/current;
- previous same-type default/current is unset;
- other type default/current unchanged;
- another account’s ApplicantParty is rejected;
- create of second same-type party does not switch default/current without explicit action;
- existing requests remain unchanged.
```

## 7. Out Of Scope

```text
- add ApplicantParty command;
- read Applicant Parties page state;
- delete/archive lifecycle;
- edit/version behavior;
- request creation.
```
