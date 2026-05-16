# Source Provenance Protocol For VKR Text

Status: draft  
Scope: how to decide where VKR draft information comes from and how to present it

## 1. Source Categories

| Code | Source category | Examples | How to use in VKR |
|---|---|---|---|
| A | Project planning artifacts | scenarios, slice notes, design decisions, questions/answers | Main source for project-specific text |
| B | Implementation evidence | code, API, DB schema, tests, screenshots | Used to describe and prove implementation |
| C | External research | official documentation, standards, product pages, books, articles | Used for definitions, external facts and comparison |
| D | Author analysis | conclusions from comparing project needs and alternatives | Written as author reasoning |
| E | Visual evidence | diagrams, screenshots, tables, testing matrix | Used to make the text verifiable and readable |

## 2. How To Use Each Source Type

### A. Project planning artifacts

Use when describing:

```text
- why the system exists;
- roles and scenarios;
- request lifecycle;
- application architecture;
- API contract decisions;
- testing strategy;
- limitations and future work.
```

VKR wording:

```text
В разрабатываемой системе...
Для рассматриваемого сценария...
В рамках проектирования было принято решение...
```

Citation rule:

```text
No external citation is needed for original project decisions.
```

### B. Implementation evidence

Use when describing implemented features.

Examples:

```text
- backend endpoints;
- domain entities;
- database tables;
- frontend pages;
- generated API artifacts;
- tests.
```

VKR wording:

```text
В реализованной части приложения...
На серверной стороне...
В клиентской части...
Работоспособность проверяется...
```

Required checks:

```text
CHECK REPO: before final text, confirm file paths, current implementation status and screenshots.
```

### C. External research

Use only when needed for:

```text
- product facts;
- official technology facts;
- standards;
- terminology;
- comparison criteria;
- general engineering practice.
```

VKR wording:

```text
Согласно официальной документации...
В документации указывается...
В качестве внешнего аналога рассмотрено...
```

Citation rule:

```text
External facts require a source.
```

Do not use external research as ready-made prose.

### D. Author analysis

Use for conclusions such as:

```text
- why custom development is justified;
- why a client-server architecture is appropriate;
- why OpenAPI helps this project;
- why a feature is planned rather than implemented in the current cut.
```

VKR wording:

```text
По результатам анализа...
С учётом цели ВКР...
Для рассматриваемого проекта выбран...
```

Citation rule:

```text
Author conclusions do not need citations, but the facts they are based on may need citations.
```

### E. Visual evidence

Use for:

```text
- business process;
- use case diagram;
- request lifecycle;
- architecture;
- API contract flow;
- ERD;
- UI screenshots;
- testing matrix.
```

VKR captions:

```text
Рисунок X — ...
Источник: составлено автором.

Рисунок X — ...
Источник: скриншот разработанного приложения.

Таблица X — ...
Источник: составлено автором на основе анализа официальной документации решений.
```

## 3. Fragment Decision Table

Before writing a subsection, fill this mentally or explicitly:

| Field | Question |
|---|---|
| VKR section | Where will this fragment be placed? |
| Project question | What project-specific question does it answer? |
| Project sources | Which planning/code/UI/test artifacts support it? |
| External sources | Which external facts need citations? |
| Author conclusion | What decision or conclusion do we make? |
| Visual support | Table, diagram, screenshot, code listing or none? |
| Risk | Could this become generic or borrowed-looking? |
| Mitigation | How to make it project-specific? |

## 4. Anti-Generic Rule

If a paragraph could be copied into almost any VKR about a web application, it is not ready.

Make it answer one of these:

```text
- why this matters for ООО «ЗСК»;
- how it affects client requests;
- how it affects document handling;
- how it affects roles and access;
- how it affects API/frontend/backend design;
- how it appears in implementation;
- how it will be verified.
```
