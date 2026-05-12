# SC-13A — My Agreements DATA

## DATA Blocks

### SC-13A-DATA-01 — Agreement list visible DATA

Type: Visible DATA  
Actor: Client  
Used by: My Agreements list

Visible DATA:

```text
- agreement proposal summary visible enough to identify the proposal;
- related Approved request;
- sender: employee or client;
- agreement proposal status.
```

Core statuses:

```text
- AwaitingClientConfirmation;
- SentByClient;
- Accepted.
```

Future statuses:

```text
[VAR:EXPAND]
- Signed;
- Rejected;
- Expired;
- Superseded;
- Cancelled.
```

### SC-13A-DATA-02 — Agreement filter DATA

Type: Filter DATA  
Actor: Client  
Used by: My Agreements filtering/searching

Filter DATA:

```text
- agreement proposal status;
- sender: employee or client.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- related Approved request;
- signed/unsigned state after electronic signature is introduced;
- date/period only if UX later needs time-based filtering.
```

Open questions:

```text
Q: Should My Agreements include only proposals or also final accepted/signed agreements after future signature flow?
Q: Does approval always create an agreement proposal, or does employee send it separately?
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-13A-my-agreements.md
```
