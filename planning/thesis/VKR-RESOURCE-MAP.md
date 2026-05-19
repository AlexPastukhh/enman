# VKR resource map

This file answers: where are VKR resources stored and how can a chat reach the necessary files.

## 1. Main entry points

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/chat-action-algorithms/README.md
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-clean/README.md
```

## 2. Topic drafts

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Topic drafts are the primary semantic base for VKR sections.

Active high-level areas:

```text
vkr-topic-workbench/01-introduction/
vkr-topic-workbench/02-chapter-1-analysis/
vkr-topic-workbench/03-chapter-2-design/
vkr-topic-workbench/04-chapter-3-implementation/
vkr-topic-workbench/05-conclusion/
vkr-topic-workbench/06-preddiploma-derivatives/
```

If legacy/support folders exist, do not write new drafts there unless navigation explicitly says so.

## 3. Section drafts

```text
planning/thesis/vkr-clean/section-drafts/
```

Section drafts are developing VKR text. They accumulate text by section blocks that come from topic drafts.

## 4. Existing chapter drafts

```text
planning/thesis/vkr-clean/existing-chapter-drafts/
```

Use this for existing/early chapters that are not final but may be useful.

Suggested structure:

```text
existing-chapter-drafts/
├─ README.md
├─ chapter-1/
├─ chapter-2/
├─ review-notes/
└─ extracted-materials/
```

These drafts can provide:

```text
structure candidates;
successful wording;
examples of tables;
problem notes;
things to avoid;
source reminders.
```

They must not bypass topic-draft workflow.

## 5. Research materials

Primary location depends on current repository state, but the resource should be reachable through:

```text
planning/thesis/vkr-topic-workbench/00-research-materials/
```

If research is stored elsewhere, update this file and the relevant README.

Research is used through questions:

```text
question from topic draft
→ research answer
→ project conclusion
→ section draft block
```

## 6. Visual materials and briefs

Visual information may live inside topic drafts or in chapter-level visual briefs.

If a dedicated folder is created, use:

```text
planning/thesis/vkr-topic-workbench/<chapter>/visual-briefs/
```

A visual must be connected to a section block and must not create overclaim.

## 7. Evidence / implementation facts

For chapter 3 use:

```text
planning/thesis/vkr-clean/chapter-3/
```

Typical files:

```text
chapter3_repo_preflight.md
chapter3_repo_check_facts.md
chapter3_slices_matrix.md
chapter3_screenshots_inventory.md
chapter3_open_questions.md
```

If absent, create only after repo/evidence check.

## 8. Scenarios, DATA, domain and slices

These usually live outside `planning/thesis/` in main planning areas. When a topic needs them, use `chapter-source-priority-map.md` and `source-material-harvest-for-topic.md`.

The chat must not assume paths by memory. If a source path is missing or moved, update navigation.

## 9. Reachability check

A chat can reach necessary VKR files if it can follow:

```text
README.md
→ VKR-WORKFLOW-SOURCE-OF-TRUTH.md
→ VKR-RESOURCE-MAP.md
→ relevant layer README
→ relevant chapter README / topic-index
→ topic draft / section draft / research / evidence file
```

If any step is missing, update navigation before or with the content change.
