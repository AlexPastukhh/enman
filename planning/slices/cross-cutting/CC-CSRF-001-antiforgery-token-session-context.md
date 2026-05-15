# CC-CSRF-001 — Antiforgery Token / Session Context

Status: implementation-ready draft  
Slice type: cross-cutting slice  
Layers: Server API boundary + framework filters + client shared API/helper + tests  
Depends on: ASP.NET Core cookie authentication, API error contract, generated client constants, client request helper architecture  
Used by: all browser unsafe API requests, auth/session flows, protected command slices

## 1. Purpose

Provide antiforgery/CSRF protection for cookie-authenticated browser API commands and normalize framework-level antiforgery failures into project API error contract.

This slice covers:

```text
token issue
token attach
unsafe request validation
session-context token refresh
antiforgery failure ProblemDetails normalization
client recovery behavior
server/client tests
```

## 2. Why This Is A Cross-Cutting Slice

This is not an individual business scenario slice.

It is cross-cutting because it protects many browser command slices and auth/session flows.

### Observable/support behavior

```text
- browser client can obtain antiforgery request token;
- unsafe browser API requests without valid token are rejected;
- safe requests do not require request token by default;
- client helper attaches token to unsafe requests;
- client refetches/resets token on session context changes;
- antiforgery failure is distinguishable from ordinary validation failures;
- client can recover without blindly replaying unsafe command.
```

### Implementation path

```text
- configure antiforgery services/options;
- token issue endpoint;
- global unsafe request validation;
- always-run result filter for antiforgery failure normalization;
- shared client antiforgery token helper;
- shared unsafe request helper/header attach;
- generated constants/error codes if needed;
- server integration tests;
- client helper tests;
- later E2E smoke coverage.
```

### Independent testability

```text
- server integration tests can call unsafe endpoints with missing/invalid/valid token;
- client tests can cover token fetch/store/attach/refetch behavior without business slice UI;
- business slices only need to prove their own command behavior and rely on this cross-cutting support.
```

## 3. Inputs / Sources

| Source | Purpose |
|---|---|
| `planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md` | Cross-cutting browser security requirements |
| `planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md` | Security-derived behavior items |
| `planning/slices/shared/antiforgery-token-session-context.md` | Existing shared support note |
| `planning/api/api-error-contract.md` | ProblemDetails / ServerError contract |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generated client-facing error code/constants strategy |
| `planning/testing/testing-principles.md` | Test layer boundaries |
| `planning/testing/e2e-testing-workflow.md` | Later E2E coverage |

## 4. Concern-Derived Behavior Items

| ID | Behavior | Concern flow step |
|---|---|---|
| CC-CSRF-SRV-001 | Server provides same-origin token issue path. | F01 |
| CC-CSRF-SRV-002 | Server validates unsafe browser API requests before business action logic. | F03 |
| CC-CSRF-SRV-003 | Safe/read requests do not require request token by default. | F03 |
| CC-CSRF-SRV-004 | Token endpoint must not become uncontrolled cross-site token bootstrap. | F01 |
| CC-CSRF-CL-001 | Client fetches and stores antiforgery request token. | F01 |
| CC-CSRF-CL-002 | Client attaches token to unsafe requests through shared helper. | F02 |
| CC-CSRF-CL-003 | Client refetches/resets token after login/logout/session context change. | F05 |
| CC-CSRF-ERR-001 | Antiforgery failure becomes distinguishable ProblemDetails with stable code. | F04 |
| CC-CSRF-ERR-002 | Client treats antiforgery failure as recoverable security/session failure. | F06 |
| CC-CSRF-NW-001 | Client does not blindly auto-replay unsafe command after token refresh. | F06 |
| CC-CSRF-TEST-001 | Server tests cover missing token failure. | F07 |
| CC-CSRF-TEST-002 | Server tests cover invalid token failure. | F07 |
| CC-CSRF-TEST-003 | Server tests prove DTO validation is not mislabeled as antiforgery failure. | F07 |
| CC-CSRF-TEST-004 | Client tests cover token helper behavior. | F07 |
| CC-CSRF-TEST-005 | Later E2E covers login/session plus unsafe command success. | F07 |

