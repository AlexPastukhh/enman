# SC-10 — Applicant Data

## Status

Corrected scenario specification draft.

## Purpose

Client provides or updates applicant data.

## Actor / Screen

Actor: Client  
Screen: Applicant Data page / Applicant Data section during request creation  
Goal: Provide applicant data

## Entry Points

Entry A: Client opens applicant data page directly.

Entry B: Client reaches applicant data while creating a request because applicant data is missing or needs update.

## Preconditions

- Client is signed in.
- Applicant data page or request creation context is reachable.

## DETAIL

Applicant data details  
`SC-10-DETAIL-01`

- individual applicant data;
- contact data;
- identity data if relevant;
- future applicant types;
- [VAR:EXPAND]

## Main Flow

1. Client opens applicant data entry context.
2. Client enters or updates applicant data.
3. Client-side validation runs automatically.
4. Client corrects applicant data if validation fails.
5. Client submits/saves applicant data.
6. System checks whether applicant data is accepted.
7. If accepted, applicant data is saved.
8. Saved applicant data is visible/reusable.

## Branches

### Applicant data valid

-> applicant data accepted
-> applicant data is saved
-> updated applicant data is visible/reusable

### Applicant data invalid

-> applicant data invalid
-> validation errors visible
-> client corrects applicant data
-> back to entering applicant data

## Invariants

Invalid applicant data is not saved.

Attach to:

- applicant data accepted?;
- save applicant data transition;

Standalone applicant data editing does not trigger verification.

Attach to:

- verification policy note;

## Outcomes

- Client can provide applicant data.
- Client can update applicant data.
- Applicant data can be reused in request creation.
- Invalid applicant data is not saved.

## ADR / Policy Candidates

ADR?: Verification happens only when data is used in request context.

ADR?: Standalone applicant data editing does not trigger employee verification.

## Diagram Notes

- Do not label this as Create ApplicantParty or Create applicant profile as primary user-facing wording.
- Use Provide applicant data.
- Use entry points, not ambiguous purple opened-from arrows, for the two contexts.
