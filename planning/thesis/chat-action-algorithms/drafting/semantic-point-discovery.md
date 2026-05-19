# Algorithm: semantic point discovery

Run this algorithm when a chat needs to discover, justify or revise semantic points for a topic draft.

## Core idea

A semantic point is not created just because a paragraph exists in an existing chapter.

A semantic point appears because it is needed to reach the desired outcome of the current structural element.

```text
desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ discovery questions
→ semantic points
→ order of disclosure
→ section draft blocks
```

## Important correction: coverage tables live inside semantic point cards

Do not create one huge "coverage of desired results" table for the entire topic.

Use this structure:

```text
desired outcome of topic
→ discovery table: which semantic points are needed
→ semantic point card
→ desired result of this semantic point
→ coverage table inside this semantic point card
```

The topic-level desired outcome defines which semantic points are needed.

The detailed coverage table belongs to the specific semantic point, because it answers:

```text
what exactly should this point achieve,
what material/idea covers that result,
what is missing,
where it affects the section draft.
```

## Structural hierarchy

Use this hierarchy recursively:

```text
VKR
→ chapter
→ subsection / required point
→ topic
→ semantic point
→ section draft block
```

Every element must explain how it helps the parent element reach its desired outcome.

## Required discovery steps

1. Identify the parent element.
2. Define the desired outcome of the parent element.
3. Identify the current element.
4. Define the role of the current element in the parent outcome.
5. Define the desired outcome of the current element.
6. Define negative boundaries.
7. Ask discovery questions.
8. Derive semantic points from answers.
9. For each semantic point:
   - define why it is needed;
   - define what desired topic outcome it supports;
   - define its own desired result;
   - create a coverage table for its desired result;
   - define order of disclosure;
   - define source checks and open questions.
10. Map semantic points to section draft blocks.

## Topic-level discovery table

At topic level use a concise table:

```markdown
| Желаемый итог темы | Какой смысловой пункт нужен | Почему |
|---|---|---|
```

Do not put detailed "what already exists / what is missing / where it goes" for every result here. That belongs inside semantic point cards.

## Semantic point card

Each semantic point should have:

```markdown
### Смысловой пункт: <name>

**Цель пункта:**
...

**Желаемый результат смыслового пункта:**

После этого блока читатель должен понять:
- ...
- ...

**Какой желаемый итог темы поддерживает:**
...

**Какой старший итог поддерживает:**
...

**Покрытие желаемого результата:**

| Желаемый результат внутри пункта | Чем раскрываем | Что уже есть | Чего не хватает | Куда влияет |
|---|---|---|---|---|

**Почему этот пункт нужен:**
...

**Место в порядке раскрытия:**
- после:
- перед:
- почему здесь:

**Existing section candidate material:**
...

**Вопросы:**
| Вопрос | Приоритет | Зачем нужен | Дефолтный ответ | Куда влияет |
|---|---|---|---|---|

**Источники / проверки:**
- research:
- repo/evidence:
- scenarios:
- domain:
- slices:
- visuals:
- existing section candidate:

**План раскрытия:**
1. ...
2. ...
3. ...

**Пример перевода в section draft:**
...

**Решение по section draft:**
оставить / уплотнить / расширить / переписать / перенести / удалить / добавить соседний блок
```

## Example: request state and allowed actions

Semantic point: `Состояние заявки и допустимые действия`

Desired result:

```text
После блока читатель должен понять, что заявка важна не только как запись с данными,
но и как элемент процесса, состояние которого разрешает или ограничивает дальнейшие действия.
```

Coverage table:

| Желаемый результат внутри пункта | Чем раскрываем | Что уже есть | Чего не хватает | Куда влияет |
|---|---|---|---|---|
| Понять, что у заявки есть состояние | Объяснить, что заявка проходит обработку не одномоментно | Есть идея про статус заявки | Аккуратные формулировки без точных статусов до repo-check | Текст 1.1 |
| Понять, что действия зависят от состояния | Примеры: договорный этап только после одобрения, отклонённая заявка не идёт к договору | Есть идея про правила и согласованность | Проверить реальные состояния | 1.1, 1.3, глава 2 |
| Понять, почему автоматизация шире формы | Показать, что система помогает не только вводить данные, но и не нарушать порядок процесса | Пользовательская идея уже сформулирована | Связать с проблемами ручного процесса | 1.1 → 1.3 |

## Main rule

Do not ask random large lists of questions.

Questions must be selected according to the current desired outcome and the current stage of drafting.
