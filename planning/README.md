# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

The current main planning workflow is gradual domain discovery followed by domain foundation, slice drafting and implementation planning:

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
-> domain testing rules
-> domain classes + unit tests
-> L1 slice drafting
-> scenario-to-slice coverage review
-> application/API/persistence/UI slices
-> ADR updates as decisions stabilize
```

The current main-domain working step is:

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for L1 implementation readiness.
```

The current L1 implementation planning entry points are:

```text
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

The current slice and ADR planning entry points are:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/adr/README.md
planning/adr/adr-candidates.md
```

## 2. Current L1 Implementation Readiness Position

`domain-draft-01.md` exists and is the current first saved domain draft.

Before asking an implementation agent to code L1, use this order:

```text
1. Review domain-draft-01.md.
2. Use planning/l1-domain-implementation-cut.md.
3. Use planning/l1-domain-testing-rules.md.
4. Implement only the agreed L1 domain cut.
5. Add domain unit tests for local invariants and no-write behavior.
6. Do not implement persistence/API/UI unless explicitly requested.
```

Do not ask an agent to implement the whole draft at once.

Unit tests are first.

Integration tests are planned after the first domain implementation is green.

## 3. Slice Planning Position

The L1 domain cut does not replace slice planning.

The L1 domain cut is a domain-foundation cut.

After the first green L1 domain implementation, create L1 slice drafts before application/API/persistence/UI implementation work.

Slice definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

Slices are derived from:

```text
scenarios
scenario DATA facts
scenario behavior baseline items
domain draft / L1 domain implementation result
```

Slice drafts should organize work by scenario sections and list the slices derived from each scenario.

Use:

```text
planning/slices/l1-slice-drafting-guide.md
```

## 4. Current Read Order

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
16. planning/l1-domain-testing-rules.md
17. planning/slices/README.md
18. planning/slices/l1-slice-drafting-guide.md
19. planning/adr/README.md
20. planning/adr/adr-candidates.md
21. planning/current-state.md
22. planning/domain-model.md
23. planning/ui/README.md
```

## 5. Source Files For Domain Drafts

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

## 6. Scenario Behavior Coverage Baseline

The active pre-domain control artifact is:

```text
planning/tables/pre-domain-variants-input.md
```

Despite the historical filename, this file now acts as:

```text
Scenario Behavior Coverage Baseline
```

It collects scenario-derived behavior items with stable IDs.

It does not define domain classes, aggregates, slices or final method names.

Each domain draft uses those item IDs to show what is covered, partial, unresolved, deferred or placed outside the current domain model.

Each slice draft uses those item IDs to show which behavior is planned in which independently testable slice.

If slice planning reveals missing UI/read/UX items, add them as candidate items in the slice draft before promoting them to the baseline.

## 7. L1 Testing Rules

The current L1 testing rules are:

```text
planning/l1-domain-testing-rules.md
```

Use this file before giving implementation work to an agent.

Current testing position:

```text
- unit tests first;
- existing Tests.EnergyManagement is the local test project and style baseline;
- xUnit + FluentAssertions are the current unit-test baseline;
- integration tests come after the first green domain implementation;
- do not mix persistence/API/UI tests into the first L1 domain cut.
```

Existing tests under:

```text
Tests.EnergyManagement/
```

are the valid local examples for test style.

If exact existing test class/method names are needed, inspect the local checkout before writing the implementation prompt.

## 8. ADR Planning

ADR candidates are collected in:

```text
planning/adr/adr-candidates.md
```

Do not write full ADRs for every minor choice.

Add ADR candidates when a decision affects multiple slices, layer boundaries, plugin/external dependencies, persistence/read model strategy, transaction boundaries, testing strategy, or diploma-level architecture explanation.

## 9. Replacement File Generation Workflow

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

## 10. Superseded / Not Current

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

## 11. Agent Rules

Planning agents should:

```text
- read current indexes first;
- use corrected scenario text specs and DATA specs as source of truth;
- use pre-domain-variants-input.md as scenario behavior coverage baseline before generating domain drafts;
- after a draft exists, do not say “create draft 1” as the current step;
- use planning/l1-domain-implementation-cut.md before giving L1 coding work to an implementation agent;
- use planning/l1-domain-testing-rules.md before writing L1 tests;
- keep L1 implementation narrow: domain classes + unit tests first;
- use existing Tests.EnergyManagement as the local test style baseline;
- do not implement persistence/API/UI unless explicitly requested;
- use progressive file splitting during L1 implementation;
- after green L1 domain foundation, use planning/slices/l1-slice-drafting-guide.md before planning application/API/persistence/UI work;
- derive slices from scenarios, behavior items and independent testability;
- collect ADR candidates when decisions affect multiple slices or architecture boundaries;
- create or update files only when explicitly requested.
```
