# Merge Risk Report

## High-risk replacements

### planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/README.md

Reason:
- Chapter 1 active structure is being refined;
- old initial folder names are not deleted;
- risk: duplicate folders may confuse navigation if not documented.

Required post-apply check:
- confirm active folder list is visible;
- confirm legacy initial folder names are explicitly marked as superseded naming;
- confirm no technical Chapter 2/3 topics moved into Chapter 1.

### planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/topic-index.md

Reason:
- chapter-level index now points to new active folders;
- existing first-version folders may still exist in the repo.

Required post-apply check:
- confirm active folder list matches actual new folders;
- confirm legacy folder note is clear;
- confirm no accidental deletion is expected from zip apply.

## Medium-risk replacements

### planning/thesis/vkr-topic-workbench/00-workflow-and-rules/author-materials/raw-author-message-log.md

Reason:
- raw author messages are appended;
- risk: raw log may become too large over time.

Required post-apply check:
- confirm entries are raw and not processed;
- confirm no raw message is presented as final thesis wording.

## Additive files

The archive adds:

- `archive-merge-safety-protocol.md`;
- refined Chapter 1 active folders;
- first topic draft for point 1.1;
- starter topic stubs for points 1.2–1.5.

## Cleanup rule

Do not delete old Chapter 1 initial folders in this archive. Cleanup should be a separate explicit cleanup/archive step after material review.
