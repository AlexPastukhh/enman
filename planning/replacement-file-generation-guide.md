# Replacement File Generation Guide

Status: current replacement package guide
Doc version: v0.1.0
Scope: how to generate archives/files for manual application to repository

## 0. Quick Rule For New Chats

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

When the user asks for an archive, replacement package or files to apply locally, use this guide before generating the archive.

Default meaning:

```text
"давай архив" / "собери архив" / "replacement package" / "archive for manual apply"
  = output mode: replacement archive/package.
```

This is different from archive read-source commands:

```text
"арх" / "из архива" / "use archive"
  = read source mode: use an uploaded archive snapshot for analysis.
```

If both apply, state both explicitly:

```text
Read source mode:
- archive snapshot / GitHub / conversation

Output mode:
- replacement archive/package
```

Replacement archive/package mode means:

```text
- complete replacement/add files;
- repository-relative paths under replacement-files/;
- MANIFEST.md;
- APPLY.md;
- no patch scripts, diff-only files or generated apply scripts;
- default post-apply review transfer through a saved diff file copied to clipboard.
```

If complete replacement files cannot be produced safely, stop and say so. Do not silently switch to patch-script mode.

If full current file contents are required for a safe replacement archive and GitHub/repo access cannot provide complete, trustworthy file contents, stop and ask the user for a fresh repo/archive snapshot or full target-file copies. Do not generate a replacement archive from truncated connector output, partial snippets, inferred file tails or stale local assumptions.

A large file is not automatically excluded from replacement archive mode. If a fresh full repo/archive snapshot or current full file makes complete replacement safe and reviewable, prefer complete replacement over a script.

If only some files cannot be produced safely as complete replacements, use hybrid delivery:

```text
safe complete files -> replacement archive/package
each large/shared risky file -> separate Local Targeted Script Edit script
```

Do not hide script artifacts inside replacement archive/package mode.

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Use this guide when the user asks to create an archive or replacement package for the repository.

## 2. Core Rules

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

```text
- Generate complete replacement files, not patches.
- Preserve repository-relative paths.
- Include MANIFEST.md.
- Include APPLY.md.
- Do not write directly to the repository unless explicitly asked.
- Do not create branches, commits, PRs or GitHub comments unless explicitly asked.
- Do not include unrelated implementation changes.
- Keep archive scope focused, but do not artificially split one coherent accepted update into many tiny archives.
- Do not include patch scripts or script-based patch applicators in replacement archive mode.
- Use saved-diff-to-clipboard mode as the default post-apply review transfer.
- Use review-diff-file mode only when the user explicitly requests repo-stored review diff transfer or clipboard diff transfer is impossible and the user approves the switch.
- In review-diff-file mode, the apply command itself may create, commit and push only `_ai-review-diffs/last-archive.diff`.
- Do not create or commit extra review artifacts by default.
- Do not commit or push real archive replacement files before diff review.
- If complete current file contents are required and cannot be obtained completely from GitHub/repo access, ask the user for a fresh archive or full file copies before generating replacement files.
- If a coherent update has safe files and large/risky files, split by delivery safety instead of blocking the safe archive.
```

Patch scripts, unified diffs, generated apply scripts and partial snippets are **not** replacement files.

If the intended output is a patch or script, label the mode as:

```text
patch proposal only
```

or, when it is an approved local targeted edit:

```text
local targeted script edit
```

Do not call it a replacement archive/package.

## 3. GitHub Mutation Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

For archive work, do not use GitHub mutation tools.

Forbidden unless the user explicitly asks for direct GitHub changes:

```text
create_file
update_file
delete_file
create_branch
create_commit
update_ref
create_pull_request
add_comment_to_issue / PR comments
```

The normal deliverable is a local zip archive link.

## 4. Repository Check Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Before generating an archive, check current repository docs when possible.

Use repository connector/API for:

```text
- reading current files;
- checking whether previous archive was applied;
- checking existing file paths;
- avoiding guessed paths;
- checking current implementation status when docs mention status.
```

If repo cannot be checked, say so and generate from known context only.

