# VKR Section Drafts

Статус: active / block-based section drafting / reviewer workflow

Эта папка содержит section-драфты — рабочие тексты подразделов, которые ближе к ПЗ, чем topic-драфты и planning notes.

## 1. Section draft не пишется из воздуха

Section draft должен расти из topic-драфта:

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

## 2. Как выглядит section draft на раннем этапе

Section draft может быть не сплошным текстом, а рабочей структурой блоков:

```markdown
# <подраздел>

## Блок: <название>

Вопросы:
- ...

План раскрытия:
- ...

Текст:
...
```

Потом вопросы и план убираются или переносятся в notes, когда текст становится чистовым.

## 3. Что должно быть у каждого блока

```text
- какой вопрос закрывает блок;
- какой смысл взят из topic-драфта;
- какие уточнения нужны;
- нужен ли research bridge;
- нужен ли repo/evidence check;
- нужен ли visual bridge;
- план раскрытия;
- рабочий текст.
```

## 4. Текущие draft folders

```text
chapter-1/
```

Слабые/хаотичные старые главы хранить не здесь, а в:

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/
```

## 5. Current Workflow Files

| File | Purpose |
|---|---|
| `vkr-section-drafting-workflow.md` | Short/full draft workflow and coordinator loop |
| `section-draft-workflow.md` | Section draft workflow notes |
| `reviewer-workflow.md` | Reviewer roles and feedback consolidation |
| `reviewer-prompts.md` | Reusable prompts for reviewer chats |
| `section-draft-register.md` | Status register for subsection drafts and review state |
| `short-draft-template.md` | Legacy/helper template for short drafts |
| `full-draft-template.md` | Full draft attempt template |
| `full-draft-review-checklist.md` | Checklist for full draft attempts |

`fragment-bank.md` is deprecated/support and not part of the active drafting loop.

## 6. Working rule

Do not generate a full section draft as one undifferentiated text. First create section blocks, discuss questions, make disclosure plans, then generate text under those blocks.

## 7. Reviewer pass

After section draft v1, use content / structure / style-originality reviewer workflow. Reviewer feedback should update the section draft and, if needed, the topic-draft questions/blocks.
