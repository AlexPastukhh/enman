# Section Draft Workflow

Status: working rule  
Scope: how to write VKR subsection drafts from project artifacts, implementation and research

## 1. Core idea

A VKR subsection should not start from general theory.

It should start from a project question:

```text
What does this subsection need to explain for the developed web application?
```

Then the draft uses external sources only when they help answer that question.

## 2. Subsection protocol

Each subsection draft should keep this block near the top until final editing:

| Field | Meaning |
|---|---|
| VKR place | Chapter and subsection where the text may be used |
| Project question | What the subsection must explain |
| Project sources | Which project/planning/repo artifacts define the local meaning |
| Research sources | Which external sources are needed and why |
| Author contribution | What conclusion or comparison is made by the author |
| Visual/material insertions | What table, figure, screenshot or diagram is needed |
| Citation policy | Where references are required |
| Finalization checks | What to verify before final diploma text |

## 3. Source types

| Type | Examples | How to use |
|---|---|---|
| Project notes | planning, clean-source files, scenario specs, section drafts | Main source for project-specific statements |
| Implementation | repo code, tests, UI screenshots, generated artifacts | Evidence for implemented behavior |
| Research | official docs, standards, articles, books | Support external facts, concepts and product descriptions |
| Author analysis | comparison, criteria, selection reasoning | Written as own conclusion |
| Visual artifacts | diagrams, screenshots, tables | Used to prove or clarify the text |

## 4. Citation rule

Use citations for:

```text
- external products;
- definitions from standards or official docs;
- technology claims;
- statistics;
- regulatory or normative statements;
- borrowed classifications or comparison criteria.
```

Do not cite external sources for:

```text
- description of the developed application;
- project-specific scenario flow;
- domain model created for the VKR;
- implementation facts confirmed by repo;
- screenshots of the developed application;
- author conclusions based on comparison.
```

## 5. Anti-template rule

Avoid standalone generic paragraphs.

Bad direction:

```text
Electronic document management systems are widely used in modern organizations...
```

Better direction:

```text
In the developed application, document handling appears after the request has been reviewed. Therefore, when comparing existing solutions, it is important to evaluate not only document storage, but also the link between a request, an employee decision and a generated document.
```

## 6. Draft markers

Use these markers while drafting:

```text
TODO SOURCE: add or verify an external source
TODO REPO: check implementation or exact status in repo
TODO INSERT: insert table, screenshot, diagram or listing
TODO STYLE: rewrite before final diploma text
TODO DECIDE: unresolved choice
```

Remove or resolve markers before final submission.
