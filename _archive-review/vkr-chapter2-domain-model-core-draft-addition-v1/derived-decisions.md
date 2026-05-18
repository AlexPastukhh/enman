# Derived decisions

## Draft identity

- VKR Chapter 2 topic draft.
- Chapter section: `2.2 Проектирование предметной модели и жизненных циклов`.
- Topic: `Предметная модель web-приложения: основные понятия, сущности и связи`.

## Repository placement

```text
planning/thesis/vkr-topic-workbench/03-chapter-2-design/02-domain-model-lifecycles/01-domain-model-core.topic.md
```

## Key content decisions preserved

- Keep the main VKR language in Russian subject-domain terms.
- Treat repo/code terms as a correspondence layer for repo-check, not as the main language of the VKR text.
- Separate:
  - subject-domain concept;
  - design concept for Chapter 2;
  - possible repo/code term;
  - implementation details not to pull into 2.2.1.
- Avoid turning the topic into a code/DDD/entity/table/endpoints description.
- Keep file storage as infrastructure; detailed data/file storage belongs to 2.4 and Chapter 3.
- Avoid overclaiming: no full SED/ECM, no legally significant EDI, no electronic signature, no automatic contract generation.

## Merge mode

Addition/replacement:
- If the target file does not exist, add it.
- If the target file exists locally, preserve a copy into `_archive-review/.../original-files/` before copying.
