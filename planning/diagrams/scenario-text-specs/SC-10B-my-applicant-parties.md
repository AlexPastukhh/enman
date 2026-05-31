# SC-10B — Applicant Parties Future Management

Status: future scenario addendum / same Applicant Parties page management behavior  
Doc version: v0.1.0  
Source type: scenario text specification addendum  
Parent scenario: `SC-10 — Applicant Parties Page / Applicant Data`

## 1. Purpose

SC-10B is not a separate "My Applicant Parties" page scenario.

It is a future-management addendum for the same Applicant Parties page / section described by SC-10.

Use this file for future behavior that extends the same page beyond current add/read/default planning.

## 2. Same Page Direction

```text
Applicant Parties page / section
        ↓
Default/current templates area
        ↓
Other saved ApplicantParties area
        ↓
Add ApplicantParty action/form
        ↓
Future explicit make default/current action
        ↓
Future delete/archive lifecycle actions
```

## 3. Future Capabilities

Future capabilities on the same Applicant Parties page may include:

```text
- view all saved ApplicantParties grouped by current/default and other saved;
- see current/default per applicant type;
- add ApplicantParty of supported types;
- explicitly make a saved ApplicantParty current/default for its type;
- edit ApplicantParty subject to history/version policy;
- delete/archive/hide ApplicantParty subject to safety rules.
```

## 4. Future Delete / Archive Lifecycle Direction

Delete/archive/hide is not current L1 behavior and should not appear in current behavior items or current tests.

Keep it as an extension/change point until a dedicated lifecycle slice exists.

Future lifecycle rules to evaluate:

```text
- if ApplicantParty has contracts, deletion may be blocked;
- if ApplicantParty has InReview requests, Approved pre-contract requests, or active contract-version exchange processes, deletion/archive may require a strong warning and may cancel/stop related in-progress processes;
- verified ApplicantParty may require stricter warning/rules than unverified ApplicantParty;
- existing historical requests/contracts must not be silently rewritten.
```

Primary shared register for this future pressure:

```text
planning/slices/slice-extension-points-register.md
```

## 5. Out Of Current Scope

```text
- separate My Applicant Parties page;
- current delete/archive behavior;
- current edit/version behavior;
- current lifecycle tests;
- changing existing requests when ApplicantParty is added or default/current changes.
```