## 4A. Target File Delivery Safety Check

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Before generating an archive, classify every target file by delivery safety.

Use:

```text
safe complete replacement/archive
  Include as a complete file in replacement-files/.

one-file targeted script
  Exclude from the archive and provide a separate targeted script for that one large/shared file.

no change
  Mention as intentionally not changed when relevant.
```

Rules:

```text
- Do this before package generation.
- Large file alone is not a reason to switch to script mode.
- If a large file must change, first check whether fresh full repo/archive content or the current full file makes complete replacement safe.
- Prefer complete replacement when full current content is available and the diff remains reviewable.
- Do not let one large/risky file block safe archive delivery for other files.
- Do not include partial replacements or script patches for large files inside the archive.
- Use one targeted write script per large/shared file when complete replacement is unsafe.
- A final combined diff command may review all affected files, but write scripts should stay one large/shared file at a time.
```

## 5. Responsibility Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Before deciding where a replacement file belongs, check:

```text
planning/planning-doc-responsibility-map.md
```

Do not put global workflow/common rules into local scenario/slice/client files.

## 6. Relevant Questions Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Before generating an archive, ask only questions that can change the archive contents.

For every question, include the current assumption/preferred answer.

If the question is future-only and does not affect the archive, record it elsewhere or mention it as non-blocking.

## 7. Archive Contents

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Each replacement archive should contain:

```text
MANIFEST.md
APPLY.md
replacement-files/<repository-relative-path>
```

`MANIFEST.md` should list:

```text
Add
Replace
Delete
```

`APPLY.md` should include local repository application commands.

Required order:

```text
1. Pull the current target branch state into the local repository.
2. Apply replacement files from the archive to the local repository root.
3. Check git status and diff.
4. Review and commit locally.
```

## 7A. Post-Apply Preservation Check

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Replacement archive verification has two parts:

```text
Applied
  = intended files changed.

Preserved
  = unrelated information was not lost, removed or overwritten.
```

Every APPLY.md and assistant archive response should tell the user to review both application and preservation before committing real archive files.

Default post-apply review transfer is:

```text
saved-diff-to-clipboard mode
```

Default review artifact path is package-specific and local, for example:

```text
<package-name>.diff
```

The post-apply review should check:

```text
- only intended real files changed;
- all expected new real files appear in the diff, using `git add -N` when needed;
- diff matches package intent;
- no unrelated sections, register entries, commands, examples, routing rows or source-of-truth rules were removed;
- shared-state files such as registers preserve existing entries unless removal was explicit;
- real commit commands add only intended real files and never use `git add .`.
```

If the user asks `проверь` after applying a replacement archive, treat it as this post-apply preservation check. By default, read the pasted/copied diff from chat. If the user explicitly used review-diff-file mode, read `_ai-review-diffs/last-archive.diff` from the active repo/branch.

## 7B. Diff Capture And Clipboard Commands (Default)

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

When the assistant provides a replacement archive, the default chat response should include ready-to-run PowerShell commands that apply the archive, save the scoped diff to a local `.diff` file and copy that diff to the clipboard.

Default mode:

```text
saved-diff-to-clipboard mode
```

All assistant-provided diff commands in this mode must use `git --no-pager diff`. The full diff must be written to a file with `--output`; it must not be printed to the terminal or opened in a pager.

Required response shape:

```text
1. Pull the target branch.
2. Apply archive replacement files.
3. Make expected new files visible using `$newFiles` and `git add -N`.
4. Create a package-specific `.diff` with `git --no-pager diff --no-color --output=...`.
5. Copy the saved diff to clipboard with `[System.IO.File]::ReadAllText(...)`.
6. Ask the user to paste the diff for review.
7. Keep real archive replacement files local and uncommitted until review approval.
```

Required apply/diff command shape:

