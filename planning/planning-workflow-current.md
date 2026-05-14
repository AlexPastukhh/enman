# Current Planning Workflow

Status: current workflow

## 1. Current Point

Scenario text specifications are ready.

Scenario DATA files are ready.

Validation-related files remain part of the workflow.

The current pre-domain control artifact is:

```text
planning/tables/pre-domain-variants-input.md
```

Historical filename note:

```text
pre-domain-variants-input.md now acts as the Scenario Behavior Coverage Baseline.
```

The current saved domain draft is:

```text
planning/tables/domain-drafts/domain-draft-01.md
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

## 2. Main Domain And Slice Workflow

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
-> L1 implementation cut
-> domain testing rules
-> domain classes + unit tests
-> L1 slice drafts
-> scenario-to-slice coverage review
-> application/API/persistence/UI slices
-> ADR updates as decisions stabilize
```

The goal is gradual domain discovery followed by scenario-derived slice planning.

The goal is not selection between competing domain alternatives.

## 3. What A Domain Draft Means

Each domain draft is a complete snapshot of the current domain understanding.

Each next draft should:

```text
- preserve the same general direction unless a clear correction is needed;
- improve class/aggregate boundaries;
- make state/method ownership more explicit;
- cover more scenario behavior baseline items;
- reduce Partial/Missing/Question items;
- reduce open questions;
- make use-case coordination decisions clearer.
```

`domain-draft-01.md` already exists. The current task is review/refinement for implementation readiness, not creating the first draft from scratch.

## 4. What A Slice Draft Means

A slice draft is a scenario-derived implementation planning artifact.

Working definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A slice draft should:

```text
- start from scenarios;
- list relevant scenario-derived behavior items;
- include relevant DATA facts;
- mark candidate missing UI/read/UX items;
- derive independently testable slices;
- describe domain/application/persistence/read/API/UI/auth/infra participation;
- include test coverage;
- include per-slice coverage;
- include behavior item coverage;
- collect questions locally and in one consolidated section;
- collect ADR candidates.
```

The L1 domain implementation cut is not a full scenario slice.

It is a domain-foundation cut that prepares the project for later application/API/persistence/UI slice planning.

## 5. Source Files For Domain Drafts

Use exactly these inputs:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
5. planning/tables/pre-domain-variants-input.md
6. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
7. planning/domain-draft-generation-guide.md
```

## 6. Source Files For Slice Drafts

Use:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/tables/pre-domain-variants-input.md
4. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
5. planning/tables/domain-drafts/domain-draft-01.md
6. planning/l1-domain-implementation-cut.md
7. planning/l1-domain-testing-rules.md
8. planning/slices/l1-slice-drafting-guide.md
```

After implementation starts, also use:

```text
actual L1 domain implementation result
actual L1 domain unit tests
```

## 7. How Coverage Works

The baseline gives each behavior item a stable ID.

Each domain draft has a coverage section:

```text
Item ID
Readable requirement / invariant
Status
Draft answer / placement
Covered by
Gap / next action
```

Coverage item does not mean “domain class must implement this.”

Coverage item means:

```text
the system must explain/cover this behavior.
```

Slice drafts then map behavior items into independently testable slices.

If a needed UI/read/UX behavior item is missing from the baseline, mark it as a candidate item in the slice draft and review it before promoting it to the baseline.

## 8. L1 Implementation Readiness

Before implementation work, read:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
planning/current-state.md
planning/domain-model.md
```

Interpretation:

```text
domain-draft-01.md = target domain direction for next L1 domain refinement.
current-state.md = repository implementation snapshot.
domain-model.md = background L1 rules and compatibility notes.
l1-domain-implementation-cut.md = narrow implementation task boundary.
l1-domain-testing-rules.md = testing rules for L1 domain unit tests and later integration tests.
```

The first implementation agent should not implement the whole draft.

The first implementation agent should implement only the agreed L1 domain cut:

```text
domain classes + unit tests
no persistence
no API
no UI
no agreement exchange unless explicitly included
```

## 9. L1 Testing Position

Testing entry point:

```text
planning/l1-domain-testing-rules.md
```

Current testing decisions:

```text
- unit tests are written first;
- existing Tests.EnergyManagement is the local test project and style baseline;
- current test stack is xUnit + FluentAssertions, with Moq and WebApplicationFactory available for later boundary/integration tests;
- integration tests start after the first L1 domain implementation is green;
- integration tests should not be mixed into the first domain unit-test cut.
```

Testing principles:

```text
- verify observable behavior and domain rules;
- do not test private methods directly;
- avoid interaction-heavy domain unit tests;
- do not mock domain entities/value objects;
- check no-write behavior when commands fail;
- do not chase coverage percentage as the goal.
```

Existing tests under:

```text
Tests.EnergyManagement/
```

are valid local examples.

If exact current test names are needed, inspect the local checkout and list them before implementation.

## 10. L1 File Strategy

Use progressive file splitting.

During first implementation of a mini-cut, keep related domain code close enough for agent readability.

After the mini-cut is behavior-complete and unit tests are green, split/normalize files into production structure.

Do not keep the whole L1 domain in one giant file until the end.

Do not create excessive micro-files before behavior stabilizes.

Recommended mini-cut order:

```text
1. Account / ClientAccount activation marker
2. ApplicantParty / IndividualApplicantParty
3. ConnectionRequest / review behavior
4. AgreementProposalExchange later, after core request/refactor is stable
```

## 11. ADR Position

ADR candidates are collected in:

```text
planning/adr/adr-candidates.md
```

Do not write full ADRs unless explicitly requested.

Add candidates when a decision affects multiple slices, layer boundaries, plugin/external dependencies, persistence/read model tradeoffs, transaction boundaries, testing strategy, or diploma-level architecture explanation.

## 12. Replacement File Generation Workflow

When the user asks for files to replace in the repository manually, use:

```text
planning/replacement-file-generation-guide.md
```

Rules:

```text
- generate complete replacement files;
- preserve repository-relative paths;
- package files in a zip archive;
- include all files from a previous archive if the user says it was not applied;
- do not output patches or fragments unless explicitly asked.
```

## 13. Current Next Steps

Main domain branch:

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for implementation readiness.
Then use planning/l1-domain-implementation-cut.md and planning/l1-domain-testing-rules.md to start narrow L1 domain implementation.
After first green L1 domain foundation, use planning/slices/l1-slice-drafting-guide.md to create L1 slice drafts before application/API/persistence/UI work.
```

UI branch and VKR branch remain parallel/supporting branches.
