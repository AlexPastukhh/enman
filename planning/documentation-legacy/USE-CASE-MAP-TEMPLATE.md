# Use-Case Map Template

Status: current reusable documentation-layer template  
Scope: exact reusable Markdown shape for concrete use-case maps

Use with:

```text
planning/documentation/use-case-map-workflow.md
```

This template is generic. Replace system-specific owner files, layer names and permission boundaries for the target system.


Reusable setup field kit:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
```

Profile-specific route setup, when applicable:

```text
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

This template defines shape only. It is not a second use-case map.

## Template

````markdown
# <System / Area> Use Case Map

Status: <current / draft / experimental> <scope description>  
Scope: maps user-visible actions and commands to docs, workflows, templates, sources, expected outputs and permission boundaries for <system / area>

## 1. Purpose

This file answers:

```text
User says X.
What should the chat/agent do?
Which docs should it read?
Why those docs?
Where is the obligation to read/use them defined?
What output is expected?
What requires explicit permission?
```

This file is not:

```text
- a full workflow;
- a template;
- a responsibility map;
- a README replacement;
- a source-of-truth for owner workflows it routes to.
```

## 2. Relationship To Owner Files

```text
<README or entrypoint>
  Overview / entry point / source-of-truth map.

<workflow activation or routing file>
  Workflow activation and preflight rules.

<responsibility map>
  Layer or local ownership routing.

<this use-case map>
  User command/action -> docs/workflows/templates/sources/output/permission route.
```

Reusable use-case-map setup, maintenance and shape are owned by:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```

## 3. Universal Algorithm

For non-trivial work in this system, use this path:

```text
1. Decide whether the request starts new work or continues active context.
2. Use the system entrypoint and activation/routing docs.
3. Use the responsibility map to choose the owner layer/file.
4. Select traversal depth.
5. Select read source mode.
6. Select output mode when the user asks for a deliverable.
7. Read owner workflows for algorithms.
8. Read templates for output shape.
9. Read sources/evidence needed for this pass.
10. Report sources used and relevant not-checked sources.
11. Separate implicit checks from explicit-permission actions.
12. Produce answer/update/plan/draft/package.
```

## 4. Active Context Rule

Short commands apply to active context unless the user names another target.

Active context can be:

```text
- active draft;
- active answer;
- active plan;
- active section;
- active document;
- active use case;
- active source snapshot;
- active repo/archive/read mode.
```

If there is no active context, ask for the target unless the target is obvious.

## 5. Repeated / Continuation Commands

| User says | If active context exists | If no active context exists | Traversal depth | Read source mode | Expected output |
|---|---|---|---|---|---|
| `<command>` | <behavior> | <fallback/clarification> | <none/reuse/targeted/full> | <conversation/archive/repo/files> | <output> |

## 6. Primary Use Case Table

Use the full table for complex systems where source mode, workflow activation and permission boundaries matter.

| User says | Task type | Active context? | Traversal depth | Read source mode | Activated workflows | Required reads | Source of obligation | Expected output | Permission boundary |
|---|---|---:|---|---|---|---|---|---|---|
| `<phrasing / command>` | <task type> | <new/active/optional> | <targeted/full/etc.> | <source mode> | <workflows/templates> | <files/sources> | <why this route is required> | <expected output> | <permission boundary> |

## 7. Reduced Use Case Table

Use the reduced table only for smaller systems where the full table would add noise.

| User says | Task type | Required reads | Expected output | Permission boundary |
|---|---|---|---|---|
| `<phrasing / command>` | <task type> | <files/sources> | <expected output> | <permission boundary> |

## 8. Detailed Traces

Add detailed traces only for routes that need more than a table row.

### Detailed Trace: <route name>

User says:

```text
<phrasing / command>
```

Steps:

```text
1. <step>
2. <step>
3. <step>
```

Boundaries:

```text
- <what this route must not do>
```
````

## Field Notes

```text
User says
  Actual phrases, command aliases or trigger language.

Task type
  Short classification of the work.

Active context?
  Whether active draft/plan/answer/source context changes behavior.

Traversal depth
  How much surrounding context should be read.

Read source mode
  Conversation, archive, current repo, named files, web or other sources.

Activated workflows
  Owner workflows/templates used by the route. Link/name owners; do not copy them.

Required reads
  Concrete files/sources to inspect.

Source of obligation
  Why this route or read set is required.

Expected output
  Short reviewable output shape. Link owner template/workflow when relevant.

Permission boundary
  What requires explicit approval or must not be done implicitly.
```

## Rules

```text
- Keep use-case rows short enough to scan.
- Use detailed traces only when a row is not enough.
- Link owner workflows/templates instead of copying their logic.
- Keep reusable use-case-map maintenance rules in use-case-map-workflow.md.
- Keep exact reusable shape in this template.
- Adapt concrete maps to their system; do not copy project-specific owner files into unrelated systems.
```

## Root Map Rule

A concrete project should normally have one root use-case map.

The reusable documentation layer owns setup guidance, workflow and template shape. The project root map owns concrete command routing.

Do not create a generic reusable use-case map inside the documentation layer.
