# CC-CSRF-001 — Antiforgery Token / Session Context

Status: implementation-ready draft / consumer and drafting considerations synchronized  
Slice type: cross-cutting security/support slice  
Layers: Server API boundary + framework filters + client shared API/helper + auth/session integration + tests  
Depends on: ASP.NET Core cookie authentication, API error contract, generated client constants, client request helper architecture  
Used by: browser unsafe API requests, auth/session flows, protected command slices, file/document command uploads

## 1. Purpose

Provide antiforgery/CSRF protection for cookie-authenticated browser API commands and normalize framework-level antiforgery failures into the project API error contract.

This slice covers:

```text
token issue
token attach
unsafe request validation
session-context token refresh/reset
antiforgery failure ProblemDetails normalization
client recovery behavior without blind unsafe replay
server/client tests
consumer rules for business/client slices
```

## 2. Why This Is Cross-Cutting

This is not an individual business scenario slice.

It protects many browser-origin commands:

```text
register/login/logout
create/edit/delete commands
make-default/current commands
request review commands
agreement proposal commands
final refusal commands
multipart/file/document command uploads
```

Business slices must consume shared CSRF support instead of implementing local token mechanics.

## 3. Scope And Non-Scope

In scope:

```text
- same-origin token issue path;
- shared request-token storage/refetch behavior;
- shared unsafe request helper attaching token;
- server validation before business action logic;
- distinguishable ProblemDetails for CSRF failures;
- no blind replay of unsafe commands;
- server/client tests for CSRF support behavior.
```

Out of scope:

```text
- business command behavior;
- aggregate/domain CSRF checks;
- per-slice local CSRF token handling;
- replacing auth/session model;
- blob/file storage implementation;
- exhaustive E2E coverage for every unsafe endpoint.
```

## 4. Inputs / Sources

| Source | Purpose |
|---|---|
| `planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md` | Cross-cutting browser security requirements. |
| `planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md` | Security-derived behavior items. |
| `planning/api/api-error-contract.md` | ProblemDetails / ServerError contract. |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generated client-facing error code/constants strategy. |
| `planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md` | Consumer concern checklist for business/client drafts. |
| `planning/testing/testing-principles.md` | Test layer boundaries. |
| `planning/testing/e2e-testing-workflow.md` | Later E2E coverage. |

## 5. Concern-Derived Behavior Items

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

## 6. Security Concern Flow

### F01 — Token is available to same-origin browser client

The browser client must be able to obtain an antiforgery request token.

Requirements:

```text
- token issue path is available to browser client;
- token issue does not create uncontrolled cross-site token bootstrap;
- client stores token in runtime/session client state;
- token can be fetched before anonymous unsafe auth commands such as register/login;
- login success can refresh token for authenticated session context.
```

### F02 — Unsafe browser request carries token

Unsafe browser API requests include the request token through shared request infrastructure.

Unsafe means:

```text
POST
PUT
PATCH
DELETE
multipart/form-data command uploads
```

Requirements:

```text
- feature/business slices do not manually attach token;
- shared API request helper attaches token to unsafe requests;
- safe/read GET requests are not forced to attach token by default.
```

### F03 — Server validates unsafe request before business action

Server rejects unsafe browser API requests without valid token before business command logic executes.

Requirements:

```text
- unsafe methods require valid token;
- safe/read methods do not require token by default;
- failed antiforgery validation prevents action business logic from running;
- business handlers/domain methods do not contain CSRF logic.
```

### F04 — Antiforgery failure is distinguishable API failure

Antiforgery failure must not look like ordinary DTO/domain validation.

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

Critical rule:

```text
Do not infer CSRF failure from generic HTTP 400.
```

### F05 — Session context change requires token refresh/reset

Login/logout/session reset changes the effective security context.

Requirements:

```text
- after login, client refetches token;
- after logout/session reset, client clears or refetches token;
- stale/mismatched token failure is recoverable;
- register/login may need anonymous-session token before authentication.
```

### F06 — Client recovers without blind unsafe replay

Client can recover from antiforgery failure but does not automatically replay the unsafe command.

Requirements:

```text
- client may refetch token;
- client shows recoverable session/security message;
- user intentionally retries unsafe command;
- helper must not silently replay POST/PUT/PATCH/DELETE or multipart command uploads.
```

### F07 — Behavior is independently testable

Cross-cutting CSRF support must be tested independently from individual business slices.

Requirements:

```text
- server integration tests cover missing/invalid/valid token behavior;
- client tests cover fetch/store/attach/refetch/no-replay helper behavior;
- later E2E covers one real login/session + unsafe command flow;
- business slices rely on CC-CSRF tests unless they have special security behavior.
```

## 7. Implementation Flow

Implementation flow explains how the concern flow is implemented. It must not be copied into business Scenario Flow.

### I01 — Configure antiforgery services/options

Implementation direction:

```text
configure ASP.NET antiforgery service;
choose request header name;
make header/error-code names available through shared constants if client depends on them.
```

Open detail:

```text
exact header name, assumed candidate `X-CSRF-TOKEN` until implementation decides.
```

### I02 — Token issue endpoint

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

