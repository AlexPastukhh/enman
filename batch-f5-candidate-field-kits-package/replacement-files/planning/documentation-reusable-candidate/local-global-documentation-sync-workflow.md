# Local / Global Documentation Synchronization Workflow

Status: reusable candidate workflow  
Scope: repeated process for synchronizing local documentation details with shared visibility targets after a project Shared Visibility Map exists

> Candidate note: this file belongs to `planning/documentation-reusable-candidate/`. It is not an active Enman source of truth until a later migration/switch batch approves it.

## 1. Purpose

This workflow keeps local documentation details discoverable when they affect future work outside one local file.

It is not the setup kit.

Use the setup kit first when the project has not yet defined a Shared Visibility Map:

```text
planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
```

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Principles | Why local future-impacting details need shared visibility. |
| Field kit | How to define the project Shared Visibility Map. |
| Project shared visibility map | Concrete local-detail types and shared targets for one project. |
| This workflow | Repeated local/global sync process using the configured map. |
| Examples | Demonstrations only. |

## 3. Inputs

Before running this workflow, identify:

```text
- local file(s) changed;
- local detail type(s) changed;
- project Shared Visibility Map;
- shared indexes/registers in scope;
- whether new files were added/moved/superseded.
```

For Enman candidate setup:

```text
planning/documentation-reusable-candidate/enman-shared-visibility-map.md
```

## 4. Core Rule

```text
local file detail
        ↓
classify local detail type
        ↓
check project Shared Visibility Map
        ↓
update local file
        ↓
mirror to shared index/register when required
        ↓
update navigation if files were added/moved/superseded
```

## 5. Sync Process

```text
1. Read the project Shared Visibility Map.
2. Identify local detail types in the changed local file.
3. Decide whether each detail is local-only or shared-visible.
4. If shared-visible, update the mapped shared index/register.
5. If local-only, record or preserve the reason when useful.
6. If a mapped shared target does not exist, create a follow-up rather than hiding the detail.
7. Update navigation/read-order only when files were added, moved, renamed or superseded.
```

## 6. Output Table

Use this table when helpful:

| Local file | Local detail | Detail type | Shared target | Action | Notes |
|---|---|---|---|---|---|

## 7. Do Not

```text
- Do not assume every local detail needs a global row.
- Do not leave cross-file/future-impacting questions buried in a local file.
- Do not invent shared targets during repeated workflow if the project map is missing.
- Do not make the workflow own project-specific register paths.
```

## 8. Related Files

```text
planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
planning/documentation-reusable-candidate/enman-shared-visibility-map.md
planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
```
