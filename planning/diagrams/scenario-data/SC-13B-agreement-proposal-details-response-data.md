# SC-13B — Agreement Proposal Details / Response DATA

## DATA Blocks

### SC-13B-DATA-01 — Agreement proposal details visible DATA

Type: Visible DATA  
Actor: Client  
Used by: Agreement Proposal Details page

Visible DATA:

```text
- related Approved request;
- agreement proposal summary/name;
- sender: employee or client;
- agreement proposal status;
- attached agreement document/file;
- available client action for current status.
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

### SC-13B-DATA-02 — Client agreement attachment DATA

Type: Attachment DATA  
Actor: Client  
Used by: send own agreement version/counterproposal branch

Attachment DATA:

```text
- client-selected agreement document/version to send back.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- optional comment/message;
- electronic signature status;
- signed agreement document;
- signature timestamp;
- final agreement number.
```

Notes:

```text
Accept/confirm is scenario branch/action, not DATA subtype.
The key agreement proposal DATA for flow is sender + status + attached document.
```

Open questions:

```text
Q: Is accept/confirm legally meaningful acceptance or only confirmation of the current proposal?
Q: Can client send multiple versions, or only one response per awaiting proposal?
Q: Does employee see and accept/reject client-sent proposal in a future employee agreement scenario?
Q: Should final accepted proposal become a separate final Agreement entity later?
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-13B-agreement-proposal-details-response.md
```
