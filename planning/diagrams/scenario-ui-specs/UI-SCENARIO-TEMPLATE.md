# UI Scenario Template

Status: canonical template for scenario-level UI requirements

Copy this structure for new UI scenario specs.

```markdown
# <SC-ID> — <Title> UI Scenario

Status:
Applies to:
Actors:
Related scenario:
Related slices:

## 1. User Goal

State what the user is trying to accomplish.

## 2. Screen Entry Points

List routes/entry points in user-facing terms.

```text
User opens ...
        ↓
System shows ...
```

## 3. Screen Composition

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

## 4. Visible Data

List data the user must see.

## 5. Actions

List visible actions and when they are available/unavailable.

## 6. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|

## 7. Empty / Loading / Error States

Describe visible loading, empty, validation, access and server error states.

## 8. Actor-Specific Differences

Describe differences for Guest / Client / Employee if relevant.

If no differences:

```text
No actor-specific UI differences beyond access.
```

## 9. Feedback / Validation Requirements

Describe field feedback, form/root errors, success feedback and warnings.

## 10. Accessibility Notes

Describe semantic expectations, labels, keyboard behavior and alerts when relevant.

## 11. Out of Scope

State what this UI scenario does not cover.

## 12. Related Client Slice Drafts

List client slice drafts that consume this UI scenario source.
```
