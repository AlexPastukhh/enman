# Cross-Cutting Concerns Drafting Checklist

Status: current / mandatory drafting checklist  
Scope: backend slices, client sidecars, cross-cutting/helper slices, L2 Employee/Review/Agreement/Document drafts

## 1. Purpose

Every non-trivial slice draft must include a section named:

```text
Cross-Cutting Concerns / Considerations
```

This checklist prevents new drafts from forgetting shared project concerns that are not owned by one business scenario.

The section is a checklist, not a place to invent behavior.

## 2. Source Rule

Cross-cutting concerns are not automatically scenario behavior items.

Use this rule:

```text
Scenario Flow:
  only scenario/user/system behavior from scenario sources.

Behavior Coverage:
  source scenario behavior items + concern-derived behavior items from explicit cross-cutting sources.

Cross-Cutting Concerns / Considerations:
  applies / not applicable / future owner notes for shared concerns.
```

Do not put implementation details such as CSRF token headers, React Query invalidation, repository methods or generated files into Scenario Flow.

## 3. Required Section Shape

Use this table in slice drafts:

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes/no | ... |
| Authorization/ownership | yes/no | ... |
| Antiforgery / browser unsafe requests | yes/no | ... |
| Request validation / ProblemDetails | yes/no | ... |
| OpenAPI / generated artifacts | yes/no | ... |
| Generated constants / error codes | yes/no | ... |
| Transaction / atomicity / no partial write | yes/no | ... |
| No-mutation / existing data safety | yes/no | ... |
| Idempotency / double-submit / retry | yes/no | ... |
| Concurrency / stale state | yes/no | ... |
| File/document boundary | yes/no | ... |
| Clock/audit actor fields | yes/no | ... |
| Privacy / cross-account data exposure | yes/no | ... |
| Client feedback / accessibility | yes/no | ... |
| Testing responsibility split | yes/no | ... |

`no` is allowed, but it must be intentional.

## 4. Concern Details

### 4.1 Auth / Session / Account Context

Consider:

```text
- Does the server derive current account/employee from auth context?
- Is the actor ClientAccount or Employee?
- Does protected use case require active/activated account?
- Does client page need unauthenticated/session loading branches?
- Does login/logout/current-user affect cache/token/session state?
```

Owner examples:

```text
SL-AUTH-* / current-user sidecar
Employee auth/account slice, if L2 employee identity is introduced
business slice only derives/uses current actor
```

### 4.2 Authorization / Ownership

Consider:

```text
- Does selected entity belong to current account/employee scope?
- What is not-owned behavior: 404, 403 or 422 by current L1 convention?
- Are cross-account rows excluded from list/read models?
- Does employee action require assignment/started-by/current employee ownership?
```

Do not model ownership only in the UI. Server/domain/application remains source of truth.

### 4.3 Antiforgery / Browser Unsafe Requests

Applies to browser-origin unsafe API requests:

```text
POST
PUT
PATCH
DELETE
multipart/form-data command uploads
```

Use:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Draft note pattern:

```text
Unsafe browser command uses shared API helper protected by CC-CSRF-001.
This slice does not implement local antiforgery mechanics.
```

Safe/read GET endpoints do not require antiforgery token by default, but still require auth/authorization when protected.

### 4.4 Request Validation / ProblemDetails

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
```

Consider:

```text
- DTO/body/query/route shape validation;
- branch/discriminator rules;
- mutually exclusive fields;
- API field names in errors;
- business/domain validation vs request-shape validation;
- native ProblemDetails + shared errors extension.
```

### 4.5 OpenAPI / Generated Artifacts

Use:

```text
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Consider:

```text
- Does this slice add/change an endpoint or DTO?
- Are generated TypeScript types needed before client implementation?
- Do not invent generated operation ids before generation.
- Do not manually edit Shared/openapi.json or generated TypeScript types.
```

### 4.6 Generated Constants / Error Codes

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Consider:

```text
- new stable error code?
- status enum labels shared with client?
- client-facing constants used for rendering or branching?
```

### 4.7 Transaction / Atomicity / No Partial Write

Consider:

```text
- Does command write multiple aggregates/entities?
- Is one SaveChanges enough or is explicit transaction needed?
- What happens if second write fails?
- Does command leave orphan rows on failure?
```

Example:

```text
New ApplicantParty + Request creation is one user intent and must be atomic.
Final refusal orchestration changes Exchange and Request through application service in one transaction.
```

### 4.8 No-Mutation / Existing Data Safety

Consider:

```text
- Existing requests must not be relinked/re-written by ApplicantParty default changes.
- Read endpoints must not repair/mutate state.
- Failed commands must leave persisted state unchanged.
- Other types/rows must remain unchanged.
```

