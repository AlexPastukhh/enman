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
- text details/comment sent with proposal;
- attached agreement document/file;
- available client action for current status.
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

### SC-13B-DATA-02 — Client agreement proposal submission DATA

Type: Attachment DATA / Input DATA  
Actor: Client  
Used by: send own agreement version branch

Attachment DATA:

```text
- client-selected agreement document/version to send back.
```

Input DATA:

```text
- text details/comment sent with client proposal.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- comment-only message without sending a new proposal;
- electronic signature status;
- signed agreement document;
- signature timestamp;
- final agreement number.
```

Notes:

```text
Accept/confirm is scenario branch/action, not DATA subtype.
Client can send only one own proposal version in response to an employee-sent proposal in core.
Client cannot start agreement proposal exchange without an employee-sent proposal.
The key agreement proposal DATA for flow is sender + status + attached document + text details/comment.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-13B-agreement-proposal-details-response.md
```
