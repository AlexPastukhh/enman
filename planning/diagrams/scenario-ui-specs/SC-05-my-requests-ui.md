# SC-05 — My Requests UI Scenario

Status: mostly complete / normalized UI scenario source  
Applies to: My Requests list, filters, details entry  
Actors: Client  
Related scenario: `SC-05 — My Requests / Own Request Details`  
Related slices: `SL-REQ-002-my-requests-list`, `SL-REQ-003-own-request-details`, related client sidecars when migrated

## 1. User Goal

The authenticated Client views their own requests, filters the list by supported criteria and opens one request details page.

## 2. Screen Entry Points

```text
Authenticated Client opens My Requests page
        ↓
Client UI loads own requests
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ own requests exist           │ no own requests              │
 ▼                              ▼
request list visible            regular empty state visible
```

## 3. Screen Composition

```text
My Requests page
  Page title / intro
  Filter area
  Request list
  Empty state area when needed
  Error/access state area when needed
```

## 4. Visible Data

Each list item should show enough information to identify the request:

```text
request status
request type
created date
summary
object address / address summary when available
```

## 5. Actions

```text
Select status filter
Reset filters
Open request details
```

Status is the first supported filter.

Supported first-cut statuses:

```text
InReview
Approved
Rejected
```

Future filters such as request type, date range or search are out of current scope until backend contract and scenario sources support them.

## 6. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|
| Requests exist | Request list | Open details, filter | Shows own requests only |
| No requests | Regular empty state | Create request if available | Text: `У вас пока нет заявок.` |
| Filter has matches | Filtered list | Reset filters, open details | URL/page state reflects selected filter |
| Filter no matches | Filtered empty state | Reset filters | Text: `Заявок с выбранным фильтром не найдено.` |
| Invalid filter URL | Safe invalid-filter or normalized state | Reset filters | UI must not crash |

## 7. Empty / Loading / Error States

Regular empty state:

```text
У вас пока нет заявок.
```

Filtered empty state:

```text
Заявок с выбранным фильтром не найдено.
```

Filtered empty state should offer a reset filters action.

Invalid filter URL example:

```text
/requests?status=Done
```

Accepted first-cut behavior:

```text
- show a safe invalid-filter state with reset action;
- or normalize to unfiltered state without crashing.
```

The final choice belongs to the related client slice draft.

## 8. Actor-Specific Differences

Client-only screen first pass.

## 9. Feedback / Validation Requirements

```text
invalid filter state must be visible or safely normalized
filter reset action must be understandable
```

## 10. Accessibility Notes

```text
request details entry is a link/action with accessible name
filter controls have labels
empty/error states are text-visible
```

## 11. Out of Scope

```text
React component placement
query key mechanics
shared API wrapper implementation
backend filter implementation
request details page internals
request creation UI
employee review UI
```

## 12. Related Client Slice Drafts

```text
my requests list client draft when migrated
own request details client draft when migrated
```
