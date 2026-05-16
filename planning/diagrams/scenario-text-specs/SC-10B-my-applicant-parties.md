# SC-10B — My Applicant Parties

Status: future scenario specification draft  
Source family: scenario text + DATA + UI scenario + behavior items  
Related scenario: `SC-10 Applicant Data`

## 1. Purpose

Client manages saved ApplicantParty profiles over time.

This scenario is future-facing. Current Account page can show Applicant Parties inline, but a dedicated management area may be introduced later when list/details/edit/delete/default-selection behavior grows.

Target model:

```text
- account may have many saved ApplicantParties;
- one current/default template may exist per applicant type;
- current/default controls future request prefill/default selection;
- saved ApplicantParties remain available for requests/history unless explicitly deleted/archived under safe policy;
- new ApplicantParty starts NotVerified;
- ApplicantParty verification happens during request review context.
```

## 2. Actor / Screen

Actor: Client  
Screen: Account page Applicant Parties section or future My Applicant Parties page  
Goal: Review and manage saved ApplicantParty profiles

## 3. Main Flow

1. Client opens Applicant Parties management area.
2. System shows current/default templates by applicant type when available.
3. System shows all saved ApplicantParties.
4. Client can view ApplicantParty details inline or in future details view.
5. Client can add ApplicantParty of supported type.
6. Client can explicitly select one saved ApplicantParty as current/default template for its applicant type.
7. Client can edit ApplicantParty when policy allows it.
8. Client can delete/archive/hide ApplicantParty when policy allows it.
9. System warns before destructive or potentially risky removal.

## 4. Branches

### Select current/default template

```text
-> client selects saved ApplicantParty
-> system checks ownership and applicant type
-> selected ApplicantParty becomes current/default for that type
-> previous default for that type is no longer default
-> existing requests remain unchanged
```

### Delete/archive ApplicantParty [future]

```text
-> client attempts to remove ApplicantParty
-> system checks whether it is used by requests or approved/reviewed flows
-> if safe, removal/hide/archive can proceed
-> if risky, user sees warning or action is blocked
```

### Add ApplicantParty

```text
-> client enters applicant data
-> new ApplicantParty is created
-> starts NotVerified
-> if no default for type, it may become initial default
-> otherwise it remains saved until explicit default selection
```

## 5. Questions / Decisions

Accepted direction:

```text
Decision:
My Applicant Parties is a future management area, not required for the first Account page applicant create flow.
```

Future review:

```text
Q: Should dedicated My Applicant Parties page exist, or is Account page enough?
Q: Does delete mean hard delete, archive, deactivate or hide?
Q: Can a used ApplicantParty be edited in place, or should edit create a new profile/version?
Q: What warnings are required before delete/archive?
Q: Should current/default selection be available from Account page or only management page?
```

## 6. Source Links

```text
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md
planning/diagrams/scenario-ui-specs/SC-10B-my-applicant-parties-ui.md
planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
```
