# MANIFEST — Employee Dashboard + Start Review Full Drafts

Archive: `employee-dashboard-start-review-full-drafts-sync.zip`  
Scope: docs-only full slice draft package from the two uploaded draft inputs  
Repo: `AlexPastukhh/enman`  
Branch target: `my-changes`

## Add

| File | Why |
|---|---|
| `planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md` | Converts uploaded Employee Dashboard client short draft into a full client read sidecar. |
| `planning/slices/SL-EMP-REQ-003-start-request-review.md` | Adds uploaded Start Request Review full backend/API command slice draft as repo planning doc. |

## Replace

| File | Why |
|---|---|
| `planning/slices/l2/README.md` | Adds dashboard client sidecar and StartReview backend command draft to L2 navigation. |
| `planning/slices/README.md` | Adds new L2 drafts to central slice navigation. |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Adds explicit mappings for L2 dashboard client and StartReview command draft. |

## Delete

None.

## Source Inputs

The package is based on the current uploaded drafts:

```text
- Employee dashboard client sidecar short draft.
- SL-EMP-REQ-003 Start Request Review full backend/API command draft.
```

Scenario files remain the source of truth for Scenario Flow and Behavior Coverage.

`planning/tables/domain-drafts/domain-draft-02.md` remains domain-design input for aggregate boundaries, naming, invariants and target code-sketch direction.

## Not Included

```text
- no runtime code;
- no tests;
- no generated artifacts;
- no branch/commit/PR;
- no generated OpenAPI/types edits.
```
