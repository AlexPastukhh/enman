# Client Architecture Principles For Slice Sidecars

Status: current client architecture planning principles / entity-feature API ownership synchronized  
Scope: frontend/client implementation mapping for `.client.md` sidecars

## 1. Mapping

```text
Read slice    -> pages + entities + shared transport/generated infrastructure
Command slice -> pages + features + entities + shared transport/generated infrastructure
Shared concern -> app / shared
```

## 2. Client API Placement

Current accepted direction:

```text
shared/
  only truly shared infrastructure.

entities/*/
  read/data/display ownership.

features/*/
  command/user-action ownership.
```

Detailed decision:

```text
planning/client/client-api-placement-decision.md
planning/client/client-layering-for-read-and-command-slices.md
```

## 3. Architecture Terms

### pages

Route-level composition. A page may read route params, load read context through entity query hooks, show loading/error/not-found/forbidden states, compose entity display components and render command features.

A page should not contain the core command logic itself.

### entities

Reusable client read/data/display modules.

Owns:

```text
read API functions
read endpoint paths
read DTO aliases/mapping from generated OpenAPI types
query keys
generic read query hooks
read DTO/view types
status/read helpers
reusable display components
```

Does not own command mutations or command endpoint wrappers.

### features

User command/action behavior.

Owns:

```text
command API/mutation functions
command endpoint paths
command DTO/result aliases from generated OpenAPI types
form values tied to command behavior
mutation hooks
action/form components
command success/error behavior
query invalidation/refetch after command success
```

### shared

Domain-agnostic primitives and utilities.

Owns:

```text
fetchJson
ProblemDetails / ApiError
CSRF/antiforgery transport helpers
generated OpenAPI types
generic URL/query helpers
UI primitives/config/lib
```

Does not own business-specific wrappers such as `listEmployeeDashboardRequests()` or `startRequestReview()`.

## 4. Component Placement Rule

| Component kind | Preferred layer | Example |
|---|---|---|
| Route/screen composition | pages | EmployeeRequestDetailsPage |
| Business data display | entities | RequestStatusBadge |
| Read endpoint wrapper | entities/api | getEmployeeRequestDetails |
| Command endpoint wrapper | features/api | startRequestReview |
| Command action/form | features/ui | ApproveRequestAction |
| Domain-agnostic primitive | shared | Button |
