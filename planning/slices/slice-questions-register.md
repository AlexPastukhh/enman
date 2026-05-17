# Slice Questions Register

Status: active / client API placement synchronized

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `CL-API-PLACEMENT-Q-001` | `planning/client/client-api-placement-decision.md` | client architecture | accepted | Should business endpoint wrappers live in `shared/api`? | No for new drafts. `shared/api` owns transport/generated infrastructure only. | New read/command wrappers move to entities/features. |
| `CL-API-PLACEMENT-Q-002` | same | generated types | accepted | Can entities/features import generated OpenAPI types directly? | Yes. Generated types are shared infrastructure; business aliases live in owning entity/feature API files. | Avoids business-aware shared wrappers. |
| `CL-API-PLACEMENT-Q-003` | same | migration | accepted | Should existing shared business wrappers be mass-migrated now? | No. Treat them as transitional compatibility and migrate only in concrete slice/cleanup scope. | Avoids broad client churn. |
| `CL-API-PLACEMENT-Q-004` | same | read placement | accepted | Where do read endpoint wrappers live? | `entities/<entity>/api`. | Employee dashboard/details and ApplicantParty reads. |
| `CL-API-PLACEMENT-Q-005` | same | command placement | accepted | Where do command/mutation endpoint wrappers live? | `features/<business-action>/api`. | StartReview, make-current-default, create actions. |
| `CL-LAYER-Q-001` | old docs | shared API | superseded | `shared/api` is the low-level client/server boundary grouped by layer. | Superseded for business-specific wrappers; shared remains transport/generated boundary only. | Do not copy old shared wrapper shape into new drafts. |
