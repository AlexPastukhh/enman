# Diagrams And Scenario Specs Index

Status: current navigation index for `planning/diagrams`

## 1. Current Semantic Source Of Truth

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

## 2. Subfolders

```text
scenario-text-specs/
  corrected textual scenario specs and validation/security addenda

scenario-data/
  per-scenario DATA specs

scenario-behavior-items/
  per-scenario behavior item files and lightweight behavior-item index

scenario-*.drawio / .svg / .png
  visual artifacts, only authoritative after regenerated from corrected text specs
```

## 3. Scenario Questions

Central scenario-stage questions register:

```text
planning/diagrams/scenario-questions-register.md
```

Use it when questions affect scenario behavior, DATA, validation/security, visible outcome or scenario semantics.

## 4. Scenario Behavior Items

Behavior item folder:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items are derived from scenario text specs, DATA specs and validation/security addenda.

Behavior item migration / cleanup is a separate future step.

## 5. How To Continue To Domain / Slice / Client Planning

For current planning, read:

```text
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/tables/pre-domain-variants-input.md
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
```

Then continue to domain drafts, slice boundary drafts, parent vertical slice files and `.client.md` sidecars when concrete client work starts.

## 6. Do Not Use Old Downstream Path

Do not route current planning through:

```text
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/domain-variants/
```
