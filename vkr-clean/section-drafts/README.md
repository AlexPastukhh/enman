# VKR Section Drafts

Status: working drafts / reviewer workflow synchronized  
Scope: gradually updated clean subsection drafts for the VKR explanatory note

This folder contains subsection drafts that are closer to final VKR text than planning notes, research reports or raw clean-source files.

## 1. Purpose

Section drafts are used to gradually assemble VKR text from:

```text
project-specific notes
repo/planning artifacts
implementation facts
research sources
author analysis
tables / screenshots / diagrams
reviewer feedback
```

They are not final diploma text yet. Each draft should keep enough source protocol so that it is clear:

```text
- what project question the subsection answers;
- which information comes from the project;
- which information comes from external research;
- where a citation is needed;
- where a diagram, screenshot or table should be inserted;
- what must be checked before finalizing;
- what reviewer feedback has been accepted, deferred or rejected.
```

## 2. Current Workflow Files

| File | Purpose |
|---|---|
| `vkr-section-drafting-workflow.md` | Short/full draft workflow and coordinator loop |
| `reviewer-workflow.md` | Reviewer roles, review process and feedback consolidation |
| `reviewer-prompts.md` | Reusable prompts for reviewer chats |
| `fragment-bank.md` | Strong reusable fragments, transitions, captions and conclusions |
| `section-draft-register.md` | Status register for subsection drafts and review state |
| `short-draft-template.md` | Template for short drafts when a short draft must be preserved |
| `full-draft-template.md` | Template for full draft attempts |
| `full-draft-review-checklist.md` | Checklist for full draft attempts |

## 3. Current Drafts

```text
chapter-1/01-01-problem-domain.full-v1.md
chapter-1/01-01-problem-domain.fragment-candidates.md
chapter-1/01-01-problem-domain.full-v1-review-notes.md
chapter-1/01-02-existing-solutions-and-own-development.md
```

## 4. Roles

### VKR Coordinator & Drafter

Owns the main drafting loop:

```text
short draft in chat
-> discussion
-> full draft archive attempt
-> reviewer feedback collection
-> feedback consolidation
-> fragment bank update
-> full draft v2/v3
```

The Coordinator/Drafter may prepare text drafts. It also coordinates repo/planning/research/implementation context.

### VKR Content Reviewer

Checks whether the draft answers its project question and preserves the VKR's project lines:

```text
client requests
document flow
contracts/documents
notifications
```

### VKR Structure Reviewer

Checks form, order, subsection placement, intro/body/conclusion and chapter boundaries.

### VKR Style & Originality Reviewer

Checks style, generic theory, template phrases, AI-like wording, citation needs and project-specific rewriting opportunities.

It does not help bypass originality checks. The goal is authorial, project-specific and properly sourced text.

### Documentation Keeper

Keeps workflow/navigation/register docs synchronized. It does not write actual VKR section text or perform content/style review unless explicitly asked.

## 5. Working Rule

Short drafts are normally discussed in chat first. They are used as a readable narrative map, not as mini-versions of final text.

Short drafts are not stored as files by default.

Full drafts are intermediate text attempts. A full draft should be readable as VKR text, but it may contain:

```text
TODO SOURCE
TODO INSERT FIGURE
TODO INSERT TABLE
TODO INSERT SCREENSHOT
TODO CHECK REPO
TODO REWRITE
TODO DECIDE
```

Do not paste large research paragraphs into a draft.

Use research materials only to support project-specific reasoning, comparison criteria and verifiable external facts.

The center of each subsection must be the VKR project:

```text
client -> applicant -> request -> employee review -> decision -> contract/document -> notification
```

Useful fragments from full draft attempts and reviewer notes should be harvested into `fragment-bank.md` before major rewrites.

## 6. Reviewer Output Rule

Reviewer outputs are not final text.

They are inputs for Coordinator/Drafter consolidation.

A reviewer should not rewrite the whole section by default. It should identify:

```text
- what is strong and should be kept;
- what is missing;
- what is generic or overclaimed;
- what needs source/repo/visual check;
- what belongs to another chapter;
- what should go to fragment bank.
```

## 7. File Naming Guidance

Recommended full draft attempt names:

```text
chapter-1/01-01-problem-domain.full-v1.md
chapter-1/01-01-problem-domain.full-v2.md
chapter-2/02-04-api-contract.full-v1.md
```

Recommended review output names when stored:

```text
chapter-1/01-01-problem-domain.content-review-v1.md
chapter-1/01-01-problem-domain.structure-review-v1.md
chapter-1/01-01-problem-domain.style-review-v1.md
chapter-1/01-01-problem-domain.review-consolidation-v1.md
```

Short drafts are usually chat-first. If a short draft must be preserved:

```text
chapter-1/01-01-problem-domain.short.md
```
