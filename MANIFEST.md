# MANIFEST — Employee Request Details Client Full Draft Sync

Archive: `employee-details-client-full-draft-sync.zip`  
Scope: documentation-only planning update  
Purpose: add full L2 Employee Request Details client sidecar derived from the uploaded short draft.

## Add / Replace

| File | Why |
|---|---|
| `planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md` | Adds full client read sidecar for Employee Request Details. |
| `planning/slices/l2/README.md` | Adds the new sidecar to L2 navigation. |
| `planning/slices/README.md` | Adds the new L2 client sidecar to global slice navigation. |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Adds source mapping for the details client sidecar. |
| `planning/slices/slice-implementation-notes-register.md` | Adds implementation notes for details read client contract/blockers and future command sidecars. |

## Source Input

The full sidecar is derived from the uploaded Employee Request Details client sidecar short draft.

Important source decisions preserved:

```text
- Details read sidecar waits for SL-EMP-REQ-002 server details endpoint and generated DTO.
- StartReview contract is known as future command sidecar input, but StartReviewResponseDto is not the details DTO.
- Details client sidecar is read-only and maps to pages + entities.
- Start/approve/reject command actions stay in future features.
- Details read uses safe GET; antiforgery belongs to future unsafe command sidecars.
- Use Employee, not Worker.
```

## Not Included

```text
- no runtime code;
- no tests;
- no generated OpenAPI artifacts;
- no generated TypeScript;
- no GitHub branch/commit/PR.
```
