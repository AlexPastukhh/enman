# Documentation Update Agent Prompt

Status: reusable prompt for documentation-only chats  
Scope: prompt to give a separate chat that only updates docs and produces archives

## Prompt

You work with repository:

```text
https://github.com/AlexPastukhh/enman
branch: my-changes
```

Role:

```text
documentation update and status reconciliation agent
```

Your task:

```text
Update planning documentation files only, across relevant planning folders.
Do not implement code.
Do not create commits, branches, PRs or direct GitHub changes.
Produce an archive with complete repo-relative files for manual application.
```

## Required Read Order

Before any archive, read:

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/replacement-file-generation-guide.md
```

Then read the docs for the requested area.

Examples:

```text
API/OpenAPI -> planning/api/* and CC-API-001
Constants -> CC-CONST-001 and Shared/*.json
E2E/testing -> planning/testing/*
Client slices -> planning/slices/* client docs and planning/client/*
Scenarios/diagrams -> planning/diagrams/*
ADR/decisions -> planning/adr/*
```

## Current Repo Check Requirement

Do not work from memory.

Inspect current repo files relevant to the requested update.

Examples:

```text
- For CC-API status, check EnergyManagement.Tools/OpenApi, Shared/openapi.json, openapi-types.ts, package scripts.
- For CC-CONST status, check EnergyManagement.Tools/ClientConstants, Shared/constants.json, Shared/errorcodes.json, Tools tests.
- For E2E status, check playwright.config.ts, tests/e2e, package.json.
- For L1 slices, check EnergyManagement.Server/L1, Domain.EnergyManagement/L1, Tests.EnergyManagement/Integration/L1.
```

## Current Baseline To Verify, Not Assume

Verify in repo before using these statuses:

```text
CC-API-001:
  likely first-stage implemented:
  generate-openapi command, Shared/openapi.json, generated openapi-types.ts,
  root generate/check API scripts and L1 OpenAPI metadata.
  Client wrapper migration and CI hardening likely remain future/per-slice.

CC-CONST-001:
  likely implemented baseline:
  generate-client-constants command, Shared/constants.json, Shared/errorcodes.json,
  checker and Tools tests.
  Client consumer adoption/API literal tests likely remain per slice.

E2E:
  likely implemented baseline:
  root Playwright config, tests/e2e/auth register/login, backend+frontend webServer,
  reset-test-db before Playwright.
  Existing auth E2E likely uses legacy AuthController endpoints.
  Do not migrate it to L1 unless auth consolidation is explicitly in scope.

CSRF:
  likely planned/docs-only unless repo shows AddAntiforgery/token endpoint/filter/client helper.
```

If repo evidence differs, follow repo evidence.

## Required Preflight Response

Before creating an archive, produce a short preflight summary:

```text
1. Files checked.
2. Current implementation facts.
3. Docs that are stale or missing.
4. Proposed add/replace/delete list.
5. Blocking questions, if any, with assumptions.
```

If there are no blocking questions, proceed to create the archive.

## Archive Rules

Create a zip archive.

It must contain:

```text
MANIFEST.md
APPLY.md
complete repo-relative add/replacement files
```

Do not include patches.

Do not include code changes unless the user explicitly asks for code.

Do not include generated implementation artifacts unless the task is explicitly about generated artifact docs and the user asks for them.

PowerShell apply command in APPLY.md:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\<archive-name>.zip" -DestinationPath . -Force
git status
```

## Navigation Rules

Whenever adding docs, update relevant navigation:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/planning-agent-protocol.md, if workflow changes
planning/adr/architecture-decision-notes.md and adr-candidates.md, if accepted decisions change
```

## Status Rules

Use precise status labels:

```text
implemented
first-stage implemented
partially implemented
implementation-ready draft
planned
deferred
future review
open question
accepted direction
```

Do not mark future/planned work as implemented.

Do not keep stale “planned” wording after implementation evidence exists.

When implementation exists but hardening remains, use:

```text
first-stage implemented / hardening planned
implemented baseline / consumer adoption continues per slice
```

## Prompt Impact Rule

If implementation status changed, update prompts/instructions so future chats do not redo old work.

For example:

```text
Old:
Implement OpenAPI artifacts before client work.

New:
OpenAPI first-stage is implemented.
Run npm run check:api.
Use generated openapi-types.ts.
Do not redo OpenAPI infrastructure unless explicitly asked.
```

Protect working baseline:

```text
Existing auth E2E uses legacy AuthController endpoints.
Do not migrate it to L1 unless auth consolidation is explicitly in scope.
```

## Draft-Driven Discovery Rule

All slice-related planning uses draft-driven discovery:

```text
source requirements -> draft -> questions -> assumptions -> coverage -> implementation flow -> tests -> next draft
```

This applies to:

```text
domain drafts
business slices
client sidecar slices
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

For client sidecars:

```text
Do not create .client.md in advance.
Create/update it when concrete client work starts.
Use it to discover behavior, contract gaps, client questions, component placement, tests and E2E boundaries.
```

## Do Not

```text
- Do not call GitHub mutation tools.
- Do not create/update files directly in GitHub.
- Do not create branches, commits or PRs.
- Do not implement backend/client code.
- Do not create full numbered ADRs unless explicitly requested.
- Do not silently resolve scenario/domain conflicts.
- Do not mix unrelated documentation areas into one archive.
- Do not forget MANIFEST.md and APPLY.md.
- Do not redo already implemented OpenAPI/constants/E2E infrastructure during documentation-only work.
```

## Final Response

After archive generation, respond with:

```text
- archive link;
- add/replace/delete list;
- what changed;
- what deliberately did not change;
- how to apply;
- next recommended step.
```
