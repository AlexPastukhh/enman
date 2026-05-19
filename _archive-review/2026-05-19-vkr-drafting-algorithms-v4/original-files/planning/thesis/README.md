# VKR thesis workspace

This directory contains the working materials for the VKR / diploma text.

## Read first

1. `VKR-WORKFLOW-SOURCE-OF-TRUTH.md` — главный источник правил и навигации.
2. `NEW-CHAT-ONBOARDING.md` — краткая памятка для нового чата.
3. `chat-action-algorithms/README.md` — обязательные алгоритмы поведения чатов.
4. `vkr-topic-workbench/README.md` — где живут topic-драфты.
5. `vkr-clean/README.md` — где живут section-драфты и clean-материалы.

## Core storage

```text
planning/thesis/
├─ VKR-WORKFLOW-SOURCE-OF-TRUTH.md
├─ NEW-CHAT-ONBOARDING.md
├─ chat-action-algorithms/
├─ vkr-topic-workbench/
└─ vkr-clean/
```

## File roles

### Topic drafts

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Topic draft is the semantic base of a topic: placement, questions, research bridge, source materials, visual plan, boundaries, section blocks and drafting plan.

### Section drafts

```text
planning/thesis/vkr-clean/section-drafts/
```

Section draft is a developing text of a VKR subsection. It accumulates text inside section blocks prepared from topic drafts.

### Chat action algorithms

```text
planning/thesis/chat-action-algorithms/
```

This is the mandatory behavior layer. If the user asks for a draft, clarification, archive, visual review, repo-check or `тчт`, the chat must run the relevant algorithm.

## Active vs legacy

Legacy/support folders are not deleted automatically. They are used only for harvest and comparison. New drafts should be written only into active folders named in `VKR-WORKFLOW-SOURCE-OF-TRUTH.md` and related README files.

## Navigation rule

Whenever a file is created, moved, deleted or a folder structure changes, run navigation impact check:

```text
planning/thesis/chat-action-algorithms/navigation-impact-check.md
```

The chat must ask/check: which navigation files need updating because of this change?
