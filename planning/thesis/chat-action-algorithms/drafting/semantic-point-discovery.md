# Algorithm: semantic point discovery

Run when a chat needs to discover, justify or revise semantic points for a topic draft.

## Core idea

```text
desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ source-pass
→ discovery questions
→ semantic points
→ section/full-text blocks
```

## Desired result coverage

Use either labels `[A]`, `[B]` or status coverage tables.

The important requirement is that the draft shows:

- what desired results exist;
- which are covered;
- which are partial;
- which require scenario/domain/research/repo/visual check.

## Semantic point card minimum

```markdown
### Смысловой пункт: <name>

Статус покрытия:
Закрывает желаемый результат:
Цель:
Место в раскрытии:
Покрытие результата:
Source-pass notes:
Вопросы:
План раскрытия:
Пример section/full-text paragraph:
Решение:
```

## Source-pass integration

If a source-pass changes the understanding of a semantic point, update the semantic point card.
