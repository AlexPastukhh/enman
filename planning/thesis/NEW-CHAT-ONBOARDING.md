# New Chat Onboarding for VKR Work

Статус: обязательная памятка для новых чатов

Если чат потерял контекст или начинает работать с ВКР заново, сначала читать этот файл.

## 1. Что сейчас строится

Мы готовим ВКР по теме:

> Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО «ЗСК».

Цель текущего workflow — не сразу писать ПЗ, а управляемо переходить от topic-драфтов к section-драфтам.

## 2. Обязательные входы

Читать в таком порядке:

```text
1. planning/thesis/README.md
2. planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
3. planning/thesis/vkr-topic-workbench/README.md
4. planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
5. planning/thesis/vkr-topic-workbench/00-workflow-and-rules/README.md
6. active chapter README
7. topic-index.md
8. нужный .topic.md
9. связанные research / visual / repo-check файлы
10. section draft, если он уже есть
```

## 3. Главное различие файлов

```text
planning/ — рабочая инженерная кухня проекта.
vkr-topic-workbench/ — смысловая база тем.
vkr-clean/section-drafts/ — текстовые черновики подразделов.
vkr-clean/ — clean layer для материалов ВКР.
```

Не копировать planning напрямую в ПЗ.

## 4. Основной workflow

```text
topic draft
↔ вопросы / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Не ждать, пока topic-драфт станет идеальным. Если отдельный блок темы уже понятен, можно создавать соответствующий блок section draft и постепенно его заполнять.

## 5. Что нельзя делать

```text
- не писать финальный текст ВКР из воздуха;
- не использовать AI/chats/prompts в тексте ПЗ;
- не использовать L1/L2 как язык ВКР;
- не писать “реализовано” без repo/evidence check;
- не называть mock-проверку реальной внешней интеграцией;
- не называть договорный обмен юридически значимым подписанием;
- не заявлять ЭДО/ЭП/внешние сервисы без проверки;
- не писать новые драфты в legacy/support папки;
- не удалять legacy без отдельного cleanup архива.
```

## 6. Как действовать при неопределённости

Если непонятно, active ли папка или файл:

```text
1. проверить README текущей папки;
2. проверить topic-index.md;
3. проверить VKR-WORKFLOW-SOURCE-OF-TRUTH.md;
4. спросить пользователя, если active/support статус всё ещё неясен;
5. не переписывать и не удалять по памяти.
```

## 7. Как писать про реализацию

Для главы 3 обязательно смотреть:

```text
код;
tests;
screenshots/demo;
slice drafts;
scenario specs;
DATA;
domain drafts;
ADR/решения;
questions/decisions.
```

Глава 3 — доказательная глава. В ней нельзя опираться только на красивый topic-драфт.

## 8. Как писать про research

Research не вставляется буквально. Нужно сформулировать вопрос темы, найти ответ в research, переработать его под проект и вставить в нужный блок section draft.

## 9. Команды пользователя

Если пользователь пишет `тчт`, значит нужно сохранить **предыдущий уже данный ответ** в `.txt` и дать ссылку на файл.

Если пользователь просит архив, использовать safe-merge/archive алгоритм из `planning/thesis/chat-action-algorithms/archive-generation-and-navigation-update.md`.
