# SC-05 — My Requests DATA

Status: current DATA source

## SC-05-DATA-01 — My Requests list visible DATA

```text
- request summary visible enough to identify the request;
- request status: InReview / Approved / Rejected.
```

Current summary direction:

```text
requestId
requestType
status
createdAt
summary
objectAddress
```

## SC-05-DATA-02 — Request filter DATA

Current first filter:

```text
status
```

Allowed status values:

```text
InReview
Approved
Rejected
```

Future filter candidates:

```text
requestType
createdFrom
createdTo
search
```

These are future only until backend and UI support are planned.

## SC-05-DATA-03 — Own Request Details visible DATA

```text
- request id for route/read identity;
- request status;
- request type;
- created date;
- submitted request details;
- submitted object address;
- status-specific review result/feedback.
```

Status-specific:

```text
InReview:
- under-review state;
- reviewResult can be null.

Approved:
- approved decision;
- decision date.

Rejected:
- rejected decision;
- decision date;
- rejection reason/feedback;
- original submitted request content stays visible.
```
