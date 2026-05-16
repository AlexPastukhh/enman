# Clean Requirements

Status: draft  
Scope: short requirement summary for VKR chapter 2

This file is a summary. Detailed behavior is described in:

```text
vkr-clean/functional-specification.md
```

## 1. Functional Areas

The system is designed around:

```text
client account
applicant party
connection request
employee review
document/agreement preparation
notification and further document workflow
```

## 2. Client Capabilities

```text
- registration of a client account;
- login to the system;
- current user/session loading;
- logout;
- creation of an individual applicant party;
- creation of a connection request;
- later viewing of own requests, statuses and details;
- later viewing of related documents and notifications.
```

The final diploma text should describe only the implemented screens as implemented and leave remaining screens as designed or planned until repo evidence confirms them.

## 3. Employee Capabilities

```text
- viewing incoming client requests;
- opening request details;
- checking request and applicant data;
- approving a request;
- rejecting a request with feedback when needed;
- preparing an agreement/document after approval;
- triggering or observing client notifications.
```

Important design rule:

```text
approval of a request and creation of an agreement/document should remain separate actions unless the final implementation intentionally couples them.
```

## 4. Non-Functional Requirements

```text
- web access through browser;
- client-server architecture;
- separation of frontend and backend responsibilities;
- typed API communication between frontend and backend;
- stable handling of API errors;
- storage in relational database;
- role-based access direction for client and employee operations;
- protection of client data from unauthorized access;
- maintainability through layered architecture;
- testability through separated domain, API, client and E2E testing layers.
```

## 5. Data And Behavior Constraints

```text
- a request must be connected with an applicant party;
- applicant data should not be accidentally mutated by editing request-local form data;
- a client should not access another client's requests;
- an already processed request should not be processed again;
- rejection feedback is a behavior/policy point and should be clarified by the final business rule;
- document/agreement preparation starts from an approved request context;
- notification should not be sent before the relevant server-side operation is successfully saved.
```
