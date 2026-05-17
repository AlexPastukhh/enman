# Writing Protocol For VKR Drafts

Status: draft  
Scope: controlled conversion of project artifacts, research materials and implementation evidence into VKR draft text

## 1. Purpose

This folder defines a working protocol for writing VKR sections without turning the text into a generic literature review or a copied research report.

The goal is to make each VKR fragment answer a concrete project question:

```text
What does this subsection need to prove or explain for the web application being developed?
```

The text should be based primarily on project artifacts:

```text
- scenario specifications;
- planning notes;
- design decisions;
- implementation evidence;
- UI screenshots;
- diagrams;
- tests;
- author analysis.
```

External research is used carefully:

```text
- to support terminology;
- to cite official product/technology facts;
- to justify comparison criteria;
- to provide references for general engineering practices;
- not as ready-made paragraph text.
```

## 2. Core Rule

Do not start from a general topic.

Start from the project question.

Bad:

```text
Describe document management systems.
```

Better:

```text
Explain which document-management and request-processing capabilities are relevant for the system being developed for ООО «ЗСК», and why a custom web application is justified for this VKR.
```

## 3. Folder Contents

```text
source-provenance-protocol.md
section-card-template.md
research-usage-rules.md
chapter-section-question-map.md
page-fragment-checklist.md
pilot-section-existing-solutions.md
```

Use these files before expanding `chapter-*.md` drafts.

## 4. Recommended Writing Loop

```text
1. Select a VKR subsection.
2. Define the project question for that subsection.
3. List project artifacts that answer it.
4. List external sources only where a citation is needed.
5. Decide whether the fragment needs text, table, screenshot, diagram or code listing.
6. Draft text from the project context.
7. Add external references only for external facts.
8. Mark TODO/INSERT/CHECK REPO where the text depends on implementation status.
9. Check that the paragraph still explains the ООО «ЗСК» project, not an abstract topic.
```

## 5. Output Style

The final VKR text should be:

```text
- project-centered;
- specific to the developed application;
- supported by repo/planning artifacts;
- supported by external sources only where needed;
- not written as a generic compilation of Internet materials.
```
