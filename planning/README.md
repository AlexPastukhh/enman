# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts, read order, agent roles, documentation governance and slice/client/scenario/diagram planning entry points

## 1. Current Active Planning Focus

```text
Client/server contract artifacts and documentation/status reconciliation:
OpenAPI structural contract + generated semantic constants are first-stage baseline;
next work should avoid stale docs and proceed through draft-driven scenario/domain/slice/client planning.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

Do not write directly to GitHub from documentation-only work unless explicitly requested.

For documentation maintenance, keep local docs synchronized with central navigation, responsibility maps and shared registers.

For scenario drafting, maintain scenario text, DATA, UI specs, behavior items, questions and clarifications together when relevant.

For diagram generation, use repo-grounded preflight first and target draw.io XML diagram-book artifacts.

Full backend slice docs should include diagram-like Visual Scenario Flow and Visual Implementation Flow before detailed flow sections.

Important open questions should appear first in local `Questions / Decisions` sections and should also be mirrored into the relevant shared register when they remain relevant beyond the local file.

Every important question should include status, assumption/current direction and impact.

## 2. Agent Roles

Before specialized work, identify the role and required actions:

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

A future chat should be able to start from this README, read the role map and then follow the relevant workflow docs without a long external prompt.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md
5. planning/agent-roles-and-required-actions.md

6. planning/documentation/README.md
7. planning/documentation/documentation-update-workflow.md
8. planning/documentation/status-reconciliation-workflow.md
9. planning/documentation/local-global-documentation-sync-workflow.md
10. planning/documentation/documentation-update-agent-prompt.md

11. planning/adr/README.md
12. planning/adr/adr-workflow.md
13. planning/adr/architecture-decision-notes.md
14. planning/adr/adr-candidates.md

15. planning/testing/README.md
16. planning/testing/testing-principles.md
17. planning/testing/e2e-testing-workflow.md
18. planning/testing/test-object-patterns.md
19. planning/testing/playwright-e2e-cleanup-plan.md

20. planning/api/README.md
21. planning/api/client-server-contract-principles.md
22. planning/api/api-error-contract.md
23. planning/api/api-error-mapping-boundary.md
24. planning/api/openapi-contract-generation.md
25. planning/api/client-constants-generation.md
26. planning/api/fluentvalidation-error-code-policy-note.md

27. planning/diagrams/README.md
28. planning/diagrams/scenario-drafting-workflow.md
29. planning/diagrams/diagram-prompt-generation-workflow.md
30. planning/diagrams/drawio-diagram-generation-workflow.md
31. planning/diagrams/scenario-text-specs/README.md
32. planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
33. planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
34. planning/diagrams/scenario-data/README.md
35. planning/diagrams/scenario-data/00-scenario-data-index.md
36. planning/diagrams/scenario-ui-specs/README.md
37. planning/diagrams/scenario-behavior-items/README.md
38. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
39. planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
40. planning/diagrams/scenario-clarifications/README.md
41. planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
42. planning/diagrams/scenario-clarifications/diagram-generation-readiness-guardrails.md
43. planning/diagrams/scenario-questions-register.md

44. planning/scenario-specification-principles.md
45. planning/scenario-domain-validation-principles.md
46. planning/client/README.md
47. planning/client/cross-cutting/README.md

48. planning/slices/README.md
49. planning/slices/draft-driven-discovery-principles.md
50. planning/slices/l1-slice-drafting-guide.md
51. planning/slices/slice-questions-register.md
52. planning/slices/slice-extension-points-register.md
53. planning/slices/slice-implementation-notes-register.md
54. planning/slices/SL-ACC-001-register-client-account.md
55. planning/slices/SL-APPL-001-create-individual-applicant-party.md
56. planning/slices/SL-REQ-001-create-connection-request.md
57. planning/slices/examples/README.md
58. planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
59. planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
60. planning/slices/cross-cutting/README.md
61. planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
62. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
63. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
64. planning/slices/shared/README.md
65. planning/slices/shared/antiforgery-token-session-context.md
66. planning/slices/implementation-principles.md
67. planning/slices/client-architecture-principles.md
68. planning/slices/client-component-discovery-guide.md
69. planning/slices/change-extension-points-principles.md
70. planning/replacement-file-generation-guide.md
```

