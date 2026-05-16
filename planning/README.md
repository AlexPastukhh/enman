# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts, read order, documentation governance and slice/client/diagram planning entry points

## 1. Current Active Planning Focus

```text
L1 backend/API/persistence/session baseline is implemented for:
- register client account;
- login client account;
- current user;
- logout;
- create individual applicant party;
- create connection request.
- My Requests list read.

Generated OpenAPI TypeScript types and semantic constants support are available.

First-stage L1 client implementation now exists for:
- app shell/routing/providers;
- shared typed L1 API wrappers;
- shared fetch/ProblemDetails/form error mapping;
- current-user session bootstrap;
- registration UI;
- login UI;
- Account page applicant create UI.
- logout UI/cache/navigation flow.

Remaining L1 client/read work:
- request creation form UI;
- My Requests client UI;
- own request details read/client flow;
- client/component tests for implemented client flows;
- browser E2E happy paths after UI/read flows are stable.
```

Do not create `.client.md` files in advance.

Create/update `.client.md` files when concrete client work starts or when implemented client logic must be documented and reconciled.

Do not create full numbered ADRs unless explicitly requested.

Do not write directly to GitHub from documentation-only work unless explicitly requested.

For documentation maintenance, keep local docs synchronized with central navigation, responsibility maps and shared registers.

For diagram generation, use repo-grounded preflight first and target draw.io XML diagram-book artifacts.

Full backend slice docs should include diagram-like Visual Scenario Flow and Visual Implementation Flow before detailed flow sections.

Full client sidecar docs should include architecture/folder-based Visual Client Implementation Flow before detailed client implementation flow.

Shortened slice drafts should capture extension/change points when future behavior affects current design.

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
50. planning/slices/change-extension-points-principles.md
51. planning/slices/slice-questions-register.md
52. planning/slices/slice-extension-points-register.md
53. planning/slices/slice-implementation-notes-register.md
54. planning/slices/SL-ACC-001-register-client-account.md
55. planning/slices/SL-ACC-001-register-client-account.client.md
56. planning/slices/SL-AUTH-001-login-client-account.md
57. planning/slices/SL-AUTH-001-login-client-account.client.md
58. planning/slices/SL-AUTH-002-current-user.md
59. planning/slices/SL-AUTH-002-current-user.client.md
60. planning/slices/SL-AUTH-003-logout.md
61. planning/slices/SL-APPL-001-create-individual-applicant-party.md
62. planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
63. planning/slices/SL-REQ-001-create-connection-request.md
64. planning/slices/SL-REQ-002-my-requests-list.md
65. planning/slices/examples/README.md
66. planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
67. planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md
68. planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md
69. planning/slices/cross-cutting/README.md
70. planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
71. planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
72. planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
73. planning/slices/shared/README.md
74. planning/slices/shared/antiforgery-token-session-context.md
75. planning/slices/shared/maybe-for-optional-results.md
76. planning/slices/implementation-principles.md
77. planning/slices/client-architecture-principles.md
78. planning/slices/client-component-discovery-guide.md
79. planning/slices/change-extension-points-principles.md
80. planning/replacement-file-generation-guide.md
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

Extension/change point workflow:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

Slice examples:

```text
planning/slices/examples/
```

Primary shortened example:

```text
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
```

Use it for short drafts with assumptions, extension/change points and shared register sync.

Slice shared registers:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

## 5. Current L1 Backend / Client State

| Area | Current backend/code state | Client/UI state | Current docs rule |
|---|---|---|---|
| Register client account | `POST /api/l1/auth/register`, DTO `email + password`, response `AccountId + Email`, persisted active ClientAccount | first-stage `/register` UI implemented: email/password/confirmation, API submit, ProblemDetails mapping, success -> `/login` | Use `SL-ACC-001` and `SL-ACC-001.client`; password confirmation is client-only unless backend contract changes |
| Login client account | `POST /api/l1/auth/login`, returns current-user shape and issues L1 cookie | first-stage `/login` UI implemented: validation, API submit, session query invalidation, success -> home | Use `SL-AUTH-001` and `SL-AUTH-001.client` |
| Current user | `GET /api/l1/auth/current-user`, protected, L1 marker/account lookup | first-stage session bootstrap implemented: `SessionProvider`, `useSessionQuery`, 401 -> null session | Use `SL-AUTH-002` and `SL-AUTH-002.client`; protected-route policy remains open |
| Logout | `POST /api/l1/auth/logout`, protected, clears cookie and returns 204 | authenticated header logout action implemented; clears session state, removes known applicant-party read query, navigates Home, shows visible failure feedback | Use `SL-AUTH-003` and `SL-AUTH-003-logout.client.md`; CSRF remains deferred |
| Create individual applicant party | protected `POST /api/l1/applicant-parties/individual`, server-derived account id | first-stage Account page applicant form implemented; success invalidates/refetches current applicant read state | Use `SL-APPL-001` and `SL-APPL-001.client` |
| Current applicant party read | protected `GET /api/l1/applicant-parties/current-individual`, returns `exists` + applicant data/null, includes `verificationStatus` | Account page reads current applicant state on open; shows read-only applicant data when present and create form when missing | Use `L1-APPLICANT-PARTY-READ-CURRENT` and `.client` sidecar |
| Create connection request | protected `POST /api/l1/requests`, DTO `details + address`, server-selected current active applicant, no required response body | request creation UI, My Requests read screens and E2E are future work | Use `SL-REQ-001` and `CL-COMMAND-001`; do not create request `.client.md` until work starts |
| My Requests list | protected `GET /api/l1/requests`, optional `status`, current-account summaries, newest first, empty `[]` | My Requests UI remains future work | Use `SL-REQ-002`; details remain future `SL-REQ-003` |
| Generated contracts | `openapi-types.ts` includes L1 paths/types; client package can generate API types | generated types are used by shared API wrappers/support and must be consumed by client sidecars | Client slices should consume generated types and generated constants |

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
5. Use the read-current early short example when drafting the next Account page read slice.
6. Keep local slice questions synchronized with the shared slice question register.
7. Keep extension/change pressure synchronized with the extension register.
8. For full backend slice docs, keep visual maps before detailed scenario/implementation flows.
9. For diagrams, run Diagram Chat Phase 1 preflight before generating `.drawio` XML.
```
