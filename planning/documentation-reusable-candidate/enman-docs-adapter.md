# Enman Docs Adapter

Status: candidate project adapter / non-canonical preservation mapping  
Scope: concrete Enman/current-project documentation mappings extracted from candidate principles

> Candidate note: this file belongs to `planning/documentation-reusable-candidate/`. It preserves project-specific knowledge for migration review. It is not the active Enman source of truth until a later migration/switch batch approves it.

## 1. Purpose

This adapter preserves concrete Enman mappings that should not live as universal reusable principles.

It answers:

```text
How does the reusable documentation architecture candidate map to the current Enman repo?
Which exact paths, layer names, evidence sources, shared registers and external-output files are Enman-specific?
```

This adapter is not a dumping ground. If a paragraph has reusable value, the reusable principle should remain in the principles file or specialized profile, while the concrete Enman mapping lives here.

## 2. Project Identity

```text
Project: Enman
Repository scope: current `my-changes` planning documentation layer
Active docs layer: planning/documentation/
Reusable candidate workspace: planning/documentation-reusable-candidate/
```

## 3. Candidate Status Boundary

This file is candidate-only.

Active Enman documentation remains under:

```text
planning/documentation/
```

Do not use this adapter as the active source of truth until a later migration/switch batch approves it.

## 4. Documentation Layer Paths

Active documentation layer:

```text
planning/documentation/
```

Candidate reusable layer:

```text
planning/documentation-reusable-candidate/
```

Candidate guardrails:

```text
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
planning/documentation-reusable-candidate/PRINCIPLES-RESPONSIBILITY-CLASSIFICATION.md
```

## 5. Responsibility Map / Entry Point Paths

Current Enman planning entrypoint:

```text
planning/README.md
```

Current root responsibility map:

```text
planning/planning-doc-responsibility-map.md
```

Current documentation-layer responsibility map:

```text
planning/documentation/documentation-responsibility-map.md
```

Candidate documentation-layer responsibility map copy:

```text
planning/documentation-reusable-candidate/documentation-responsibility-map.md
```

## 6. Enman Layer Vocabulary

Current Enman documentation uses these layer concepts:

| Layer concept | Concrete Enman meaning |
|---|---|
| Documentation layer | Planning docs architecture, docs update workflows, responsibility maps, agent-output workflows and documentation governance. |
| Scenario layer | Scenario text, UI specs, clarifications and behavior source. |
| DATA set layer | Per-scenario DATA sets: what actors enter, see, select, filter, attach or reference. |
| Behavior items set layer | Per-scenario behavior item sets derived from scenario/DATA/UI/cross-cutting sources. |
| Domain layer | Domain concepts, value objects, invariants, aggregate boundaries, domain decisions and domain drafts. |
| Slice layer | Slice drafts, slice boundaries, scenario/source mapping, questions, extension points and implementation notes. |
| API / testing layer | API contract rules, generated contract rules, error contracts, testing principles and E2E workflows. |
| Implementation evidence layer | Current branch, code, tests, migrations, generated artifacts and runtime screenshots. |
| VKR / thesis layer | Clean thesis wording, thesis resources, evidence maps and presentation/defense-safe wording. |

The scenario/domain/slice pattern is reusable as a specialized profile, but these exact labels and paths are Enman mapping.

## 7. Exact Source Hierarchy Paths

### Scenario behavior truth

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
```

### Slice scope truth

```text
planning/slices/
planning/slices/l2/
planning/slices/cross-cutting/
```

Slice docs describe scope and intended work. They are not proof that work is implemented.

### Architecture / API / client / testing decisions

```text
planning/architecture/
planning/api/
planning/client/
planning/testing/
planning/adr/
```

### VKR / thesis clean wording

```text
planning/vkr-clean-reference.md
planning/thesis/
```

### Recovery material

Use only after canonical docs:

```text
planning/dirty-drafts/
```

## 8. Evidence / Current Implementation Profile

For Enman software/application status claims, current implementation evidence may include:

```text
current Git branch
code
tests
migrations
OpenAPI / generated contracts
runtime screenshots
```

This evidence profile should be refined through the status reconciliation workflow/profile work. It is not a universal evidence model for every documentation domain.

Deferred follow-up:

```text
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
```

## 9. Shared Visibility Map Candidates

The current principles referenced these concrete shared visibility examples:

```text
planning/README.md
folder README files
planning/planning-doc-responsibility-map.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

These are Enman shared visibility/register targets.

The generic local/global rule belongs in reusable principles. The process belongs in local/global sync workflow. The exact target map belongs here or in a later project adapter section after F5 review.

## 10. External Output Mapping

For Enman, the external/public-facing output layer includes VKR/thesis-related material.

Concrete paths:

```text
planning/vkr-clean-reference.md
planning/thesis/
```

Reusable principle:

```text
Internal working docs may use internal workflow terms, but external-facing output must use audience-appropriate wording.
```

Enman-specific mapping:

```text
VKR/thesis wording and defense/presentation/practice-report wording rules are project-specific.
```

## 11. Dirty Draft / Recovery Mapping

Concrete recovery path:

```text
planning/dirty-drafts/
```

Rules preserved from candidate principles:

```text
- dirty drafts are never source of truth;
- canonical docs and current evidence win;
- use dirty drafts only after canonical docs were checked;
- rewrite useful wording into clean terminology before external output use;
- promote stable useful decisions into canonical docs through normal documentation sync.
```

## 12. Relation To Scenario-Driven Profile

The scenario/domain/slice profile describes the reusable specialized topology.

This adapter instantiates that topology for Enman with exact paths and layer vocabulary.

Profile:

```text
planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md
```

## 13. Deferred Items

Some extracted material is not simply Enman mapping.

Deferred items include:

```text
source usage/cascade field-kit extraction;
status reconciliation/evidence model setup;
local-global Shared Visibility Map setup;
examples extraction;
final naming and canonical switch decisions.
```

Owner:

```text
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
```

## 14. Do Not

```text
- Do not treat this adapter as universal principles.
- Do not use this adapter as active canonical docs before a switch batch.
- Do not move reusable principles here just because they mention an Enman path.
- Do not treat scenario/domain/slice topology as universal for every docs system.
- Do not bury workflow/field-kit follow-ups here; track them in PORTABILITY-FOLLOWUPS.md.
```
