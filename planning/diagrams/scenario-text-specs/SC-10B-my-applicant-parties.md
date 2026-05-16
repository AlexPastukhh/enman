# SC-10B — My Applicant Parties

## Status

Future scenario specification draft.

## Purpose

Client views and manages saved ApplicantParty profiles.

This scenario is separated from the current Account page applicant template section:

```text
Account page can show current/default templates per applicant type.
My Applicant Parties is a future management area for all saved ApplicantParties.
```

## Actor / Screen

Actor: Client  
Future screen: My Applicant Parties / Applicant profiles management  
Goal: View, add, edit, delete/archive and choose current/default ApplicantParty templates

## Preconditions

- Client is signed in.
- Client can access future My Applicant Parties management area.

## DATA

Uses:

```text
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
```

## Main Flow

1. Client opens My Applicant Parties.
2. System shows all saved ApplicantParty profiles for the account.
3. Client can filter or visually distinguish profiles by applicant type and verification state.
4. Client can open ApplicantParty details.
5. Client can add a new ApplicantParty of a supported applicant type.
6. Client can choose one ApplicantParty as current/default template for its applicant type.
7. Client can edit ApplicantParty when allowed by safety/history rules.
8. Client can request delete/archive/hide when allowed by safety/history rules.

## Branches

### Add ApplicantParty

```text
-> client chooses applicant type
-> client enters applicant data
-> system creates new ApplicantParty
-> status = NotVerified
-> UI offers to make it current/default template for its type
```

### Set current/default template

```text
-> client chooses saved ApplicantParty
-> system marks it as current/default template for that applicant type
-> previous default for that type stops being default
-> previous ApplicantParty remains stored
-> future request creation prefill uses new current/default template
```

### Delete / archive ApplicantParty [future]

```text
-> client requests delete/archive
-> system checks whether ApplicantParty is used by requests or approved requests
-> UI shows warning level according to deletion risk
-> system may hard-delete only when safe, or archive/hide/deactivate instead
```

### View ApplicantParty details

```text
-> client opens profile details
-> UI shows applicant data, type, verification state and usage/history information when available
```

## Invariants

An account may store multiple ApplicantParty profiles.

At most one ApplicantParty per applicant type can be current/default template at a time.

Changing current/default template does not change previous requests.

ApplicantParty used by previous requests remains meaningful for history and review/audit.

New ApplicantParty starts as NotVerified.

Verification happens during request/review context.

Delete/archive semantics must not break request history.

## Outcomes

- Client can view saved ApplicantParties.
- Client can manage future applicant templates deliberately.
- Client can set current/default template per applicant type.
- Client can add applicant profiles of different supported types.
- Future request creation can select from all saved ApplicantParties or default to current/default template.

## Questions / Decisions

Accepted direction:

```text
Decision:
My Applicant Parties is future management of saved ApplicantParty profiles.

It is not required for the first Account page applicant create/read flow,
but the scenario must preserve the future direction.
```

```text
Decision:
Delete/archive must be warning-driven and usage-aware.

Profiles used by requests or approved requests may need archive/hide instead of hard delete.
```

Open / future-review questions:

```text
Q: Should My Applicant Parties be a separate page, account subpage, modal, or section?
Q: Should delete mean hard delete, archive, hide or deactivate?
Q: What warnings are required for NotVerified unused, used by request, used by approved request, and current/default ApplicantParty?
Q: Should edit mutate profile in place or create a new version?
Q: How much request usage/history is visible in ApplicantParty details?
```

## Diagram Notes

- Draw this as future management area, not as current implemented UI.
- Show Account page current/default templates separately from all saved profile management if needed.
- Show current/default as per applicant type.
- Do not draw adding a new ApplicantParty as deleting old profiles.
