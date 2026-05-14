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

## 2. Main Domain Workflow

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
-> then application/API/persistence/UI planning
```

The goal is gradual domain discovery, not selection between competing domain alternatives.

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

## 4. Source Files For Domain Drafts

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

## 5. How Coverage Works

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

## 6. L1 Implementation Readiness

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

## 7. L1 Testing Position

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

## 8. L1 File Strategy

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

## 9. Replacement File Generation Workflow

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

## 10. Current Next Steps

Main domain branch:

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for implementation readiness.
Then use planning/l1-domain-implementation-cut.md and planning/l1-domain-testing-rules.md to start narrow L1 domain implementation.
```

UI branch and VKR branch remain parallel/supporting branches.
