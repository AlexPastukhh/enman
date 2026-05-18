# Topic-First VKR Workflow

Status: initial  
Scope: workflow before section drafts

## 1. Purpose

The workbench is used to build the semantic base of the VKR before writing long drafts.

Main pipeline:

```text
mandatory VKR point
-> project-specific topic
-> material harvest
-> visual evidence
-> topic coverage check
-> section short draft
-> full draft
-> reviewer pass
-> final text
```

## 2. Folder hierarchy rule

Top-level folders are mandatory VKR content blocks, not internal engineering interests.

Correct:

```text
02-chapter-1-analysis/
03-chapter-2-design/
04-chapter-3-implementation-and-testing/
```

Incorrect as top-level folders:

```text
scenarios-and-specification/
domain-model-and-ddd/
architecture-and-slices/
```

These belong inside the relevant VKR point.

## 3. L1/L2 terminology rule

Do not expose `L1` / `L2` labels as VKR-level concepts. Treat them as internal planning labels only.

Use thesis-facing terms instead:

```text
client request flow
employee review flow
agreement/document exchange
implementation phase
software increment
vertical slice
```

`L1` / `L2` may appear only in material-harvest notes when tracing repository/planning evidence.

## 4. Topic card rule

Each `.topic.md` must answer:

```text
why the topic is needed;
where it belongs;
what the reader must understand;
how the topic will be disclosed;
what material is needed;
what visual evidence is needed;
which questions and assumptions exist;
how coverage will be checked.
```

## 5. Visual-first personalization rule

Use project evidence rather than generic prose:

```text
thesis claim
-> diagram / screenshot / table
-> explanation
-> conclusion
```

Large diagrams should be simplified in the main text and moved in full to appendices.

## 6. Author message capture rule

Before creating a replacement/archive package, list in chat which user messages will be captured into `author-materials/raw-author-message-log.md`.

Raw author messages are captured as source material only. They are not final thesis text and are not automatically accepted as workflow rules.

## 7. Relationship with preddiploma practice

The preddiploma practice report is treated as a derivative of VKR materials. Do not make it the main drafting center.

```text
VKR topic workbench
-> preliminary explanatory note
-> preddiploma report as compressed derivative
-> presentation
-> speech
```