```powershell
$files = @(
  "path/to/changed-file-1.md",
  "path/to/changed-file-2.md",
  "path/to/new-file.md"
)

$newFiles = @(
  "path/to/new-file.md"
)

# Use @() when the package does not add files.
# Make expected new files visible in git diff without staging their contents.
foreach ($file in $newFiles) {
  if (Test-Path $file) {
    git add -N -- $file
  } else {
    throw "Missing expected new file: $file"
  }
}

$pkgName = "real-package-name-without-angle-brackets"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git --no-pager diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files

$diffText = [System.IO.File]::ReadAllText($diffFile, [System.Text.Encoding]::UTF8)
Set-Clipboard -Value $diffText

git restore --staged -- $files

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Rules:

```text
- Use a real package name and branch in runnable commands; do not leave placeholders in final user-facing commands.
- `$files` must include all expected changed and added real files.
- `$newFiles` must include every expected added real file; use `$newFiles = @()` when the package does not add files.
- Run `git add -N -- <new-file>` before generating the review diff for packages that add files.
- `git add -N` is used only to make untracked files visible in `git diff`.
- If an expected new file is missing, treat that as a review blocker until resolved.
- Use `git --no-pager diff --stat -- $files` for stat output.
- Use `git --no-pager diff --no-color --output="$diffFile" -- $files` to write the diff file.
- Do not ask the user to manually scroll through paged diff output.
- Do not print the full diff to terminal.
- Do not commit or push real archive files before diff review approval.
- Do not run `git diff --cached --check` on generated `.diff` artifacts; run whitespace checks on real files before real commit when needed.
```

Do not rely on `git diff | Tee-Object | Set-Clipboard`, raw `git diff`, or `Get-Content ... | Set-Clipboard` for reviewable diff transfer when non-ASCII text may be present.

If the pasted diff shows mojibake or suspicious broken Cyrillic, do not conclude from the diff alone that the repository file is corrupted. Ask for or provide a suspect-file content copy command.

Suspect-file fallback command shape:

```powershell
$suspectFiles = @(
  "path/to/suspect-file-1.md",
  "path/to/suspect-file-2.md"
)

$pkgName = "real-package-name-without-angle-brackets"
$suspectDump = Join-Path (Get-Location) "$pkgName.suspect-files.txt"

$bundle = foreach ($file in $suspectFiles) {
  "===== $file ====="
  Get-Content -Path $file -Raw -Encoding UTF8
  ""
}

$bundle -join "`r`n" | Set-Content -Path $suspectDump -Encoding UTF8

$suspectText = [System.IO.File]::ReadAllText($suspectDump, [System.Text.Encoding]::UTF8)
Set-Clipboard -Value $suspectText

Write-Host "Suspect file contents saved to: $suspectDump"
Write-Host "Suspect file contents copied to clipboard. Paste it into chat."
```

## 7C. Review Diff File Mode (Explicit Only)

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Review diff file mode is not the default archive review transfer.

Use it only when:

```text
- the user explicitly asks for `review diff file`, repo-stored review diff or similar wording;
- pasted/clipboard diff transfer is too large or impossible and the user approves the switch;
- target repo policy allows committing review artifacts;
- connector/repo access is available for reading review diff files.
```

Owner workflow:

```text
planning/documentation/review-diff-file-workflow.md
```

Explicit review artifact path:

```text
_ai-review-diffs/last-archive.diff
```

The explicit apply command may create, commit and push only this review artifact. Real archive replacement files must remain local and uncommitted until diff review approval.

Do not create or commit `_ai-review-diffs/last-archive-summary.md` by default.

## 7D. Replacement Archive Conversation Review Loop

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Replacement archive work is a review loop between the assistant and the user.

Use this default loop when the assistant creates a replacement archive and the user applies it locally:

```text
1. Assistant creates a replacement archive/package.
2. Assistant response includes:
   - archive link;
   - intended changed files;
   - intended new files, if any;
   - PowerShell apply/diff commands that save the diff to a local file and copy it to clipboard;
   - scoped real commit commands only after review approval.
