# Client Cross-Cutting Planning

Status: current client cross-cutting index  
Scope: client-wide conventions used by multiple client sidecars

## 1. Purpose

Client cross-cutting docs describe behavior and implementation conventions reused by multiple client slices.

They are not business slices by themselves.

## 2. Current Docs

| ID | File | Purpose |
|---|---|---|
| CL-FORM-VALIDATION-001 | CL-FORM-VALIDATION-001-deferred-validation.md | Deferred/client-side validation convention |
| CL-ERROR-HANDLING-001 | CL-ERROR-HANDLING-001-client-server-errors.md | Server/client error mapping convention |
| CL-STYLING-001 | CL-STYLING-001-css-modules-tokens.md | CSS Modules, CSS variables/tokens, styling ownership |
| CL-A11Y-001 | CL-A11Y-001-accessibility-and-aria.md | Accessibility and ARIA contract |

## 3. Usage

A `.client.md` sidecar should reference these docs when the slice uses forms, error handling, styling, or accessibility-sensitive UI.

## 4. Rule

Do not use cross-cutting docs to hide concrete slice behavior.

Concrete behavior coverage belongs in scenario UI specs and `.client.md` files.
