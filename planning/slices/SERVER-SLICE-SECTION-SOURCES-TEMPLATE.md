# Server Slice Section Sources Template

Status: active Enman server slice section source template  
Doc version: v0.1.0  
Scope: usual local `Sources:` blocks for sections in server/backend/API slice drafts

Use with:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-responsibility-map.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/server-implementation-principles.md
planning/slices/slice-test-plan-workflow.md
planning/testing/server-slice-test-plan-rules.md
```

## 1. Purpose

Use this template when adding local section-level `Sources:` blocks to an active server/backend/API slice draft.

This file does not replace `planning/slices/server/SERVER-SLICE-TEMPLATE.md`. It explains which sources usually belong to each server slice draft section.

Rules:

```text
- Use the exact fenced text block format from planning/SOURCE-SECTION-SOURCES-TEMPLATE.md.
- Replace placeholders with concrete repo-relative paths.
- Do not list a source as checked unless it was actually read.
- If a source should matter but was not checked, list it under Not checked.
- Scenario-specific DATA usually comes from the scenario text spec #DATA section.
- Keep each block local to the section.
- Do not invent behavior locally inside a slice when source behavior files exist.
```

## 2. Section Source Defaults

### 2.1 `## 0. Scenario Sources / Source Sync`

Usually use:

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - planning/slices/SLICE-INDEX.md
    - planning/slices/slice-scenario-flow-behavior-register.md
    - <scenario text specs>
    - <scenario text spec #DATA for scenario-specific DATA>
    - <scenario behavior items>
    - <domain aggregate/value object drafts>
    - <API/generated contract sources when relevant>
    - <testing/source workflow when verification is non-trivial>
  Internal dependencies:
    - none
  Not checked:
    - <current code/tests unless implementation sync is in scope>
```

Use this section as source overview only. Section-level `Sources:` blocks below are authoritative for local section work.

### 2.2 `## 1. Scope`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/slice-draft-authoring-principles.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - planning/slices/SLICE-INDEX.md
    - planning/slices/slice-scenario-flow-behavior-register.md
    - <scenario text spec>
    - <scenario behavior items>
  Internal dependencies:
    - Scenario Sources / Source Sync
  Not checked:
    - <related scenario behavior not in current slice if not reviewed>
```

Use this section to define the behavior subset, not a full scenario dependency map.

### 2.3 `## 2. Scenario Scope / Slice Boundary`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/slice-draft-authoring-principles.md
    - planning/slices/slice-responsibility-map.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - planning/slices/SLICE-INDEX.md
    - planning/slices/slice-scenario-flow-behavior-register.md
    - planning/diagrams/scenario-artifact-map.md
    - <scenario text spec>
    - <scenario text spec #DATA for scenario-specific DATA>
    - <scenario behavior items>
    - <cross-cutting behavior/concern sources if applicable>
  Internal dependencies:
    - Scenario Sources / Source Sync
    - Scope
  Not checked:
    - <current runtime implementation unless status/current sync is in scope>
```

Use this section to state what scenario/source behavior this slice implements and what it explicitly does not implement.

### 2.4 `## 3. Slice Relations And Future-Change Check`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/slice-draft-authoring-principles.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
  Content:
    - planning/slices/SLICE-INDEX.md
    - planning/slices/slice-scenario-flow-behavior-register.md
    - <known prerequisite/follow-up slice drafts>
    - <scenario behavior items that imply future related slices>
  Internal dependencies:
    - Scenario Scope / Slice Boundary
  Not checked:
    - <future slices not reviewed in this pass>
```

Use this section to account for dependencies without expanding current scope.

### 2.5 `## 4. Domain Methods / Domain Behavior Contract`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/domain/domain-modeling-principles.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - <relevant aggregate draft>
    - <relevant value object drafts>
    - <scenario behavior items that require domain behavior>
    - Scenario Scope / Slice Boundary
  Internal dependencies:
    - Scenario Scope / Slice Boundary
  Not checked:
    - <current domain implementation unless implementation sync is in scope>
```

Use this section to show what domain behavior the slice calls or relies on. Do not move domain invariants into controller/validator.

### 2.6 `## 5. Implementation Components Overview`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Domain Methods / Domain Behavior Contract
    - API Contract, if already drafted
    - <current implementation files only if explicitly reviewed>
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
  Not checked:
    - <current controller/service/repository names unless implementation sync is in scope>
```

Use this section for responsibility boundaries, not final runtime names.

### 2.7 `## 6. Implementation Flow`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Implementation Components Overview
    - Domain Methods / Domain Behavior Contract
    - API Contract
    - DTO Validation
    - Repository / Persistence Notes
    - <current code only if implementation sync is in scope>
  Internal dependencies:
    - Implementation Components Overview
    - Domain Methods / Domain Behavior Contract
    - API Contract
  Not checked:
    - <current runtime implementation unless explicitly reviewed>
