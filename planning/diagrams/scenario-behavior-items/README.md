# Scenario Behavior Items

Status: workflow scaffold  
Scope: per-scenario behavior item files derived from scenario text specs, DATA specs and validation/security addenda

## 1. Purpose

This folder will contain per-scenario behavior items.

Behavior items are derived from:

```text
scenario main flow
branches
invariants
outcomes
DATA requirements
validation/security addenda
```

They do not invent new behavior.

If behavior item creation reveals missing behavior, update the scenario text spec.

If it reveals missing visible/input/selectable/filter/attachment data, update the DATA file.

If it reveals multiple valid interpretations, add/update:

```text
planning/diagrams/scenario-questions-register.md
```

## 2. Why Per-Scenario Files

The compiled baseline lives in:

```text
planning/tables/pre-domain-variants-input.md
```

It remains useful as a downstream compiled baseline / historical domain-draft input.

For future planning, per-scenario files help trace behavior to scenarios and feed domain drafts, slice coverage and client sidecar coverage.

## 3. Current Rule

Do not migrate all existing items in this workflow update.

Behavior item migration / cleanup is a separate future step.

## 4. Category Style

Use the same category style as `planning/tables/pre-domain-variants-input.md`.

Default categories:

```text
CMD
LC
IBS
VI
UCQ
READ
INT
FUT
NW
SQ
```

Do not add new categories without an explicit decision.

## 5. Per-Scenario File Template

```text
# SC-XX — Scenario Name Behavior Items

Status:
Source scenario:
Source DATA:
Validation/security sources:

## 1. Purpose
## 2. Source Set
## 3. Coverage Items Registry
## 4. Command Behavior Cards
## 5. Scenario State / Condition Matrices
## 6. Impossible Business State Candidates
## 7. Value Integrity Items
## 8. Use-Case Coordination Items
## 9. Read / Access / Integration / Future Items
## 10. No-Write / Failure Preservation Items
## 11. Scenario Questions Raised
```

## 6. Downstream Use

Per-scenario behavior items feed domain drafts, slice boundary drafts, parent vertical slice files, client sidecar coverage tables and test planning.
