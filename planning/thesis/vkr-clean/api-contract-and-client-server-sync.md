# API Contract And Client/Server Synchronization

Status: draft  
Scope: diploma-ready explanation of OpenAPI, generated constants and typed frontend/backend synchronization

## 1. Purpose

The application contains a separate backend and frontend. Because of this, both parts must agree on endpoint paths, HTTP methods, request DTOs, response DTOs, response status codes, error response shape, stable error codes and field names.

If this contract is maintained manually, the frontend can drift from the backend. The project reduces this risk through generated contract artifacts.

## 2. Two-Channel Contract Approach

```text
OpenAPI = structural API contract.
Generated constants JSON = semantic constants.
```

OpenAPI describes the shape of API requests and responses. Error codes and semantic field names are consumed as explicit generated constants.

## 3. OpenAPI Structural Contract

OpenAPI is used for:

```text
- endpoint paths;
- HTTP methods;
- route/query/body parameters;
- request DTO shape;
- response DTO shape;
- status codes;
- ProblemDetails response shape;
- operation identifiers where practical.
```

Typical artifact:

```text
Shared/openapi.json
```

Client TypeScript types are generated from this artifact:

```text
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

## 4. Generated Semantic Constants

Generated constants are used for:

```text
- client-facing error codes;
- ProblemDetails extension names;
- ServerError / ServerValidationError field names;
- temporary legacy route constants while migration is incomplete.
```

Typical artifacts:

```text
Shared/constants.json
Shared/errorcodes.json
```

## 5. Thin Client API Wrappers

The frontend direction is:

```text
generated TypeScript DTO/types
+
thin handwritten API wrapper functions
```

Diploma-safe wording:

```text
Клиентская часть использует сгенерированные TypeScript-типы API, но сами функции обращения к серверу остаются тонкими вручную написанными обертками. Это позволяет сохранить понятную структуру frontend-кода и одновременно уменьшить риск несовпадения DTO между клиентом и сервером.
```

## 6. ProblemDetails And Error Mapping

Important policies:

```text
- error codes are stable symbolic identifiers, not UI messages;
- server validation uses API DTO field names;
- client maps API field names to form field names when needed;
- client maps error codes to local UI messages/behavior.
```

## 7. Contract Change Workflow

When an API contract changes, review:

```text
- backend endpoint and DTO metadata;
- Shared/openapi.json;
- generated TypeScript API types;
- server constants / error codes when semantic constants change;
- Shared/constants.json;
- Shared/errorcodes.json;
- client API wrappers;
- error parser / form error mapper;
- integration tests and contract checks;
- slice/client documentation.
```

## 8. Relation To Testing

The contract is supported by:

```text
- OpenAPI generation/check;
- generated TypeScript type generation/check;
- generated constants check;
- tools tests for artifact generation;
- server integration/API tests;
- client typecheck and client/component tests.
```

## 9. Suggested Diagram

```text
ASP.NET Core endpoints / DTO metadata
        |
        v
Shared/openapi.json
        |
        v
generated TypeScript API types
        |
        v
React typed API wrappers
        |
        v
UI forms and pages
```

Parallel semantic constants flow:

```text
server error codes / constants
        |
        v
Shared/constants.json + Shared/errorcodes.json
        |
        v
client error parser / form error mapping
        |
        v
field-level and global UI messages
```
