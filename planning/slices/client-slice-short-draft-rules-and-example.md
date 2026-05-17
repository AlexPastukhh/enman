# Client Slice Short Draft Rules And Canonical Example

Status: current / canonical short-draft form with entity-feature API placement  
Scope: client sidecar short drafts, scenario flow vs implementation flow, behavior coverage discipline, cross-cutting considerations

## 1. Non-Negotiable Rule

Client slice drafters must follow existing examples and this canonical shape.

Do not improvise a new structure unless the user explicitly asks for a different format.

## 2. Client API Placement Rule

Current accepted placement:

```text
Read endpoint wrapper:
  entities/<entity>/api

Command/mutation endpoint wrapper:
  features/<business-action>/api

Shared transport/generated infrastructure:
  shared/api/fetchJson
  shared/api/ProblemDetails / ApiError
  shared/api/antiforgery helpers
  shared/api/generated/openapi-types.ts
```

Do not copy old examples where a business-specific shared API wrapper owns the endpoint.

Generated OpenAPI types remain shared infrastructure and may be imported from entity/feature API files.

## 3. Canonical Short Draft Shape

```text
# SLICE-ID.client — Title

Status:
Parent slice:
Slice type:
Architecture direction:
Backend/API contract evidence, when relevant:

## 1. Scope
## 2. Out of Scope
## 3. Related Slices / Owners
## 4. Visual UI / Scenario Flow
## 5. Visual Client Implementation Flow
## 6. Client API / Server Contract
## 7. Cross-Cutting Concerns / Considerations
## 8. Questions / Decisions
## 9. Extension / Change Points
## 10. Behavior Coverage
## 11. Client / Component / E2E Verification Plan
## 12. Implementation Checklist
## 13. Next Step
```

## 4. Canonical Read Implementation Flow

```text
[Route / Page Layer]
pages/employee/dashboard/EmployeeDashboardPage.tsx
        ↓
[Entity Query Layer]
entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
        ↓
[Entity API Layer]
entities/employee-request/api/listEmployeeDashboardRequests.ts
  owns GET endpoint wrapper and generated DTO aliases
        ↓
[Shared Transport / Generated Layer]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
  owns transport and generated structural types only
        ↓
[Entity Display UI Layer]
entities/employee-request/ui/*
```

## 5. Canonical Command Implementation Flow

```text
[Feature UI Layer]
features/employee-request/start-review/ui/StartReviewButton.tsx
        ↓
[Feature Model Layer]
features/employee-request/start-review/model/useStartRequestReviewMutation.ts
        ↓
[Feature API Layer]
features/employee-request/start-review/api/startRequestReview.ts
  owns POST endpoint wrapper and generated command result alias
        ↓
[Shared Transport / Generated Layer]
shared/api/fetchJson.ts
shared/api/antiforgery helpers
shared/api/generated/openapi-types.ts
```

## 6. Drafting Checklist

```text
[ ] I copied the canonical section structure.
[ ] Scenario Flow is from scenario/UI specs and only covers this slice.
[ ] I placed read endpoint wrappers in entities/*/api.
[ ] I placed command endpoint wrappers in features/*/api.
[ ] I kept shared/api to fetchJson, ProblemDetails, CSRF helpers and generated OpenAPI types.
[ ] I did not call implementation details behavior items.
[ ] I separated Behavior Coverage from Test/Verification Plan.
```
