# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, test planning, API contract artifacts, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios/concerns and how to plan implementation one slice at a time.

## 2. Draft-Driven Discovery

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
```

This applies to:

```text
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

## 3. Slice Types

### Business slice

Scenario-derived business behavior slice.

### Client sidecar

Client implementation file for a concrete business slice.

Client sidecars are not created in advance. They are created or updated when concrete client work starts and then used as draft/discovery files.

### Cross-cutting slice

Technical/support slice with observable behavior, concern-derived behavior items, concern flow, implementation flow and tests/checks, used by multiple business slices.

### Helper slice

Smaller reusable helper/support behavior with implementation and tests.

## 4. Slice Support Files

```text
planning/slices/draft-driven-discovery-principles.md
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

## 5. Cross-Cutting / Helper Slices

Use:

```text
planning/slices/cross-cutting/
```

Current cross-cutting slices:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Cross-cutting/helper slices must have:

```text
- observable/support behavior;
- concern-derived behavior items;
- concern slice flow;
- implementation flow;
- tests/checks;
- consumers / used-by slices;
- local questions;
- ADR impact.
```

## 6. API Contract Support

API contract docs live in:

```text
planning/api/
```

Use them before client slice implementation.

## 7. Shared Notes

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have a full slice behavior/test flow.

## 8. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

## 9. Parent Business Slice Files

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
- links to cross-cutting/helper slices when used;
- link to `.client.md` sidecar when client work starts.
```

## 10. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

It owns detailed client implementation planning and client/component tests.

It should include E2E coverage only for cross-layer behavior that truly needs browser-client-server wiring.
