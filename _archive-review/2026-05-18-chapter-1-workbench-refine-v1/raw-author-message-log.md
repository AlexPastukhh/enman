## Entry 009 — Chapter 1 point folders and internal topics

Date: 2026-05-18  
Source: chat message  
Related area: Chapter 1 workbench / topic granularity  
Status: raw / not-processed

Raw author message:
> какиме внутренние темы, драфты могут быть у этих папок ключевых тем(мы можем в одном файл драфте маленькую папку покрыть в теории если реально так )

Notes:
- Store raw only.
- Important because it clarifies that some VKR point folders can be covered by one strong topic draft rather than many small files.

## Entry 010 — Fix Chapter 1 structure and start first topic draft

Date: 2026-05-18  
Source: chat message  
Related area: Chapter 1 workbench / archive request  
Status: raw / not-processed

Raw author message:
> давай зафиксируем и перейдем к драфту первой темы для 1 пункта главы, можно архив дать, только вот тут обновления по большим архивам на всякий случай чтобы ты знал-

Notes:
- Store raw only.
- Leads to this archive: Chapter 1 structure refinement and first topic draft for point 1.1.

## Entry 011 — Safe archive merge workflow update

Date: 2026-05-18  
Source: uploaded text file / pasted workflow  
Related area: archive workflow / merge safety  
Status: raw / not-processed

