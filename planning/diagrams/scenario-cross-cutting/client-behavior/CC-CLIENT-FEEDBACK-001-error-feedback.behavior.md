# CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility Behavior

Status: current first-pass behavior source  
Doc version: v0.1.0  
Type: cross-cutting client behavior  
Applies to: client read and command flows

## 1. User Goal

The user should see clear, actionable feedback for loading, empty, error, validation, pending and success states.

## 2. Behavior Flow

```text
Client operation starts
        ↓
UI shows stable pending/loading state if needed
        ↓
Operation succeeds or fails
        ↓
Success updates visible state or shows success outcome
        ↓
Failure shows actionable field/root/page error
```

## 3. Behavior Items

| ID | Behavior |
|---|---|
| `CC-CLIENT-FEEDBACK-001-B01` | Loading/pending state should not break layout or hide the page shell. |
| `CC-CLIENT-FEEDBACK-001-B02` | Empty states must be distinguishable from errors. |
| `CC-CLIENT-FEEDBACK-001-B03` | Field errors are shown near fields when field-specific. |
| `CC-CLIENT-FEEDBACK-001-B04` | Root/domain errors are shown in a form/page error area. |
| `CC-CLIENT-FEEDBACK-001-B05` | Do not show only `Something went wrong` when server provides useful messages. |
| `CC-CLIENT-FEEDBACK-001-B06` | Disabled/unavailable actions must be understandable when visible. |
| `CC-CLIENT-FEEDBACK-001-B07` | Success should result in visible refreshed state, navigation, or success message according to the slice. |

## 4. Out of Scope

```text
specific toast library
React Query implementation
exact copy text for every scenario
```

## 5. Related Implementation Drafts

Future:

```text
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FEEDBACK-001-error-feedback.client.md
```
