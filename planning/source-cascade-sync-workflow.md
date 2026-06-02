# Enman Source Cascade Sync Workflow

Status: active Enman project workflow
Doc version: v0.5.0
Scope: how to add local section-level source blocks, use source-sync register skeletons safely, prepare doc version/source synchronization, derive layer source-sync registers and decide when structured files require local section sources

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - none
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

This workflow explains how Enman tracks source, version and cascade dependencies for active planning drafts.

Primary rules:

```text
Local section-level Sources blocks are the primary working mechanism.
Layer source-sync registers are navigation/sync indexes.
A layer register may be skeleton, derived or synchronized.
A skeleton register is allowed only when it is explicitly marked incomplete and does not claim full source coverage.
Structured files with stable semantic sections need local section-level Sources blocks when those sections rely on real external sources or meaning-bearing internal dependencies.
```

This workflow is Enman-specific. The reusable setup model lives in:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

The Enman project profile lives in:

```text
planning/source-usage-cascade-profile.md
```

Root planning/source-governance dependencies are tracked from:

```text
planning/root-source-sync-register.md
```

## 2. Core Concepts

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
Doc version:
  The version marker on an active planning/documentation file.

Format/process source:
  A workflow, template, principles file or responsibility map that defines how a section should be shaped or reviewed.

Content source:
  A scenario, DATA, behavior, domain, slice, testing, API or implementation-evidence file that provides section meaning.

Internal dependency:
  A section inside the same draft that the current section depends on.

Structured section file:
  A file with stable semantic sections where different sections may depend on different source sets.

Local Sources block:
  A fenced text block placed immediately after a section heading.

Layer source-sync register:
  A layer-level navigation/sync index of dependencies between files or sections.

Register state:
  skeleton:
    Logical register shape and known dependency candidates exist, but full local source coverage has not been proven.
  derived:
    Register rows were derived from at least one reviewed local Sources pass.
  synchronized:
    Register rows and local Sources blocks were compared and aligned with current source version/status labels.
```

## 3. Required Read Order

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Core Concepts
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

For Enman source cascade work, read:

```text
planning/source-cascade-sync-workflow.md
planning/root-source-sync-register.md when root planning/workflow/router files are involved
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md when adding/reviewing aggregate draft section Sources blocks
planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md when adding/reviewing server/backend/API slice draft section Sources blocks
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
planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-source-sync-register.md
```

For domain value object drafts:

```text
planning/domain/value-object-drafting-workflow.md
planning/domain/value-object-draft-template.md
planning/domain/domain-modeling-principles.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-source-sync-register.md when downstream aggregate/slice sync impact is involved
```

For slice drafts:

```text
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-responsibility-map.md
planning/slices/SLICE-INDEX.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/domain/domain-source-sync-register.md when the slice depends on domain aggregate/value-object sources
planning/slices/slice-source-sync-register.md once created
```

For server slice drafts:

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md
planning/slices/server-implementation-principles.md
planning/slices/slice-test-plan-workflow.md
planning/testing/server-slice-test-plan-rules.md
planning/domain/domain-source-sync-register.md when domain dependencies are in scope
planning/slices/slice-source-sync-register.md once created
```

## 4. Workflow

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Core Concepts
    - Required Read Order
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Use this workflow for one active draft/file at a time.

```text
1. Select one active draft/file.
2. Identify its layer and file type.
3. Read the file itself.
4. Read the file-type workflow, template, principles and responsibility map.
5. If a layer/root source-sync register exists, read its status/scope before editing.
6. Identify which sections should have local Sources blocks, which files can use file-level source audit and which register rows should exist.
7. For each section/row, identify:
   - format/process sources;
   - content sources;
   - internal dependencies;
   - explicitly not-checked evidence/sources;
   - source version/status label when known.
8. Insert the fenced text Sources block immediately after the section heading when doing local section work.
9. Update section content only when source review shows a mismatch.
10. Add or bump Doc version when the active file is materially changed.
11. Update or create the relevant register row only within the register's declared state/scope.
12. Later, derive/synchronize layer source-sync register rows from local Sources blocks.
```

