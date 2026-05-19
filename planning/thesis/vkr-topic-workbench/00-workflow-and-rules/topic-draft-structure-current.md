# Current topic draft structure

This file fixes the current structure of topic drafts.

## Main model

```text
raw notes
→ VKR-DRAFTING-ROADMAP.md
→ chapter roadmap
→ existing chapter / ready subsection
→ section draft candidate
→ reverse engineering of semantic points
→ topic draft
→ questions / sources / checks / plan of disclosure
→ updated section draft block
→ clean VKR text
```

## Core unit: semantic point

A semantic point is the minimal meaningful unit of disclosure.

It exists because it helps the current topic/subsection/chapter reach a desired outcome.

```text
desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ discovery questions
→ semantic points
```

## Important correction

Do not make a single huge coverage table for all desired topic results.

Use:

```text
topic desired outcome
→ concise discovery of semantic points
→ each semantic point has its own desired result
→ each semantic point has its own coverage table
```

## Recommended topic draft sections

```markdown
# Тема: <название>

Статус: <рабочий статус внутри файла>

## 0. Что изменилось с прошлого драфта

## 1. Roadmap-capture перед обновлением темы

Что из обсуждения положено в общий roadmap, что относится к текущей теме, что остаётся на будущее.

## 2. Карта размещения

## 3. Что это за тема

## 4. Зачем нужен этот элемент и какой итог он должен дать

### 4.1 Итог старшего элемента

### 4.2 Роль текущего элемента в итоге старшего элемента

### 4.3 Желаемый итог текущего элемента

### 4.4 Что не должно быть итогом текущего элемента

## 5. Discovery смысловых пунктов

### 5.1 Вопросы discovery

### 5.2 Как из желаемого итога темы рождаются смысловые пункты

## 6. Источники материалов для темы

## 7. Разбор existing section draft candidate

## 8. Смысловые пункты, выделенные из discovery и existing section draft candidate

## 9. Карточки смысловых пунктов

## 10. Новые смысловые пункты, которых нет в готовом section candidate

## 11. Research bridge

## 12. Repo/evidence questions

## 13. Visual bridge

## 14. Границы утверждений / overclaim risks

## 15. Решения по теме

## 16. Блоки будущего section draft

## 17. Карта будущего текста

## 18. Что обновить в section draft

## 19. Связанные roadmap-заметки

## 20. Что проверить дальше

## 21. Итоговая формула темы
```

## Desired outcome block

Use one combined section instead of separating "why needed" and "desired outcome".

```markdown
## Зачем нужен этот элемент и какой итог он должен дать
```

It must show:

1. desired outcome of the parent element;
2. role of the current element in the parent outcome;
3. desired outcome of the current element;
4. negative boundaries;
5. how the desired outcome produces semantic points.

## Discovery table at topic level

```markdown
| Желаемый итог темы | Какой смысловой пункт нужен | Почему |
|---|---|---|
```

## Semantic point coverage

Inside each semantic point card include:

```markdown
**Желаемый результат смыслового пункта:**

**Покрытие желаемого результата:**

| Желаемый результат внутри пункта | Чем раскрываем | Что уже есть | Чего не хватает | Куда влияет |
|---|---|---|---|---|
```

## Existing section candidate rule

Existing chapter/subsection text is a section draft candidate.

Do not paste it into topic draft wholesale.

Use reverse engineering:

```text
existing section candidate
→ semantic points
→ questions
→ checks
→ disclosure plan
→ updated section draft blocks
```

## Section draft role

Section draft contains candidate text.

Topic draft contains semantic points, questions, checks, risks and plans.
