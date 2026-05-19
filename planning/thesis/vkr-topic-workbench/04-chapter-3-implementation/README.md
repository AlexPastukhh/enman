# 04-chapter-3-implementation

Status: active / Chapter 3 implementation workbench

Purpose:
store and organize topic drafts for Chapter 3 of the VKR. Chapter 3 is the implementation and evidence chapter: it should show what is actually implemented, how the implementation corresponds to Chapter 2, how the application works, how it is tested and what can be demonstrated.

## Recommended Chapter 3 structure for VKR text

```text
3 Реализация и проверка web-приложения

3.1 Структура решения и организация реализации
3.2 Реализация сценариев приложения через slices
3.3 Реализация API и взаимодействия frontend/backend
3.4 Реализация хранения данных и файлов
3.5 Реализация пользовательского интерфейса
3.6 Тестирование и готовность к демонстрации
```

## Topic files

```text
01-solution-structure-and-implementation-overview.topic.md
02-slices-and-application-scenarios.topic.md
03-api-implementation.topic.md
04-data-and-file-storage-implementation.topic.md
05-frontend-implementation.topic.md
06-testing-and-demo-readiness.topic.md
```

## Chapter 3 flow

```text
solution structure
→ implemented slices
→ API implementation
→ data/file storage implementation
→ frontend implementation
→ testing and demo readiness
```

## Rules

- Do not turn Chapter 3 into new theory.
- Prefer implementation evidence: files, routes, handlers, DTO, screens, tests, screenshots.
- Use real repo terms after repo-check, but explain them in thesis-facing language.
- Keep screenshots and demo checklist close to the topic drafts.
- Avoid claiming features that are not implemented, such as electronic signature, full EDMS, object storage or automatic contract generation, unless repo-check confirms them.