3. User runs the apply/diff command locally.
4. User pastes the copied diff into chat.
5. Assistant reviews the diff before telling the user to commit real archive files.
6. If the diff is OK, assistant gives scoped `git add`, `git commit` and `git push` commands for real files.
7. User commits and pushes real files.
8. When user says `проверь`, assistant checks the remote branch/files and confirms what landed.
```

Explicit review-diff-file loop:

```text
Use only when the user explicitly requested review-diff-file mode.
The apply command may create and push only `_ai-review-diffs/last-archive.diff`.
The assistant then reads that repo-stored diff before approving the real commit.
```

Diff review must check:

```text
- only intended real files appear in the scoped diff;
- all expected new real files appear in the diff, using `git add -N` when needed;
- no expected file is missing;
- no unrelated sections, register entries, examples, routing rows or source-of-truth rules were removed;
- package intent matches the diff;
- service files such as MANIFEST.md and APPLY.md were not added to the repository unless they are intended repository files;
- final real commit commands stage only intended real files and do not use `git add .`.
```

If the review diff is incomplete:

```text
- do not approve real commit yet;
- explain what is missing;
- provide the exact command needed to regenerate the review diff;
- for missing added files, provide the `$newFiles` / `git add -N` command from §7B or §7C;
- for mojibake in pasted diff mode, provide the suspect-file content copy command from §7B.
```

If the diff is correct:

```text
- state that the diff is complete enough for review;
- summarize what was checked;
- give only scoped real commit commands for the intended files;
- remind not to use `git add .` when unrelated local changes may exist.
```

This loop is not a new output mode. It is the default review procedure for replacement archive/package output mode.

## 7E. Replacement Archive Batch Scope Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Do not artificially split a coherent accepted update into many tiny archives.

Choose archive scope before generating the package:

```text
- include all files needed for the coherent accepted update;
- exclude unrelated files;
- avoid one-file or two-file splitting when the update is already understood;
- avoid repeated apply/diff/check cycles when one coherent archive can be reviewed safely.
```

Prefer one coherent archive when:

```text
- the direction was already discussed and accepted;
- files are logically coupled;
- the same reviewer context is needed for the files;
- splitting would create extra manual apply/diff/commit/recheck cycles without improving safety.
```

Safety comes from complete replacement files, scoped file lists, saved review diffs, preservation checks and scoped commit commands, not from making every archive artificially tiny.

Do not bundle unrelated work just to reduce archive count.

## 7F. Hybrid Archive / Script Delivery Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Use hybrid delivery when one coherent accepted update contains both safe complete-replacement files and large/shared files whose complete replacement is unsafe and therefore need targeted script edits.

Required shape:

```text
Replacement archive/package:
  - safe complete replacement/add files only.

Separate Local Targeted Script Edit file(s):
  - one `.ps1` per large/shared file that needs targeted writes.

Final review:
  - one combined scoped saved diff file may include all affected files.
