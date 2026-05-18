# Client Accessibility / ARIA Workflow

Status: current client-wide accessibility and test convention

## 1. Purpose

Accessibility is not an afterthought and ARIA is not decoration.

This file is a design and test contract for client components.

It applies to:

```text
client/component tests
Playwright E2E locators
Component / Layout Plan accessibility columns
```

## 2. Core Principle

```text
Native semantic HTML first.
ARIA only when native HTML is not enough.
```

Wrong ARIA can make accessibility worse.

## 3. Core Rules

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
- Tests should prefer getByRole/getByLabelText/getByLabel over test ids.
```

## 4. Accessibility As Test Contract

A component should normally be testable through user-visible semantics:

```text
Testing Library:
- getByRole
- getByLabelText
- getByText when role/label is not the right semantic target

Playwright:
- page.getByRole
- page.getByLabel
- page.getByText when role/label is not the right semantic target
```

Use test ids only when there is no meaningful user-visible semantic target.

## 5. `.client.md` Accessibility / ARIA Contract

Each `.client.md` must include:

```text
## Accessibility / ARIA Contract
```

Table:

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|

## 6. Where ARIA May Be Needed

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
