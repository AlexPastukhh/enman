# MANIFEST: vkr-chapter1-documents-feedback-drafts-replacement-v1

## Purpose

Replace/add two Chapter 1 topic drafts for VKR subsection 1.2:

- 1.2.2 — document version and storage boundary.
- 1.2.3 — client feedback, statuses, and actions after request decision.

## Replacement files

- `planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/02-document-reference-and-storage-boundary.topic.md`
  - Replaces the older 1.2.2 draft with the updated Russian v4 draft.
  - Focus: договорная версия, отправитель версии, метаданные в SQL Server, файл в файловом хранилище, простая директория, границы scope.

- `planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/03-client-feedback-and-notification-boundary.topic.md`
  - Adds or replaces the 1.2.3 draft.
  - Focus: обратная связь клиента, статусы, результат решения, действия клиента, уведомления without email/push overclaiming.

## Source drafts included for review

- `_archive-review/vkr-chapter1-documents-feedback-drafts-replacement-v1/source-drafts/01-uploaded-1-2-2-document-storage-draft.txt`
- `_archive-review/vkr-chapter1-documents-feedback-drafts-replacement-v1/source-drafts/02-uploaded-1-2-3-client-feedback-draft.md`

## Merge risk

This archive intentionally replaces topic files. Use `git diff` before commit.
The safe apply command in `APPLY.vkr-chapter1-documents-feedback-drafts-replacement-v1.md` backs up existing destination files into `_archive-review/vkr-chapter1-documents-feedback-drafts-replacement-v1/original-files/` before copying replacements.
