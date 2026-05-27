# UI Scenario Template

Status: canonical template for scenario-level UI presentation / UX requirements

Copy this structure for new UI scenario specs.

# `<SC-ID>` — `<Title>` UI Scenario

Status:
Applies to:
Actors:
Related core scenario:
Related DATA files/items:
Related business behavior items:
Related slices:

## 1. User Goal

State what the user is trying to accomplish from the UI.

## 2. Core Scenario / DATA Alignment

This UI scenario presents:

```text
core scenario behavior:
- ...

DATA items:
- ...
```

This UI scenario must not introduce new business behavior.

Potential consistency questions:
- ...

## 3. Screen Entry Points

List routes/entry points in user-facing terms.

```text
User opens ...
        ↓
System shows ...
```

## 4. Screen Composition

Describe visible screen blocks.

```text
[AppShell]
  Header
  Main page area
    Page title / intro
    Primary content block
    Action area
  Footer
```

Do not describe React ownership here.

## 5. DATA Presentation

| DATA item | Presentation / UX requirement | Source |
|---|---|---|
| ... | ... | ... |

## 6. Business Behavior Presentation

| Behavior item | UI/UX projection | Source |
|---|---|---|
| ... | ... | ... |

## 7. Visible Actions

List visible actions and when they are available/unavailable.

## 8. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|

## 9. Empty / Loading / Error States

Describe visible loading, empty, validation, access and server error states.

## 10. Feedback / Deferred Validation Requirements

Describe field feedback, form/root errors, success feedback, warnings and deferred validation behavior.

## 11. UI-Only Interaction Requirements

Use only for presentation/interaction behavior that does not map cleanly to one business behavior item.

Examples:
- focus behavior;
- keyboard navigation;
- animation;
- responsive layout behavior;
- visual marker;
- loading/empty/error UX.

## 12. Actor-Specific Differences

Describe differences for Guest / Client / Employee if relevant.

If no differences:

```text
No actor-specific UI differences beyond access.
```

## 13. Accessibility Notes

Describe semantic expectations, labels, keyboard behavior and alerts when relevant.

## 14. Out of Scope

State what this UI scenario does not cover.

## 15. Related Client Slice Drafts

List client slice drafts that consume this UI scenario source.
