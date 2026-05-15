# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, test planning, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios and how to plan implementation one slice at a time.

## 2. Slice Types

### Business slice

Scenario-derived business behavior slice.

### Client sidecar

Client implementation file for a concrete business slice.

### Cross-cutting slice

Technical/support slice with observable behavior, implementation flow and tests, used by multiple business slices.

### Helper slice

Smaller reusable helper/support behavior with implementation and tests.

## 3. Slice Support Files

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
planning/slices/cross-cutting/README.md
```

## 4. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

Use it when planning:

```text
client/component tests
server integration/API tests
end-to-end tests
Playwright cleanup
Page Object / Component Object decisions
```

## 5. Cross-Cutting / Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

Current cross-cutting slices:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Cross-cutting/helper slices must have:

```text
- observable/support behavior;
- implementation flow;
- tests;
- consumers / used-by slices;
- local questions;
- ADR impact.
```

## 6. Shared Notes

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have a full slice behavior/test flow.

## 7. Parent Slice Files

Parent business slice files own:

```text
- vertical behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- cross-layer Implementation Flow;
- extension/change/pressure decisions;
- application/domain/persistence responsibilities;
- server/integration tests;
- end-to-end test coverage summary when relevant;
- link to `.client.md` sidecar when client work starts.
```

## 8. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

It owns detailed client implementation planning and client/component tests.

It should include E2E coverage only for cross-layer behavior that truly needs browser-client-server wiring.
