# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

The current main planning workflow is gradual domain discovery:

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> scenario behavior coverage baseline
-> domain draft 1
-> coverage review
-> domain draft 2
-> coverage review
-> ...
-> final domain model candidate
-> then plan aggregates/slices/implementation
```

The current main-domain next working step is:

```text
Create / refine domain draft 1 using the scenario behavior coverage baseline.
```

## 2. What Changed From The Older “Domain Variants” Wording

Older notes used wording like:

```text
domain model variant 1
domain model variant 2
compare/refine variants
choose domain model direction
```

That wording is superseded.

Current meaning:

```text
Domain draft N = complete current snapshot of the same developing domain model.
Domain draft N+1 = same direction, but more detailed, with better scenario behavior coverage and fewer questions.
```

The goal is not to create competing alternative designs.

The goal is to gradually discover the final domain model.

## 3. Parallel UI Planning Branch

UI planning is a separate parallel branch after scenario text specs and DATA files are ready.

It does not replace domain draft planning.

UI planning workflow:

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> ui-planning-workflow.md
-> test-site-ui-plan.md
-> ui-questions-register.md
-> optional visual / HTML / React low-fidelity mockup later
```

Current UI planning entry point:

```text
planning/ui/README.md
```

Current UI planning outputs:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

## 4. Current Read Order

Use this read order for the whole planning area:

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/scenario-specification-principles.md
4. planning/scenario-domain-validation-principles.md
5. planning/replacement-file-generation-guide.md
6. planning/diagrams/README.md
7. planning/diagrams/scenario-text-specs/README.md
8. planning/diagrams/scenario-data/README.md
9. planning/diagrams/scenario-diagram-consistency-report.md
10. planning/tables/README.md
11. planning/tables/pre-domain-variants-input.md
12. planning/domain-draft-generation-guide.md
13. planning/tables/domain-drafts/README.md
14. planning/ui/README.md
15. planning/ui/ui-planning-workflow.md
16. planning/ui/test-site-ui-plan.md
17. planning/ui/ui-questions-register.md
```

## 5. Source Files For Domain Drafts

Use exactly these inputs when generating domain drafts:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
5. planning/domain-draft-generation-guide.md
```

Optional context:

```text
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
```

## 6. Scenario Behavior Coverage Baseline

The active pre-domain control artifact is:

```text
planning/tables/pre-domain-variants-input.md
```

Despite the historical filename, this file now acts as:

```text
Scenario Behavior Coverage Baseline
```

It collects scenario-derived behavior items with stable IDs:

```text
- command behavior items;
- scenario state/condition items;
- impossible business state candidates;
- value integrity / anti-primitive-obsession items;
- use-case coordination items;
- read/access/integration/future items.
```

It does not define domain classes, aggregates or final method names.

Each domain draft uses those item IDs to show what is covered, partial, unresolved, deferred or placed outside the current domain model.

## 7. Source Files For UI Planning

Use these inputs when creating `test-site-ui-plan.md`:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
5. planning/diagrams/scenario-diagram-consistency-report.md
6. planning/scenario-specification-principles.md
7. planning/ui/ui-planning-workflow.md
```

UI planning output is textual page planning, not final visual design.

## 8. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

Current replacement-file rule:

```text
generate complete files
package them with repository-relative paths
include all needed files if previous archive was not applied
keep the response practical and list target paths
```

Do not provide partial snippets unless explicitly requested.

## 9. Superseded / Not Current

Do not use this old path as the current workflow:

```text
scenario-domain-design-input-gate.md
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

Do not use this as the current domain workflow:

```text
domain model variant 1
-> domain model variant 2
-> compare competing variants
-> choose one
```

If these files/folders exist from older packages, treat them as stale/superseded notes:

```text
planning/domain-model-variant-generation-guide.md
planning/tables/domain-variants/
planning/tables/scenario-domain-design-input-gate.md
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/aggregate-boundary-candidates-core.md
planning/tables/domain-model-options-core.md
```

## 10. Diagram / Scenario Source Of Truth

Use corrected text specs and DATA specs as semantic source of truth.

Generated diagram package summaries and old `.drawio` pages may exist for visual reference, but they are not semantic source of truth until regenerated from corrected text specs.

## 11. Folder Map

```text
planning/
  README.md
  planning-workflow-current.md
  scenario-specification-principles.md
  scenario-domain-validation-principles.md
  replacement-file-generation-guide.md
  domain-draft-generation-guide.md
  scenario-to-implementation-workflow-v5-consolidated.md  # superseded compatibility note

planning/diagrams/
  README.md
  scenario-text-specs/
  scenario-data/
  scenario-diagram-consistency-report.md

planning/tables/
  README.md
  00-planning-tables-index.md
  pre-domain-variants-input.md
  domain-drafts/

planning/ui/
  README.md
  ui-planning-workflow.md
  test-site-ui-plan.md
  ui-questions-register.md
  mockup-generation-guide.md
  prompts/
```

## 12. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected scenario text specs and DATA specs as source of truth;
- keep DATA files narrow;
- keep validation in validation-related files and scenario specs;
- use pre-domain-variants-input.md as scenario behavior coverage baseline before generating domain drafts;
- generate domain drafts iteratively, not competing alternatives;
- each draft must include coverage against baseline item IDs;
- use planning/ui/README.md and ui-planning-workflow.md before UI planning;
- keep textual UI plans separate from visual/HTML prototypes;
- generate complete replacement files when the user asks for manual replacement files;
- include all files from previous unapplied archive when the user says it was not applied;
- avoid implementation terms before scenario-to-slice planning;
- create or update files only when explicitly requested.
```
