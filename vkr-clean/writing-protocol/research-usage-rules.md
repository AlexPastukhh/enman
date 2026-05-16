# Research Usage Rules

Status: draft  
Scope: how to use Deep Research and external sources in VKR text without turning the work into a compilation

## 1. Main Rule

Research is not the main author of the VKR text.

Research provides:

```text
- external facts;
- official source links;
- comparison criteria;
- terminology support;
- examples of existing solutions;
- bibliography candidates.
```

The VKR text is written from:

```text
- the project goal;
- ООО «ЗСК» scenario;
- planning artifacts;
- implementation evidence;
- author analysis.
```

## 2. Safe Use Cases

Use research for:

```text
- brief product descriptions in the analogues section;
- official technology definitions;
- explanation of OpenAPI / ASP.NET Core / React / EF Core / SQL Server;
- comparison table criteria;
- security/testing terminology;
- bibliography.
```

## 3. Risky Use Cases

Use carefully:

```text
- generic “relevance” paragraphs;
- long theory paragraphs;
- advantages/disadvantages copied from product pages;
- textbook-like descriptions of web applications;
- broad claims about digital transformation;
- descriptions of document management not tied to ООО «ЗСК».
```

## 4. Rule For External Product Descriptions

Do not write:

```text
Directum RX is a powerful platform that provides extensive tools for document management and automation...
```

Write:

```text
Directum RX was considered as an analogue because it supports enterprise document management and process automation. For the current VKR, these capabilities are relevant only as comparison criteria, since the developed application focuses on a narrower scenario: client request intake, employee review and document draft preparation.
```

## 5. Rule For Technology Descriptions

Do not write:

```text
ASP.NET Core is a modern cross-platform framework for building high-performance web applications.
```

Write:

```text
The backend part of the application is implemented with ASP.NET Core because the project requires HTTP API endpoints, authentication/session support, validation, OpenAPI metadata and integration with the persistence layer. The general framework capabilities are confirmed by official documentation, while the specific use in this VKR is defined by the implemented request-processing scenario.
```

## 6. Citation Rule

Cite external sources when the text says:

```text
- what a product supports;
- what a framework is intended for;
- what a standard defines;
- what a guideline recommends;
- what a research/article/book states.
```

Do not cite external sources for:

```text
- our entity model;
- our UI decisions;
- our specific request lifecycle;
- our testing plan;
- our conclusions from comparing options.
```

## 7. Borrowing Risk Reduction

To reduce borrowing risk:

```text
1. Do not copy source wording.
2. Do not translate long fragments from foreign sources.
3. Do not follow a single source structure.
4. Write from the project question first.
5. Use external facts as short support, not as paragraph skeleton.
6. Add author comparison and project-specific conclusions.
7. Prefer tables composed by the author over long product retellings.
8. Use diagrams/screenshots generated from the project.
```

## 8. Suggested Text Pattern

```text
For the current project, the important requirement is [project need].
Existing solutions show that this requirement can be addressed through [general class of mechanisms].
However, the VKR system has a narrower scope: [specific project scenario].
Therefore, the selected approach is [project decision].
```
