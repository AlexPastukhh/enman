# Replacement File Generation Guide

Status: current replacement package guide  
Scope: how to generate archives/files for manual application to repository

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
```

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

Each archive should contain:

```text
MANIFEST.md
APPLY.md
repository-relative replacement/add files
```

`MANIFEST.md` should list:

```text
Add
Replace
Delete
```

`APPLY.md` should include the Windows PowerShell apply command:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\<archive-name>.zip" -DestinationPath . -Force
git status
```

## 8. Scope Statement

Every final response with an archive should state:

```text
- what the archive changes;
- what it deliberately does not change;
- next step after applying;
- any blocking questions.
```

## 9. Status Reconciliation Rule

When an archive updates planning docs after implementation changes, use:

```text
planning/documentation/status-reconciliation-workflow.md
```

Do not leave docs saying `planned` when current repo evidence shows `implemented` or `first-stage implemented`.

## 10. Do Not

```text
- Do not mix workflow cleanup with code implementation.
- Do not create .client.md sidecars unless concrete client work starts.
- Do not migrate behavior items inside unrelated archives.
- Do not change API/domain/test behavior in documentation-only archives.
- Do not generate partial snippets when full replacement files are expected.
- Do not directly change GitHub when the user asked for an archive.
```
