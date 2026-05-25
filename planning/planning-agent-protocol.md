# Planning Agent Protocol

Status: current collaboration protocol / GitHub line-link workflow synchronized

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

## 3. Repo-Grounded GitHub Line Link Rule

Before explaining current repo code/docs, implementation status, tests, generated artifacts or a concrete consistency problem, read:

```text
planning/repo-grounded-github-line-links-workflow.md
```

When the user asks for a link, says `дай ссылку`, or when the answer describes a concrete repo fact, provide clickable Markdown GitHub links to exact lines or ranges:

```markdown
[short path, lines 10-25](https://github.com/AlexPastukhh/enman/blob/<commit-sha>/path/to/file.ext#L10-L25)
```

Use commit SHA links when possible. If the current commit SHA is unavailable, use `blob/my-changes` as a fallback and state that the branch link may drift.

Do not use whole-file links for specific implementation/code/test/status claims.

## 4. Agent Scope Boundary Rule

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

## 5. Documentation Update Agent Rule

Documentation-only agents must read:

```text
planning/documentation/README.md
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
```

When archive/replacement output is requested, also read:

```text
planning/replacement-file-generation-guide.md
```

Documentation-only agents must:

```text
- inspect current repo state before changing docs;
- prepare a Documentation Update Plan before broad docs/navigation/status/register changes;
- update navigation/responsibility maps when adding docs;
- synchronize local docs with shared registers when needed;
- choose an explicit output mode: plan-only, direct GitHub edits, archive/replacement package or patch proposal;
- not change code;
- not change generated artifacts;
- not write directly to GitHub unless explicitly asked.
```

Direct GitHub edit mode is allowed only when the user explicitly asks to apply/update/edit repository files. Use small reviewable commits, one file per commit by default.

Archive/replacement package mode is still valid for manual application, archive output, package output or broad changes that should not be applied directly.

## 6. Questions First And Assumption Rule

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

## 7. Scenario Drafting Rule

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

## 8. Draft-Driven Slice Rule

All slice-related planning uses:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
```

Slice drafts must include visual scenario/UI flow, visual implementation flow, behavior coverage and test/verification planning.

## 9. Server Request Validation Rule

When planning or implementing server API input, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
planning/api/fluentvalidation-error-code-policy-note.md
```

Request-level FluentValidation handles DTO/query shape, required fields, discriminator/branch rules and mutually exclusive fields.

Application/domain validation still owns ownership, account existence, state transitions, domain invariants, no-write and atomicity.

## 10. Client / Server Contract Rule

Before planning missing client slices, check:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

The agent must not let client code guess routes, DTOs, statuses, ProblemDetails extension names or error code strings.

## 11. Do Not

```text
- Do not implement client slices by manually guessing API contract.
- Do not use OpenAPI as replacement for stable semantic error-code constants.
- Do not implement cross-cutting security from loose notes only.
- Do not generate diagrams without repo-grounded preflight.
- Do not use PlantUML as the primary diagram deliverable unless explicitly asked.
- Do not use GitHub mutation tools during documentation-only plan/archive work unless direct GitHub edits were explicitly requested.
- Do not leave important local questions only in local files when they affect future work.
- Do not hide assumptions in prose.
- Do not give another chat permission to edit docs/domain/generated artifacts unless the user explicitly asked for that scope.
- Do not provide whole-file GitHub links when the answer needs a concrete line/range.
```
