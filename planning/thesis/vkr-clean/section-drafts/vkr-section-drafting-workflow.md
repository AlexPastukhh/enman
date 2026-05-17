# VKR Section Drafting Workflow

Status: current workflow / reviewer-loop synchronized  
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

It is written in the main chat first because it must be easy to read, challenge and reorder.

It is a narrative map, not a mini-final text.

Short drafts are not stored as files by default.

A short draft contains:

```text
1. Subsection place
2. Main project question
3. Why this subsection exists
4. High-level narrative flow
5. Project-specific materials to use
6. External sources to use, only if needed
7. Visual/table/screenshot needs
8. Required thesis points
9. Open questions
10. Work needed before full draft
11. Risks and anti-patterns
12. Expected result of full draft
```

### 3.2 Full Draft Attempt

The full draft attempt is a connected text version of a subsection.

It is allowed to be incomplete, but it must already read like a VKR subsection.

It is not final until source/repo/visual/reviewer checks are done.

Full draft attempts are stored as files.

They may have versions:

```text
full-v1
full-v2
full-v3
chapter-ready
```

A full draft may include:

```text
TODO SOURCE
TODO CHECK REPO
TODO INSERT TABLE
TODO INSERT FIGURE
TODO INSERT SCREENSHOT
TODO REWRITE
TODO DECIDE
```

## 4. Roles

### VKR Coordinator & Drafter

Owns the high-level drafting loop:

```text
- prepares short drafts in chat;
- creates full draft archive attempts;
- updates section draft files;
- consolidates reviewer feedback;
- maintains fragment bank and draft status;
- coordinates repo/planning/research/implementation context.
```

### Reviewer Chats

Reviewer chats inspect full draft attempts after v1 or later.

They do not own final text.

Reviewer types:

```text
VKR Content Reviewer
VKR Structure Reviewer
VKR Style & Originality Reviewer
```

Use:

```text
reviewer-workflow.md
reviewer-prompts.md
```

## 5. Workflow Loop

```text
short draft in chat
  -> discussion / correction
  -> full draft attempt v1
  -> reviewer pass, if useful
  -> coordinator consolidates reviewer feedback
  -> harvest good fragments
  -> update fragment bank
  -> update section-draft-register.md
  -> revise short draft direction if narrative changed
  -> full draft attempt v2
  -> source/repo/visual checks
  -> chapter-ready version
  -> merge into chapter
```

## 6. Short Draft Rules

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

## 7. Full Draft Rules

Full draft should:

```text
- follow the high-level flow from the short draft;
- stay tied to the ООО «ЗСК» project;
- preserve the main project lines:
  client requests,
  document flow,
  contracts/documents,
  notifications;
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
- remove TODO markers before checks are complete;
- use internal workflow words such as chat/prompt/agent in final VKR text.
```

Workflow files may mention chats/roles because they are internal process documents. Final VKR text should not.

## 8. Source Handling

Use three source types differently.

### 8.1 Project materials

Examples:

```text
planning notes
scenario specifications
domain model notes
slice notes
clean architecture/testing/UI docs
```

Use as the main source for project-specific text.

### 8.2 Implementation evidence

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

When a draft describes code/implementation, prefer links to concrete GitHub lines/ranges.

### 8.3 External research

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

## 9. Visual Artifacts

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

## 10. Reviewer Pass

After full draft v1, reviewer chats may inspect it.

Reviewer outputs should answer:

```text
- what works;
- what is missing;
- what overclaims;
- what is generic;
- what needs sources;
- what needs visuals;
- what belongs to another section/chapter;
- what should be preserved in the fragment bank.
```

Reviewer outputs are not final text.

Coordinator/Drafter consolidates reviewer feedback and decides what changes become v2.

## 11. Fragment Harvesting

After each full draft attempt and after reviewer feedback, identify:

```text
- strong thesis statements;
- good transitions;
- project-centered explanations;
- comparison conclusions;
- figure/table captions;
- clean formulations for defense;
- paragraphs that may fit another section;
- reviewer-suggested rewrites worth preserving.
```

Move them to:

```text
fragment-bank.md
```

Do not rely on old draft files to preserve good wording.

## 12. Draft Statuses

Use these statuses in the register:

```text
no-draft
short-discussed
short-approved
full-draft-v1
content-reviewed
structure-reviewed
style-reviewed
review-consolidated
full-draft-v2
full-draft-v3
fragments-harvested
source-checked
repo-checked
visuals-inserted
chapter-ready
merged
superseded
```

## 13. Section Draft Naming

Recommended naming:

```text
chapter-1/01-01-problem-domain.full-v1.md
chapter-1/01-02-existing-solutions-and-own-development.full-v1.md
chapter-2/02-03-api-contract.full-v1.md
```

Review outputs, if stored:

```text
chapter-1/01-01-problem-domain.content-review-v1.md
chapter-1/01-01-problem-domain.structure-review-v1.md
chapter-1/01-01-problem-domain.style-review-v1.md
chapter-1/01-01-problem-domain.review-consolidation-v1.md
```

Short drafts are normally chat-first.  
If a short draft must be preserved, use:

```text
chapter-1/01-01-problem-domain.short.md
```

## 14. Decision Boundary

Create an archive when:

```text
- saving a full draft attempt;
- updating workflow/templates/register;
- preserving a fragment bank;
- saving a batch of reviewed fragments;
- saving reviewer outputs or consolidation notes;
- making a chapter-ready version.
```

Do not create an archive just to propose a short draft.  
Short drafts are normally shown in the chat first.
