# Client Component Discovery Guide

Status: current component discovery workflow  
Scope: how `.client.md` sidecars discover components, layouts, styling and accessibility contracts

## 1. Purpose

Component discovery derives client components from scenario behavior, UI specs, DATA and slice behavior.

It answers:

```text
- which behavior/UI item this component covers;
- where it belongs in client architecture;
- what data it receives;
- what state it owns;
- whether it calls API;
- how it is styled;
- how it is accessible;
- how it is tested.
```

## 2. Component / Layout Plan

Each `.client.md` should include:

```text
## Component / Layout Plan
```

Recommended columns:

| Component | Layer | Responsibility | Behavior/UI items | Data input | Owns state? | Calls API? | Styling owner | Reusable? | Test focus |
|---|---|---|---|---|---|---|---|---|---|

Accessibility columns may be included here or in a separate accessibility table:

| Semantic element / role | Accessible name source | Keyboard behavior | ARIA decision | Test query |
|---|---|---|---|---|

## 3. Component Discovery Questions

```text
Purpose:
- Which behavior item or UI behavior item does this component cover?
- Is it read/display, command/action, layout/composition, or shared primitive?

Layer:
- Is it page, entity, feature, widget or shared?
- Why?

Data:
- What data does it receive?
- Whole DTO or smaller view props?
- Is mapper needed?

State:
- Does it own page state, form state, mutation state, visual state?
- Should state be lifted?

API:
- Does it call API?
- Read or command?
- Should this be entity hook or feature hook instead?

Reuse:
- Page-local, entity display, feature action, widget, or shared primitive?
- Is reuse real now or speculative?

Styling:
- Who owns internal styling?
- Who owns layout placement?
- Which styling change points are needed?

Accessibility:
- Native element first?
- Accessible name?
- Keyboard behavior?
- ARIA needed?
- Test query?

Tests:
- Which minimal test proves behavior coverage?
- Is this component tested directly or through page/feature test?
```

## 4. Layer Placement Quick Guide

| Component kind | Preferred layer | Example |
|---|---|---|
| Route/screen composition | pages | EmployeeRequestReviewPage |
| Page-local list/table/filter | pages/.../components | EmployeeRequestsTable |
| Business data display | entities | RequestStatusBadge |
| Command action/form | features | ApproveRequestAction |
| Reusable large composition | widgets, only if reused | EmployeeRequestDetailsPanel |
| Domain-agnostic primitive | shared | Button |

## 5. Rule Against Premature Widgets

A widget is introduced only after a large page-local block is reused or is clearly about to be reused by another concrete page/slice.
