# VKR resource map

This file answers: where are VKR resources stored and how can a chat reach the necessary files.

## 1. Main entry points

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/VKR-RESOURCE-MAP.md
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/chat-action-algorithms/README.md
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-clean/README.md
```

## 2. Roadmap and inbox

```text
planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
planning/thesis/vkr-topic-workbench/00-inbox/
planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/CHAPTER-1-ROADMAP.md
planning/thesis/vkr-topic-workbench/03-chapter-2-design/CHAPTER-2-ROADMAP.md
planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/CHAPTER-3-ROADMAP.md
```

Use these files for high-level notes, raw captured material, cross-chapter decisions and future planning.

## 3. Topic drafts

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Topic drafts are the primary semantic base for VKR sections.

They should not carry all future notes. Future/cross-topic notes should be kept in the roadmap layer.

## 4. Section drafts

```text
planning/thesis/vkr-clean/section-drafts/
```

Section drafts are developing VKR text. They accumulate text by section blocks that come from topic draft semantic points.

When existing chapters already exist, those chapters may temporarily be section draft candidates.

## 5. Existing chapter drafts

```text
planning/thesis/vkr-clean/existing-chapter-drafts/
```

Use this for existing/early chapters that are not final but may be useful.

## 6. Research materials

Primary location depends on current repository state, but the resource should be reachable through:

```text
planning/thesis/vkr-topic-workbench/00-research-materials/
```

If research is stored elsewhere, update this file and the relevant README.

## 7. Visual materials and briefs

Visual information may live inside topic drafts or in chapter-level visual briefs.

If a dedicated folder is created, use:

```text
planning/thesis/vkr-topic-workbench/<chapter>/visual-briefs/
```

A visual must be connected to a section block and must not create overclaim.

## 8. Evidence / implementation facts

For chapter 3 use:

```text
planning/thesis/vkr-clean/chapter-3/
```

If absent, create only after repo/evidence check.

## 9. Scenarios, DATA, domain and slices

These usually live outside `planning/thesis/` in main planning areas. When a topic needs them, use `chapter-source-priority-map.md` and `source-material-harvest-for-topic.md`.

The chat must not assume paths by memory. If a source path is missing or moved, update navigation.

## 10. Reachability check

A chat can reach necessary VKR files if it can follow:

```text
README.md
→ VKR-WORKFLOW-SOURCE-OF-TRUTH.md
→ VKR-RESOURCE-MAP.md
→ relevant layer README
→ relevant chapter README / roadmap / topic-index
→ topic draft / section draft / research / evidence file
```

If any step is missing, update navigation before or with the content change.
