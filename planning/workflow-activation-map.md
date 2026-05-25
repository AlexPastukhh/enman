# Workflow Activation Map

Status: current workflow activation map  
Scope: how chats select and disclose workflows before non-trivial planning/repo work

## 1. Purpose

This file helps a chat decide which repository workflows apply to the current task.

A user should not need to remember workflow file names.

For non-trivial planning/repo work, the chat should:

```text
1. identify the active role;
2. identify task type;
3. select all relevant workflows;
4. disclose them in a Workflow Preflight;
5. separate implicit workflows from actions that need explicit user permission;
6. avoid pretending future/missing workflows already exist.
```

This file is a workflow router. It does not replace the workflow docs it points to.

## 2. Core Rule

Before non-trivial planning/repo work, output a short `Workflow Preflight`.

The preflight is required when the task may involve:

```text
- planning docs;
- repository files;
- source-of-truth decisions;
- scenario/domain/slice/API/testing/VKR docs;
- documentation refactors;
- shared registers;
- GitHub writes;
- implementation status claims;
- handoff prompts for another chat;
- workflow or prompt-manager changes.
```

The preflight can be skipped or shortened for trivial answers, simple clarifications, casual discussion, or purely local wording that does not affect files/workflows.

## 3. Workflow Preflight Format

Use this format before the main answer or before proposing edits:

```text
Workflow Preflight

Active role:
- ...

Task type:
- ...

Activated workflows:
- <workflow file>
  Reason:
  Activation type:
  Applies to:

Implicit checks:
- ...

Requires explicit permission:
- No / Yes
- If yes, for what:

Not activated but relevant:
- <workflow file>
  Reason:

Future / missing workflows:
- <workflow name or topic>
  Current status:
```

The preflight is not a permission grant. It only makes the intended workflow path visible before work continues.

## 4. Activation Types

| Activation type | Meaning |
|---|---|
| `always-on check` | Should be used automatically for non-trivial planning/repo work. |
| `conditional implicit` | Should be used automatically when the trigger matches. |
| `response-format` | Controls answer/review format; not a repo action. |
| `explicit-only` | Use only when the user directly requests this workflow/action. |
| `approval-required action` | Requires explicit user permission before executing the action. |
| `future / missing` | Principle or need exists, but no full workflow exists yet. |
| `transitional` | File/workflow exists but needs audit/refactor before being treated as fully canonical. |

## 5. Workflow Registry

| Workflow / file | Trigger | Activation type | Requires explicit user command? | Output / result |
|---|---|---|---|---|
| `planning/planning-agent-protocol.md` | Any non-trivial planning/repo task | `always-on check` | No | Role/scope/safety rules |
| `planning/agent-roles-and-required-actions.md` | Role must be selected or task crosses roles | `always-on check` | No | Active role and must-read docs |
| `planning/workflow-activation-map.md` | Non-trivial planning/repo task | `always-on check` | No | Workflow Preflight |
| `planning/planning-doc-responsibility-map.md` | Need to choose planning layer or owner area | `always-on check` | No | Layer routing |
| `planning/documentation/documentation-responsibility-map.md` | Information already belongs to documentation layer | `conditional implicit` | No | Documentation-layer owner file |
| `planning/documentation/documentation-update-plan-workflow.md` | Broad docs/navigation/status/register/source-of-truth/multi-file change | `conditional implicit` | No for plan; yes for edits | Documentation Update Plan |
| `planning/documentation/documentation-update-workflow.md` | Applying approved documentation update or planning docs update process | `conditional implicit` | Yes for GitHub writes | Docs update process |
| `planning/documentation/local-global-documentation-sync-workflow.md` | Local change may affect shared register/index/navigation | `conditional implicit` | No for check; yes for edits | Local/global sync check |
| `planning/documentation/status-reconciliation-workflow.md` | Docs status may differ from code/tests/generated artifacts | `conditional implicit` | No for analysis; yes for edits | Status findings / sync plan |
| `planning/documentation/reviewable-agent-output-workflow.md` | Non-trivial answer, audit, plan, handoff, or user says level/lvl/ур 1/2/3 | `response-format` | No | Structured reviewable answer |
| `planning/diagrams/scenario-drafting-workflow.md` | Scenario text/DATA/UI/behavior items/questions/clarifications work | `conditional implicit` | No for draft/analysis; yes for edits | Scenario source workflow |
| `planning/slices/draft-driven-discovery-principles.md` | Domain/slice/client/cross-cutting/testing/status draft work | `conditional implicit` | No | Draft-driven discovery loop |
| `planning/slices/l1-slice-drafting-guide.md` | L1/backend/client slice drafting or client sidecar drafting | `conditional implicit` | No for draft/analysis; yes for edits | Slice draft structure |
| `planning/slices/implemented-slice-sync-workflow.md` | Existing implemented slice draft needs sync against source/domain/code/tests | `transitional` / `conditional implicit` | No for audit; yes for edits | Implemented slice sync report |
| `planning/repo-grounded-github-line-links-workflow.md` | Explaining concrete repo facts, code/docs/status, or user asks for links | `conditional implicit` | No | Exact GitHub line links |
| `planning/replacement-file-generation-guide.md` | Archive/replacement output requested or chosen | `conditional implicit` | Usually explicit user request | Replacement package rules |
| GitHub direct edits / commits | User asks apply/update/create/delete in repo | `approval-required action` | Yes | GitHub commits |
| Source/version cascade sync workflow | Upstream source version changed and downstream review is needed | `future / missing` | Yes before designing/applying | No full workflow yet |

