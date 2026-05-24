# Documentation Update Plan Workflow

Status: current documentation preflight workflow  
Scope: how to prepare a reviewable plan before changing planning/documentation files

## 1. Purpose

Documentation updates can easily drift across navigation, responsibility maps, registers, status wording and source-of-truth rules.

Before changing planning docs, the chat should prepare a `Documentation Update Plan` when the change is broad enough to affect discoverability, status, source-of-truth rules or more than one file.

The plan exists for two reasons:

```text
- make the chat check the relevant docs before editing;
- make the intended changes visible and reviewable before files are changed.
```

A documentation update plan is not a replacement for the update itself. It is a preflight step.

## 2. When This Workflow Is Required

Prepare a `Documentation Update Plan` before changing docs when the task touches any of these areas:

```text
planning/README.md
folder README / index files
planning/planning-doc-responsibility-map.md
planning/documentation/* workflow docs
source-of-truth rules
status labels or status reconciliation
navigation/read order
shared registers
scenario/slice/API/testing navigation
VKR/thesis clean wording
multiple planning files in one task
```

Use it especially when the user asks to:

```text
- update planning navigation;
- reconcile docs with current implementation;
- add a new planning workflow;
- change documentation responsibility boundaries;
- clean stale planning terminology;
- add, supersede or reorganize planning docs;
- prepare a documentation update for another chat to apply.
```

## 3. When This Workflow Is Not Required

A full documentation update plan is usually not required for:

```text
- a small typo fix in one local doc;
- a read-only explanation;
- a draft discussion where no files will be changed;
- a narrow wording update that does not affect navigation, source-of-truth rules or registers.
```

Even for small edits, keep the scope explicit.

## 4. Relationship To Output Modes

This workflow is output-mode neutral.

After the plan is reviewed, the user may choose one of these output modes:

```text
- direct GitHub edits;
- replacement archive/package;
- patch proposal only;
- plan only.
```

For small scoped documentation changes, direct GitHub edits are allowed when the user explicitly asks to apply them. By default, use one file per commit.

For broad generated replacements or manual application, use replacement archives and include the required manifest/apply instructions.

Do not use GitHub mutation tools unless the user explicitly asks to apply changes to the repository.

## 5. Required Documentation Update Plan Format

Use this format for meaningful planning-doc updates.

### 1. Task Understanding

Include:

```text
User request
My understanding
Goal of this documentation update
Non-goals
```

### 2. Active Role

State the role used for the task, for example:

```text
Documentation Keeper
Status Reconciliation Chat
Architecture Documentation Reviewer
VKR Clean Wording Keeper
```

Explain why that role is appropriate.

### 3. Scope / Out Of Scope

List:

```text
In scope
Out of scope
Explicitly forbidden unless user approves
```

Scope must mention whether code, generated artifacts, GitHub writes, navigation, registers or responsibility maps are included.

### 4. Files Checked

List files actually checked.

Separate:

```text
Core workflow files
Target area files
Repo/code evidence checked, if needed
Files not checked and why
```

Do not pretend a file was checked if it was not.

### 5. Current State Findings

Use a compact table when helpful:

| Area | Current evidence | Current status | Problem / drift |
|---|---|---|---|

Focus on drift, missing navigation, stale status, conflicting source-of-truth rules and docs that no longer match the current update model.

### 6. Source-of-Truth Classification

Classify the sources for each relevant topic:

| Topic | Source of truth | Supporting docs | Stale / non-canonical docs |
|---|---|---|---|

Remember:

```text
Implementation truth comes from current branch/code/tests/migrations/generated contracts/runtime evidence.
Scenario behavior comes from scenario specs and behavior items.
Slice docs describe scope and intended work, not implementation proof.
Dirty drafts are recovery/context notes only.
```

### 7. Proposed File Changes

Use this table:

| File | Action | Reason | Type of change | Risk |
|---|---|---|---|---|

Allowed actions:

```text
Add
Replace
Update section
Delete
Move
Mark deprecated
No change
Needs user decision
```

Do not hide semantic changes behind vague wording such as “polish”.

### 8. Navigation / Register Sync

Explicitly state whether updates are needed for:

```text
planning/README.md
folder README.md files
planning/planning-doc-responsibility-map.md
shared slice/scenario/ADR registers
planning-agent-protocol or workflow docs
```

If no sync is needed, say why.

### 9. Questions / Assumptions

Separate:

```text
Blocking questions
Assumptions if no answer
Non-blocking future notes
```

Ask only questions that can change the planned update.

### 10. Safety Checks

State what the update will not do.

Typical checks:

```text
- will not change code;
- will not change generated artifacts;
- will not silently change scenario/domain/API/testing meaning;
- will not treat dirty drafts as source of truth;
- will not overclaim planned work as implemented;
- will not rewrite unrelated docs;
- will not create GitHub commits unless explicitly approved.
```

### 11. Planned Output

State the output mode and file list.

For direct GitHub edits, include proposed commit messages.

For archives, include expected archive contents such as:

```text
MANIFEST.md
APPLY.md
repo-relative replacement/add files
```

### 12. Verification After Update

List checks to run after changes, for example:

```text
- navigation links point to the new files;
- responsibility map includes new responsibilities;
- docs do not contradict source-of-truth rules;
- status language is synchronized;
- replacement archive rules remain available when needed;
- direct GitHub edit rules are not applied without explicit user approval;
- no master-chat/work-register/status-packet requirement was introduced accidentally.
```

## 6. File Action Types

Use these meanings consistently.

| Action | Meaning |
|---|---|
| Add | Create a new file. |
| Replace | Replace the entire file. Use only when safer than patching sections. |
| Update section | Change a specific section while preserving the rest. |
| Delete | Remove a file. Requires explicit user approval. |
| Move | Rename or relocate a file. Requires explicit user approval because links may break. |
| Mark deprecated | Keep the file but add clear historical/superseded warning. |
| No change | File was considered but should remain unchanged. |
| Needs user decision | The plan cannot safely choose without user input. |

## 7. Safety Rules

Documentation updates must stay inside their approved scope.

Do not:

```text
- change application code;
- change generated artifacts;
- silently change scenario behavior;
- silently change API, domain, security or testing meaning;
- treat dirty drafts or archives as current truth;
- overclaim planned work as implemented;
- leave new docs orphaned from navigation;
- update one local file when a shared register/navigation update is required;
- create GitHub commits unless the user explicitly requested repository changes.
```

If a documentation issue reveals a real implementation/API/domain question, record it as a question or future review item instead of silently changing behavior.

## 8. Direct GitHub Edit Rules

When the user explicitly asks to apply the documentation update to the repository:

```text
- keep the approved scope;
- use one file per commit by default;
- use specific commit messages;
- do not combine unrelated documentation refactors;
- do not include code or generated artifact changes unless explicitly in scope;
- report the changed files and commit SHAs after applying.
```

This makes every documentation change easy to inspect and easy to revert.

## 9. Archive / Replacement Package Rules

Replacement archives are still valid when direct GitHub edits are not desired or when a broad generated package is easier to review manually.

When using archive mode, follow:

```text
planning/replacement-file-generation-guide.md
```

Archive mode should include complete files, not partial patches, and must include clear apply instructions.

## 10. Do Not

```text
- Do not skip the plan for broad docs/navigation/status/register changes.
- Do not update docs from memory only.
- Do not confuse current implementation with target direction.
- Do not make dirty drafts canonical by copying them directly.
- Do not introduce master-chat, work-register or mandatory status-packet workflow unless explicitly requested.
- Do not make replacement archives mandatory for all documentation updates.
- Do not make direct GitHub edits without explicit user approval.
```
