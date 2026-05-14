# Current Planning Workflow

Status: current workflow

## 1. Current Point

The current active workflow step is:

```text
Use the general L1 slice boundary draft and per-slice files to plan/implement one slice at a time.
```

The current slice planning files are:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l1-slice-boundary-draft-01.md
```

## 2. Main Workflow

```text
scenario text specs
-> scenario DATA files
-> validation-related files
-> scenario behavior coverage baseline
-> domain draft(s)
-> L1 domain implementation cut
-> domain testing rules
-> L1 domain foundation
-> L1 slice boundary draft
-> per-slice implementation files
-> implement one slice at a time
-> update coverage / questions / ADR candidates
```

## 3. What A Slice Boundary Draft Means

The general boundary draft is:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

It identifies real slices from scenarios.

It should:

```text
- start from scenarios;
- list derived slices;
- explain why each is a real slice;
- include Scenario Slice Flow;
- embed DATA / behavior items / invariants / no-write rules in flow steps;
- mark dependent/extension/UI/read/plugin slices;
- split L1 and later-package behavior;
- collect boundary questions and decisions;
- collect ADR candidates.
```

It should not contain detailed implementation flow.

## 4. What A Per-Slice File Means

A per-slice file is the implementation-planning artifact for one slice.

It should:

```text
- duplicate/refine Scenario Slice Flow;
- add Implementation Flow;
- include UI blueprint;
- include questions overview near the beginning;
- include flow coverage overview near the beginning;
- describe API/application/domain/persistence/read/UI/auth/infra participation;
- keep tests in a separate Test Plan / Test Coverage section;
- include detailed implementation notes and pseudocode/code snippets when useful;
- include implementation checklist.
```

Current per-slice files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

## 5. Source Files For Slice Drafts

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
9. actual L1 implementation result
10. actual L1 tests
```

## 6. Current Slice Status

Recent resolved issue:

```text
SL-REQ-001 Submitted vs InReview conflict has been resolved in active implementation/tests.
```

Target domain direction:

```text
InReview
Approved
Rejected
```

The request creation integration/API expectation is aligned with `InReview`.

## 7. L1 Testing Position

Testing guide:

```text
planning/l1-domain-testing-rules.md
```

Current decisions:

```text
- unit tests first for domain foundation;
- integration tests are per-slice after behavior is stable;
- UI tests are per UI/full-stack slice;
- existing Tests.EnergyManagement is the local style baseline;
- tests verify observable behavior and no-write guarantees.
```

## 8. ADR Position

ADR candidates are collected in:

```text
planning/adr/adr-candidates.md
```

## 9. Current Next Steps

Recommended next steps:

```text
1. Apply current slice workflow documentation update.
2. Review l1-slice-boundary-draft-01.md.
3. Choose the next slice implementation target.
4. Before implementation, create/refine the relevant per-slice file.
```

## 10. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```
