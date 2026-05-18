# CC-CLIENT-UI-MIGRATION-001 — Current UI To Slice-Owned UI

Status: temporary implementation goal / not an application scenario  
Type: cross-cutting implementation migration note  
Applies to: client UI, CSS, client slice drafts, scenario UI specs

## 1. Purpose

Document the transition problem from current mixed UI/CSS state to clean slice-owned UI.

This is not an application scenario. It describes an implementation migration goal and may be removed or compressed after the transition is complete.

## 2. Current Problem

The current UI grew through multiple layers:

```text
old global styles
page-specific CSS
feature/page quick fixes
mock/test styles
partially updated home/header/auth pages
client slice drafts without explicit UI/CSS sections
scenario UI sources that are missing or partial
```

If we directly "make screens prettier" without updating UI scenario sources and client slice drafts, the project will keep accumulating unclear ownership.

## 3. Target Direction

Move toward:

```text
scenario UI specs define visible user requirements
client slice drafts translate those requirements to implementation
CSS ownership follows global/page/widget/entity/feature/shared boundaries
implementation archives follow the updated slice draft
```

## 4. Migration Order

```text
1. Normalize UI scenario source rules and template.
2. Review which UI scenario specs are complete, partial, stub or missing.
3. Add/normalize UI scenario specs before refreshing related client slice drafts.
4. Refresh client slice drafts using new template.
5. Implement UI foundation/layout/forms.
6. Normalize production pages.
7. Clean old duplicate CSS/UI only after behavior and ownership are clear.
```

## 5. What Not To Do

```text
- do not mass-rewrite existing slice drafts without a focused task;
- do not implement large UI changes without an updated client slice draft;
- do not treat CSS as afterthought;
- do not place business-specific CSS into shared/ui;
- do not add decorative public navigation before app flow is clean;
- do not treat this migration note as a user scenario.
```

## 6. Temporary Use For Diploma/Docs

This note may help explain the project transition from working-but-fragmented UI to scenario-driven and slice-owned UI.

It is intentionally practical and may be deleted or rewritten after the migration is finished.
