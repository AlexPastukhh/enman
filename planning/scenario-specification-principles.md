# Scenario Specification Principles

Status: source of truth for general scenario specification principles  
Scope: textual scenario specs, scenario diagrams and scenario DATA specs

This file defines general rules that are not tied to one scenario. Concrete scenario details live in `planning/diagrams/scenario-text-specs/`; scenario DATA lives in `planning/diagrams/scenario-data/`.

## 1. Purpose

Scenario specifications describe user-facing behavior and planning semantics. They bridge user-facing behavior -> textual scenario specs -> diagrams -> DATA specs -> responsibility tables -> architecture planning.

They must not become domain model, DB schema, DTO/API contract, controller/handler flow, React component spec, implementation plan, responsibility table or aggregate boundary design.

## 2. Artifact Roles

```text
planning/scenario-specification-principles.md = general scenario specification principles
planning/diagram-scenario-spec.md = visual scenario/use-case diagram semantics
planning/diagrams/scenario-text-specs/ = concrete per-scenario textual specs
planning/diagrams/scenario-data/ = concrete per-scenario DATA specs
planning/diagrams/scenario-data-spec.md = compatibility pointer to scenario-data/
```

## 3. Scenario Unit

The basic unit is `Actor + UX/business context + goal`.

## 4. Entry Points

Entry point = actor starts a scenario from a UX/business context.

Good: `Client selects request from My Requests list`, `Employee starts review from dashboard`, `Guest follows recovery link from email`.

Avoid as default wording: `from URL`, `from route`, `from controller`, `from notification provider`, `from email provider`.

Password recovery explicitly uses email, so `recovery email` and `recovery link from email` are accepted UX wording.

## 5. Off-Page Links

Off-page link = current scenario opens or leaves to another scenario/page. Use `EXTND` in item refs. `[EXT]` is a roadmap marker, not an item-ref code.

## 6. Triggered By

Use `Triggered by` rarely. It means a non-actor/system/background/domain process activates a scenario and no actor clearly starts it.

Do not use triggered-by for normal actor navigation or employee actions. SC-14 Client Data Verification is future employee-started behavior, not triggered-by.

## 7. DATA Blocks

`DATA` replaces the old term `DETAIL`.

DATA = scenario-relevant business data that actor enters, sees, selects, filters by, or attaches/uploads.

Allowed DATA types:

```text
Input DATA
Visible DATA
Selection DATA
Filter DATA
Attachment DATA
Reference DATA, only when useful
```

Do not use as DATA types:

```text
Decision DATA
Response DATA
Policy DATA
```

Decision/response behavior belongs to branches/outcomes. Policy/access behavior belongs to preconditions, invariants or security specs.

DATA must not become DTO, DB schema, entity properties, API contract, React state, EF/SQL columns or arbitrary common app fields.

## 8. DATA Inclusion Rule

Do not add DATA fields just because they are common. Add DATA only when needed for scenario logic, actor-visible behavior, current/discussed future UX, filtering/searching with clear UX value, identifying a business item, visible status/feedback/result, or later domain/storage planning.

Date/time is not core by default. Use only when meaningful business-visible DATA; otherwise keep as `[VAR:EXPAND]`.

## 9. DATA Files Must Stay Narrow

DATA files must not contain sections such as validation/rules, testable behavior, invariants, preconditions, branches, access rules or security policy.

DATA files may contain: Purpose, DATA blocks, Type, Actor, Used by, Input DATA, Visible DATA, Selection DATA, Filter DATA, Attachment DATA, Reference DATA, Extension/Future DATA, Notes, Open questions, Scenario spec references.

## 10. Validation, Branches And Invariants

Validation behavior belongs to scenario specs, not DATA files. Correctable validation errors usually loop back to input.

Invariants belong to scenario specs and attach to enforcement points: decision, guarded transition, action visibility, access check or state transition.

Errors and invalid branches are not automatically `[ALT]`. Use `[ALT]` narrowly only for an alternative way to achieve the same/equivalent goal.

## 11. Concrete Scenario Text Specs

Recommended structure: Status, Purpose, Actor/Screen, Entry Points, Preconditions, DATA, Main Flow, Branches, Invariants, Step Postconditions, Outcomes, Open Questions, ADR Candidates, Diagram Notes.

## 12. Current Project Decisions

Request statuses: `InReview`, `Approved`, `Rejected`. Do not use `Submitted`.

Standalone SC-08 Approved Result and SC-09 Rejected Result are removed/merged. SC-12 is merged into SC-05 + SC-04. SC-16 is removed. SC-18 is deferred.

SC-10 Applicant DATA is pending separate applicant discussion. SC-13 Agreement/Proposal is pending separate discussion.

SC-14 is future employee-started Client Data Verification; mocking is ADR/implementation note, not scenario name.

## 13. Read Order

```text
1. planning/scenario-specification-principles.md
2. planning/diagram-scenario-spec.md
3. planning/diagrams/scenario-data/00-scenario-data-index.md, if available
4. planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md, if available
5. concrete scenario specs
6. scenario diagram packages, if available
7. planning/diagram-common-mistakes.md
```
