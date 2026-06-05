# Parallel Work Goal Map

Status: chat-local living Goal Map synced to repo / active planning workstream
Doc version: v0.1.0
Scope: reusable documentation-layer parallel-agent workspace architecture, aggregate sync planning and safe parallel work boundaries

## 0. Source Sync / Map Origin

```text
Sources:
  Conversation/local decisions:
    - Accepted chat-local Parallel Work Goal Map
    - User decision: parallel work architecture belongs in the documentation layer
    - User decision: workspace includes README.md, base-snapshot.md, responsibility-map.md, local-action-log.md and copies/
    - User decision: aggregate sync-plan.md belongs under syncs/<sync-id>/ and may include multiple workspaces
  Repo/archive snapshot:
    - enman-my-changes - 2026-06-05T103955.434.zip
  Related canonical docs to update in PAR-WORK-1:
    - planning/documentation/planning-docs-architecture-principles.md
    - planning/documentation/documentation-responsibility-map.md
    - planning/documentation/README.md
    - planning/planning-use-case-map.md
    - planning/workflow-activation-map.md
    - planning/planning-doc-responsibility-map.md
    - planning/documentation-action-log.md
  Not checked:
    - remote changes after archive snapshot
    - actual PAR-WORK-1 file contents, because this map-sync batch does not implement the workflow yet
```

This file records the current target-state map only. It does not create the parallel-work workflow, workspace templates or sync templates.

## 1. Goal

Create reusable documentation-layer architecture for safe parallel agent work.

Parallel agents should be able to work in isolated staging workspaces without directly editing shared canonical documentation files. Canonical docs are updated later through explicit aggregate sync plans.

## 2. Current Strategy

```text
Canonical docs:
  remain source of truth.

Parallel workspace:
  staging-only local work area for one agent/workstream.

Aggregate sync:
  reviewed integration step that can merge one or more finished workspaces into canonical docs.
```

The architecture should live in the active reusable documentation layer:

```text
planning/documentation/parallel-work/
```

not in a root-level `planning/parallel-work/` folder.

## 3. Roadmap

| Slice | Goal | Status | Next action |
|---|---|---|---|
| PAR-WORK-1 — parallel workspace architecture | Add reusable docs-layer workflow/templates for parallel agent staging and aggregate sync plans. | ▶ NOW / planning | Create narrow docs architecture batch. |
| PAR-WORK-2 — first real parallel workspace | Create the first concrete staging workspace after the architecture exists. | ⬜ PLANNED | Wait for PAR-WORK-1. |
| PAR-SYNC-1 — first aggregate sync-plan | Create one sync plan that can integrate one or more parallel workspaces. | ⬜ PLANNED | Wait for at least one sync-candidate workspace. |
| SLICE-TEST-SRC-0 — slice/testing source-register skeletons | Continue slice/testing source-register work in other chats without racing shared docs. | parallel / other chats | Keep separate from PAR-WORK-1. |
| Command expansion | Expand command system/Tampermonkey behavior. | ⬜ DEFERRED | Do not start in PAR-WORK-1. |

## 4. Current Slice

### PAR-WORK-1 — parallel workspace architecture

Status:
  ▶ NOW / planning

Why now:
  Parallel command/docs chats and slice/source chats can conflict on shared root docs and action logs. The project needs a staging model before real parallel command work starts.

Target owner area:

```text
planning/documentation/parallel-work/
```

Planned new reusable files:

```text
planning/documentation/parallel-work/README.md
planning/documentation/parallel-work/parallel-workflow.md
planning/documentation/parallel-work/parallel-sync-workflow.md
planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
```

Planned canonical integration files:

```text
planning/documentation/README.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/planning-docs-architecture-principles.md
planning/planning-use-case-map.md
planning/workflow-activation-map.md
planning/planning-doc-responsibility-map.md
planning/documentation-action-log.md
```

Maybe, only with narrow register scope:

```text
planning/root-source-sync-register.md
```

## 5. Accepted Workspace Shape

Each concrete workspace should represent one parallel agent/workstream.

Required workspace files/folders:

