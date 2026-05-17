# Clean UI Description

Status: draft  
Scope: user interface description for VKR chapters 2 and 3

## 1. UI Purpose

The user interface provides browser access to the web application. It supports two main user groups:

```text
- client;
- employee of the network company.
```

The current diploma text should describe implemented screens only when repo evidence confirms them. Planned screens should be described as part of the target design.

## 2. Client UI

Client UI includes:

```text
- registration page;
- login page;
- session-aware application shell;
- account page;
- individual applicant creation form;
- request creation form;
- My Requests list;
- request detail page;
- document/notification view when document workflow is added.
```

Current first-stage client baseline includes registration, login, session bootstrap and applicant creation on the account page. Request creation and request read flows should be checked before writing them as implemented.

## 3. Employee UI

Employee UI target includes:

```text
- request queue page;
- request detail/review page;
- approve request action;
- reject request action with feedback/confirmation;
- approved request document/agreement preparation action;
- status/history view.
```

Design note:

```text
Entering a review page should be treated as navigation/read context unless the system introduces a server-side review lock/session/assignment command.
```

## 4. Form Behavior

```text
- client-side validation improves usability but does not replace server validation;
- server ProblemDetails responses are mapped to field-level and global messages;
- form values and API DTOs may differ;
- trimming, null normalization and DTO mapping should be explicit;
- editing request-local data must not mutate saved applicant data.
```

## 5. Frontend Structure

```text
app      = providers, routing, session bootstrap
pages    = route-level composition
entities = reusable read/display modules
features = command/action forms and mutations
shared   = reusable primitives, API client, generated types/constants
```

## 6. Screenshots For VKR

Recommended screenshots:

```text
1. Registration page.
2. Login page.
3. Account page.
4. Applicant creation form.
5. Applicant creation success state.
6. Request creation form after implementation.
7. My Requests list after implementation.
8. Request detail page after implementation.
9. Employee request queue after implementation.
10. Employee review page after implementation.
```
