# Documentation Examples Index

Status: current documentation-layer examples index / coverage tracker  
Doc version: v0.2.0
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

## 2A. Command Examples Placement And Reuse Levels

Reusable command/output examples live under:

```text
planning/documentation/examples/
```

The examples index distinguishes these reuse levels:

```text
fully reusable example:
  Works across most projects that use the same workflow/template/output mode.

profile-specific reusable example:
  Works for a project type/profile, such as a scenario-driven planning app.
  It may be referenced from a concrete root UCM only after the user/project accepts the example fit.

project-local example:
  Works only for one concrete current project.
  It should live in the project/root-local examples area chosen by that project, not as reusable docs-layer rule logic.
```

Preferred profile-specific reusable example placement:

```text
planning/documentation/examples/profiles/<profile-name>/
```

Legacy paths such as `planning/documentation/examples/project-specific/<project>/` may still exist. Treat them carefully: they can contain profile-specific reusable demonstrations or literal project-local examples. Mark the intended reuse level explicitly before wiring them into concrete root routes.

Examples referenced from root use-case rows or detailed traces are part of the required traversal chain for that command/action when the task is non-trivial. They demonstrate valid execution. They do not own command semantics, routing, source truth, output mode or permission boundary.

## 3. Example Coverage Table

The original copied active example coverage rows remain deferred. Field-kit examples are listed separately below.

