# Section Card Template

Status: draft  
Scope: reusable template for preparing each VKR subsection

Use this before drafting a subsection.

## Template

```text
VKR section:
[Example: 1.2 Анализ существующих решений]

Subsection purpose:
[What this subsection must prove/explain.]

Project question:
[The exact project-centered question.]

Why this question appears in this VKR:
[Connect to ООО «ЗСК», client requests, document flow, roles, architecture or implementation.]

Project materials:
- [planning artifact / scenario / slice / clean source / implementation evidence]
- [...]

External sources:
- [official product / standard / documentation / article]
- [...]

What is written from the project:
- [...]

What is taken from research:
- [...]

Author analysis:
- [...]

Visual/table support:
- [table / diagram / screenshot / code listing / none]

Draft form:
- [paragraphs / comparison table / scenario card / implementation description / conclusion]

Citation points:
- [where citations are needed]

TODO / INSERT:
- TODO SOURCE:
- TODO INSERT TABLE:
- TODO INSERT DIAGRAM:
- TODO INSERT SCREENSHOT:
- CHECK REPO:

Borrowing risk:
[low / medium / high]

How to reduce borrowing risk:
[write from project context, cite facts, avoid copying descriptions, use author comparison]
```

## Mini Example

```text
VKR section:
1.2 Анализ существующих решений

Project question:
Which existing solutions can solve parts of the ООО «ЗСК» scenario, and why is a custom web application justified for the VKR?

Why this question appears:
The project needs request intake, status handling, employee review, document draft preparation and notification. Existing systems may cover these functions, but often with broader enterprise scope.

Project materials:
- scenario: client -> applicant -> request -> employee -> decision -> document -> notification
- clean-requirements
- existing-solutions-analysis
- Deep Research comparison report

External sources:
- official pages for 1С:Документооборот, Directum RX, ELMA365, Bitrix24, Naumen Service Desk, Jira Service Management, Microsoft email/Excel documentation

Author analysis:
Ready-made systems are useful as analogues, but a custom VKR system is justified because it demonstrates design, implementation and testing of a focused scenario.

Visual/table support:
Comparison table.

Citation points:
Product capabilities and official positioning.

Borrowing risk:
Medium.

How to reduce:
Do not retell product pages. Compare each product only by criteria needed for the VKR scenario.
```
