# Enman Source Usage Cascade Profile

Status: active Enman source usage cascade profile
Doc version: v0.1.0
Scope: concrete Enman source/consumer categories, row conventions, register states and cascade triggers

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/documentation/field-kits/source-usage-cascade-field-kit.md @ version not confirmed
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md @ version not confirmed
  Internal dependencies:
    - none
  Not checked:
    - planned planning/slices/slice-source-sync-register.md does not exist yet
    - preserved SC-13D pilot register was not filled in this pass
```

This file defines Enman source/consumer categories, source usage row conventions, register states and cascade-review triggers.

It is a project-level profile.

Reusable setup owner:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

Historical origin:

```text
former reusable candidate workspace
```

Project pilot folder:

```text
planning/source-usage-pilots/
```

Operational drafting workflow/templates:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md
planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md
```

Active/source-register files:

```text
planning/root-source-sync-register.md
planning/domain/domain-source-sync-register.md
planning/slices/slice-source-sync-register.md planned
```

Use the domain/server slice section source templates when adding local `Sources:` blocks during aggregate or server slice draft work.

## 2. Source Categories

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Source category | Enman paths |
|---|---|
| Root planning/router/source-governance source | `planning/README.md`, `planning/workflow-activation-map.md`, `planning/planning-use-case-map.md`, `planning/source-cascade-sync-workflow.md`, `planning/source-usage-cascade-profile.md`, `planning/root-source-sync-register.md` |
| Scenario text/spec source | `planning/diagrams/scenario-text-specs/` |
| Scenario DATA source | primary scenario-specific DATA in `planning/diagrams/scenario-text-specs/<SCENARIO>.md#DATA`; reusable/shared/audited/transitional sidecars in `planning/diagrams/scenario-data/` |
| Scenario behavior item source | `planning/diagrams/scenario-behavior-items/` |
| Scenario clarification source | `planning/diagrams/scenario-clarifications/` |
| Drafting workflow/template/process source | `planning/source-cascade-sync-workflow.md`, `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`, `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md`, `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md` |
| Domain source | domain drafts/decisions/registers where accepted/current |
| Slice source | `planning/slices/`, `planning/slices/l2/`, `planning/slices/cross-cutting/` |
| Architecture/API/client/testing source | `planning/architecture/`, `planning/api/`, `planning/client/`, `planning/testing/` |
| External-output source/consumer | `planning/vkr-clean-reference.md`, `planning/thesis/` |

## 3. Consumer Categories

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Source Categories
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Consumer category | Examples |
|---|---|
| Root/router consumers | planning use-case map, workflow activation map, root/source cascade workflows, replacement package workflow |
| DATA/behavior consumers | derived scenario DATA and behavior item sets |
| Domain consumers | domain drafts/registers that use scenario/DATA/behavior/domain source |
| Slice consumers | slice docs/registers that use scenario/domain/source material |
| API/testing consumers | contracts, error contracts, testing plans and E2E workflows |
| External-output consumers | VKR/thesis materials that consume internal planning docs |

## 4. Source Usage Row Shape

```text
Sources:
  Format/process:
    - planning/documentation/field-kits/source-usage-cascade-field-kit.md @ version not confirmed
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
  Internal dependencies:
    - Source Categories
    - Consumer Categories
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
```

| Field | Enman convention |
|---|---|
| source_id | repo-relative path + optional section/scope |
| source_scope | scenario/data/behavior/domain/slice/API/testing/root section or row |
| source_status | draft, accepted, current, historical, superseded, skeleton, derived, synchronized |
| consumer_id | repo-relative file path |
| consumer_scope | downstream section/decision/claim/register row |
| reviewed_against | commit/date/source version or explicit reviewed source state |
| sync_status | current, stale, needs-review, metadata-only-reviewed, skeleton, not-applicable |
| review_outcome | no-change, update-needed, follow-up, superseded, blocked, local-sources-needed |
| last_reviewed | date/commit/review marker |
| notes | short reason only |

