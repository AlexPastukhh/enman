# Planning Agent Protocol

Status: current collaboration protocol / server validation rule added

## 1. Core Rule

Do not continue implementation planning through a question that may change required behavior, API contract, validation responsibility, security requirement, client-facing constants, testing responsibility, E2E scope, cross-layer responsibility, scenario meaning or diagram interpretation.

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

If a task crosses role boundaries, stop and write a handoff note instead of silently changing responsibility.

Handoff format:

```text
Boundary reached:
Target role:
Reason:
Current facts:
Open questions:
Assumption / current direction:
Recommended next action:
```

## 3. Documentation Update Agent Rule

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

## 4. Local / Global Sync Rule

Local planning files own detailed context.

Shared indexes/registers own discoverability across time and chats.

When a local planning file introduces or changes a question, decision, future note, extension pressure or status, decide whether a shared file must also be updated.

Use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

Important local slice questions that remain relevant after the local draft must be mirrored into:

```text
planning/slices/slice-questions-register.md
```

Extension/change pressure belongs in:

```text
planning/slices/slice-extension-points-register.md
```

Concrete future implementation/client/testing notes belong in:

```text
planning/slices/slice-implementation-notes-register.md
```

Scenario/domain questions that can change scenario behavior, DATA, UI-visible behavior or diagram semantics belong in:

```text
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-clarifications/, when accepted clarification is needed
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

This applies to:

```text
scenario specs and scenario registers
business slice files
client sidecars
cross-cutting/helper slices
scenario clarifications
status reconciliation docs
ADR candidates
diagram planning docs
```

Accepted decisions should not hide unresolved questions below them.

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

Scenario Draft Chat may prepare a repo-grounded diagram request/prompt when diagrams are requested from scenario sources.

That request is handed to the single Diagram Chat. Scenario Draft Chat does not draw diagrams itself.

## 7. Diagram Workflow Rule

Diagram-related planning must use:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Rules:

```text
- a scenario/documentation/prompt chat prepares a repo-grounded prompt; it does not draw diagrams itself;
- the single Diagram Chat must run Phase 1 preflight before generating diagrams;
- target diagram format is draw.io XML;
- preferred artifact is one multi-page `.drawio` diagram book;
- do not ask for all diagrams at once by default;
- generate only the selected batch after preflight;
- do not overclaim implementation status;
- use status markers: [CORE], [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION];
- inspect scenario text specs, DATA, UI specs, behavior items, API/security addenda, scenario clarifications and current implementation evidence before drawing.
```

Final VKR-clean diagrams must not mention:

```text
AI
ChatGPT
prompt
agent
internal workflow
planning chat
```

## 8. Draft-Driven Discovery Rule

All slice-related planning uses draft-driven discovery:

```text
source requirements
-> draft
-> open questions and assumptions
-> visual flow maps
-> detailed flow
-> behavior coverage
-> validation boundary review
-> implementation direction
-> test / verification planning
-> status reconciliation
-> next draft or implementation step
```

Use:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
```

This applies to:

```text
domain drafts
business slices
client sidecar slices
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
diagram planning drafts
```

## 9. Client / Server Contract Rule

Before planning missing client slices, check:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

The agent must not let client code guess:

```text
routes
request DTOs
response DTOs
status/error shapes
ProblemDetails extension names
error code strings
```

Use generated OpenAPI types for structure and generated constants for semantics.

## 10. Server Request Validation Rule

Before planning or implementing a server/API slice with request body or query validation responsibility, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
planning/api/fluentvalidation-error-code-policy-note.md
```

Slice drafts must distinguish:

```text
FluentValidation request DTO/query validation:
- required fields;
- discriminator/branch rules;
- mutually exclusive fields;
- basic DTO/query shape;
- allowed query parameter values;
- nested DTO presence.

Application/domain validation:
- account existence;
- ownership;
- selected entity belongs to current account;
- domain value object invariants;
- state transitions;
- no-write/atomicity.
```

Backend Visual Implementation Flow should include `[FluentValidation]` before `[Application Handler]` when DTO/request rules exist.

Do not put FluentValidation mechanics into Visual Scenario Flow.

## 11. OpenAPI / Constants Split Rule

```text
OpenAPI = structural contract:
  endpoints, methods, DTOs, response schemas, statuses.

Generated constants JSON = semantic contract:
  error codes, ProblemDetails extension names, ServerError field names,
  temporary legacy route constants.
```

Do not collapse these into one artifact.

## 12. Generated Artifact Rule

Generated artifacts are produced by explicit commands/checks.

Do not write generated client artifacts during normal server startup.

## 13. Endpoint Classification Rule

Before generating client API types/wrappers for a slice, classify endpoint as:

```text
target L1
legacy/current support
temporary compatibility
internal/not client-facing
```

If current endpoint status is unclear, ask or document assumption before implementation.

## 14. Cross-Cutting / Helper Slice Rule

When work is cross-cutting or helper-like, do not bury it only in workflow docs or shared notes.

Create or update a cross-cutting/helper slice if the work has:

```text
- observable/support behavior;
- concrete implementation flow;
- independent tests/checks;
- multiple consumers;
- contract/helper/tooling/security/validation responsibility.
```

Use:

```text
planning/slices/cross-cutting/
```

## 15. Same Format Rule

Cross-cutting/helper slices must follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> visual concern/scenario flow, when useful
-> concern slice flow
-> visual implementation flow
-> implementation flow
-> behavior coverage
-> tests/checks
-> coverage/questions/ADR impact
```

## 16. CSRF / Antiforgery Rule

When planning browser unsafe API request security, use:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 17. Testing Responsibility Rule

When planning slice/client/API work, classify tests as:

```text
Validator/unit tests
Domain unit tests
Server integration/API tests
Client/component tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
```

## 18. Implementation Flow Detail Filter

Slice flow may include involved classes, methods and short code snippets.

Do so only when they clarify behavior, boundary, trade-off, testability/checkability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If class/method details make the flow noisy, suggest a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 19. Do Not

```text
- Do not implement client slices by manually guessing API contract.
- Do not use OpenAPI as replacement for stable semantic error-code constants.
- Do not use generated constants JSON as long-term replacement for OpenAPI structural route/DTO contract.
- Do not write generated artifacts during normal server startup.
- Do not implement cross-cutting security from loose notes only.
- Do not skip behavior items for technical concerns when traceability is needed.
- Do not skip request-level FluentValidation planning for new/changed server API input.
- Do not mix DTO/request validation with domain/application invariants without saying which layer owns what.
- Do not label antiforgery failure by generic HTTP 400.
- Do not blindly replay unsafe requests after token refresh.
- Do not generate diagrams without repo-grounded preflight.
- Do not use PlantUML as the primary diagram deliverable unless explicitly asked.
- Do not overclaim diagram implementation status.
- Do not use GitHub mutation tools during documentation-only archive work unless explicitly requested.
- Do not leave important local questions only in local files when they affect future work.
- Do not hide assumptions in prose.
```
