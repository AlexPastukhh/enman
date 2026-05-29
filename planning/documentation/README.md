# Documentation Update Workflow Index

Status: current documentation layer index / read order  
Scope: documentation update planning, documentation architecture, local responsibility routing, status reconciliation, local/global synchronization, reviewable agent outputs, response-level commands, file update overview summaries, working example coverage, source usage cascade governance, documentation action logging and navigation updates

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
- decide whether a working example is needed when templates, output shapes, commands or draft formats change;
- use File Update Overview when a non-trivial file/docs/code update answer needs a final structured file-change summary;
- use source usage cascade governance when source/stale-reference/reviewed-upstream-work concerns appear;
- record significant logical documentation actions in the documentation action log;
- use direct GitHub edits only when explicitly asked to apply changes;
- use one file per commit by default for direct GitHub edits;
- use archive/replacement packages when manual application or broad generated replacement is more practical.
```

## 2. Files

Core documentation-layer files:

```text
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
planning/documentation/example-coverage-workflow.md
planning/documentation/source-usage-cascade-governance-plan.md
planning/documentation/source-usage-pilots/README.md
planning/documentation/documentation-action-log.md
planning/documentation/examples/README.md
planning/documentation/documentation-update-agent-prompt.md
```

Related root/global docs:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
planning/planning-agent-protocol.md
planning/planning-workflow-current.md
planning/planning-use-case-map.md
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

For documentation-layer portability, responsibility-zone review or reusable migration planning:

```text
1. planning/README.md
2. planning/documentation/README.md
3. planning/documentation/documentation-layer-portability-migration-plan.md
4. planning/documentation/documentation-responsibility-zone-review-workflow.md
5. planning/documentation/planning-docs-architecture-principles.md
6. planning/documentation/documentation-responsibility-map.md
7. target files being reviewed
```

Use this when reviewing whether existing documentation-layer content belongs to reusable principles, a specialized profile, a project adapter/profile, a workflow, a field kit, a template or an example.

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
12. planning/documentation/example-coverage-workflow.md, if the update changes templates, output shapes, response commands, output modes, draft formats or example coverage
13. planning/documentation/file-update-overview-workflow.md and planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md, if the update changes File Update Overview behavior or template shape
14. planning/documentation/source-usage-cascade-governance-plan.md, if the update touches source usage, stale references, cascade review, reviewed upstream work or source usage pilots
15. planning/documentation/documentation-action-log.md, if the update is a significant logical documentation action
16. planning/replacement-file-generation-guide.md, if archive/replacement mode is relevant
17. relevant domain/API/testing/slice/client/scenario docs for the requested area
```

For answer-format, reviewable-output or response-command work:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

For File Update Overview work:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

Use this when adding or changing the final structured file-change summary for non-trivial file/docs/code update answers.

For use-case-map creation/update work:

```text
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```

Use this when creating or updating reusable use-case maps, concrete route maps, command rows or expected-output routing tables.

For working example coverage decisions:

```text
planning/documentation/example-coverage-workflow.md
planning/documentation/examples/README.md
```

Use this when adding or changing templates, workflows with expected output, response commands, output modes, draft formats or example indexes.

For documentation responsibility-zone review:

```text
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/documentation-layer-portability-migration-plan.md
```

Use this when reviewing existing documentation-layer content for reusable/specialized/project-specific boundaries, owner-zone drift, field-kit candidates or candidate migration planning.

For source usage cascade governance and pilots:

```text
planning/documentation/source-usage-cascade-governance-plan.md
planning/documentation/source-usage-pilots/README.md
planning/documentation/documentation-action-log.md
```

Use this when working on source usage relationships, stale downstream references, reviewed-upstream-work concerns, cascade review pilots or the future source usage workflow.

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
documentation-layer portability migration plan -> documentation-layer-portability-migration-plan.md
documentation responsibility-zone review -> documentation-responsibility-zone-review-workflow.md
docs update process -> documentation-update-workflow.md
preflight docs update plan format -> documentation-update-plan-workflow.md
local/global sync process -> local-global-documentation-sync-workflow.md
status reconciliation process -> status-reconciliation-workflow.md
agent answer/review format and response-level commands -> reviewable-agent-output-and-commands-workflow.md
file update overview process -> file-update-overview-workflow.md
file update overview template -> FILE-UPDATE-OVERVIEW-TEMPLATE.md
use-case map workflow -> use-case-map-workflow.md
use-case map template -> USE-CASE-MAP-TEMPLATE.md
field-kit setup guidance -> relevant `*-field-kit.md` file when created; principles define the type
project adapter/profile mapping -> project adapter/profile file when created
example coverage decision process -> example-coverage-workflow.md
documentation-layer examples navigation -> examples/README.md
source usage cascade pilot governance -> source-usage-cascade-governance-plan.md
source usage pilot folder navigation -> source-usage-pilots/README.md
documentation logical action log -> documentation-action-log.md
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

## 9. Reviewable Output And Commands Link

For non-trivial answers, audits, plans, reviews, handoffs or response-level commands, use:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This workflow defines response levels, sources/coverage, section-level sources and commands such as recheck, clarify, keep prev and no ch.

## 10. File Update Overview

For the reusable final structured summary of file/docs/code update plans, archives, diff reviews and applied-update checks, use:

```text
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

File Update Overview is a final summary block. It does not replace the main reviewable answer.

## 11. Example Coverage And Examples Index

For reusable templates, workflows with expected output, response commands, output modes, draft formats or repeated chat failures caused by missing examples, use:

```text
planning/documentation/example-coverage-workflow.md
```

For documentation-layer working example navigation, use:

```text
planning/documentation/examples/README.md
```

Examples are supporting artifacts. They should demonstrate correct output or usage shape, but must not duplicate routing, source-mode, output-mode, permission or workflow activation logic owned by use-case maps and workflow files.

## 12. Source Usage Cascade Governance

For source usage relationships, layer encapsulation, stale downstream reference concerns and cascade-review pilot planning, use:

```text
planning/documentation/source-usage-cascade-governance-plan.md
```

For pilot folder navigation, use:

```text
planning/documentation/source-usage-pilots/README.md
```

Pilot registers are experimental review/cascade aids. They do not replace scenario/domain/slice source files, maps, drafts or future permanent source usage registers.


## 13. Documentation Action Log

For significant logical documentation actions and short explanations of why they happened, use:

```text
planning/documentation/documentation-action-log.md
```

The action log records completed logical documentation actions. It is not the PMR, not a task register and not the source of truth for rules. Owner files define rules; the action log records what changed, why, affected files/layers and optional PMR relation.

Use it for changes to architecture principles, workflow behavior, accepted command meaning, source-of-truth boundaries, source usage/cascade governance, template/output shapes, onboarding routes, example infrastructure, replacement archive behavior or PMR/task governance.

## 14. Scoped Sync Notes

Scoped sync notes are case-specific documentation synchronization notes. They are useful for trace/audit/context, but they are not reusable workflows or global architecture principles.

Current scoped sync notes in this folder:

```text
planning/documentation/cc-doc-001-upload-agreement-proposal-document-sync-note.md
planning/documentation/l2-final-readme-doc-sync-note.md
planning/documentation/l2-scenario-status-marker-sync-note.md
```

Do not treat scoped sync notes as canonical workflow files. If a scoped sync note contains a reusable rule, promote that rule into the correct workflow or architecture file through a normal documentation update.

## 15. Draft-Driven Discovery Scope Note

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
slice verification/test planning inside a slice draft.
```

Do not use that file as the owner for domain drafting, scenario drafting or documentation/status reconciliation. Those areas use their own layer owners and workflows.
