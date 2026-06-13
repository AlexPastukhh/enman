# Tampermonkey Command Helper Reusable Tool Notes

Status: active reusable documentation-layer tool starter notes
Doc version: v0.1.0
Scope: portable notes for creating a project-specific Tampermonkey/ChatGPT command helper implementation from the reusable documentation layer

Use with:

```text
planning/documentation/tampermonkey-command-projection-workflow.md
planning/documentation/command-creation-workflow.md
planning/documentation/field-kits/root-use-case-map-field-kit.md
```

## 1. Purpose

These notes are copied with the reusable documentation layer so a new project can create its own helper under a project-local tools folder without depending on Enman-specific workstream files.

Suggested target placement after adaptation:

```text
tools/tampermonkey/README.md
tools/tampermonkey/chat-command-palette.user.js
```

## 2. What To Adapt

When creating the project-local helper:

```text
- set @namespace / repository URL for the target project;
- set @name for the target project;
- seed COMMANDS from the project's root use-case map;
- remove commands that do not apply to the target project;
- keep englishName and english_name for display/readability;
- keep route_read_rule and key_reminders in inserted bodies;
- preserve the rule that the helper is projection only.
```

## 3. Minimum Implementation Contract

A basic helper needs:

```text
- a COMMANDS array;
- each command profile with id/group/label/description/body/englishName;
- a button/list renderer that displays <englishName> · <label>;
- an insertion function that inserts body text into the active ChatGPT prompt box;
- no hidden side effects;
- no repo writes, network calls, commits or pushes.
```

## 4. Starter Template

A minimal starter template lives at:

```text
planning/documentation/tools/tampermonkey/chat-command-palette.starter.user.js
```

It is intentionally generic and should be copied/adapted into the target project's `tools/tampermonkey/` folder.

## 5. Do Not

```text
- Do not treat this reusable tool note as command authority.
- Do not copy Enman command history as target-project config.
- Do not add project-local commands here; add them to the project root UCM and project-local userscript.
- Do not keep the starter template unadapted if the project has real command routes.
```
