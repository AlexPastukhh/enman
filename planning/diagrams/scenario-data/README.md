# Scenario DATA Specs Index

Status: current scenario DATA navigation index

## 1. Purpose

This folder contains per-scenario DATA specs.

DATA means only what actor:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

## 2. Read First

```text
00-scenario-data-index.md
```

## 3. DATA Is Not Validation

DATA files must not contain:

```text
- validation/rules sections;
- testable behavior sections;
- invariants;
- preconditions;
- branches;
- access rules;
- security policy;
- layout choices.
```

Validation belongs to:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

The pre-domain coverage baseline uses DATA together with scenario and validation-related files:

```text
planning/tables/pre-domain-variants-input.md
```

The UI planning branch uses DATA to define visible/input/selectable page content:

```text
planning/ui/test-site-ui-plan.md
```

## 4. Active DATA Files

See:

```text
00-scenario-data-index.md
```

## 5. Downstream Use

DATA specs feed:

```text
scenario-server-domain-validation-addendum.md
pre-domain-variants-input.md
domain drafts
test-site-ui-plan.md
ui-questions-register.md
```

DATA fields are candidates for:

```text
- value integrity / anti-primitive-obsession items in the coverage baseline;
- value objects discovered during domain draft generation;
- visible page data;
- command/input objects later;
- persistence fields later.
```

But DATA files themselves are not DB schemas, DTO contracts or UI layouts.

## 6. Current Next Steps

Domain branch:

```text
planning/tables/pre-domain-variants-input.md
-> planning/tables/domain-drafts/domain-draft-01.md
```

UI branch:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```
