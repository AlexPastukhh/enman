# Documentation Update Workflow Index

Status: current documentation layer index / read order  
Scope: documentation update planning, documentation architecture, local responsibility routing, status reconciliation, local/global synchronization, reviewable agent outputs and navigation updates

## 1. Purpose

This folder defines how documentation-only chats/agents should understand and update planning docs.

This README is navigation and read order only. It should not duplicate full architecture principles, responsibility maps or workflow details.

A documentation update agent must:

```text
- read current repo state before changing docs;
- use the root responsibility map to choose the planning layer;
- use the documentation responsibility map for documentation-layer placement;
- prepare a Documentation Update Plan for broad docs/navigation/status/register changes;
- reconcile docs with implemented/planned/deferred status;
- synchronize local docs with shared indexes/registers;
- update navigation/responsibility maps together with new docs;
- use direct GitHub edits only when explicitly asked to apply changes;
- use one file per commit by default for direct GitHub edits;
- use archive/replacement packages when manual application or broad generated replacement is more practical.
```

## 2. Files

Core documentation-layer files:

```text
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/reviewable-agent-output-workflow.md
planning/documentation/documentation-update-agent-prompt.md
```

Related root/global docs:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
planning/planning-agent-protocol.md
planning/planning-workflow-current.md
planning/replacement-file-generation-guide.md
planning/vkr-clean-reference.md
```

## 3. Read Order For Documentation-Layer Work

For documentation-layer architecture/routing work:

```text
1. planning/README.md
2. planning/documentation/README.md
3. planning/documentation/planning-docs-architecture-principles.md
4. planning/planning-doc-responsibility-map.md
5. planning/documentation/documentation-responsibility-map.md
6. relevant documentation workflow file
```

For broad documentation updates:

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/planning-agent-protocol.md
4. planning/planning-doc-responsibility-map.md
5. planning/documentation/README.md
6. planning/documentation/planning-docs-architecture-principles.md
7. planning/documentation/documentation-responsibility-map.md
8. planning/documentation/documentation-update-plan-workflow.md
9. planning/documentation/documentation-update-workflow.md
10. planning/documentation/status-reconciliation-workflow.md
11. planning/documentation/local-global-documentation-sync-workflow.md
12. planning/replacement-file-generation-guide.md, if archive/replacement mode is relevant
13. relevant domain/API/testing/slice/client/scenario docs for the requested area
```

For answer-format or reviewable-output work:

```text
planning/documentation/reviewable-agent-output-workflow.md
```

## 4. Responsibility Routing Rule

If deciding which planning layer information belongs to, start with:

```text
planning/planning-doc-responsibility-map.md
```

If the information already belongs to the documentation layer, use:

```text
planning/documentation/documentation-responsibility-map.md
```

Examples:

```text
global docs architecture principle -> planning-docs-architecture-principles.md
documentation-layer placement rule -> documentation-responsibility-map.md
docs update process -> documentation-update-workflow.md
preflight docs update plan format -> documentation-update-plan-workflow.md
local/global sync process -> local-global-documentation-sync-workflow.md
status reconciliation process -> status-reconciliation-workflow.md
agent answer/review format -> reviewable-agent-output-workflow.md
reusable prompt -> documentation-update-agent-prompt.md
```

## 5. Documentation Update Plan Rule

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

## 6. Documentation-Only Agent Rule

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

## 7. Local / Global Sync Rule

Documentation-only work must check whether local changes need shared index/register updates.

Use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

Important local slice questions should not remain discoverable only from one slice file when they can affect other work.

## 8. Documentation Architecture Link

Planning docs should follow the architecture principles in:

```text
planning/documentation/planning-docs-architecture-principles.md
```

The key rule is that docs must be navigable, source-of-truth aware, type-explicit and safe to update in small scoped changes.

## 9. Reviewable Agent Output Link

For non-trivial answers, audits, plans, reviews or handoffs, use:

```text
planning/documentation/reviewable-agent-output-workflow.md
```

This workflow defines response levels, sources/coverage blocks, section-level sources and commands such as recheck, clarify and keep prev.

## 10. Scoped Sync Notes

Scoped sync notes are case-specific documentation synchronization notes. They are useful for trace/audit/context, but they are not reusable workflows or global architecture principles.

Current scoped sync notes in this folder:

```text
planning/documentation/cc-doc-001-upload-agreement-proposal-document-sync-note.md
planning/documentation/l2-final-readme-doc-sync-note.md
planning/documentation/l2-scenario-status-marker-sync-note.md
```

Do not treat scoped sync notes as canonical workflow files. If a scoped sync note contains a reusable rule, promote that rule into the correct workflow or architecture file through a normal documentation update.

## 11. Draft-Driven Discovery Scope Note

Draft-driven discovery is currently a slice-layer discovery loop.

Use:

```text
planning/slices/draft-driven-discovery-principles.md
```

for:

```text
business slice drafts;
server/backend/API slice drafts;
client sidecar drafts;
cross-cutting/helper slice drafts;
slice verification/test planning inside a slice draft;
implemented-slice draft sync when a slice already has code/tests.
```

Do not use that file as the owner for domain drafting, scenario drafting or documentation/status reconciliation. Those areas use their own layer owners and workflows.
