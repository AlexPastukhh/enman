# Goal Map Brief Response Example

Status: current reusable response-output example / demonstration-only
Scope: demonstrates compact in-answer Goal Map Brief shape for an active long-running workstream

## 1. Purpose

This example demonstrates the compact `Goal Map Brief / Карта цели` response block.

Owner files:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/goal-map-principles-workflow-template.md
planning/planning-use-case-map.md
```

This example is demonstration-only. It does not own Goal Map workflow rules, command routing, source truth, output modes, permission boundaries or living map state.

## 2. User Commands

```text
кц
карта цели кратко
goal map brief
выведи краткую карту цели
```

## 3. Placement

In Level 2 / Level 3 answers, place the brief near the end:

```text
main answer / plan / review
+
Краткое саммари
+
Goal Map Brief / Карта цели
+
Итог, only when file/change/update context applies
```

## 4. Example Output

## Goal Map Brief / Карта цели

**Goal**

Построить переносимую систему коротких команд и Tampermonkey prompt helper для длинных чатов, чтобы не терялись цель, текущий рабочий слайс, границы действий и следующий шаг.

**Current slice**

**SL-4 — Critical Review Command**
Status: **NEXT / not started**

### Current slice chain

**Why now**

Нужна команда, которая включает честную критическую оценку вместо автоматического согласия.

**Done**

- reusable command examples уже есть;
- root use-case map уже маршрутизирует команды;
- Goal Map owner/example/living workstream уже есть;
- Level 2 / Key points / `Краткое саммари` rules уже есть.

**Now**

- определить семантику команды `крит`;
- определить aliases;
- определить boundaries;
- определить route из root use-case map.

**Next**

- добавить reusable example;
- обновить examples index;
- добавить/обновить route;
- обновить living Goal Map evidence.

**After**

- перейти к `SL-5 — Generic Action Overview` или `SL-6 — Tampermonkey Guided Prompt Helper` по выбранному пути.

### Other slices

| Slice | Status |
|---|---|
| SL-1 — Reusable Command Semantics Foundation | ✅ DONE |
| SL-2 — Root Command Routing | ✅ DONE |
| SL-3 — Goal Map Format And Living Workstream | ✅ DONE |
| SL-5 — Generic Action Overview | ⬜ PLANNED / not started |
| SL-6 — Tampermonkey Guided Prompt Helper | ⬜ PLANNED / not started |

## 5. Rules Demonstrated

```text
- Current slice is expanded.
- Other slices are status-only.
- No `<details>` / collapsible blocks are used.
- The block appears after `Краткое саммари` and before `Итог` when `Итог` applies.
- The block does not replace the full living Goal Map.
- The command does not grant permission to edit repo files.
```
