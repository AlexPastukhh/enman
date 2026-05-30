# Local / Global Documentation Synchronization Workflow

Status: active reusable workflow  
Scope: repeated process for synchronizing local documentation details with shared visibility targets after a project Shared Visibility Map exists

## 1. Purpose

This workflow keeps local documentation details discoverable when they affect future work outside one local file.

It is not the setup kit.

Use the setup kit first when the project has not yet defined a Shared Visibility Map:

```text
planning/documentation/field-kits/shared-visibility-map-field-kit.md
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

For Enman project map:

```text
planning/shared-visibility-map.md
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

## 5A. Question Status / Assumption Rule

When a local detail is a question, decision risk or assumption, keep status explicit.

Recommended statuses:

| Status | Meaning |
|---|---|
| `open` | Needs an answer before related behavior/contract/design can be finalized. |
| `blocked` | Cannot be answered until another decision/source/evidence is available. |
| `assumption` | A working answer is being used so draft work can continue; user/project owner should confirm, reject or refine it. |
| `accepted direction` | Current direction is good enough for planning and may affect future work. |
| `future review` | Not a current blocker; revisit when relevant future work starts. |
| `resolved` | Answered and no longer open; keep only if useful for traceability. |
| `superseded` | Replaced by a newer question/decision/source. |
| `local only` | Intentionally not mirrored globally; local file should say why. |

Assumption rule:

```text
If a draft can proceed without a final answer, write a reasonable assumption/current direction.
The assumption must be explicit enough for a reviewer to confirm, reject or refine.
Do not hide assumptions in prose.
Do not mark assumptions as resolved.
```

## 5B. Local-Only Reason Rule

A local detail may stay local-only when it affects only that local file.

When a meaningful detail is not mirrored, record the reason when useful:

```text
Local only because:
- affects only this file/scope;
- no cross-file consumer yet;
- not an extension/change pressure;
- not an architecture/source-of-truth decision;
- resolved inside this draft and no longer affects future work.
```

## 5C. Back-Reference Rule

When a local detail is mirrored to a shared target, keep a lightweight local back-reference.

Example shape:

```text
Shared visibility:
- <shared register/index path> / <row ID or heading>
```

The shared target should also point back to the local file or scope when useful.

## 5D. Preflight Checklist

Before finalizing a local/global documentation sync, check:

```text
1. Which local files changed?
2. Which local detail types changed?
3. Does the project Shared Visibility Map require a shared update?
4. Are important open questions first or easy to find locally?
5. Do non-trivial questions have explicit status?
6. Do unresolved assumptions have explicit current direction?
7. Are shared rows back-linked to local files when useful?
8. Are local-only decisions justified when they could look shared-relevant?
9. Are new/moved/superseded files visible from navigation?
10. Is the owner/responsibility route still clear?
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
planning/documentation/field-kits/shared-visibility-map-field-kit.md
planning/shared-visibility-map.md
planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
```