## 5. Coverage Overview

| Behavior item | Concern flow | Implementation flow | Test coverage | Status |
|---|---|---|---|---|
| CC-CSRF-SRV-001 | F01 | I02 | token endpoint integration test | planned |
| CC-CSRF-SRV-002 | F03 | I03 | missing/invalid token integration tests | planned |
| CC-CSRF-SRV-003 | F03 | I03 | safe GET no-token test | planned |
| CC-CSRF-SRV-004 | F01 | I02 | config/security review + integration constraints | planned |
| CC-CSRF-CL-001 | F01 | I05 | client helper tests | planned |
| CC-CSRF-CL-002 | F02 | I06 | client request helper tests | planned |
| CC-CSRF-CL-003 | F05 | I07 | login/logout/session helper tests | planned |
| CC-CSRF-ERR-001 | F04 | I04 | ProblemDetails integration tests | planned |
| CC-CSRF-ERR-002 | F06 | I08 | client error handling tests | planned |
| CC-CSRF-NW-001 | F06 | I08 | no blind replay client tests | planned |
| CC-CSRF-TEST-001 | F07 | I09 | integration test | planned |
| CC-CSRF-TEST-002 | F07 | I09 | integration test | planned |
| CC-CSRF-TEST-003 | F07 | I09 | integration test | planned |
| CC-CSRF-TEST-004 | F07 | I10 | client tests | planned |
| CC-CSRF-TEST-005 | F07 | I11 | later E2E | planned later |

## 6. Security Concern Flow

This is the required behavior flow.

Implementation details come after this section.

### F01 — Token is available to same-origin browser client

The browser client must be able to obtain an antiforgery request token.

Covers:

```text
CC-CSRF-SRV-001
CC-CSRF-SRV-004
CC-CSRF-CL-001
```

Requirements:

```text
- token issue path is available to the browser client;
- token issue does not create uncontrolled cross-site token bootstrap;
- client stores token in runtime/session client state;
- token can be fetched before anonymous unsafe auth commands such as register/login.
```

### F02 — Unsafe request carries request token

Unsafe browser API requests must include the antiforgery request token through shared client request infrastructure.

Covers:

```text
CC-CSRF-CL-002
```

Requirements:

```text
- feature/business slices do not manually attach token;
- shared API request helper attaches token to POST/PUT/PATCH/DELETE;
- safe/read requests are not forced to attach token unless a later decision requires it.
```

### F03 — Server validates unsafe request before business action

Server rejects unsafe browser API requests without valid token before business command logic executes.

Covers:

```text
CC-CSRF-SRV-002
CC-CSRF-SRV-003
```

Requirements:

```text
- unsafe methods require valid token;
- safe methods do not require token by default;
- failed antiforgery validation prevents action business logic from running.
```

### F04 — Antiforgery failure is distinguishable API failure

Antiforgery failure must be distinguishable from ordinary DTO validation or domain errors.

Covers:

```text
CC-CSRF-ERR-001
```

Required response direction:

```text
native ProblemDetails
+ shared errors extension
+ stable client-facing error code
```

Candidate error code:

```text
security.antiforgery.validation.failed
```

### F05 — Session context change requires token refresh/reset

Login/logout/session reset changes the effective security context.

Covers:

```text
CC-CSRF-CL-003
```

Requirements:

```text
- after login, client refetches token;
- after logout/session reset, client clears or refetches token;
- stale/mismatched token failure is recoverable.
```

### F06 — Client recovers without blind unsafe replay

Client can recover from antiforgery failure but does not automatically replay the unsafe command.

Covers:

```text
CC-CSRF-ERR-002
CC-CSRF-NW-001
```

