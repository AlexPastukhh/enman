# Planning To VKR Extraction Map

Status: draft  
Scope: mapping from planning materials to diploma sections

This file explains how to use planning documents without copying internal planning language into the VKR.

## 1. Extraction Rule

| Planning wording | VKR wording |
|---|---|
| slice | функциональный / технический срез реализации |
| behavior items | проверяемые элементы поведения |
| sidecar | уточнение клиентской реализации |
| agent / prompt | omit |
| baseline | текущая реализационная основа / текущий срез |
| gate | контрольное условие / проверка перед реализацией |
| ADR note | архитектурное решение |

## 2. Useful Planning Sources

| Planning source | Use in VKR |
|---|---|
| `planning/slices/README.md` | Explain implementation by business and cross-cutting slices |
| `planning/slices/implementation-principles.md` | Explain scenario -> behavior -> implementation -> tests methodology |
| `planning/api/client-server-contract-principles.md` | Explain OpenAPI/constants split |
| `planning/slices/cross-cutting/CC-API-001...` | Explain OpenAPI artifact and generated TypeScript API types |
| `planning/slices/cross-cutting/CC-CONST-001...` | Explain generated constants and contract testing |
| `planning/testing/testing-principles.md` | Explain test responsibility matrix |
| `planning/slices/client-architecture-principles.md` | Explain frontend architecture mapping |
| `planning/slices/slice-extension-points-register.md` | Explain extension points and anti-coupling |
| `planning/slices/slice-implementation-notes-register.md` | Extract UI/error mapping/form behavior notes |
| `planning/adr/architecture-decision-notes.md` | Extract accepted architecture decisions |
| `planning/planning-workflow-current.md` | Check current repo-grounded implementation baseline |

## 3. Chapter 2 Extraction

Planning-derived chapter 2 themes:

```text
- behavior specification through scenarios;
- actors and Use Cases;
- invariants and pre/postconditions;
- client-server architecture;
- API contract design;
- domain model;
- extension points.
```

## 4. Chapter 3 Extraction

Planning-derived chapter 3 themes:

```text
- backend/API implementation;
- L1 endpoints;
- generated OpenAPI and constants artifacts;
- typed client API wrappers;
- form error mapping;
- EF Core persistence;
- testing by layers;
- contract checks.
```

## 5. Architecture Decisions Worth Mentioning

```text
1. Separate frontend and backend.
2. Use OpenAPI for structural API contract.
3. Use generated constants for semantic error/field constants.
4. Keep generated artifacts committed and checkable.
5. Use generated TypeScript API types, but keep thin handwritten API wrappers.
6. Separate approval from document/agreement creation.
7. Separate domain tests, API integration tests, client tests and E2E tests.
```

## 6. Extension Points Worth Mentioning

```text
- approved request can start agreement/document preparation;
- notifications can be triggered after review decision or document event;
- request documents can be added later using request context;
- external applicant verification can be added later;
- saved dashboard filters can be added later;
- review lock/session can be introduced later if business process needs it.
```

## 7. What Not To Put Into VKR

```text
- internal planning mechanics;
- full archive/replacement workflow;
- detailed agent rules;
- long status tables inside final prose;
- speculative patterns not connected to the project;
- claims about UI/API/email/documents without repo evidence.
```
