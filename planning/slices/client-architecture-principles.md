# Client Architecture Principles For Slice Sidecars

Status: current client architecture planning principles  
Scope: frontend/client implementation mapping for `.client.md` sidecars

## 1. Purpose

This file explains how client-side implementation should be planned for vertical slices.

It belongs near slice planning because `.client.md` sidecars describe the client/UI layer of a vertical slice.

Core rule:

```text
Planning slice and frontend feature are not 1:1.
```

In planning:

```text
Read behavior is still a slice.
Command behavior is still a slice.
```

In frontend code:

```text
Read slice    -> pages + entities (+ widgets if reused)
Command slice -> pages + features + entities
Shared concern -> app / shared
```

## 2. Frontend Architecture Terms

### app

Application infrastructure and cross-cutting setup.

Typical contents:

```text
app/router/
app/providers/
app/guards/
app/layouts/
app/config/
```

Owns router setup, providers, query client setup, auth/session provider, route guards, activation/role guards and global layouts.

Does not own business actions such as approve/reject/create request.

### pages

Route-level composition.

A page may:

```text
- read route params;
- load read context through entity query hooks;
- show loading/error/not found/forbidden states;
- compose entity display components;
- render command features;
- own page-local read-context components.
```

A page should not contain the core command logic itself.

### entities

Reusable client read/data modules for business objects.

This is not a backend domain entity and not a backend repository.

Think of it as:

```text
client read module / query module / display module
```

Typical contents:

```text
entities/request/api/
entities/request/logic/
entities/request/types/
entities/request/components/

entities/applicant-party/api/
entities/applicant-party/logic/
entities/applicant-party/types/
entities/applicant-party/components/
```

Owns read API functions, query keys, generic read query hooks, read DTO/view types, status helpers and small/medium reusable display components for business data.

Does not own command mutations, feature-specific success/error orchestration, navigation caused by a command, or full screen/read-context layouts.

### features

User command/action behavior.

Typical examples:

```text
features/approve-request/
features/reject-request/
features/create-request/
features/save-applicant-party/
```

Owns command API/mutation functions, command DTO/result types, form values tied to command behavior, mutation hooks, command-specific normalization/DTO mapping, action/form components and command client tests.

### widgets

Optional reusable large read-context/composition blocks.

Do not introduce widgets by default.

Use widgets only when a large page-local block becomes reused by multiple pages.

Example:

```text
pages/employee-request-details/components/EmployeeRequestDetailsPanel.tsx
```

may become:

```text
widgets/employee-request-details-panel/EmployeeRequestDetailsPanel.tsx
```

if reused by both request details and review pages.

### shared

Domain-agnostic primitives.

Owns:

```text
shared/ui/Button.tsx
shared/ui/Card.tsx
shared/ui/FormField.tsx
shared/ui/Modal.tsx
shared/api/fetchWrapper.ts
shared/api/problemDetails.ts
shared/lib/formatDate.ts
shared/lib/normalizeEmptyString.ts
```

Does not own business-specific types/components such as:

```text
RequestStatusBadge
ApplicantSummaryCard
ApproveRequestDto
RejectRequestForm
```

## 3. Read Slice Mapping

A read slice is a real planning slice.

Example:

```text
SL-EMP-READ-001 — Employee request dashboard/details
```

Client implementation usually maps to:

```text
pages/employee-requests/
  EmployeeRequestsPage.tsx
  components/
    EmployeeRequestsTable.tsx
    EmployeeRequestsFilters.tsx
    EmployeeRequestsEmptyState.tsx

pages/employee-request-details/
  EmployeeRequestDetailsPage.tsx
  components/
    EmployeeRequestDetailsHeader.tsx
    EmployeeRequestDetailsActionsPanel.tsx

entities/request/
  api/
    getEmployeeRequests.ts
    getEmployeeRequestDetails.ts
  logic/
    useEmployeeRequests.ts
    useEmployeeRequestDetails.ts
    requestQueryKeys.ts
  types/
    requestTypes.ts
  components/
    RequestStatusBadge.tsx
    RequestSummaryCard.tsx

entities/applicant-party/
  types/
    applicantPartyTypes.ts
  components/
    ApplicantSummaryCard.tsx
    ApplicantVerificationBadge.tsx
```

Read slice client logic:

```text
pages + entities
```

Optional:

```text
widgets
```

only after a large read-context block becomes reused.

## 4. Command Slice Mapping

A command slice is a real planning slice.

Example:

```text
SL-REVIEW-001 — Approve request and verify ApplicantParty
```

Client implementation usually maps to:

```text
pages/employee-request-review/
  EmployeeRequestReviewPage.tsx

features/approve-request/
  api/
    approveRequest.ts
  logic/
    useApproveRequest.ts
  types/
    approveRequestTypes.ts
  components/
    ApproveRequestAction.tsx
  tests/

entities/request/
  logic/
    useEmployeeRequestDetails.ts
    requestQueryKeys.ts
  components/
    EmployeeRequestDetailsCard.tsx
    RequestStatusBadge.tsx

entities/applicant-party/
  components/
    ApplicantSummaryCard.tsx
    ApplicantVerificationBadge.tsx
```

Command slice client implementation:

```text
pages + features + entities
```

Meaning:

```text
pages    = route / composition / read context
features = command behavior
entities = reusable read data and display dependencies
```

Do not say “command logic lives in pages”.

Say:

```text
The command slice client implementation uses a page as composition/context,
but command behavior lives in feature modules.
```

## 5. Entity Query Hook Rule

Entity-level React Query hooks are allowed when they remain generic read hooks.

Allowed in entity query hooks:

```text
- queryKey;
- queryFn;
- enabled guard;
- select option;
- placeholderData;
- staleTime;
- caller options merged with base options.
```

