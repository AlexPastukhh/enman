# Material Harvest Protocol

Status: initial

## Purpose

Material harvest means collecting raw project evidence before writing polished VKR text.

Do not harvest random paragraphs. Harvest decisions, questions, rules, scenarios, visuals and evidence.

## Source categories

| Source | What to collect |
|---|---|
| Planning docs | questions, decisions, accepted rules, scenario reasoning |
| Scenario specs | actors, preconditions, main flow, alternatives, postconditions, invariants |
| Slice docs | behavior units, boundaries, implementation decisions, test points |
| Repo/code | actual implementation evidence, routes, APIs, pages, tests |
| Research | definitions, standards, external comparison criteria |
| Other chats | author reasoning, doubts, decision background, living formulations |

## Harvest rule

Every harvested item must be assigned to a topic and marked with status:

```text
candidate
useful
needs-source
needs-repo-check
needs-visual
superseded
not-for-final
```

## L1/L2 note

Internal labels such as `L1` / `L2` may be stored only in harvest notes when tracing source documents. They should be translated into VKR-facing language before drafting.

Examples:

```text
L1 -> client request flow / first implemented client-request software increment
L2 -> employee review and agreement/document exchange flow / next software increment
```
