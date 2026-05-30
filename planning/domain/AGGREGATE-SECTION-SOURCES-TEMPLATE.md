# Aggregate Section Sources Template

Status: active Enman aggregate section source template  
Doc version: v0.1.0  
Scope: usual local `Sources:` blocks for sections in domain aggregate drafts

Use with:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/domain/aggregate-drafting-workflow.md
planning/domain/aggregate-draft-template.md
planning/domain/domain-modeling-principles.md
planning/domain/domain-responsibility-map.md
planning/domain/scenario-to-aggregate-map.md
```

## 1. Purpose

Use this template when adding local section-level `Sources:` blocks to an active domain aggregate draft.

This file does not replace `planning/domain/aggregate-draft-template.md`. It explains which sources usually belong to each aggregate draft section.

Rules:

```text
- Use the exact fenced text block format from planning/SOURCE-SECTION-SOURCES-TEMPLATE.md.
- Replace placeholders with concrete repo-relative paths.
- Do not list a source as checked unless it was actually read.
- If a source should matter but was not checked, list it under Not checked.
- Scenario-specific DATA usually comes from the scenario text spec #DATA section.
- Use planning/diagrams/scenario-data/ only for reusable/shared/audited/transitional DATA sidecars.
- Keep each block local to the section.
```

## 2. Section Source Defaults

### 2.1 `## 1. Purpose`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <scenario text specs that introduce this aggregate behavior>
    - <scenario clarifications / terminology decisions that affect aggregate meaning>
  Internal dependencies:
    - none
  Not checked:
    - <implementation/code/tests unless explicitly reviewed>
```

Use this section to establish what business behavior the aggregate owns.

### 2.2 `## 2. Source Inputs`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/diagrams/scenario-data/README.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <scenario text specs that provide behavior and inline #DATA>
    - <scenario behavior items mapped to this aggregate>
    - <relevant clarifications/questions>
    - <relevant domain decisions>
    - <relevant old monolithic domain drafts, if still used>
    - <relevant value object drafts>
  Internal dependencies:
    - none
  Not checked:
    - <sources expected but not read in this pass>
```

`Source Inputs` is an overview. It is not the only source authority. Section-level `Sources:` blocks below are authoritative for local section work.

### 2.3 `## 3. Aggregate Boundary`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <scenario behavior items mapped to this aggregate>
    - <scenario clarifications that affect ownership/boundary>
    - <related aggregate drafts used only as external references>
  Internal dependencies:
    - Purpose
  Not checked:
    - <current runtime implementation unless boundary is being reconciled with code>
```

Use this section to distinguish owned aggregate behavior from external aggregates, application coordination, UI/API concerns and read-model/query concerns.

### 2.4 `## 4. Owned State`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <scenario text spec #DATA when state comes from scenario information needs>
    - <scenario behavior items that require state/lifecycle>
    - <relevant value object drafts>
    - <related aggregate drafts for external ids/references only>
  Internal dependencies:
    - Aggregate Boundary
  Not checked:
    - <persistence mapping / EF configuration unless explicitly reviewed>
```

Use this section for state owned by the aggregate, not for DTO shape, UI state or persistence convenience.

### 2.5 `## 5. Domain Methods / Commands`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <scenario text specs that define command inputs/results>
    - <scenario text spec #DATA for scenario-specific command input/result DATA>
    - <scenario behavior command/lifecycle items mapped to this aggregate>
    - <relevant value object drafts>
    - <related aggregate drafts for preconditions/coordination only>
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
  Not checked:
    - <application service orchestration unless explicitly reviewed>
    - <implementation code/tests unless explicitly reviewed>
```

For large command sections, individual methods may also get a smaller local `Sources:` block when each method depends on different behavior items.

### 2.6 `## 6. Invariants`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - <scenario behavior invariant/impossible-state/validation items mapped to this aggregate>
    - <relevant value object drafts>
    - <related aggregate drafts only for cross-aggregate preconditions>
    - Domain Methods / Commands
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Domain Methods / Commands
  Not checked:
    - <runtime enforcement / tests unless explicitly reviewed>
```

Use this section to show aggregate-owned protection rules, not DTO validation or controller checks.

### 2.7 `## 7. Lifecycle / State Machine`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - <scenario behavior lifecycle items mapped to this aggregate>
    - Domain Methods / Commands
    - Invariants
    - <value object drafts that define state/version concepts>
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - <other scenario branches not included by mapping>
```

Use this section only for aggregate-owned states/transitions.

### 2.8 `## 8. Impossible States Prevented`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - <scenario behavior impossible-state / validation / guardrail items>
    - Invariants
    - Lifecycle / State Machine
    - <relevant value object drafts>
  Internal dependencies:
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Not checked:
    - <runtime code/tests unless explicitly reviewed>
```

Use this section to make negative guarantees visible.

### 2.9 `## 9. Value Objects Used`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/domain/value-object-drafting-workflow.md
    - planning/domain/value-object-draft-template.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <relevant value object drafts>
    - <scenario behavior items that require the value object>
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - <current runtime implementation unless explicitly read>
```

Use this section to link reusable/non-trivial value concepts to their own files.

### 2.10 `## 10. Cross-Aggregate Relations`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <related aggregate drafts>
    - <scenario behavior items that require cross-aggregate coordination>
    - <clarifications/decisions about ownership boundaries>
  Internal dependencies:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - <application service implementation unless explicitly reviewed>
```

Use this section to separate aggregate-owned rules from application coordination.

### 2.11 `## 11. Behavior Coverage`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
  Content:
    - <scenario behavior items mapped to this aggregate>
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
    - Cross-Aggregate Relations
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
  Not checked:
    - <behavior items from unrelated scenario branches unless included by mapping>
```

Use this section to prove behavior items are covered, partial, outside the aggregate or deferred.

### 2.12 `## 12. Persistence / EF Notes`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - Owned State
    - Value Objects Used
    - Aggregate Boundary
    - <implementation/persistence evidence only if explicitly checked>
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Value Objects Used
  Not checked:
    - <current EF configuration unless explicitly reviewed>
```

Use this section only when persistence notes are needed. Do not let EF navigation convenience define domain ownership.

### 2.13 `## 13. Cross-Layer Placement Notes`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/domain-responsibility-map.md
    - planning/domain/domain-modeling-principles.md
    - planning/domain/aggregate-drafting-workflow.md
  Content:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Cross-Aggregate Relations
    - Behavior Coverage
    - planning/domain/scenario-to-aggregate-map.md
  Internal dependencies:
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - Behavior Coverage
  Not checked:
    - <API/client/slice implementation unless explicitly reviewed>
```

Use this section to say what belongs to application, API, client, testing or other layers.

### 2.14 `## 14. Questions / Decisions`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
  Content:
    - <scenario questions / clarifications>
    - <behavior item questions>
    - <unresolved source conflicts from this aggregate draft>
    - Behavior Coverage
    - Cross-Aggregate Relations
  Internal dependencies:
    - Source Inputs
    - Behavior Coverage
    - Cross-Aggregate Relations
  Not checked:
    - <sources needed to resolve open questions but not read>
```

Use this section to keep uncertainty explicit.

### 2.15 `## 15. Source Delta / Change Log`

Usually use:

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/source-cascade-sync-workflow.md
  Content:
    - <sources changed or newly reviewed in this update>
    - <sections changed by the source delta>
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - <downstream consumers not reviewed in this pass>
```

Use this section to record why the aggregate draft changed in this pass.
