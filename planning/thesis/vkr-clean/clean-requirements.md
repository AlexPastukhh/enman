# Clean Requirements

Status: draft / repo-inspection sync  
Scope: short requirement summary for VKR chapter 2

This file is a summary. Detailed behavior is described in:

```text
planning/thesis/vkr-clean/functional-specification.md
```

## 1. Functional Areas

The system is designed around:

```text
client account
applicant party
connection request
employee review
agreement/document exchange
notification and further document workflow
```

## 2. Client Capabilities

Repo-observed / implemented client capabilities:

```text
- registration of a client account;
- login to the system;
- current user/session loading;
- logout on server side;
- creation of an individual applicant party;
- reading account applicant parties;
- selecting current/default applicant party;
- reading current individual applicant party;
- creation of a connection request;
- viewing own requests, statuses and details;
- viewing related agreement exchanges and agreement exchange details.
```

Planned/future client capabilities:

```text
- receiving real email notifications;
- viewing persisted notification history;
- participating in legally significant electronic signing, if added later.
```

## 3. Employee Capabilities

Repo-observed / implemented employee capabilities:

```text
- employee sign-in flow for the employee role;
- viewing incoming client requests in dashboard form;
- filtering request dashboard by status/review state;
- opening request details;
- checking request and applicant data;
- starting request review;
- approving a request;
- rejecting a request with feedback;
- starting an agreement/document exchange after approval;
- viewing employee-side agreement exchanges;
- sending agreement proposal versions;
- final refusal of agreement exchange when needed.
```

Important design rule:

```text
approval of a request and start of agreement/document exchange should remain separate actions unless the final implementation intentionally couples them.
```

## 4. Agreement / Document Exchange Capabilities

Repo-observed / implemented agreement exchange capabilities:

```text
- start exchange from an approved request;
- store active proposal version;
- store document reference and proposal comment;
- show proposal history;
- client accepts active employee proposal;
- client sends own counter-proposal version;
- employee sends a new proposal version;
- employee finally refuses exchange;
- list and view exchange details for client and employee contexts.
```

Diploma-safe formulation:

```text
Требования договорного этапа в текущем программном срезе реализованы как обмен версиями проектного документа между сотрудником и клиентом. Это не следует описывать как полноценное юридическое подписание договора.
```

## 5. Non-Functional Requirements

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
- testability through separated domain, API, client and E2E testing layers;
- repeatable generation/checking of API contract artifacts;
- repeatable UI screenshots for report/presentation when needed.
```

## 6. Data And Behavior Constraints

```text
- a request must be connected with an applicant party;
- a request must be connected with the client account that created it;
- applicant data should not be accidentally mutated by editing request-local form data;
- a client should not access another client's requests;
- employee actions must require employee role/identity;
- a request review should be started before approval/rejection;
- an already processed request should not be processed again;
- rejection feedback is stored when request is rejected;
- agreement/document exchange starts from an approved request context;
- agreement exchange must preserve active proposal version and proposal history;
- notification should not be sent before the relevant server-side operation is successfully saved, if notification slice is implemented.
```
