# MERGE RISK REPORT

## Risk level

Low to medium.

## Why

- The target file is expected to be new according to the provided draft.
- If the file already exists locally, it may contain uncommitted work. The apply command backs it up before copying.
- The draft is a long topic-planning file intended for text generation and visual bridge work, not final thesis prose.

## Manual checks after applying

1. Run `git status`.
2. Run `git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/05-selected-direction-and-requirements/01-selected-direction-goals-tasks-requirements.topic.md`.
3. Confirm the file starts with `# Тема: Выбор направления разработки, цель, задачи и требования к web-приложению`.
4. Confirm no old 1.5 file with a different name should be merged.
