# Portable Starter Kit

Status: active one-time portable adaptation guide  
Scope: how to copy and adapt this reusable documentation layer into a new project or documentation domain

## 1. When To Read This File

Read this file when:

```text
- copying planning/documentation/ into a new project;
- adapting the reusable documentation layer to a new documentation domain;
- creating project-root profiles from reusable field kits;
- deciding which examples are reusable demonstrations and which are project-specific examples.
```

After the target project has its own root use-case map, root profiles/maps and local routing, this file is not part of the normal daily read order.

## 2. What To Copy By Default

Copy the reusable documentation layer core:

```text
planning/documentation/README.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-update-workflow.md
planning/documentation/documentation-update-plan-workflow.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
planning/documentation/example-coverage-workflow.md
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/command-creation-workflow.md
planning/documentation/tampermonkey-command-projection-workflow.md
planning/documentation/parallel-work/
planning/documentation/PORTABLE-STARTER-KIT.md
```

Copy reusable kits and reusable specialized profiles:

```text
planning/documentation/field-kits/
planning/documentation/profiles/
```

Copy reusable full helper docs/tool when the target project may use command-helper UI projection:

```text
planning/documentation/tools/tampermonkey/
```

Copy generic examples:

```text
planning/documentation/examples/README.md
planning/documentation/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
```

## 3. What To Copy As Examples Only

Project-specific example folders may be copied as demonstration material:

```text
planning/documentation/examples/project-specific/enman/
```

These examples are useful because they show how a reusable field kit can be instantiated for a real project/scenario/application architecture.

Do not copy project-specific examples as target-project configuration.

For a new project, either:

```text
- keep the Enman example only as a worked reference; or
- replace it with planning/documentation/examples/project-specific/<new-project>/...
```

## 4. What Not To Copy As Reusable Starter Content

Do not copy Enman history, legacy or active project configuration as reusable starter content:

```text
planning/documentation-action-log.md
planning/documentation-migration/
planning/documentation-legacy/ (older Enman copies only; removed from current active repo)
planning/source-usage-pilots/
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

Root project files may be used as examples, but a new project should create its own versions.

## 5. Project-Root Files To Create After Copying

After copying the reusable layer, create project-specific root files.

Recommended root files:

```text
planning/planning-use-case-map.md
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
planning/documentation-action-log.md
```

Create these from the field kits and templates; do not blindly copy the Enman versions as active config.

## 6. How To Use Field Kits

Use field kits as setup tools.

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
  -> create/update planning/planning-use-case-map.md

planning/documentation/field-kits/status-reconciliation-field-kit.md
  -> create/update planning/status-evidence-profile.md

planning/documentation/field-kits/shared-visibility-map-field-kit.md
  -> create/update planning/shared-visibility-map.md

planning/documentation/field-kits/source-usage-cascade-field-kit.md
  -> create/update planning/source-usage-cascade-profile.md and optional pilot/register placement
```

After the project-root files exist, normal project work should read the project-root files, not this starter guide.

## 7. How To Use Profiles

Reusable profiles under `planning/documentation/profiles/` are specialized guidance for a kind of documentation architecture, not active project configuration.

Use them only when applicable.

For scenario/domain/slice-style projects:

```text
planning/documentation/profiles/scenario-domain-slice-docs-profile.md
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

If the new project does not use scenario/domain/slice topology, define a different project/domain profile instead of forcing this one.

## 8. Adaptation Checklist

Before considering the copied layer ready:

```text
1. Replace project names and project-specific paths.
2. Create the root use-case map for the new project.
3. Create root project profiles/maps from field kits.
4. Decide which generic examples remain useful.
5. Keep or replace project-specific examples.
6. Create a fresh project action log if needed.
7. Do not copy migration or legacy folders as active documentation.
8. If helper UI commands are needed, use planning/documentation/tools/tampermonkey/ as the reusable helper source and keep Tampermonkey projection rules inside planning/documentation/.
9. Confirm README read order points to the new project's root files.
```

## 9. Stop Condition

Stop reading this file after the target project has:

```text
- a concrete root use-case map;
- root project profiles/maps for status, visibility and source usage if needed;
- an active documentation README;
- adapted example policy;
- no active routes to Enman-specific config.
```

After that, use normal read order from `planning/documentation/README.md`.


## Tampermonkey reusable-only note

The reusable full helper is tracked inside:

```text
planning/documentation/tools/tampermonkey/chat-command-palette.user.js
```

Do not create a second tracked project-local helper copy by default. A copied project may install/adapt the userscript in Tampermonkey, but command semantics still come from the project root UCM and owner workflows.
