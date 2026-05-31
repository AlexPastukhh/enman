# Level 2 Key Points / Summary Example

Status: current reusable response/output example  
Scope: demonstrates valid Level 2 shape with non-fixed Key points, detailed answer, fixed-order `Краткое саммари` and optional file/change `Итог` boundary

## What This Demonstrates

Owner workflow:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

Used by route/examples chain:

```text
planning/planning-use-case-map.md
```

This example demonstrates valid execution only. It does not own response-level command semantics, routing or permission boundaries.

## Example

User says:

```text
ур2, проверь как должен выглядеть ответ с key points
```

Valid assistant shape:

```markdown
## Key points

- Key points are a compressed version of the detailed answer, not a fixed mini-template.
- Each key point carries key information plus the conclusion or practical meaning that follows from it.
- Key points do not replace the detailed answer; if the answer is already short, skip them.
- Goal, context and source traceability belong near the end in `Краткое саммари`, not in a long opening preamble.

## Answer

For Level 2, use Key points only when the answer is long enough that a short lead-in helps.

Key points should mirror the structure of the detailed answer: if the detailed answer has points about command meaning, permission boundary, sources and next actions, the Key points should preview those same ideas briefly.

Do not force Key points into labels like `Goal`, `Sources`, `Conclusion`, `Next step`. That fixed format belongs to `Краткое саммари`, not to Key points.

## Details

1. Key points are optional.
2. Key points have no fixed internal field format.
3. Key points should not contain unsupported claims absent from the main answer.
4. The detailed answer must still expose reasoning, limits, risks or verification when the task requires them.

## Краткое саммари

Вывод:
  Key points are a short preview of the detailed answer, not a replacement and not a fixed-format block.

Следующие действия:
  Update the response workflow and use-case route examples so future chats follow this shape.

Цель понял так:
  Clarify how Level 2 should use Key points without turning them into a rigid template.

Учтённый контекст:
  - Key points should mirror detailed-answer points.
  - `Краткое саммари` has the fixed ordering.
  - `Итог` is separate and only for file/change/update contexts.

Границы:
  - This is a response-shape example, not a file update overview.
```

## Why This Is Valid

```text
- Key points have no fixed internal field format.
- Key points mirror the detailed answer.
- The detailed answer is still present.
- `Краткое саммари` uses fixed order: Вывод, Следующие действия, Цель понял так, Учтённый контекст, Границы.
- No file/change `Итог` is added because this example does not update a file tree.
```
