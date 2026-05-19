# Algorithm: topic draft default flow

Run when the user says:

```text
дай драфт;
давай драфт;
след драфт;
обнови драфт;
давай next topic draft.
```

## Required behavior

1. Identify the current topic and parent chapter.
2. Check roadmap and chapter roadmap.
3. Run source-pass using source priority.
4. Generate/update the topic draft.
5. Say what changed since the previous draft.
6. Ask focused questions and propose default answers.
7. Mark what needs research/scenario/domain/repo/visual check.

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

Old drafts and ready chapters are fallback candidate sources. They are not ignored, but they are not the first source unless the user points to them directly.

## Previous drafts as examples

If the user says that a previous draft should be used as an example, include it in source-pass.

Do not copy it mechanically. Use it as an example of structure, depth, table style, semantic point cards, visual bridge and source-pass format.

## Required first block

```markdown
## Что изменилось с прошлого драфта

...

## Source-pass / уточнение по источникам

| Источник | Приоритет | Что проверяли | Что найдено | Как влияет на драфт |
|---|---:|---|---|---|
```
