# MERGE-RISK-REPORT

## Risk level

Medium.

## Why

The target file already exists in the repository and is being replaced with a newer full topic draft.

## Main risks

1. Local uncommitted edits in the target file may be overwritten if not reviewed.
2. Existing references in README/topic-index files may still point to older wording.
3. The new draft intentionally keeps citation pass as a future step.
4. Section 1.4 must remain separated from section 1.5: do not move full goals/tasks/requirements into this topic.

## Safe apply approach

Use the provided PowerShell command. It backs up the existing target file into `_archive-review/.../original-files/` before copying the replacement.
