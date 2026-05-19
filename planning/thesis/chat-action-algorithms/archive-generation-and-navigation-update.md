# Algorithm: Archive Generation and Navigation Update

Когда пользователь просит создать архив с обновлением docs/workflow/planning:

## Обязательные действия

```text
1. Проверить текущий repo/archive, а не работать по памяти.
2. Определить add/replace/deprecate/delete candidates.
3. Не удалять legacy/support файлы смешанным архивом.
4. Для replacement-файлов сохранить originals в `_archive-review/<slug>/original-files/`.
5. Обновить navigation/source-of-truth в том же архиве.
6. Если добавлена папка, создать README или указать её в навигации.
7. Если меняется active/legacy статус, явно отметить это в README.
8. Создать MANIFEST и APPLY с понятными командами.
9. Дать пользователю ссылку на архив и краткое резюме.
```

## Navigation минимум

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-topic-workbench/00-workflow-and-rules/README.md
planning/thesis/vkr-clean/README.md, если меняется clean layer
```

## Нельзя

```text
- перезаписывать пользовательские изменения без originals;
- удалять legacy без отдельного cleanup;
- делать архив, который меняет workflow, но не меняет navigation;
- утверждать, что repo проверен, если архив не был прочитан.
```
