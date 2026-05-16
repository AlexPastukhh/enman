# VKR Reviewer Workflow

Status: current reviewer workflow  
Scope: how reviewer chats inspect full VKR subsection draft attempts

## 1. Purpose

Reviewer chats are used after a full draft attempt exists.

They help improve a subsection draft without turning reviewer output into final text.

The Coordinator/Drafter owns final consolidation.

## 2. When To Use Reviewers

Use reviewer chats after:

```text
full draft attempt v1
full draft attempt v2, if major rewrite happened
chapter-ready candidate, if final risk check is needed
```

Do not use reviewers for a short draft by default. Short drafts are for discussion in the main Coordinator/Drafter chat.

## 3. Reviewer Inputs

A reviewer should receive:

```text
- repository and branch, if repo evidence is relevant;
- VKR topic;
- subsection title and chapter placement;
- project question for the subsection;
- current full draft text;
- known source/repo/visual TODOs;
- relevant clean source files or planning docs, if needed;
- scope of the reviewer role.
```

Reviewer chats may read supporting docs, but should not rewrite workflow docs or section files unless explicitly asked.

## 4. Reviewer Outputs Are Not Final Text

Reviewer output is feedback.

It may include suggested fragments or rewritten sentences, but Coordinator/Drafter decides what becomes the next full draft version.

Reviewer output should mark:

```text
- keep;
- revise;
- move;
- cut;
- needs source;
- needs repo check;
- needs visual;
- overclaim;
- generic;
- fragment-bank candidate.
```

## 5. VKR Content Reviewer

### Responsibility

Checks whether a subsection draft answers its project question.

Checks whether the draft preserves the main VKR lines:

```text
client requests
document flow
contracts/documents
notifications
```

### Looks for

```text
- missing project logic;
- missing system-specific facts;
- weak connection to ООО «ЗСК»;
- generic theory not tied to the project;
- missing source/evidence for project-specific claims;
- overclaiming implementation status;
- places where a table, figure, screenshot or diagram is needed.
```

### Does not own

```text
- final rewrite;
- chapter structure decisions outside review notes;
- style originality cleanup beyond content specificity;
- implementation changes.
```

## 6. VKR Structure Reviewer

### Responsibility

Checks form, order and placement of the subsection inside the explanatory note.

### Looks for

```text
- whether the subsection has a clear introduction/body/conclusion;
- whether paragraphs are in the right order;
- whether analysis, design and implementation are mixed incorrectly;
- whether material belongs to another chapter;
- whether a table/figure/application would be better than prose;
- whether subsection transitions are missing.
```

### Does not own

```text
- final wording polish;
- content correctness beyond structure impact;
- source selection details unless structure depends on it.
```

## 7. VKR Style & Originality Reviewer

### Responsibility

Checks style, wording, template phrases, generic theory and compilation risk.

The goal is authorial, project-specific and properly sourced text.

### Looks for

```text
- text that sounds generic or AI-like;
- template phrases;
- too much general theory;
- unsupported external facts;
- places needing citation;
- paragraphs that should be rewritten around project facts;
- opportunities to make wording more precise and less copied-looking.
```

### Boundary

This reviewer does not help bypass originality checks.

It helps make the text more authorial, project-specific and properly sourced.

## 8. Consolidation By Coordinator/Drafter

After reviewer passes, Coordinator/Drafter should create a consolidation note or update the full draft plan.

Consolidation should group feedback into:

```text
Accepted changes
Rejected/deferred suggestions
Fragment bank candidates
Source/repo checks required
Visual/table/figure additions
Structure moves
Risks for next draft
```

Then Coordinator/Drafter prepares full draft v2.

## 9. Fragment Bank Rule

Good reviewer suggestions should not be lost.

If a reviewer suggests a strong paragraph, transition, conclusion or caption, save it in:

```text
fragment-bank.md
```

Use statuses:

```text
candidate
reusable
needs-source
needs-repo-check
inserted
superseded
```

## 10. Suggested Review File Names

If reviewer outputs are stored as files, use:

```text
chapter-1/01-01-problem-domain.content-review-v1.md
chapter-1/01-01-problem-domain.structure-review-v1.md
chapter-1/01-01-problem-domain.style-review-v1.md
chapter-1/01-01-problem-domain.review-consolidation-v1.md
```

## 11. Review Checklist

For each review, answer:

```text
1. Does the subsection answer its project question?
2. Does it remain centered on the developed ООО «ЗСК» web application?
3. Does it preserve the request/document/contract/notification line where relevant?
4. Does it avoid overclaiming current implementation?
5. Does it distinguish project facts from external facts?
6. Does it need sources, repo checks, tables, figures, diagrams or screenshots?
7. Does any material belong to another chapter?
8. Which fragments should be saved?
9. What should Coordinator/Drafter change in the next version?
```