Do not start with a global filled register and then guess local dependencies. Start from the file/section and its workflow/template when filling authoritative rows. A skeleton register may exist first, but it must say which rows are provisional and what local source pass is still needed.

## 4A. Structured File Local Sources Rule

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Core Concepts
    - Workflow
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Use local section-level `Sources:` blocks when all of these are true:

```text
- the file has stable semantic sections;
- different sections may rely on different source sets or internal dependencies;
- the file participates in source/version/cascade work;
- later downstream work needs to know which source was used for which section.
```

This rule applies to:

```text
- active domain aggregate drafts;
- domain value object drafts with structured sections;
- slice drafts;
- structured root workflow/router/template files when section-level sources are meaningful;
- other structured planning files when they consume scenario/domain/slice/testing/API/implementation evidence.
```

A register row alone is not enough for section-level work when a structured file has meaningful section-specific dependencies. A file-level dependency audit can be enough only for flat logs, simple indexes, registers, or files where section-level sources would be artificial noise.

## 5. Local Sources Block Placement

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Internal dependencies:
    - Core Concepts
    - Workflow
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Use this placement:

````markdown
## <Section Heading>

```text
Sources:
  Format/process:
    - <workflow/template/principles/responsibility-map file> @ <Doc version/status>
  Content:
    - <scenario/domain/slice/testing/API source file or section> @ <Doc version/status>
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
- Every listed source should include a source version/status label.
- When a source file has a Doc version, include that version/status label in local Sources blocks.
- When a source file is unversioned, historical, prior evidence or implementation-only, mark that explicitly instead of inventing a version.
```

## 5A. Explicit Link / Dependency Declaration Rule

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Core Concepts
    - Workflow
    - Structured File Local Sources Rule
    - Local Sources Block Placement
  Not checked:
    - full root/router local source audit outside the source-dependency command route
    - slice register/read-order/template sync not completed in this pass
```

When a change introduces or points out a file-to-file reference, section dependency, source link or claim that one file uses information from another file, classify it before editing.

Use these dependency classes:

```text
simple navigational link:
  The link helps readers find related material but does not provide meaning, rules, evidence or downstream source truth.

format/process dependency:
  The source defines workflow, template, ownership, output shape, validation process or drafting rules.

content/source dependency:
  The source provides scenario, DATA, behavior, domain, slice, testing, API or documentation meaning used by the target.

internal dependency:
  The target section depends on another section in the same file.

register-index dependency:
  The source is a root/layer source-sync register used to discover or synchronize dependencies.

implementation/evidence dependency:
  The target uses implementation/test/helper evidence and must say whether that evidence was checked in this pass or is prior evidence.
