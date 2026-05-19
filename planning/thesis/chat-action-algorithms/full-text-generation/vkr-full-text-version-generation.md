# Algorithm: VKR full text version generation

Run when the user asks for a connected VKR text version.

## Output

Generate connected academic VKR text, not a topic draft.

## Required source-pass

Before text:

```markdown
## Краткий результат уточнения по источникам

- Использованные источники:
- Что подтвердилось:
- Что устарело / противоречит текущим решениям:
- Какие открытые вопросы удалось закрыть:
- Какие новые вопросы появились:
- Что остаётся спорным:
- Как это повлияло на текст:
```

## Source priority

Use the same source discipline as topic-draft chats:

```text
1. Active topic drafts and confirmed roadmap decisions.
2. Chapter roadmap and raw notes as cautious source.
3. Scenarios and subject/process rules.
4. Research.
5. Visual materials.
6. Existing section drafts.
7. Existing chapter drafts.
8. Legacy chaotic drafts.
```

Exception: for Chapter 3, repo/evidence comes first.

## Old drafts / ready chapters

Old drafts and ready chapters are fallback candidate sources for full text.

Use them to recover wording/structure after current sources are checked, not as the main truth.

## Research

Use research through `00-research-materials/research-index.md`.

Do not paste research literally.

## Forbidden inside VKR text

Do not write:

```text
roadmap;
topic draft;
raw notes;
source-pass;
semantic point;
workflow;
chat;
AI.
```
