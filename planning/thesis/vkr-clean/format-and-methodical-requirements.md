# Format And Methodical Requirements

Status: draft  
Scope: methodical requirements, observed VKR structure patterns, and application to the ООО «ЗСК» thesis

This file is a working support file. It is not final diploma text.

## 1. Purpose

This file collects methodical and formatting requirements that affect the structure of the VKR and defense materials.

Use it when preparing:

```text
planning/thesis/vkr-clean/vkr-outline.md
planning/thesis/vkr-clean/chapter-*.md
planning/thesis/presentation/slide-outline.md
planning/thesis/presentation/speech-draft.md
planning/thesis/presentation/demo-script.md
```

Do not copy this file into the diploma as a chapter. It is a checklist and a source of structure.

## 2. Main Methodical Sources

Current working sources:

```text
PraktikiVkr_MetodUk_2025-11-08.doc
PraktikiVkr_MetodUk_2021-12-30.pdf
Real VKR examples from АлтГТУ 2025:
- Lifke I.V. — web application for interaction with technical support
- Zykov V.E. — web application for strength training tracking
- Shtabkin S.V. — web resource for digital media artists
```

The 2025 methodical document is preferred when it is available. The 2021 methodical PDF is used as a supporting source because its structure and wording largely match the later material.

## 3. Methodical Requirements To Track

### VKR volume

Working target:

```text
about 120 pages of main explanatory note text,
including tables, diagrams and graphs.
```

Applications are not included into this main page count.

### Introduction

The methodical target for introduction is:

```text
5-7 pages.
```

The introduction should contain:

```text
- VKR topic;
- information about the profile organization;
- relevance;
- practical significance;
- achieved results;
- approbation / testing;
- deployment / implementation notes, if available;
- short content by sections.
```

For this VKR, the organization is ООО «ЗСК», used as a conditional/example network company.

### Main sections

The methodical structure uses four large parts:

```text
1. Pre-project investigation, problem discovery, goals and tasks.
2. Project/design concept and selected development means.
3. System project, implementation, database, code description, UI and testing.
4. Lifecycle, operation, modification reserve and further development.
```

Real 2025 web-oriented VKR examples often use a more practical three-section form:

```text
1. Subject area / existing solutions analysis.
2. Design and technology selection.
3. Implementation and testing.
```

Working decision for this project:

```text
Keep the four-section methodical logic visible,
but allow the final thesis text to follow a practical three-section structure
if the supervisor accepts it.
```

If a three-section structure is used, lifecycle/deployment/development material should be included in the end of section 3, conclusion and appendices.

## 4. Required Content For This VKR

The VKR must eventually cover:

```text
- subject area: client requests and document flow in a network company;
- problem: manual/disconnected request handling and document tracking;
- existing solutions / alternatives;
- goal and tasks;
- functional specification of scenarios;
- use-case diagrams;
- client-server architecture;
- API contract between backend and frontend;
- domain model;
- database model;
- user interface;
- backend implementation;
- frontend implementation;
- testing;
- results;
- limitations;
- future development.
```

## 5. Defense Materials

For defense / pre-defense prepare:

```text
- report/speech for about 6 minutes;
- illustrative material: around 20 slides;
- speech text file;
- demo plan;
- visual materials: diagrams, screenshots, tables;
- supporting documents requested by the department.
```

The presentation should not be a copy of the thesis text. It should show the project logic:

```text
topic -> problem -> goal/tasks -> scenarios -> architecture -> data/model -> UI -> implementation -> tests -> results.
```

## 6. Code Placement

In the main VKR text:

```text
- describe modules, classes, methods and relationships;
- include only short code fragments when they explain a key design or implementation decision;
- explain every code fragment in text.
```

In appendices:

```text
- place long listings;
- place auxiliary code fragments;
- place generated or bulky technical artifacts only when they support the thesis.
```

Do not fill the main text with large code blocks.

## 7. Diagrams And Tables

Useful diagrams for this VKR:

```text
- business process of client request handling;
- Use Case diagram;
- architecture diagram;
- API contract / frontend-backend synchronization diagram;
- domain model;
- ERD / database structure;
- request lifecycle;
- document/agreement lifecycle;
- sequence diagram for request creation;
- sequence diagram for request review;
- testing responsibility matrix.
```

Large diagrams may be placed in appendices. In the main text, use readable fragments and provide explanations.

## 8. Bibliography

Working target:

```text
at least 45 sources.
```

Source categories:

```text
- official documentation for ASP.NET Core, EF Core, React, TypeScript, SQL Server, OpenAPI;
- sources on information systems and document flow;
- sources on requirements, Use Case, testing, software architecture;
- sources on web application security and API error handling;
- sources on existing systems / alternatives;
- internal methodical materials and VKR examples as structural references.
```

Do not let AI invent bibliography entries. Each source must be checked.

## 9. Normal Control Items Still To Confirm

The currently available methodical materials define structure and volume, but final formatting must still be confirmed with the supervisor or normal-control template:

```text
- font;
- font size;
- line spacing;
- margins;
- paragraph indent;
- title formatting;
- figure/table caption formatting;
- bibliography formatting;
- appendix formatting.
```

Until confirmed, use a standard Russian VKR layout:

```text
A4;
Times New Roman 14;
1.5 line spacing;
justified text;
paragraph indent 1.25 cm;
left margin 30 mm;
right margin 10-15 mm;
top/bottom margin 20 mm.
```

This is a working assumption, not a confirmed department rule.

## 10. Project-Specific Application

For the ООО «ЗСК» VKR, the methodical structure maps as follows:

| Methodical item | Project-specific content |
|---|---|
| Profile organization | ООО «ЗСК» as conditional network company |
| Problem process | client request submission, review, decision, document preparation |
| Automation goal | web application for request handling and document flow support |
| Design concept | ASP.NET Core backend + React/TypeScript frontend |
| Data storage | SQL Server through EF Core |
| API contract | OpenAPI + generated constants for backend/frontend synchronization |
| UI | registration, login, account/applicant page, request creation/read flows, future employee workspace |
| Testing | domain unit tests, server/API integration tests, future client/component and E2E tests |
| Future development | employee workspace completion, documents, notifications, analytics, integrations |

## 11. Do Not Include In Final Text

Do not include in the final thesis:

```text
- references to ChatGPT or AI-assisted writing;
- prompt or archive generation workflow;
- internal planning wording such as "agent", "sidecar", "gate" without translation;
- raw planning IDs unless used in an appendix or traceability table;
- statements that a feature is implemented without repo evidence.
```

Use internal planning only as source material and translate it into ordinary engineering language.
