# Documentation Examples Index

Status: current documentation-layer examples index / coverage tracker  
Scope: navigation for documentation-layer working examples and explicit missing/deferred example decisions

## 1. Purpose

This file indexes working examples for documentation-layer workflows, templates, response commands, output modes and draft formats.

It answers:

```text
Which examples exist?
Which owner files do they demonstrate?
Which expected examples are intentionally missing or deferred?
Where should a future chat look before creating a new example?
```

This file is navigation and coverage tracking only. It does not own command routing, source modes, output modes, permission boundaries or workflow activation.

## 2. Owner Rules

Examples are supporting artifacts.

```text
Rules define correctness.
Templates define shape.
Workflows define process.
Use-case maps define routing and output mode.
Examples demonstrate correct application.
```

If an example needs command/source/output/permission logic, link to the owner file instead of copying that logic here.

## 3. Example Coverage Table

No filled documentation-layer examples are added yet.

| Example ID | Type | Owner | Related use-case/workflow | Status | File | Missing/deferred reason |
|---|---|---|---|---|---|---|
| `REPLACEMENT-ARCHIVE-PACKAGE-v1` | output example | `planning/replacement-file-generation-guide.md` | replacement archive/package generation | deferred | _not created yet_ | Candidate from chat history; add after example infrastructure is committed. |
| `POST-APPLY-PRESERVATION-CHECK-v1` | response/check example | `planning/replacement-file-generation-guide.md` and `planning/planning-use-case-map.md` | `проверь` after replacement archive/package application | deferred | _not created yet_ | Candidate from chat history; should show applied + preserved review. |
| `DIFF-CAPTURE-UTF8-CLIPBOARD-v1` | command-block example | `planning/replacement-file-generation-guide.md` | diff capture and clipboard commands | deferred | _not created yet_ | Candidate from chat history; should show `git --output` + UTF-8 clipboard + mojibake fallback. |
| `REPLACEMENT-ARCHIVE-NEW-FILES-DIFF-v1` | command-block example | `planning/replacement-file-generation-guide.md` | replacement archive diff checks for packages that add files | deferred | _not created yet_ | Candidate from chat history; should show `$newFiles`, `git add -N`, and full diff capture for added files. |
| `REPLACEMENT-ARCHIVE-CONVERSATION-LOOP-v1` | conversation workflow example | `planning/replacement-file-generation-guide.md` | replacement archive conversation review loop | deferred | _not created yet_ | Candidate from chat history; should show assistant archive output, user diff, assistant review, commit command and remote verification. |
| `SOURCE-USAGE-CASCADE-PILOT-FRAMEWORK-v1` | register/framework example | `planning/documentation/source-usage-cascade-governance-plan.md` | source usage cascade pilot framework | deferred | _not created yet_ | Framework and pilot skeleton are added first; add a filled example after the real SC-13D pilot row set is audited. |
| `FILE-UPDATE-OVERVIEW-v1` | response/output example | `planning/documentation/file-update-overview-workflow.md` and `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | table-based File Update Overview / final `Итог` block | deferred | _not created yet_ | Add after the grouped-table overview/template is used on at least one follow-up file/docs/code update; should include delivery-safety check rows. |
| `LOCAL-TARGETED-SCRIPT-EDIT-v1` | command/script workflow example | `planning/documentation/documentation-update-workflow.md` | local targeted script edit mode | deferred | _not created yet_ | Add after the one-large-file-one-script workflow is used in a clean follow-up; should show `.ps1` file link, copy/run commands, scoped diff capture and no auto-commit. |
| `HYBRID-ARCHIVE-SCRIPT-DELIVERY-v1` | output/conversation workflow example | `planning/replacement-file-generation-guide.md` and `planning/documentation/documentation-update-workflow.md` | hybrid archive plus targeted script delivery | deferred | _not created yet_ | Add after a real mixed safe-files archive + one-file script package is reviewed; should show archive for safe files, separate script for large file and combined diff review. |
| `LEVEL-2-KEY-POINTS-SUMMARY-ITOG-v1` | response/output example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` and `planning/documentation/file-update-overview-workflow.md` | Level 2 answer with key points, contextual summary and final table-based `Итог` | deferred | _not created yet_ | Add after the format is used cleanly on a follow-up planning/update answer; final key point should show delivery safety for file updates. |
| `DRAFT-UPDATE-DIFFERENCES-v1` | draft update example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | active draft update with differences block | deferred | _not created yet_ | Should show `Отличия от предыдущего драфта` instead of `Key points first`. |
| `STRICT-TEMPLATE-NO-KEY-POINTS-v1` | response/template example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | strict specialized template output | deferred | _not created yet_ | Should show when not to add `Key points first` because the template already provides reviewable structure. |
| `LARGE-FILE-FULL-ARCHIVE-PREFERENCE-v1` | delivery-planning example | `planning/documentation/documentation-update-workflow.md` and `planning/replacement-file-generation-guide.md` | large-file delivery safety classification | deferred | _not created yet_ | Should show fresh full repo/archive + safe complete replacement preferred before script fallback. |
| `USE-CASE-MAP-CREATION-UPDATE-v1` | workflow/template example | `planning/documentation/use-case-map-workflow.md` and `planning/documentation/USE-CASE-MAP-TEMPLATE.md` | reusable use-case-map creation/update | deferred | _not created yet_ | Add after the workflow/template is used to create or update a concrete use-case map cleanly; should show owner linking without copying workflow/template logic. |
| `DOCUMENTATION-RESPONSIBILITY-ZONE-REVIEW-v1` | review workflow example | `planning/documentation/documentation-responsibility-zone-review-workflow.md` | existing doc content classification into reusable principle / specialized profile / adapter mapping / example / workflow detail | deferred | _not created yet_ | Add after the workflow is used on a real candidate migration or responsibility-boundary review. |
| `FIELD-KIT-SETUP-v1` | setup example | future field-kit owner file | deriving project-specific workflow/profile/adapter from a field kit | deferred | _not created yet_ | Add after the first field kit is created and used to derive project-specific artifacts. |
| `DOCUMENTATION-ACTION-LOG-ENTRY-v1` | log-entry example | `planning/documentation/documentation-action-log.md` | documentation action log entries | deferred | _not created yet_ | Add after the action log format is used on at least one follow-up update; should show concise logical action entry with PMR relation. |
| `NEW-CHAT-ONBOARDING-v1` | response example | `planning/planning-use-case-map.md` | New Chat Onboarding / first planning pass | deferred | _not created yet_ | Candidate from chat history; should show compact onboarding answer. |
| `ACCEPTED-COMMAND-NO-REINVENTION-v1` | guardrail example | `planning/planning-use-case-map.md` | accepted command / no reinvention | deferred | _not created yet_ | Candidate from chat history; should show stop-and-explain instead of silent mode switch. |

## 4. Adding A New Example

Before adding a new example, use:

```text
planning/documentation/example-coverage-workflow.md
```

A new example should include:

```text
- what it demonstrates;
- owner workflow/template/use-case link;
- the example content;
- a short note about why it is valid;
- current/deferred/historical status if relevant.
```

Do not add examples that copy routing/source/output/permission logic from owner files.

## 5. Missing Or Deferred Examples

If a template, workflow, command, output mode or draft format does not have an example, record one of:

```text
not needed: <reason>
deferred: <reason and target condition>
covered: <existing example id>
```

Do not leave important template/output/command behavior without either an example or a recorded reason.