```

Use this section to describe responsibility flow by owner.

### 2.8 `## 7. API Contract`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Scenario Scope / Slice Boundary
    - <scenario text spec #DATA for request/response DATA>
    - Domain Methods / Domain Behavior Contract
    - Application Result Model
    - <OpenAPI/generated contract sources if relevant>
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - Application Result Model
  Not checked:
    - <generated artifacts unless explicitly reviewed>
```

Use this section for endpoint/request/response/error contract.

### 2.9 `## 8. Application Result Model`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Domain Methods / Domain Behavior Contract
    - API Contract
    - DTO Validation
    - <domain/application error mapping sources if present>
  Internal dependencies:
    - Domain Methods / Domain Behavior Contract
    - API Contract
  Not checked:
    - <current application result implementation unless explicitly reviewed>
```

Use this section for success/failure mapping.

### 2.10 `## 9. DTO Validation`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
    - <validation cross-cutting source if applicable>
  Content:
    - <scenario text spec #DATA for request shape>
    - API Contract
    - Domain Methods / Domain Behavior Contract
  Internal dependencies:
    - API Contract
    - Domain Methods / Domain Behavior Contract
  Not checked:
    - <current validator implementation unless explicitly reviewed>
```

Use this section to separate request shape validation from domain/application lifecycle rules.

### 2.11 `## 10. Repository / Persistence Notes`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
    - <relevant aggregate draft>
    - <current repository/persistence implementation only if explicitly reviewed>
  Internal dependencies:
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
  Not checked:
    - <current persistence implementation unless explicitly reviewed>
```

Use this section for load/save requirements and no-partial-write behavior.

### 2.12 `## 11. Cross-Cutting Concerns / Considerations`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Scenario Scope / Slice Boundary
    - Implementation Flow
    - API Contract
    - <cross-cutting concern sources that apply to this slice>
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Implementation Flow
    - API Contract
  Not checked:
    - <cross-cutting concerns not reviewed in this pass>
```

Use this section for implementation-level concerns only. Scenario-level concern applicability belongs in Scenario Scope / Slice Boundary.

### 2.13 `## 12. Questions / Decisions`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - <source conflicts from scenario/domain/API/testing sections>
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - API Contract
    - Behavior Coverage
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - Behavior Coverage
  Not checked:
    - <sources needed to resolve open questions but not read>
```

Use this section to keep uncertainty explicit.

### 2.14 `## 13. Behavior Coverage`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - <scenario behavior items>
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
    - API Contract
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
  Not checked:
    - <behavior items from out-of-scope related scenarios unless included by slice scope>
```

Use this section to prove source behavior is covered, delegated, out of scope, future, blocked or not applicable.

### 2.15 `## 14. Test / Verification Plan`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-test-plan-workflow.md
    - planning/testing/server-slice-test-plan-rules.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - <scenario behavior items>
    - Behavior Coverage
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
    - API Contract
    - Repository / Persistence Notes
  Internal dependencies:
    - Behavior Coverage
    - Domain Methods / Domain Behavior Contract
    - Implementation Flow
    - API Contract
  Not checked:
    - <actual test files unless explicitly read>
```

Use this section to prove behavior and observable outcomes, not implementation details as the reason for the test.

### 2.16 `## 15. Backend Implementation Direction / Sync Checklist`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
    - planning/documentation/status-reconciliation-workflow.md
  Content:
    - Implementation Flow
    - API Contract
    - DTO Validation
    - Repository / Persistence Notes
    - Test / Verification Plan
    - <current code/tests/generated artifacts only if implementation sync is in scope>
  Internal dependencies:
    - Implementation Flow
    - API Contract
    - Test / Verification Plan
  Not checked:
    - <runtime implementation when this is a planning-only draft>
```

Use this section only when preparing implementation or syncing with already implemented code.

### 2.17 `## 16. Local / Global Sync Check`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
  Content:
    - planning/slices/SLICE-INDEX.md
    - planning/slices/slice-scenario-flow-behavior-register.md
    - planning/slices/slice-questions-register.md
    - planning/slices/slice-extension-points-register.md
    - planning/slices/slice-implementation-notes-register.md
    - planning/slices/slice-responsibility-map.md
    - planning/slices/README.md
    - <changed sections in this slice draft>
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - <registers not reviewed in this pass>
```

Use this section to decide whether layer/global files need updates.

### 2.18 `## 17. Guardrail Summary`

Usually use:

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md
    - planning/slices/slice-draft-authoring-principles.md
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md
    - planning/slices/server-implementation-principles.md
  Content:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - DTO Validation
    - Behavior Coverage
    - Test / Verification Plan
  Internal dependencies:
    - Scenario Scope / Slice Boundary
    - Domain Methods / Domain Behavior Contract
    - Behavior Coverage
    - Test / Verification Plan
  Not checked:
    - <none, unless guardrail source files were not reviewed>
```

Use this section to summarize the most important slice guardrails.
