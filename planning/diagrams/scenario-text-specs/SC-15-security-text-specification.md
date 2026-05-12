# SC-15 — Security Text Specification

## Status

Text specification only. Not a full scenario diagram page unless explicitly requested later.

## Purpose

Capture security policies that affect scenarios but should not become a large user-facing scenario diagram.

## Scope

Security policies are documented in Markdown companion specs and referenced as local invariants/preconditions where they affect concrete scenarios.

Auth is not a separate scenario here. Auth remains part of:

```text
- preconditions;
- local invariants;
- framework responsibility;
- scenario-specific access rules.
```

## Security Topics

### Login / account protection

- login throttling;
- rate limiting;
- account lock policy, if planned;
- invalid login error behavior;
- no excessive information in error messages.

### Password recovery protection

- no account enumeration;
- recovery token expiration;
- recovery token single-use;
- password policy.

### Client data access

- client can view only own requests;
- client can view only own agreements;
- client cannot open another client's request/agreement details.

### Employee access

- employee dashboard requires employee permissions;
- employee request details require employee permissions;
- employee review requires employee permissions;
- only InReview requests can enter review.

## Related Scenarios

```text
SC-02 Login
SC-03A Password Recovery Request
SC-03B Set New Password
SC-05 My Requests / Own Request Details
SC-07A Employee Request Details
SC-07B Employee Request Review
SC-13 My Agreements / Agreement Details / Agreement Response
```

## Diagram Notes

Do not draw this as a large framework/auth diagram unless explicitly requested.  
Use local invariants on actual scenarios instead.
