# CL-A11Y-001 — Accessibility And ARIA

Status: current client-wide accessibility convention  
Type: client cross-cutting convention

## 1. Purpose

Define baseline accessibility rules for client implementation and tests.

## 2. Core Rules

```text
- Prefer native semantic HTML.
- Add ARIA only when native HTML is not enough.
- Every interactive element must have an accessible name.
- Button actions must use <button>.
- Navigation links must use links.
- Inputs/textareas/selects must have labels.
- Field errors should be associated with fields when possible.
- Disabled/unavailable actions should be understandable.
- Dialog/modal requires focus and keyboard plan.
- Dynamic important status/error may use role="status" or role="alert".
- Tests should prefer getByRole/getByLabelText over test ids.
```

## 3. `.client.md` Accessibility Contract

Each `.client.md` should include:

```text
## Accessibility / ARIA Contract
```

Table:

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|

## 4. Examples

```text
Approve button:
- Native element: button
- Accessible name: visible text
- Keyboard: Enter/Space activates
- ARIA: none if native button + text is enough
- Test: getByRole("button", { name: /approve/i })
```

```text
Form error summary:
- ARIA: role="alert" for important submit errors if appropriate
- Test: getByRole("alert")
```

## 5. Change Points

| ID | Behavior aspect | Change point | Current decision |
|---|---|---|---|
| CP-CL-A11Y-001 | Error announcement | role alert/status choice | use only when meaningful |
| CP-CL-A11Y-002 | Disabled actions | disabled vs explanation pattern | unavailable action must be understandable |
| CP-CL-A11Y-003 | Test selectors | role/label-first tests | prefer semantic queries |
