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
```

## 3. DATA Is Not Validation

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

Validation belongs to scenario text specs and validation/security addenda.

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
pre-domain-variants-input.md compiled baseline
domain drafts
slice boundary drafts
parent slice files
.client.md Scenario / DATA Coverage tables
read/query DTO planning
client-visible page data planning
```

DATA fields are candidates for value integrity items, value objects, visible page data, form input values, command/input objects, read DTO fields and persistence fields.

DATA files themselves are not DB schemas, DTO contracts or UI layouts.

## 6. Current Next Steps

Current flow:

```text
scenario text specs
+ scenario DATA files
+ validation/security addenda
-> scenario questions register
-> per-scenario behavior items
-> slice/domain/client planning
```

Behavior item migration / cleanup is a separate future step.
