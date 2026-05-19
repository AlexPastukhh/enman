# Topic-to-section block workflow

This file explains how topic drafts become section drafts.

## Main idea

```text
topic draft does not replace section draft;
topic draft feeds section draft.
```

Topic draft and section draft develop in parallel.

```text
topic draft
↔ questions / research / repo-check / visual bridge / existing chapter drafts
↔ semantic points
↔ section draft blocks
→ section draft v1
→ review
→ clean VKR text
```

## Semantic point

A semantic point is the smallest meaningful unit of disclosure inside a topic.

It has:

```text
name;
purpose in the topic;
place in the disclosure order;
what goes before it;
what goes after it;
questions;
default answers / assumptions;
source support;
plan of disclosure;
example of conversion into VKR text.
```

## Process from topic draft to section draft

1. Topic draft describes semantic points of a topic.
2. For each semantic point, the chat generates questions.
3. Questions are prioritised: blocking, strong, research, repo/evidence, visual, style.
4. The chat collects source materials: scenarios, DATA, domain, slices, research, visuals, repo/evidence, existing chapter drafts.
5. For each point, the chat creates a plan of disclosure.
6. The section draft creates similar headings/blocks.
7. Text is gradually written inside those blocks.
8. The section draft is reviewed and edited.

## Existing chapter drafts as section draft candidates

Existing chapter drafts can be used as a secondary source and as current section draft candidates.

They may provide:

```text
structure candidates;
semantic point candidates;
paragraph candidates;
table candidates;
problem notes;
questions;
things to avoid.
```

But they cannot bypass workflow.

Reuse must follow:

```text
existing chapter material
→ review
→ reverse-engineer semantic point
→ topic draft semantic point / material candidate
→ questions
→ source support
→ overclaim check
→ section draft block decision
```

Use:

```text
planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-section-draft-reverse-engineering.md
```

## Do not paste raw chapter text into topic draft

Correct:

```text
existing paragraph
→ semantic point
→ topic-draft questions and plan
→ revised section block
```

Incorrect:

```text
existing paragraph
→ copied into topic draft as if it were topic analysis
```

## No fragment bank

There is no mandatory fragment bank. Section draft itself is where text accumulates.

If a paragraph has no clear section block, do not store it as random text. Return to topic draft and decide which semantic point / block it supports.
