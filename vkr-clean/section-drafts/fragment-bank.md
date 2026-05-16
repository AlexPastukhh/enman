# VKR Fragment Bank

Status: working bank  
Scope: reusable strong fragments harvested from full section draft attempts

## 1. Purpose

This file stores successful fragments so that good wording is not lost when full drafts are reordered, rewritten or superseded.

Store only fragments that are likely to be reused.

## 2. Fragment Types

```text
thesis
transition
project explanation
comparison conclusion
architecture rationale
implementation explanation
testing explanation
figure caption
table caption
chapter conclusion
defense wording
```

## 3. Fragment Statuses

```text
candidate
reusable
needs-source
needs-repo-check
inserted
superseded
```

## 4. Fragment Template

```text
## FR-<CHAPTER>-<NUMBER>

Target:
Fragment type:
Status:
Source basis:
Needs:
Risk:

Text:

Why keep:

Possible placements:
```

## 5. Fragments

### FR-CH1-001

Target: Chapter 1 / problem domain or design transition  
Fragment type: thesis  
Status: candidate  
Source basis: project-specific scenario and domain model  
Needs: no external source needed  
Risk: low

Text:

> В разрабатываемой системе заявка рассматривается как центральный объект процесса, поскольку именно она связывает клиента, заявителя, сотрудника, результат рассмотрения и последующий документный этап.

Why keep:

This fragment clearly connects the subject domain to the project model and can be reused in Chapter 1, Chapter 2 or defense speech.

Possible placements:

```text
1.1 Анализ предметной области
2.x Проектирование доменной модели
presentation speech
```