| Example ID | Type | Owner | Related use-case/workflow | Status | File | Missing/deferred reason |
|---|---|---|---|---|---|---|
| `LEVEL-2-KEY-POINTS-SUMMARY-v1` | response/output example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | Level 2 answer with non-fixed Key points and fixed-order `Краткое саммари` | current example | `planning/documentation/examples/LEVEL-2-KEY-POINTS-SUMMARY-EXAMPLE.md` | covered |
| `PLAN-COMMAND-VALID-EXECUTION-v1` | response/command example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` and `planning/planning-use-case-map.md` | `планируй` / `спланируй` / `давай план` | current example | `planning/documentation/examples/PLAN-COMMAND-VALID-EXECUTION-EXAMPLE.md` | covered |
| `GOAL-MAP-BRIEF-RESPONSE-v1` | response/output command example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md`, `planning/goal-map-principles-workflow-template.md` and `planning/planning-use-case-map.md` | `кц` / `карта цели кратко` / `goal map brief` | current example | `planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md` | covered |
| `GOAL-MAP-SYNC-COMMAND-v1` | map-maintenance command + archive-output example | `planning/goal-map-principles-workflow-template.md`, `planning/planning-use-case-map.md` and active living Goal Map files | `синх карта` / `синхронизируй карту` / `синх карта архив` | current example | `planning/documentation/examples/GOAL-MAP-SYNC-COMMAND-EXAMPLE.md` | covered |
| `CRITICAL-REVIEW-COMMAND-v1` | response/modifier command example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` and `planning/planning-use-case-map.md` | `крит` / `критически оцени` / `critical review` | current example | `planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md` | covered |
| `PLAN-FILE-UPDATE-COMMAND-v1` | response/command + output example | `planning/documentation/file-update-overview-workflow.md`, `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md`, `planning/documentation/reviewable-agent-output-and-commands-workflow.md` and `planning/planning-use-case-map.md` | `план файл-обновление` / `спланируй файл-обновление` / `спланируй архив` | current example | `planning/documentation/examples/PLAN-FILE-UPDATE-COMMAND-EXAMPLE.md` | covered |
| `CURRENT-PLANNING-STATE-RESPONSE-v1` | response/output command example | `planning/CURRENT-PLANNING-STATE-TEMPLATE.md`, `planning/planning-use-case-map.md` and `planning/source-cascade-sync-workflow.md` | `положняк` / `текущий положняк` / `current planning state` | current example | `planning/documentation/examples/CURRENT-PLANNING-STATE-RESPONSE-EXAMPLE.md` | covered |
| `ARCHIVE-SOURCE-VS-OUTPUT-PACKAGE-v1` | command/output example | `planning/planning-use-case-map.md` and `planning/replacement-file-generation-guide.md` | `арх` vs `давай архив` | current example | `planning/documentation/examples/ARCHIVE-SOURCE-VS-OUTPUT-PACKAGE-EXAMPLE.md` | covered |
| `COMMAND-CREATION-WORKFLOW-v1` | command/workflow example | `planning/documentation/command-creation-workflow.md` and `planning/planning-use-case-map.md` | `создай команду` / `создай новую команду` / `create command` | deferred | _not created yet_ | Add after the workflow is used on a clean real follow-up command; example must demonstrate rule/template-driven command creation without copying owner logic. |
| `REPLACEMENT-ARCHIVE-PACKAGE-v1` | output example | `planning/replacement-file-generation-guide.md` | replacement archive/package generation | deferred | _not created yet_ | Deferred from prior documentation migration; add after example infrastructure is committed. |
| `POST-APPLY-PRESERVATION-CHECK-v1` | response/check example | `planning/replacement-file-generation-guide.md` and `planning/planning-use-case-map.md` | `проверь` after replacement archive/package application | deferred | _not created yet_ | Deferred from prior documentation migration; should show applied + preserved review. |
| `DIFF-CAPTURE-UTF8-CLIPBOARD-v1` | command-block example | `planning/replacement-file-generation-guide.md` | diff capture and clipboard commands | deferred | _not created yet_ | Deferred from prior documentation migration; should show `git --output` + UTF-8 clipboard + mojibake fallback. |
| `REPLACEMENT-ARCHIVE-NEW-FILES-DIFF-v1` | command-block example | `planning/replacement-file-generation-guide.md` | replacement archive diff checks for packages that add files | deferred | _not created yet_ | Deferred from prior documentation migration; should show `$newFiles`, `git add -N`, and full diff capture for added files. |
| `REPLACEMENT-ARCHIVE-CONVERSATION-LOOP-v1` | conversation workflow example | `planning/replacement-file-generation-guide.md` | replacement archive conversation review loop | deferred | _not created yet_ | Deferred from prior documentation migration; should show assistant archive output, user diff, assistant review, commit command and remote verification. |
| `SOURCE-USAGE-CASCADE-PILOT-FRAMEWORK-v1` | register/framework example | `planning/documentation/source-usage-cascade-governance-plan.md` | source usage cascade pilot framework | deferred | _not created yet_ | Framework and pilot skeleton are added first; add a filled example after the real SC-13D pilot row set is audited. |
| `FILE-UPDATE-OVERVIEW-v1` | response/output example | `planning/documentation/file-update-overview-workflow.md` and `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | table-based File Update Overview / final `План файл-обновление` block | deferred | _not created yet_ | Add after the grouped-table overview/template is used on at least one follow-up file/docs/code update; should include delivery-safety check rows. |
| `LOCAL-TARGETED-SCRIPT-EDIT-v1` | command/script workflow example | `planning/documentation/documentation-update-workflow.md` | local targeted script edit mode | deferred | _not created yet_ | Add after the one-large-file-one-script workflow is used in a clean follow-up; should show `.ps1` file link, copy/run commands, scoped diff capture and no auto-commit. |
| `HYBRID-ARCHIVE-SCRIPT-DELIVERY-v1` | output/conversation workflow example | `planning/replacement-file-generation-guide.md` and `planning/documentation/documentation-update-workflow.md` | hybrid archive plus targeted script delivery | deferred | _not created yet_ | Add after a real mixed safe-files archive + one-file script package is reviewed; should show archive for safe files, separate script for large file and combined diff review. |
| `LEVEL-2-KEY-POINTS-SUMMARY-FILE-UPDATE-v1` | response/output example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` and `planning/documentation/file-update-overview-workflow.md` | Level 2 answer with key points, contextual summary and final table-based `План файл-обновление` | deferred | _not created yet_ | Add after the format is used cleanly on a follow-up planning/update answer; final key point should show delivery safety for file updates. |
| `DRAFT-UPDATE-DIFFERENCES-v1` | draft update example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | active draft update with differences block | deferred | _not created yet_ | Should show `Отличия от предыдущего драфта` instead of `Key points first`. |
| `STRICT-TEMPLATE-NO-KEY-POINTS-v1` | response/template example | `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | strict specialized template output | deferred | _not created yet_ | Should show when not to add `Key points first` because the template already provides reviewable structure. |
| `LARGE-FILE-FULL-ARCHIVE-PREFERENCE-v1` | delivery-planning example | `planning/documentation/documentation-update-workflow.md` and `planning/replacement-file-generation-guide.md` | large-file delivery safety classification | deferred | _not created yet_ | Should show fresh full repo/archive + safe complete replacement preferred before script fallback. |
| `USE-CASE-MAP-CREATION-UPDATE-v1` | workflow/template example | `planning/documentation/use-case-map-workflow.md` and `planning/documentation/USE-CASE-MAP-TEMPLATE.md` | reusable use-case-map creation/update | deferred | _not created yet_ | Add after the workflow/template is used to create or update a concrete use-case map cleanly; should show owner linking without copying workflow/template logic. |
| `DOCUMENTATION-RESPONSIBILITY-ZONE-REVIEW-v1` | review workflow example | `planning/documentation/documentation-responsibility-zone-review-workflow.md` | existing doc content classification into reusable principle / specialized profile / adapter mapping / example / workflow detail | deferred | _not created yet_ | Add after the workflow is used on a real candidate migration or responsibility-boundary review. |
| `FIELD-KIT-SETUP-v1` | setup example | future field-kit owner file | deriving project-specific workflow/profile/adapter from a field kit | deferred | _not created yet_ | Add after the first field kit is created and used to derive project-specific artifacts. |
| `DOCUMENTATION-ACTION-LOG-ENTRY-v1` | log-entry example | `planning/documentation/documentation-action-log.md` | documentation action log entries | deferred | _not created yet_ | Add after the action log format is used on at least one follow-up update; should show concise logical action entry with PMR relation. |
| `NEW-CHAT-ONBOARDING-v1` | response example | `planning/planning-use-case-map.md` | New Chat Onboarding / first planning pass | deferred | _not created yet_ | Deferred from prior documentation migration; should show compact onboarding answer. |
| `ACCEPTED-COMMAND-NO-REINVENTION-v1` | guardrail example | `planning/planning-use-case-map.md` | accepted command / no reinvention | deferred | _not created yet_ | Deferred from prior documentation migration; should show stop-and-explain instead of silent mode switch. |

