# Review Diff File Workflow

Status: active optional archive review workflow
Scope: explicit repo-stored review diff file flow for replacement archive/package application

Use with:

```text
planning/replacement-file-generation-guide.md
planning/planning-use-case-map.md
```

## 1. Purpose

This workflow defines the optional review-diff-file mode for archive review when the default clipboard diff transfer is not desired or not practical.

Default archive review transfer remains:

```text
saved-diff-to-clipboard mode
```

Use this workflow only when the user explicitly asks for repo-stored review diff transfer, for example:

```text
давай архив с review diff file
repo-stored review diff
проверь review diff file
```

Instead of pasting a large diff into chat, the apply command may create and push a repository-stored review artifact:

```text
_ai-review-diffs/last-archive.diff
```

The assistant then reviews that repo-stored diff before giving commands to commit real archive files.

## 2. Core Rule

```text
The apply command may commit and push only `_ai-review-diffs/last-archive.diff`.
Real archive files must remain local and uncommitted until the review diff is approved.
```

Committed review artifact:

```text
_ai-review-diffs/last-archive.diff
```

Do not create or commit extra review artifacts by default.

Do not create or commit:

```text
_ai-review-diffs/last-archive-summary.md
```

unless a separate reviewed rule explicitly reintroduces it.

## 3. Required Apply Flow

The explicit review-diff-file apply command should:

```text
1. pull the current branch;
2. apply the archive replacement files;
3. make expected new files visible with `$newFiles` and `git add -N`;
4. create `_ai-review-diffs/last-archive.diff` using `git --no-pager diff --no-color --output=...`;
5. stage only `_ai-review-diffs/last-archive.diff`;
6. verify no other file is staged;
7. commit and push only the review diff file;
8. tell the user to ask the chat to review the diff file.
```

## 4. New File Visibility

Untracked new files do not appear in `git diff` by default.

Every explicit review-diff-file archive command that expects new files must include:

```powershell
$newFiles = @(
  "path/to/new-file.md"
)

foreach ($file in $newFiles) {
  if (Test-Path $file) {
    git add -N -- $file
  } else {
    throw "Missing expected new file: $file"
  }
}
```

Use `$newFiles = @()` when the archive does not add files.

## 5. Diff Command Rules

Use no-pager diff commands:

```powershell
git --no-pager diff --stat -- $files
git --no-pager diff --no-color --output="$ReviewDiff" -- $files
git --no-pager diff --cached --name-only
```

Do not use:

```text
- raw `git diff --stat -- $files`;
- raw `git diff --cached --name-only`;
- paged diff output;
- full diff printed to terminal.
```

## 6. Review Artifact Commit Rules

The review artifact commit may stage only:

```text
_ai-review-diffs/last-archive.diff
```

Required guard:

```powershell
$ReviewDiff = "_ai-review-diffs/last-archive.diff"
$reviewFiles = @($ReviewDiff)

git restore --staged -- .
git add -- $ReviewDiff

$staged = @(git --no-pager diff --cached --name-only)
foreach ($f in $staged) {
  if ($reviewFiles -notcontains $f) {
    throw "Unexpected staged file. Refusing review-diff commit: $f"
  }
}

if ($staged -notcontains $ReviewDiff) {
  throw "Expected review diff file is not staged: $ReviewDiff"
}
```

Do not run `git diff --cached --check` on review `.diff` artifacts. The `.diff` file intentionally preserves whitespace from the real diff.

Run whitespace checks on real files before the real commit, after the review diff is approved.

## 7. Chat Review Command

After the apply command pushes the review diff, tell the chat:

```text
проверь review diff file
Repo: <owner/repo>
Branch: <branch>
Path: _ai-review-diffs/last-archive.diff
```

The chat should read the diff from the repo and check:

```text
- expected files are present;
- expected new files appear;
- no unrelated files are included;
- package intent matches the diff;
- no old conflicting workflow text remains;
- real files are not committed yet.
```

## 8. Approval Boundary

Before approval:

```text
- do not commit real archive files;
- do not push real archive files;
- do not use `git add .`;
- do not treat a review artifact commit as proof the real archive has landed.
```

After approval, the assistant may provide scoped real-file commit commands.
