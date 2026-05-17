# Slice Implementation Notes Register

Status: active / client API placement synchronized

## Client API Placement Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-CLIENT-API-PLACEMENT-001` | new client reads | Put read endpoint wrappers, path constants and generated DTO aliases in `entities/<entity>/api`. Use shared `fetchJson` and generated OpenAPI types. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-002` | new client commands | Put command/mutation endpoint wrappers, path constants and generated command DTO/result aliases in `features/<business-action>/api`. Use shared unsafe/CSRF-aware transport. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-003` | shared/api | Keep only transport/generated infrastructure in `shared/api`: `fetchJson`, ProblemDetails/ApiError, CSRF helpers, generated OpenAPI types and generic helpers. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-004` | existing runtime | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility. Do not mass-migrate without concrete slice scope. | future cleanup |
| `IMPL-CLIENT-API-PLACEMENT-005` | generated types | Entities/features may import generated OpenAPI types and define local business aliases. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-006` | old sidecars | If an older sidecar says “Shared API wrapper owns low-level HTTP call”, interpret it through the new rule: entity/feature API owns business wrapper; shared owns transport/generated infrastructure only. | compatibility note |
