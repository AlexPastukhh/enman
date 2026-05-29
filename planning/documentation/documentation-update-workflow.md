# Documentation Update Workflow

Status: current documentation-only workflow  
Scope: how to update planning docs accurately without touching code or generated artifacts

## 1. Core Principle

Documentation updates must be repo-grounded, plan-first and scope-controlled.

A documentation update is not a place to implement behavior.

For broad docs/navigation/status/register changes, prepare a `Documentation Update Plan` first:

```text
planning/documentation/documentation-update-plan-workflow.md
```

After the plan is reviewed, use the output mode explicitly requested or approved by the user:

```text
direct repository edits
replacement archive/package
local targeted script edit
patch proposal only
plan only
```

## 2. Workflow

```text
1. Read central navigation and responsibility docs.
2. Read documentation update, planning architecture and local/global sync workflow docs.
3. Read the current docs for the requested area.
4. Inspect current code/artifacts only enough to avoid stale status.
5. Identify doc drift:
   - implemented but documented as planned;
   - planned but documented as implemented;
   - moved file/path not reflected in navigation;
   - local file carrying global workflow rules;
   - local question not mirrored in shared register;
   - shared register stale compared to local file;
   - missing responsibility owner;
   - docs still assuming archive-only output when direct repository edits are approved.
6. Decide update scope.
7. Classify every target file by delivery safety before choosing archive/script/direct edit mode.
8. Prepare a Documentation Update Plan when the change is broad or multi-file.
9. Ask only blocking questions that can change the planned update.
10. After approval, apply the selected output mode.
11. Check whether the documentation action log needs an entry.
12. Final response includes changed files or archive/script link, scope, non-goals, commit SHAs when applicable and next step.
```

## 2A. Target File Delivery Safety Check

Before generating an archive, script or direct edit plan, classify each target file.

Use this classification:

```text
safe complete replacement/archive
  The file is small or medium enough to safely produce as a complete replacement file.

one-file targeted script
  The file is large, shared, register-like, route-table-like or fragile, and only a small anchor-based edit is needed.

direct repository edit
  The user explicitly approved direct repository changes and the tool/mode can safely replace the complete file or perform the requested operation.

no change
  The file is relevant to the plan but intentionally left untouched.
```

Rules:

```text
- Do this classification during planning, before creating artifacts.
- Do not let one large/risky file block safe archive delivery for other files.
- Do not put large/shared files into a replacement archive unless a complete replacement can be produced safely.
- Do not use one write script to modify multiple large/shared files.
- If several large/shared files need targeted edits, create one targeted write script per large/shared file.
- A final combined diff command may include all files for review, but write scripts should stay one large/shared file at a time.
```

## 3. Required Current-State Check

Before changing docs, check whether the relevant implementation/status changed.

Examples:

```text
- If updating CC-API docs, check generated OpenAPI command, Shared/openapi.json and generated openapi-types.ts.
- If updating CC-CONST docs, check Tools generator/checker and Shared/constants.json / Shared/errorcodes.json.
- If updating E2E docs, check playwright.config.ts, tests/e2e and package scripts.
- If updating L1 slice docs, check L1 controller, DTOs, commands, handlers and integration tests.
```

Do not trust old archive assumptions when current repo has changed.

## 4. Local / Global Synchronization Check

Before applying or finalizing a documentation update, check whether local changes must be reflected globally.

Use:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

Typical sync targets:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

If a local question remains local only, record why.

If a shared register row is stale, update or supersede it.

## 5. Status Labels

Use clear status language:

```text
implemented
first-stage implemented
partially implemented
implementation-ready draft
planned
deferred
future review
open question
accepted direction
resolved
superseded
historical/internal
non-canonical
```

Avoid ambiguous status like “done” unless the scope is very small and exact.

## 6. Navigation Update Rule

