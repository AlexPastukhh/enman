# Parallel Sync Plan Template

Status: active reusable template
Doc version: v0.1.0
Scope: exact shape for one aggregate sync plan that may include one or more parallel workspaces

Use with:

```text
planning/documentation/parallel-work/parallel-sync-workflow.md
```

## 1. Folder Shape

```text
planning/documentation/parallel-work/syncs/<sync-id>/
  README.md
  sync-plan.md
```

## 2. README.md Template

```markdown
# <Sync Name>

Status: planned / in-progress / applied / partially-applied / blocked / superseded
Sync ID: <YYYY-MM-DD-short-scope>
Scope: <one sentence>

## Included Workspaces

- planning/documentation/parallel-work/workspaces/<workspace-id>/

## Target Canonical Scope

- <repo-relative path>

## Current Status

<Short current status.>
```

## 3. sync-plan.md Template

```markdown
# Parallel Work Sync Plan

Status: planned / in-progress / applied / partially-applied / blocked / superseded
Sync ID: <sync-id>
Scope: <one sentence>

## 1. Purpose

<What this sync should integrate into canonical docs.>

## 2. Included Workspace Inputs

| Workspace | Status | Base snapshot | Local log | Notes |
|---|---|---|---|---|
| <workspace path> | <status> | <base summary> | <local log summary> | <notes> |

## 3. Canonical Files To Reread Before Sync

- <repo-relative path>

## 4. Planned Accepted Changes

| Target canonical file | Workspace source | Planned change | Reason | Conflict risk |
|---|---|---|---|---|
| <path> | <workspace/copies/path> | <change> | <why> | low/medium/high |

## 5. Rejected / Not Synced Workspace Changes

| Workspace/source | Decision | Reason |
|---|---|---|
| <path> | reject / defer / supersede | <why> |

## 6. Freshness / Conflict Checks

- [ ] Current canonical files reread.
- [ ] Base snapshots compared against current canonical files.
- [ ] Stale copies adapted or rejected.
- [ ] Expected changed files listed.
- [ ] Main action-log entry will be appended last.

## 7. Execution Checklist

- [ ] Apply accepted canonical changes.
- [ ] Run diff/check commands.
- [ ] Verify changed files match this sync plan.
- [ ] Append main documentation action-log entry.
- [ ] Record final result below.

## 8. Draft Main Action-Log Entry

```text
### <date> - Synced parallel workspace changes

Type:
  parallel-agent sync / <topic>

Ordering note:
  This work was prepared in one or more parallel workspaces.
  Preparation time may overlap with other batches.
  Canonical ordering starts at this sync entry.
```

Fill final changed files only after the real diff is known.

## 9. Result

Status:
  applied / partially-applied / blocked / superseded

Actual changed files:
  - <path>

Not synced:
  - <path/reason>

Other agents must reread:
  - <path>

Follow-ups:
  - <item>
```

## 4. Do Not

```text
- Do not create a sync plan until at least one workspace is sync-candidate.
- Do not use sync-plan.md as proof that canonical files changed before the sync is applied.
- Do not omit rejected/not-synced workspace changes.
- Do not append main action log before actual changed files are known.
```
