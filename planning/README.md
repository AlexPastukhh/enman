# Planning Index

Status: current planning navigation index  
Scope: repository planning artifacts and read order

## 1. Current Main Workflow

The current workflow is scenario-first, then domain foundation, then slice-by-slice implementation planning:

```text
scenario text specs
-> scenario DATA files
-> validation-related files
-> scenario behavior coverage baseline
-> domain draft(s)
-> L1 domain implementation cut
-> L1 domain testing rules
-> L1 domain foundation implementation
-> L1 slice boundary draft
-> per-slice implementation files
-> implement one slice at a time
-> update coverage / decisions / ADR candidates
```

The current active planning focus is:

```text
L1 slice boundary and per-slice implementation planning.
```

## 2. Current L1 Slice Entry Points

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l1-slice-boundary-draft-01.md
```

Current implemented/partial slice files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

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
16. planning/l1-domain-testing-rules.md
17. planning/slices/README.md
18. planning/slices/l1-slice-drafting-guide.md
19. planning/slices/l1-slice-boundary-draft-01.md
20. planning/slices/SL-REQ-001-create-connection-request.md
21. planning/adr/README.md
22. planning/adr/adr-candidates.md
23. planning/current-state.md
24. planning/domain-model.md
25. planning/ui/README.md
```

## 4. Slice Planning Position

Slice definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

There are two slice planning levels:

```text
General boundary draft:
- identifies real slices;
- explains why each slice is valid;
- shows Scenario Slice Flow;
- assigns scenario flow parts to slices;
- collects boundary questions/decisions.

Per-slice file:
- duplicates/refines Scenario Slice Flow;
- adds Implementation Flow;
- includes UI blueprint;
- includes test plan;
- includes detailed implementation notes and checklist.
```

Do not put detailed implementation flow into the general boundary draft.

Do not implement new application/API/persistence/UI work before the relevant per-slice file exists.

## 5. Source Files For Slice Drafts

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
planning/slices/l1-slice-drafting-guide.md
actual L1 implementation result
actual L1 tests
```

## 6. L1 Testing Rules

The current L1 testing rules are:

```text
planning/l1-domain-testing-rules.md
```

Existing tests under:

```text
Tests.EnergyManagement/
```

are valid local examples for test style.

Unit tests are first for domain foundation.

Integration/UI tests are planned in the relevant per-slice files.

## 7. ADR Planning

ADR candidates are collected in:

```text
planning/adr/adr-candidates.md
```

## 8. Replacement File Generation Workflow

When the user asks for files to replace manually, use:

```text
planning/replacement-file-generation-guide.md
```

Generate complete files, package them with repository-relative paths, and include `APPLY.md` / `MANIFEST.md`.

## 9. Superseded / Not Current

Do not use the old path as the current workflow:

```text
scenario-domain-design-input-gate.md
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
-> domain-model-options-core.md
```

If old files/folders exist, treat them as stale/superseded notes.