Requirements:

```text
- client may refetch token;
- client shows recoverable session/security message;
- user intentionally retries unsafe command;
- helper must not silently replay POST/PUT/PATCH/DELETE.
```

### F07 — Behavior is independently testable

Cross-cutting behavior must be tested independently from individual business slices.

Covers:

```text
CC-CSRF-TEST-001
CC-CSRF-TEST-002
CC-CSRF-TEST-003
CC-CSRF-TEST-004
CC-CSRF-TEST-005
```

Requirements:

```text
- server integration tests cover missing/invalid/valid token behavior;
- client tests cover fetch/store/attach/refetch/no-replay helper behavior;
- later E2E covers one real login/session + unsafe command flow.
```

## 7. Implementation Flow

Implementation flow explains how the concern flow is implemented.

It should stay behavior-first and include code/classes only where they clarify important boundaries.

### I01 — Configure antiforgery services/options

Concern flow:

```text
F02
F03
```

Implementation direction:

```text
configure ASP.NET antiforgery service;
choose request header name;
make header/error-code names available through shared constants if client depends on them.
```

Open detail:

```text
exact header name, currently assumed `X-CSRF-TOKEN`.
```

### I02 — Token issue endpoint

Concern flow:

```text
F01
```

Implementation direction:

```text
GET /api/antiforgery/token
```

Server behavior:

```text
- calls antiforgery service to create/store tokens;
- writes required antiforgery cookie/context;
- returns request token to same-origin browser client.
```

Important boundary:

```text
Token endpoint is support/security infrastructure,
not business scenario endpoint.
```

### I03 — Unsafe request validation

Concern flow:

```text
F02
F03
```

Implementation direction:

```text
apply global antiforgery validation to browser unsafe API requests,
for example through AutoValidateAntiforgeryToken or equivalent global validation.
```

Important boundary:

```text
Validation runs before action business logic.
Business slices should not manually validate antiforgery token.
```

### I04 — Normalize antiforgery failure through always-run result filter

Concern flow:

```text
F04
```

Implementation direction:

```text
IAlwaysRunResultFilter detects antiforgery validation failure result/marker
and replaces the result with project ProblemDetails.
```

Critical decision:

```text
Do not infer CSRF failure from generic HTTP 400.
```

Reason:

```text
ordinary DTO validation and other bad-request failures must not be converted into
security.antiforgery.validation.failed.
```

Expected output:

```text
ProblemDetails
errors[0].ErrorCode = security.antiforgery.validation.failed
```

### I05 — Client token fetch/store helper

Concern flow:

```text
F01
F05
```

Implementation direction:

```text
shared client helper fetches token and stores it in runtime/session state.
```

Not now:

```text
Do not store token in localStorage unless separately decided.
```

### I06 — Client unsafe request helper attaches token

Concern flow:

```text
F02
```

Implementation direction:

```text
shared API request helper attaches token header to unsafe requests.
```

Business feature code should call shared request helper rather than manually managing antiforgery headers.

### I07 — Login/logout/session refetch integration

Concern flow:

```text
F05
```

Implementation direction:

```text
login success -> refetch token;
logout/session reset -> clear/refetch token;
missing token before unsafe request -> fetch token.
```

Important:

```text
register/login may require anonymous-session token before authentication,
then login success refreshes token for authenticated session context.
```

### I08 — Client failure handling without blind replay

Concern flow:

```text
F06
```

Implementation direction:

```text
client recognizes antiforgery failure code;
client may refetch token;
client displays recoverable message;
client requires explicit user retry.
```

No-write/no-replay guarantee:

```text
unsafe command is not automatically replayed by helper after refresh.
```

### I09 — Server integration tests

Concern flow:

```text
F07
```

Test cases:

