# Documentation Update Workflow

Status: current documentation-only workflow  
Scope: how to update planning docs accurately without touching code or generated artifacts

## 1. Core Principle

Documentation updates must be repo-grounded, plan-first and scope-controlled.

A documentation update is not a place to implement behavior.

For broad docs/navigation/status/register changes, prepare a `Documentation Update Plan` first:

```text
planning/documentation/documentation-update-plan-workflow.md
```

After the plan is reviewed, use the output mode explicitly requested or approved by the user:

```text
direct repository edits
replacement archive/package
patch proposal only
plan only
```

## 2. Workflow

```text
1. Read central navigation and responsibility docs.
2. Read documentation update, planning architecture and local/global sync workflow docs.
3. Read the current docs for the requested area.
4. Inspect current code/artifacts only enough to avoid stale status.
5. Identify doc drift:
   - implemented but documented as planned;
   - planned but documented as implemented;
   - moved file/path not reflected in navigation;
   - local file carrying global workflow rules;
   - local question not mirrored in shared register;
   - shared register stale compared to local file;
   - missing responsibility owner;
   - docs still assuming archive-only output when direct repository edits are approved.
6. Decide update scope.
7. Prepare a Documentation Update Plan when the change is broad or multi-file.
8. Ask only blocking questions that can change the planned update.
9. After approval, apply the selected output mode.
10. Final response includes changed files or archive link, scope, non-goals, commit SHAs when applicable and next step.
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

## 4. Local / Global Synchronization Check

Before applying or finalizing a documentation update, check whether local changes must be reflected globally.

Use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

Typical sync targets:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

If a local question remains local only, record why.

If a shared register row is stale, update or supersede it.

## 5. Status Labels

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
resolved
superseded
historical/internal
non-canonical
```

Avoid ambiguous status like “done” unless the scope is very small and exact.

## 6. Navigation Update Rule

Whenever adding, moving or superseding planning docs, update relevant navigation:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/planning-agent-protocol.md, if workflow behavior changed
ADR notes/candidates, if accepted architecture decisions changed
```

Do not leave orphan docs.

## 7. Responsibility / Placement Rule

Before placing new information, choose the layer and then use the local responsibility map for that layer.

For root layer routing, use:

```text
planning/planning-doc-responsibility-map.md
```

For documentation-layer placement, use:

```text
planning/documentation/documentation-responsibility-map.md
```

Do not duplicate full placement tables in this workflow. This workflow describes the update process; responsibility maps own placement decisions.

## 8. Question Ordering Rule

In any `Questions / Decisions` section, important open questions and unresolved risks come before accepted decisions.

This applies to:

```text
slice files
client sidecars
cross-cutting/helper slices
scenario clarifications
status reconciliation docs
ADR candidate notes
documentation update plans
```

Accepted decisions are still recorded, but they must not hide unresolved questions below them.

## 9. Blocking Questions Rule

Ask only questions that can change this documentation update.

For every question include an assumption.

Non-blocking questions should be recorded as future review items or mirrored into the relevant register instead of stopping the update.

## 10. Output Mode Rule

Default behavior is plan-first.

After the plan is reviewed, use the output mode requested or approved by the user.

### Direct repository edit mode

Use direct repository edits only when the user explicitly asks to apply changes to the repository.

Rules:

```text
- keep the approved scope;
- use specific commit messages;
- do not combine unrelated documentation refactors;
- do not change code or generated artifacts unless explicitly in scope;
- report changed files and commit SHAs after applying;
- choose commit mode before writing.
```

Commit mode rule:

```text
Use one file per commit by default for semantic documentation edits where each file has its own reviewable meaning.

Use one bundled/bulk commit for mechanical multi-file link/path/name synchronization or shallow navigation routing when:
- every edited file participates in the same logical sync;
- the change is shallow and does not require independent semantic review per file;
- no unrelated semantic refactors are mixed in;
- the user explicitly approves bundled commits or bulk update mode;
- the final response lists all changed files and the shared commit SHA.

If the current tool mode cannot create one bundled commit, stop before writing and disclose the limitation.
Offer either:
- continue with per-file commits;
- create a replacement/archive package for manual one-commit application;
- use a bulk-capable Git tree/commit workflow if available.
```

### Archive / replacement package mode

Use archive mode when direct repo edits are not requested or when a broad generated package is easier to review manually.

Rules:

```text
- include complete replacement/add files;
- include MANIFEST.md and APPLY.md;
- do not include code changes in documentation-only archives;
- follow planning/replacement-file-generation-guide.md.
```

## 11. Documentation Quality Checklist

Before finalizing a documentation update, verify:

```text
- broad changes had a Documentation Update Plan;
- every added file appears in navigation or a folder README;
- responsibility maps know the new responsibility;
- local questions that matter later are mirrored into shared registers;
- shared register rows are not stale compared to local docs;
- docs do not conflict with current repo status;
- old paths/names are not accidentally reintroduced;
- future questions are not presented as current defects;
- planned features are not overclaimed as implemented;
- selected output mode is explicit;
- direct repository edits use one file per commit for independent semantic edits and bundled/bulk commit for approved shallow mechanical multi-file sync;
- archive mode contains complete files, not patches;
- APPLY.md and MANIFEST.md are present for archive mode;
- no code/generated changes are included unless explicitly in scope.
```

## 12. Do Not

```text
- Do not write docs from memory only.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not create full numbered ADRs unless explicitly requested.
- Do not mix OpenAPI/client implementation with documentation-only updates.
- Do not update the repository directly unless explicitly requested.
- Do not hide uncertainty; record assumptions and questions.
- Do not leave important local slice questions only in local tables.
- Do not duplicate full responsibility/placement maps inside workflow files.
- Do not introduce master-chat, work-register or mandatory status-packet workflow unless explicitly requested.
- Do not make replacement archives mandatory for every documentation update.
- Do not bundle unrelated semantic changes into one mechanical link-sync commit.
- Do not start per-file commits for an approved mechanical multi-file sync without first checking whether bundled/bulk commit mode is available.
```
