# Client Architecture Principles For Slice Sidecars

Status: current client architecture planning principles  
Scope: frontend/client implementation mapping for `.client.md` sidecars

## 1. Purpose

This file explains how client-side implementation should be planned for vertical slices.

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

For component discovery, styling and accessibility, use:

```text
planning/slices/client-component-discovery-guide.md
planning/client/
```

For change/extension/pressure decisions, use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

## 2. Frontend Architecture Terms

### app

Owns router setup, providers, query client setup, auth/session provider, route guards, activation/role guards, global layouts and global config.

### pages

Route-level composition. A page may read route params, load read context through entity query hooks, show loading/error/not-found/forbidden states, compose entity display components, render command features and own page-local read-context components.

A page should not contain the core command logic itself.

### entities

Reusable client read/data/display modules for business objects.

Owns read API functions, query keys, generic read query hooks, read DTO/view types, status helpers and small/medium reusable display components for business data.

Does not own command mutations, feature-specific success/error orchestration, navigation caused by a command, or full screen/read-context layouts.

### features

User command/action behavior. Owns command API/mutation functions, command DTO/result types, form values tied to command behavior, mutation hooks, command-specific normalization/DTO mapping, action/form components and command client tests.

### widgets

Optional reusable large read-context/composition blocks. Do not introduce widgets by default.

### shared

Domain-agnostic primitives and utilities.

Does not own business-specific types/components such as RequestStatusBadge, ApplicantSummaryCard, ApproveRequestDto or RejectRequestForm.

## 3. Read Slice Mapping

Read slice client logic is usually:

```text
pages + entities
```

Optional widgets appear only after real reuse.

Filtering is part of a read slice when it only changes read query state.

Filtering becomes a command feature only if a user action changes persistent state.

## 4. Command Slice Mapping

Command slice client implementation usually maps to:

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

## 5. Entity Query Hook Rule

Entity-level React Query hooks are allowed when they remain generic read hooks.

Allowed: queryKey, queryFn, enabled guard, select, placeholderData, staleTime, caller options merged with base options.

Not allowed: feature-specific toast/navigation, mutation success behavior, command-specific orchestration, approve/reject invalidation inside entity read hook.

Caller options may override ordinary query options, but must not replace queryKey or queryFn.

`enabled` should be combined with the base guard.

## 6. Component Placement Rule

| Component kind | Preferred layer | Example |
|---|---|---|
| Route/screen composition | pages | EmployeeRequestReviewPage |
| Page-local list/table/filter | pages/.../components | EmployeeRequestsTable |
| Business data display | entities | RequestStatusBadge |
| Command action/form | features | ApproveRequestAction |
| Reusable large composition | widgets, only if reused | EmployeeRequestDetailsPanel |
| Domain-agnostic primitive | shared | Button |

## 7. Review Page Rule

Opening a review page is not a command feature if it is only navigation/read context.

Do not create `features/start-review/` unless entering review creates server-side state.

## 8. Extension Pressure In Client Architecture

Default architecture conventions can be adjusted when known extension pressure would otherwise create harmful coupling.

This does not mean creating abstractions for every future idea.

A `.client.md` should record future client extension point, current anti-coupling decision, whether current convention is followed or intentionally avoided, trade-off, and revisit condition.

Example:

```text
Approve request feature should not import or navigate to agreement proposal creation,
because agreement proposal creation is a separate future slice.
```

## 9. Client Sidecar Architecture Mapping Section

Each `.client.md` file must include Client Architecture Mapping and explain which client architecture part covers which behavior/UI item.

## 10. Client Architecture Questions

Question types include architecture, read-vs-command, feature-vs-page, entity-vs-feature, page-vs-widget, entity-query-hook, component-placement, client/API, cache, presentation, accessibility, styling, extension-pressure and scenario-level.

For each question, include assumption/preferred answer.
