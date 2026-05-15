# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, examples, test planning, API contract artifacts, client architecture, extension/change points and implementation notes

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

Full backend/API/persistence business slice files should include:

```text
Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
```

Visual flow sections should be diagram-like maps, not only linear arrow lists.

### Client sidecar

Client implementation file for a concrete business slice.

Client sidecars are not created in advance. They are created or updated when concrete client work starts and then used as draft/discovery files.

### Cross-cutting slice

Technical/support slice with observable behavior, concern-derived behavior items, concern flow, implementation flow and tests/checks, used by multiple business slices.

### Helper slice

Smaller reusable helper/support behavior with implementation and tests.

### Example slice draft

Example files live in:

```text
planning/slices/examples/
```

They are examples only and are not current implementation evidence unless copied into an active slice and reconciled with current repo state.

## 4. Current Backend Slice Files

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

These files are current backend/API/persistence slice docs and should follow the full backend slice flow rule.

## 5. Slice Support Files

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
planning/slices/examples/README.md
```

## 6. Examples

Use:

```text
planning/slices/examples/
```

Current examples:

```text
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
```

The early short example shows the valid shortened working format.

The full backend slice example shows visual maps before detailed scenario/implementation flows.

## 7. Cross-Cutting / Helper Slices

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

## 8. API Contract Support

API contract docs live in:

```text
planning/api/
```

Use them before client slice implementation.

## 9. Shared Notes

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have a full slice behavior/test flow.

## 10. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

## 11. Parent Business Slice Files

Parent business slice files own:

```text
- vertical behavior;
- Visual Scenario Flow;
- Scenario Slice Flow;
- behavior item coverage summary;
- Visual Implementation Flow;
- cross-layer Implementation Flow;
- API contract;
- extension/change/pressure decisions;
- application/domain/persistence responsibilities;
- server/integration tests;
- end-to-end test coverage summary when relevant;
- links to cross-cutting/helper slices when used;
- link to `.client.md` sidecar when client work starts.
```

## 12. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

It owns detailed client implementation planning and client/component tests.

It should include E2E coverage only for cross-layer behavior that truly needs browser-client-server wiring.
