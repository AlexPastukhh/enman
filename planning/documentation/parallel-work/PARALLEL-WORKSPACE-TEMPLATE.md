# Parallel Workspace Template

Status: active reusable template
Doc version: v0.1.0
Scope: exact shape for one staging-only parallel-agent workspace

Use with:

```text
planning/documentation/parallel-work/parallel-workflow.md
```

## 1. Folder Shape

```text
planning/documentation/parallel-work/workspaces/<workspace-id>/
  README.md
  base-snapshot.md
  responsibility-map.md
  local-action-log.md
  copies/
  notes/
```

## 2. README.md Template

```markdown
# <Workspace Name>

Status: local / active / paused / sync-candidate / superseded
Workspace ID: <YYYY-MM-DD-short-scope>
Owner/chat: <agent/chat description>
Scope: <one sentence>

## 1. Purpose

<Why this workspace exists.>

## 2. In Scope

- <item>

## 3. Out Of Scope

- <item>

## 4. Canonical Files Potentially Affected Later

- <repo-relative path>

## 5. Workspace Files

- base-snapshot.md
- responsibility-map.md
- local-action-log.md
- copies/
- notes/

## 6. Sync Readiness

Status: not-ready / sync-candidate / synced / rejected

Reason:
  <why>
```

## 3. base-snapshot.md Template

```markdown
# Base Snapshot

Status: current for workspace creation / stale / needs reread
Captured: <date/time>
Source: <archive / commit / branch / repo state>

## Copied Canonical Files

| Canonical file | Workspace copy | Doc version/status | Notes |
|---|---|---|---|
| <path> | copies/<path> | <version/status> | <notes> |

## Not Checked

- <item>

## Freshness Risks

- <file or scope that may change in parallel>

## Sync Requirement

Before any canonical sync, reread current canonical target files and compare them with this base snapshot.
```

## 4. responsibility-map.md Template

```markdown
# Workspace Responsibility Map

Status: local responsibility map for workspace
Scope: maps workspace files and proposed changes to canonical owners

| Workspace file/scope | Responsibility | Canonical owner affected later |
|---|---|---|
| copies/<path> | <proposed change area> | <canonical file/path> |
| notes/<file>.md | <decision/risk notes> | <owner if synced> |

## Out Of Scope

- <item>
```

## 5. local-action-log.md Template

```markdown
# Local Action Log

Status: local workspace history only
Scope: local parallel-agent steps; not canonical action history

## Entries

### <date/time> — <local step>

Status:
  local / staged / superseded / sync-candidate / rejected-locally

Action:
  <what happened>

Workspace files touched:
  - <path>

Canonical files affected later:
  - <path>

Ordering note:
  This is parallel-agent local history. Canonical ordering starts only after reviewed sync.
```

## 6. copies/ Rule

Place shadow copies at:

```text
copies/<repo-relative-path>
```

A file in `copies/` is never canonical by itself.

## 7. notes/ Rule

Use `notes/` for decisions, risks, rejected options and unresolved local notes that are useful for sync review.

Do not use `notes/` as a global backlog.
