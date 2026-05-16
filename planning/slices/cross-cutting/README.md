# Cross-Cutting And Helper Slices

Status: current cross-cutting/helper slice index / validation transition synchronized  
Scope: independently testable technical/support behavior used by multiple business slices

## 1. Purpose

This folder contains cross-cutting and helper slices.

These are not business scenario slices, but they still use the same source-items-flow-implementation-tests discipline as business slices.

## 2. Current Cross-Cutting Slices

| Slice | Purpose | Status |
|---|---|---|
| `CC-API-001-openapi-contract-artifacts-and-type-generation.md` | Generates/checks structural OpenAPI contract artifacts and client TypeScript types | first-stage implemented; client wrapper migration and hardening planned |
| `CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generates/checks client-facing semantic constants and testing strategy | implemented baseline; client-consumer usage remains per slice |
| `CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context behavior, failure normalization and tests | implementation-ready draft |
| `CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | L1 server request validation transition to FluentValidation; request-shape vs application/domain boundary | final transition draft / implementation-planning |

## 3. When To Create A Cross-Cutting Or Helper Slice

Create one when the work:

```text
- is used by multiple business slices;
- has observable/support behavior;
- has a concrete implementation path;
- has independent tests/checks;
- introduces a reusable contract, artifact, tool, helper, mapper or shared support flow;
- is too concrete to be only a workflow note.
```

## 4. Same Format Rule

Cross-cutting and helper slices must follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> slice flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

They are not allowed to skip behavior items or flow just because the concern is technical.

## 5. Validation Consumer Rule

Server/API business slices with body/query input must read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

and explicitly state which validation layer owns:

```text
- request DTO/query shape;
- branch/discriminator rules;
- mutually exclusive fields;
- ownership/account existence;
- transaction/no-write;
- domain invariants.
```
