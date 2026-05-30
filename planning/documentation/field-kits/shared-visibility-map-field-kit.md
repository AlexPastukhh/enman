# Shared Visibility Map Field Kit

Status: active reusable field kit  
Scope: setup guidance for deriving a project-specific local-detail to shared-visibility map

## 1. Purpose

This field kit helps a project define where local documentation details become globally discoverable.

It is used before the repeated local/global synchronization workflow.

It answers:

```text
Which local details can stay local?
Which local details must be visible globally?
Which shared indexes/registers own visibility for each detail type?
Where should the project Shared Visibility Map live?
```

## 2. Responsibility Split

| Owner | Owns |
|---|---|
| Principles | Invariant: local details that affect future work need shared visibility or explicit local-only status. |
| This field kit | Setup questions and map shape for project-specific local-detail -> shared target routing. |
| Local/global workflow | Repeated sync process using the configured map. |
| Project shared visibility map | Concrete detail types, shared targets and project paths. |
| Examples | Demonstrations only. |

## 3. Setup Questions

Answer these before creating the project map:

```text
1. What kinds of local files exist?
2. What local sections commonly contain questions, assumptions, decisions or future reminders?
3. Which detail types affect more than one local file?
4. Which detail types affect future files not yet created?
5. Which shared indexes/registers already exist?
6. Which shared indexes/registers are missing?
7. What detail types are safe to keep local-only?
8. How should local-only reasons be recorded?
9. Where should the Shared Visibility Map live?
```

## 4. Candidate Map Shape

Create a project-specific file with this table:

| Local detail type | Local owner | Shared visibility target | Mirror condition | Local-only allowed? | Notes |
|---|---|---|---|---|---|

For the active Enman project map, see:

```text
planning/shared-visibility-map.md
```

## 5. Handoff To Workflow

After the project map exists, use the repeated workflow:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

The workflow should apply the project map instead of guessing shared targets every time.

## 6. Example

Scenario/project example:

```text
planning/documentation/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
```
