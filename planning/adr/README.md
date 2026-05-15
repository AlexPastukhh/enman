# Architecture Decision Records Index

Status: current ADR planning entry point  
Scope: accepted architecture decision notes, ADR candidates and future full ADRs

## 1. Purpose

This folder captures architecture decisions discovered during domain drafting, slice drafting, client planning and implementation planning.

Full numbered ADRs are not being created yet unless explicitly requested.

## 2. Files

```text
planning/adr/README.md
planning/adr/adr-workflow.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

## 3. What Is What

### Architecture decision notes

File:

```text
planning/adr/architecture-decision-notes.md
```

Meaning:

```text
accepted/current decisions that guide planning and implementation now.
```

Use this file before making implementation/slice/client decisions.

### ADR candidates

File:

```text
planning/adr/adr-candidates.md
```

Meaning:

```text
promotion backlog for decisions that may later become full numbered ADRs.
```

Candidates are not the primary guiding source.

### Full numbered ADRs

Pattern:

```text
planning/adr/ADR-0001-title.md
```

Meaning:

```text
formal architecture decision records.
```

Do not create full numbered ADRs unless explicitly requested.

## 4. Source Priority

```text
1. Full numbered ADR, if present.
2. architecture-decision-notes.md.
3. Local slice/domain/client decision sections.
4. adr-candidates.md as promotion backlog.
```

If candidate and note conflict, stop and clarify.

## 5. When To Update Decision Notes

Update `architecture-decision-notes.md` when a decision:

```text
- is accepted/current enough to guide planning;
- has meaningful alternatives or trade-off;
- affects multiple slices or layers;
- should not remain only in chat/local file;
- may be useful in diploma text.
```

## 6. When To Update Candidates

Update `adr-candidates.md` when a decision:

```text
- may deserve a full numbered ADR later;
- is architecturally significant;
- affects multiple slices/layers;
- is expensive to change;
- has useful alternatives/consequences for diploma.
```

## 7. Agent Rule

Agents must report ADR impact in planning/archive/implementation responses when relevant:

```text
ADR impact:
- no ADR update needed
- decision note added/updated
- candidate added/updated
- full ADR promotion proposed
```
