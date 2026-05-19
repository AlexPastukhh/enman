# VKR topic workbench

This is the semantic workspace for VKR topic drafts.

## Active source of topic drafts

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

A topic draft is not final VKR text. It stores meaning, questions, source materials, research bridge, visual bridge, evidence questions, boundaries and future section draft blocks.

## Main workflow files

```text
../VKR-WORKFLOW-SOURCE-OF-TRUTH.md
../chat-action-algorithms/README.md
TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
00-workflow-and-rules/
```

## Active Chapter 1 folders

```text
02-chapter-1-analysis/01-domain-process/
02-chapter-1-analysis/02-documents-and-feedback/
02-chapter-1-analysis/03-problematic/
02-chapter-1-analysis/04-automation-options/
02-chapter-1-analysis/05-selected-direction-and-requirements/
```

Legacy Chapter 1 folders should be treated as support/harvest only. Do not create new drafts there.

## Chapter 3 note

If there are two Chapter 3 folders, use the source-of-truth active marker. Current active path:

```text
04-chapter-3-implementation/
```

Support/detailed path, not active for new topic drafts:

```text
04-chapter-3-implementation-and-testing/
```

## Navigation rule

If a topic draft is added, renamed, moved or removed, run navigation impact check. Update topic-index only when topics actually changed. Do not store task statuses in topic-index.
