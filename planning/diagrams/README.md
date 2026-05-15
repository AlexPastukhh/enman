# Diagrams And Scenario Specs Index

Status: current navigation index for `planning/diagrams`

## 1. Responsibility

This folder owns scenario-related source artifacts:

```text
scenario text specs
scenario DATA specs
validation/security addenda
scenario questions register
per-scenario behavior items
scenario diagram consistency report
```

It does not own global planning workflow rules. Use:

```text
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
```

for global workflow/agent/responsibility rules.

## 2. Current Semantic Source Of Truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-diagram-consistency-report.md
```

## 3. Scenario Questions

Central scenario-stage questions register:

```text
planning/diagrams/scenario-questions-register.md
```

Use it when questions affect scenario behavior, DATA, validation/security or visible outcome.

## 4. Scenario Behavior Items

Behavior item folder:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items are derived from scenario text specs, DATA specs, validation/security addenda and existing compiled baselines.

## 5. Stale Package Summaries

The old package summaries are compatibility notes / stale visual summaries:

```text
planning/diagrams/scenario-core-package.md
planning/diagrams/scenario-extension-package.md
planning/diagrams/scenario-advanced-package.md
```

Do not use them as semantic source of truth for domain planning, slice planning or client planning.

## 6. How To Continue

For current planning, read:

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
```

Then continue to domain drafts, slice boundary drafts, parent vertical slice files or `.client.md` sidecars when concrete client work starts.
