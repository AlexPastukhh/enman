# VKR Section Drafting Workflow

Status: current workflow v2  
Scope: how to create short and full subsection drafts for the VKR explanatory note

## 1. Purpose

This workflow defines how to write VKR subsections without turning the text into:

```text
- generic theory;
- copied research summary;
- implementation status tracker;
- unstructured prose;
- disconnected fragments.
```

The goal is to write each subsection as a project-centered answer to a concrete question.

## 2. Main Rule

Each subsection must answer a project question.

Not:

```text
"Describe document management."
```

But:

```text
"Why does the ООО «ЗСК» web application need a controlled request-to-document process?"
```

Not:

```text
"Describe REST API."
```

But:

```text
"Why does this React + ASP.NET Core system need an explicit API contract between frontend and backend?"
```

## 3. Draft Types

### 3.1 Short Draft

The short draft is the default first output.

It is written in the chat first because it must be easy to read, challenge and reorder.

It is a narrative map, not a mini-final text.

It contains:

```text
1. Subsection place
2. Main project question
3. Why this subsection exists
4. High-level narrative flow
5. Project-specific materials to use
6. External sources to use, only if needed
7. Visual/table/screenshot inserts
8. Required thesis points
9. Open questions
10. Work needed before full draft
11. Risks and anti-patterns
12. Expected result of full draft
```

### 3.2 Full Draft

The full draft is a connected text attempt.

It is allowed to be incomplete, but it must already read like a VKR subsection.

It is not final until source/repo/visual checks are done.

Full draft may include:

```text
TODO SOURCE
TODO CHECK REPO
TODO INSERT TABLE
TODO INSERT FIGURE
TODO INSERT SCREENSHOT
TODO REWRITE
TODO DECIDE
```

## 4. Workflow Loop

```text
short draft in chat
  -> discussion / correction
  -> full draft attempt v1
  -> review against checklist
  -> harvest good fragments
  -> update fragment bank
  -> revise short draft if narrative changed
  -> full draft attempt v2
  -> source/repo/visual checks
  -> chapter-ready version
  -> merge into chapter
```

## 5. Short Draft Rules

Short draft should be concise but information-dense.

It must show:

```text
- what the subsection is trying to prove;
- how it moves the VKR forward;
- where project material enters the text;
- where research may support the text;
- what visual artifacts are expected;
- what questions can change the final text.
```

Short draft should not:

```text
- contain final paragraphs;
- quote external sources;
- become a research summary;
- become a code status report;
- hide unresolved decisions.
```

## 6. Full Draft Rules

Full draft should:

```text
- follow the high-level flow from the short draft;
- stay tied to the ООО «ЗСК» project;
- introduce external concepts only when they answer the subsection question;
- include TODO markers where evidence, source or visual material is missing;
- keep strong paragraphs even if the draft later changes structure.
```

Full draft should not:

```text
- copy research paragraphs;
- overclaim current implementation;
- describe technologies without project relevance;
- replace diagrams/screenshots with long prose;
- remove TODO markers before checks are complete.
```

## 7. Source Handling

Use three source types differently.

### 7.1 Project materials

Examples:

```text
planning notes
scenario specifications
domain model notes
slice notes
clean architecture/testing/UI docs
```

Use as the main source for project-specific text.

### 7.2 Implementation evidence

Examples:

```text
code
tests
generated artifacts
screenshots
database schema
API endpoints
```

Use only after current repo check when claiming implementation.

### 7.3 External research

Examples:

```text
official product pages
documentation
standards
books
articles
Deep Research output
```

Use only for:

```text
- definitions;
- external product facts;
- technology references;
- comparison criteria;
- general engineering practices.
```

Do not use research as ready-made paragraphs.

## 8. Visual Artifacts

Each short draft should decide whether the subsection needs:

```text
figure
table
screenshot
diagram
code fragment
test result
none
```

If a visual artifact is needed, mark it in the future full draft as:

```text
TODO INSERT FIGURE
TODO INSERT TABLE
TODO INSERT SCREENSHOT
```

## 9. Fragment Harvesting

After each full draft attempt, identify:

```text
- strong thesis statements;
- good transitions;
- project-centered explanations;
- comparison conclusions;
- figure/table captions;
- clean formulations for defense;
- paragraphs that may fit another section.
```

Move them to:

```text
fragment-bank.md
```

Do not rely on old draft files to preserve good wording.

## 10. Draft Statuses

Use these statuses in the register:

```text
no-draft
short-discussed
short-approved
full-draft-v1
full-draft-v2
reviewed
fragments-harvested
source-checked
repo-checked
visuals-inserted
chapter-ready
merged
superseded
```

## 11. Section Draft Naming

Recommended naming:

```text
chapter-1/01-01-problem-domain.full-v1.md
chapter-1/01-02-existing-solutions-and-own-development.full-v1.md
chapter-2/02-03-api-contract.full-v1.md
```

Short drafts are normally chat-first.  
If a short draft must be preserved, use:

```text
chapter-1/01-01-problem-domain.short.md
```

## 12. Decision Boundary

Create an archive when:

```text
- saving a full draft attempt;
- updating workflow/templates/register;
- preserving a fragment bank;
- saving a batch of reviewed fragments;
- making a chapter-ready version.
```

Do not create an archive just to propose a short draft.  
Short drafts are normally shown in the chat first.
