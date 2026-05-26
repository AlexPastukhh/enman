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

For user-action/use-case traces, repeated commands, active context, traversal depth and read-source decisions, use:

```text
planning/planning-use-case-map.md
```

Response-level commands are documented in:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

Examples:

```text
level/lvl/ур
recheck
clarify
keep prev / кип прев
no ch / изм нет / без изм
use archive / читать архив / арх
б из арх
show section separately
merge section back
```

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

The preflight can be skipped or shortened for trivial answers, simple clarifications, casual discussion, purely local wording that does not affect files/workflows, or active-context continuation that does not need new evidence.

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
| `response-command` | Controls answer operation/checking behavior; not a repo action. |
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
| `planning/planning-use-case-map.md` | Need to map a user action/short command to docs/workflows/templates/read paths, active context, traversal depth or read source mode | `conditional implicit` / `response-command` | No | Action/use-case trace and traversal/source decision |
| `planning/planning-doc-responsibility-map.md` | Need to choose planning layer or owner area | `always-on check` | No | Layer routing |
| `planning/documentation/documentation-responsibility-map.md` | Information already belongs to documentation layer | `conditional implicit` | No | Documentation-layer owner file |
| `planning/documentation/documentation-update-plan-workflow.md` | Broad docs/navigation/status/register/source-of-truth/multi-file change | `conditional implicit` | No for plan; yes for edits | Documentation Update Plan |
| `planning/documentation/documentation-update-workflow.md` | Applying approved documentation update or planning docs update process | `conditional implicit` | Yes for GitHub writes | Docs update process |
| `planning/documentation/local-global-documentation-sync-workflow.md` | Local change may affect shared register/index/navigation | `conditional implicit` | No for check; yes for edits | Local/global sync check |
| `planning/documentation/status-reconciliation-workflow.md` | Docs status may differ from code/tests/generated artifacts | `conditional implicit` | No for analysis; yes for edits | Status findings / sync plan |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | Non-trivial answer, audit, plan, handoff, level/lvl/ур command, recheck, clarify, keep prev, no ch/без изм, use archive/арх, active-context draft/update command, section command | `response-format` / `response-command` | No | Structured reviewable answer or response-level command behavior |
| `planning/diagrams/scenario-drafting-workflow.md` | Scenario text/DATA/UI/behavior items/questions/clarifications work | `conditional implicit` | No for draft/analysis; yes for edits | Scenario source workflow |
| `planning/slices/slice-responsibility-map.md` | Need to place slice-layer information or decide slice file owner | `conditional implicit` | No | Slice-layer owner routing |
| `planning/slices/slice-draft-authoring-principles.md` | Create/review/refactor slice draft structure or section meaning | `conditional implicit` | No | Slice draft authoring rules |
| `planning/slices/slice-draft-authoring-workflow.md` | Create/review/refactor a slice draft or prepare slice draft plan | `conditional implicit` | No for draft/analysis; yes for edits | Root slice draft authoring process |
| `planning/slices/draft-driven-discovery-principles.md` | Slice-layer discovery for business/server/client/cross-cutting/helper slice drafts | `conditional implicit` | No | Slice discovery loop |
| `planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md` | Client sidecar drafting | `conditional implicit` | No for draft/analysis; yes for edits | Client-specific slice drafting algorithm |
| `planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md` | Server/backend/API slice drafting | `conditional implicit` | No for draft/analysis; yes for edits | Server-specific slice drafting algorithm |
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
planning-use-case-map.md, when the request is action/command-driven or repeated
planning-doc-responsibility-map.md
documentation-responsibility-map.md, if documentation layer is involved
documentation-update-plan-workflow.md
documentation-update-workflow.md
local-global-documentation-sync-workflow.md
status-reconciliation-workflow.md, if implementation/status evidence is involved
reviewable-agent-output-and-commands-workflow.md, for structured plan/audit/handoff or response commands
```

### Response command work

Trigger examples:

```text
level 1 / lvl 2 / ур 3;
recheck;
clarify;
keep prev / кип прев;
no ch / изм нет / без изм;
use archive / читать архив / арх;
б из арх;
show section separately;
merge section back;
давай драфт / обнови, when there is active draft context.
```

Activated workflows:

```text
workflow-activation-map.md
planning-use-case-map.md, for active context, traversal depth and read source mode
reviewable-agent-output-and-commands-workflow.md
```

Response commands do not grant permission to edit files, commit changes, delete files, move files or skip evidence required for current-state claims.

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
planning-use-case-map.md, for action/use-case trace if needed
scenario-drafting-workflow.md
local-global-documentation-sync-workflow.md
reviewable-agent-output-and-commands-workflow.md
```

