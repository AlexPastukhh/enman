# Tampermonkey Command Projection Workflow

Status: active reusable documentation-layer workflow
Doc version: v0.1.0
Scope: reusable rules for projecting accepted command routes into a Tampermonkey/ChatGPT command helper UI without making the helper the command authority

## 1. Purpose

Use this workflow when a project using the reusable documentation layer wants a Tampermonkey helper or similar UI to insert structured command prompts into ChatGPT.

Tampermonkey projection exists to improve command recall and prompt-body consistency. It does not define command meaning.

## 2. Authority Split

| Owner | Owns |
|---|---|
| Project root use-case map | Concrete command route, accepted aliases, traversal/read mode, owner files, expected output and permission boundary. |
| Command owner workflow/template | Reusable behavior and output rules for the command family. |
| This workflow | Reusable rules for projecting commands into a helper UI. |
| Project-specific tools/tampermonkey files | Local implementation and command-profile instances for one project. |
| Project-specific workstream plan | Local application/projection backlog and smoke-test state. |

## 3. Core Rule

```text
Tampermonkey is projection only, not authority.
```

If a Tampermonkey profile conflicts with the project root use-case map or owner workflow, the root use-case map / owner workflow wins.

Do not create Tampermonkey command before UCM route exists. Do not create a helper profile for a command before the command route exists in the project root use-case map or before the user explicitly asks to draft the route and projection together.

## 4. Required Command Profile Shape

Every projected command profile should have:

```text
id
  Stable machine id, e.g. replacement_archive.create.

group
  UI grouping only. Does not define command behavior.

label
  The short user-facing command phrase, usually the Russian/compact command.

description
  Short helper description.

body
  Full inserted [ENMAN_COMMAND] body.

englishName
  Neutral English display/readability name.
```

Button label rule:

```text
<englishName> · <label>
```

Examples:

```text
give arch · давай архив
gm brief · кц
polozh · положняк
create command · создай команду
start parallel work · начни параллельную работу
```

English names are display/readability names only. Do not mark Russian as primary or English as secondary inside the reusable rule.

## 5. Inserted Command Body Contract

Every inserted command body should use this structure:

```text
[ENMAN_COMMAND]
Read this whole command body before answering.
Do not ignore `key_reminders`.

command:
  <canonical command>

english_name:
  <neutral short English display name>

command_family:
  `<alias 1>` / `<alias 2>` / `<English alias>`

source_of_truth:
  Start from `planning/planning-use-case-map.md`.
  Then read the owner / linked files for this command route.

route_read_rule:
  If you have not read this command route and its linked owner/example files in this chat, read them before answering.
  If you have read them but do not remember the required behavior, boundaries or key points, reread from `planning/planning-use-case-map.md` before answering.
  Do not rely only on this prompt when command behavior is uncertain.

key_reminders:
  - <command-specific source/permission/output reminders>

user_target:
  <placeholder>

[/ENMAN_COMMAND]
```

## 6. Source Of Truth Requirements

A profile's `source_of_truth` block must start with the project root UCM:

```text
planning/planning-use-case-map.md
```

Then link command-specific owners, for example:

```text
planning/documentation/command-creation-workflow.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/parallel-work/parallel-workflow.md
planning/replacement-file-generation-guide.md
```

Project-specific implementation files may be named in local projection docs, but do not make them semantic owners.

## 7. Common Reusable Projection Checks

Before adding/updating a helper command profile, verify:

```text
1. The command route exists or is being created in the same approved batch.
2. The command's source_of_truth points to the UCM and real owner docs.
3. key_reminders include output-mode and permission boundaries.
4. englishName and english_name match unless the local project intentionally uses different display wording.
5. The helper label renders as <englishName> · <label>.
6. The helper does not add hidden behavior that is absent from the route.
7. The project-specific tool README says the helper is projection only.
```

## 8. Project-Specific Tool Placement

A project may place implementation under:

```text
tools/tampermonkey/
```

Common local files:

```text
tools/tampermonkey/README.md
tools/tampermonkey/IMPLEMENTATION-NOTES.md
tools/tampermonkey/chat-command-palette.user.js
```

Reusable starter/tool docs may be copied from:

```text
planning/documentation/tools/tampermonkey/
```

After copying, adapt project names, repository URLs, command list and source paths.

## 9. Do Not

```text
- Do not create Tampermonkey command semantics without a UCM route.
- Do not treat the userscript as a command source of truth.
- Do not put project-specific command profiles into this reusable workflow.
- Do not copy Enman workstream history into a new project's reusable docs.
- Do not silently change command meaning while adding UI labels.
- Do not omit key_reminders from inserted command bodies.
- Do not omit english_name when the helper exposes englishName.
```
