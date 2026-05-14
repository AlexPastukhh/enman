# Slice Planning Index

Status: current slice-planning guide  
Scope: scenario-derived slice discovery after the first green L1 domain foundation

## 1. Purpose

This folder documents how to derive implementation slices from scenarios and scenario-derived behavior items.

A slice is not a controller, endpoint, repository, table, React component, or aggregate.

Working definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A slice may be:

```text
full-stack
backend/application
read/query
UI-only
extension
dependent
cross-cutting
plugin / external-integration
```

## 2. Relationship To L1 Domain Cut

The current L1 domain implementation cut is a domain-foundation cut, not a full scenario slice.

It exists to stabilize:

```text
Account / ClientAccount
ApplicantParty / IndividualApplicantParty
ConnectionRequest / review behavior
domain unit tests
```

After the first L1 domain classes and unit tests are green, use slice planning to move toward application/API/persistence/UI work.

Do not treat `planning/l1-domain-implementation-cut.md` as a replacement for slice planning.

## 3. Current Slice Drafting Guide

Use:

```text
planning/slices/l1-slice-drafting-guide.md
```

This guide defines:

```text
- slice discovery questions;
- scenario section structure;
- slice card structure;
- behavior item coverage;
- candidate missing item handling;
- per-slice questions;
- consolidated questions;
- test coverage;
- ADR candidate collection.
```

## 4. Source Basis For Slice Drafts

Slice drafts are derived from:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

After implementation starts, also use the actual L1 domain implementation result and tests as a reality check.

## 5. Drafting Style

Use the same broad style as domain drafts:

```text
- readable sections;
- traceable references;
- questions near the relevant slice;
- all questions consolidated in one place at the end;
- coverage tables near the end;
- explicit deferred/out-of-scope notes;
- ADR candidates collected as they emerge.
```

The central section unit is the scenario.

Inside each scenario section, list the slices derived from that scenario.

## 6. Planned Slice Artifacts

Expected future files:

```text
planning/slices/l1-slice-draft-01.md
planning/slices/l1-slice-draft-02.md
planning/slices/l1-scenario-to-slice-map.md
planning/slices/l1-slice-delivery-plan.md
```

Do not create these files until the relevant planning step is explicitly requested.

## 7. Agent Rules

Slice-planning agents should:

```text
- start from scenarios, not endpoints;
- collect scenario-derived behavior items before naming slices;
- include DATA facts when they shape behavior;
- ask slice discovery questions for every scenario;
- treat independent testability as a primary slice boundary criterion;
- include domain/application/persistence/read/API/UI/auth/infra/test notes per slice;
- mark missing UI/read/UX behavior items as candidates instead of silently ignoring them;
- keep all slice questions consolidated at the end;
- collect ADR candidates when decisions affect multiple slices or architecture boundaries.
```
