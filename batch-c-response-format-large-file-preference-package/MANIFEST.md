# Batch C Response Format And Large-File Preference Package

Status: ready for manual apply  
Source snapshot: uploaded `enman-my-changes (84).zip`  
Scope: response format cleanup, File Update Overview / `Итог` semantics, use-case routing and large-file archive-first preference

## Purpose

This package applies Batch C as complete replacement files.

It updates the response/output system so that:

```text
- Level 2 keeps the reviewable body and can add `Key points first`;
- `Краткое саммари` is contextual and appears before `Итог`;
- `Итог` is file/change/update-oriented and remains the final block when present;
- during planning, `Итог` is the rolling nearest-batch plan;
- after artifact/diff/application, `Итог` summarizes actual update state;
- draft updates use `Отличия от предыдущего драфта`;
- strict-template answers do not need `Key points first` by default;
- large file alone is not a reason for script mode;
- fresh full repo/archive content + safe complete replacement is preferred for large files when possible.
```

## Target File Delivery Safety Classification

Included as complete replacements because a fresh full repo archive was provided:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/planning-use-case-map.md
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
planning/documentation/documentation-update-workflow.md
planning/replacement-file-generation-guide.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/examples/README.md
planning/documentation/documentation-action-log.md
```

No targeted scripts are included.

## Changes

```text
Response format:
- Add conditional `Key points first`.
- Preserve Level 2 task/scope, sources/coverage, assumptions/risks, verification and next step.
- Add `Краткое саммари` as contextual summary.
- Keep `Итог` as the final file/change/update block only when applicable.
- Add direct response-block commands.

Drafting:
- Use `Отличия от предыдущего драфта` for active draft updates.
- Do not force `Key points first` on draft updates or strict-template answers.

File Update Overview / Итог:
- Clarify planned mode and actual mode.
- Update template to normal Markdown output shape.
- Clarify ownership between response workflow, overview workflow and template.

Large-file preference:
- Do not choose script mode only because a file is large.
- Prefer fresh full repo/archive content + safe complete replacement when available.
- Keep targeted scripts as fallback when complete replacement is unsafe.
- Treat repeated script need as a split/refactor signal.

Examples/action log:
- Add deferred example rows for the new response/output behavior.
- Add action-log entry for Batch C.
```

## Not Included

```text
- No .ps1 scripts.
- No code or generated artifacts.
- No new documentation owner files.
- No PMR update.
```
