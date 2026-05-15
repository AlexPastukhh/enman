# API Client Constants Generation

Status: pointer / API relationship note  
Scope: API relationship to generated client constants

## 1. Primary Source

The implemented-baseline cross-cutting slice is:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Use that file for:

```text
- Tools command;
- writer/checker/generator classes;
- test plan;
- generated artifact checks;
- literal contract tests;
- consumer rules for business slices;
- current implementation status and future review items.
```

## 2. API Contract Role

This API file only states how constants relate to API contract.

Core split:

```text
OpenAPI = structural API contract:
  endpoints, methods, DTOs, response schemas, status codes.

Generated Shared/*.json = semantic constants:
  client-facing error codes, ProblemDetails extension names,
  ServerError field names, temporary route/field constants if needed.
```

## 3. Commands

Normal generation:

```bash
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared
```

Check mode:

```bash
dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check
```

`--check` does not write files. It compares generated output with committed `Shared/*.json`.

Current artifacts:

```text
Shared/constants.json
Shared/errorcodes.json
```

## 4. Temporary Route Constants

Route constants in `Shared/constants.json` are temporary/legacy while OpenAPI migration is incomplete.

Current direction:

```text
- keep existing legacy route constants while current client depends on them;
- do not add new route constants when OpenAPI/generated API contract can cover them;
- move route/path contract toward OpenAPI over time.
```

## 5. Consumer Rule

If a parent slice introduces client-facing error codes, it must reference `CC-CONST-001` and classify each code as:

```text
ordinary validation
important domain
critical behavioral
```

Then:

```text
1. Add/update source C# constants.
2. Regenerate Shared/constants.json and Shared/errorcodes.json.
3. Run generate-client-constants --check.
4. Add/update API/client tests according to code stability.
5. Map ErrorCode -> UI behavior in `.client.md` if concrete client work exists.
```
