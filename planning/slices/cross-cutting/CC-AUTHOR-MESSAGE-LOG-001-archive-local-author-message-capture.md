# CC-AUTHOR-MESSAGE-LOG-001 — Archive-Local Author Message Capture

Status: current workflow rule  
Type: cross-cutting documentation/workflow convention

## 1. Purpose

When a docs/archive task is based on the user's long-form reasoning, preserve raw author wording locally to that archive when it may help future thesis writing.

This is for language/voice extraction, not for normative requirements.

## 2. Rule

Do not use one global project-level raw message log.

Bad:

```text
planning/raw-author-message-log.md
docs/raw-author-message-log.md
```

Reason:

```text
multiple archives may overwrite each other's logs;
raw author notes from unrelated tasks become mixed;
future cleanup becomes harder.
```

Use an archive-local folder instead:

```text
_archive-notes/<archive-slug>/raw-author-message-log.md
_archive-notes/<archive-slug>/derived-decisions.md
```

## 3. File meanings

```text
raw-author-message-log.md
  raw or lightly cleaned user messages;
  not normative;
  only for thesis/writing voice extraction.

derived-decisions.md
  explicit decisions derived from the raw messages;
  may be reflected in actual planning docs.
```

## 4. Before Creating Archive

Before creating an archive that includes raw author logs, list the messages/topics that will be captured.

Example:

```text
Author messages to capture:
- UI requirements belong in scenario-ui-specs.
- Raw logs must be archive-local.
- Transition problem should be a temporary implementation goal, not an application scenario.
```

## 5. Actual Rules Still Belong In Normal Docs

Raw author messages are not a source of requirements.

Real rules must be written into normal docs such as:

```text
planning/diagrams/scenario-ui-specs/*.md
planning/slices/client/*.md
planning/slices/cross-cutting/*.md
```
