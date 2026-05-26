# Client Implementation Principles

Status: current client implementation principles  
Scope: client layering, API ownership, generated contract usage and read/command ownership for client slice drafts

## 1. Purpose

Use this file when writing or reviewing client slice drafts and client implementation plans.

This file does not define general slice-draft authoring rules. For scope, scenario boundary, behavior coverage, implementation drift and test-trace principles, use:

```text
planning/slices/slice-draft-authoring-principles.md
```

## 2. Core Direction

Accepted principle:

```text
shared/
  owns only genuinely shared infrastructure.

entities/*/
  owns read-side business/entity API wrappers, read hooks, read models and display UI.

features/*/
  owns command/user-action API wrappers, mutation hooks, forms/actions and command feedback.

widgets/*/
  owns larger reusable composed UI blocks used by multiple pages.
```

This supersedes earlier directions where `shared/api/*Api.ts` owned all low-level business endpoint wrappers and entities/features delegated to them.

## 3. Read vs Command Mapping

Read slice:

```text
pages + entities + widgets when needed + shared transport/generated infrastructure
```

Command/user-action slice:

```text
pages + features + entities + widgets when hosted in read UI + shared transport/generated infrastructure
```

## 4. Shared Ownership

`shared/api` owns:

```text
fetchJson;
credentials/session transport behavior;
ProblemDetails parsing;
ApiError / AntiforgeryApiError;
CSRF/antiforgery token attach/refresh helpers;
generated OpenAPI types;
generic request/response helpers;
generic URL/query builder helpers if not business-specific.
```

`shared/api` does not own:

```text
ApplicantParty endpoint wrapper;
EmployeeRequest endpoint wrapper;
Request command wrapper;
AgreementExchange endpoint wrapper;
business-specific DTO aliases;
business-specific endpoint path constants;
entity-specific mapping;
query keys;
mutation hooks;
command success/error behavior.
```

## 5. Entities Ownership

Entities own reusable read/data/display modules for a business object.

`entities/<entity>/api` owns:

```text
read endpoint wrappers;
read endpoint paths for that entity read operation;
generated DTO aliases for the read operation;
read DTO -> view model mapping when needed;
fetchJson call for GET/read endpoint;
business read operation name.
```

`entities/<entity>/model` owns:

```text
React Query query keys;
React Query read hooks;
read model helpers;
read state mapping when needed.
```

`entities/<entity>/ui` owns:

```text
read-only business display components;
business status display;
entity cards/lists when they are not page-specific widgets.
```

## 6. Features Ownership

Features own user command/action behavior.

`features/<action>/api` owns:

```text
mutation endpoint path for that command;
generated command DTO/result aliases;
request body construction for the command;
fetchJson/unsafe request call;
no React Query mutation state.
```

`features/<action>/model` owns:

```text
mutation hook;
submit orchestration;
query invalidation/refetch after success/error;
command error propagation.
```

`features/<action>/ui` owns:

```text
action button;
command form;
pending/disabled/error/success feedback;
action-level validation UI.
```

## 7. Widgets Ownership

Widgets own larger reusable composed UI blocks.

Use widgets when:

```text
the same composed block appears in multiple pages;
the block combines several entity display parts;
the block exposes action slots for feature sidecars;
the block is not owned by one specific route.
```

Widgets must receive route/action/navigation behavior through props. Widgets should not own route-specific navigation decisions.

## 8. Generated Types Rule

Generated OpenAPI types remain shared infrastructure:

```text
shared/api/generated/openapi-types.ts
```

Entities and features may import generated types directly and create local aliases near the owner.

Do not manually edit generated artifacts.

## 9. Transitional Compatibility Rule

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