### Slice or client/server draft work

Trigger examples:

```text
create/review slice draft;
create/review client sidecar;
create/review server/backend/API slice draft;
review slice questions;
identify extension/change pressure;
plan implementation/testing from behavior items;
active draft continuation: давай драфт / обнови.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
planning-use-case-map.md
planning-doc-responsibility-map.md
slice-responsibility-map.md
slice-draft-authoring-principles.md
slice-draft-authoring-workflow.md
draft-driven-discovery-principles.md
client/CLIENT-SLICE-DRAFTING-WORKFLOW.md, for client sidecars
server/SERVER-SLICE-DRAFTING-WORKFLOW.md, for server/backend/API slices
local-global-documentation-sync-workflow.md
status-reconciliation-workflow.md, when implementation evidence is involved
reviewable-agent-output-and-commands-workflow.md
```

### Current implementation / status reconciliation

Trigger examples:

```text
old implemented slice draft looks stale;
implemented code/tests exist but draft is missing current structure/status;
draft, source, code and tests may disagree;
planning docs claim implemented/current state.
```

Activated workflows:

```text
planning-agent-protocol.md
agent-roles-and-required-actions.md
workflow-activation-map.md
planning-use-case-map.md, for traversal/source decision if a response command is used
status-reconciliation-workflow.md
local-global-documentation-sync-workflow.md
reviewable-agent-output-and-commands-workflow.md
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
- use planning-use-case-map.md to decide action trace/traversal/read source mode;
- use Level 1/2/3 answer format;
- use recheck/clarify/keep-prev/no-ch/use-archive/active-context style response commands.
```

## 8. Future / Missing Workflows

Do not pretend these are implemented if no workflow file exists.

| Topic | Current status | When to revisit |
|---|---|---|
| Source/version cascade sync | Principle exists in `planning/documentation/planning-docs-architecture-principles.md`, but full workflow/register system does not exist yet. | After local responsibility maps and at least one source usage register pilot. |
| Scenario/domain local responsibility maps | Documentation and slice local maps exist. Scenario/domain local maps are future. | During scenario/domain layer refactors. |
| Root map shrink to thin router | Root map is transitional and intentionally detailed. | After local responsibility maps exist for major layers. |
| Server/client/cross-cutting drafting workflow split | Root slice authoring workflow exists; server/client local workflows exist; cross-cutting workflow is deferred. | After section-level source/source-version model stabilizes or real draft refactors prove repeated side-specific algorithm steps. |
| Slice test plan workflow split | Unified slice test workflow exists. | After testing layer audit. |
| Reviewable output vs response commands split | Output templates and response commands currently live in one workflow. | After response-command vocabulary stabilizes or if the combined file becomes too large/confusing. |

Longer-term reminders live in:

```text
planning/planning-maintenance-register.md
```

## 9. Do Not

```text
- Do not skip Workflow Preflight for non-trivial repo/planning work.
- Do not hide activated workflows from the user.
- Do not treat response-format/response-command workflows as permission to edit files.
- Do not use GitHub mutation tools unless the user explicitly requested repository changes.
- Do not pretend future/missing workflows already exist.
- Do not activate only the top-level workflow when a nested workflow is clearly required.
- Do not apply a workflow silently when its activation would change scope or require user approval.
- Do not treat `no ch` as permission to skip targeted checks required before writes, deletes, renames or current-state claims.
- Do not treat `use archive` as remote/current proof when archive freshness is uncertain.
- Do not treat `planning-use-case-map.md` as a replacement for specialized workflows/templates.
```

## 10. Success Criteria

Workflow activation is working when:

```text
- the user can see which workflows are being used before work continues;
- all relevant activated workflows are mentioned;
- implicit checks and explicit permission-required actions are separated;
- future/missing workflows are called out honestly;
- response-level commands are discoverable from this map;
- action/use-case trace is discoverable through planning-use-case-map.md;
- the user can correct workflow choice before edits or deep work;
- new workflows can be added to this map without rewriting every prompt.
```
