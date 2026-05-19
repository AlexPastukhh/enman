# Topic-to-section block workflow

This file explains how topic drafts become section drafts.

## Main idea

```text
topic draft не заменяет section draft;
topic draft питает section draft;
section draft аккумулирует текст внутри подготовленных блоков.
```

## Workflow

```text
topic draft
↔ questions / research / repo-check / visual bridge / source harvest
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

## Required process

1. Topic draft describes semantic blocks of a topic.
2. For each semantic block, ask questions.
3. For each block, collect source materials:
   - scenarios;
   - DATA;
   - domain;
   - slices;
   - ADR/questions/decisions;
   - research;
   - visuals;
   - repo/evidence when needed.
4. For each block, define plan of disclosure.
5. Section draft creates similar headings/subheadings.
6. Text is gradually written inside those section blocks.
7. Topic draft remains alive and can be updated after section draft work reveals new questions.

## No mandatory fragment bank

Do not use a fragment bank as the main mechanism. If text is ready, put it into the relevant section draft block.

## Required topic draft block

Each mature topic draft should include:

```markdown
## Блоки будущего section draft

| Блок section draft | Смысл из topic-драфта | Вопросы к блоку | План раскрытия | Источники |
|---|---|---|---|---|
```
