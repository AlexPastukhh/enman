# VKR Topic Workbench

Status: updated structure / topic-first VKR drafting workflow  
Scope: semantic topic workbench before full section drafts

This directory is a working base for assembling VKR content by meaning, not a final thesis text directory.

The workbench organizes:

```text
mandatory VKR content block
-> future VKR point/subsection
-> project-specific topic
-> material harvest
-> visual evidence
-> questions / assumptions / coverage
-> later section draft
```

## Core rule

Top-level folders represent mandatory VKR content blocks. Topic folders/files represent the project-specific way to disclose those blocks.

Do not use internal implementation labels such as `L1` / `L2` as VKR-level concepts. In thesis-facing materials use project/domain terms: client request flow, employee review flow, agreement/document exchange, implementation phase, software increment or vertical slice.

## Relation to existing workflows

This workbench is placed before `planning/thesis/vkr-clean/section-drafts/`:

```text
semantic topic map
-> material harvest
-> visual evidence
-> section short draft
-> full draft
-> reviewer pass
-> final text
```

It does not replace section drafts, preddiploma documents or final Word/PDF files.

## Main folders

| Folder | Meaning |
|---|---|
| `00-workflow-and-rules/` | Rules, templates, protocols and author-message capture |
| `01-introduction/` | Introductory VKR content: relevance, goal, tasks, significance |
| `02-chapter-1-analysis/` | Analysis chapter topics: domain, problems, alternatives, requirements |
| `03-chapter-2-design/` | Design chapter topics: roles, scenarios, lifecycles, domain model, storage, architecture, API, UI |
| `04-chapter-3-implementation/` | Implementation, testing and demo evidence topics |
| `05-conclusion/` | Results, task completion and future development |
| `06-preddiploma-derivatives/` | Practice report, presentation and speech as derivatives of VKR materials |

## Current use

Use this directory to store what should be said and shown before writing long polished text. For each topic, collect:

```text
purpose
questions
planned disclosure flow
materials to harvest
visual evidence
coverage
risks
raw author messages when useful
```

## Chapter folders currently used

```text
02-chapter-1-analysis/
03-chapter-2-design/
04-chapter-3-implementation/
```

The folder `04-chapter-3-implementation-and-testing/` was an earlier naming idea and should not be used for the current Chapter 3 topic drafts.
