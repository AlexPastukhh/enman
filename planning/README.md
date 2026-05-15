# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts, read order, documentation governance and slice/client/diagram planning entry points

## 1. Current Active Planning Focus

```text
L1 backend/API/persistence baseline is implemented for:
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request.

Generated OpenAPI TypeScript types and semantic constants support are available.

The next implementation/planning focus is client-side L1 feature work:
- auth/session client baseline;
- registration/login/current-user/logout client integration;
- applicant data form UI;
- request creation form UI;
- My Requests read/list/detail UI;
- client-side ProblemDetails/error mapping and browser E2E after UI exists.
```

Do not create `.client.md` files in advance.

Do not create full numbered ADRs unless explicitly requested.

Do not write directly to GitHub from documentation-only work unless explicitly requested.

For documentation maintenance, keep local docs synchronized with central navigation, responsibility maps and shared registers.

For diagram generation, use repo-grounded preflight first and target draw.io XML diagram-book artifacts.

Full backend slice docs should include diagram-like Visual Scenario Flow and Visual Implementation Flow before detailed flow sections.

Important open questions should appear first in local `Questions / Decisions` sections and should also be mirrored into the relevant shared register when they remain relevant beyond the local file.

## 2. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md

5. planning/documentation/README.md
6. planning/documentation/documentation-update-workflow.md
7. planning/documentation/status-reconciliation-workflow.md
8. planning/documentation/local-global-documentation-sync-workflow.md
9. planning/documentation/documentation-update-agent-prompt.md

10. planning/adr/README.md
11. planning/adr/adr-workflow.md
12. planning/adr/architecture-decision-notes.md
13. planning/adr/adr-candidates.md

14. planning/testing/README.md
15. planning/testing/testing-principles.md
16. planning/testing/e2e-testing-workflow.md
17. planning/testing/test-object-patterns.md
18. planning/testing/playwright-e2e-cleanup-plan.md

19. planning/api/README.md
20. planning/api/client-server-contract-principles.md
21. planning/api/api-error-contract.md
22. planning/api/api-error-mapping-boundary.md
23. planning/api/openapi-contract-generation.md
24. planning/api/client-constants-generation.md
25. planning/api/fluentvalidation-error-code-policy-note.md

26. planning/diagrams/README.md
27. planning/diagrams/diagram-prompt-generation-workflow.md
28. planning/diagrams/drawio-diagram-generation-workflow.md
29. planning/diagrams/scenario-drafting-workflow.md, if exists
30. planning/diagrams/scenario-text-specs/README.md
31. planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
32. planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
33. planning/diagrams/scenario-data/README.md
34. planning/diagrams/scenario-data/00-scenario-data-index.md
35. planning/diagrams/scenario-ui-specs/README.md
36. planning/diagrams/scenario-behavior-items/README.md
37. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
38. planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
39. planning/diagrams/scenario-clarifications/README.md
40. planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
41. planning/diagrams/scenario-clarifications/diagram-generation-readiness-guardrails.md
42. planning/diagrams/scenario-questions-register.md

43. planning/scenario-specification-principles.md
44. planning/scenario-domain-validation-principles.md
45. planning/client/README.md
46. planning/client/cross-cutting/README.md

47. planning/slices/README.md
48. planning/slices/draft-driven-discovery-principles.md
49. planning/slices/l1-slice-drafting-guide.md
50. planning/slices/slice-questions-register.md
51. planning/slices/slice-extension-points-register.md
52. planning/slices/slice-implementation-notes-register.md
53. planning/slices/SL-ACC-001-register-client-account.md
54. planning/slices/SL-AUTH-001-login-client-account.md
55. planning/slices/SL-AUTH-002-current-user.md
56. planning/slices/SL-AUTH-003-logout.md
57. planning/slices/SL-APPL-001-create-individual-applicant-party.md
58. planning/slices/SL-REQ-001-create-connection-request.md
59. planning/slices/examples/README.md
60. planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
61. planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
62. planning/slices/cross-cutting/README.md
63. planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
64. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
65. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
66. planning/slices/shared/README.md
67. planning/slices/shared/antiforgery-token-session-context.md
68. planning/slices/implementation-principles.md
69. planning/slices/client-architecture-principles.md
70. planning/slices/client-component-discovery-guide.md
71. planning/slices/change-extension-points-principles.md
72. planning/replacement-file-generation-guide.md
```

## 3. Documentation Update Direction

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

The goal is that a future chat can start from this README, follow the read order and understand the project workflow without a long external prompt.

## 4. Draft-Driven Discovery Direction

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

## 5. Current L1 Backend / Client State

| Area | Current backend/code state | Client/UI state | Current docs rule |
|---|---|---|---|
| Register client account | `POST /api/l1/auth/register`, DTO `email + password`, response `AccountId + Email`, persisted active ClientAccount | registration UI/password confirmation/auto-login are future client/auth work | Use `SL-ACC-001`; do not treat password confirmation as current backend DTO |
| Login client account | `POST /api/l1/auth/login`, returns current-user shape and issues L1 cookie | concrete client login/session flow is future client work | Use `SL-AUTH-001`; next client baseline should consume generated types |
| Current user | `GET /api/l1/auth/current-user`, protected, L1 marker/account lookup | client bootstrapping/route guard work is future client work | Use `SL-AUTH-002` |
| Logout | `POST /api/l1/auth/logout`, protected, clears cookie and returns 204 | logout UI/session invalidation flow is future client work | Use `SL-AUTH-003`; unsafe browser command should follow CSRF planning later |
| Create individual applicant party | protected `POST /api/l1/applicant-parties/individual`, server-derived account id | applicant form UI/client sidecar is future work | Use `SL-APPL-001` |
| Create connection request | protected `POST /api/l1/requests`, DTO `details + address`, server-selected current active applicant, no required response body | request creation UI, success navigation and My Requests read screens are future work | Use `SL-REQ-001` and `CL-COMMAND-001` |
| Generated contracts | `openapi-types.ts` includes L1 paths/types; client package can generate API types | generated types are support, not completed feature UI | Client slices should consume generated types when concrete work starts |

## 6. Local Questions / Shared Registers Direction

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

## 7. Client / Server Contract Direction

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

## 8. Diagram Generation Direction

Diagram-related planning uses:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Current diagram artifact direction:

```text
- a Scenario Draft Chat or planning chat may prepare a repo-grounded diagram request;
- a single Diagram Chat runs preflight before drawing;
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

## 9. CSRF / Antiforgery Direction

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

Current known status remains planning/future hardening unless repo evidence later shows antiforgery implementation.

## 10. Current Next Step

```text
1. Keep docs reconciled with current implementation status.
2. Do not redo already implemented OpenAPI/constants/E2E infrastructure.
3. Do not redo implemented L1 backend/API/persistence flows.
4. Use draft-driven discovery for remaining L1 client sidecars and read flows.
5. Recommended client order: auth/session baseline -> applicant data UI -> request creation UI -> My Requests read/list/detail -> E2E happy paths.
6. Keep local slice questions synchronized with the shared slice question register.
7. For full backend slice docs, keep visual maps before detailed scenario/implementation flows.
8. For diagrams, run Diagram Chat Phase 1 preflight before generating `.drawio` XML.
```
