# Scenario / Domain / Slice Command Routing Example

Status: current Enman-specific command-routing example / demonstration-only
Scope: demonstrates how Enman root use-case map routes scenario/domain/slice commands without copying workflow logic

## What This Demonstrates

Owner / routing files:

```text
planning/planning-use-case-map.md
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

Related reusable maintenance rules:

```text
planning/documentation/use-case-map-workflow.md
planning/documentation/example-coverage-workflow.md
```

This example demonstrates valid traversal only. It does not own command semantics, routing, source truth, output mode, permission boundary, scenario drafting workflow, domain workflow, slice workflow or testing workflow.

## Example A — Scenario Source Work

User says:

```text
сделай сценарий / DATA для <feature or behavior>
```

Valid route:

```text
planning/planning-use-case-map.md
  -> Primary row: “сделай сценарий / DATA / UI scenario / behavior items”
  -> planning/diagrams/README.md when diagram/source topology is relevant
  -> scenario responsibility/artifact/drafting docs
  -> relevant scenario/source files
```

Expected output:

```text
Scenario text / DATA / UI scenario / behavior item draft or update plan.
```

Boundary:

```text
- Do not edit files unless explicitly approved.
- Do not treat this example as the scenario drafting workflow.
- Do not skip owner scenario responsibility/drafting docs when the work is non-trivial.
```

## Example B — Domain Work

User says:

```text
разбери domain draft / domain discovery / aggregate draft
```

Valid route:

```text
planning/planning-use-case-map.md
  -> Primary row: “разбери domain draft / domain discovery / aggregate draft”
  -> planning/domain/README.md
  -> planning/domain/domain-responsibility-map.md
  -> planning/domain/scenario-to-aggregate-map.md
  -> relevant scenario/domain sources
```

Expected output:

```text
Domain discovery map, aggregate draft, value-object draft or review plan with local Sources blocks when requested/in scope.
```

Boundary:

```text
- Do not use old domain drafts as current truth without marking them historical/cross-check when applicable.
- Do not add local Sources/version fields outside the accepted source cascade/local Sources model.
- File writes require approval.
```

## Example C — Slice / Server / Client Work

User says:

```text
задрафти slice / server slice / client sidecar
```

Valid route:

```text
planning/planning-use-case-map.md
  -> Primary slice/server/client row
  -> slice README / responsibility map / relevant slice drafting workflows
  -> server/client templates and principles by target
  -> source cascade/local Sources templates when requested or in scope
  -> testing selector/docs when test impact is relevant
```

Expected output:

```text
Slice draft, server/backend/API draft, client sidecar draft, or scoped plan with local Sources blocks when requested/in scope.
```

Boundary:

```text
- Do not silently broaden server slice work into client/testing implementation unless requested or required by the selected workflow.
- Do not skip testing responsibility map when test-layer-specific guidance is needed.
- File writes require approval.
```

## Example D — Testing Route

User says:

```text
разбери тестирование / test plan / проверь тестирование
```

Valid route:

```text
planning/planning-use-case-map.md
  -> Primary testing row
  -> planning/testing/README.md
  -> planning/testing/testing-responsibility-map.md
  -> planning/testing/testing-principles.md
  -> specific testing docs selected by scope
```

Expected output:

```text
Testing audit, testing plan, or testing responsibility clarification.
```

Boundary:

```text
- Do not claim current implementation/test status without current evidence.
- Do not write or generate tests unless explicitly requested and routed through the implementation/output mode.
```

## Why This Is Valid

```text
- The concrete Enman root use-case map owns the route rows.
- The scenario/domain/slice field kit suggests route families but is not a second use-case map.
- This file is project-specific demonstration-only.
- It links owner files instead of copying their workflows.
- It keeps file writes/package/direct edits behind explicit approval.
```
