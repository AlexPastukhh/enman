# Tampermonkey Command Helper Reusable Tool

Status: active reusable documentation-layer full tool
Doc version: v0.2.1
Scope: reusable full Tampermonkey/ChatGPT command helper implementation and adaptation notes for projects using the reusable documentation layer

Use with:

```text
planning/documentation/tampermonkey-command-projection-workflow.md
planning/documentation/command-creation-workflow.md
planning/documentation/field-kits/root-use-case-map-field-kit.md
```

## 1. Purpose

This folder owns the reusable Tampermonkey helper implementation shipped with the reusable documentation layer.

The full helper userscript lives here:

```text
planning/documentation/tools/tampermonkey/chat-command-palette.user.js
```

There should not be a second project-local tracked helper copy while this reusable-only model is active.

## 2. Authority Boundary

```text
Tampermonkey is projection only, not authority.
```

The helper inserts command bodies and improves command recall. It does not define command meaning.

Command semantics must come from:

```text
planning/planning-use-case-map.md
planning/documentation/command-creation-workflow.md
planning/documentation/tampermonkey-command-projection-workflow.md
other linked command owner workflows/examples
```

## 3. Included Full Helper

The included userscript provides:

```text
- draggable helper panel;
- command search/list;
- one-click insertion into ChatGPT prompt textarea/contenteditable;
- button labels rendered as <englishName> · <label>;
- full inserted command bodies with command, english_name, command_family, source_of_truth, route_read_rule, key_reminders and user_target;
- no repo writes, network calls, commits or pushes.
```

## 4. Adaptation Rule For Another Project

A project copying `planning/documentation/` can use this tracked full helper directly as the reusable command-helper UI projection.

Before enabling or adapting the reusable helper for a target project, verify:

```text
1. The project root UCM exists.
2. Each command in COMMANDS exists in the project root UCM or is being created in the same approved batch.
3. Commands that do not apply to the target project are removed.
4. source_of_truth points to the target project's real route/owner docs.
5. @name and @namespace are adapted only if the project intentionally forks or rebrands the reusable helper.
6. The helper remains projection-only.
7. No second tracked project-local helper copy is created by default.
```

## 5. Do Not

```text
- Do not create a second tracked local helper copy by default.
- Do not treat this helper as command authority.
- Do not add project-only command semantics here without a UCM route.
- Do not keep both the reusable full helper and a tracked local helper fork as competing authorities.
- Do not use the helper to write to the repo or perform network calls.
```
