# Algorithm: navigation impact check

Run this whenever a file is created, changed, moved, renamed or deleted.

## Required question

```text
Какие навигационные файлы нужно обновить из-за этого изменения?
```

Do not ask only abstractly. Check the likely locations below.

## Always consider

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
```

## If new-chat or chat algorithms changed

```text
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/chat-action-algorithms/README.md
```

## If topic-workbench changed

```text
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-topic-workbench/00-workflow-and-rules/README.md
corresponding chapter README
topic-index.md — only if topics were added/renamed/moved
```

Do not use `topic-index.md` as a status board.

## If clean layer changed

```text
planning/thesis/vkr-clean/README.md
planning/thesis/vkr-clean/vkr-materials-index.md
planning/thesis/vkr-clean/section-drafts/README.md
corresponding chapter README/index if it exists
```

## If visual briefs changed

```text
chapter visual-briefs/README.md, if it exists
figures index, if it exists
related topic draft, if the visual belongs to a topic
```

## If legacy/chaotic drafts changed

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/README.md
planning/thesis/vkr-clean/legacy-chaotic-drafts/chaotic-draft-review-template.md
```

## If an archive is created

```text
MANIFEST
APPLY
DELETIONS, if anything is removed
_archive-review/<slug>/ORIGINALS-INDEX.md
_archive-review/<slug>/MERGE-RISK-REPORT.md
```
