# Algorithm: topic clarify and recheck flow

Run when the user says:

```text
уточни;
перепроверь;
проверь всё;
обнови с проверкой;
что не так.
```

## Core idea

Clarification is not only asking the user.

It is:

```text
source-pass
→ answers from current sources
→ new questions
→ contradictions/outdated material
→ draft update
```

## Source priority

```text
1. Active topic drafts and confirmed roadmap decisions.
2. Roadmap/raw notes as cautious source.
3. Scenarios and subject/process rules.
4. Research.
5. Visual materials.
6. Existing section drafts.
7. Existing chapter drafts.
8. Legacy chaotic drafts.
```

Old drafts are checked late, as fallback/candidate material, unless the user explicitly says to use a specific previous draft as an example.

## Required first block

```markdown
## Краткий результат уточнения по источникам

- Какие источники использованы:
- Что подтвердилось:
- Что устарело / противоречит текущим решениям:
- Какие открытые вопросы удалось закрыть:
- Какие новые вопросы появились:
- Что остаётся спорным:
- Что нужно обновить в драфте:
```

## Required source table

```markdown
| Источник | Приоритет | Что проверяли | Что найдено | Как влияет на драфт |
|---|---:|---|---|---|
```

## Old draft handling

If normal sources do not give enough material, or if the old draft may contain missed ideas:

```text
old draft
→ candidate material
→ extract idea
→ check against desired outcome
→ check against current decisions
→ rewrite / use / move / reject
```
