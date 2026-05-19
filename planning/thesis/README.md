# VKR thesis workspace

This directory contains the working materials for the VKR / diploma text.

## Read first

1. `VKR-WORKFLOW-SOURCE-OF-TRUTH.md` — главный источник правил и workflow.
2. `VKR-RESOURCE-MAP.md` — карта ресурсов: где искать topic-драфты, section-драфты, research, существующие главы и evidence.
3. `NEW-CHAT-ONBOARDING.md` — памятка для нового чата.
4. `chat-action-algorithms/README.md` — обязательные алгоритмы поведения чатов.
5. `vkr-topic-workbench/README.md` — где живут topic-драфты и как они устроены.
6. `vkr-clean/README.md` — где живут section-драфты, existing chapter drafts и clean-материалы.

## Core storage

```text
planning/thesis/
├─ VKR-WORKFLOW-SOURCE-OF-TRUTH.md
├─ VKR-RESOURCE-MAP.md
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

Topic draft is the semantic base of a topic: placement, questions, source materials, research bridge, visual plan, boundaries, section blocks and drafting plan.

### Section drafts

```text
planning/thesis/vkr-clean/section-drafts/
```

Section draft is a developing text of a VKR subsection. It accumulates text inside section blocks prepared through topic-draft workflow.

### Existing chapter drafts

```text
planning/thesis/vkr-clean/existing-chapter-drafts/
```

Existing chapter drafts are early/ хаотично созданные главы or large section drafts that are not final, but are useful resources. They can provide structure, successful wording, tables and problem notes. They must be reviewed and passed through topic-draft workflow before reuse.

### Chat action algorithms

```text
planning/thesis/chat-action-algorithms/
```

This is the mandatory behavior layer. If the user asks for a draft, clarification, archive, visual review, repo-check or `тчт`, the chat must run the relevant algorithm.

## Active vs legacy

Legacy/support folders are not deleted automatically. They are used only for harvest and comparison. New drafts should be written only into active folders named in `VKR-WORKFLOW-SOURCE-OF-TRUTH.md`, `VKR-RESOURCE-MAP.md` and related README files.

## Navigation rule

Whenever a file is created, moved, deleted, renamed, or a folder structure changes, run navigation impact check:

```text
planning/thesis/chat-action-algorithms/navigation-impact-check.md
```

The chat must ask/check:

```text
Какие навигационные файлы нужно обновить из-за этого изменения?
```

At minimum check `README.md`, `VKR-WORKFLOW-SOURCE-OF-TRUTH.md`, `VKR-RESOURCE-MAP.md` and the README of the changed layer.
