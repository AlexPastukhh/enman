# Client Cross-Cutting Planning

Status: current client cross-cutting index  
Scope: client-wide conventions used by multiple client sidecars

## Current Docs

| ID | File | Purpose |
|---|---|---|
| CL-FORM-VALIDATION-001 | CL-FORM-VALIDATION-001-deferred-validation.md | Deferred/client-side validation convention |
| CL-ERROR-HANDLING-001 | CL-ERROR-HANDLING-001-client-server-errors.md | Client-side handling of API errors; API boundary lives in `planning/api/` |
| CL-COMMAND-001 | CL-COMMAND-001-command-success-without-required-response-body.md | Command success convention for flows where HTTP success is enough and no response body is required by default |
| CL-FEEDBACK-001 | CL-FEEDBACK-001-client-feedback-messages.md | Client-wide feedback/message surface convention for success/info/warning/error outcomes |
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

Client-wide implementation conventions may be referenced by multiple sidecars, but each sidecar still owns its concrete success/error/navigation behavior.

Feedback/message conventions define reusable UI feedback direction.

They do not replace scenario UI behavior items or feature-specific `.client.md` verification plans.
