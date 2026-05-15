# Documentation Update Workflow

Status: current documentation-only workflow  
Scope: how to update planning docs accurately without touching code or GitHub

## 1. Core Principle

Documentation updates must be repo-grounded and scope-controlled.

A documentation update is not a place to implement behavior.

## 2. Workflow

```text
1. Read central navigation and responsibility docs.
2. Read the current docs for the requested area.
3. Inspect current code/artifacts only enough to avoid stale status.
4. Identify doc drift:
   - implemented but documented as planned;
   - planned but documented as implemented;
   - moved file/path not reflected in navigation;
   - local file carrying global workflow rules;
   - missing responsibility owner.
5. Decide update scope.
6. Ask only blocking questions that can change archive contents.
7. If no blocking questions, create complete replacement/add files.
8. Include MANIFEST.md and APPLY.md.
9. Final response includes archive link, scope, non-goals and next step.
```

## 3. Required Current-State Check

Before changing docs, check whether the relevant implementation/status changed.

Examples:

```text
- If updating CC-API docs, check generated OpenAPI command, Shared/openapi.json and generated openapi-types.ts.
- If updating CC-CONST docs, check Tools generator/checker and Shared/constants.json / Shared/errorcodes.json.
- If updating E2E docs, check playwright.config.ts, tests/e2e and package scripts.
- If updating L1 slice docs, check L1 controller, DTOs, commands, handlers and integration tests.
```

Do not trust old archive assumptions when current repo has changed.

## 4. Status Labels

Use clear status language:

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

Avoid ambiguous status like “done” unless the scope is very small and exact.

## 5. Navigation Update Rule

Whenever adding, moving or superseding planning docs, update relevant navigation:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/planning-agent-protocol.md, if workflow behavior changed
ADR notes/candidates, if accepted architecture decisions changed
```

Do not leave orphan docs.

## 6. Responsibility Rule

Before placing content, use:

```text
planning/planning-doc-responsibility-map.md
```

Heuristic:

```text
global workflow rule -> central workflow/protocol docs
documentation update process -> planning/documentation/
archive packaging rules -> replacement-file-generation-guide.md
API contract -> planning/api/
cross-cutting implementation flow -> planning/slices/cross-cutting/
slice implementation -> slice file / .client.md
client architecture convention -> planning/client/ or client architecture docs
testing/E2E rules -> planning/testing/
scenario meaning -> planning/diagrams/scenario-text-specs or scenario-clarifications
behavior items -> scenario-behavior-items
accepted decisions -> planning/adr/architecture-decision-notes.md
future full ADR backlog -> planning/adr/adr-candidates.md
```

## 7. Blocking Questions Rule

Ask only questions that can change this archive.

For every question include an assumption.

Example:

```text
Question:
Should CC-API status be first-stage implemented or still planned?

Assumption:
first-stage implemented, because generate-openapi, Shared/openapi.json and generated openapi-types.ts exist.
```

Non-blocking questions should be recorded as future review items instead of stopping the update.

## 8. Archive-Only Rule

Default output is an archive.

Do not use GitHub mutation tools such as creating/updating files, branches, commits, PRs or comments unless the user explicitly asks for direct GitHub changes.

## 9. Documentation Quality Checklist

Before finalizing archive, verify:

```text
- every added file appears in navigation or a folder README;
- responsibility map knows the new responsibility;
- docs do not conflict with current repo status;
- old paths/names are not accidentally reintroduced;
- future questions are not presented as current defects;
- planned features are not overclaimed as implemented;
- archive contains complete files, not patches;
- APPLY.md and MANIFEST.md are present;
- no code changes are included in documentation-only archive.
```

## 10. Do Not

```text
- Do not write docs from memory only.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not create full numbered ADRs unless explicitly requested.
- Do not mix OpenAPI/client implementation with documentation-only updates.
- Do not update GitHub directly unless explicitly requested.
- Do not hide uncertainty; record assumptions and questions.
```
