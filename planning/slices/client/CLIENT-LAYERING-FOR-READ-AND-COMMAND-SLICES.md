# Client Layering For Read And Command Slices

Status: current / entity-feature API ownership synchronized  
Scope: placement of pages, entities, features, widgets, shared transport/generated infrastructure, API functions and client sidecar drafting

## 1. Purpose

Current accepted direction:

```text
shared/
  only genuinely shared infrastructure.

entities/*/
  read-side business/entity API, read hooks, read models and display UI.

features/*/
  command/user-action API, mutation hooks, forms/actions and command feedback.

widgets/*/
  larger reusable composed UI blocks used by multiple pages.
```

## 2. High-Level Mapping

Read slice:

```text
pages + entities + widgets when needed + shared transport/generated infrastructure
```

Command/user-action slice:

```text
pages + features + entities + widgets when hosted in read UI + shared transport/generated infrastructure
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
- AgreementExchange details endpoint wrapper;
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
- read DTO -> view model mapping when needed;
- fetchJson call for GET/read endpoint;
- business read operation name.
```

`entities/<entity>/model` owns:

```text
- React Query query keys;
- React Query read hooks;
- read model helpers;
- read state mapping when needed.
```

`entities/<entity>/ui` owns:

```text
- read-only business display components;
- business status display;
- entity cards/lists when they are not page-specific widgets.
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

`features/<action>/model` owns:

```text
- mutation hook;
- submit orchestration;
- query invalidation/refetch after success/error;
- command error propagation.
```

`features/<action>/ui` owns:

```text
- action button;
- command form;
- pending/disabled/error/success feedback;
- action-level validation UI.
```

## 6. widgets

Widgets own larger reusable composed UI blocks.

Use widgets when:

```text
- the same composed block appears in multiple pages;
- the block combines several entity display parts;
- the block exposes action slots for feature sidecars;
- the block is not owned by one specific route.
```

Example:

```text
widgets/agreement-exchange-list/AgreementExchangeList.tsx
widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
```

Widgets must receive route/action/navigation behavior through props. Widgets should not own route-specific navigation decisions.

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
