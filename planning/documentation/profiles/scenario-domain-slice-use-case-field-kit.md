# Scenario / Domain / Slice Use-Case Field Kit

Status: active reusable profile-specific field kit  
Scope: setup guidance for adding scenario/domain/slice route rows to a project's single root use-case map

## 1. Purpose

This field kit helps scenario-driven app/product projects add concrete scenario/domain/slice command routes to their root use-case map.

It is profile-specific reusable guidance.

It is not a second use-case map.

For Enman, concrete rows belong in:

```text
planning/planning-use-case-map.md
```

## 2. Applicability

Use this kit when a project has documentation layers like:

```text
scenario/source behavior
  -> DATA / behavior item sets
  -> domain interpretation
  -> slices / delivery scopes
  -> API/testing/evidence
```

Related reusable profile candidate:

```text
planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md
```

That profile is still pending active promotion in a later reusable docs-layer switch batch. Do not reference `planning/documentation/profiles/scenario-domain-slice-docs-profile.md` until that active profile exists.

If the project does not use this topology, do not force these routes into the root map.

## 3. Responsibility Split

| Owner | Owns |
|---|---|
| Root use-case map | Concrete project route rows, exact file paths and command aliases. |
| This field kit | Suggested scenario/domain/slice route families and setup questions. |
| Scenario/domain/slice profile | Documentation topology and layer responsibility pattern. |
| Layer responsibility maps | Local placement/routing inside scenario, domain, slice, testing or API layers. |
| Workflows/templates | Actual drafting/review algorithms and output shapes. |

## 4. Route Families To Consider

| Route family | User wording examples | Typical owner reads |
|---|---|---|
| Scenario source work | “сделай сценарий”, “DATA”, “UI scenario”, “behavior items” | scenario README, scenario responsibility map, scenario drafting workflow, relevant source files |
| Domain work | “domain discovery”, “aggregate draft”, “value object draft” | domain README/map, scenario-to-aggregate map, relevant scenario/domain sources |
| Slice work | “задрафти slice”, “server slice”, “client sidecar”, “cross-cutting umbrella” | slices README/map, slice drafting workflows/templates, relevant scenario/domain/API/testing sources |
| Testing hooks | “test plan”, “проверь тестирование” | testing responsibility map, slice test-plan workflow, relevant testing docs |
| Source usage / cascade | “source usage”, “stale downstream”, “каскад” | source usage profile/governance, relevant source and consumer docs |
| Diagram/scenario consistency | “диаграммы”, “draw.io”, “diagram prompt” | diagramming responsibility map and scenario/domain/testing sources when relevant |

## 5. Setup Questions

Before adding scenario-driven rows to the project root map, answer:

```text
1. Which scenario/domain/slice layers exist in this project?
2. Which user commands recur often enough to deserve root-map rows?
3. Which layer README/responsibility map owns first routing for each command?
4. Which workflows/templates own actual drafting/review?
5. Which root project profiles must be read for status/evidence/source usage?
6. Which commands are project-specific and should not become reusable examples?
7. Which rows need detailed traces because they are high-risk or broad?
```

## 6. Suggested Root Row Pattern

Use this row pattern in the concrete root map:

| User says | Task type | Active context? | Traversal depth | Read source mode | Activated workflows | Required reads | Source of obligation | Expected output | Permission boundary |
|---|---|---:|---|---|---|---|---|---|---|

Rows should contain project paths, not abstract placeholders.

## 7. Enman Active Root Profiles

For Enman-like projects, some scenario/domain/slice routes may need active root profiles:

```text
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

These files are project configuration, not reusable workflow logic.

## 8. Do Not

```text
- Do not duplicate the root use-case map here.
- Do not bury project-critical scenario commands only in this profile kit.
- Do not make scenario/domain/slice topology universal.
- Do not copy long workflow steps into use-case rows.
- Do not route status/source/evidence claims without the relevant project root profiles.
```
