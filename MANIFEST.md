# MANIFEST — L2 Review Full Drafts Sync

Package: `l2-review-full-drafts-sync.zip`

## Purpose

Adds full slice drafts from the uploaded materials and synchronizes L2 navigation/registers.

## Added / replaced docs

```text
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-EMP-REQ-005-reject-request-review.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/README.md
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
```

## Normalization applied

The uploaded StartReview client draft was converted to the current command contract:

```text
POST /api/employee/requests/{requestId}/review/start
success -> 204 No Content
no StartReview response DTO
client refreshes list/details reads after success
```

## Not included

```text
runtime source code
tests
generated OpenAPI/types artifacts
```