Whenever adding, moving or superseding planning docs, update relevant navigation:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
planning/planning-agent-protocol.md, if workflow behavior changed
ADR notes/candidates, if accepted architecture decisions changed
```

Do not leave orphan docs.

## 7. Responsibility / Placement Rule

Before placing new information, choose the layer and then use the local responsibility map for that layer.

For root layer routing, use:

```text
planning/planning-doc-responsibility-map.md
```

For documentation-layer placement, use:

```text
planning/documentation/documentation-responsibility-map.md
```

Do not duplicate full placement tables in this workflow. This workflow describes the update process; responsibility maps own placement decisions.

## 8. Question Ordering Rule

In any `Questions / Decisions` section, important open questions and unresolved risks come before accepted decisions.

This applies to:

```text
slice files
client sidecars
cross-cutting/helper slices
scenario clarifications
status reconciliation docs
ADR candidate notes
documentation update plans
```

Accepted decisions are still recorded, but they must not hide unresolved questions below them.

## 9. Blocking Questions Rule

Ask only questions that can change this documentation update.

For every question include an assumption.

Non-blocking questions should be recorded as future review items or mirrored into the relevant register instead of stopping the update.

## 10. Output Mode Rule

Default behavior is plan-first.

After the plan is reviewed, use the output mode requested or approved by the user.

### Direct repository edit mode

Use direct repository edits only when the user explicitly asks to apply changes to the repository.

Rules:

```text
- keep the approved scope;
- use specific commit messages;
- do not combine unrelated documentation refactors;
- do not change code or generated artifacts unless explicitly in scope;
- report changed files and commit SHAs after applying;
- choose commit mode before writing.
```

Commit mode rule:

```text
Use one file per commit by default for semantic documentation edits where each file has its own reviewable meaning.

Use one bundled/bulk commit for mechanical multi-file link/path/name synchronization or shallow navigation routing when:
- every edited file participates in the same logical sync;
- the change is shallow and does not require independent semantic review per file;
- no unrelated semantic refactors are mixed in;
- the user explicitly approves bundled commits or bulk update mode;
- the final response lists all changed files and the shared commit SHA.

If the current tool mode cannot create one bundled commit, stop before writing and disclose the limitation.
Offer either:
- continue with per-file commits;
- create a replacement/archive package for manual one-commit application;
- use a bulk-capable Git tree/commit workflow if available.
```

### Direct GitHub bulk commit mode

Use direct GitHub bulk commit mode for approved shallow mechanical multi-file sync when the available tools support:

```text
create_tree
create_commit
update_ref with force=false
```

Before using this mode, the assistant must output the commands the user can run locally to get the current base commit SHA and current base tree SHA for the target remote branch.

PowerShell-safe commands:

```powershell
git fetch origin
git rev-parse origin/<target-branch>
git show -s --format=%T origin/<target-branch>
```

For the current branch example:

```powershell
git fetch origin
git rev-parse origin/my-changes
git show -s --format=%T origin/my-changes
```

Meanings:

```text
git rev-parse origin/<target-branch>
  returns the base commit SHA.

git show -s --format=%T origin/<target-branch>
  returns the base tree SHA.
```

Do not ask the user to use `git rev-parse origin/<branch>^{tree}` in PowerShell. It may be parsed incorrectly. Prefer `git show -s --format=%T origin/<branch>`.

Required inputs before write:

```text
- target repository;
- target branch;
- base commit SHA;
- base tree SHA;
- commit message;
- explicit approval to move the target branch through direct GitHub bulk commit mode.
```

Apply sequence:

```text
1. Create one tree using base tree SHA and all changed files.
2. Create one commit using parent = base commit SHA.
3. Update the branch ref to the new commit with force=false.
4. Report changed files and the shared commit SHA.
```

Safety:

```text
- Do not hardcode base commit/tree SHA values in docs; they are one-use current-state values.
- The user or assistant must refresh the values after every branch movement.
- Use force=false for update_ref.
- If update_ref fails because the branch moved, stop and rebuild from the new HEAD.
- If base tree SHA cannot be obtained or verified, use archive/local bulk commit or ask the user for the current values.
- Do not fall back to per-file commits unless the user explicitly accepts per-file commits after the limitation is disclosed.
```

### Archive / replacement package mode

Use archive mode when direct repo edits are not requested, when direct GitHub bulk commit mode is unavailable, or when a broad generated package is easier to review manually.

Rules:

```text
- include complete replacement/add files;
- include MANIFEST.md and APPLY.md;
- do not include code changes in documentation-only archives;
- follow planning/replacement-file-generation-guide.md.
```

### Local Targeted Script Edit mode

Use local targeted script edit mode when a large/shared file needs a small, explicit, anchor-based edit and complete replacement is unsafe or too heavy for the current delivery mode.

Typical targets:

```text
- large planning registers;
- shared route/use-case maps;
- long workflow files when only one bounded section changes;
- files where complete replacement would risk dropping unrelated entries;
- files where previous archive/script attempts showed anchor or formatting fragility.
```

Rules:

```text
- Use only after the user approves script mode or when it is the explicitly selected delivery mode.
- One large/shared file = one targeted write script.
- Do not use one write script to modify multiple large/shared files.
- A script may create a final combined diff command for many files, but write operations should remain one large/shared file at a time.
- Do not commit from the script.
- Do not stage files from the script, except `git add -N` for expected new files when making untracked files visible in diff.
- Do not print full diffs to the terminal.
- Save scoped diff with `git --no-pager diff --no-color --output="$diffFile" -- $files`.
- Copy the saved diff with `Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard`.
- Give commit commands only after the pasted diff is reviewed.
```

Required script structure:

```text
1. Preflight:
   - read the target file;
   - verify all anchors/probes;
   - verify the change is not already applied;
   - verify the file is not in an unexpected partial state;
   - stop before writing if any required anchor is missing.

