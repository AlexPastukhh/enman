# SC-14 — Client Data Verification

## Status
Future / extension scenario specification draft.

## Purpose
Employee starts client data verification/check in request context so review can use result.

## Actor / Screen
Actor: Employee  
Screen/context: Employee Request Details / Employee Request Review

## Entry Points
Entry A [future]: Employee starts verification while viewing request details.  
Entry B [future]: Employee starts verification while reviewing request.

## DATA
Client verification visible DATA `SC-14-DATA-01`:
```text
- verification status;
- verification result: passed / failed / unavailable;
- verification issue/reason if failed.
```
Future: request DATA and applicant/client DATA used for verification.

## Main Flow
Employee starts verification -> mocked/current or future real check runs -> verification result becomes visible -> review can use result.

## Invariants
Verification only in request context. Standalone applicant data editing does not trigger verification. Employee cannot start verification without request.

## ADR
ADR?: Verification/check is mocked in current implementation.

## Diagram Notes
This is not triggered-by.
