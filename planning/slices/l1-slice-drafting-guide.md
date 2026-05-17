# L1 Slice Drafting Guide

Status: current / strict example-driven drafting, flow separation, cross-cutting concerns and read/command test-plan separation synchronized  
Scope: business slices, cross-cutting/helper slices, client sidecars, scenario source intake, implementation flow, extension/change points, questions, registers and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
planning/slices/draft-driven-discovery-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/change-extension-points-principles.md
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
planning/testing/server-slice-test-plan-rules.md
```

Client sidecar drafters must also read:

```text
planning/client/README.md
planning/client/client-layering-for-read-and-command-slices.md
planning/slices/client-slice-short-draft-rules-and-example.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/api/client-server-contract-principles.md
```

## 2. Example-Driven Drafting Rule

New chats must draft by existing examples.

Do not invent a new form, new section order or new terminology just because it seems nicer.

Consistency is more important than creativity.

## 3. Scenario Source Rule

Scenario text/DATA/UI/behavior files are the source of truth for Scenario Flow and Behavior Coverage.

Domain drafts are domain-design input. They do not replace scenario sources.

Before writing Scenario Flow or Behavior Coverage, read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Then read the linked scenario text, DATA, UI and behavior files.

Scenario Flow is the part of the scenario that belongs to the current slice.

It is not necessarily the whole scenario.

A scenario can be implemented by several slices and extension slices.

## 4. Scenario Flow vs Implementation Flow

Scenario Flow shows user/system behavior from scenario specs.

Implementation Flow shows code/layer responsibilities.

Bad Scenario Flow:

```text
[Controller]
Extracts account id
        ↓
[Repository]
Loads ApplicantParties
        ↓
[React Query]
Caches response
```

Good Scenario Flow:

```text
[Signed-in Employee]
opens employee request list
        ↓
System shows requests visible to Employee
        ↓
Employee can filter list by request status and/or review state
```

## 5. Behavior Items Rule

Behavior items are not implementation details.

Do not treat these as behavior items:

```text
- query key includes filters;
- fetchJson is called;
- generated type exists;
- route param is parsed;
- cache invalidation happens;
- repository method exists;
- Dapper query exists.
```

They may be implementation notes, API contract notes or test plan items.

Behavior items must come from scenario/UI/behavior sources or cross-cutting concern sources.

If source IDs are missing, write `Source BI TBD` and mark it as a source gap.

## 6. Scope / Out-of-Scope / Related-Slices Rule

Every non-trivial slice draft must explicitly define:

```text
Scope
Out of scope
Related slices / owners
Future extension points
```

Out-of-scope items must point to an owner:

```text
- existing slice;
- future slice;
- cleanup task;
- client sidecar;
- cross-cutting/helper slice;
- explicit not planned.
```

## 7. Cross-Cutting Concerns / Considerations Rule

Every non-trivial slice draft must include:

```text
## Cross-Cutting Concerns / Considerations
```

Use:

```text
planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md
```

Concerns include, when applicable:

```text
- auth/session/account context;
- authorization/ownership/visibility;
- antiforgery for browser unsafe requests;
- request validation / ProblemDetails;
- OpenAPI / generated artifacts;
- generated constants / error codes;
- transaction / atomicity;
- no-mutation safety;
- idempotency / retry;
- concurrency / stale state;
- file/document boundary;
- clock/audit actor fields;
- privacy / cross-account or cross-employee exposure;
- testing responsibility split.
```

Cross-cutting concerns should not be forced into Scenario Flow.

Implementation details from concerns are not behavior items unless a cross-cutting source explicitly defines them as concern-derived behavior items.

## 8. Client Sidecar Short Draft Template

Use exactly:

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type:
Architecture direction:
Backend/API contract evidence, when relevant:

## 1. Scope
## 2. Out of Scope
## 3. Related Slices / Owners
## 4. Visual UI / Scenario Flow
## 5. Visual Client Implementation Flow
## 6. Client API / Server Contract
## 7. Questions / Decisions
## 8. Extension / Change Points
## 9. Behavior Coverage
## 10. Cross-Cutting Concerns / Considerations
## 11. Client / Component / E2E Verification Plan
## 12. Implementation Checklist
## 13. Next Step
```

## 9. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft cover source behavior?
```

Test / Verification Plan answers:

```text
How will implementation be verified?
```

Do not mix these tables.

## 10. OpenAPI / Generated Artifact Rule

If a slice changes API contract, generated artifacts must come from repo commands, not manual edits.

Read:

```text
planning/api/generated-artifact-check-workflow.md
planning/api/openapi-contract-generation.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 11. Server Slice Test Plan Rule

Server/backend slice drafts must separate test responsibilities by what they prove.

Read:

```text
planning/testing/server-slice-test-plan-rules.md
```

For read/query slices:

```text
Primary verification: API/read integration tests.
Do not add unit tests by default.
Unit tests are allowed only for reusable helper logic with non-trivial branching.
Endpoint behavior, auth, validation, visibility, filtering and response shape are verified through integration tests.
```

Use read-slice buckets:

```text
### API boundary / access
### Query/route validation, if applicable
### Read/projection correctness
### Filtering, if applicable
### No-mutation safety, optional when cheap/important
### What not to test
```

For state-changing command slices:

```text
### API boundary / access tests
### Main DB state transition tests
### Idempotency / no-op DB state tests
### No-mutation / unrelated-record safety tests
### Same-type / type-scope tests, when applicable
### Regression guards
### What not to test
```

Do not write one flat list when the slice has meaningful boundary/projection/state behavior.

Do not use mocks as primary proof for server behavior that persists state or read behavior that must be proven through API/visibility/projection boundaries.
