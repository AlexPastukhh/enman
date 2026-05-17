# Client API Placement Architecture Sync

Status: docs-only replacement package

This package synchronizes the client architecture decision from the uploaded discussion:

```text
shared/
  only truly shared infrastructure:
  fetchJson, ProblemDetails/ApiError, antiforgery/CSRF helpers,
  generated OpenAPI types, generic transport/build-url helpers.

entities/*/
  read-side ownership:
  read endpoint wrappers, read DTO aliases/mapping, query keys,
  read hooks, read model types, display UI.

features/*/
  command/user-action ownership:
  mutation endpoint wrappers, command DTO/result aliases/mapping,
  mutation hooks, forms/actions, success/error behavior, invalidation.
```

Files included:

```text
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
planning/client/README.md
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/client-architecture-principles.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/client-api-placement-sidecar-sync-note.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/l2/README.md
planning/slices/l2/L2-client-api-placement-sync-note.md
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
```

Compatibility rule:

```text
Existing business-specific wrappers under shared/api are transitional.
Do not mass-migrate them without concrete slice scope.
New drafts and new implementation must use the new placement rule.
```
