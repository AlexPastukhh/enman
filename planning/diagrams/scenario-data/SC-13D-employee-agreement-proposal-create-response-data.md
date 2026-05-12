# SC-13D — Employee Agreement Proposal Create / Send Version DATA

## DATA Blocks

### SC-13D-DATA-01 — Employee agreement proposal creation DATA

Type: Reference DATA / Visible DATA / Attachment DATA / Input DATA  
Actor: Employee  
Used by: Agreement Proposal Create / Employee Agreement Proposal Details

Reference / Visible DATA:

```text
- related Approved request;
- client/applicant summary;
- previous agreement proposal, if responding to client-sent proposal.
```

Attachment DATA:

```text
- employee-selected agreement document/version to send.
```

Input DATA:

```text
- text details/comment sent with employee proposal.
```

Visible DATA after submit:

```text
- sender = employee;
- status = AwaitingClientConfirmation;
- attached agreement document/file;
- text details/comment.
```

### SC-13D-DATA-02 — Previous client proposal visible DATA

Type: Visible DATA  
Actor: Employee  
Used by: employee sends new version in response to client-sent proposal

Visible DATA:

```text
- previous proposal sender = client;
- previous proposal status = SentByClient;
- previous attached agreement document/file;
- previous text details/comment.
```

Visible DATA after employee sends new version:

```text
- previous client-sent proposal status = Rejected;
- new employee proposal status = AwaitingClientConfirmation.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- comment-only message without sending a new proposal;
- return-to-old-version action context;
- electronic signature status;
- signed agreement document;
- signature timestamp;
- final agreement number.
```

Notes:

```text
Agreement proposal exchange starts only by employee action on an Approved request.
Employee and client proposal submissions both include file/document and text details/comment.
When employee sends a new version in response to a client-sent proposal, the previous client-sent proposal becomes Rejected.
```

Scenario spec references:

```text
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md
```
