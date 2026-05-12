# SC-09 — Rejected Result

## Status

Merged / removed as standalone scenario.

## Decision

There will be no separate client scenario named `Rejected Result`.

## Why

Rejected is a request status and a branch/state shown inside own Request Details — SC-05.

Rejection decision is produced by Employee Request Review — SC-07B.

Rejected feedback / correction navigation belongs to SC-12.

## Mapping

Old SC-09 behavior maps to:

```text
SC-07B Employee Request Review
- employee rejects request;
- request status becomes Rejected;
- rejection explanation is recorded.

SC-05 My Requests / Own Request Details
- client views own Rejected request details.

SC-12 Review Feedback / Correction Navigation
- client continues from rejected request feedback;
- client can create a new request based on feedback.
```

## Consistency Report Note

Mark old SC-09 as merged into:

```text
SC-07B + SC-05 + SC-12
```
