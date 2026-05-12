# SC-12 — Review Feedback / Correction Navigation

## Status

Corrected scenario specification draft. Replaces old Clarification as L1 concept.

## Purpose

Client receives/reviews rejection feedback and can start a new request based on that feedback.

## Actor / Screen

Actor: Client  
Screen: Rejected Request Details / Request Creation  
Goal: Understand rejection feedback and continue with corrected request flow

## Entry Points

Entry A: Client continues from rejected request feedback.

Entry B: Client opens own Rejected request details from My Requests — SC-05.

## Preconditions

- Client is signed in.
- Client has an own request with status Rejected.
- Rejection explanation/details are available.

## DETAIL

Rejection feedback details  
`SC-12-DETAIL-01`

- rejection explanation;
- missing/incorrect information if provided;
- suggested next action if provided;
- related rejected request reference;
- [VAR:EXPAND]

## Main Flow

1. Client opens rejected request feedback context.
2. Rejection explanation/details are visible.
3. Client reviews feedback.
4. Client can start creating a new request based on feedback.
5. Request Creation — SC-04 opens with feedback context available where appropriate.

## Branches

### Client starts new request based on feedback

-> client chooses to continue from rejected request feedback
-> request creation opens
-> client can create a new request using feedback details

### Client does not continue

-> client only reviews feedback
-> rejected request remains visible
-> no new request is created

### Future update existing request

-> future behavior: client updates already submitted request data
-> Update Submitted Request flow opens instead of creating new request

## Invariants

Client can view feedback only for own rejected request.

Attach to:

- rejected request feedback opens;
- rejected request details loaded;

## Outcomes

- Client sees why request was rejected.
- Client can start a new request based on feedback.
- Future update flow is identified but not L1.

## Open Questions

Q: Where is create new request based on feedback offered: rejected Request Details, review feedback notification, both, or future update existing request flow?

Q: In future, should rejected feedback lead to updating existing submitted request instead of creating a new request?

## Diagram Notes

- Do not use Clarification as L1 live update submitted request.
- Clarification/update loop is future behavior.
- Avoid default wording like email link; use rejected feedback UX path.