### I03 — Unsafe request validation

Implementation direction:

```text
apply global antiforgery validation to browser unsafe API requests,
for example through AutoValidateAntiforgeryToken or equivalent global validation.
```

### I04 — Normalize antiforgery failure

Implementation direction:

```text
IAlwaysRunResultFilter or equivalent detects antiforgery validation failure result/marker
and replaces the result with project ProblemDetails.
```

Critical decision:

```text
Filter checks antiforgery failure marker/result, not generic HTTP 400.
```

### I05 — Client token fetch/store helper

Implementation direction:

```text
shared client helper fetches token and stores it in runtime/session state.
```

Not now:

```text
Do not store token in localStorage unless separately decided.
```

### I06 — Client unsafe request helper attaches token

Implementation direction:

```text
shared API request helper attaches token header to unsafe requests.
```

Business feature code should call shared request helper rather than manually managing antiforgery headers.

### I07 — Login/logout/session refetch integration

Implementation direction:

```text
login success -> refetch token;
logout/session reset -> clear/refetch token;
missing token before unsafe request -> fetch token.
```

### I08 — Client failure handling without blind replay

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

## 8. File / Multipart Command Considerations

Agreement proposal/document flows may involve file upload or file reference creation.

CSRF rule:

```text
If browser sends a multipart/form-data or upload-related command through cookie auth,
it is still an unsafe request and must use shared CSRF-protected request infrastructure.
```

Domain boundary:

```text
AgreementDocumentRef is document metadata/reference.
It is not bytes and not storage adapter.
```

Drafting rule:

```text
Do not add blob/file storage mechanics to CC-CSRF.
Only note that upload commands must be protected like other unsafe commands.
```

## 9. Consumer Rule For Business / Client Slices

If a slice introduces browser unsafe API command:

```text
1. Add Cross-Cutting Concerns / Considerations section.
2. Mark Antiforgery / browser unsafe requests as applies = yes.
3. Link to CC-CSRF-001.
4. State that shared request infrastructure owns token attach/refresh.
5. Do not put CSRF mechanics in domain/aggregate behavior.
6. Do not add per-feature token handling in client feature code.
7. Do not duplicate missing/invalid token tests unless special security behavior exists.
```

Standard note:

```text
Unsafe browser API requests are protected by CC-CSRF-001 through shared request infrastructure.
This slice does not implement local antiforgery mechanics.
```

For read-only slices:

```text
Safe/read GET endpoints do not require antiforgery token by default.
Authentication and authorization may still apply.
```

## 10. Test / Verification Plan

### Server integration tests

```text
1. unsafe request without token -> ProblemDetails antiforgery code.
2. unsafe request with invalid token -> ProblemDetails antiforgery code.
3. unsafe request with valid token -> reaches normal action/business/validation path.
4. ordinary DTO validation failure -> not converted to antiforgery error.
5. safe GET -> does not require token.
6. token endpoint -> returns token and writes required cookie/context.
7. multipart unsafe command, if implemented through browser helper -> token required.
```

### Client tests

```text
- helper fetches and stores token;
- unsafe request helper attaches token header;
- safe requests do not require token;
- login success triggers token refetch;
- logout/session reset clears/refetches token;
- antiforgery failure handling does not blindly replay unsafe command;
- multipart/upload command helper path attaches token if separate helper exists.
```

### Later E2E smoke coverage

```text
login through UI
-> perform one unsafe authenticated command
-> real API success
```

E2E should not exhaustively test all CSRF branches.

## 11. Target Types / Components

| Type / component | Responsibility |
|---|---|
| `AntiforgeryTokenController` or endpoint | Issues request token to browser client. |
| `AntiforgeryFailureResultFilter` | Converts antiforgery failed result to project ProblemDetails. |
| `IApiProblemDetailsFactory` / equivalent | Creates native ProblemDetails with errors extension. |
| `ClientFacingErrorCodes.Security.AntiforgeryValidationFailed` | Stable client-facing error code, if constants generation is used. |
| `antiforgeryTokenClient` / helper | Fetches and stores request token. |
| `apiClient` / unsafe request helper | Attaches token header to unsafe requests. |
| `auth/session client hooks` | Trigger token refetch after login/logout/session reset. |

This table is not a full class reference.

## 12. Local Questions

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
| Q-CC-CSRF-009 | Multipart upload helper path? | if a separate upload helper exists, it must attach token for unsafe browser upload commands | future implementation detail |

## 13. ADR Impact

Decision notes / ADR candidates:

```text
- CSRF/antiforgery is modeled as cross-cutting slice.
- CSRF has security-derived behavior items even though it is not a business scenario.
- Cross-cutting/helper slices follow same source-items-flow-implementation-tests format as business slices.
- Antiforgery failure normalization uses always-run result filter or equivalent.
- Filter checks antiforgery failure marker/result, not generic HTTP 400.
- Client does not blindly replay unsafe commands after token refresh.
- Session context change after login/logout requires token refetch/reset.
- Browser file/multipart command uploads are unsafe requests and need the same CSRF protection.
```

No full numbered ADR is created by this slice draft.
