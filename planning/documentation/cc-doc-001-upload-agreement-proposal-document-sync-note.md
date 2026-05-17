# CC-DOC-001 Upload Agreement Proposal Document Sync Note

Status: docs-only synchronization note  
Scope: registers and navigation for the agreement proposal document upload helper slice

## What changed

Added a cross-cutting/helper slice:

```text
planning/slices/cross-cutting/CC-DOC-001-upload-agreement-proposal-document.md
```

This slice owns the reusable upload/storage support flow that turns a multipart file upload into `AgreementDocumentRef`-compatible metadata.

## Why cross-cutting

The upload endpoint is consumed by multiple agreement exchange commands:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
```

It does not perform agreement lifecycle transitions and should not be modeled as a normal agreement command slice.

## Current direction

```text
POST /api/agreement-proposal-documents
multipart/form-data: document
auth: Client or Employee
CSRF: required
success: 200 OK AgreementDocumentRefDto
storage: local private App_Data/Documents/agreement-proposals
```

## Important boundary

```text
Upload first.
Receive AgreementDocumentRef-compatible metadata.
Submit start/counter-proposal command with the returned metadata.
```

Do not send binary bytes directly into agreement lifecycle command endpoints first pass.
