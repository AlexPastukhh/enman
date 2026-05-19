# Algorithm: topic clarify and recheck flow

Run when the user says:

```text
уточни;
перепроверь;
проверь всё;
давай драфт уточни;
обнови с проверкой;
что не так.
```

## Core idea

Clarification is not only asking the user for more information.

It is:

```text
user clarification
+ source-pass over available materials
+ answers from sources
+ new questions from sources
+ decisions
→ growing topic draft
```

## Required first block

Every clarify/recheck response starts with:

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

## The chat must use sources

Before asking the user, try to find answers in available materials:

- active topic draft;
- existing section/chapter draft candidates;
- roadmap and raw notes;
- chapter roadmap;
- scenarios;
- domain/process rules;
- research;
- visual materials;
- repo/evidence when appropriate.

## Answer table

Use:

```markdown
| Вопрос | Где искали | Что найдено | Статус | Что делать |
|---|---|---|---|---|
```

Statuses:

```text
answered;
partial;
ask user;
research needed;
scenario/domain check needed;
repo-check needed;
visual decision needed;
defer.
```

## Source-pass must be included in the draft

If clarification finds answers, contradictions, new questions or decisions, this must be added to the topic draft as a `Source-pass / уточнение по источникам` block.

The topic draft grows. It does not simply get rewritten shorter.

## If capture already exists

If the user says capture/roadmap was already saved:

- do not repeat the whole raw-capture table in every draft;
- reference `VKR-DRAFTING-ROADMAP.md`;
- add only new roadmap additions if new raw thoughts appear.

## Existing chapter candidates

Existing chapters, especially chapters 1-2, are valid candidate material, not garbage.

If a candidate contains a thought absent from the topic draft:

1. identify the semantic point;
2. identify desired result;
3. check risks and sources;
4. decide: add / rewrite / keep in section draft / transfer / reject.

## Chapter 1 source rule

For Chapter 1, prefer scenarios and domain/process rules over code/entity names.

Repo/code is used mainly for overclaim checks or Chapter 3.

## Required result

After source-pass, provide:

- what changes in the draft;
- updated topic draft or affected sections;
- new questions and source locations;
- decisions added to the draft.