Not allowed:

```text
- feature-specific toast;
- feature-specific navigation;
- if Approved then open agreement proposal;
- after approve/reject invalidate queries;
- mutation success behavior;
- command-specific orchestration.
```

Recommended pattern:

```text
entities/request/api/getEmployeeRequestDetails.ts
entities/request/logic/requestQueryKeys.ts
entities/request/logic/useEmployeeRequestDetails.ts
```

The hook may accept caller options, but it must protect query identity:

```text
caller options may override ordinary query options;
caller must not replace queryKey or queryFn;
enabled should be combined with the base guard.
```

Example shape:

```ts
export function useEmployeeRequestDetails<TData = EmployeeRequestDetailsDto>(
  requestId: number | undefined,
  options?: Omit<
    UseQueryOptions<EmployeeRequestDetailsDto, ApiError, TData>,
    "queryKey" | "queryFn"
  >
) {
  const canRun = typeof requestId === "number" && Number.isFinite(requestId);

  return useQuery({
    staleTime: 30_000,
    ...options,

    queryKey: requestQueryKeys.employeeDetails(requestId ?? -1),
    queryFn: () => getEmployeeRequestDetails(requestId as number),

    enabled: canRun && (options?.enabled ?? true),
  });
}
```

## 6. Component Placement Rule

Ask what the component represents.

### Entity components

Use `entities/*/components` for small/medium reusable display components that show business data.

Examples:

```text
entities/request/components/RequestStatusBadge.tsx
entities/request/components/RequestSummaryCard.tsx
entities/request/components/RequestReviewDecisionSummary.tsx

entities/applicant-party/components/ApplicantSummaryCard.tsx
entities/applicant-party/components/ApplicantVerificationBadge.tsx
```

These components can participate in read slices and command slices.

They are not whole read contexts.

### Page-local components

Use `pages/.../components` for concrete screen/read-context components.

Examples:

```text
pages/employee-requests/components/EmployeeRequestsTable.tsx
pages/employee-requests/components/EmployeeRequestsFilters.tsx
pages/employee-requests/components/EmployeeRequestsEmptyState.tsx

pages/employee-request-details/components/EmployeeRequestDetailsHeader.tsx
pages/employee-request-details/components/EmployeeRequestDetailsActionsPanel.tsx
```

### Feature components

Use `features/*/components` for command/action UI.

Examples:

```text
features/approve-request/components/ApproveRequestAction.tsx
features/reject-request/components/RejectRequestForm.tsx
features/create-request/components/CreateRequestForm.tsx
```

### Shared components

Use `shared/ui` only for domain-agnostic primitives.

Examples:

```text
Button
Card
Input
Textarea
Modal
FormField
ErrorMessage
```

## 7. Filtering Rule

Filtering is part of a read slice when it only changes read query state.

Example:

```text
GET /employee/requests?status=InReview
```

Client implementation:

```text
pages/employee-requests/components/EmployeeRequestsFilters.tsx
entities/request/logic/useEmployeeRequests(filters, options)
```

Do not create a command feature for this.

Filtering becomes a command feature only if there is a user action that changes persistent state.

Example:

```text
save my default dashboard filter
```

Then it may become:

```text
features/save-request-dashboard-filter/
```

and possibly a separate planning slice.

## 8. Review Page Rule

Opening a review page is not a command feature if it is only navigation/read context.

Example:

```text
/employee/requests/:requestId/review
```

Client implementation:

```text
pages/employee-request-review/
entities/request/
entities/applicant-party/
features/approve-request/
features/reject-request/
```

Do not create:

```text
features/start-review/
```

unless entering review creates server-side state.

If entering review creates a lock/session/assignment:

```text
POST /employee/requests/:requestId/start-review
```

then it becomes command behavior and should be raised as a scenario/API/slice question.

## 9. Client Sidecar Architecture Mapping Section

Each `.client.md` file must include:

```text
## 4. Client Architecture Mapping
```

This section answers:

```text
- Is this slice read, command or mixed?
- Which client architecture layers implement it?
- Which behavior items are covered by page, entity, feature, widget or shared support?
- Which read context does a command feature depend on?
- Which components are page-local vs entity display vs feature action?
- Is any page-local block reusable enough to become a widget?
```

## 10. Client Architecture Questions

Client sidecar questions should include architecture questions relevant to that slice.

Question types:

```text
architecture
read-vs-command
feature-vs-page
entity-vs-feature
page-vs-widget
entity-query-hook
component-placement
client/API
cache
presentation
scenario-level
```

Examples:

```text
Question:
Is review screen a separate feature?

Assumption:
No, if opening review is only navigation/read context.

Promote to scenario register?
Only if entering review creates server-side lock/session/assignment.
```

```text
Question:
Is dashboard filtering a separate feature?

Assumption:
No, if filtering is read query state. Yes only if filter is persisted as user preference.

Promote to scenario register?
Only if persisted preference/API semantics are introduced.
```

```text
Question:
Should EmployeeRequestDetailsPanel stay page-local or become widget?

Assumption:
Stay page-local until reused by multiple pages.
```

## 11. Client Tests

Client tests should prove behavior coverage, not just component rendering.

Examples:

```text
read slice:
- page loads and displays request list/details;
- filters update query state;
- forbidden/not found/error states render correctly;
- entity display components render status/applicant summary correctly.

command slice:
- feature action is available only in allowed status;
- mutation sends expected DTO;
- FormValues map to DTO correctly;
- server errors map to field/global errors;
- success invalidates relevant entity queries;
- page context provides requestId/status to feature.
```

## 12. Current Project Rule

Do not reorganize existing working client code purely for architecture.

Apply this structure to new slice/client work first.

Migrate older code gradually only when touched by a real slice/client task.
