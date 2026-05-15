# CL-COMMAND-001 — Command Success Without Required Response Body

Status: current client-wide convention  
Type: implementation convention  
Scope: client command flows  
Applies to: create/update/submit commands where the next UI step does not need returned entity data

## 1. Purpose

Define a client-side implementation convention for command flows where the scenario can continue after HTTP success without using returned entity data.

This convention is not a domain requirement.

This convention is not an API rule that forbids response bodies.

It only says that the client implementation does not require a response DTO by default when the next UI step can be completed from command success alone.

## 2. Core Convention

For command flows where the client does not need created or updated entity data to continue the scenario, HTTP success is enough to confirm successful command completion.

The UI should:

```text
- treat HTTP success as command confirmation;
- show a success message;
- navigate to the next expected read-context screen;
- not require id, status or other response DTO fields unless the scenario explicitly needs them.
```

## 3. Default Flow

```text
[Client command mutation]
Receives HTTP success
        ↓
[UI]
Shows success message
        ↓
[Navigation]
Moves to the next expected read-context screen
```

## 4. L1 Connection Request Creation Convention

For L1 connection request creation, the default client outcome is:

```text
HTTP success
        ↓
show success message
        ↓
navigate to My Requests
```

Example success message:

```text
Заявка создана и отправлена на рассмотрение.
```

Current implementation direction:

```text
- client submits request data;
- server derives account/applicant context from authenticated user context;
- client does not need requestId, status, account identity or applicant identity to show the initial command success outcome;
- the final My Requests route/read behavior belongs to the read/list requests slice or client sidecar.
```

## 5. When A Response Body Is Needed

A command response body is still appropriate when the next scenario step needs returned data.

Examples:

```text
- navigate directly to a created entity details page that requires a returned id;
- show server-computed values that cannot be derived locally;
- continue a multi-step workflow with a returned token, version, state or action availability;
- update an in-place read model without a refetch;
- satisfy an explicit scenario/UI requirement for returned data.
```

If the scenario or UI spec explicitly needs returned data, the slice/client sidecar must state that need and update the API contract accordingly.

## 6. What Sidecars Must State

For each command flow using this convention, the `.client.md` or shortened client draft should state:

```text
- command action;
- success UI message;
- next read-context screen / navigation target;
- whether returned entity data is required;
- what happens on HTTP success;
- what happens on ProblemDetails/API error;
- tests or E2E coverage for the success outcome.
```

## 7. Relationship To Scenario UI Specs

Scenario UI specs may say that the user should see a success outcome after a command.

They should not silently turn that outcome into a domain/API requirement to return `id`, `status` or a full response DTO.

Use this convention when:

```text
success outcome is visible to the user
and
the next UI step can be completed without returned entity data
```

## 8. Relationship To API Contract

This convention does not remove the need for explicit API contract planning.

For each command endpoint, the parent slice still owns:

```text
- endpoint;
- method;
- request DTO;
- response status codes;
- ProblemDetails/error responses;
- whether a response DTO is required.
```

If no response body is required, the contract can make that explicit.

## 9. Tests / Verification

Client sidecars should verify the user-visible behavior, not just the HTTP call.

Possible checks:

```text
- successful mutation shows the configured success message;
- successful mutation navigates to the expected read-context screen;
- successful mutation does not require requestId/status/body fields;
- API error does not show the success message;
- API error preserves user input where appropriate;
- E2E happy path confirms submit -> HTTP success -> success outcome -> navigation.
```