No-mutation is often a test-plan category, not a scenario behavior item unless scenario sources say so.

### 4.9 Idempotency / Double Submit / Retry

Consider:

```text
- Is the command safe if user clicks twice?
- Should selecting already-current/default be success/no-op?
- Is a retry safe after network failure?
- Does client prevent duplicate submit while pending?
- Does shared CSRF helper avoid blind unsafe replay?
```

Idempotency is not automatic. State it explicitly.

### 4.10 Concurrency / Stale State

Consider:

```text
- Another employee may start review before current employee clicks Start.
- Another employee may complete/refuse/send proposal before stale page action.
- ApplicantParty default may change after page load.
- Version exchange state may move between AwaitingClientConfirmation and AwaitingEmployeeResponse.
```

Server/domain remains source of truth. Client handles rejected/stale action with visible feedback.

### 4.11 File / Document Boundary

For agreement proposal documents use domain direction:

```text
AgreementDocumentRef = metadata reference to accepted file/document
not bytes
not storage adapter
not upload service
```

Consider:

```text
- file upload/storage belongs to infrastructure/application;
- domain stores AgreementDocumentRef;
- upload commands may need antiforgery, size/content-type validation, ProblemDetails mapping;
- do not place blob bytes in domain draft or scenario flow.
```

### 4.12 Clock / Audit Actor Fields

Consider:

```text
- CreatedAt / StartedAt / CompletedAt / RefusedAt from application clock;
- StartedByEmployeeId / CompletedByEmployeeId / SenderId / FinalRefusedByEmployeeId from domain actor;
- client does not choose authoritative server timestamps;
- API/client may display these values but not author them unless explicitly part of input.
```

### 4.13 Privacy / Cross-Account Data Exposure

Consider:

```text
- Do not expose ClientAccountId when response does not need it.
- List reads return only current account/employee scope.
- Details endpoint hides not-owned resources.
- Employee dashboards must not expose client/private data beyond scenario need.
- Applicant/contact data is PII; response DTOs should be minimal.
```

### 4.14 Client Feedback / Accessibility

Client sidecars consider:

```text
- loading/empty/error states;
- disabled/pending state for commands;
- field/root ProblemDetails mapping;
- keyboard/button accessibility;
- visible success/failure feedback;
- not-found/unauthorized navigation behavior.
```

Use client cross-cutting docs for detailed UI conventions.

### 4.15 Testing Responsibility Split

Use:

```text
planning/testing/server-slice-test-plan-rules.md
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

Consider:

```text
- API boundary/access tests;
- DB state transition tests;
- no-mutation tests;
- regression guards;
- client component tests;
- shared API/helper tests;
- E2E visible outcomes only;
- what not to test.
```

## 5. Drafting Examples

### Backend command example

```text
## Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Server derives current ClientAccountId from L1 auth claims. |
| Authorization/ownership | yes | Selected ApplicantParty must belong to current account. |
| Antiforgery / browser unsafe requests | yes | POST is protected by CC-CSRF-001 through shared browser unsafe request support. |
| Request validation / ProblemDetails | yes | Route id must be valid; ownership/business failures map to L1 ProblemDetails convention. |
| OpenAPI / generated artifacts | yes | Endpoint changes require OpenAPI + generated TS workflow. |
| Transaction / atomicity | yes | Selected/default switch persists selected + previous same-type changes together. |
| No-mutation / existing data safety | yes | Existing requests are not relinked. Other ApplicantParty types unchanged. |
| Idempotency / double-submit | yes | Selected already current/default may be idempotent success/no-op. |
| Testing responsibility split | yes | API + DB state assertions; no repository mock call-order proof. |
```

### Client command example

```text
## Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Page uses current session branch and protected API behavior. |
| Antiforgery / browser unsafe requests | yes | Feature uses shared API wrapper; no local token handling. |
| OpenAPI / generated artifacts | yes | Exact operation/schema names come from generated types. |
| Idempotency / double-submit | yes | Button disabled/pending while mutation runs. |
| Client feedback / accessibility | yes | Pending/error feedback near action; current/default action hidden/disabled for current card. |
| E2E | yes | Assert visible highlighted card changes, not query invalidation internals. |
```

## 6. Final Check

Before finalizing a draft:

```text
[ ] Cross-Cutting Concerns / Considerations section exists.
[ ] Each relevant concern says applies / not applicable / future owner.
[ ] Scenario Flow is not polluted with implementation details.
[ ] Behavior Coverage uses source items only.
[ ] API/generated artifacts are not manually invented.
[ ] Unsafe browser commands mention CC-CSRF-001.
[ ] Tests are split by proof type.
```
