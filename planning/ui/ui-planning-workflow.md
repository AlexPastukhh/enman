# UI Planning Workflow

Status: current UI planning workflow  
Scope: textual test-site UI planning before visual/HTML mockup generation

## 1. Purpose

UI planning creates a textual test-site plan for checking whether current use cases can be served by pages, visible data, inputs, actions, status states, validation/error states and navigation.

It is not final visual design, React implementation, backend route planning, or database design.

## 2. Inputs

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
```

## 3. Outputs

Create/update:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

Later, after the UI plan is reviewed, a separate mockup chat may create low-fidelity visual/HTML/React artifacts using:

```text
planning/ui/mockup-generation-guide.md
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

## 4. What Belongs In Scenario Specs

Scenario specs own mandatory observable behavior.

Examples:

```text
Client sees own requests list.
Each request shows current status.
Rejected request details show rejection feedback.
Employee can review only InReview requests.
Client can accept only employee-sent agreement proposal awaiting confirmation.
```

Do not put layout or component choices into scenario specs.

## 5. What Belongs In DATA Files

DATA files own what actor enters, sees, selects, filters by, attaches or references.

Examples:

```text
Request list visible DATA:
- request summary;
- object address;
- status.

Agreement proposal visible DATA:
- sender;
- status;
- attached document;
- text details/comment;
- related Approved request.
```

DATA files do not own layout, branches, validation or security policy.

## 6. What Belongs In Test Site UI Plan

The UI plan owns:

```text
- page inventory;
- scenario-to-page map;
- page responsibilities;
- visible DATA per page;
- inputs per page;
- actions per page;
- status-dependent states;
- validation/error states;
- empty states;
- page-to-page navigation;
- summary of open UI questions.
```

The UI plan is textual page planning, not a visual prototype.

## 7. What Belongs In UI Questions Register

The questions register owns unresolved UI alternatives.

Use it when:

```text
- several UI options are reasonable;
- the scenario does not force one UI solution;
- the choice depends on future domain/implementation decision;
- the UI plan would otherwise silently invent UX.
```

Each question should include:

```text
- id;
- status;
- affected scenarios;
- affected pages;
- question;
- options;
- current preference;
- what blocks final decision;
- notes.
```

Status values:

```text
Open
Tentative
Accepted
Rejected
Deferred
```

## 8. Workflow Steps

```text
1. Read scenario specs.
2. Read DATA files.
3. Read validation addendum.
4. Build page inventory.
5. Map scenarios to pages.
6. Define page responsibilities.
7. Define visible DATA and inputs per page.
8. Define actions and status-dependent states.
9. Define validation/error/empty states.
10. Register unclear UI alternatives in ui-questions-register.md.
11. Update scenario specs only if missing mandatory observable behavior is found.
12. Update DATA files only if missing required visible/input/selectable DATA is found.
```

## 9. Feedback Rules

UI planning can reveal that a scenario or DATA file is underspecified.

```text
If the UI plan reveals that the user must see, understand, or do something for the use case to be valid, update the scenario spec.

If the UI plan reveals that a page needs a specific visible/input/selectable/filter/attachment DATA item to support an already-defined scenario, update the DATA file.

If the issue is only layout, component choice, navigation style, or one of several acceptable UX options, keep it in test-site-ui-plan.md or ui-questions-register.md.
```

## 10. When To Create UI Plan

Create the UI plan now when:

```text
- scenario has clear actor/page/action/DATA;
- UI path is needed to validate the use case;
- page/action is needed for current core;
- status-dependent behavior is clear.
```

Mark as question when:

```text
- several UI options exist;
- exact UI depends on unresolved domain decision;
- action availability may change after implementation design;
- scenario is future/deferred;
- UI choice would overfit implementation too early.
```

## 11. What Not To Do

Do not create from this step:

```text
- final production visual design;
- React components;
- CSS design system;
- backend endpoints;
- database schema;
- final aggregate implementation.
```

Do not silently invent business behavior. Do not use stale scenario package summaries as source of truth.
