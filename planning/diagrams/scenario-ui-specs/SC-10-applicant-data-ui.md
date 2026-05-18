# SC-10 — Account Applicant Parties UI Scenario

Status: current canonical ApplicantParty UI scenario source / normalized  
Applies to: Account page Applicant Parties section  
Actors: Client  
Related scenario: `SC-10 — Applicant Data`, `SC-10B — My Applicant Parties` as deprecated/future wording source only  
Related slices: ApplicantParty read/create/default sidecars when migrated

## 1. User Goal

The signed-in Client manages their saved applicant parties from the Account page section.

The current UI direction is not a separate "My Applicant Parties" page. It is an Account page section.

## 2. Screen Entry Points

```text
Signed-in Client opens Account page
        ↓
System shows Account page
        ↓
Account page contains My Applicant Parties / Applicant Parties section
        ↓
Client sees current/default applicant parties and other saved applicant parties
        ↓
Client may add an ApplicantParty
```

## 3. Screen Composition

```text
Account page
  Account/profile area
  Applicant Parties section
    Current/default templates area
      highlighted cards
    Other saved ApplicantParties area
      regular cards
    Actions area
      Add ApplicantParty action/form
      Future same-section actions
```

## 4. Visible Data

The Applicant Parties section should show:

```text
current/default ApplicantParty per supported type when available
visual marker/highlight for current/default cards
other saved non-default ApplicantParties
created ApplicantParty after successful add
validation/error feedback for rejected applicant data
```

## 5. Actions

Current first pass:

```text
Add individual ApplicantParty
```

Future actions, not implemented now:

```text
edit ApplicantParty
delete/archive ApplicantParty
explicit make current/default
destructive/history-affecting action warning
```

All future ApplicantParty actions should happen in the Account page Applicant Parties section unless a later decision changes this.

## 6. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|
| No saved ApplicantParties | Empty/default area + add action | Add ApplicantParty | First ApplicantParty of a type may become initial current/default |
| Current/default exists | Highlighted current/default card area | Add ApplicantParty | Highlight does not imply edit/delete support |
| Other saved ApplicantParties exist | Regular saved cards below current/default area | Add ApplicantParty | Existing cards remain visible |
| Add succeeds | New ApplicantParty appears in saved UI | Add another if allowed | Existing requests remain unchanged |
| Add rejected | Validation/error feedback visible | Correct and resubmit | Error must be actionable |
| Future edit/delete/archive | Not rendered as implemented behavior | none now | Requires dedicated future slice/implementation |

## 7. Empty / Loading / Error States

```text
loading: stable section placeholder
empty: clear empty ApplicantParty section with add action
validation errors: near fields or form-level area
server errors: visible and actionable
```

## 8. Actor-Specific Differences

Client-only section first pass.

## 9. Feedback / Validation Requirements

```text
field-level validation for ApplicantParty form
root/server errors in form/section error area
created ApplicantParty remains visible after success
existing ApplicantParties remain visible and unchanged
existing requests are not visually changed by ApplicantParty create/default actions
```

## 10. Accessibility Notes

```text
section has a clear heading
cards have readable text content
current/default marker is not color-only
form fields have labels
field errors are associated with fields when possible
destructive future actions will require confirmation/focus plan
```

## 11. Out of Scope

```text
separate My Applicant Parties page
ApplicantParty details page
current edit/delete/archive implementation
changing existing requests after ApplicantParty create/default action
backend lifecycle implementation
```

## 12. Related Client Slice Drafts

```text
Account page client draft when migrated
ApplicantParty read/create/default client sidecars when migrated
```
