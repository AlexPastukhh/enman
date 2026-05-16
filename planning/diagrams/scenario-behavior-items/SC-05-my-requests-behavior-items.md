# SC-05 — My Requests Behavior Items

Status: current behavior item source

## List behavior

### SC-05-BI-001 — Client can view own requests list

The authenticated client can open My Requests and see the list of requests belonging to the current account.

### SC-05-BI-002 — Empty own requests list is a normal state

If the authenticated client has no requests, the UI can show an empty state based on an empty successful response.

### SC-05-BI-003 — Request summary identifies a request

Each list item shows enough information to identify the request.

### SC-05-BI-004 — Request status is visible in list

Each list item shows request status: InReview, Approved or Rejected.

## Filter behavior

### SC-05-BI-005 — Client can filter My Requests by status

The client can filter the list by supported status values.

### SC-05-BI-006 — Status is the first filter in an extensible filter model

The status filter is not a one-off control. It is the first supported filter in a list filter architecture that can later add request type, date range or search.

### SC-05-BI-007 — Client can reset filters

The client can return to an unfiltered My Requests list.

### SC-05-BI-008 — Invalid status URL value is safe

If a URL contains an unsupported status value, the page does not crash and does not silently show misleading filtered data.

## Details behavior

### SC-05-BI-009 — Client can open own request details

The authenticated client can open details for a selected request.

### SC-05-BI-010 — Client sees only own request details

The client sees details only if the request belongs to the authenticated account.

### SC-05-BI-011 — Missing or not-owned request shows not-found state

If the request does not exist or belongs to another account, the UI shows a not-found state instead of request data.

### SC-05-BI-012 — Client sees submitted request data

The details page shows submitted request details and submitted object address.

### SC-05-BI-013 — Client sees request metadata

The details page shows request status, request type and created date.

### SC-05-BI-014 — In-review request has no fake review result

If there is no review result yet, the details page shows submitted data and current status without fake feedback.

### SC-05-BI-015 — Approved request shows approval result

If the request is approved, the details page shows the approval decision and decision date.

### SC-05-BI-016 — Rejected request shows rejection reason

If the request is rejected, the details page shows the rejection decision, decision date and rejection reason.

### SC-05-BI-017 — Client can return to My Requests

The details page and not-found state provide navigation back to My Requests.
