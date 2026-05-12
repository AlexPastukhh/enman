# SC-14 — Client Data Verification

## Status

Corrected scenario specification draft.

## Purpose

System checks client-provided data in request context so employee review can use the result.

## Actor / Screen

Actor: System / Request processing context  
Screen: Client data verification during request processing  
Goal: Make client data verification result available for review

## Triggered By

Request processing requires client data check. This is a rare triggered-by case because it is not actor-started UX navigation.

## Preconditions

- A request exists.
- Verification/check is required in request context.
- Standalone applicant data editing does not trigger verification.

## DETAIL

Verification input details  
`SC-14-DETAIL-01`

- request data needed for verification;
- applicant/client data needed for verification;
- verification result categories if user-visible/planning-relevant;
- [VAR:EXPAND]

## Main Flow

1. Request processing requires client data verification.
2. Verification/check is requested as part of request processing.
3. Verification result becomes available.
4. Employee review can use verification result.

## Branches

### Verification passed

-> verification result = passed
-> employee review can treat data as verified

### Verification failed

-> verification result = failed
-> verification issue is visible/recorded for review
-> employee review can consider issue

### Verification unavailable

-> verification result unavailable
-> review must handle unavailable result according to policy
-> open question / ADR candidate if behavior matters

## Invariants

Verification happens only in request context.

Attach to:

- triggered-by node;
- verification request step;

Standalone applicant data editing does not trigger verification.

Attach to:

- triggered-by node;
- verification request step;

Employee cannot start verification without a request.

Attach to:

- triggered-by node;
- verification request step;

## Outcomes

- Verification result is available for employee review.
- Verification is not started from standalone applicant data editing.
- Verification is not arbitrarily started without request.

## ADR / Policy Candidates

ADR?: Verification/check is mocked in current implementation.

ADR?: Employee cannot start verification without request.

ADR?: Behavior when verification is unavailable needs decision if it affects review.

## Diagram Notes

- Do not name this scenario Mock Verification.
- Use Client Data Verification or Client Provided Data Check.
- Mocking belongs to ADR/implementation note, not scenario name.