Raw author message:
> Нам нужен архивный workflow, а не просто “сгенерировал zip и надеемся, что он не затрёт полезное”.
> 
> Проблема сейчас такая:
> 
> архив содержит много файлов
> ↓
> часть файлов новые
> часть файлов заменяет существующие
> ↓
> замена может быть правильной по новой теме,
> но случайно потерять старые guardrails/навигацию/детали
> ↓
> после проверки приходится делать v2/v3
> 
> Нужно сделать это системным.
> 
> Предлагаемый workflow: двухшаговый archive merge
> Шаг 1 — Safe merge archive
> 
> Первый архив может заменять файлы, но обязан сохранять исходники внутри себя в отдельной уникальной папке.
> 
> Структура архива:
> 
> APPLY.md
> MANIFEST.md
> 
> <новые или заменяемые файлы проекта>
> 
> _archive-review/
>   <archive-slug>/
>     ORIGINALS-INDEX.md
>     MERGE-RISK-REPORT.md
>     original-files/
>       planning/slices/README.md
>       planning/slices/client/CLIENT-SLICE-TEMPLATE.md
>       ...
>     raw-author-message-log.md
>     derived-decisions.md
> 
> То есть если архив заменяет:
> 
> planning/slices/README.md
> 
> то он также кладёт копию старого файла сюда:
> 
> _archive-review/<archive-slug>/original-files/planning/slices/README.md
> 
> Так после применения архива можно сравнить:
> 
> новый файл
> vs
> сохранённый original
> 
> и понять, что потерялось.
> 
> Шаг 2 — Merge correction archive
> 
> После применения первого архива мы делаем проверку:
> 
> - какие новые файлы норм;
> - какие replacement files потеряли информацию;
> - какие original sections надо вернуть;
> - какие docs стали короче/беднее;
> - какие файлы надо дообновить;
> - какие backup/originals можно оставить временно или потом удалить.
> 
> Второй архив уже меньше:
> 
> исправляет только проблемные файлы
> не тащит всё заново
> возвращает потерянные guardrails
> обновляет merge report/status
> Главное правило
> Если архив заменяет существующий файл, он обязан сохранить original copy в уникальной archive-review папке.
> 
> Исключение только для файлов, которые точно новые.
> 
> Уникальное имя папки
> 
> Нужно использовать уникальный slug архива:
> 
> _archive-review/source-sync-scenario-domain-slice-refactoring-workflow-2026-05-18-v1/
> 
> или компактнее:
> 
> _archive-review/source-sync-refactor-v1/
> 
> Но лучше с датой/версией:
> 
> _archive-review/2026-05-18-source-sync-refactor-v1/
> 
> Тогда разные архивы не будут перетирать друг друга.
> 
> Что должно быть в ORIGINALS-INDEX.md
> 
> Пример:
> 
> # Originals Index
> 
> Archive: 2026-05-18-source-sync-refactor-v1  
> Purpose: source-sync/refactoring workflow docs  
> Status: original files preserved for post-apply merge review
> 
> ## Replaced files with preserved originals
> 
> | Project file replaced | Original copy | Risk | Review note |
> |---|---|---|---|
> | `planning/slices/README.md` | `original-files/planning/slices/README.md` | high | May contain existing guardrails/navigation |
> | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md` | `original-files/planning/slices/client/CLIENT-SLICE-TEMPLATE.md` | high | Must preserve visual/CSS/a11y/test sections |
> | `planning/slices/slice-test-plan-workflow.md` | `original-files/planning/slices/slice-test-plan-workflow.md` | high | Must preserve Behavior-to-Test examples |
> | `planning/slices/SLICE-INDEX.md` | `original-files/planning/slices/SLICE-INDEX.md` | medium | Navigation may be incomplete after replacement |
> Что должно быть в MERGE-RISK-REPORT.md
> 
> Пример:
> 
> # Merge Risk Report
> 
> ## High-risk replacements
> 
> ### planning/slices/client/CLIENT-SLICE-TEMPLATE.md
> 
> Reason:
> - existing file has detailed visual/layout/CSS/a11y/test sections;
> - archive adds source-sync snapshot;
> - risk: new file may accidentally remove existing sections.
> 
> Required post-apply check:
> - confirm Visual UI / Scenario Flow still exists;
> - confirm Visual Layout / Screen Composition still exists;
> - confirm Styling / CSS Ownership still exists;
> - confirm Behavior-to-Test Trace still exists;
> - confirm Source / Domain / Slice Coverage Snapshot was added.
> 
> ### planning/slices/README.md
> 
> Reason:
> - existing file may contain legacy navigation and Agreement Exchange guardrails.
> 
> Required post-apply check:
> - confirm L1/L2 legacy rule preserved;
> - confirm Agreement Exchange guardrails preserved;
> - confirm StartReview entry point rule preserved;
> - confirm source-sync links added.
> Архивы надо классифицировать
> 
> Перед созданием архива нужно явно делить файлы на категории.
> 
> New files:
>   безопасно добавить
> 
> Replacement files:
>   риск потери информации, нужен original snapshot
> 
> Deprecated redirect files:
>   заменяют старый файл коротким redirect, нужен original snapshot
> 
> Generated/runtime files:
>   обычно не трогаем в docs archive
> 
> Cleanup/delete:
>   не делаем через обычный zip, только отдельным явным cleanup step
> Новое правило перед архивом
> 
> Перед генерацией архива чат должен вывести:
> 
> Archive plan:
> 
> New files:
> - ...
> 
> Files to replace:
> - ...
> 
> Original snapshots to include:
> - ...
> 
> High-risk files:
> - ...
> 
> Expected post-apply review:
> - ...
> 
> Если есть high-risk replacement, архив автоматически получает:
> 
> _archive-review/<unique-slug>/original-files/
> _archive-review/<unique-slug>/ORIGINALS-INDEX.md
> _archive-review/<unique-slug>/MERGE-RISK-REPORT.md
> Что делать с уже применённым архивом
> 
> Если архив уже применён и мы не сохранили originals внутри него, тогда приходится сравнивать с GitHub/current branch/history или с локальными копиями. Но дальше лучше не повторять эту ошибку.
> 
> Для следующих больших docs archives правило такое:
> 
> small archive, mostly additive:
>   можно без originals, если ничего не заменяет
> 
> large archive или меняет существующие docs:
>   обязательно сохранять originals
> 
> archive touches templates/workflows/index/readme:
>   всегда high-risk, originals обязательны
> Обновлённая структура будущего архива
> APPLY.md
> MANIFEST.md
> 
> planning/source-sync/...
> planning/diagrams/...
> planning/domain/...
> planning/slices/...
> 
> _archive-review/
>   2026-05-18-source-sync-refactor-v1/
>     ORIGINALS-INDEX.md
>     MERGE-RISK-REPORT.md
>     original-files/
>       planning/slices/README.md
>       planning/slices/SLICE-INDEX.md
>       planning/slices/client/CLIENT-SLICE-TEMPLATE.md
>       planning/slices/server/SERVER-SLICE-TEMPLATE.md
>       ...
>     raw-author-message-log.md
>     derived-decisions.md
> Как это связано с двухшаговым процессом
> Первый архив
> 
> Цель:
> 
> внести новую структуру/новые правила
> не потерять originals
> создать материал для review
> Проверка после применения
> 
> Чат смотрит:
> 
> project file
> vs
> _archive-review/.../original-files/project file
> 
> и выдаёт:
> 
> что потеряно
> что надо вернуть
> что стало лучше
> что конфликтует
> какие файлы требуют второго архива
> Второй архив
> 
> Цель:
> 
> точечно исправить merge issues
> вернуть потерянную информацию
> обновить финальные docs
> Короткая формула
> Большой архив не должен быть финальным merge.
> Большой архив = safe staging + originals + risk report.
> Финализация = второй меньший archive после сравнения.
> 
> Это сильно уменьшит боль: мы перестанем гадать, что архив потерял, потому что старые версии будут лежать рядом и будет индекс, что именно сравнивать.

Notes:
- Store raw only.
- This message introduced the safe archive merge workflow with preserved originals, originals index and merge risk report.
