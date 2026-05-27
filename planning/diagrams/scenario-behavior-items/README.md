# Scenario Behavior Items

Status: current behavior-item convention / business behavior with UI/UX projection support

## Purpose

Behavior item files extract and classify smaller behavior units from scenarios.

They help connect scenario source to domain drafts, slice drafts, client sidecars and tests.

## Core rule

Behavior items are primarily processed business scenario requirements.

They are smaller than slices:

```text
One scenario can contain many behavior items.
One scenario can be implemented by multiple slices.
One slice can implement an independent chain of behavior items.
One slice can extend behavior already implemented by another slice.
```

## UI / UX Projection Rule

UI scenario requirements should usually be represented as UI/UX projection notes on DATA items or business behavior items.

Examples of UI/UX projection fields:

```text
visibility;
action availability;
disabled/loading state;
deferred validation;
success/error feedback;
empty/loading/error state;
accessibility/focus/announcement behavior;
visual marker / status representation.
```

Separate UI-only behavior entries are allowed only when the requirement is independently presentation/interaction-specific and cannot be attached cleanly to one business behavior item.

Every UI/UX projection or UI-only behavior must reference at least one source:

```text
core scenario;
DATA item;
UI scenario section;
accepted decision/clarification.
```

Do not promote UI presentation detail into domain/business behavior.

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
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-cross-cutting/
```

Do not invent behavior items in a slice draft when there is a relevant source file that should own them.
