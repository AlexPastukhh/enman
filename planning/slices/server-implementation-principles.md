# Server Implementation Principles

Status: current server-slice implementation principles  
Scope: backend/server implementation architecture rules used when drafting server slice docs

## 1. Purpose

Use this file when writing or reviewing server/backend/API slice drafts.

This file records implementation principles and current directions that affect server slice planning.

It is not a server slice template and not a full backend architecture document.

## 2. Status Labels

Use these labels when a rule is not equally certain:

```text
accepted principle
current direction
implementation evidence
open question
```

Do not present an implementation detail as an accepted principle unless it is documented or verified.

## 3. Boundary Model

Accepted principle:

```text
controller = HTTP/auth/route/DTO binding/response mapping;
validator = request/query shape validation;
application = orchestration, current actor/load/save, transaction boundary;
domain = invariants, lifecycle, state transition and impossible-state protection;
persistence = durable atomic storage of the chosen changes.
```

A server slice draft should make these responsibilities visible in Implementation Components Overview and Implementation Flow.

## 4. Controller Boundary

Accepted principle:

Controllers may own:

```text
route binding;
request body/query binding;
auth/role attributes;
CSRF/unsafe request attributes;
current account/session extraction;
calling application service/handler;
HTTP response mapping.
```

Controllers must not own:

```text
business lifecycle rules;
aggregate state transitions;
turn/current-actor invariants;
partial persistence decisions;
security rules that belong to domain/application ownership checks.
```

## 5. DTO Validation Boundary

Accepted principle:

Request/query validators own input shape, not lifecycle or ownership.

Validators may check:

```text
required fields;
string length;
positive numeric values;
basic enum value presence;
file/document reference shape;
query paging/sorting shape.
```

Validators must not check:

```text
current actor owns aggregate;
current turn;
aggregate status;
terminal state;
visibility permissions;
domain lifecycle conditions.
```

For validation boundary details, use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 6. Application / Handler Boundary

Accepted principle:

Application services or command handlers own orchestration.

They may:

```text
load current actor;
load aggregate/read model;
create value objects from validated input;
call domain methods;
map application/domain errors;
coordinate persistence;
ensure no partial write on failure.
```

They should not bypass domain methods for behavior that the aggregate owns.

## 7. Domain Boundary

Accepted principle:

Domain methods own invariants and state transitions.

Server slice drafts should name domain aggregate methods when they are central to behavior.

Domain methods should protect:

```text
ownership and participant rules;
current turn/lifecycle rules;
terminal-state rules;
state transitions;
impossible states;
proposal/version/status changes;
no partial mutation on failed transition.
```

## 8. Command / Read Split

Current direction:

```text
commands mutate state and prove persisted outcomes;
reads project state and do not mutate;
query handlers should not perform writes;
command handlers should not return large read/details payloads unless the contract explicitly requires it.
```

If a slice requires a different shape, the draft must explain why.

## 9. Persistence Direction

Current direction / needs verification per slice:

```text
state-changing command paths usually work through aggregate persistence and EF Core / DbContext-style save boundaries;
read paths may use read repositories, projections or Dapper-style read models where that is the established implementation shape;
query handlers do not mutate.
```

Do not claim “all reads use Dapper” or “all writes use EF Core” without checking the current code and relevant architecture docs.

When current implementation evidence matters, inspect the current branch.

## 10. SaveChanges / Transaction / No-Partial-Write Boundary

Accepted principle:

A state-changing command should have an explicit persistence boundary.

The draft must identify:

```text
what is loaded;
what domain/application method changes state;
where changes are persisted;
what must stay unchanged on failure.
```

No-partial-write behavior is part of command correctness.

If failure happens before a valid state transition, the command must not create partial rows or change aggregate state.

## 11. API Result And Error Mapping

Current direction:

```text
request shape failures -> validation/problem response;
auth/session failures -> auth/forbidden/not-found behavior as appropriate;
application/domain failures -> standard result/error mapping;
success for command slices may be 204 No Content when no response body is needed.
```

Use API/error contract docs when error response shape is relevant:

```text
planning/api/
```

## 12. Repository / Load Patterns

Current direction:

A server slice draft should state the load pattern semantically:

```text
load aggregate by id/requestId with required child state;
load read projection by id/filter;
ensure current actor can access loaded state;
save only after domain/application success.
```

Exact repository/ORM names are semantic first-pass names in drafts unless the task is implementation sync against current code.

## 13. Testing Implications

Accepted principle:

Tests prove behavior and outcomes, not handler internals.

Command slices usually need:

```text
API/integration test or equivalent public-boundary command test;
DB/persistence assertions for state changes;
no-mutation assertions for rejected commands;
domain unit tests for core invariants when useful.
```

Read slices usually need:

```text
API/read integration tests;
projection/response shape assertions;
access/visibility filtering assertions.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

## 14. Open Questions / Per-Slice Verification

These items must be checked per slice until they are more strongly documented:

```text
whether a read path uses Dapper, EF projection, repository abstraction or other current implementation;
where SaveChanges is called in the current code path;
whether an application service or command handler owns the transaction boundary;
whether generated OpenAPI artifacts need updating;
whether current tests already prove no-partial-write behavior.
```

A draft may record these as implementation direction or implementation evidence, but should not overstate them as universal accepted principles.
