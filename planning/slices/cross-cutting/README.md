# Cross-Cutting And Helper Slices

Status: current cross-cutting/helper slice index / drafting concerns checklist synchronized  
Scope: independently testable technical/support behavior and drafting considerations used by multiple business slices

## 1. Purpose

This folder contains cross-cutting and helper slices.

These are not business scenario slices, but they still use the same source-items-flow-implementation-tests discipline as business slices.

It also contains the mandatory cross-cutting concerns checklist used by backend/client slice drafters.

## 2. Current Cross-Cutting Slices And Checklists

| File | Purpose | Status |
|---|---|---|
| `cross-cutting-concerns-drafting-checklist.md` | Mandatory checklist for slice drafts: auth/session, ownership, CSRF, validation, OpenAPI/generated artifacts, transactions, no-mutation, idempotency, concurrency, documents, audit, privacy, testing. | current drafting rule |
| `CC-API-001-openapi-contract-artifacts-and-type-generation.md` | Generates/checks structural OpenAPI contract artifacts and client TypeScript types. | first-stage implemented; client wrapper migration and hardening planned |
| `CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generates/checks client-facing semantic constants and testing strategy. | implemented baseline; client-consumer usage remains per slice |
| `CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context behavior, failure normalization, consumer rules and tests. | implementation-ready draft |
| `CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | L1 server request validation transition to FluentValidation; request-shape vs application/domain boundary. | final transition draft / implementation-planning |

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
-> concern/scenario flow
-> implementation flow
-> tests/checks
-> coverage/questions/ADR impact
```

They are not allowed to skip behavior items or flow just because the concern is technical.

## 5. Draft Consumer Rule

Every non-trivial backend/client slice draft must include:

```text
## Cross-Cutting Concerns / Considerations
```

Use:

```text
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

The section should say which concerns apply, which do not apply, and which are owned by related/future slices.

Do not put cross-cutting mechanics into Scenario Flow unless a scenario/cross-cutting behavior source defines them as behavior.

## 6. Antiforgery Consumer Rule

Business/client slices that introduce browser unsafe API commands must reference:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Use this note:

```text
Unsafe browser API requests are protected by CC-CSRF-001 through shared request infrastructure.
This slice does not implement local antiforgery mechanics.
```

Do not duplicate missing/invalid token tests in every business slice unless the slice has special security behavior.

## 7. Validation Consumer Rule

Server/API business slices with body/query input must read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

and explicitly state which validation layer owns:

```text
- request DTO/query shape;
- route input shape;
- branch/discriminator rules;
- mutually exclusive fields;
- ownership/account existence;
- transaction/no-write;
- domain invariants.
```

## 8. API / Generated Artifact Consumer Rule

API-changing slices must read:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
```

Generated artifacts must come from repo commands, not manual edits.
