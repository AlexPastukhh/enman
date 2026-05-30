# Enman Source Cascade Sync Workflow

Status: active Enman project workflow  
Doc version: v0.1.0  
Scope: how to add local section-level source blocks, prepare doc version/source synchronization and later derive layer source-sync registers

## 1. Purpose

This workflow explains how Enman tracks source, version and cascade dependencies for active planning drafts.

Primary rule:

```text
Local section-level Sources blocks are the primary working mechanism.
Layer source-sync registers are derived from local section-level Sources blocks.
```

This workflow is Enman-specific. The reusable setup model lives in:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

The Enman project profile lives in:

```text
planning/source-usage-cascade-profile.md
```

## 2. Core Concepts

```text
Doc version:
  The version marker on an active planning/documentation file.

Format/process source:
  A workflow, template, principles file or responsibility map that defines how a section should be shaped or reviewed.

Content source:
  A scenario, DATA, behavior, domain, slice, testing, API or implementation-evidence file that provides section meaning.

Internal dependency:
  A section inside the same draft that the current section depends on.

Local Sources block:
  A fenced text block placed immediately after a section heading.

Layer source-sync register:
  A later aggregate view of source dependencies for one layer, derived from local Sources blocks.
```

## 3. Required Read Order

For Enman source cascade work, read:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/source-usage-cascade-profile.md
planning/documentation/field-kits/source-usage-cascade-field-kit.md
planning/planning-use-case-map.md
```

Then read layer-specific workflow/template/principles files.

For domain aggregate drafts:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-modeling-principles.md
planning/domain/domain-discovery-workflow.md
planning/domain/aggregate-drafting-workflow.md
planning/domain/aggregate-draft-template.md
planning/domain/scenario-to-aggregate-map.md
```

For domain value object drafts:

```text
planning/domain/value-object-drafting-workflow.md
planning/domain/value-object-draft-template.md
planning/domain/domain-modeling-principles.md
planning/domain/scenario-to-aggregate-map.md
```

For slice drafts:

```text
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-responsibility-map.md
planning/slices/SLICE-INDEX.md
planning/slices/slice-scenario-flow-behavior-register.md
```

For server slice drafts:

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/server-implementation-principles.md
planning/slices/slice-test-plan-workflow.md
planning/testing/server-slice-test-plan-rules.md
```

## 4. Workflow

Use this workflow for one active draft file at a time.

```text
1. Select one active draft file.
2. Identify its layer and file type.
3. Read the file itself.
4. Read the file-type workflow, template, principles and responsibility map.
5. Identify which sections should have local Sources blocks.
6. For each section, identify:
   - format/process sources;
   - content sources;
   - internal dependencies;
   - explicitly not-checked evidence/sources.
7. Insert the fenced text Sources block immediately after the section heading.
8. Update section content only when source review shows a mismatch.
9. Add or bump Doc version when the active file is materially changed.
10. Later, derive layer source-sync register rows from local Sources blocks.
```

Do not start with a global register and then guess local dependencies. Start from the draft section, because the workflow/template defines what each section needs.

## 5. Local Sources Block Placement

Use this placement:

````markdown
## <Section Heading>

```text
Sources:
  Format/process:
    - <workflow/template/principles/responsibility-map file>
  Content:
    - <scenario/domain/slice/testing/API source file or section>
  Internal dependencies:
    - <section in this same draft>
  Not checked:
    - <explicitly unchecked source/evidence>
```

<section body>
````

Rules:

```text
- Put no prose between the section heading and the fenced Sources block.
- Keep sources local to the section.
- Do not dump every known file into every section.
- If a source was not checked but should matter, put it under Not checked.
- If a source only defines format/process, do not list it as content.
- If a source provides business/domain/testing meaning, do not list it as format/process.
```

## 6. Scenario DATA Rule

Scenario-specific DATA starts inline in the core/business scenario text spec.

```text
Primary scenario DATA source:
  planning/diagrams/scenario-text-specs/<SCENARIO>.md#DATA
```

Use `planning/diagrams/scenario-data/` only for:

```text
- reusable DATA concepts shared by multiple scenarios;
- DATA concepts too large for one scenario text spec;
- separately audited DATA concepts;
- downstream reusable references;
- transitional sidecars that have not yet been merged or reclassified.
```

Do not treat a scenario DATA sidecar as the primary DATA source unless the scenario or current mapping explicitly makes it the reusable/shared/audited source.

## 7. Domain Draft Application

For aggregate drafts, local Sources blocks usually belong after these sections:

```text
## 1. Purpose
## 2. Source Inputs
## 3. Aggregate Boundary
## 4. Owned State
## 5. Domain Methods / Commands
## 6. Invariants
## 7. Lifecycle / State Machine
## 8. Impossible States Prevented
## 9. Value Objects Used
## 10. Cross-Aggregate Relations
## 11. Behavior Coverage
## 12. Persistence / EF Notes
## 13. Cross-Layer Placement Notes
## 14. Questions / Decisions
```

The `Source Inputs` section is an overview. It is not the only source authority.

Section-level Sources blocks are authoritative for local section work.

## 8. Slice Draft Application

For slice drafts, local Sources blocks usually belong after sections such as:

```text
Scenario Scope / Slice Boundary
Domain Methods / Domain Behavior Contract
Implementation Components Overview
Implementation Flow
API Contract
Behavior Coverage
Test / Verification Plan
Questions / Decisions
```

A slice draft may keep a short pointer to the future layer source-sync register, but section-level Sources blocks should remain local to the section.

## 9. Doc Version Rule

Every active planning/documentation file that participates in source cascade should eventually have:

```text
Doc version: v0.1.0
```

Do not add versions to the entire repository in one broad pass.

Add or bump versions only for files touched by the current cascade sync task.

Suggested bump meaning:

```text
patch:
  wording, formatting or typo; dependency meaning unchanged

minor:
  section meaning, source interpretation, behavior/domain/slice content changed

major:
  responsibility, ownership, workflow rule, template shape or source contract changed
```

## 10. Register Derivation

Later layer registers should be derived from local Sources blocks.

Planned domain register:

```text
planning/domain/domain-source-sync-register.md
```

Planned slice register:

```text
planning/slices/slice-source-sync-register.md
```

Register row shape should include:

```text
consumer file
consumer doc version
consumer section
source file
source doc version used
source role: format-process / content / internal
sync status
review outcome
last reviewed
notes
```

Do not create or fill a layer register until at least one representative draft has local Sources blocks that prove the shape.

## 11. First Intended Application

First vertical chain:

```text
SC-13D
  -> AgreementProposalExchange aggregate draft
  -> SL-AGR-EXCH-001 slice draft
```

First practical application should start with:

```text
planning/domain/aggregates/agreement-proposal-exchange.md
```

Then apply the same pattern to:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```
