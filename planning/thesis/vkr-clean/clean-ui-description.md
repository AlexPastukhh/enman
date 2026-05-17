# Clean UI Description

Status: draft / repo-inspection sync  
Scope: user interface description for VKR chapters 2 and 3

## 1. UI Purpose

The user interface provides browser access to the web application. It supports two main user groups:

```text
- client;
- employee of the network company.
```

Current UI materials should describe implemented screens as implemented only when repo evidence confirms them. Planned or unstable screens should be described as target design or future work.

## 2. Client UI

Repo-observed client UI includes:

```text
- home page / application shell;
- registration page;
- login page;
- session-aware header/navigation;
- account page;
- individual applicant creation and read-only/current applicant presentation;
- request creation page/form;
- My Requests list page;
- My Requests filters;
- own request details page;
- client agreement exchange list page;
- client agreement exchange details page.
```

The client scenario for screenshots and chapter 3 can be presented as:

```text
register / login
-> account page
-> create applicant
-> create request
-> view My Requests
-> open request details
-> view agreement exchange after approval/exchange start
```

## 3. Employee UI

Repo-observed employee UI includes:

```text
- employee request dashboard;
- dashboard filters by request/review state;
- employee request details page;
- start review action;
- approve review action;
- reject review form with feedback;
- start agreement exchange form from an approved request context;
- employee agreement exchange dashboard;
- employee agreement exchange details page.
```

Design note:

```text
Entering a details page is navigation/read context. Review state changes are explicit command actions: start review, approve, reject, start agreement exchange.
```

## 4. Agreement / Document UI

Agreement/document workflow is represented as agreement exchange UI rather than final legal signing UI.

Repo-observed UI areas:

```text
- agreement exchange list;
- agreement exchange details;
- active proposal panel;
- document references list;
- proposal history;
- client accept active proposal action;
- client counter-proposal action;
- employee send proposal version action;
- employee final refusal action.
```

Diploma-safe formulation:

```text
Интерфейс договорного этапа поддерживает просмотр и обмен версиями проектного документа между сотрудником и клиентом. В тексте ВКР этот механизм следует описывать как договорно-документный обмен или согласование проекта документа, не как полноценное юридическое подписание договора.
```

## 5. Form Behavior

```text
- client-side validation improves usability but does not replace server validation;
- server ProblemDetails responses are mapped to field-level and global messages;
- form values and API DTOs may differ;
- trimming, null normalization and DTO mapping should be explicit;
- editing request-local data must not mutate saved applicant data;
- command buttons should communicate unavailable reasons when state transition is not allowed.
```

## 6. Frontend Structure

```text
app      = providers, routing, session bootstrap
pages    = route-level composition
entities = reusable read/display modules
features = command/action forms and mutations
widgets  = composed reusable blocks for agreement exchange details/lists
shared   = reusable primitives, API client, generated types/constants
```

## 7. Screenshots For VKR / Prediploma

Recommended screenshots:

```text
1. Registration page.
2. Login page.
3. Account page / current applicant block.
4. Applicant creation form or success state.
5. Request creation form.
6. My Requests list with filters.
7. Own request details page.
8. Employee request dashboard.
9. Employee request details page with review actions.
10. Agreement exchange list.
11. Agreement exchange details / active proposal panel.
12. Test/report screenshot showing E2E or test results, if needed.
```

Screenshot rule:

```text
Prefer Playwright-generated screenshots for repeatability. Manual screenshots are acceptable only for quick drafts, but final report/presentation assets should be regenerated from a stable UI state.
```

See also:

```text
planning/thesis/vkr-clean/playwright-screenshot-plan.md
```
