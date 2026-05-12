# SC-17 — Anonymous Request

## Status

Corrected scenario specification draft.

## Purpose

Anonymous user submits request/contact information without an account.

## Actor / Screen

Actor: Anonymous user  
Screen: Anonymous request page  
Goal: Submit request/contact information

## Entry Points

Entry A: Anonymous user opens anonymous request page.

## Preconditions

- Anonymous request page is reachable.
- User is not required to be signed in.

## DETAIL

Anonymous request details  
`SC-17-DETAIL-01`

- request type;
- object/address/location if applicable;
- description/details;
- required anonymous request fields;
- [VAR:EXPAND]

Follow-up contact details  
`SC-17-DETAIL-02`

- email;
- phone if required;
- preferred contact method if relevant;
- [VAR:EXPAND]

## Main Flow

1. Anonymous user opens anonymous request page.
2. Anonymous user enters request data.
3. Anonymous user enters follow-up contact data.
4. Client-side validation runs automatically.
5. User corrects request/contact data if validation fails.
6. User submits anonymous request.
7. System checks whether request/contact data is accepted.
8. If accepted, anonymous request/contact request is recorded.
9. Confirmation is visible.

## Branches

### Contact data invalid

-> contact data invalid
-> validation errors visible
-> anonymous user corrects contact data
-> back to entering follow-up contact data

### Request data invalid

-> request data invalid
-> validation errors visible
-> anonymous user corrects request data
-> back to entering request data

### Anonymous request accepted

-> request/contact data accepted
-> anonymous request/contact request is recorded
-> confirmation visible

## Invariants

Anonymous request must have valid reachable contact data for follow-up.

Attach to:

- contact data valid?;
- anonymous request accepted?;
- submit anonymous request transition;

Invalid anonymous request/contact data is not accepted.

Attach to:

- anonymous request accepted?;
- submit anonymous request transition;

## Outcomes

- Anonymous user can submit request/contact information.
- System has follow-up contact data.
- Invalid contact data does not create usable anonymous request.

## Open Questions

Q: Does anonymous request create full request, contact request, or draft?

Q: Is anonymous request part of L3/future scope or nearer-term extension?

## Diagram Notes

- Do not reuse signed-in Client Request Creation semantics blindly.
- Follow-up contact validation is required and should be explicit.
