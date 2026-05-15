# CL-A11Y-001 — Accessibility And ARIA

Status: current client-wide accessibility and client-test convention  
Type: client cross-cutting convention

## Purpose

Accessibility is not an afterthought and ARIA is not decoration.

This file is a design and test contract for client components.

## Core Principle

```text
Native semantic HTML first.
ARIA only when native HTML is not enough.
```

Wrong ARIA can make accessibility worse.

## Core Rules

```text
- Prefer native semantic HTML.
- Add ARIA only to fill semantic gaps.
- Do not duplicate native roles without a real reason.
- Every interactive element must have an accessible name.
- Button actions must use <button>.
- Navigation must use links / routing links.
- Form fields must have connected labels.
- Field errors should be associated with fields when possible.
- Disabled/unavailable actions should be understandable.
- Dialog/modal requires focus and keyboard plan.
- Dynamic important status/error may use role="status" or role="alert".
- Tests should prefer getByRole/getByLabelText over test ids.
```

## Accessibility As Test Contract

A component should normally be testable through user-visible semantics:

```text
getByRole
getByLabelText
getByText, when role/label is not the right semantic target
```

If a component cannot be found by role/name/label when it should be user-interactive or user-understandable, re-check the component design.

Use test ids only when there is no meaningful user-visible semantic target.

## `.client.md` Accessibility / ARIA Contract

Each `.client.md` must include:

```text
## Accessibility / ARIA Contract
```

Table:

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|

## Examples

| Component | Native semantic element | Accessible name | Keyboard | ARIA | Test query |
|---|---|---|---|---|---|
| ApproveRequestAction | `button` | visible button text | Enter/Space | none if native text button | `getByRole("button", { name: /approve/i })` |
| Reject feedback | `textarea` + `label` | associated label | native textarea | `aria-describedby` / `aria-invalid` when error shown | `getByRole("textbox", { name: /feedback/i })` |
| Empty feedback warning | inline alert or dialog | warning text/heading | dialog needs focus plan | `role="alert"` or `role="dialog"` when needed | `getByRole("alert")` or dialog query |
| Status badge | text/span | visible status text | none | `role="status"` only for dynamic update | text/status query |

## Where ARIA May Be Needed

```text
- modal/dialog;
- inline alert/error summary;
- loading/status live region;
- custom tabs/accordion if they appear;
- aria-describedby for field help/error text;
- aria-invalid for invalid field state;
- aria-current for active navigation link;
- accessible names for icon-only buttons.
```

ARIA is usually not needed for native buttons, inputs with labels, textareas with labels, selects with labels, links, headings and native form controls.
