# MERGE-RISK-REPORT: vkr-chapter1-problematic-draft-replacement-v1

## Risk level

Medium: complete replacement of an existing topic draft.

## Reason

The uploaded draft states that the repository currently has an old preparatory file with `material-harvest-needed`, mixed Russian/English text and a short plan, and that it should be replaced by the full Russian draft.

## Files affected

- `planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-problematic/01-manual-process-problems.topic.md`

## Review checklist after apply

1. Run `git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-problematic/01-manual-process-problems.topic.md`.
2. Check that the resulting file starts with `# Тема: Проблематика ручного и разрозненного ведения заявок, документов и уведомлений`.
3. Check that the old local file was backed up under `_archive-review/vkr-chapter1-problematic-draft-replacement-v1/original-files/`.
4. Check that the draft keeps the Chapter 1 structure and does not introduce implementation/API/DB details into 1.3.
5. Check that claims about ООО «ЗСК» remain cautious and do not assert unverified real historical process facts.