## 3A. Field Kit Examples

| Example ID | Type | Owner | Related use-case/workflow | Status | File | Notes |
|---|---|---|---|---|---|---|
| `STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE` | field-kit example | `planning/documentation/field-kits/status-reconciliation-field-kit.md` | evidence/current-reality model setup for scenario-driven software projects | current example | `planning/documentation/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md` | Demonstrates first-stage implemented status and evidence checking. |
| `SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE` | field-kit example | `planning/documentation/field-kits/shared-visibility-map-field-kit.md` | local detail to shared visibility mapping | current example | `planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md` | Demonstrates when a local slice question should be mirrored. |
| `SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE` | field-kit example | `planning/documentation/field-kits/source-usage-cascade-field-kit.md` | generic source/consumer/cascade review model | current example | `planning/documentation/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md` | Demonstrates source usage row and metadata-only review outcome. |
| `SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE` | Enman-specific scenario/application example | `planning/documentation/field-kits/source-usage-cascade-field-kit.md` and `planning/source-usage-cascade-profile.md` | Enman-specific source/consumer/cascade pilot demonstration | current Enman-specific example / demonstration only | `planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md` | Demonstrates how the reusable source-usage field kit can be instantiated for one Enman scenario/application architecture pilot. Not generic reusable rule logic and not an active register. |
| `SCENARIO-DOMAIN-SLICE-COMMAND-ROUTING-EXAMPLE` | profile-specific reusable command-routing example accepted for Enman | `planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md` and `planning/planning-use-case-map.md` | Scenario/domain/slice route-family commands in the Enman root use-case map | current profile-specific reusable example / demonstration only | `planning/documentation/examples/project-specific/enman/SCENARIO-DOMAIN-SLICE-COMMAND-ROUTING-EXAMPLE.md` | Demonstrates valid traversal from root command rows to scenario/domain/slice/testing owner docs without copying workflow logic. The legacy path remains until a dedicated examples-path migration. |

Profile-specific reusable examples should preferably live under `planning/documentation/examples/profiles/<profile-name>/`. Literal project-local examples should live in the project/root-local examples area selected by that project. All examples remain demonstration-only, not reusable rule logic.

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
