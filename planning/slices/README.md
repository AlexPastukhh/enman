# Slice Planning Index

Status: current slice-planning navigation index  
Scope: business slices, client sidecars, cross-cutting/helper slices, examples, shared registers, test planning, API contract artifacts, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios/concerns and how to plan implementation one slice at a time.

It is also the entry point for slice-wide shared registers.

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

Shortened drafts should also capture extension/change points when future behavior can affect current design.

## 3. Slice Types

### Business slice

Scenario-derived business behavior slice.

Full backend/API/persistence business slice files should include:

```text
Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
-> Extension / Change Points, when relevant
```

Visual flow sections should be diagram-like maps, not only linear arrow lists.

### Client sidecar

Client implementation file for a concrete business slice.

Client sidecars are not created in advance. They are created or updated when concrete client work starts and then used as draft/discovery files.

Client sidecars should include architecture/folder-based Visual Client Implementation Flow.

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

Current implemented L1 backend/API/persistence/session slice docs:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-003-logout.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
```

## 5. Current Client Sidecar Files

Implemented/first-stage client sidecars currently documented:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Do not create new `.client.md` files before concrete client work starts or implemented client logic must be reconciled.

## 6. Slice Support Files

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
planning/slices/cross-cutting/README.md
planning/slices/examples/README.md
```

## 7. Slice Registers

Use shared registers to keep local slice discoveries visible across time and chats.

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

### Slice questions register

`slice-questions-register.md` is the shared overview of currently relevant local slice/client/cross-cutting questions.

Local `Questions / Decisions` sections remain the source of detailed context.

The register makes important local questions discoverable from one place.

### Extension points register

`slice-extension-points-register.md` owns extension points, change pressure, anti-coupling decisions and extension/change questions.

### Implementation notes register

`slice-implementation-notes-register.md` owns concrete future implementation/client/testing notes that are not yet assigned to an active slice or sidecar.

## 8. Question / Extension Sync Rule

When a local slice/client/cross-cutting file has a question that remains relevant after the local draft, mirror it into:

```text
planning/slices/slice-questions-register.md
```

Also update specialized registers when needed:

```text
extension/change pressure -> slice-extension-points-register.md
future implementation/client/testing note -> slice-implementation-notes-register.md
scenario/domain ambiguity -> planning/diagrams/scenario-questions-register.md
```

Open questions and unresolved risks should appear before accepted decisions in local files and registers.

If a question remains local only, state why.

## 9. Examples

Use:

```text
planning/slices/examples/
```

Current examples:

```text
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
```

The read-current early short example is the primary shortened draft example for visual flows, assumptions, extension/change points and shared register sync.

The connection-request example is kept as a compact command-flow example.

The full backend slice example shows visual maps before detailed scenario/implementation flows.

## 10. Cross-Cutting / Helper Slices

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

## 11. API Contract Support

API contract docs live in:

```text
planning/api/
```

Use them before client slice implementation.

For current L1 client work, generated OpenAPI TypeScript types already include L1 auth/applicant/request paths and DTOs.

Generated types are support artifacts, not completed feature UI.

## 12. Shared Notes

Use:

```text
planning/slices/shared/
```

for reusable notes/helpers that do not have a full slice behavior/test flow.

## 13. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

Current L1 backend slices are covered primarily by:

```text
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/**
```

Browser E2E for applicant/request flows should wait until concrete client/read UI exists.

## 14. Parent Business Slice Files

Parent business slice files own:

```text
- vertical behavior;
- Visual Scenario Flow;
- Scenario Slice Flow;
- behavior item coverage summary;
- Visual Implementation Flow;
- cross-layer Implementation Flow;
- API contract;
- local Questions / Decisions;
- links/back-references to shared registers when questions are mirrored;
- extension/change/pressure decisions;
- application/domain/persistence responsibilities;
- server/integration tests;
- end-to-end test coverage summary when relevant;
- links to cross-cutting/helper slices when used;
- link to `.client.md` sidecar when client work starts.
```

## 15. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts or implemented client logic must be documented and reconciled.

It owns detailed client implementation planning and client/component tests.

It should include E2E coverage only for cross-layer behavior that truly needs browser-client-server wiring.

Client sidecars also follow the local/global question sync rule and extension/change point review rule.

Recommended remaining client order:

```text
Logout UI/cache/navigation, if needed
-> Current applicant read after refresh
-> Request Creation UI
-> My Requests read/list/detail
-> Browser E2E happy paths
```
