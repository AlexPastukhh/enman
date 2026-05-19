# Derived decisions

- Do not update domain.
- Do not change migrations or EF mappings.
- Keep fix inside E2E seed tooling.
- Add preflight schema compatibility before the seed SQL batch so SQL Server does not fail parse-time validation on local/legacy test schemas.
