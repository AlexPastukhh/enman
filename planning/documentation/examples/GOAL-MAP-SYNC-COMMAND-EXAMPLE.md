# Goal Map Sync Command Example

Status: current reusable map-maintenance command example / demonstration-only
Scope: demonstrates `синх карта` response shape: synced target-state Goal Map Brief + archive + apply commands + `План файл-обновление`

## 1. Purpose

This example demonstrates the combined Goal Map synchronization command.

Owner files:

```text
planning/goal-map-principles-workflow-template.md
planning/planning-use-case-map.md
```

This example is demonstration-only. It does not own Goal Map semantics, routing, output permissions, archive format, command behavior or the active living map state.

## 2. User Command Examples

```text
синх карта
синхронизируй карту
синх карта архив
карта синх архив
синхронизируй карту и дай архив
sync goal map
sync map archive
```

## 3. Meaning

`синх карта` means:

```text
- inspect the relevant living Goal Map;
- determine whether it matches the current state of the workstream;
- identify stale snapshot/roadmap/scenario/slice/decision records;
- create a narrow replacement archive to synchronize the map and command route/example records if needed;
- show a Goal Map Brief in the target synchronized state in the same response;
- include ready-to-run apply/diff commands in the same response.
```

The brief is target-state output. It describes the state after the archive is applied, even before the user applies it locally.

## 4. Does Not Mean

```text
- do not start the next functional slice;
- do not implement Generic Action Overview;
- do not write a Tampermonkey userscript;
- do not change archive layout/default rules;
- do not commit or push;
- do not perform broad cleanup outside the map-sync scope.
```

## 5. Example Input

User:

```text
синх карта
```

## 6. Example Output Shape

```text
## Goal Map Brief / Карта цели

Mode: synced target state / целевое состояние после применения архива

Goal:
  <current goal>

Current state:
  Active slice: none selected / decision pending.

Latest completed:
  <latest meaningful completed batch>

Now:
  Goal Map synchronized; next slice selection pending.

Next candidates:
  - <candidate slice A>
  - <candidate slice B>

Slice statuses:
  - SL-1 — DONE / validating
  - SL-2 — DONE / validating
  - SL-3 — DONE / validating
  - SL-4 — DONE / validating
  - SL-5 — NEXT candidate / not started
  - SL-6 — NEXT candidate / not started

Detected stale map points:
  - <stale status/decision rows that the archive fixes>

## Archive

<archive link>

Inside:
  - <replacement files>

## Apply commands

<one PowerShell block that applies the archive, stages expected files, runs checks, saves full diff to file and copies it to clipboard>

## План файл-обновление

Статус: archive created

Change groups:
  - map sync command
  - living map status cleanup
  - example/index/action-log update

Boundaries:
  - Generic Action Overview not started
  - Tampermonkey userscript not started
  - archive layout/default not changed

Next action:
  Apply archive, paste diff, wait for review before commit.
```

## 7. Rules Demonstrated

```text
- The command includes archive output by design.
- The synced brief appears before the archive link.
- The brief describes the target synchronized state, not stale current file state.
- Apply/diff commands are in the chat response.
- The command remains narrow: map sync and related command/example/index/log records only.
- Commit/push waits for pasted diff review.
```
