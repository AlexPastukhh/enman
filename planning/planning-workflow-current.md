# Current Planning Workflow

Status: current workflow

## 1. Current Point

Scenario text specifications are ready.

Scenario DATA files are ready.

Validation-related file remains part of the workflow.

The current pre-domain control artifact is:

```text
planning/tables/pre-domain-variants-input.md
```

Historical filename note:

```text
pre-domain-variants-input.md now acts as the Scenario Behavior Coverage Baseline.
```

The current domain draft guide is:

```text
planning/domain-draft-generation-guide.md
```

The current UI-planning entry point is:

```text
planning/ui/README.md
```

## 2. Main Domain Workflow

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

The goal is gradual domain discovery, not selection between competing domain alternatives.

## 3. What A Domain Draft Means

Each domain draft is a complete snapshot of the current domain understanding.

Each draft includes the same required sections.

Each next draft should:

```text
- preserve the same general direction unless a clear correction is needed;
- improve class/aggregate boundaries;
- make state/method ownership more explicit;
- cover more scenario behavior baseline items;
- reduce Partial/Missing/Question items;
- reduce open questions;
- make use-case coordination decisions clearer.
```

## 4. Source Files For Domain Drafts

Use exactly these inputs:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
5. planning/domain-draft-generation-guide.md
```

## 5. Purpose Of Scenario Behavior Coverage Baseline

`planning/tables/pre-domain-variants-input.md` collects scenario-derived coverage items with stable IDs.

It exists before domain drafts.

It does not define domain classes, aggregates or final method names.

It records:

```text
- required behavior / guarantees from scenarios;
- failure / no-write guarantees;
- scenario state/condition matrices;
- impossible business state candidates;
- value integrity / anti-primitive-obsession items;
- use-case coordination items;
- read/access/integration/future behavior items;
- draft questions that future domain drafts must answer.
```

## 6. How Coverage Works

The baseline gives each behavior item a stable ID.

Each domain draft has a coverage section:

```text
Item ID
Status
Draft answer / placement
Covered by
Gap / next action
```

Coverage statuses:

```text
Missing
Partial
Covered
Resolved outside current domain model
Deferred
Question
```

Coverage item does not mean “domain class must implement this.”

Coverage item means:

```text
the system must explain/cover this behavior.
```

A domain draft may cover it with:

```text
- domain class;
- value object;
- domain service;
- application/use-case orchestration;
- read/query/access placement;
- DB constraint;
- infrastructure/integration;
- deferred future decision.
```

## 7. Parallel UI Planning Branch

UI planning is a parallel branch after scenario text specs and DATA files are ready.

It does not replace domain draft planning.

UI planning creates a textual test-site UI plan, not final visual design.

Workflow:

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> ui-planning-workflow.md
-> test-site-ui-plan.md
-> ui-questions-register.md
-> optional visual / HTML / React low-fidelity mockup later
```

Inputs:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
planning/ui/ui-planning-workflow.md
```

Outputs:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

## 8. Scenario / DATA Feedback From UI Planning

UI planning can reveal that a scenario or DATA file is underspecified.

Use this rule:

```text
If the UI plan reveals that the user must see, understand, or do something for the use case to be valid, update the scenario spec.

If it reveals that a page needs a specific visible/input/selectable/filter/attachment DATA item to support an already-defined scenario, update the DATA file.

If the issue is only layout, component choice, navigation style, or one of several acceptable UX options, keep it in test-site-ui-plan.md or ui-questions-register.md.
```

## 9. Replacement File Generation Workflow

When the user asks for files to replace in the repository manually, use:

```text
planning/replacement-file-generation-guide.md
```

Rules:

```text
- generate complete replacement files;
- preserve repository-relative paths;
- package files in a zip archive;
- include all files from a previous archive if the user says it was not applied;
- do not output patches or fragments unless explicitly asked.
```

## 10. Current Next Steps

Main domain branch:

```text
Create / refine domain draft 1 in planning/tables/domain-drafts/.
```

UI branch:

```text
Create / fill planning/ui/test-site-ui-plan.md and planning/ui/ui-questions-register.md.
```