## 4. Documentation Update Direction

Documentation-only updates use:

```text
planning/documentation/
planning/replacement-file-generation-guide.md
```

Rules:

```text
- check current repo state;
- update navigation/responsibility maps;
- synchronize local file changes with shared registers when needed;
- create archive packages for manual application;
- do not write to GitHub directly unless explicitly requested.
```

For local/global synchronization rules, use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

## 5. Scenario Drafting Direction

Scenario drafting uses:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-drafting-workflow.md
```

Scenario draft work should maintain related source artifacts together:

```text
scenario text spec
scenario DATA spec
scenario UI spec, when relevant
validation/security addendum, when relevant
scenario behavior items
scenario questions register
scenario clarifications, when needed
```

A Scenario Draft Chat may prepare a repo-grounded diagram request/prompt when diagrams are requested from scenario sources.

That request is handed to the single Diagram Chat. Scenario Draft Chat does not create diagrams itself.

## 6. Draft-Driven Discovery Direction

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
```

This applies to:

```text
domain drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
testing/support drafts
documentation/status reconciliation drafts
diagram planning drafts
```

Practical slice workflow:

```text
planning/slices/l1-slice-drafting-guide.md
```

Slice examples:

```text
planning/slices/examples/
```

Slice shared registers:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

## 7. Local Questions / Shared Registers Direction

Local `Questions / Decisions` sections are required, but they are not enough.

Use:

```text
planning/slices/slice-questions-register.md
```

as the shared overview of currently relevant slice questions discovered in local slice/client/cross-cutting docs.

Use:

```text
planning/slices/slice-extension-points-register.md
```

for extension/change pressure, anti-coupling decisions and cross-slice questions tied to future slices.

Use:

```text
planning/slices/slice-implementation-notes-register.md
```

for concrete future implementation/client/testing notes that are not yet assigned to an active slice/client sidecar.

Important open questions should appear first locally and should be mirrored to the shared register when they are still relevant after the local draft.

Every important question should carry:

```text
Question status
Assumption / current direction
Impact
Shared register / local-only reason
```

## 8. Client / Server Contract Direction

Primary principles:

```text
planning/api/client-server-contract-principles.md
```

Structural contract:

```text
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

Semantic constants:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Core split:

```text
OpenAPI = endpoints / methods / DTOs / response schemas / statuses.
Generated constants JSON = error codes / ProblemDetails extension names / ServerError fields.
```

## 9. Diagram Generation Direction

Diagram-related planning uses:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Current diagram artifact direction:

```text
- scenario/documentation work may prepare a repo-grounded diagram request/prompt;
- the single Diagram Chat runs preflight before drawing;
- target format is draw.io XML;
- preferred output is one multi-page `.drawio` diagram book;
- diagram generation should not overclaim implementation status;
- final VKR-clean diagrams must not mention AI/internal workflow wording.
```

Required diagram markers:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

## 10. CSRF / Antiforgery Direction

Security requirements:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

Implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

## 11. Current Next Step

```text
1. Keep docs reconciled with current implementation status.
2. Do not redo already implemented OpenAPI/constants/E2E infrastructure.
3. Use scenario drafting workflow for future scenario/DATA/UI/behavior-item work.
4. Use draft-driven discovery for L1 consolidation and client sidecar work.
5. Client work should proceed from confirmed backend-backed slices and generated contract artifacts.
6. Keep local slice questions synchronized with the shared slice question register.
7. For full backend slice docs, keep visual maps before detailed scenario/implementation flows.
8. For diagrams, prepare a diagram request/prompt when useful, then run the single Diagram Chat Phase 1 preflight before generating `.drawio` XML.
```
