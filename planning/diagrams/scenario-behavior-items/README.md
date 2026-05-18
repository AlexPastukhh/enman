# Scenario Behavior Items

Status: current behavior-item convention

## Purpose

Behavior item files extract and classify smaller behavior units from scenarios.

They help connect scenario source to slice drafts and to tests.

## Core rule

Behavior items are smaller than slices.

```text
One scenario can contain many behavior items.
One scenario can be implemented by multiple slices.
One slice can implement an independent chain of behavior items.
One slice can extend behavior already implemented by another slice.
```

## Behavior items are not implementation details

Behavior items describe observable/required behavior, not how it is implemented.

Good:

```text
When the user submits an invalid form, the relevant field error is visible near the field.
```

Bad:

```text
useMutation invalidates query key X.
```

## Slice relationship

A slice draft should list:

```text
behavior items covered
behavior items intentionally out of scope
related/future behavior items
```

The implementation plan and tests in the slice draft should prove that the covered behavior items are achieved.

## Test relationship

Test plans must trace behavior items to planned/actual tests.

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

A test is useful when it proves a behavior item or scenario outcome and has acceptable escape/refactor risk.

## Source relationship

Behavior items are extracted from:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-cross-cutting/
planning/diagrams/scenario-data/
```

Do not invent behavior items in a slice draft when there is a relevant source file that should own them.
