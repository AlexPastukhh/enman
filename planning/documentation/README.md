# Documentation Update Workflow Index

Status: current documentation-update workflow index  
Scope: documentation update planning, status reconciliation, local/global synchronization, archive generation, direct scoped GitHub edits and navigation updates

## 1. Purpose

This folder defines how documentation-only chats/agents should update planning docs.

A documentation update agent must:

```text
- read current repo state before changing docs;
- prepare a Documentation Update Plan for broad docs/navigation/status/register changes;
- reconcile docs with implemented/planned/deferred status;
- synchronize local docs with shared indexes/registers;
- update navigation/responsibility maps together with new docs;
- use direct GitHub edits only when explicitly asked to apply changes;
- use one file per commit by default for direct GitHub edits;
- use archive/replacement packages when manual application or broad generated replacement is more practical.
```

## 2. Files

```text
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/documentation-update-workflow.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/documentation-update-agent-prompt.md
```

Related archive/package guide:

```text
planning/replacement-file-generation-guide.md
```

Related responsibility and clean wording docs:

```text
planning/planning-doc-responsibility-map.md
planning/vkr-clean-reference.md
```

## 3. Read Order For Documentation-Only Work

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md
5. planning/documentation/README.md
6. planning/documentation/documentation-update-plan-workflow.md
7. planning/documentation/documentation-update-workflow.md
8. planning/documentation/planning-docs-architecture-principles.md
9. planning/documentation/status-reconciliation-workflow.md
10. planning/documentation/local-global-documentation-sync-workflow.md
11. planning/replacement-file-generation-guide.md
12. relevant domain/API/testing/slice/client docs for the requested area
```

## 4. Documentation Update Plan Rule

Before broad documentation changes, prepare a `Documentation Update Plan`.

Use:

```text
planning/documentation/documentation-update-plan-workflow.md
```

A plan is required when the task affects:

```text
planning navigation
folder README/index files
source-of-truth rules
responsibility boundaries
status labels
shared registers
multiple planning files
VKR/thesis clean wording
```

A full plan is usually not required for small typo fixes, read-only analysis or narrow local wording changes that do not affect navigation/source-of-truth/registers.

## 5. Documentation-Only Agent Rule

Documentation-only work must not:

```text
- change code;
- change generated artifacts;
- implement backend/client/API behavior;
- silently rewrite unrelated docs;
- silently change scenario/domain/API/testing meaning;
- overclaim planned work as implemented;
- treat dirty drafts as source of truth.
```

Direct GitHub writes are allowed only when the user explicitly asks to apply repository changes. For direct GitHub edits, use one file per commit by default.

If direct repository writes are not requested or the update is better reviewed manually, output an archive with complete replacement/add files.

## 6. Local / Global Sync Rule

Documentation-only work must check whether local changes need shared index/register updates.

Use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

Important local slice questions should not remain discoverable only from one slice file when they can affect other work.

## 7. Documentation Architecture Link

Planning docs should follow the architecture principles in:

```text
planning/documentation/planning-docs-architecture-principles.md
```

The key rule is that docs must be navigable, source-of-truth aware and safe to update in small scoped changes.

## 8. Draft-Driven Discovery Link

All slice families use draft-driven discovery:

```text
domain drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
documentation/status reconciliation drafts
```

Primary source:

```text
planning/slices/draft-driven-discovery-principles.md
```
