# Planning Tables Index

Status: current planning tables navigation

## 1. Responsibility

This folder owns compiled tables and historical/aggregate baselines.

It does not own global planning workflow rules. Use:

```text
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
```

for global workflow/agent/responsibility rules.

## 2. Current Role

The compiled behavior baseline remains:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Per-scenario behavior item files now live under:

```text
planning/diagrams/scenario-behavior-items/
```

`pre-domain-variants-input.md` remains useful as a compiled downstream baseline / existing domain draft input.

Future slice/client planning should prefer per-scenario behavior item files and use compiled baselines for cross-checking/history.

## 3. Current Read Order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-questions-register.md
4. planning/diagrams/scenario-behavior-items/README.md
5. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
6. planning/tables/pre-domain-variants-input.md
7. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
8. planning/tables/domain-drafts/domain-draft-01.md
9. planning/slices/README.md
10. planning/slices/l1-slice-drafting-guide.md
```

## 4. Behavior Items Position

Desired flow:

```text
scenario text spec
+ scenario DATA file
+ validation/security addendum
-> per-scenario behavior items
-> domain drafts / slice drafts / client sidecars
```

The compiled baseline can be regenerated or cross-checked later after per-scenario files stabilize.

## 5. Avoid

Do not add or use old intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```
