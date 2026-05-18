# MERGE-RISK-REPORT

## Risk level

Medium-low.

## Why

- The archive adds/replaces one topic draft file under Chapter 2 workbench.
- The target is a planned new Chapter 2 topic draft path.
- If a local file already exists, the apply command preserves it in `_archive-review` before replacement.

## Main review points after apply

1. Confirm the target file starts with `# Тема: Предметная модель web-приложения...`.
2. Confirm placement under `03-chapter-2-design/02-domain-model-lifecycles/`.
3. Review that the preface placement instructions from the uploaded source are not inserted into the topic file.
4. Check that repo/code terms remain correspondence terms, not the primary VKR language.
5. Check that no existing local draft was unintentionally overwritten without backup.
