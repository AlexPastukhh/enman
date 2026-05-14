# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

The current main planning workflow is gradual domain discovery:

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
-> implementation readiness review
-> L1 domain implementation cut
-> domain classes + unit tests
-> then plan application/API/persistence/UI slices
```

The current main-domain working step is:

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for L1 implementation readiness.
```

The current implementation planning entry point is:

```text
planning/l1-domain-implementation-cut.md
```

## 2. Current L1 Implementation Readiness Position

`domain-draft-01.md` exists and is the current first saved domain draft.

Before asking an implementation agent to code L1, use this order:

```text
1. Review domain-draft-01.md.
2. Use planning/l1-domain-implementation-cut.md.
3. Implement only the agreed L1 domain cut.
4. Add domain unit tests for local invariants and no-write behavior.
5. Do not implement persistence/API/UI unless explicitly requested.
```

Do not ask an agent to implement the whole draft at once.

## 3. Current Read Order

```text
1. planning/README.md
2. planning/planning-workflow-current.md
3. planning/scenario-specification-principles.md
4. planning/scenario-domain-validation-principles.md
5. planning/replacement-file-generation-guide.md
6. planning/diagrams/README.md
7. planning/diagrams/scenario-text-specs/README.md
8. planning/diagrams/scenario-data/README.md
9. planning/tables/README.md
10. planning/tables/pre-domain-variants-input.md
11. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
12. planning/domain-draft-generation-guide.md
13. planning/tables/domain-drafts/README.md
14. planning/tables/domain-drafts/domain-draft-01.md
15. planning/l1-domain-implementation-cut.md
16. planning/current-state.md
17. planning/domain-model.md
18. planning/ui/README.md
```

## 4. Source Files For Domain Drafts

Use exactly these inputs when generating or refining domain drafts:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
5. planning/tables/pre-domain-variants-input.md
6. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
7. planning/domain-draft-generation-guide.md
```

Optional context:

```text
planning/current-state.md
planning/domain-model.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
```

## 5. Scenario Behavior Coverage Baseline

The active pre-domain control artifact is:

```text
planning/tables/pre-domain-variants-input.md
```

Despite the historical filename, this file now acts as:

```text
Scenario Behavior Coverage Baseline
```

It collects scenario-derived behavior items with stable IDs.

It does not define domain classes, aggregates or final method names.

Each domain draft uses those item IDs to show what is covered, partial, unresolved, deferred or placed outside the current domain model.

## 6. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

Current replacement-file rule:

```text
generate complete files
package them with repository-relative paths
include all needed files if previous archive was not applied
keep the response practical and list target paths
```

Do not provide partial snippets unless explicitly requested.

## 7. Superseded / Not Current

Do not use the old path as the current workflow:

```text
scenario-domain-design-input-gate.md
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

Do not use this as the current domain workflow:

```text
domain model variant 1
-> domain model variant 2
-> compare competing variants
-> choose one
```

If old files/folders exist, treat them as stale/superseded notes.

## 8. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected scenario text specs and DATA specs as source of truth;
- use pre-domain-variants-input.md as scenario behavior coverage baseline before generating domain drafts;
- after a draft exists, do not say “create draft 1” as the current step;
- use planning/l1-domain-implementation-cut.md before giving L1 coding work to an implementation agent;
- keep L1 implementation narrow: domain classes + unit tests first;
- do not implement persistence/API/UI unless explicitly requested;
- use progressive file splitting during L1 implementation;
- create or update files only when explicitly requested.
```
