# Documentation Responsibility-Zone Review Workflow

Status: current reusable responsibility-zone review workflow  
Scope: how to review existing documentation-layer content and decide which owner/type/zone should own each piece

## 1. Purpose

This workflow preserves the method used to review existing documentation-layer files for responsibility boundaries.

It answers:

```text
What is this existing paragraph/section trying to do?
Which responsibility zone should own it?
Is it a reusable principle, workflow detail, field-kit setup question, template shape, adapter mapping, example, or navigation/placement row?
What can be extracted before moving or splitting the text?
```

Use this workflow when reviewing existing docs for:

```text
- responsibility-zone drift;
- portability / reusable-layer migration;
- principles vs workflow vs adapter confusion;
- overloaded files;
- project-specific paths embedded in reusable files;
- possible extraction into profiles, adapters, examples or field kits.
```

## 2. What This Workflow Owns

This workflow owns the **review method** for assigning existing documentation content to the correct responsibility zone.

It does not own:

```text
- the principles themselves;
- exact templates;
- project adapter contents;
- example bodies;
- command routing;
- the final target folder structure for a migration.
```

Those are owned by their specific owner files.

## 3. Core Review Method

Review existing content as a claim, not as text to move mechanically.

For each paragraph, section, table row or rule:

```text
1. Identify the fundamental purpose.
2. Ask what problem this text prevents.
3. Ask what responsibility boundary it protects.
4. Mentally remove concrete project names, paths, IDs and examples.
5. Extract the reusable core, if one exists.
6. Classify the remaining specialized or project-specific material.
7. Assign the correct owner type.
8. Record the move/keep/split action.
```

Do not move text to an adapter merely because it contains a concrete project path.

First extract the reusable principle or profile pattern.

## 4. Classification Buckets

Use these buckets:

| Bucket | Meaning | Typical owner |
|---|---|---|
| Reusable principle / invariant | Stable rule about how documentation should or should not work. | principles file |
| Specialized reusable profile | Pattern for a class of projects, but not all docs systems. | profile file |
| Project adapter mapping | Concrete project path, source map, evidence map, register map or layer vocabulary. | project adapter/profile |
| Workflow detail | Repeatable operational steps. | workflow file |
| Field-kit setup detail | Setup questions/tools used to derive project-specific artifacts. | field kit |
| Template detail | Exact artifact/output/file shape. | template |
| Responsibility placement | Where an information type belongs. | responsibility map |
| Navigation/read order | How to discover or read files. | README/index |
| Use-case route | User wording -> workflow/output/permission route. | use-case map |
| Example candidate | Concrete demonstration of a rule/workflow/profile. | example file/index |
| Historical/action note | What changed and why. | action log |
| Deferred/waiting task | Future task or waiting condition. | PMR/task register |

## 5. Portability / Reusable Migration Mode

When the review is for reusable-layer migration, use this specific split:

```text
reusable core
  = applicable to any documentation system or AI-assisted docs layer;

specialized profile
  = reusable for a class of projects, such as scenario-driven app/product projects;

project adapter mapping
  = exact current project terms, paths, IDs, evidence sources and output layers;

example candidate
  = concrete current-project demonstration that should not be embedded in reusable owner docs.
```

Common trap:

```text
A paragraph mentions an Enman path.
Wrong: move the whole paragraph to the Enman adapter.
Right: extract the reusable principle first, then move only the concrete mapping/example.
```

## 6. Documentation-Layer Boundary

When reviewing documentation-layer portability, do not drift into reviewing project content itself.

If the reviewed text mentions:

```text
planning/slices/
planning/diagrams/
planning/api/
planning/testing/
```

ask:

```text
Why does the documentation layer reference this path?
Is this reference a reusable principle, specialized profile pattern, adapter mapping, example link or workflow detail?
```

Do not review whether the slice/scenario/API content itself is correct unless a separate task asks for it.

## 7. Output Shape

Use this table for non-trivial reviews:

| Original section / text | Fundamental purpose | Reusable core | Specialized profile | Adapter mapping | Example candidate | Correct owner | Action |
|---|---|---|---|---|---|---|---|

Use short cells. Put longer reasoning below the table when needed.

## 8. Checks Before Finalizing

| Check | Question |
|---|---|
| Purpose checked | Did we identify what problem the text prevents? |
| Path trap avoided | Did we avoid moving text only because it contains a concrete path? |
| Reusable core extracted | Did we extract a reusable rule before assigning project-specific leftovers? |
| Owner type selected | Did we decide principle/workflow/field-kit/template/adapter/example/etc.? |
| Project content boundary | Did we avoid reviewing non-documentation-layer content by accident? |
| Examples separated | Are concrete examples linked or planned separately instead of embedded in reusable owners? |
| Split safety | Are active docs preserved unless an approved migration batch changes them? |
| Delivery safety | Are large/shared files and archive/source needs visible if file updates are planned? |

## 9. Do Not

```text
- Do not use this workflow as a replacement for the responsibility map.
- Do not put exact project mappings into reusable principles.
- Do not leave process steps in principles when a workflow owns them.
- Do not turn examples into owner docs.
- Do not review scenario/slice/domain/API content when the task is docs-layer responsibility review.
- Do not split active files before the candidate-copy/classification plan says to do so.
- Do not treat a field kit as the repeated project workflow after project-specific artifacts exist.
```

## 10. Related Files

```text
planning/documentation-migration/documentation-layer-portability-migration-plan.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```
