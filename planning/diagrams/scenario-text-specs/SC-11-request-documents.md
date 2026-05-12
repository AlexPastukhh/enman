# SC-11 — Request Documents

## Status

Corrected scenario specification draft.

## Purpose

Client attaches or manages request-related documents.

## Actor / Screen

Actor: Client  
Screen: Request Documents section / page  
Goal: Attach required or supporting documents

## Entry Points

Entry A: Client adds documents during request creation.

Entry B [future]: Client adds documents after feedback asks for additional information.

## Preconditions

- Client is signed in.
- Request creation or document upload context is reachable.

## DETAIL

Document criteria  
`SC-11-DETAIL-01`

- document type;
- file selected;
- allowed format/size if user-visible;
- required/optional marker;
- [VAR:EXPAND]

## Main Flow

1. Client opens request document context.
2. Client selects document to attach.
3. Client-side document validation runs automatically when possible.
4. Client corrects/reselects document if validation fails.
5. Client submits/attaches document.
6. System checks whether document is accepted.
7. If accepted, document is attached to request.
8. Attached document is visible.

## Branches

### Document accepted

-> document accepted
-> document is attached to request
-> attached document is visible

### Document rejected

-> document rejected
-> upload/document error visible
-> document is not attached
-> client can select another document

### Future after-feedback document upload

-> review feedback asks for additional document
-> client reaches document upload from feedback/update context
-> document upload flow starts

## Invariants

Rejected/invalid document is not attached.

Attach to:

- document accepted?;
- attach document transition;

## Outcomes

- Client can attach documents during request creation.
- Accepted documents are visible as attached.
- Rejected documents are not attached.

## Open Questions

Q: Can documents be added after request is InReview, or only through future feedback/update flow?

Q: Is after-feedback document upload part of L2 or L3?

## Diagram Notes

- Replace vague wording like managed from with explicit entry points.
- Do not model storage provider, file system, buckets, DB tables, or binary mechanics.
