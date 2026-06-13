# Documentation Reusable Layer Index

Status: active reusable documentation layer index / transitional post-switch cleanup  
Scope: documentation architecture, documentation update workflows, responsibility routing, reusable field kits, specialized profiles, examples, source-usage governance, parallel-agent work staging, command creation, Tampermonkey command projection, reusable full tool and action logging

## 1. Purpose

This folder is the active documentation layer:

```text
planning/documentation/
```

It now contains the reusable documentation layer that was promoted during the folder switch.

The previous active documentation folder `planning/documentation-legacy/` was removed by ROOT-LEGACY-CLEAN.

Migration-only candidate/history artifacts are preserved at:

```text
planning/documentation-migration/
```

## 2. Core Rule

This folder owns reusable documentation-layer logic.

It does not own concrete Enman project configuration. Concrete Enman project routing/profile files stay at planning root:

```text
planning/planning-use-case-map.md
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
planning/replacement-file-generation-guide.md
```

## 3. Active Structure

Core reusable documentation files:

```text
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-update-workflow.md
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
planning/documentation/tampermonkey-command-projection-workflow.md
```

Reusable field kits:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
planning/documentation/field-kits/status-reconciliation-field-kit.md
planning/documentation/field-kits/shared-visibility-map-field-kit.md
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

Specialized reusable profiles:

```text
planning/documentation/profiles/scenario-domain-slice-docs-profile.md
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

Repeated workflows / output owners:

```text
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/example-coverage-workflow.md
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/command-creation-workflow.md
```


Reusable full tool:

```text
planning/documentation/tools/tampermonkey/README.md
planning/documentation/tools/tampermonkey/chat-command-palette.user.js
```

Parallel work reusable owners:

```text
planning/documentation/parallel-work/README.md
planning/documentation/parallel-work/parallel-workflow.md
planning/documentation/parallel-work/parallel-sync-workflow.md
planning/documentation/parallel-work/PARALLEL-WORKSPACE-TEMPLATE.md
planning/documentation/parallel-work/PARALLEL-SYNC-PLAN-TEMPLATE.md
```

Examples:

```text
planning/documentation/examples/README.md
```

Project action tracking:

```text
planning/documentation-action-log.md
```

This is Enman project history, not reusable starter-kit content.

Migration/support history:

```text
planning/documentation-migration/sync-notes/
```

These migration/support files are not reusable workflows and are not active read-order sources for ordinary documentation updates.

## 4. Read Order

For documentation-layer architecture/routing work:

```text
1. planning/README.md
2. planning/documentation/README.md
3. planning/documentation/planning-docs-architecture-principles.md
4. planning/planning-doc-responsibility-map.md
5. planning/documentation/documentation-responsibility-map.md
6. relevant documentation workflow / field kit / profile
```

For broad documentation updates:

```text
1. planning/README.md
2. planning/planning-use-case-map.md
3. planning/workflow-activation-map.md
4. planning/planning-agent-protocol.md
5. planning/planning-doc-responsibility-map.md
6. planning/documentation/README.md
7. planning/documentation/planning-docs-architecture-principles.md
8. planning/documentation/documentation-responsibility-map.md
9. planning/documentation/documentation-update-plan-workflow.md
10. planning/documentation/documentation-update-workflow.md
11. planning/documentation/status-reconciliation-workflow.md, when status/current-state claims are involved
12. planning/documentation/local-global-documentation-sync-workflow.md, when local details need shared visibility
13. planning/documentation/field-kits/source-usage-cascade-field-kit.md, when source usage/cascade/stale-reference setup is involved
14. planning/documentation/example-coverage-workflow.md, when templates, output shapes, commands, output modes, draft formats or examples change
15. planning/documentation/file-update-overview-workflow.md and planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md, when file/change summary behavior changes
16. planning/documentation-action-log.md, when the update is a significant logical documentation action
17. planning/replacement-file-generation-guide.md, when archive/replacement mode is relevant
```

For root use-case map or command-routing work:

```text
1. planning/planning-use-case-map.md
2. planning/documentation/field-kits/root-use-case-map-field-kit.md
3. planning/documentation/use-case-map-workflow.md
4. planning/documentation/USE-CASE-MAP-TEMPLATE.md
5. planning/documentation/command-creation-workflow.md, when a command route is created or changed
6. planning/documentation/tampermonkey-command-projection-workflow.md, when command helper projection is in scope
7. planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md, when scenario/domain/slice route rows are added or changed
```

For status/shared visibility/source-usage setup or repeated use:

```text
status:
  setup: planning/documentation/field-kits/status-reconciliation-field-kit.md
  project profile: planning/status-evidence-profile.md
  repeated workflow: planning/documentation/status-reconciliation-workflow.md

shared visibility:
  setup: planning/documentation/field-kits/shared-visibility-map-field-kit.md
  project map: planning/shared-visibility-map.md
  repeated workflow: planning/documentation/local-global-documentation-sync-workflow.md

source usage / cascade:
  setup: planning/documentation/field-kits/source-usage-cascade-field-kit.md
  project profile: planning/source-usage-cascade-profile.md
  project pilots: planning/source-usage-pilots/
```

For parallel-agent workspace or aggregate sync work:

```text
1. planning/README.md
2. planning/documentation/README.md
3. planning/documentation/planning-docs-architecture-principles.md
4. planning/documentation/documentation-responsibility-map.md
5. planning/documentation/parallel-work/README.md
6. planning/documentation/parallel-work/parallel-workflow.md, for one staging workspace
7. planning/documentation/parallel-work/parallel-sync-workflow.md, for aggregate sync from one or more workspaces
8. the relevant template under planning/documentation/parallel-work/
```

## 4A. Portable Starter-Kit Adaptation

For copying/adapting this reusable docs layer into a new project or documentation domain:

```text
1. planning/documentation/PORTABLE-STARTER-KIT.md
2. planning/documentation/README.md
3. planning/documentation/planning-docs-architecture-principles.md
4. planning/documentation/documentation-responsibility-map.md
5. relevant field kits/profiles
6. planning/documentation/tampermonkey-command-projection-workflow.md, when helper projection is part of the target project
7. planning/documentation/tools/tampermonkey/README.md, when the reusable full helper is used or adapted
```

`PORTABLE-STARTER-KIT.md` is a one-time adaptation guide. It is not a normal read-order source after the target project has its root profiles, root use-case map and active documentation routing.

## 5. Migration / Legacy State

After ROOT-LEGACY-CLEAN:

```text
planning/documentation/
  active reusable documentation layer

planning/documentation-migration/
  migration-only candidate guardrails, classification artifacts and Enman adapter/profile history
```

The former `planning/documentation-legacy/` folder has been removed. Do not route active work to it.
Do not use `planning/documentation-migration/` as an active read-order source unless the task is explicitly about migration/history verification.

## 6. Do Not

```text
- Do not recreate a separate reusable-candidate folder after the switch.
- Do not move root Enman project profiles into this folder.
- Do not create a second generic root use-case map inside this folder.
- Do not treat examples as rule owners.
- Do not keep Enman project action history inside this reusable layer.
- Do not treat parallel-work workspace copies as canonical documentation.
- Do not create sync plans inside each workspace by default; aggregate sync plans belong under `parallel-work/syncs/`.
- Do not keep transitional source-usage governance bridges inside this reusable layer once field kits/profiles own the flow.
- Do not keep a second tracked project-local tools/tampermonkey copy when the reusable full helper is active.
- Do not treat any Tampermonkey userscript as command authority.
- Do not put reusable Tampermonkey projection rules only in project workstreams or local tools.
```
