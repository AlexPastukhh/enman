# SC-05 — My Requests UI Spec

Status: current UI scenario source / list-filter-details split  
Scenario: `SC-05 — My Requests / Own Request Details`  
Scope: visible UI behavior for My Requests list, filters, empty states and details entry.

## 1. Purpose

The My Requests UI lets an authenticated client view their own requests, filter the list by supported criteria and open one request details page.

This file describes visible outcomes and accepted UI behavior. It does not define React component placement, query keys or HTTP wrapper implementation.

## 2. List UI

When the authenticated client opens My Requests:

```text
[Authenticated Client]
opens My Requests page
        ↓
[Client UI]
loads own requests
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ own requests exist           │ no own requests              │
 ▼                              ▼
request list visible            regular empty state visible
```

Each list item should show enough information to identify the request:

```text
- request status;
- request type;
- created date;
- summary;
- object address / address summary when available.
```

## 3. Filter UI

Status is the first supported filter.

Visible filter behavior:

```text
Client sees status filter controls
        ↓
Client selects a supported status
        ↓
List updates to show matching own requests
        ↓
URL/page state reflects selected filter
        ↓
Client can reset filters
        ↓
List returns to unfiltered state
```

Supported first-cut statuses:

```text
InReview
Approved
Rejected
```

Future filters such as request type, date range or search are out of current scope until backend contract and scenario sources support them.

## 4. Filtered Empty State

Regular empty state:

```text
У вас пока нет заявок.
```

Filtered empty state:

```text
Заявок с выбранным фильтром не найдено.
```

Filtered empty state should offer a reset filters action.

## 5. Invalid Filter URL State

If the page is opened with an unsupported status value, the UI must not crash or show a misleading selected filter.

Example:

```text
/requests?status=Done
```

Accepted first-cut behavior:

```text
- show a safe invalid-filter state with reset action;
- or normalize to unfiltered state without crashing.
```

The final choice belongs to `L1-MY-REQUESTS-LIST-FILTERS.client.md`.

## 6. Details Entry

List items may provide an action/link to open the request details page:

```text
My Requests list item
        ↓
Client selects/open details
        ↓
/requests/:requestId
```

Details rendering is owned by:

```text
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

## 7. Out Of Scope

```text
- React component placement;
- query key mechanics;
- shared API wrapper implementation;
- backend filter implementation;
- request details page internals;
- request creation UI;
- employee review UI.
```
