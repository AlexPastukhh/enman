# Diagrams And Scenario Specs Index

Status: current navigation index for `planning/diagrams`

## 1. Responsibility

This folder owns scenario-related source artifacts:

```text
scenario text specs
scenario DATA specs
scenario UI specs
validation/security addenda
scenario questions register
per-scenario behavior items
scenario diagram consistency report
```

It does not own global planning workflow rules.

## 2. Current Semantic Source Of Truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-diagram-consistency-report.md
```

## 3. Scenario UI Specs

Scenario UI spec folder:

```text
planning/diagrams/scenario-ui-specs/
```

UI specs capture UI-visible requirements and UI behavior items.

They do not define React implementation details.

## 4. Scenario Questions

Central scenario-stage questions register:

```text
planning/diagrams/scenario-questions-register.md
```

Use it when questions affect scenario behavior, DATA, UI-visible requirement, validation/security or visible outcome.

## 5. Scenario Behavior Items

Behavior item folder:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items are derived from scenario text specs, DATA specs, UI specs, validation/security addenda and existing compiled baselines.

## 6. Stale Package Summaries

The old package summaries are compatibility notes / stale visual summaries:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

Do not use them as semantic source of truth for domain planning, slice planning or client planning.

## 7. How To Continue

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-ui-specs/00-scenario-ui-specs-index.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
```
