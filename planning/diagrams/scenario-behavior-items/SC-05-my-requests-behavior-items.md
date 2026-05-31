# SC-05 — My Requests Behavior Items

Status: current behavior item source / list-filter-details split  
Doc version: v0.1.0  
Scenario: `SC-05 — My Requests / Own Request Details`

## 1. List Behavior Items

### SC-05-BI-001 — Client can view own requests list

The authenticated client can open My Requests and see requests belonging to the current account.

### SC-05-BI-002 — Empty own requests list is a normal state

If the authenticated client has no requests, the page can show an empty state instead of an error.

### SC-05-BI-003 — Request summary is visible

Each list item shows enough visible summary data to identify the request, including status.

### SC-05-BI-004 — Client can open request details

The client can select a request from My Requests and open details for that request.

## 2. Filter Behavior Items

### SC-05-BI-005 — Client can filter My Requests by status

The authenticated client can filter the My Requests list by a supported request status.

### SC-05-BI-006 — Status is the first filter in an extensible filter model

Status is the first implemented filter entry. Future filters may add request type, date range or search when source behavior and backend support exist.

### SC-05-BI-007 — Client can reset filters

The client can clear selected filters and return to the unfiltered My Requests list.

### SC-05-BI-008 — Invalid status URL value is handled safely

Unsupported status values in the URL do not crash the page and do not produce misleading filter state.

## 3. Details Behavior Items

### SC-05-BI-009 — Client can view own request details

The authenticated client can open details for a request that belongs to the current account.

### SC-05-BI-010 — Missing or not-owned request shows not-found state

If the request does not exist or belongs to another account, the details page shows a not-found state instead of request data.

### SC-05-BI-011 — Submitted request data is visible in details

The details page shows the submitted request details and object address.

### SC-05-BI-012 — Request metadata is visible in details

The details page shows status, request type and created date.

### SC-05-BI-013 — In-review request has no fake review result

If the request has no review result yet, the page shows submitted data and current status without inventing feedback.

### SC-05-BI-014 — Approved request shows approval result when available

If the request is approved and review result exists, the page shows the approved decision and decision date.

### SC-05-BI-015 — Rejected request shows rejection reason

If the request is rejected, the page shows rejection decision, decision date and rejection reason/feedback.

### SC-05-BI-016 — Client can return from details to My Requests

The details page provides navigation back to My Requests.

## 4. Source Boundary

These behavior items describe user/system-visible behavior.

Do not add implementation mechanics here:

```text
- React Query key names;
- route helper names;
- API wrapper function names;
- cache invalidation mechanics;
- repository joins.
```

Those belong to slice sidecars and implementation notes.
