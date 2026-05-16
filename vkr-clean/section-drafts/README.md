# VKR Section Drafts

Status: current working draft workflow  
Scope: gradually updated clean subsection drafts for the VKR explanatory note

This folder contains a workflow for preparing VKR subsection drafts.

It is not the place for raw research dumps, implementation planning or final diploma formatting.  
It is a controlled workspace for turning project-specific materials into VKR text.

## 1. Core Idea

Each VKR subsection is developed through two draft levels:

```text
short draft -> full draft attempt -> review/harvest -> fragment bank -> next full draft -> chapter-ready text
```

The two draft levels have different purposes.

## 2. Short Draft

A short draft is not a shorter version of the final text.

It is a readable planning note for the subsection.  
By default, it should be written in the chat first, not immediately committed as a file.

The short draft defines:

```text
- where the subsection belongs;
- what project question it answers;
- why the subsection is needed in the VKR logic;
- high-level narrative flow;
- project-specific materials to use;
- external sources to use and why;
- figures/tables/screenshots to insert;
- important questions and assumptions;
- what must be done before a full draft attempt;
- risks: generic theory, research copying, overclaiming, weak project connection.
```

Short draft target:

```text
Make the subsection logic discussable before writing paragraphs.
```

## 3. Full Draft

A full draft is a connected text attempt.

It is closer to final VKR text, but it is not automatically final or complete.

A full draft may contain:

```text
TODO SOURCE
TODO CHECK REPO
TODO INSERT TABLE
TODO INSERT FIGURE
TODO INSERT SCREENSHOT
TODO REWRITE
TODO DECIDE
```

Full draft target:

```text
Test whether the short-draft logic works as readable VKR text.
```

Full drafts are stored as files because they can be long and need versioned review.

## 4. Fragment Bank

When a full draft contains especially successful paragraphs, transitions, conclusions or figure/table captions, keep them in:

```text
fragment-bank.md
```

Do not lose good fragments when draft structure changes.

A fragment can be reused later in another subsection or chapter.

## 5. Current Draft Areas

Current working areas:

```text
chapter-1/
```

Known existing draft:

```text
chapter-1/01-02-existing-solutions-and-own-development.md
```

## 6. Default Workflow

```text
1. Write short draft in chat.
2. Discuss and revise short draft.
3. Create full draft attempt as a file.
4. Review full draft using full-draft-review-checklist.md.
5. Move strong fragments to fragment-bank.md.
6. Update section-draft-register.md.
7. Produce next full draft version or mark as chapter-ready.
```

## 7. Working Rule

Do not paste large research paragraphs into a draft.

Use research materials only to support project-specific reasoning, comparison criteria and verifiable external facts.

The center of each subsection must be the VKR project:

```text
client -> applicant -> request -> employee review -> decision -> document -> notification
```

## 8. What Belongs Here

Belongs here:

```text
- short/full draft templates;
- full draft attempts;
- fragment bank;
- review checklist;
- subsection draft register;
- chapter-ready subsection candidates.
```

Does not belong here:

```text
- raw Deep Research reports;
- final draw.io diagram generation;
- code implementation plans;
- API/slice/domain planning source-of-truth files;
- final DOCX formatting.
```
