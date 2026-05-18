# MERGE-RISK-REPORT: vkr-chapter1-documents-feedback-drafts-replacement-v1

## Files affected

1. `planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/02-document-reference-and-storage-boundary.topic.md`
   - Expected action: replace old/rough 1.2.2 draft.
   - Risk: old guardrails or notes may be overwritten.
   - Mitigation: safe apply backs up the original file before replacement.

2. `planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/03-client-feedback-and-notification-boundary.topic.md`
   - Expected action: add or replace 1.2.3 draft.
   - Risk: if an existing file was manually created, it may be overwritten.
   - Mitigation: safe apply backs up the original file before replacement.

## Review checklist after apply

```powershell
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/02-document-reference-and-storage-boundary.topic.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-documents-and-feedback/03-client-feedback-and-notification-boundary.topic.md
```

Check that:

- 1.2.2 uses “сотрудник”, not “работник”.
- 1.2.2 states: metadata in SQL Server, file in filesystem, simple directory storage for diploma scope.
- 1.2.2 does not claim full ECM/СЭД, ЭДО, ЭП, or industrial storage service.
- 1.2.3 separates status, decision result, and notification.
- 1.2.3 does not claim email/push/notification service without repo-check.
- 1.2.3 includes the expanded question matrix.
```
