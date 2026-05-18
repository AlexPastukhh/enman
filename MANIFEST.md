# MANIFEST

Archive: `sl-emp-req-003-draft-refactor-v1`  
Review folder: `_archive-review/2026-05-19-sl-emp-req-003-draft-refactor-v1/`

## Purpose

Refactor one existing server slice draft without checking runtime implementation.

Slice:

```text
SL-EMP-REQ-003 — Start Request Review
```

## Replacement files

```text
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

## Original snapshots included

```text
_archive-review/2026-05-19-sl-emp-req-003-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-003-start-request-review.md
```

## Main draft changes

```text
- add Source / Domain / Slice Coverage Snapshot;
- add Implementation Sync Status with implementation-not-rechecked note;
- change success contract to 204 No Content;
- remove response DTO from first-pass contract;
- replace duplicate-start idempotent assumption with 422/no-mutation behavior;
- add Behavior-to-Test Trace;
- keep UI/page-flow/redirect work out of scope.
```

## Not included

```text
- no implementation inspection;
- no runtime code changes;
- no test changes;
- no generated file changes;
- no UI refactoring;
- no page redirect audit.
```
