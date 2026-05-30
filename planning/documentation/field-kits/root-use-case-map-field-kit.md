# Root Use-Case Map Field Kit

Status: active reusable documentation-layer field kit  
Scope: one-time / rare setup guidance for deriving a single project root use-case map

## 1. Purpose

This field kit helps a project create or restructure one concrete root use-case map.

A project should usually have exactly one root use-case map.

Example: in Enman, the concrete project instance is:

```text
planning/planning-use-case-map.md
```

This field kit is not a second use-case map and should not be used at runtime instead of the project root map.

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Project root use-case map | Concrete routes for one project: user wording, traversal, read sources, activated workflows, expected output and permission boundaries. |
| This field kit | Setup choices for creating/adapting the project root use-case map, including common command clusters. |
| Use-case-map workflow | Repeated maintenance process after the project map exists. |
| Use-case-map template | Exact reusable Markdown shape for a concrete map. |
| Profile-specific field kits | Optional route clusters for project classes, such as scenario/domain/slice projects. |

## 3. Core Rule

```text
One project -> one concrete root use-case map.
```

Do not create a generic reusable use-case map inside the documentation layer.

Reusable docs should provide:

```text
- field kits for setup;
- workflows for maintenance;
- templates for shape;
- examples for demonstration.
```

The concrete project map should remain the only runtime router.

## 4. Setup Questions

Before creating or restructuring a root use-case map, answer:

```text
1. What is the project entrypoint README?
2. What is the root workflow activation map?
3. What is the root responsibility map?
4. Which repeated user commands need stable routing?
5. Which project-specific task families need primary use-case rows?
6. Which source modes exist: conversation, GitHub/repo, archive, uploaded files, canvas?
7. Which output modes exist: answer, draft, plan, diff, package, direct edit?
8. Which actions require explicit user permission?
9. Which docs layer files own reusable workflow/template logic?
10. Which profile-specific route kits apply to this project?
```

## 5. Common Command Clusters

Most command-heavy assistant projects need some subset of these reusable command clusters.

| Cluster | Typical commands | Purpose |
|---|---|---|
| Draft continuation | `драфт`, `давай драфт`, `обнови драфт`, `отличия драфта` | Continue or compare the active draft without starting a new one. |
| Answer shape | `кп`, `без кп`, `саммари`, `без саммари`, `итог`, `без итога`, `полный конец` | Control key points, contextual summary and file/update overview blocks. |
| Recheck/context | `обс`, `перепроверь`, `проверь`, `recheck` | Re-check prior discussion, active answer, sources or applied changes. |
| Source mode | `арх`, `из архива`, `учти файл X`, `без изм` | Decide whether to use archive/uploaded file/current context and traversal depth. |
| Output package | `давай архив`, `собери архив`, `replacement package` | Produce manual replacement archive/package output. |
| Permission boundary | `без изм`, package vs direct edit wording | Prevent silent repository changes or output-mode substitution. |

A project can localize aliases and wording. Keep active-context behavior explicit.

## 6. Root Use-Case Map Sections

A concrete root map should normally include:

```text
- purpose;
- relationship to root files;
- universal algorithm;
- principle/workflow/source references;
- active context rule;
- accepted command/no-reinvention rule;
- traversal depth;
- read source modes;
- output modes;
- source model/delta rules;
- repeated/continuation commands table;
- primary use case table;
- detailed traces for high-risk routes.
```

Exact shape belongs to:

```text
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```

## 7. Row Creation Checklist

For each command or use case row:

```text
1. Capture user wording / aliases.
2. Decide whether active context changes behavior.
3. Choose traversal depth.
4. Choose read source mode.
5. Name activated workflows/templates.
6. Name required reads.
7. Name source of obligation.
8. Describe expected output briefly.
9. Define permission boundary.
10. Link owner files instead of copying their logic.
```

Maintenance workflow:

```text
planning/documentation/use-case-map-workflow.md
```

## 8. Profile-Specific Route Kits

If a project has a specialized structure, use a profile-specific field kit to derive route rows.

For scenario/domain/slice projects, use:

```text
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

The profile kit suggests route families. The project root map still owns the concrete rows.

## 9. Do Not

```text
- Do not create a second generic use-case map inside the documentation layer.
- Do not move concrete project routes out of the root map into this field kit.
- Do not put full workflow steps into root map rows.
- Do not hide scenario/domain/slice command setup in a deep folder where project maintainers will not find it.
- Do not silently change command meaning when extracting reusable setup logic.
```
