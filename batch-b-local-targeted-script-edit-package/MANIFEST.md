# Batch B Local Targeted Script Edit Replacement Package

Status: ready for manual apply  
Scope: safe complete-replacement files for Batch B only

## Purpose

This package documents the Local Targeted Script Edit / large-file update mode and hybrid archive/script delivery rules.

## Target File Delivery Safety Classification

Included as complete replacements:

```text
planning/documentation/documentation-update-workflow.md
planning/replacement-file-generation-guide.md
planning/documentation/examples/README.md
planning/documentation/documentation-action-log.md
```

Excluded from this archive:

```text
planning/planning-use-case-map.md
```

Reason:

```text
planning/planning-use-case-map.md is a large/shared routing table.
If a Batch B route marker is needed, it should be changed with a separate one-file targeted script.
```

## Changes

```text
- Add required target-file delivery safety classification to documentation update planning.
- Add Local Targeted Script Edit mode.
- Add one large/shared file = one targeted write script rule.
- Add preflight-before-write script structure.
- Add no full diff printed to terminal rule.
- Add hybrid archive/script delivery boundary to replacement archive guidance.
- Add deferred example rows for local targeted scripts and hybrid delivery.
- Add documentation action-log entry.
```

## Not Included

```text
- planning/planning-use-case-map.md route marker.
- Any code or generated artifact.
- Any tracked .ps1 helper script.
```