## 6. Implicit Workflow Chains

### Broad documentation update

Trigger examples:

```text
update docs architecture;
refactor documentation layer;
change responsibility maps;
add workflow docs;
change shared registers/navigation;
update status wording across files.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
planning-doc-responsibility-map.md
documentation-responsibility-map.md, if documentation layer is involved
documentation-update-plan-workflow.md
documentation-update-workflow.md
local-global-documentation-sync-workflow.md
status-reconciliation-workflow.md, if implementation/status evidence is involved
reviewable-agent-output-workflow.md, for structured plan/audit/handoff
```

### Scenario source work

Trigger examples:

```text
create/update scenario text;
update DATA;
derive/update behavior items;
resolve scenario ambiguity;
prepare diagram request from scenario sources.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
scenario-drafting-workflow.md
local-global-documentation-sync-workflow.md
reviewable-agent-output-workflow.md
```

### Slice or client draft work

Trigger examples:

```text
create/review slice draft;
create/review client sidecar;
review slice questions;
identify extension/change pressure;
plan implementation/testing from behavior items.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
draft-driven-discovery-principles.md
l1-slice-drafting-guide.md, when L1/client/backend slice format is relevant
local-global-documentation-sync-workflow.md
status-reconciliation-workflow.md, when implementation evidence is involved
reviewable-agent-output-workflow.md
```

### Implemented slice sync

Trigger examples:

```text
old implemented slice draft looks stale;
implemented code/tests exist but draft is missing current structure/status;
draft, source, code and tests may disagree.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
implemented-slice-sync-workflow.md
status-reconciliation-workflow.md
local-global-documentation-sync-workflow.md
reviewable-agent-output-workflow.md
```

Caution:

```text
implemented-slice-sync-workflow.md is transitional and must be audited before being treated as fully canonical because it references source/domain registry files that may be future/legacy/missing.
```

## 7. Explicit Permission Rules

The following actions require explicit user permission:

```text
- create/update/delete files in GitHub;
- commit changes;
- create or update branches;
- create PRs;
- move/rename files;
- delete/archive docs;
- apply a plan;
- change application code;
- change generated artifacts;
- run a broad migration/refactor;
- create a replacement archive/package when the user asked only for analysis.
```

The following do not require explicit permission because they are read-only or answer-format behavior:

```text
- identify active role;
- output Workflow Preflight;
- read relevant docs;
- choose activated workflows;
- prepare a plan;
- do read-only audit/check/review;
- state assumptions;
- use Level 1/2/3 answer format;
- use recheck/clarify/keep-prev style response commands.
```

## 8. Future / Missing Workflows

Do not pretend these are implemented if no workflow file exists.

| Topic | Current status | When to revisit |
|---|---|---|
| Source/version cascade sync | Principle exists in `planning/documentation/planning-docs-architecture-principles.md`, but full workflow/register system does not exist yet. | After local responsibility maps and at least one source usage register pilot. |
| Scenario/domain/slice local responsibility maps | Documentation local map exists; other layer maps are future. | During layer refactors. |
| Root map shrink to thin router | Root map is transitional and intentionally detailed. | After local responsibility maps exist for major layers. |
| Implemented slice sync modernization | Workflow exists but references possibly future/legacy registry names. | Before using it as canonical implemented slice workflow. |

Longer-term reminders live in:

```text
planning/planning-maintenance-register.md
```

## 9. Do Not

```text
- Do not skip Workflow Preflight for non-trivial repo/planning work.
- Do not hide activated workflows from the user.
- Do not treat response-format workflows as permission to edit files.
- Do not use GitHub mutation tools unless the user explicitly requested repository changes.
- Do not pretend future/missing workflows already exist.
- Do not activate only the top-level workflow when a nested workflow is clearly required.
- Do not apply a workflow silently when its activation would change scope or require user approval.
```

## 10. Success Criteria

Workflow activation is working when:

```text
- the user can see which workflows are being used before work continues;
- all relevant activated workflows are mentioned;
- implicit checks and explicit permission-required actions are separated;
- future/missing workflows are called out honestly;
- the user can correct workflow choice before edits or deep work;
- new workflows can be added to this map without rewriting every prompt.
```
