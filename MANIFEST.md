# MANIFEST — Employee Request Read Slice Docs Sync

Archive: `employee-request-read-docs-sync.zip`  
Scope: documentation-only synchronization for L2 Employee request read slices and server read-slice test-plan rules  
Repo branch baseline: `my-changes`

## Add

| File | Purpose |
|---|---|
| `planning/slices/SL-EMP-REQ-001-employee-request-list-read.md` | Full backend/API read slice draft for employee request list + status/reviewState filters. |
| `planning/slices/SL-EMP-REQ-002-employee-request-details-read.md` | Full backend/API read slice draft for employee request details by id with Dapper/read projection direction. |
| `planning/slices/l2/README.md` | L2 slice navigation for Employee/Review/AgreementProposalExchange draft progression. |

## Replace

| File | Purpose |
|---|---|
| `planning/slices/README.md` | Adds L2 slice navigation and links to the new Employee request read slices. |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Adds/clarifies L2 source mapping and explicitly keeps scenario sources as source of truth for Scenario Flow/Behavior Coverage. |
| `planning/testing/server-slice-test-plan-rules.md` | Adds read-slice test-plan rules: API/read integration tests as primary proof; no unit tests by default. |
| `planning/testing/README.md` | Updates testing index to mention both read-slice and state-changing command test-plan rules. |
| `planning/slices/l1-slice-drafting-guide.md` | Adds mandatory reference to read-slice test plan rules and no-unit-tests-by-default guidance. |

## Source inputs

This archive is based on the two provided working drafts:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
```

The important test-plan correction from the conversation is preserved:

```text
Primary verification: API/read integration tests.
Do not add unit tests by default.
Unit tests are allowed only for reusable helper logic with non-trivial branching.
```

## Guardrails

```text
- Scenario files are the source of truth for Scenario Flow and Behavior Coverage.
- Domain draft is domain-design input, not a replacement for scenario sources.
- Registers are navigation/sync docs, not primary behavior sources.
- No code, tests or generated artifacts are included.
```
