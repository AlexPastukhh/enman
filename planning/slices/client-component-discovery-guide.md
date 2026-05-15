# Client Component Discovery Guide

Status: current component discovery workflow  
Scope: how `.client.md` sidecars discover components, layouts, styling, accessibility and API-contract usage

## Purpose

Component discovery derives client components from scenario behavior, UI specs, DATA and slice behavior.

It answers:

```text
- which behavior/UI item this component covers;
- where it belongs in client architecture;
- what data it receives;
- what state it owns;
- whether it calls API;
- what API/error contract it uses;
- how it is styled;
- how it is accessible;
- how it is tested.
```

## Component / Layout Plan

Recommended columns:

| Component | Layer | Responsibility | Behavior/UI items | Data input | Owns state? | Calls API? | API contract used? | Styling owner | Reusable? | Test focus |
|---|---|---|---|---|---|---|---|---|---|---|

Accessibility columns may be included here or in a separate accessibility table:

| Semantic element / role | Accessible name source | Keyboard behavior | ARIA decision | Test query |
|---|---|---|---|---|

## Component Discovery Questions

```text
Purpose:
- Which behavior item or UI behavior item does this component cover?
- Is it read/display, command/action, layout/composition, or shared primitive?

Layer:
- Is it page, entity, feature, widget or shared?
- Why?

Data:
- What data does it receive?
- Whole DTO, generated OpenAPI type, or smaller view props?
- Is mapper needed?

API / Error Contract:
- Does it call API?
- Read or command?
- Should this be entity hook or feature hook instead?
- Which generated OpenAPI type is used?
- Which generated error codes/constants are used?
- Does it handle field errors, root errors, stale-state errors, or generic errors?
- Does it need DTO field -> form field mapping?

Accessibility:
- Native semantic element first?
- Accessible name source?
- Keyboard behavior?
- Is ARIA actually needed?
- What Testing Library query proves user-visible behavior?
```

Accessibility gate:

```text
If a component cannot be tested by role/name/label when it should be user-interactive or user-understandable,
check whether the component is correctly designed for the user.
```

## API Contract Rule

A component/feature must not invent API shape or error codes.

Use:

```text
planning/api/
parent slice API layer
generated OpenAPI types
generated shared constants JSON
```
