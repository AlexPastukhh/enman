# SC-05 — My Requests UI Spec

Status: current `[UI-SCENARIO]` source

## 1. My Requests List UI

The authenticated client can open My Requests and see either:

```text
- loading state;
- own requests list;
- empty state;
- error state.
```

Each visible request summary should make the request identifiable and show current status.

## 2. Filter UI

The list has a filter area.

Current first supported filter:

```text
status
```

Status filter UI supports:

```text
All / no status filter
InReview
Approved
Rejected
Reset filters
```

Invalid URL status values should not crash the page. The UI should either ignore the invalid value or normalize it to an unfiltered state and keep the behavior understandable.

Future filters may be added later without changing the page ownership rule.

## 3. Own Request Details UI

When the client opens a request details page, the page shows:

```text
- request status;
- request type;
- created date;
- submitted request details;
- submitted object address;
- review result when present.
```

If `reviewResult = null`, the UI shows submitted request data and current status without fake feedback.

If approved, the UI shows approved decision and decision date.

If rejected, the UI shows rejected decision, decision date and rejection reason.

If the request is missing or not owned by the client, the UI shows a not-found state and a link back to My Requests.

## 4. UI Ownership Rules

```text
Page owns URL query params.
Page owns route params.
Filter feature owns controls.
Details feature owns details rendering.
Shared API does not own filter state or route state.
```
