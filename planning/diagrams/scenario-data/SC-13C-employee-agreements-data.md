# SC-13C — Employee Agreements DATA

Doc version: v0.1.0
## DATA Blocks

### SC-13C-DATA-01 — Employee agreement list visible DATA

Type: Visible DATA  
Actor: Employee  
Used by: Employee Agreements list

Visible DATA:

```text
- agreement proposal summary visible enough to identify the proposal;
- related Approved request;
- client/applicant summary;
- sender: employee or client;
- agreement proposal status.
```

Core statuses:

```text
- AwaitingClientConfirmation;
- SentByClient;
- Accepted;
- Rejected.
```

Future statuses:

```text
[VAR:EXPAND]
- Signed;
- Expired;
- Superseded;
- Cancelled.
```

### SC-13C-DATA-02 — Employee agreement filter DATA

Type: Filter DATA  
Actor: Employee  
Used by: Employee Agreements filtering/searching

Filter DATA:

```text
- agreement proposal status;
- sender: employee or client.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- client/applicant search;
- related Approved request;
- assigned employee if assignment is introduced;
- signed/unsigned state after electronic signature is introduced;
- date/period only if UX later needs time-based filtering.
```

Notes:

```text
Employee Agreements mirrors My Agreements, but from employee side.
Employee can see client-sent proposals and send a new employee version from SC-13D.
Date/period filtering is future only.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-13C-employee-agreements.md
```
