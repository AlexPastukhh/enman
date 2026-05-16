# Planning Agent Protocol

Status: current collaboration protocol / scope-safe prompt generation

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, security requirement, client-facing constants, testing responsibility, E2E scope, cross-layer responsibility, scenario meaning or diagram interpretation.

If the question is not blocking the current task, record it with a status and assumption/current direction, then sync it to the relevant shared register when it can affect future work.

## 2. Role Identification Rule

Before specialized work, identify the active role using:

```text
planning/agent-roles-and-required-actions.md
```

Core roles:

```text
Documentation Keeper / Status Reconciliation Chat
Scenario Draft Chat
Domain Draft Chat
Slice Draft Chat
Diagram Chat
Architecture / Implementation Handoff Chat
API / Contract Keeper Gate
Testing / E2E Keeper Gate
```

If a task crosses role boundaries, write a handoff note instead of silently changing responsibility.

## 3. Agent Scope Boundary Rule

Before writing a prompt for another chat, read:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

Prompt creators must not grant permission to change docs, domain code or generated artifacts unless the user explicitly requested that scope.

Allowed by default for implementation prompts:

```text
- read planning docs;
- read current code;
- implement only the requested slice/code area;
- report docs drift as a handoff note.
```

Forbidden by default unless explicitly requested:

```text
- change planning docs;
- change Domain.EnergyManagement/**;
- change generated artifacts;
- rewrite scenario sources;
- create branch/commit/PR.
```

## 4. Documentation Update Agent Rule

Documentation-only agents must read:

```text
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/replacement-file-generation-guide.md
```

Documentation-only agents must:

```text
- inspect current repo state before changing docs;
- update navigation/responsibility maps when adding docs;
- synchronize local docs with shared registers when needed;
- create archive packages with full replacement files;
- not change code;
- not write directly to GitHub unless explicitly asked.
```

## 5. Questions First And Assumption Rule

In any `Questions / Decisions` section, list:

```text
1. open questions;
2. blocked questions;
3. assumptions/current directions needing confirmation;
4. unresolved behavior / contract / design risks;
5. accepted decisions;
6. resolved/superseded history only when still useful.
```

Each important question should include:

```text
Question status:
Question:
Assumption / current direction:
Impact:
Shared register / local-only reason:
```

## 6. Scenario Drafting Rule

Scenario drafting uses:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-drafting-workflow.md
```

Scenario Draft Chat must maintain related artifacts together when relevant:

```text
scenario text spec
scenario DATA spec
scenario UI spec
validation/security addendum
scenario behavior items
scenario questions register
scenario clarifications
```

## 7. Draft-Driven Slice Rule

All slice-related planning uses:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
```

Slice drafts must include visual scenario/UI flow, visual implementation flow, behavior coverage and test/verification planning.

## 8. Server Request Validation Rule

When planning or implementing server API input, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
planning/api/fluentvalidation-error-code-policy-note.md
```

Request-level FluentValidation handles DTO/query shape, required fields, discriminator/branch rules and mutually exclusive fields.

Application/domain validation still owns ownership, account existence, state transitions, domain invariants, no-write and atomicity.

## 9. Client / Server Contract Rule

Before planning missing client slices, check:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

The agent must not let client code guess routes, DTOs, statuses, ProblemDetails extension names or error code strings.

## 10. Do Not

```text
- Do not implement client slices by manually guessing API contract.
- Do not use OpenAPI as replacement for stable semantic error-code constants.
- Do not implement cross-cutting security from loose notes only.
- Do not generate diagrams without repo-grounded preflight.
- Do not use PlantUML as the primary diagram deliverable unless explicitly asked.
- Do not use GitHub mutation tools during documentation-only archive work unless explicitly requested.
- Do not leave important local questions only in local files when they affect future work.
- Do not hide assumptions in prose.
- Do not give another chat permission to edit docs/domain/generated artifacts unless the user explicitly asked for that scope.
```
