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
  raw-notes-capture-and-distribution.md
  topic-draft-default-flow.md
  topic-clarify-and-recheck-flow.md
  topic-to-section-block-drafting.md
  section-draft-generation.md
  question-generation-for-drafts.md
  question-priority.md
  semantic-point-discovery.md

evidence-and-materials/
  chapter-source-priority-map.md
  source-material-harvest-for-topic.md
  research-bridge.md
  repo-check-before-implementation-text.md
  visual-material-review.md

cleanup-and-legacy/
  legacy-chaotic-draft-review.md
  existing-chapter-draft-review.md
  existing-section-draft-reverse-engineering.md
```

## Core idea

```text
User command
→ mandatory algorithm
→ roadmap capture if needed
→ desired outcome / semantic points
→ semantic point desired-result coverage
→ relevant sources
→ question priority
→ questions + default answers
→ safe draft/update/review/archive
```

## Roadmap capture

If the user gives many cross-topic or future notes, use:

```text
drafting/raw-notes-capture-and-distribution.md
```

## Semantic point coverage

If the user gives an idea that explains a result, attach it to the relevant semantic point card.

Do not keep one huge topic-level coverage table.

## Existing section candidates

Existing chapters are not ignored. They can be used as section draft candidates, but only through reverse engineering and topic/section block workflow.

Use:

```text
cleanup-and-legacy/existing-section-draft-reverse-engineering.md
cleanup-and-legacy/existing-chapter-draft-review.md
```

## Navigation rule

Any time a file is created, changed, moved or deleted, run `navigation-impact-check.md` and ask/check which navigation files must be updated.
