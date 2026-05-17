# Cross-Cutting Concerns Drafting Checklist

Status: current / includes antiforgery internal-public marker distinction  
Scope: required concern scan for server slices, client sidecars and cross-cutting/helper slices

## 1. Purpose

Every non-trivial slice draft should include:

```text
## Cross-Cutting Concerns / Considerations
```

This section is not Scenario Flow.

It is a concern checklist that prevents drafts from forgetting shared rules that affect many slices.

Use it to write:

```text
Applies / not applicable / future owner / handled by cross-cutting slice.
```

Do not turn implementation mechanics into Behavior Items unless a cross-cutting behavior source explicitly defines them.

## 2. Concern Categories

### Auth / session / actor context

Consider:

```text
- authenticated vs anonymous;
- current account id;
- current employee id;
- session bootstrap;
- login/logout context changes;
- active account/employee requirement.
```

Draft note shape:

```text
Auth/session:
  Applies.
  Uses current authenticated actor from session/claims.
  Domain method receives loaded domain actor when behavior depends on actor capability.
```

### Authorization / ownership / data isolation

Consider:

```text
- owned account resource;
- not-owned resource handling;
- cross-account data exposure;
- employee access boundary;
- request belongs to applicant/client context.
```

### Antiforgery / browser unsafe requests

Applies to browser unsafe methods:

```text
POST
PUT
PATCH
DELETE
multipart/document command upload
```

Use:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Draft note shape:

```text
Antiforgery / browser unsafe request:
  Applies.
  Unsafe browser API request must use shared antiforgery helper.
  Server normalization is owned by CC-CSRF-001.
  Internal marker:
    antiforgery-specific framework failed result, preferably IAntiforgeryValidationFailedResult.
  Public client marker:
    ProblemDetails.Extensions["code"] = security.antiforgery.validation.failed.
  Do not infer CSRF from generic 400/BadRequest.
  Do not use FluentValidation/ServerValidationError/errors[] for CSRF marker.
  No blind unsafe auto-replay after token refresh.
```

For read-only safe GET:

```text
Antiforgery / browser unsafe request:
  Not applicable because this slice is safe/read-only GET.
```

### Request validation / ProblemDetails

Consider:

```text
- DTO/body/query/route shape validation;
- FluentValidation ownership;
- application/domain errors;
- 422 validation ProblemDetails;
- stable error codes.
```

Important distinction:

```text
Antiforgery failure is 400 ProblemDetails with top-level code.
DTO validation is 422 ProblemDetails and must not be mislabeled as CSRF.
```

### OpenAPI / generated artifacts

Consider:

```text
- endpoint/DTO/status changes;
- generated OpenAPI;
- generated TypeScript;
- generated constants/error codes.
```

Rule:

```text
Generated artifacts come from repo generation commands.
Do not hand-edit generated files.
```

### Transaction / atomicity / no partial write

Consider:

```text
- multiple aggregates;
- multiple repositories;
- request + exchange orchestration;
- applicant + request creation;
- no orphan rows;
- SaveChanges boundary.
```

### No-mutation / existing data safety

Consider:

```text
- existing requests not relinked/recreated;
- unrelated rows unchanged;
- old default/current row not deleted;
- proposal replacement is superseded, not rejected;
- read endpoints do not mutate or repair state.
```

### Idempotency / retry / double-submit

Consider:

```text
- command safe if submitted twice;
- already-current/default state;
- already-started review;
- already-accepted or finally-refused exchange;
- no blind replay after token refresh.
```

### Concurrency / stale state

Consider:

```text
- two employees attempt to start same review;
- same review completed by another employee;
- proposal version stale when client responds;
- stale current/default view;
- optimistic concurrency or domain precondition failure.
```

Do not invent full locking strategy unless the slice owns it. Mark open/future if needed.

### File / document boundary

Consider:

```text
- uploaded bytes vs domain document reference;
- metadata validation;
- storage adapter outside domain;
- AgreementDocumentRef stores reference metadata only;
- file scanning/size/content-type policy.
```

### Clock / audit actor fields

Consider:

```text
- StartedAt;
- CompletedAt;
- FinalRefusedAt;
- SenderId;
- StartedByEmployeeId;
- CompletedByEmployeeId;
- current actor from auth context.
```

### Privacy / PII / display minimization

Consider:

```text
- applicant contact data;
- employee dashboard data;
- file metadata;
- cross-account leakage;
- unnecessary ids in client response.
```

### Client feedback / accessibility

Consider:

```text
- visible pending/success/error state;
- recoverable security/session failure;
- explicit retry;
- field vs root errors;
- keyboard/button disabled state.
```

### Testing responsibility split

Consider:

```text
- API boundary tests;
- DB state transition tests;
- no-mutation tests;
- client component tests;
- shared API/helper tests;
- E2E visible outcome only;
- cross-cutting tests owned by cross-cutting slice.
```

## 3. Required Draft Section Template

Use this in slice drafts:

```text
## Cross-Cutting Concerns / Considerations

| Concern | Applies? | Decision / owner |
|---|---|---|
| Auth/session/actor context | yes/no | ... |
| Authorization/ownership | yes/no | ... |
| Antiforgery / unsafe request | yes/no | ... |
| Request validation / ProblemDetails | yes/no | ... |
| OpenAPI / generated artifacts | yes/no | ... |
| Transaction / atomicity | yes/no | ... |
| No-mutation / existing data safety | yes/no | ... |
| Idempotency / retry / double-submit | yes/no | ... |
| Concurrency / stale state | yes/no | ... |
| File/document boundary | yes/no | ... |
| Clock/audit actor fields | yes/no | ... |
| Privacy / data exposure | yes/no | ... |
| Client feedback / accessibility | yes/no | ... |
| Testing responsibility split | yes/no | ... |
```

Keep entries short. Put details in slice-specific sections only when the concern actually affects the slice design.

## 4. Do Not

```text
- Do not put this checklist into Scenario Flow.
- Do not turn every concern into a behavior item.
- Do not duplicate cross-cutting tests in every business slice.
- Do not implement CSRF locally inside business handlers/domain.
- Do not infer antiforgery failure from generic 400/BadRequest.
- Do not confuse public client marker with internal framework marker.
- Do not hand-edit generated artifacts.
```
