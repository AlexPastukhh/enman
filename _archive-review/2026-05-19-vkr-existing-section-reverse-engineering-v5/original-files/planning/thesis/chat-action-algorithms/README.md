# Chat action algorithms

This folder contains mandatory behavior algorithms for chats working on VKR materials.

Algorithms are not optional suggestions. If the user gives a matching command, the chat must run the relevant algorithm.

## Main map

```text
archive-generation-and-navigation-update.md
navigation-impact-check.md
new-chat-context-recovery.md
tcht-command.md

drafting/
  topic-draft-default-flow.md
  topic-clarify-and-recheck-flow.md
  topic-to-section-block-drafting.md
  section-draft-generation.md
  question-generation-for-drafts.md
  question-priority.md

evidence-and-materials/
  chapter-source-priority-map.md
  source-material-harvest-for-topic.md
  research-bridge.md
  repo-check-before-implementation-text.md
  visual-material-review.md

cleanup-and-legacy/
  legacy-chaotic-draft-review.md
  existing-chapter-draft-review.md
```

## Core idea

```text
User command
→ mandatory algorithm
→ relevant sources
→ question priority
→ questions + default answers
→ safe draft/update/review/archive
```

## Existing chapter drafts

Existing chapters are not ignored. They can be used as a resource, but only through review and topic/section block workflow.

Use:

```text
cleanup-and-legacy/existing-chapter-draft-review.md
```

## Navigation rule

Any time a file is created, changed, moved or deleted, run `navigation-impact-check.md` and ask/check which navigation files must be updated.
