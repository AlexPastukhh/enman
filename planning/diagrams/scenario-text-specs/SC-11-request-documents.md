# SC-11 — Request Documents

## Status
Corrected scenario specification draft.  
Doc version: v0.1.0

## Purpose
Client attaches or manages request-related documents.

## DATA
Request document DATA `SC-11-DATA-01`

Attachment DATA:
```text
- selected file/document.
```
Future: document type, format/size if user-visible, required/optional marker, after-feedback document requirement.

## Main Flow
Client selects document -> validation/errors if needed -> accepted document is attached and visible; rejected document is not attached and can be reselected.

## Invariants
Rejected/invalid document is not attached.
