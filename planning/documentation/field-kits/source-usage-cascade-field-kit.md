# Source Usage Cascade Field Kit

Status: active reusable field kit  
Scope: setup guidance for defining source/consumer relationships and cascade review conventions for a project

## 1. Purpose

This field kit helps a project define how upstream sources and downstream consumers are tracked.

It answers:

```text
What counts as a source artifact/scope?
What counts as a consumer artifact/scope?
Which source changes trigger downstream review?
Which row fields should the project source usage register use?
When is metadata-only review enough?
Where should project-specific source usage conventions live?
```

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Principles | Invariant: source/consumer relationships and cascade review must be visible when downstream docs depend on upstream sources. |
| This field kit | Setup questions, source/consumer model, row-shape candidates and pilot criteria. |
| Project source usage profile | Concrete source categories, consumer categories, row fields and cascade conventions for one project. |
| Workflow | Repeated cascade review after a project profile/register exists. |
| Examples | Demonstrations only. |

## 3. Core Model

The unit is a source usage relationship:

```text
source artifact/scope
  -> consumer artifact/scope
  -> reviewed_against / sync_status / review_outcome when needed
```

This is relationship-first, not version-first.

Version markers support review and synchronization. They are not source truth by themselves.

## 4. Setup Questions

```text
1. What upstream artifacts can affect downstream docs?
2. How granular should source scopes be?
3. What downstream consumer files/scopes need review when upstream changes?
4. What source_status values are needed?
5. What sync_status values are needed?
6. What row fields are mandatory?
7. What row fields are optional?
8. What changes trigger review?
9. What review outcomes are allowed?
10. Where will project source usage profile/registers live?
11. What pilot proves the row shape before full workflow adoption?
```

## 5. Candidate Row Shape

Use only the fields that the project needs.

| Field | Meaning |
|---|---|
| source_id | Stable source identifier, often repo-relative path + section/scope. |
| source_scope | The precise source section/table/row/artifact used. |
| source_status | Whether the source is current, draft, accepted, superseded or historical. |
| consumer_id | File/section/scope that consumes the source. |
| consumer_scope | Exact downstream section or decision using the source. |
| reviewed_against | Source version/date/commit/state reviewed by the consumer. |
| sync_status | current, stale, needs-review, metadata-only-reviewed, not-applicable. |
| review_outcome | no-change, update-needed, follow-up, superseded, blocked. |
| last_reviewed | Date/commit/review marker. |
| notes | Short context only; owner logic stays in owner docs. |

## 6. Pilot Criteria

Before creating a heavy repeated workflow, run a pilot.

A good pilot should prove:

```text
- the source/consumer unit is clear enough;
- the row shape is not too heavy;
- cascade review can be targeted;
- metadata-only review cases are understandable;
- downstream reviewers know when source changes matter;
- the register/profile is useful enough to maintain.
```

## 7. Project Profile Shape

Create a project-specific file with this shape:

```text
# <Project> Source Usage Cascade Profile

## Purpose
## Source Categories
## Consumer Categories
## Row Shape
## Status Values
## Cascade Triggers
## Review Outcomes
## Pilot Scope
## Do Not
```

For the candidate Enman instance, see:

```text
planning/source-usage-cascade-profile.md
```

## 8. Example

Generic reusable example:

```text
planning/documentation/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
```
