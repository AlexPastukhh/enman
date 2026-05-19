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
research reports;
visual reviews;
clean requirements;
scenarios only to check process logic;
domain only as subject concepts;
repo-check only for overclaim risks.
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
repo/evidence checks.
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
