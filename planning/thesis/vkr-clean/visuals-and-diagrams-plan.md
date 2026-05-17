# Visuals And Diagrams Plan

Status: draft / repo-inspection sync  
Scope: diagrams, tables and visual materials for VKR, prediploma report and defense

## 1. Main Principle

Large diagrams are useful for VKR, but the main text should include readable overview diagrams. Detailed large diagrams can be moved to appendices.

Each diagram should have:

```text
- figure number;
- title;
- explanatory paragraph;
- relation to system behavior or architecture.
```

For screenshots, prefer repeatable Playwright-generated assets after UI state is stable.

## 2. Recommended Diagrams

| Diagram | VKR placement | Purpose |
|---|---|---|
| Business process of request handling | Chapter 1 or 2 | Shows client -> applicant -> request -> employee -> decision -> agreement/document exchange -> notification/future feedback |
| General Use Case diagram | Chapter 2 | Shows actors and main system use cases |
| Client Use Case diagram | Chapter 2 or appendix | Details client actions |
| Employee Use Case diagram | Chapter 2 or appendix | Details employee review and agreement actions |
| Request lifecycle | Chapter 2 | Shows request states and transitions |
| Agreement exchange lifecycle | Chapter 2/3 | Shows proposal exchange states after approved request |
| Client-server architecture | Chapter 2/3 | Shows React -> API -> backend -> DB |
| API contract flow | Chapter 2/3 | Shows OpenAPI/types/constants synchronization |
| Domain model | Chapter 2/3 | Shows Account, ApplicantParty, ConnectionRequest, RequestReview, AgreementProposalExchange, AgreementProposal |
| ERD / database structure | Chapter 3 | Shows tables and relationships |
| Sequence diagram: register/login | Chapter 3 | Shows frontend/API/session flow |
| Sequence diagram: applicant creation | Chapter 3 | Shows Account page -> API -> handler -> DB |
| Sequence diagram: request creation | Chapter 3 | Shows form -> API -> handler -> domain -> DB |
| Sequence diagram: employee review | Chapter 3 | Shows employee dashboard/details -> start/approve/reject -> API -> domain -> DB |
| Sequence diagram: agreement exchange | Chapter 3 | Shows approved request -> start exchange -> send proposal -> client response |
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
-> при одобрении: договорно-документный обмен
-> клиент видит результат / документный обмен
-> email-уведомление как planned/future feedback channel
```

## 4. Request Lifecycle Diagram

Use exact names after final repo-check. Current semantic lifecycle:

```text
Submitted / InReview
        |
        +-> review started
                |
                +-> Approved
                |      |
                |      +-> Agreement exchange started
                |
                +-> Rejected
```

Additional implementation state:

```text
AgreementExchangeFailed
```

## 5. Agreement Exchange Lifecycle Diagram

```text
Approved request
-> Employee starts agreement exchange with first proposal
-> AwaitingClientConfirmation
      |\
      | +-> Client accepts active proposal -> Accepted
      |
      +-> Client sends own version -> AwaitingEmployeeResponse
              |\
              | +-> Employee sends new version -> AwaitingClientConfirmation
              |
              +-> Employee final refusal -> FinallyRefused
```

Use this diagram to avoid claiming full legal signing. Title should be similar to:

```text
Жизненный цикл обмена проектами договорного документа
```

## 6. API Contract Diagram

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

## 7. Screenshots For Prediploma / PZ / Slides

Recommended screenshot set:

```text
01-register-client.png
02-login-client.png
03-account-applicant.png
04-create-request.png
05-my-requests-list.png
06-my-request-details.png
07-employee-request-dashboard.png
08-employee-request-details-review-actions.png
09-agreement-exchange-list.png
10-agreement-exchange-details.png
11-test-run-or-playwright-report.png
```

See detailed plan:

```text
planning/thesis/vkr-clean/playwright-screenshot-plan.md
```

## 8. Defense Slides

For defense use simplified versions:

```text
- one business process diagram;
- one architecture diagram;
- one domain model overview;
- one agreement exchange lifecycle diagram;
- one database overview;
- one testing matrix;
- one status/results table;
- several UI screenshots from Playwright.
```
