# Literature Plan

Status: draft  
Scope: categories of sources and bibliography collection plan for the VKR

This file is not the final bibliography. It is a plan for collecting and verifying sources.

## 1. Target

Working target:

```text
at least 45 sources.
```

The final bibliography should combine:

```text
- official documentation;
- books / textbooks;
- scientific and educational sources;
- standards / methodical sources;
- sources on existing solutions;
- project-specific internal evidence, if allowed by the supervisor.
```

Do not invent sources. Every bibliography item must be verified.

## 2. Source Categories

### 2.1 Methodical And Institutional Sources

Purpose:

```text
support thesis structure, defense requirements and formatting decisions.
```

Candidate sources:

```text
1. PraktikiVkr_MetodUk_2025-11-08.doc
2. PraktikiVkr_MetodUk_2021-12-30.pdf
3. Department / university normal-control requirements, if provided.
4. Real published VKR examples from АлтГТУ 2025 as structural references.
```

### 2.2 Subject Area And Document Flow

Purpose:

```text
support relevance, document flow, request handling and automation problem.
```

Search directions:

```text
- electronic document flow;
- workflow automation;
- client request processing;
- information systems for organizations;
- BPM / business process modeling;
- document lifecycle management.
```

### 2.3 Requirements And Use Case Modeling

Purpose:

```text
support functional specification, use cases, scenarios, preconditions and postconditions.
```

Search directions:

```text
- requirements engineering;
- use case diagrams;
- UML basics;
- functional specification;
- software requirements.
```

### 2.4 Software Architecture

Purpose:

```text
support client-server architecture, layered backend and frontend/backend separation.
```

Search directions:

```text
- client-server architecture;
- layered architecture;
- web application architecture;
- API design;
- separation of concerns;
- full-stack web applications.
```

### 2.5 Backend Technologies

Purpose:

```text
support ASP.NET Core, C#, API, authentication/session, ProblemDetails.
```

Candidate sources:

```text
- official ASP.NET Core documentation;
- official Microsoft documentation on controllers/minimal APIs;
- official documentation on authentication and authorization;
- official documentation on ProblemDetails and error handling;
- C#/.NET documentation.
```

### 2.6 Frontend Technologies

Purpose:

```text
support React, TypeScript, SPA, routing, forms and UI architecture.
```

Candidate sources:

```text
- official React documentation;
- official TypeScript documentation;
- Vite documentation;
- React Router documentation, if used;
- documentation for query/form/testing libraries actually used in repo.
```

### 2.7 Database And Persistence

Purpose:

```text
support EF Core, SQL Server and relational data modeling.
```

Candidate sources:

```text
- official Entity Framework Core documentation;
- official SQL Server documentation;
- textbooks on databases;
- sources on relational modeling and normalization.
```

### 2.8 API Contract And Generated Artifacts

Purpose:

```text
support OpenAPI, generated TypeScript types, semantic constants and frontend/backend synchronization.
```

Candidate sources:

```text
- OpenAPI Specification official site;
- Swagger / Swashbuckle documentation, if used;
- openapi-typescript documentation, if used;
- Microsoft API documentation;
- sources on API contract testing.
```

### 2.9 Testing

Purpose:

```text
support domain tests, integration/API tests, client/component tests, E2E tests.
```

Candidate sources:

```text
- xUnit documentation;
- ASP.NET Core integration testing documentation;
- Playwright documentation, if used;
- Testing Library documentation, if used;
- software testing textbooks;
- testing_spo.pdf as educational source, if allowed.
```

### 2.10 Security And Access Control

Purpose:

```text
support authentication, authorization, access to own data and protection of unsafe requests.
```

Candidate sources:

```text
- OWASP materials;
- ASP.NET Core security documentation;
- sources on CSRF/antiforgery if implemented or discussed;
- sources on secure web application development.
```

### 2.11 Existing Solutions And Alternatives

Purpose:

```text
support comparison of manual processing, CRM/helpdesk, document-flow systems and custom web application.
```

Candidate source categories:

```text
- official product pages;
- product documentation;
- articles comparing ECM/CRM/helpdesk systems;
- official documentation of systems considered in comparison.
```

Do not cite marketing claims without checking.

## 3. Suggested Distribution

| Category | Approximate count |
|---|---:|
| Methodical / institutional | 3-5 |
| Subject area / document flow / business process | 6-8 |
| Requirements / UML / Use Case | 4-6 |
| Architecture / web applications | 5-7 |
| Backend / ASP.NET Core / .NET | 5-7 |
| Frontend / React / TypeScript | 5-7 |
| Database / EF Core / SQL Server | 4-6 |
| OpenAPI / API contract | 3-5 |
| Testing / quality / security | 6-8 |
| Existing solutions | 4-6 |

This gives enough room to reach 45+ sources without overloading one category.

## 4. Bibliography Collection Workflow

```text
1. Create a raw source list.
2. Verify that each source exists and is accessible.
3. Record title, author/organization, year, URL/publisher.
4. Mark what each source supports.
5. Remove weak or duplicate sources.
6. Format bibliography according to supervisor/normal-control requirements.
7. Check that all cited statements in the text have sources.
```

## 5. Source Tracking Table Template

| ID | Source | Type | Supports section | Checked? | Notes |
|---|---|---|---|---|---|
| SRC-METH-001 | PraktikiVkr_MetodUk_2025-11-08.doc | methodical | structure / volume / defense | yes | internal upload |
| SRC-WEB-001 | ASP.NET Core official documentation | official docs | backend | TODO | verify URL |
| SRC-WEB-002 | React official documentation | official docs | frontend | TODO | verify URL |
| SRC-TEST-001 | xUnit documentation | official docs | tests | TODO | verify URL |
| SRC-API-001 | OpenAPI Specification | official docs | API contract | TODO | verify URL |

## 6. Do Not Do

```text
- Do not create fake bibliography entries.
- Do not cite sources that were not opened or verified.
- Do not copy long text from examples.
- Do not cite student VKR examples as theoretical authority; use them only as structural references unless supervisor allows otherwise.
- Do not rely only on websites; include books, methodical materials and official documentation.
```
