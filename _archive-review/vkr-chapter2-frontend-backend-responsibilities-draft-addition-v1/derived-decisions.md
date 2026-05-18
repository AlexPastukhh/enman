# Derived decisions

## Topic placement

The uploaded VKR topic draft is added as:

`planning/thesis/vkr-topic-workbench/03-chapter-2-design/03-architecture/02-frontend-backend-responsibilities.topic.md`

## Topic identity

- Chapter block: `03-chapter-2-design`
- Section block: `03-architecture`
- Topic: `2.3.2 Разделение ответственности frontend и backend`
- Operation: addition/replacement-safe
- Main file content starts at `# Тема: Разделение ответственности frontend и backend`

## Important preserved decisions

- Frontend is not the source of truth for business rules.
- Backend validates access and executes state-changing scenarios.
- Application layer coordinates user scenarios.
- Domain layer contains business rules and invariants.
- Frontend validation is a user hint, not a system guarantee.
- Read API and command API have different responsibilities.
- Read DTO may contain action availability, but command validation remains on backend/domain.
- DTO is not the domain model.
- Errors should be structured where confirmed by repo/API contract.
- 401/403/404 should be distinguished carefully.
- Mock verification is helper information, not an automatic decision.
- Upload/download file flows go through backend and storage boundaries.
