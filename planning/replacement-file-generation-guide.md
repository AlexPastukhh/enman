# Replacement File Generation Guide

Status: current replacement package guide  
Scope: how to generate archives/files for manual application to repository

## 0. Quick Rule For New Chats

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
- no patch scripts, diff-only files or generated apply scripts.
```

If complete replacement files cannot be produced safely, stop and say so. Do not silently switch to patch-script mode.

## 1. Purpose

Use this guide when the user asks to create an archive or replacement package for the repository.

## 2. Core Rules

```text
- Generate complete replacement files, not patches.
- Preserve repository-relative paths.
- Include MANIFEST.md.
- Include APPLY.md.
- Do not write directly to the repository unless explicitly asked.
- Do not create branches, commits, PRs or GitHub comments unless explicitly asked.
- Do not include unrelated implementation changes.
- Keep archive scope focused.
- Do not include patch scripts or script-based patch applicators in replacement archive mode.
```

Patch scripts, unified diffs, generated apply scripts and partial snippets are **not** replacement files.

If the intended output is a patch or script, label the mode as:

```text
patch proposal only
```

Do not call it a replacement archive/package.

## 3. GitHub Mutation Rule

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

## 5. Responsibility Rule

Before deciding where a replacement file belongs, check:

```text
planning/planning-doc-responsibility-map.md
```

Do not put global workflow/common rules into local scenario/slice/client files.

## 6. Relevant Questions Rule

Before generating an archive, ask only questions that can change the archive contents.

For every question, include the current assumption/preferred answer.

If the question is future-only and does not affect the archive, record it elsewhere or mention it as non-blocking.

## 7. Archive Contents

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

Replacement archive verification has two parts:

```text
Applied
  = intended files changed.

Preserved
  = unrelated information was not lost, removed or overwritten.
```

Every APPLY.md should tell the user to review both application and preservation before committing.

Include commands like:

```powershell
git status --short

git diff --stat -- `
  <changed-file-1> `
  <changed-file-2>

git --no-pager diff --no-color -- `
  <changed-file-1> `
  <changed-file-2>
```

The post-apply review should check:

```text
- only intended files changed;
- diff matches package intent;
- no unrelated sections, register entries, commands, examples, routing rows or source-of-truth rules were removed;
- shared-state files such as registers preserve existing entries unless removal was explicit;
- commit commands add only intended files and never use `git add .`.
```

If the user asks `проверь` after applying a replacement archive, treat it as this post-apply preservation check, not only as a file-name/status check.

## 7B. Diff Capture And Clipboard Commands

When the assistant provides a replacement archive, the chat response should include ready-to-run PowerShell commands for post-apply review, not only instructions hidden inside `APPLY.md`.

The command block should avoid paged terminal diff output and should avoid PowerShell pipeline encoding problems with Cyrillic text.

It must also make expected new files visible in the review diff. Untracked files are not shown by `git diff` by default.

Required shape:

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
    Write-Host "Missing expected new file: $file"
  }
}

$pkgName = "real-package-name-without-angle-brackets"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files

git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Rules:

```text
- Use a real package name in `$pkgName`; do not leave placeholders such as `<ARCHIVE_NAME>` in runnable commands.
- `$files` must include all expected changed and added files.
- `$newFiles` must include every expected added file; use `$newFiles = @()` when the package does not add files.
- Run `git add -N -- <new-file>` before generating the review diff for packages that add files.
- `git add -N` is used only to make untracked files visible in `git diff`; the final commit command must still explicitly stage only intended files.
- If an expected new file is missing, treat that as a review blocker until resolved.
- Use `git --no-pager diff --no-color --output="$diffFile" -- $files` to write the diff file.
- Copy the saved diff with `Get-Content -Raw -Encoding UTF8 | Set-Clipboard`.
- Do not rely on `git diff | Tee-Object | Set-Clipboard` for reviewable diff transfer when non-ASCII text may be present.
- Do not ask the user to manually scroll through paged diff output.
```

If the pasted diff shows mojibake or suspicious broken Cyrillic, do not conclude from the diff alone that the repository file is corrupted. Ask for or provide a suspect-file content copy command.

Fallback command shape:

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
Get-Content -Path $suspectDump -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Suspect file contents saved to: $suspectDump"
Write-Host "Suspect file contents copied to clipboard. Paste it into chat."
```

## 8. Archive Layouts And Apply Commands

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

If the user explicitly asks for a patch proposal, it may be provided separately, but the final response must label it as patch mode and must not claim it is a replacement archive.

If the target file is large, still generate the complete replacement file. If that is not safe, stop and report the limitation instead of producing a patch-script archive.

## 10. Scope Statement

Every final response with an archive should state:

```text
- what the archive changes;
- what it deliberately does not change;
- next step after applying;
- any blocking questions.
```

When the archive is intended for one local bulk commit, the final response should also include a suggested `git add` and `git commit` command.

## 11. Status Reconciliation Rule

When an archive updates planning docs after implementation changes, use:

```text
planning/documentation/status-reconciliation-workflow.md
```

Do not leave docs saying `planned` when current repo evidence shows `implemented` or `first-stage implemented`.

## 12. Do Not

```text
- Do not mix workflow cleanup with code implementation.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not migrate behavior items inside unrelated archives.
- Do not change API/domain/test behavior in documentation-only archives.
- Do not generate partial snippets when full replacement files are expected.
- Do not generate patch scripts when replacement files are expected.
- Do not directly change GitHub when the user asked for an archive.
- Do not provide archive apply instructions without a pull/current-state step first.
- Do not tell the user to extract a package-layout archive directly into the repo root as the apply step.
```
