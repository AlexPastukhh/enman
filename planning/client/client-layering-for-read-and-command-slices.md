# Client Layering For Read And Command Slices

Status: current / entity-feature API ownership synchronized  
Scope: placement of pages, entities, features, shared transport/generated infrastructure, API functions and client sidecar drafting

## 1. Purpose

Current accepted direction:

```text
shared/
  only genuinely shared infrastructure.

entities/*/
  read-side business/entity API, read hooks, read models and display UI.

features/*/
  command/user-action API, mutation hooks, forms/actions and command feedback.
```

This replaces the earlier direction where all business-specific endpoint wrappers lived under `shared/api`.

## 2. High-Level Mapping

Read slice:

```text
pages + entities + shared transport/generated infrastructure
```

Command/user-action slice:

```text
pages + features + entities + shared transport/generated infrastructure
```

Generated contracts:

```text
shared/api/generated/openapi-types.ts
```

Transport primitives:

```text
shared/api/fetchJson.ts
shared/api/ApiError / ProblemDetails helpers
shared/api/antiforgery helpers
generic URL/query helpers
```

## 3. shared

`shared/api` owns:

```text
- fetchJson;
- credentials/session transport behavior;
- ProblemDetails parsing;
- ApiError / AntiforgeryApiError;
- CSRF/antiforgery token attach/refresh helpers;
- generated OpenAPI types;
- generic request/response helpers;
- generic URL/query builder helpers if not business-specific.
```

`shared/api` does not own:

```text
- ApplicantParty read endpoint wrapper;
- EmployeeRequest details endpoint wrapper;
- StartReview command endpoint wrapper;
- business-specific DTO aliases;
- business-specific endpoint path constants;
- query keys;
- mutation hooks;
- command success/error behavior.
```

## 4. entities

Entities own reusable read/data/display modules for a business object.

`entities/<entity>/api` owns read-side API functions:

```text
- read endpoint path for that entity read operation;
- generated DTO aliases for the read operation;
- read DTO -> read model mapping when needed;
- fetchJson call for GET/read endpoint;
- business read operation name.
```

Examples:

```text
entities/applicant-party/api/listAccountApplicantParties.ts
entities/employee-request/api/listEmployeeDashboardRequests.ts
entities/employee-request/api/getEmployeeRequestDetails.ts
```

## 5. features

Features own user command/action behavior.

`features/<action>/api` owns command endpoint functions:

```text
- mutation endpoint path for that command;
- generated command DTO/result aliases;
- request body construction for the command;
- fetchJson/unsafe request call;
- no React Query mutation state.
```

Examples:

```text
features/applicant-party/create-individual/api/createIndividualApplicantParty.ts
features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts
features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/approve-review/api/approveRequestReview.ts
features/employee-request/reject-review/api/rejectRequestReview.ts
```

## 6. Generated Types Rule

Generated OpenAPI types remain shared infrastructure:

```text
shared/api/generated/openapi-types.ts
```

Entities and features may import generated types directly.

Good:

```ts
import type { components } from "../../../shared/api/generated/openapi-types";
import { fetchJson } from "../../../shared/api/fetchJson";

export type EmployeeRequestDetailsDto =
  components["schemas"]["EmployeeRequestDetailsDto"];

export const getEmployeeRequestDetails = (requestId: number) =>
  fetchJson<EmployeeRequestDetailsDto>(`/api/employee/requests/${requestId}`, {
    method: "GET",
  });
```

## 7. Transitional Compatibility Rule

Some existing runtime/docs may still use this old shape:

```text
shared/api/<businessArea>Api.ts
  business-specific endpoint wrapper
        ↓
entities/*/api or features/*/api
  semantic wrapper
```

Do not mass-migrate without a concrete implementation slice.

For new drafts and new code:

```text
entities/*/api owns read endpoint wrappers.
features/*/api owns command endpoint wrappers.
shared/api owns only transport/generated infrastructure.
```

## 8. Good Read Layering Example

```text
[Route / Page Layer]
pages/account/AccountPage.tsx
  owns session branch and page composition
  uses useAccountApplicantPartiesQuery()

[Entity Query Layer]
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
  owns React Query read hook and query key
  uses listAccountApplicantParties()

[Entity API Layer]
entities/applicant-party/api/listAccountApplicantParties.ts
  owns GET /api/l1/applicant-parties endpoint wrapper
  owns generated response alias for this read operation
  uses fetchJson() and generated OpenAPI types from shared infrastructure

[Shared Transport / Generated Layer]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
  owns transport behavior and generated structural types only

[Entity Display UI Layer]
entities/applicant-party/ui/ApplicantPartiesList.tsx
  owns read-only cards, empty state and current/default grouping UI
```

## 9. Good Command Layering Example

```text
[Route / Page Layer]
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
  owns page composition and action slot placement

[Entity Display UI Layer]
entities/employee-request/ui/EmployeeRequestDetailsView.tsx
  owns read-only details and action availability display

[Feature UI Layer]
features/employee-request/start-review/ui/StartReviewButton.tsx
  owns visible Start Review action and pending/error feedback

[Feature Model Layer]
features/employee-request/start-review/model/useStartRequestReviewMutation.ts
  owns mutation hook and details/dashboard refresh after success

[Feature API Layer]
features/employee-request/start-review/api/startRequestReview.ts
  owns POST /api/employee/requests/{requestId}/review/start wrapper
  uses CSRF-aware shared transport and generated OpenAPI types

[Shared Transport / Generated Layer]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
  owns transport/security/generated infrastructure only
```

## 10. Drafting Rule

Client sidecar drafts must explicitly say:

```text
Lives here:
Uses:
Owns:
Does not own:
```

and must apply the placement decision:

```text
read endpoint wrapper -> entities/*/api
command endpoint wrapper -> features/*/api
shared -> fetchJson / ProblemDetails / CSRF / generated types only
```
