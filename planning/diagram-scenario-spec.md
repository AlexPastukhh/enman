# Diagram Scenario Specification

Status: source of truth for user-facing use-case/scenario diagram representation  
Scope: scenario/use-case diagrams only

General principles are controlled by `planning/scenario-specification-principles.md`. This file explains visual representation.

## Core Rule

Scenario diagrams are behavioral specification diagrams. They describe actor, screen/context, goal, entry points, preconditions, DATA refs, main flow, branches, actor choices, includes, off-page links, invariants, postconditions, outcomes and markers.

They must not primarily describe controller, endpoint, handler, repository, DbContext, SQL, aggregate method, command/query class, React hook or provider API mechanics.

## Node Types

### Actor / Screen Node

Actor, screen/context, goal, level marker.

### Entry Point Node

Use for actor-started UX/business contexts. Do not use off-page links only to explain where the current scenario can be opened from.

### Main Flow Node

Short action/result label + strict ref.

### Decision / Branch Node

Conditional scenario behavior. Not an extension by itself.

### Actor Choice Node

Deliberate actor choice. Use EXTND if it opens/leaves to another scenario.

### Include Node

Use explicit `<<include>>` for mandatory reusable steps and attach to exact step.

### Off-Page / Subscenario Link Node

Use for paths leaving the current page. Item refs use `EXTND`, not `EXT`.

### DATA Node

DATA replaces DETAIL.

DATA may describe Input DATA, Visible DATA, Selection DATA, Filter DATA, Attachment DATA, or useful Reference DATA.

DATA must not describe branches, invariants, preconditions, access rules, validation flows, tests, DTO fields, DB columns, entity properties or React state.

Examples:

```text
Registration input DATA
SC-01-DATA-01

Own Request Details visible DATA
SC-05-DATA-03
```

### Invariant Node

Attach to the enforcement point.

### Postcondition / Outcome Node

Step-level postconditions attach to producing steps. Scenario end states stay compact.

## Item Reference Codes

```text
PRE, SPRE, STEP, BR, INC, EXTND, INV, POST, SPOST, OUT, DATA, AC, Q
```

Bad: `SC-02-EXT-01`, `SC-05-DETAIL-01`.

Good: `SC-02-EXTND-01`, `SC-05-DATA-01`.

## Branches And Alternatives

Errors are not automatically `[ALT]`. Use `[ALT]` only for a planned alternative way to achieve the same/equivalent goal when normal path is not suitable.

Correctable validation errors loop back to input.

## Current Project Decisions

Use statuses: `InReview`, `Approved`, `Rejected`. Do not use `Submitted`.

SC-08 and SC-09 are merged/removed. SC-12 is merged into SC-05 + SC-04. SC-16 is removed. SC-18 is deferred.

SC-10 Applicant DATA and SC-13 Agreement/Proposal are pending discussion.

SC-14 is future employee-started Client Data Verification, not triggered-by.

## Layout Rules

Main flow is visual backbone. Keep connectors local, avoid connector webs, avoid text-card-only diagrams, keep all text inside shapes, use spacious canvas and readable consistent theme.
