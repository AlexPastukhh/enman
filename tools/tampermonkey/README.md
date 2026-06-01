# Tampermonkey Chat Command Helper

Status: current Tampermonkey helper implementation entrypoint  
Doc version: v0.1.0  
Scope: implementation documentation entrypoint for the Enman Chat Command Helper; not the userscript source code

## 1. Purpose

The Tampermonkey Chat Command Helper should help the user insert structured command prompts into ChatGPT.

It should make short commands safer in long chats by adding compact reminders about:

```text
- command family;
- source of truth;
- route-read rule;
- key boundaries;
- user target/context.
```

The helper does not define command semantics. It only projects existing repository command routes into editable prompt bodies.

## 2. Current Implementation Documentation

Start here:

```text
tools/tampermonkey/README.md
```

Detailed implementation notes live here:

```text
tools/tampermonkey/IMPLEMENTATION-NOTES.md
```

Planning source:

```text
planning/workstreams/tampermonkey-command-projection-plan.md
```

Command source of truth:

```text
planning/planning-use-case-map.md
```

## 3. MVP Behavior

MVP helper behavior:

```text
1. User opens command palette.
2. User selects command profile.
3. Helper renders a preview of the command body.
4. User edits target/context.
5. Helper inserts the prompt into the ChatGPT input.
6. Helper does not auto-send by default.
```

## 4. Hard Boundaries

```text
- The helper does not browse the repo.
- The helper does not verify source freshness.
- The helper does not edit repository files.
- The helper does not create commits or push.
- The helper does not become source of truth.
- The helper only inserts a better prompt for the chat to execute.
```

## 5. Implementation Status

```text
current:
  implementation docs infrastructure

next:
  convert documented command bodies into userscript profile data

later:
  build command palette UI
  add preview/edit before insert
  insert prompt into ChatGPT input
  manual browser test
```

## 6. Manual Test Checklist Placeholder

```text
- Command palette opens.
- MVP-1 command can be selected.
- Preview shows compact command body.
- User target/context can be edited.
- Prompt inserts into ChatGPT input.
- Prompt is not auto-sent.
- Inserted body includes source_of_truth.
- Inserted body includes route_read_rule.
- Inserted body includes compact key_reminders.
```