## 4A. Register Coverage Model

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Source Usage Row Shape
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Layer/root source-sync registers may be in one of these states:

```text
skeleton:
  Logical register file exists and names known/candidate dependencies.
  It is incomplete and requires local source passes or file-level audits before it can be treated as derived.

derived:
  Register rows are derived from local Sources blocks or an explicit file-level dependency audit.

synchronized:
  Register rows and local Sources blocks were compared and aligned with current source paths and version/status labels.
```

Coverage rule:

```text
A register should eventually cover every active file in its layer/root scope that has dependencies on another planning file, workflow, register, scenario, domain, slice, testing, API or implementation-evidence source.
```

Do not treat a skeleton register as full coverage.

## 5. Cascade Triggers

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Source Categories
    - Consumer Categories
    - Register Coverage Model
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Run cascade review when:

```text
- scenario/source behavior changes;
- DATA/behavior items change;
- domain interpretation changes;
- slice scope changes;
- workflow/template/register rules change;
- root routing/use-case map changes;
- API/testing contract or evidence changes;
- external-facing VKR/thesis claim depends on changed internal source.
```

## 6. Pilot Scope

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md @ version not confirmed
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Source Categories
    - Consumer Categories
    - Cascade Triggers
  Not checked:
    - preserved SC-13D pilot register was not filled in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Recommended first pilot:

```text
one scenario source -> one DATA/behavior item set -> one domain/slice consumer -> one external/evidence-related claim if relevant
```

Keep the pilot small enough to prove row shape and review value.

## 6A. Preserved Pilot Candidate: SC-13D / AgreementProposalExchange

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md @ version not confirmed
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
  Internal dependencies:
    - Pilot Scope
  Not checked:
    - preserved SC-13D pilot register was not filled in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

Earlier candidate governance notes identified a concrete Enman pilot candidate:

```text
SC-13D / AgreementProposalExchange source usage pilot
```

Target chain:

```text
SC-13D scenario text / DATA / behavior items
  -> scenario artifact/currentness map
  -> scenario-to-aggregate map
  -> AgreementProposalExchange aggregate draft
  -> related value object drafts
  -> domain-source-sync-register
  -> SL-AGR-EXCH-001 server slice draft
  -> slice Behavior Coverage and Test / Verification Plan sections
```

Earlier skeleton register path:

```text
planning/source-usage-pilots/SC-13D-agreement-exchange-source-usage-register.md
```

This profile is active project configuration, but the SC-13D pilot itself is **not** an active filled register yet.

A later source-usage review should decide whether to run, revise or archive that pilot.

Detailed project-specific example:

```text
planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
```

## 7. Do Not

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Source Categories
    - Consumer Categories
    - Register Coverage Model
    - Cascade Triggers
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
```

```text
- Do not create broad cascade review for every docs change by default.
- Do not copy upstream truth into downstream files just for self-containment.
- Do not treat version markers as source truth by themselves.
- Do not treat a skeleton register as complete coverage.
- Do not treat the preserved SC-13D pilot as already run.
```

## 8. Related Files

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Source Categories
    - Register Coverage Model
  Not checked:
    - full root/router local source audit outside ROOT-SRC-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
planning/root-source-sync-register.md
planning/domain/domain-source-sync-register.md
planning/slices/slice-source-sync-register.md planned
planning/documentation/field-kits/source-usage-cascade-field-kit.md
planning/documentation/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
planning/source-usage-pilots/README.md
```

## 9. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.4.0
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md @ version not confirmed
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - preserved SC-13D pilot register was not filled in this pass
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
- ROOT-SRC-1 added local section-level Sources blocks to this Enman profile without changing profile semantics.
- DOM-VO-SRC-ALL-1 made the domain register cover active aggregate and active value-object sources before this root pass.
```
