# SC-04 — Client Request Creation

## Status

Corrected scenario specification draft.

## Purpose

Client creates and submits a connection/request for review.

## Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

## Preconditions

- Client is signed in.
- Client can access request creation page.

## DETAIL

Request details  
`SC-04-DETAIL-01`

- request type;
- object/address/location;
- applicant reference/data;
- contact data if needed;
- required request fields;
- [VAR:EXPAND]

## Main Flow

1. Client opens request creation page.
2. Client enters request details.
3. Client-side validation runs automatically.
4. Client corrects request details if client-side validation fails.
5. Client submits request.
6. System checks whether request is accepted.
7. If accepted, request is created.
8. Request status becomes InReview.
9. Request appears in client's My Requests list.
10. Request appears in employee review queue.

## Branches

### Client-side validation errors

-> request form has basic validation issues
-> validation errors visible
-> client corrects request details
-> back to entering request details

### Request accepted

-> request accepted
-> request is created
-> status becomes InReview
-> request appears in My Requests
-> request appears in employee review queue

### Request rejected by validation/business rules

-> request not accepted
-> validation/business errors visible
-> client corrects request details
-> back to entering request details

## Invariants

Invalid request is not accepted.

Attach to:

- request accepted?;
- submit request transition;

## Step Postconditions

- Request is created after accepted request submission.
- Request status becomes InReview after request creation.

## Outcomes

- Client can create request.
- Client sees validation errors for invalid request data.
- Created request appears in My Requests and employee review queue.
- Initial status is InReview.

## ADR / Policy Candidates

ADR?: Client data verification happens only in request context.

ADR?: Standalone applicant data editing does not trigger verification.

ADR?: Employee cannot start verification without a request.

## Diagram Notes

- Use InReview, not Submitted.
- Entry B should be expressed as UX/business context, not as implementation link.
