# Clean VKR Materials

Статус: clean layer / section drafts / evidence / reviewer workflow

Эта папка содержит материалы, которые можно разворачивать в текст ВКР. Planning-файлы используются как источники, но не копируются напрямую.

## 1. Главная граница

```text
planning/ — рабочая инженерная кухня;
vkr-topic-workbench/ — смысловые topic-драфты и блоки будущего текста;
vkr-clean/ — чистые материалы, evidence, section-драфты и reviewer workflow.
```

В финальную ПЗ не переносятся:

```text
ИИ;
чаты;
промпты;
agent workflow;
archive notes;
raw author logs.
```

## 2. Где section-драфты

```text
planning/thesis/vkr-clean/section-drafts/
```

Section draft — это рабочий текст подраздела, который постепенно собирается из блоков, заданных topic-драфтом.

## 3. Где слабые/хаотичные главы

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/
```

Эта папка предназначена для глав, созданных до текущего workflow. Такие тексты нельзя считать clean section drafts без анализа.

## 4. Active workflow для section drafts

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Основной переход описан здесь:

```text
planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
```

## 5. Section draft files

| Path | Purpose |
|---|---|
| `section-drafts/README.md` | Entry point for section draft workflow |
| `section-drafts/section-draft-register.md` | Register of subsection draft attempts and review status |
| `section-drafts/reviewer-workflow.md` | Reviewer roles and review process |
| `section-drafts/reviewer-prompts.md` | Reusable reviewer prompts |
| `section-drafts/full-draft-template.md` | Template for full draft attempts |
| `section-drafts/full-draft-review-checklist.md` | Review checklist |

`fragment-bank.md` is deprecated/support and not part of the active workflow.

## 6. Clean source files

Use clean source files for terminology, requirements, domain, architecture, database, UI, testing, results and future work. Keep them clean, project-specific and evidence-aware.

## 7. Navigation rule

If structure changes, update:

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/vkr-clean/README.md
planning/thesis/vkr-clean/vkr-materials-index.md
relevant section-drafts README/register files
```
