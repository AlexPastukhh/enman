# Clean Architecture

Status: draft  
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
- validation and error responses;
- application handlers;
- domain model coordination;
- persistence through EF Core;
- generated OpenAPI contract support;
- semantic constants generation support;
- API integration tests.
```

## 3. Frontend

Frontend is implemented on React and TypeScript. It is responsible for:

```text
- application shell, routing and providers;
- session bootstrap and current-user state;
- registration and login pages;
- account page with applicant creation;
- typed L1 API wrappers;
- ProblemDetails and form error mapping;
- future request creation UI;
- future My Requests list/detail UI;
- future employee workspace UI.
```

## 4. Frontend Architecture Mapping

| Layer | Responsibility |
|---|---|
| `app` | Router, providers, query client, session provider, route guards, global layout |
| `pages` | Route-level composition and screen structure |
| `entities` | Reusable read models, query hooks, status helpers and display components |
| `features` | User command/action behavior, forms, mutations and command-specific tests |
| `shared` | Domain-agnostic primitives, HTTP utilities, generated API types and constants |

Diploma-safe wording:

```text
Клиентская часть разделяется на уровни маршрутизации, страниц, сущностей, функциональных действий и общих утилит. Такое разделение позволяет отделить экранную композицию от командной логики и повторно использовать отображение бизнес-объектов.
```

## 5. Application Layer

Application layer coordinates use cases:

```text
register client account
login client account
get current user
logout
create individual applicant party
create connection request
```

## 6. Domain Layer

Domain layer contains business concepts:

```text
ClientAccount / Account
IndividualApplicantParty / ApplicantParty
ConnectionRequest / ClientRequest
RequestStatus
ReviewDecisionRecord
EmployeeRef
RejectionFeedback
```

In the full VKR model it also supports:

```text
RequestReview
AgreementProposal / AgreementDocument
EmailNotification
```

## 7. API Contract Layer

The project uses a contract-based approach between backend and frontend:

```text
OpenAPI structural contract
+
generated semantic constants
```

OpenAPI describes endpoint paths, HTTP methods, DTOs, response schemas, status codes and ProblemDetails shapes. Generated constants describe stable error codes, field names and ProblemDetails extension names.

See:

```text
planning/thesis/vkr-clean/api-contract-and-client-server-sync.md
```

## 8. Extension And Anti-Coupling Decisions

Important examples:

```text
- approval of request must not automatically create an agreement proposal unless explicitly required;
- request creation should not require documents in the first slice;
- notification after approve/reject is a separate extension point;
- external applicant verification can be added later without hard-coupling the current review logic.
```

Diploma-safe wording:

```text
Одобрение заявки и подготовка проекта договора рассматриваются как связанные, но отдельные операции. Такое разделение снижает связанность между обработкой заявки и последующим документооборотом.
```
