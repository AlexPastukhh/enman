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
../scenario-questions-register.md
../scenario-behavior-items/README.md
../scenario-behavior-items/00-scenario-behavior-items-index.md
```

## 3. DATA Is Not Validation

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

Validation belongs to:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
```

## 4. Scenario Questions

If DATA is underspecified, add/update:

```text
planning/diagrams/scenario-questions-register.md
```

Use the scenario question loop before continuing implementation planning.

## 5. Downstream Use

DATA specs feed:

```text
scenario questions register
per-scenario behavior items
compiled baselines
domain drafts
slice boundary drafts
parent slice files
.client.md Scenario / DATA Coverage tables
read/query DTO planning
client-visible page data planning
```

DATA files themselves are not DB schemas, DTO contracts or UI layouts.

## 6. Active DATA Files

See:

```text
00-scenario-data-index.md
```
