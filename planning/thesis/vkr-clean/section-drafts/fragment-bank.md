# VKR Fragment Bank

Status: working bank / reviewer workflow synchronized  
Scope: reusable strong fragments harvested from full section draft attempts and reviewer feedback

## 1. Purpose

This file stores successful fragments so that good wording is not lost when full drafts are reordered, rewritten or superseded.

Store only fragments that are likely to be reused.

Fragments may come from:

```text
- full draft attempts;
- reviewer suggestions;
- Coordinator/Drafter consolidation;
- defense wording;
- figure/table caption drafts;
- project-specific wording discovered during repo/source checks.
```

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
reviewer suggestion
source-backed statement
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
Origin:
Needs:
Risk:

Text:

Why keep:

Possible placements:

Reviewer / consolidation notes:
```

## 5. Harvesting Rules

After each full draft attempt and reviewer pass, check for:

```text
- strong thesis statements;
- good transitions;
- project-centered explanations;
- comparison conclusions;
- figure/table captions;
- clean formulations for defense;
- paragraphs that may fit another section;
- reviewer-suggested rewrites worth preserving.
```

Do not keep fragments only because they sound polished.

Keep fragments that help the VKR explain this specific project.

## 6. Fragments

### FR-CH1-001

Target: Chapter 1 / problem domain or design transition  
Fragment type: thesis  
Status: candidate  
Source basis: project-specific scenario and domain model  
Origin: early drafting  
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

Reviewer / consolidation notes:

```text
Use only where the request-centered process is being introduced.
```
