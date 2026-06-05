# Parallel Work Documentation Index

Status: active reusable documentation-layer parallel-work index
Doc version: v0.1.0
Scope: reusable workflow and templates for parallel-agent staging workspaces and aggregate sync plans

## 1. Purpose

This folder defines how documentation agents should work when several chats may progress in parallel and shared canonical documentation files would otherwise conflict.

The model is:

```text
canonical docs
  remain source of truth

parallel workspaces
  staging-only local work areas for one agent/workstream

aggregate sync plans
  reviewed integration steps that can merge one or more workspaces into canonical docs
```

This folder is reusable documentation-layer architecture. It is not an Enman slice/source register and it is not a concrete project workspace by itself.

## 2. Files

```text
planning/documentation/parallel-work/README.md
planning/documentation/parallel-work/parallel-workflow.md
planning/documentation/parallel-work/parallel-sync-workflow.md
planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
```

## 3. Runtime Areas

Concrete workspaces are created only when a parallel-agent task is started:

```text
planning/documentation/parallel-work/workspaces/<workspace-id>/
```

Aggregate sync plans are created only when one or more workspaces become sync-candidate:

```text
planning/documentation/parallel-work/syncs/<sync-id>/
```

Do not create placeholder workspace or sync folders only to show future intent.

## 4. Read Order

For creating or continuing one parallel workspace:

```text
1. planning/documentation/parallel-work/README.md
2. planning/documentation/parallel-work/parallel-workflow.md
3. planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
4. target canonical files listed by the workspace scope
```

For syncing one or more workspaces into canonical docs:

```text
1. planning/documentation/parallel-work/README.md
2. planning/documentation/parallel-work/parallel-sync-workflow.md
3. planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
4. included workspace README/base-snapshot/responsibility-map/local-action-log files
5. current canonical target files
```

## 5. Key Boundary

A workspace may contain shadow copies and local decisions, but it does not override canonical documentation.

Canonical updates happen only when a sync plan is reviewed and applied.

Main project action logs should record the sync, not every local workspace step.

## 6. Do Not

```text
- Do not treat files under workspaces/ as canonical docs.
- Do not put sync-plan.md inside every workspace by default.
- Do not create sync-review.md in v1; sync-plan.md owns plan, checklist and result.
- Do not append the main documentation action log during local workspace steps.
- Do not edit shared canonical files directly from a parallel workspace when other agents may race the same files.
```