2. Build:
   - compute the new content in memory;
   - use robust heading/line anchors where possible;
   - avoid long exact blocks when a shorter stable heading/line anchor exists.

3. Write:
   - write the target file only after preflight/build succeed;
   - write as UTF-8 without BOM for repo markdown unless Windows PowerShell script encoding requires otherwise.

4. Review:
   - run `git status --short -- $files`;
   - run `git diff --stat -- $files`;
   - write full diff to a file;
   - copy full diff to clipboard;
   - ask the user to paste the diff before commit.
```

PowerShell delivery rules:

```text
- Provide a downloadable `.ps1` file when the script is long, contains here-strings, regex, backticks, Cyrillic or multi-step logic.
- In chat, provide only the copy-to-root and run commands unless the user asks to see the full script.
- Save `.ps1` with UTF-8 BOM when Windows PowerShell 5.1 must parse Cyrillic source text.
- Avoid inline PowerShell one-liners for regex/backticks/Cyrillic edits.
```

## 11. Documentation Action Log Check

Before finalizing a documentation update, decide whether the update is a significant logical documentation action.

Update:

```text
planning/documentation/documentation-action-log.md
```

when the change affects:

```text
- architecture principles;
- workflow behavior;
- accepted command semantics;
- source-of-truth boundaries;
- source usage/cascade governance;
- template or output shape;
- onboarding route;
- PMR/task governance;
- documentation-layer examples infrastructure;
- replacement archive/package behavior;
- local targeted script edit behavior.
```

Do not update it for typo-only edits, minor link fixes or purely mechanical sync already covered by a larger logged action.

The action log records what changed and why. It does not own rules, PMR task state or source truth.

## 12. Documentation Quality Checklist

Before finalizing a documentation update, verify:

```text
- broad changes had a Documentation Update Plan;
- every target file was classified by delivery safety before archive/script/direct edit mode was chosen;
- every added file appears in navigation or a folder README;
- responsibility maps know the new responsibility;
- local questions that matter later are mirrored into shared registers;
- shared register rows are not stale compared to local docs;
- docs do not conflict with current repo status;
- old paths/names are not accidentally reintroduced;
- future questions are not presented as current defects;
- planned features are not overclaimed as implemented;
- selected output mode is explicit;
- direct repository edits use one file per commit for independent semantic edits and bundled/bulk commit for approved shallow mechanical multi-file sync;
- direct GitHub bulk commit mode outputs base commit/tree SHA commands before asking the user for values;
- archive mode contains complete files, not patches;
- local targeted script edit mode uses one write script per large/shared file;
- targeted scripts preflight anchors before writing and do not print full diffs to terminal;
- APPLY.md and MANIFEST.md are present for archive mode;
- significant logical documentation actions are recorded in documentation-action-log.md or explicitly marked as not needing a log entry;
- no code/generated changes are included unless explicitly in scope.
```

## 13. Do Not

```text
- Do not write docs from memory only.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not create full numbered ADRs unless explicitly requested.
- Do not mix OpenAPI/client implementation with documentation-only updates.
- Do not update the repository directly unless explicitly requested.
- Do not hide uncertainty; record assumptions and questions.
- Do not leave important local slice questions only in local tables.
- Do not duplicate full responsibility/placement maps inside workflow files.
- Do not introduce master-chat, work-register or mandatory status-packet workflow unless explicitly requested.
- Do not make replacement archives mandatory for every documentation update.
- Do not bundle unrelated semantic changes into one mechanical link-sync commit.
- Do not start per-file commits for an approved mechanical multi-file sync without first checking whether bundled/bulk commit mode is available.
- Do not attempt direct GitHub bulk commit without current base commit SHA and base tree SHA.
- Do not use PowerShell-unsafe tree commands when asking the user for base tree SHA; prefer `git show -s --format=%T origin/<branch>`.
- Do not use one local targeted write script to modify multiple large/shared files.
- Do not print full diffs to terminal for large/script updates; write diff to file and copy it to clipboard.
```
