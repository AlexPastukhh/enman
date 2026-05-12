# SC-08 — Approved Result

## Status

Merged / removed as standalone scenario.

## Decision

There will be no separate client scenario named `Approved Result`.

## Why

Approved is a request status and a branch/state shown inside own Request Details — SC-05.

Approval decision is produced by Employee Request Review — SC-07B.

Agreement-related behavior after approval belongs to My Agreements / Agreement Details / Agreement Response — SC-13.

## Mapping

Old SC-08 behavior maps to:

```text
SC-07B Employee Request Review
- employee approves request;
- request status becomes Approved;
- agreement option/draft can be sent to client.

SC-05 My Requests / Own Request Details
- client views own Approved request details.

SC-13 My Agreements / Agreement Details / Agreement Response
- client reviews agreement option/draft after approval.
```

## Consistency Report Note

Mark old SC-08 as merged into:

```text
SC-07B + SC-05 + SC-13
```
