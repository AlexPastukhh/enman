# Clean Architecture

Status: draft / repo-inspection sync  
Scope: architecture explanation for VKR chapters 2 and 3

## 1. Architecture Overview

The system is designed as a client-server web application.

```text
Browser / React / TypeScript
        ->
HTTP API
        ->
ASP.NET Core backend
        ->
Application handlers
        ->
Domain model
        ->
EF Core / SQL Server
```

This structure separates user interface, API contract, application use cases, domain rules, persistence and generated contract artifacts.

## 2. Backend

Backend is implemented on ASP.NET Core. It is responsible for:

```text
- HTTP API endpoints;
- authentication/session operations;
- client and employee authorization boundaries;
- validation and ProblemDetails error responses;
- application handlers;
- domain model coordination;
- persistence through EF Core;
- generated OpenAPI contract support;
- semantic constants generation support;
- API integration tests.
```

Repo-observed endpoint/controller areas:

```text
- L1 client/auth/applicant/request API;
- EmployeeAuth;
- EmployeeRequests;
- AgreementExchanges;
- Antiforgery.
```

## 3. Frontend

Frontend is implemented on React and TypeScript. It is responsible for:

```text
- application shell, routing and providers;
- session bootstrap and current-user state;
- registration and login pages;
- account page with applicant creation/read behavior;
- request creation UI;
- My Requests list/detail UI;
- employee request dashboard and details UI;
- employee review actions UI;
- client and employee agreement exchange UI;
- typed API wrappers;
- ProblemDetails and form error mapping;
- client/component tests;
- E2E browser flows.
```

## 4. Frontend Architecture Mapping

| Layer | Responsibility |
|---|---|
| `app` | Router, providers, query client, session provider, route guards, global layout |
| `pages` | Route-level composition and screen structure |
| `entities` | Reusable read models, query hooks, status helpers and display components |
| `features` | User command/action behavior, forms, mutations and command-specific tests |
| `widgets` | Composed reusable screen sections, especially agreement exchange lists/details |
| `shared` | Domain-agnostic primitives, HTTP utilities, generated API types and constants |

Diploma-safe wording:

```text
Клиентская часть разделяется на уровни маршрутизации, страниц, сущностей, функциональных действий, виджетов и общих утилит. Такое разделение позволяет отделить экранную композицию от командной логики и повторно использовать отображение бизнес-объектов.
```

## 5. Application Layer

Application layer coordinates use cases:

```text
register client account
login client account
get current user
logout
create individual applicant party
list/select/read applicant parties
create connection request
list own requests
get own request details
employee list request dashboard
employee get request details
employee start request review
employee approve request review
employee reject request review
start agreement exchange
list/get agreement exchange details
send agreement proposal version
client accept active proposal
final refuse agreement exchange
```

## 6. Domain Layer

Domain layer contains business concepts:

```text
Account / ClientAccount / Employee
ApplicantParty / IndividualApplicantParty
ClientRequest / ConnectionRequest
RequestStatus
RequestReview
RejectionFeedback
AgreementProposalExchange
AgreementProposal
AgreementDocumentRef
AgreementExchangeStatus
```

Email notification remains a planned extension point unless a concrete sender/outbox implementation is added.

## 7. API Contract Layer

The project uses a contract-based approach between backend and frontend:

```text
OpenAPI structural contract
+
generated TypeScript API types
+
generated semantic constants/error artifacts
```

OpenAPI describes endpoint paths, HTTP methods, DTOs, response schemas, status codes and ProblemDetails shapes. Generated constants describe stable error codes, field names and ProblemDetails extension names.

See:

```text
planning/thesis/vkr-clean/api-contract-and-client-server-sync.md
```

## 8. Extension And Anti-Coupling Decisions

Important examples:

```text
- approval of request and agreement exchange start are separate actions;
- request creation does not require documents in the first client slice;
- agreement exchange is tied to approved request context;
- email notification after approve/reject/document events is a separate extension point;
- external applicant verification can be added later without hard-coupling the current review logic;
- legally significant electronic signing is outside the current implemented baseline.
```

Diploma-safe wording:

```text
Одобрение заявки и подготовка проектного договорного документа рассматриваются как связанные, но отдельные операции. Такое разделение снижает связанность между обработкой заявки и последующим документооборотом и позволяет развивать договорный этап независимо от базового процесса рассмотрения заявки.
```
