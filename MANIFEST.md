# MANIFEST — Employee Request Read Slice Full Drafts Sync

Archive: `employee-request-read-slices-full-drafts-sync.zip`  
Scope: docs-only slice planning update for L2 Employee request read drafts and read-slice test-plan rules

## Add

| File | Why |
|---|---|
| `planning/slices/SL-EMP-REQ-001-employee-request-list-read.md` | Full backend/API read slice draft for employee request list + filters. |
| `planning/slices/SL-EMP-REQ-002-employee-request-details-read.md` | Full backend/API read slice draft for employee request details by id. |
| `planning/slices/l2/README.md` | L2 slice drafting navigation for Employee request/review/agreement sequence. |

## Replace

| File | Why |
|---|---|
| `planning/slices/README.md` | Add L2 Employee request read drafts to slice navigation. |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Clarify scenario sources vs domain draft input and map SL-EMP-REQ-001/002 to scenario files. |
| `planning/testing/server-slice-test-plan-rules.md` | Add server read-slice test-plan rules: API/read integration primary, no unit tests by default. |

## Source inputs

Current user-provided drafts:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
```

Scenario/domain input relationship:

```text
Scenario text/DATA/UI/behavior files remain source of truth for Scenario Flow and Behavior Coverage.
planning/tables/domain-drafts/domain-draft-02.md is domain-design input, not a replacement for scenario sources.
```

## Not included

```text
- no runtime code;
- no tests;
- no generated artifacts;
- no GitHub branch/commit/PR.
```
