# SC-16 — Removed Standalone Notification Scenario

## Status

Removed as standalone scenario.

## Decision

Do not keep `Reliable Notification Delivery` as a standalone scenario.

## Why

Notification/email/navigation behavior belongs to concrete scenarios:

```text
SC-05 My Requests / Own Request Details
SC-12 Review Feedback / Correction Navigation
SC-13 My Agreements / Agreement Details / Agreement Response
SC-07B Employee Request Review
```

Reliability/failure handling belongs later to:

```text
ADR candidates
ports/adapters map
changeability/extensibility map
testing map
```

## Replacement

Use UX/business wording inside related scenarios:

```text
Client opens processed/rejected request after review feedback.
Client continues from rejected request feedback.
Client opens agreement details after agreement option became available.
```

Do not use default flow wording like:

```text
email link
notification provider
notification delivery service
URL
route
```

unless it appears in a Q/ADR side block as an implementation decision.

## Open Questions

Q: Is review feedback shown through email, in-app details page, or both?

Q: After approval/agreement feedback, should UX guide client to Request Details, My Agreements, Agreement Details, or multiple destinations?

Q: Should notification reliability be an ADR candidate in later planning?
