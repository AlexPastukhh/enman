# SC-05 — My Requests DATA

Status: current DATA source / list-filter-details split
Doc version: v0.1.0  

## SC-05-DATA-01 — My Requests list visible DATA

```text
- request summary visible enough to identify the request;
- request status: InReview / Approved / Rejected.
```

Current list summary may include:

```text
- request id;
- request type;
- status;
- created date;
- summary;
- object address / address summary.
```

## SC-05-DATA-02 — Request filter DATA

Current supported filter:

```text
- status.
```

Status is the first entry in an extensible My Requests filter model.

Future filters are not current DATA until explicitly added:

```text
- request type;
- text/search;
- date/period.
```

## SC-05-DATA-03 — Own Request Details visible DATA

```text
- request status;
- request type;
- created date;
- submitted request data visible to the client;
- object address;
- status-specific review result/feedback.
```

Status-specific details:

```text
InReview:
- under-review state;
- no fake review result.

Approved:
- approval result/message if available;
- decision date when available;
- agreement-related status/action only when future slices add it.

Rejected:
- rejection explanation/details;
- rejection decision date when available;
- original submitted request content remains visible;
- feedback context for creating a new request.
```