```

If the dependency affects meaning, workflow, template shape, source truth, validation, state, behavior, routing, output mode or downstream sync, record it explicitly in one of these places:

```text
- local section-level Sources block for structured files with section-specific dependencies;
- file-level dependency audit/register row for flat indexes, registers or logs where section-level blocks would be artificial noise;
- relevant root/domain/slice source-sync register when the register scope includes the target file.
```

Then check whether a root/domain/slice register row or sync status needs an update.

Rules:

```text
- Do not treat every markdown link as a source dependency.
- Do not hide a meaning-bearing dependency as a simple link.
- Do not invent Doc versions for unversioned sources.
- Use declared Doc version/status labels or explicit `version not declared`, `version not confirmed`, `historical/cross-check`, `prior evidence` or `implementation/helper source, version not applicable` labels.
- Do not claim a register is synchronized until local Sources blocks or a file-level audit prove the row.
- If the user only asks to identify the dependency, report the classification and register impact without editing files.
```

## 6. Scenario DATA Rule

```text
Sources:
  Format/process:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/diagrams/scenario-text-specs/**#DATA @ Doc version: v0.1.0 after F7K-D2
    - planning/diagrams/scenario-data/** @ Doc version: v0.1.0 after F7K-D2
  Internal dependencies:
    - Core Concepts
    - Local Sources Block Placement
  Not checked:
    - scenario files were not re-audited in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Required Read Order
    - Local Sources Block Placement
    - Register State And Derivation
  Not checked:
    - domain aggregate drafts were not edited in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

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

For active aggregate drafts, also check:

```text
planning/domain/domain-source-sync-register.md
```

The domain register is the current synchronized dependency index for active aggregate local Sources blocks. It does not replace the local section-level Sources blocks.

## 7A. Domain Value Object Draft Application

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Required Read Order
    - Domain Draft Application
    - Local Sources Block Placement
  Not checked:
    - domain value-object drafts were not edited in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

For value object drafts, local Sources blocks usually belong after these sections:

```text
## 1. Purpose
## 2. Source Inputs
## 3. Used By
## 4. Shape / Fields
## 5. Invariants
## 6. Creation / Normalization Rules
## 7. Equality Rule
## 8. Validation Boundary
## 9. Persistence / Serialization Notes
## 10. Invalid Examples
## 11. Questions / Decisions
## 12. Source Delta / Change Log
```

Use local Sources blocks for value objects because their sections commonly depend on different source sets:

```text
Purpose / Source Inputs:
  scenario, behavior, aggregate and historical discovery sources.

Used By:
  aggregates, entities, API/client references and implementation evidence.

Shape / Invariants / Creation / Equality / Validation:
  behavior items, aggregate usage, domain modeling principles and implementation evidence when checked.

Persistence / Serialization:
  aggregate persistence notes, implementation evidence or Not checked when not reviewed.
```

Do not leave a generic `Full source/version/cascade metadata not checked` note after a value-object source coverage pass. Replace it with explicit `Not checked` items in the relevant local Sources blocks.

When value-object local Sources are added, update `planning/domain/domain-source-sync-register.md` only within an explicitly declared value-object coverage state. Do not claim full domain-folder coverage until all relevant domain files have been reviewed.

## 8. Slice Draft Application

```text
Sources:
  Format/process:
    - planning/slices/slice-draft-authoring-workflow.md @ version not confirmed
    - planning/slices/slice-draft-authoring-principles.md @ version not confirmed
    - planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md @ version not confirmed
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Required Read Order
    - Domain Draft Application
    - Domain Value Object Draft Application
  Not checked:
    - slice drafts were not edited in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

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

A slice draft may keep a short pointer to the slice layer source-sync register once it exists, but section-level Sources blocks should remain local to the section.

When a slice depends on domain aggregate/value-object behavior, check:

```text
planning/domain/domain-source-sync-register.md
```

Use the domain register as an upstream dependency index, then read the relevant aggregate/value-object local sources directly.

## 9. Doc Version Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
  Internal dependencies:
    - Core Concepts
    - Register State And Derivation
  Not checked:
    - full root Doc version seed was not performed in this pass
```

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

## 9A. Active File Creation / Update Version And Register Checks

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
  Internal dependencies:
    - Doc Version Rule
    - Register State And Derivation
  Not checked:
    - repository-wide stale version usage scan
    - repository-wide local Sources stale-source scan
```

When creating an active planning/documentation/source file:

```text
1. Decide whether the file is active, historical, implementation/helper or example-only.
2. If it is an active planning/documentation source, add a `Doc version:` marker unless the file type has a separate implementation version policy.
3. Decide whether the file has external source dependencies or meaning-bearing internal dependencies.
4. If yes, add local section-level `Sources:` blocks or a narrow file-level source sync section.
5. Check whether the relevant register needs a new row:
   - planning/root-source-sync-register.md for root planning/workflow/router/helper files;
   - planning/domain/domain-source-sync-register.md for domain aggregate/value-object sources;
   - planning/slices/slice-source-sync-register.md once created.
6. Do not claim synchronized coverage until local source blocks or a file-level dependency audit were actually checked.
```

When updating an active planning/documentation/source file:

```text
1. Check whether the update changes source relationships, command semantics, output rules, routes, templates, registers or workflow read order.
2. If the source relationship changed, update the local `Sources:` block or file-level source sync section.
3. If a referenced source version changed, refresh the local version label and the relevant register row.
4. If the file becomes a source for another file, add or update the consumer's local Sources block/register row.
5. If the update only changes wording without source relationship impact, record that no register change is needed when the batch is reviewed.
```

Future maintenance commands should be added later, not in this ROOT-FULL-1 batch:

```text
стейл версии в регистрах:
  check source-sync registers for stale source/version usage.

стейл локальные сорсы:
  check local `Sources:` blocks in active files/drafts for stale versions, stale paths and stale source relationships.

полная source/version проверка:
  combine register scan + local Sources scan + active-file Doc version scan.
```

## 10. Register State And Derivation

```text
Sources:
  Format/process:
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Core Concepts
    - Workflow
  Not checked:
    - full root local/file-level source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Current root register:

```text
planning/root-source-sync-register.md
  Status: skeleton until root planning/workflow/router files are fully audited and/or local source coverage is added where useful.
```

Current domain register:

```text
planning/domain/domain-source-sync-register.md
  Status: synchronized with versioned active aggregate local Sources blocks and derived value-object local Sources blocks.
```

Planned slice register:

```text
planning/slices/slice-source-sync-register.md
  Status: not created yet; create as skeleton before the first broad slice refactor if needed.
```

Register row shape should include:

```text
consumer file
consumer doc version/status
consumer section or responsibility scope
source file
source doc version/status used
source role: format-process / content / internal / register-index
sync status
review outcome
last reviewed
notes
```

Register state rules:

```text
Skeleton register:
  allowed before full local Sources coverage;
  must be explicitly marked skeleton/incomplete;
  may list candidate dependencies and planned rows;
  must not claim source coverage or semantic review.

Derived register:
  allowed after at least one representative local Sources pass proves row shape;
  must name the local source blocks/files it was derived from.

Synchronized register:
  allowed only after local Sources blocks and register rows were compared and aligned with current path/version/status labels.
```

Do not fill a register as complete/synchronized until local Sources blocks or equivalent file-level audit prove the rows.

## 11. First Intended Application

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Required Read Order
    - Domain Draft Application
    - Domain Value Object Draft Application
    - Slice Draft Application
  Not checked:
    - slice register/read-order/template sync not completed in this pass
```

First vertical chain:

```text
SC-13D
  -> AgreementProposalExchange aggregate draft
  -> AgreementProposalExchange value objects
  -> SL-AGR-EXCH-001 slice draft
```

Current state:

```text
- domain aggregate/register part is complete enough for domain-side source/version/cascade review;
- active value-object local Sources pass is complete enough on domain side and reflected in domain register v0.2.0;
- root register skeleton exists for root workflow/router dependencies;
- slice register is still planned;
- first slice-side application should start only after slice read order/template/register skeleton are aligned with the domain register and the relevant value-object sources are covered.
```

First slice-side application should target:

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```

## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Explicit Link / Dependency Declaration Rule
    - Register State And Derivation
  Not checked:
    - root/router local source coverage outside this route update
    - slice source-sync register/read-order/template sync
```

```text
- SRC-DEP-CMD-1 added an explicit link/dependency declaration rule for file-to-file and section-to-file dependencies.
- SRC-DEP-CMD-1 clarified that meaning-bearing dependencies need a local `Sources:` block or file-level/register audit decision, plus relevant register impact check.
- Bumped this workflow to Doc version: v0.4.0 because the source dependency command route adds an explicit workflow rule.
- ROOT-FULL-1 added active file creation/update version and register checks and parked future stale register/local Sources scan commands.
```