```text
1. unsafe request without token -> ProblemDetails antiforgery code.
2. unsafe request with invalid token -> ProblemDetails antiforgery code.
3. unsafe request with valid token -> reaches normal action/business/validation path.
4. ordinary DTO validation failure -> not converted to antiforgery error.
5. safe GET -> does not require token.
6. token endpoint -> returns token and writes required cookie/context.
```

### I10 — Client tests

Concern flow:

```text
F07
```

Test cases:

```text
- helper fetches and stores token;
- unsafe request helper attaches token header;
- safe requests do not require token;
- login success triggers token refetch;
- logout/session reset clears/refetches token;
- antiforgery failure handling does not blindly replay unsafe command.
```

### I11 — Later E2E smoke coverage

Concern flow:

```text
F07
```

Later E2E:

```text
login through UI
-> perform one unsafe authenticated command
-> real API success
```

E2E should not exhaustively test all CSRF branches.

## 8. Target Types / Components

| Type / component | Responsibility |
|---|---|
| `AntiforgeryTokenController` or endpoint | Issues request token to browser client. |
| `AntiforgeryFailureResultFilter` | Converts antiforgery failed result to project ProblemDetails. |
| `IApiProblemDetailsFactory` / equivalent | Creates native ProblemDetails with errors extension. |
| `ClientFacingErrorCodes.Security.AntiforgeryValidationFailed` | Stable client-facing error code. |
| `antiforgeryTokenClient` / helper | Fetches and stores request token. |
| `apiClient` / unsafe request helper | Attaches token header to unsafe requests. |
| `auth/session client hooks` | Trigger token refetch after login/logout/session reset. |

This table is not a full class reference.

Keep implementation details in flow only where they clarify behavior.

## 9. Consumer Rule For Business Slices

If a business slice introduces browser unsafe API command:

```text
1. Link to CC-CSRF-001 in parent slice API/security section.
2. Do not add local antiforgery mechanics to business aggregate/domain logic.
3. Ensure client command path uses shared unsafe request helper.
4. Add E2E/API/client tests only for slice-specific behavior.
5. Rely on CC-CSRF tests for cross-cutting missing/invalid token behavior unless the slice has special security behavior.
```

Parent slice API/security note:

```text
Unsafe browser API requests are protected by CC-CSRF-001.
```

## 10. Local Questions

| ID | Question | Assumption / current direction | Status |
|---|---|---|---|
| Q-CC-CSRF-001 | Exact token endpoint path? | `/api/antiforgery/token` | open |
| Q-CC-CSRF-002 | Exact header name? | explicit shared constant, e.g. `X-CSRF-TOKEN` | open |
| Q-CC-CSRF-003 | Does register/login require antiforgery? | yes for browser unsafe API requests; anonymous token before login/register | accepted direction |
| Q-CC-CSRF-004 | Validation mechanism? | ASP.NET antiforgery + global validation policy/filter | open implementation detail |
| Q-CC-CSRF-005 | How to normalize failure? | always-run result filter checks antiforgery failure marker/result, not HTTP 400 | accepted direction |
| Q-CC-CSRF-006 | Auto-retry after token refetch? | no blind replay of unsafe commands | accepted direction |
| Q-CC-CSRF-007 | Token endpoint cross-site bootstrap? | same-origin/controlled origin only | open production hardening |
| Q-CC-CSRF-008 | Cross-tab/session mismatch? | recoverable failure + refetch + explicit user retry | future hardening |

## 11. ADR Impact

Decision notes / ADR candidates:

```text
- CSRF/antiforgery is modeled as cross-cutting slice.
- CSRF has security-derived behavior items even though it is not a business scenario.
- Cross-cutting/helper slices must follow same source-items-flow-implementation-tests format as business slices.
- Antiforgery failure normalization uses always-run result filter.
- Filter checks antiforgery failure marker/result, not generic HTTP 400.
- Client does not blindly replay unsafe commands after token refresh.
- Session context change after login/logout requires token refetch/reset.
```

No full numbered ADR is created by this slice draft.
