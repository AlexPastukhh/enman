# Algorithm: VKR full text version generation

Run this when the user asks for a connected version of VKR text, not a topic draft.

Example user signals:

```text
дай версию текста;
сгенерируй текст ВКР;
дай полный текст пункта;
дай версию главы;
обнови текст 1.1;
сделай связный текст.
```

## Output target

The output is a connected academic text version:

- VKR point;
- subsection;
- chapter fragment;
- full chapter;
- whole-text version.

It is not:

- topic draft;
- section draft workbench;
- semantic point list;
- roadmap;
- raw notes dump.

## Required source-pass before text

Before every full-text version, create a short source-pass.

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

The source-pass is external commentary. It must not be copied into the VKR text itself.

## Required source behavior

Use the same source logic as topic-draft chats:

```text
roadmap
→ chapter roadmap
→ topic drafts
→ existing chapter / section draft candidates
→ raw notes as cautious source
→ scenarios/domain/research/repo/visuals according to chapter priority
→ text version
```

## The chat must search sources, not only ask the user

When open questions exist, first try to answer them from available sources.

Use this status table:

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

Ask the user only after the source-pass shows what could not be answered.

## If information is incomplete

Do not stop automatically.

Write a cautious high-level version if possible:

```text
unknown exact statuses → write "state of processing" instead of inventing exact statuses;
unknown implementation → avoid implementation claim;
unknown research claim → move to "requires research".
```

After the text, list cautious places.

## Required response format

```markdown
## Краткий результат уточнения по источникам

...

## Что использовано и как собрана версия

...

## Версия текста ВКР

<connected academic text>

## Осторожные места

...

## Что проверить дальше

...

## Что улучшить в следующей версии

...
```

## Inside VKR text

The VKR text must be in Russian academic style.

It may include:

- paragraphs;
- tables;
- figure/table placeholders;
- captions.

It must not include internal labels:

```text
semantic point;
roadmap note;
raw note;
source-pass;
workflow;
chat;
prompt;
topic draft.
```

## Raw notes

Raw notes are cautious sources.

They can explain user intent and project decisions, but must be rewritten and checked.

Do not paste raw notes into VKR text.

## Existing chapters

Existing chapters 1-2 are valid candidate drafts, not garbage and not final text.

If an existing chapter contains an idea absent from the topic draft, do not ignore it. Extract the idea, check it against desired outcomes and sources, then decide whether to use it in the full-text version.

## Chapter 1

For Chapter 1, prefer:

- scenarios;
- domain/process rules;
- existing section/chapter candidates;
- topic drafts;
- roadmap/raw notes as cautious source;
- research when needed.

Repo/code is mainly for overclaim checks.

## Chapter 2

For Chapter 2, use results of Chapter 1 and design materials. Existing chapter 2 drafts are valid candidate material and must be source-passed before being rewritten.

## Chapter 3

For Chapter 3, use repo/evidence:

- code;
- tests;
- slice drafts;
- screenshots;
- scenarios;
- DATA/domain;
- ADR/questions/decisions.

Do not claim implementation without evidence.

## Guardrails

Do not claim without checking:

- account is required by law;
- anonymous applications are impossible;
- real external verification is implemented;
- mock-check is real integration;
- system automatically makes decisions;
- system replaces employee;
- contract is generated automatically;
- full ECM/EDO exists;
- email notifications are fully implemented.
