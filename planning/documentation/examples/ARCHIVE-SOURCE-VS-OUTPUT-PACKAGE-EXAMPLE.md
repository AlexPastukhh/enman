# Archive Source Vs Output Package Example

Status: current reusable command/output example  
Scope: demonstrates valid separation between archive-as-read-source and archive-as-output-package

## What This Demonstrates

Owner files:

```text
planning/planning-use-case-map.md
planning/replacement-file-generation-guide.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

This example demonstrates valid execution only. It does not own archive/package rules, source-mode routing or permission boundaries.

## Example A — `арх` Means Read Source

User says:

```text
арх, проверь что осталось
```

Valid assistant behavior:

```text
- Treat the latest uploaded archive as the read source.
- Perform the requested check against that source.
- State that archive snapshot was used.
- Do not create a new output archive.
- Do not commit or edit files.
```

Invalid behavior:

```text
- Generate a replacement package just because user said `арх`.
- Treat archive freshness as remote GitHub proof when remote freshness matters.
```

## Example B — `давай архив` Means Output Package

User says:

```text
давай архив
```

Valid assistant behavior:

```text
- Treat this as output mode.
- Use planning/replacement-file-generation-guide.md.
- Build a replacement package for the active approved scope.
- Include MANIFEST.md, APPLY.md and replacement-files/.
- Include apply/diff capture commands.
- Ask the user to paste diff for review before commit.
```

Invalid behavior:

```text
- Reply only with a patch when replacement archive was requested.
- Add scripts without need or permission.
- Commit automatically.
```

## Why This Is Valid

```text
- It separates read source mode from output mode.
- It preserves accepted command semantics.
- It keeps file edit/package generation behind explicit output request.
```
