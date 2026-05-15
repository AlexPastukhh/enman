# Testing Principles

Status: current testing responsibility map  
Scope: domain, server/API, client/component and E2E test boundaries

## 1. Purpose

This file defines what belongs to each testing layer.

It is intentionally explicit because several test responsibilities are easy to mix:

```text
client-visible UI behavior
API/server behavior
cross-layer E2E communication
accessibility locator contract
generated API/client constants
```

## 2. Test Responsibility Matrix

| Test layer | Owns | Does not own |
|---|---|---|
| Domain unit tests | Domain invariants, state transitions, failed-command no-write behavior | API routing, persistence, browser behavior |
| Server integration/API tests | Application/service orchestration, API contract, validation, ProblemDetails/ServerError, persistence effects | Full browser workflow, detailed component UI state |
| Client/component tests | Visible UI behavior, form state, deferred validation, field errors, ARIA/accessibility contract, disabled/enabled states, client DTO mapping, error parser/mapping | Real server persistence, full browser-server wiring |
| E2E tests | Real browser flow, real HTTP communication, route/session/navigation wiring, persistence/server-visible outcome, critical happy paths and critical cross-layer failure/stale/auth flows | Exhaustive field validation, UI state matrix, every server validation branch |

## 3. Core E2E Boundary Decision

E2E tests use the UI as the public entry point and final observation point.

However:

```text
E2E tests do not exhaustively test client-visible UI behavior.
```

Detailed UI behavior belongs to client/component tests:

```text
- labels;
- field errors;
- deferred validation timing;
- aria-describedby / aria-invalid;
- disabled/enabled state;
- field-level server error mapping;
- form state matrix.
```

E2E tests verify that a scenario is actually wired across layers:

```text
browser
-> React client
-> HTTP API
-> server/application/domain/persistence/session
-> response
-> visible route/state/result
```

## 4. When E2E Is Worth It

Write E2E tests for:

```text
- critical happy paths;
- flows crossing browser + client + real HTTP API + server + persistence/session;
- flows where route/navigation/session matters;
- flows where UI behavior depends on server result;
- critical stale/auth/access failure flows;
- flows important for diploma demonstration.
```

Do not write E2E tests for every field validation case.

## 5. Testing Notes For Slice Files

Parent slice and `.client.md` files should classify test coverage as:

```text
Client/component tests
Server integration/API tests
End-to-end tests
```

If a behavior item is client-visible but not cross-layer, prefer client/component tests.

If a behavior item is server/API contract, prefer server integration/API tests.

If a behavior item proves browser-client-server-persistence/session wiring, use E2E.

## 6. Generated Constants And API Contract Testing

Use:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

for generated constants and client-facing error code testing rules.

Do not duplicate those rules in every slice.
