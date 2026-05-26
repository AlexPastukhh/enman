# Planning Tables Index

Status: current planning tables navigation / compiled and historical baseline owner

## 1. Responsibility

This folder owns compiled tables, historical aggregate baselines and pre-domain source snapshots.

It is not the current domain-layer entrypoint.

Use the domain layer for current aggregate-based domain planning:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/scenario-to-aggregate-map.md
```

Use root/global docs for global workflow/agent/responsibility rules:

```text
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
```

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

`pre-domain-variants-input.md` remains useful as a compiled downstream baseline / historical domain discovery input.

Future domain/slice/client planning should prefer per-scenario behavior item files and use compiled baselines for cross-checking/history.

## 3. Current Read Order

For current domain discovery, start from:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/domain-discovery-workflow.md
4. planning/domain/scenario-to-aggregate-map.md
```

Use this folder as historical/cross-check input after reading current scenario/domain routing:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-questions-register.md
4. planning/diagrams/scenario-behavior-items/README.md
5. planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
6. planning/tables/pre-domain-variants-input.md
7. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
8. planning/tables/domain-drafts/
9. planning/domain/
10. planning/slices/README.md
```

## 4. Behavior Items Position

Desired flow:

```text
scenario text spec
+ scenario DATA file
+ validation/security addendum
-> per-scenario behavior items
-> domain discovery / scenario-to-aggregate map
-> aggregate drafts / value object drafts
-> slice drafts / client sidecars
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

Use the current domain-layer files instead.