```text
workspaces/<workspace-id>/
  README.md
  base-snapshot.md
  responsibility-map.md
  local-action-log.md
  copies/
  notes/
```

Meaning:

```text
README.md
  Workspace purpose, scope, status, owner/chat, in-scope/out-of-scope boundaries.

base-snapshot.md
  Archive/commit/date and Doc versions used when creating shadow copies.
  Required so sync can detect stale copies.

responsibility-map.md
  Workspace-local ownership map and canonical owners affected later.

local-action-log.md
  Parallel-agent local history only.
  Does not establish canonical ordering.

copies/
  Shadow copies and proposed changes.
  Not canonical.

notes/
  Decisions, risks, rejected options and unresolved local notes.
```

## 6. Accepted Aggregate Sync Shape

Sync plans should not be mandatory inside each workspace.

A sync is a separate aggregate operation that may include multiple parallel workspaces.

Target sync area:

```text
planning/documentation/parallel-work/syncs/<sync-id>/
  README.md
  sync-plan.md
```

`sync-plan.md` is the lifecycle file for the sync. It should cover:

```text
- included workspaces;
- current canonical files to reread;
- target canonical files;
- planned accepted changes;
- rejected/not-synced workspace changes;
- freshness/conflict checks;
- execution checklist;
- draft main documentation-action-log entry;
- final result after the sync is applied.
```

No separate `sync-review.md` is required in v1. If later syncs become too large, the model may split `sync-plan.md` into plan/result files.

## 7. Action Log / Ordering Rule

Main `planning/documentation-action-log.md` should not receive entries for every local parallel workspace step.

Canonical action-log entries should be added only when an aggregate sync is applied.

The entry should state:

```text
Type:
  parallel-agent sync / documentation architecture

Ordering note:
  This work was prepared in one or more parallel workspaces.
  Preparation time may overlap with other batches.
  Canonical ordering starts at this sync entry.
```

Local workspace history stays in:

```text
workspaces/<workspace-id>/local-action-log.md
```

## 8. Do Not Do In PAR-WORK-1

```text
- Do not create a real parallel workspace yet.
- Do not create an imaginary sync-plan.md yet.
- Do not add standalone proposed-main-action-log-entry.md.
- Do not start command expansion.
- Do not edit slice/testing/domain files.
- Do not update slice/testing source registers.
- Do not treat workspace shadow copies as canonical docs.
```

## 9. Goal Map Brief Target State

Goal:
  Create reusable docs-layer architecture for safe parallel agent work.

Current slice:
  PAR-WORK-1 — parallel workspace architecture
  Status: NOW / planning

Current slice chain:

Done:
  - identified shared root/action-log conflict risk;
  - accepted documentation-layer owner path;
  - accepted required workspace files;
  - accepted aggregate syncs under `syncs/<sync-id>/`;
  - removed v1 need for standalone `sync-review.md` and `proposed-main-action-log-entry.md`.

Now:
  - implement reusable workflow/template docs in `planning/documentation/parallel-work/`;
  - update architecture principles and responsibility routing;
  - add root use-case/activation routing;
  - record map sync and later PAR-WORK-1 in action log.

Next:
  - create PAR-WORK-1 replacement archive when requested;
  - after PAR-WORK-1, create first real parallel workspace only if needed.

After:
  - create first aggregate sync-plan when one or more workspaces are sync-candidate.

Other slices:

| Slice | Status |
|---|---|
| CASCADE-SRC-1A — targeted source-impact foundation | DONE |
| CASCADE-ROUTE-1B — root routing cleanup | DONE |
| SLICE-TEST-SRC-0 — slice/testing source-register skeletons | parallel / other chats |
| Command expansion | DEFERRED |
| PAR-WORK-2 — first real parallel workspace | PLANNED after PAR-WORK-1 |
| PAR-SYNC-1 — first aggregate sync-plan | PLANNED after workspace candidate exists |

## 10. Next Action

Prepare PAR-WORK-1 only after user asks for an archive or file update.

PAR-WORK-1 should add the reusable docs-layer architecture and should not create a real workspace yet.
