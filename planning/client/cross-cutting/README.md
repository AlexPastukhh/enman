# Client Cross-Cutting Planning

Status: current client cross-cutting index  
Scope: client-wide conventions used by multiple client sidecars

## Current Docs

| ID | File | Purpose |
|---|---|---|
| CL-FORM-VALIDATION-001 | CL-FORM-VALIDATION-001-deferred-validation.md | Deferred/client-side validation convention |
| CL-ERROR-HANDLING-001 | CL-ERROR-HANDLING-001-client-server-errors.md | Client-side handling of API errors; API boundary lives in `planning/api/` |
| CL-STYLING-001 | CL-STYLING-001-css-modules-tokens.md | CSS Modules, CSS variables/tokens, styling ownership |
| CL-A11Y-001 | CL-A11Y-001-accessibility-and-aria.md | Accessibility as component/test contract |

## API Boundary Docs

Server/client API boundary docs live in:

```text
planning/api/
```

Use those docs for OpenAPI, ProblemDetails, ServerError / ServerValidationError, client-facing error codes and generated shared constants JSON.

## Rule

Do not use cross-cutting docs to hide concrete slice behavior.

Concrete behavior coverage belongs in scenario UI specs and `.client.md` files.
