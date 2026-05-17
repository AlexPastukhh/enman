# L2 Employee Request Page Placement

Status: accepted placement rule

## Decision

Employee request-area pages are grouped under:

```text
pages/employee/requests/*
```

Target placement:

```text
pages/employee/requests/dashboard
pages/employee/requests/details
```

Do not use a separate page area for Employee dashboard:

```text
pages/employee/dashboard
```

## Reason

Employee dashboard is a request-list page, not a separate employee profile/home area.

Keeping dashboard and details in the same request-area namespace makes the L2 client structure line up with the routes:

```text
/employee/requests
/employee/requests/:requestId
```

## Ownership

```text
SL-EMP-REQ-001
  server/API read endpoint for Employee request list.

L2-EMP-DASH-001.client
  client dashboard/list page under pages/employee/requests/dashboard.

SL-EMP-REQ-002
  server/API read endpoint for Employee request details.

L2-EMP-DETAILS-001.client
  client details page under pages/employee/requests/details.
```
