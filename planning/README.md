# Planning Documentation

Status: stable navigation / source-of-truth map  
Purpose: explain where to read planning materials and how not to confuse planning notes with implementation truth.

## 1. Core Rule

Planning docs are not a replacement for checking the current codebase.

For implementation state, inspect the current branch, code, tests, migrations, generated API contracts and runtime screenshots.

For scenario behavior, use scenario specs and behavior items.

For VKR/thesis wording, use `planning/vkr-clean-reference.md`.

## 2. Repository Editing Workflow

When documentation changes are small and well-scoped, prefer direct GitHub file edits from ChatGPT over manual replacement archives.

By default, use one commit per file. This makes each change easy to inspect and easy to revert.

Direct GitHub edits must still follow scope rules:

```text
- change only files explicitly included in the requested scope;
- do not rewrite unrelated planning documents;
- do not change code, generated artifacts or implementation files unless explicitly requested;
- keep commit messages specific;
- prefer one file per commit by default.
```

## 3. Workflow Activation

For non-trivial planning/repo work, read:

```text
planning/workflow-activation-map.md
```

The workflow activation map explains:

```text
- which workflows exist;
- when each workflow is activated;
- which workflows are implicit checks;
- which actions require explicit user permission;
- what Workflow Preflight should be shown before deep work or edits;
- which workflows are future/missing or transitional.
```

A future chat should not require the user to remember workflow file names. It should read the activation map and disclose activated workflows before continuing.

For deferred planning-docs/workflow cleanup tasks and condition-based follow-ups, use:

```text
planning/planning-maintenance-register.md
```

This register is for future documentation/workflow maintenance, not for ordinary feature TODOs.

## 4. Source-of-Truth Model

### Implementation truth

Use:

```text
current Git branch
code
tests
migrations
OpenAPI / generated contracts
runtime screenshots
```

Do not use planning status files as proof that a feature is implemented.

### Scenario behavior truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
```

### Slice scope truth

Use:

```text
planning/slices/
planning/slices/l2/
planning/slices/cross-cutting/
```

Slice docs describe intended work and scope. They are not implementation proof.

### Architecture, API, client and testing decisions

Use:

```text
planning/architecture/
planning/api/
planning/client/
planning/testing/
planning/adr/
```

### VKR / thesis clean wording

Use:

```text
planning/vkr-clean-reference.md
```

### Non-canonical recovery notes

Use only after canonical docs:

```text
planning/dirty-drafts/
```

Dirty drafts are not source of truth.

## 5. Task-Based Navigation

### For implementation planning

Read:

```text
planning/README.md
planning/workflow-activation-map.md
relevant scenario spec
relevant slice doc
relevant architecture/API/client/testing doc
```

Then inspect the current branch.

### For VKR / thesis writing

Read:

```text
planning/README.md
planning/workflow-activation-map.md
planning/vkr-clean-reference.md
relevant scenario/domain/architecture docs
```

Then verify implementation from code, tests, screenshots or generated contracts.

### For scenario and diagram work

Read:

```text
planning/workflow-activation-map.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/diagrams/scenario-clarifications/README.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

### For slice work

Read:

```text
planning/workflow-activation-map.md
planning/slices/README.md
planning/slices/l2/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

### For backend cleanup / legacy boundary work

Read:

```text
planning/workflow-activation-map.md
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
```

### For documentation-only work

Read:

```text
planning/workflow-activation-map.md
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/replacement-file-generation-guide.md
```

### For agent scope / prompt safety work

Read:

```text
planning/workflow-activation-map.md
planning/planning-agent-protocol.md
planning/agent-scope-boundaries-and-prompt-safety.md
planning/planning-doc-responsibility-map.md
```

### For workflow/documentation maintenance tracking

Read:

```text
planning/workflow-activation-map.md
planning/planning-maintenance-register.md
planning/planning-doc-responsibility-map.md
```

Use the maintenance register for deferred planning-docs/workflow tasks that have trigger conditions.

### For dirty draft recovery

Read dirty drafts only after canonical docs:

```text
planning/dirty-drafts/README.md
```

Dirty drafts are recovery/context notes, not implementation truth and not final VKR wording.

## 6. Historical / Internal Status Notes

The following files may be useful as historical or internal handoff notes, but they are not implementation truth:

```text
planning/l1-current-implementation-status.md
planning/l2-current-planning-status.md
planning/planning-workflow-current.md
```

Use them only as context. For implementation state, inspect the current branch.

## 7. Internal Labels and VKR Wording

`L1` and `L2` may appear in internal planning history, but they must not be used in VKR, presentation, report or defense speech.

Use clean terms instead:

| Internal label | Clean wording |
|---|---|
| L1 | client account, applicant, request creation, my requests |
| L2 | employee review and agreement exchange |
| Employee Review | request review by employee |
| Agreement Exchange | agreement/document exchange |
| AgreementDocumentRef | document reference and metadata |

## 8. Scenario / Diagram Status Markers

For post-L1/L2 scenario and diagram work, use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Scenario docs may mark future implementation and deferred extension points uniformly with:

```text
[PLANNED]
[DEFERRED]
[DESIGNED]
[QUESTION]
```

`[IMPLEMENTED]` still requires current repo evidence.

## 9. Not Source of Truth

Do not use these as current implementation proof:

```text
dirty drafts
old current-status snapshots
implementation archives
chat/prompt notes
unverified planning text
```