```

Rules:

```text
- Label the delivery as hybrid archive/script mode.
- Do not include targeted write scripts inside `replacement-files/`.
- Do not call a script artifact a replacement file.
- Do not let one large/risky file block safe archive delivery for other files.
- Do not choose script mode only because a file is large; prefer complete replacement when fresh full content makes it safe.
- Do not use one write script for multiple large/shared files.
- If a large/shared file must be changed and complete replacement is unsafe, give a separate script link, copy-to-root command and run command.
- Commit commands must stage only intended repository files, not package metadata or helper scripts unless intentionally tracked.
```

## 8. Archive Layouts And Apply Commands

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

There are two supported archive layouts.

### Package layout

Use package layout by default for reviewable replacement packages:

```text
MANIFEST.md
APPLY.md
replacement-files/<repository-relative-path>
```

For package layout, do not instruct the user to extract the zip directly into the repository root as the apply step. Direct extraction would create `replacement-files/` instead of replacing repository files.

Use a temporary directory and then copy `replacement-files/*` into the repository root.

PowerShell guardrail:

```text
For Copy-Item, use -Destination. Do not use -DestinationPath.
```

PowerShell shape:

```powershell
# Run from the local repository root.
git fetch origin
git checkout <target-branch>
git pull --ff-only origin <target-branch>

$archive = "C:\Users\alexa\Downloads\<archive-name>.zip"
$tmp = Join-Path $env:TEMP "<archive-name>-apply"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Path $tmp | Out-Null
Expand-Archive -Path $archive -DestinationPath $tmp -Force
Copy-Item -Path (Join-Path $tmp "replacement-files\*") -Destination . -Recurse -Force

git status
```

### Direct-root layout

Use direct-root layout only when the archive is intentionally generated for direct extraction into the repository root:

```text
<repository-relative-path>
<repository-relative-path>
...
```

In direct-root layout, `MANIFEST.md` and `APPLY.md` should not be placed at repository root unless they are intended repository files. Put package metadata somewhere outside the direct-root zip, or use package layout instead.

PowerShell shape:

```powershell
# Run from the local repository root.
git fetch origin
git checkout <target-branch>
git pull --ff-only origin <target-branch>

Expand-Archive -Path "C:\Users\alexa\Downloads\<archive-name>.zip" -DestinationPath . -Force
git status
```

If the target branch is known, replace `<target-branch>` with the branch name, for example:

```powershell
git checkout my-changes
git pull --ff-only origin my-changes
```

The final assistant response that provides the archive should show the correct command for the archive layout actually used, not only hide it inside `APPLY.md`.

## 9. Patch / Script Artifacts Are Not Replacement Packages

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Do not use any of these inside replacement archive mode:

```text
patch-files/
*.patch
*.diff
apply_*.py
apply_*.ps1 that rewrites files by search/replace
APPLY.md that tells the user to run a generated patch script
partial snippets that must be manually inserted
```

These are patch proposal artifacts, not replacement package artifacts.

If the user explicitly asks for a patch proposal or targeted local script, it may be provided separately, but the final response must label it correctly and must not claim it is a replacement archive.

If the target file is large, still generate the complete replacement file when fresh full content makes it safe and reviewable. If that is not safe, exclude that file from the replacement archive and use hybrid archive/script delivery or stop and report the limitation.

## 10. Scope Statement

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

Every final response with an archive should state:

```text
- what the archive changes;
- what it deliberately does not change;
- next step after applying;
- any blocking questions.
```

When the archive is intended for one local bulk commit, the final response should include real `git add`, `git commit` and `git push` commands only after the saved diff has been reviewed and approved.

## 11. Status Reconciliation Rule

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

When an archive updates planning docs after implementation changes, use:

```text
planning/documentation/status-reconciliation-workflow.md
```

Do not leave docs saying `planned` when current repo evidence shows `implemented` or `first-stage implemented`.

## 12. Do Not

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/review-diff-file-workflow.md @ Doc version: v0.1.0
    - planning/documentation/file-update-overview-workflow.md @ Doc version: v0.1.0
  Internal dependencies:
    - Quick Rule For New Chats
  Not checked:
    - review-diff-file explicit mode checked only by its workflow
    - direct GitHub write mode not changed in ROOT-SRC-3A
```

```text
- Do not mix workflow cleanup with code implementation.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not migrate behavior items inside unrelated archives.
- Do not change API/domain/test behavior in documentation-only archives.
- Do not generate partial snippets when full replacement files are expected.
- Do not generate patch scripts when replacement files are expected.
- Do not directly change GitHub when the user asked for an archive.
- Do not provide archive apply instructions without a pull/current-state step first.
- Do not ask for terminal/pager diff review; save the diff to a file and copy it to clipboard by default.
- Do not run `git diff --cached --check` on generated `.diff` artifacts; run checks on real files when needed.
- Do not generate a replacement archive from truncated GitHub/connector output; ask for a fresh archive or full target-file copies when complete files are required and unavailable.
- Do not tell the user to extract a package-layout archive directly into the repo root as the apply step.
- Do not let one large/risky file block safe archive delivery for other files.
- Do not choose script mode only because a file is large.
- Do not use one write script for multiple large/shared files.
```


## Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.8.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Purpose
  Not checked:
    - Goal Map/Tampermonkey and slice source passes outside ROOT-SRC-3A
```

```text
- ROOT-SRC-3A added Doc version and local section-level Sources blocks to this output/archive workflow file.
- ROOT-SRC-3A did not change output semantics or grant edit/commit permission.
```
