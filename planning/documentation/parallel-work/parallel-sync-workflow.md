# Parallel Work Sync Workflow

Status: active reusable workflow
Doc version: v0.1.0
Scope: how to integrate one or more parallel workspaces into canonical documentation through an aggregate sync plan

Use with:

```text
planning/documentation/parallel-work/README.md
planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
included workspace README/base-snapshot/responsibility-map/local-action-log files
```

## 1. Purpose

Use this workflow when one or more parallel workspaces are ready to be reviewed and synchronized into canonical docs.

A sync is not the same as continuing a workspace. It is a coordination step that decides what becomes canonical.

## 2. When To Activate

Activate when:

```text
- at least one workspace is sync-candidate;
- several parallel workspaces need one coordinated merge;
- shared canonical files may have changed since workspace base snapshots;
- main action-log ordering must be established after parallel preparation.
```

## 3. Create Aggregate Sync

Create:

```text
planning/documentation/parallel-work/syncs/<sync-id>/
  README.md
  sync-plan.md
```

Use the exact shape from:

```text
planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
```

One sync may include one workspace or several workspaces.

## 4. Required Review Before Applying

Before changing canonical docs, reread:

```text
- each included workspace README.md;
- each included workspace base-snapshot.md;
- each included workspace responsibility-map.md;
- each included workspace local-action-log.md;
- all target canonical files in their current state.
```

Compare each workspace base snapshot with the current canonical target file. If the target changed, decide whether the workspace change is still valid, needs adaptation or must be rejected.

## 5. Sync Plan Lifecycle

`sync-plan.md` owns the whole sync lifecycle:

```text
planned -> in-progress -> applied / partially-applied / blocked / superseded
```

It should contain:

```text
- included workspaces;
- current canonical files to reread;
- target canonical files;
- planned accepted changes;
- rejected/not-synced changes;
- freshness/conflict checks;
- execution checklist;
- draft main documentation-action-log entry;
- final result after the sync is applied.
```

Do not require a separate `sync-review.md` in v1. If syncs become too large later, split plan/result only after the repeated need is proven.

## 6. Apply Canonical Changes

Apply only accepted changes.

After applying, check:

```text
git diff --check
git diff --stat
git diff --name-only
```

The final changed file list must match the sync-plan result.

## 7. Main Action Log

Append the main `planning/documentation-action-log.md` entry last, after the actual canonical diff is known.

The entry should include an ordering note:

```text
This work was prepared in one or more parallel workspaces.
Preparation time may overlap with other batches.
Canonical ordering starts at this sync entry.
```

Do not copy local action-log entries directly into the main action log.

## 8. Notify Other Agents

The sync result must state which files other active chats should reread.

At minimum:

```text
- changed canonical files;
- changed command/root routing files;
- changed source/register files;
- files intentionally not changed.
```

## 9. Do Not

```text
- Do not sync stale workspace copies without rereading current canonical files.
- Do not sync every workspace change automatically.
- Do not update the main action log before canonical changed files are known.
- Do not treat the sync plan as applied until real files are changed and checked.
- Do not hide rejected workspace changes; record why they were not synced.
```
