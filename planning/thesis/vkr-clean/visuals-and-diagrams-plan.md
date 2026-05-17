# Visuals And Diagrams Plan

Status: draft  
Scope: diagrams, tables and visual materials for VKR and defense

## 1. Main Principle

Large diagrams are useful for VKR, but the main text should include readable overview diagrams. Detailed large diagrams can be moved to appendices.

Each diagram should have:

```text
- figure number;
- title;
- explanatory paragraph;
- relation to system behavior or architecture.
```

## 2. Recommended Diagrams

| Diagram | VKR placement | Purpose |
|---|---|---|
| Business process of request handling | Chapter 1 or 2 | Shows client -> applicant -> request -> employee -> decision -> document -> notification |
| General Use Case diagram | Chapter 2 | Shows actors and main system use cases |
| Client Use Case diagram | Chapter 2 or appendix | Details client actions |
| Employee Use Case diagram | Chapter 2 or appendix | Details employee review actions |
| Request lifecycle | Chapter 2 | Shows request states and transitions |
| Document/agreement lifecycle | Chapter 2 or future work | Shows document draft and further exchange |
| Client-server architecture | Chapter 2/3 | Shows React -> API -> backend -> DB |
| API contract flow | Chapter 2/3 | Shows OpenAPI/types/constants synchronization |
| Domain model | Chapter 2/3 | Shows Account, ApplicantParty, ConnectionRequest, ReviewDecision, Agreement/Document |
| ERD / database structure | Chapter 3 | Shows tables and relationships |
| Sequence diagram: register/login | Chapter 3 | Shows frontend/API/session flow |
| Sequence diagram: applicant creation | Chapter 3 | Shows Account page -> API -> handler -> DB |
| Sequence diagram: request creation | Chapter 3 | Shows form -> API -> handler -> domain -> DB |
| Testing responsibility matrix | Chapter 3 | Shows Domain/API/Client/E2E responsibility split |

## 3. Business Process Diagram Content

```text
Клиент
-> регистрация / вход
-> создание заявителя
-> подача заявки
-> сохранение заявки
-> очередь сотрудника
-> ручная проверка
-> одобрение / отклонение
-> проект договора / документа
-> уведомление клиента
```

## 4. Request Lifecycle Diagram

```text
Created / InReview
        |
        +-> Approved
        |
        +-> Rejected
```

If final code uses exact names, use the exact names from implementation.

## 5. API Contract Diagram

```text
ASP.NET Core endpoints / DTO metadata
        |
        v
Shared/openapi.json
        |
        v
generated TypeScript API types
        |
        v
typed client API wrappers
        |
        v
React forms/pages
```

Parallel flow:

```text
server error codes / constants
        |
        v
Shared/constants.json + Shared/errorcodes.json
        |
        v
client error parser / form error mapping
```

## 6. Defense Slides

For defense use simplified versions:

```text
- one business process diagram;
- one architecture diagram;
- one domain model overview;
- one database overview;
- one testing matrix;
- one status/results table.
```
