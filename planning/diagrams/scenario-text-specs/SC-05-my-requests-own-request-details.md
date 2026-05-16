# SC-05 — My Requests / Own Request Details

Status: current scenario specification draft  
Scope: client views own requests list, filters own requests and opens own request details

## 1. Purpose

Client needs a read context for submitted requests.

SC-05 contains three related but separable user intents:

```text
1. Client opens My Requests and sees own requests list.
2. Client filters the list by supported criteria.
3. Client selects one request and opens own request details.
```

The implementation should keep these as separate slices/sidecars when they grow.

## 2. Actor / Screens

Actor: signed-in Client

Screens:

```text
My Requests list
My Requests list filters
Own Request Details
```

## 3. Preconditions

```text
- Client is signed in.
- Client may have zero or more requests.
- Server owns account scoping.
```

## 4. DATA

My Requests list visible DATA `SC-05-DATA-01`:

```text
- request summary visible enough to identify the request;
- request status: InReview / Approved / Rejected.
```

Request filter DATA `SC-05-DATA-02`:

```text
- status.
```

`status` is the first filter entry in a broader filter architecture. Future filter entries may include request type, text/search and date/period only if UX and backend support are added.

Own Request Details visible DATA `SC-05-DATA-03`:

```text
- request status;
- request type;
- created date;
- submitted request data visible to client;
- submitted object address;
- status-specific review result/feedback when available.
```

Status-specific visible DATA:

```text
InReview: under-review state and no fake review result.
Approved: approval result/message if available; agreement-related status/action only when future slices implement it.
Rejected: rejection explanation/details; feedback context for creating a new request.
```

## 5. Main Flow — List

```text
Signed-in Client opens My Requests page
        ↓
System loads requests for current authenticated account
        ↓
Client sees own requests list or empty state
        ↓
Each request item shows summary and status
```

## 6. Main Flow — Filters

```text
Client opens My Requests page
        ↓
System/page initializes filter state from URL query params
        ↓
Client chooses supported filter
        ↓
Client list query reloads with filter object
        ↓
Client sees filtered list or empty filtered state
```

Current supported filter:

```text
status
```

Future filters must extend the filter model without moving URL ownership out of the page.

## 7. Main Flow — Details

```text
Client selects a request from My Requests list
        ↓
Own Request Details page opens
        ↓
System loads request details for current account
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ request found                │ request missing / not own    │
 ▼                              ▼
Client sees submitted            Client sees not-found state
request data and review state    and link back to My Requests
```

## 8. Invariants

```text
Client can view only own requests.
Client cannot open another client's request details.
Client does not send accountId for list or details.
Backend ownership/scoping is authoritative.
```

## 9. Open Questions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `SC-05-Q-001` | open | What exact request summary is enough to identify a request? | Current summary uses request type/status/created date/details summary/object address. Refine when UI needs it. |
| `SC-05-Q-002` | future review | Where is “create new request based on feedback” offered? | Details page may show it after rejected feedback; implementation belongs to future request/details client work. |
| `SC-05-Q-003` | future review | Which filters follow status? | Request type/date/search only after backend and UI support are planned. |

## 10. Diagram Notes

SC-12 is merged into this scenario plus SC-04.
