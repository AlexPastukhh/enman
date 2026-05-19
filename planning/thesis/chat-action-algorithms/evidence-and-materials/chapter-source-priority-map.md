# Chapter source priority map

Use this to decide which sources to check for each chapter.

## Chapter 1 — analysis and automation justification

Purpose:

```text
предметная область;
процесс;
проблемы;
варианты автоматизации;
выбор направления.
```

Priority sources:

```text
topic-драфты главы 1;
existing chapter drafts / ранние главы 1–2 as secondary resource;
research reports;
visual reviews;
clean requirements;
scenarios only to check process logic;
domain only as subject concepts;
repo-check only for overclaim risks.
```

Use existing chapter drafts carefully:

```text
good structure → candidate for section blocks;
good paragraph → place into section draft block and review;
weak/general paragraph → convert to questions and rewrite;
problem notes → update topic questions/checklists.
```

## Chapter 2 — requirements and design

Purpose:

```text
требования;
сценарии;
данные;
доменная модель;
архитектура;
БД;
API/UI проектирование.
```

Priority sources:

```text
topic-драфты главы 2;
existing chapter drafts / previous chapter 2 text as secondary resource;
scenario specs;
DATA;
domain drafts;
requirements;
architecture decisions;
UI planning;
API planning;
database planning;
research on requirements/architecture if needed.
```

## Chapter 3 — implementation and testing

Purpose:

```text
реализация;
код;
тесты;
скриншоты;
демонстрация;
ограничения.
```

Priority sources:

```text
code;
tests;
slice drafts;
scenario specs;
DATA;
domain drafts;
ADR / questions / decisions;
UI screenshots;
repo/evidence checks;
existing chapter drafts only as style/structure resource, not fact source.
```

Chapter 3 source chain:

```text
сценарии
→ вопросы
→ решения
→ домен
→ ADR / архитектурные решения
→ slice drafts
→ код / тесты / скриншоты
→ чистый текст главы 3
```
