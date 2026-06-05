# Parallel Workspace Workflow

Status: active reusable workflow
Doc version: v0.1.0
Scope: how to create and run one staging-only parallel-agent workspace

Use with:

```text
planning/documentation/parallel-work/README.md
planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
planning/documentation/parallel-work/parallel-sync-workflow.md
```

## 1. Purpose

Use this workflow when one chat/agent needs to work in parallel with other chats and the target changes may touch shared canonical documentation files.

The workspace lets the agent prepare proposed changes without directly changing shared canonical files.

## 2. When To Activate

Activate this workflow when:

```text
- multiple chats may edit the same root/documentation files;
- the work is useful but not ready to block other agents;
- the agent needs shadow copies to explore changes safely;
- action-log ordering would be ambiguous if the agent wrote directly to the main action log;
- the user explicitly asks for parallel work / parallel agent / staging workspace.
```

Do not activate it for simple single-file edits, read-only checks or normal archive generation.

## 3. Create Workspace

Choose a stable workspace id:

```text
<YYYY-MM-DD>-<short-scope>
```

Create:

```text
planning/documentation/parallel-work/workspaces/<workspace-id>/
  README.md
  base-snapshot.md
  responsibility-map.md
  local-action-log.md
  copies/
  notes/
```

Use the exact shape from:

```text
planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
```

## 4. Base Snapshot

Record the source state before creating shadow copies.

Include:

```text
- archive/commit/branch/date;
- copied canonical files;
- Doc version/status labels where present;
- files not checked;
- known parallel work that may make copies stale.
```

A base snapshot is not a proof that the copy remains current later. Sync must reread current canonical files.

## 5. Shadow Copies

Put proposed edits under:

```text
copies/<repo-relative-path>
```

For example:

```text
copies/planning/planning-use-case-map.md
copies/planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

Shadow copies are staging artifacts only. They do not override canonical docs.

## 6. Local Action Log

Record local steps in:

```text
local-action-log.md
```

Use local statuses such as:

```text
local / staged / superseded / sync-candidate / rejected-locally
```

The local action log does not establish canonical ordering. Canonical ordering starts only when a reviewed sync is applied and recorded in the main action log.

## 7. Sync-Candidate Criteria

A workspace may become sync-candidate when:

```text
- in-scope proposed changes are represented in copies/ or notes/;
- base-snapshot.md is complete enough for freshness review;
- responsibility-map.md names target canonical owners;
- local-action-log.md records meaningful local decisions;
- out-of-scope and rejected local changes are clear.
```

Do not create an aggregate sync plan until at least one workspace is sync-candidate.

## 8. Handoff To Sync

When ready, hand off to:

```text
planning/documentation/parallel-work/parallel-sync-workflow.md
```

The sync operation may include this workspace alone or several workspaces together.

## 9. Do Not

```text
- Do not edit shared canonical root/docs files directly during parallel workspace work.
- Do not append planning/documentation-action-log.md during local workspace steps.
- Do not create sync-plan.md inside the workspace by default.
- Do not claim workspace copies are current after canonical files change.
- Do not treat rejected local notes as backlog unless they are copied into an owner backlog/deferred file during sync.
```
