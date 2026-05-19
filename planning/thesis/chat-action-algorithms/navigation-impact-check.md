# Algorithm: navigation impact check

Run whenever a file is created, changed, moved, renamed or deleted.

## Central question

```text
Какие навигационные файлы нужно обновить из-за этого изменения?
```

## Always check

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/VKR-RESOURCE-MAP.md
```

## If changed rules for new chats

```text
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/chat-action-algorithms/README.md
```

## If changed chat algorithms

```text
planning/thesis/chat-action-algorithms/README.md
README of subfolder: drafting/ evidence-and-materials/ cleanup-and-legacy/
```

## If changed topic workbench

```text
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-topic-workbench/00-workflow-and-rules/README.md
relevant chapter README
topic-index.md only if topics were added/renamed/moved
```

Do not maintain statuses in topic-index.

## If changed clean layer

```text
planning/thesis/vkr-clean/README.md
planning/thesis/vkr-clean/vkr-materials-index.md
planning/thesis/vkr-clean/section-drafts/README.md
planning/thesis/vkr-clean/existing-chapter-drafts/README.md
planning/thesis/vkr-clean/legacy-chaotic-drafts/README.md
```

## If changed research/visual/evidence resources

Update:

```text
planning/thesis/VKR-RESOURCE-MAP.md
relevant chapter README / visual-briefs README / evidence README when present
```

## If creating archive

Check archive-level navigation:

```text
MANIFEST
APPLY
DELETIONS if needed
_archive-review/.../ORIGINALS-INDEX.md
_archive-review/.../MERGE-RISK-REPORT.md
_archive-review/.../PRE-DELIVERY-CHECK.md
```

## Reachability check

A necessary file is reachable if a new chat can follow:

```text
planning/thesis/README.md
→ VKR-WORKFLOW-SOURCE-OF-TRUTH.md
→ VKR-RESOURCE-MAP.md
→ relevant layer README
→ relevant chapter README / topic-index
→ target file
```

If any step is missing, update navigation.
