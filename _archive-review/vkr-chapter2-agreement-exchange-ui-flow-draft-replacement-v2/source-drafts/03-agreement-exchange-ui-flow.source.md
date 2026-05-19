Добавлять потом сюда:

```text
planning/thesis/vkr-topic-workbench/
└─ 03-chapter-2-design/
   └─ 06-user-interface/
      └─ 03-agreement-exchange-ui-flow.topic.md
```

Полный путь:

```text
planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/03-agreement-exchange-ui-flow.topic.md
```

---

# Тема: UI-flow договорного обмена

**Статус:** быстрый topic-драфт v1 / блок 2.6.3 / нужен repo-check по экрану exchange, proposal history, upload/download UI, sender side, availableActions, accept/counter actions, file errors.

---

## 1. Зачем тема нужна

Эта тема объясняет, как пользовательский интерфейс поддерживает договорный этап после одобрения заявки.

Главная мысль:

> UI договорного обмена должен показывать историю версий договорного предложения, отправителя каждой версии, прикреплённый файл и доступные действия. Frontend отображает версии и запускает upload/download через API, но не управляет состоянием exchange напрямую и не работает со storage path.

---

## 2. Когда появляется договорный обмен

Договорный обмен появляется после одобрения заявки.

Flow:

```text
заявка одобрена
→ сотрудник начинает договорный обмен
→ создаётся exchange
→ сотрудник отправляет первую версию предложения
→ клиент видит версию и файл
→ стороны продолжают обмен версиями, если это поддержано моделью
```

Тезис:

> Договорный обмен не является отдельным процессом без заявки. Он связан с одобренной заявкой и отображается в UI только в контексте этой заявки.

---

## 3. Экран договорного обмена

Экран может показывать:

```text
данные заявки;
статус договорного обмена;
историю версий;
отправителя версии;
дату отправки;
комментарий, если есть;
originalFileName;
кнопку скачать файл;
кнопку отправить новую версию, если доступно;
ошибки upload/download.
```

Не показывать:

```text
storage path;
storage key;
server directory;
internal file name;
служебные поля storage;
чужие документы без доступа.
```

Тезис:

> В интерфейсе договорный файл отображается как документ версии предложения, а не как физический путь в файловом хранилище.

---

## 4. История версий

История версий нужна, чтобы пользователь видел последовательность обмена.

Версия может показывать:

```text
номер или порядок версии;
кто отправил: клиент или сотрудник;
когда отправил;
имя файла;
комментарий;
статус версии, если есть;
доступные действия.
```

Важно:

```text
новый файл после отправки = новая версия;
старый файл не должен незаметно заменяться;
UI должен показывать историю, а не только последний файл.
```

Тезис:

> История версий помогает сохранить прозрачность договорного обмена: пользователь видит, какие документы и в какой последовательности отправлялись сторонами.

---

## 5. Отправка версии предложения

Upload версии — command-flow.

```text
пользователь выбирает файл
→ frontend показывает форму отправки версии
→ frontend отправляет upload/send proposal command
→ backend проверяет доступ и состояние exchange
→ backend сохраняет physical file + metadata
→ backend создаёт proposal version
→ frontend обновляет историю версий
```

Frontend отвечает за:

```text
выбор файла;
показ имени файла;
локальную проверку пустого файла;
loading state;
показ результата;
обновление history.
```

Backend отвечает за:

```text
access check;
проверку допустимости новой версии;
создание proposal version;
сохранение physical file;
создание metadata;
возврат command result или error.
```

Тезис:

> Отправка новой версии предложения в UI является запуском command API. Frontend не создаёт версию самостоятельно и не передаёт storage key.

---

## 6. Получение файла

Download-flow:

```text
пользователь нажимает “Скачать”
→ frontend отправляет documentId на backend
→ backend ищет metadata
→ backend проверяет доступ через proposal/exchange/request
→ backend читает physical file
→ frontend получает файл или safe error
```

UI показывает:

```text
кнопку скачать;
loading;
file unavailable;
forbidden/not found;
safe error message.
```

Тезис:

> Скачивание файла в UI выполняется через backend API. Интерфейс не должен использовать прямой public URL к файлу.

---

## 7. AvailableActions в договорном обмене

Read DTO exchange может содержать действия:

```text
canSendProposalVersion;
canDownloadDocument;
canAcceptProposal;
canSendCounterProposal;
canCloseExchange;
```

Осторожно:

```text
accept/counter/close не утверждать без repo-check;
точные действия проверить;
availableActions могут устареть;
backend всегда проверяет command повторно.
```

Тезис:

> AvailableActions помогают показать пользователю доступные действия в договорном обмене, но не заменяют серверную проверку состояния exchange.

---

## 8. Клиентский UI договорного обмена

Клиент видит:

```text
договорный обмен по своей заявке;
версии предложения;
файлы версий;
отправителя версии;
действия клиента, если они доступны.
```

Клиент не видит:

```text
чужие exchange;
storage path;
служебные данные сотрудника;
внутренние статусы backend;
действия сотрудника, если они не предназначены клиенту.
```

Тезис:

> Клиентский UI договорного обмена должен быть связан только с его заявкой и показывать понятную историю предложений и документов.

---

## 9. Сотруднический UI договорного обмена

Сотрудник видит:

```text
заявку;
договорный exchange;
историю версий;
версии клиента;
кнопку отправить версию предложения;
файлы и metadata;
действия сотрудника.
```

Сотрудник запускает:

```text
start exchange;
send proposal version;
upload file;
download file.
```

Тезис:

> Для сотрудника экран договорного обмена является продолжением карточки одобренной заявки и позволяет сопровождать обмен версиями предложения.

---

## 10. Ошибки UI

UI должен обрабатывать:

```text
validation error при upload;
file too large, если есть ограничение;
unsupported file type, если есть ограничение;
upload failed;
file unavailable;
forbidden/not found;
ActionNoLongerAvailable;
exchange state changed;
unexpected error.
```

Тезис:

> Ошибки договорного обмена должны отображаться пользователю безопасно и понятно, без раскрытия internal path, storage key и деталей чужих документов.

---

## 11. Таблица для ВКР

| UI-сценарий      | Frontend                    | Backend                           |
| ---------------- | --------------------------- | --------------------------------- |
| Открыть exchange | запрашивает read DTO        | возвращает exchange + versions    |
| Показать версии  | отображает history          | отдаёт proposal versions          |
| Показать файл    | показывает originalFileName | отдаёт metadata                   |
| Отправить версию | upload/send command         | создаёт version + metadata + file |
| Скачать файл     | download by documentId      | metadata lookup + access check    |
| Stale action     | показывает ошибку и refresh | business-state error              |
| File unavailable | показывает safe error       | не раскрывает storage path        |

Подпись:

```text
Таблица 2.x — UI-flow договорного обмена
```

---

## 12. Визуалы

```text
1. Схема — Заявка approved → agreement exchange → proposal versions.
2. Схема — Экран договорного обмена: history + file actions.
3. Схема — Upload новой версии через command API.
4. Схема — Download файла через backend.
5. Схема — Новый файл = новая версия, не overwrite.
6. Таблица — UI-сценарии договорного обмена.
```

---

## 13. Черновой текст

Договорный обмен отображается в интерфейсе после одобрения заявки. Он связан с конкретной заявкой и показывает пользователю историю версий договорного предложения. Каждая версия имеет отправителя, дату отправки и прикреплённый документ.

Для клиента экран договорного обмена показывает документы, относящиеся к его заявке, и доступные действия. Для сотрудника этот экран является продолжением обработки одобренной заявки и позволяет отправлять версии договорного предложения.

Отправка версии выполняется через command API. Frontend выбирает файл и отправляет его на backend, а backend проверяет доступ, сохраняет physical file, создаёт metadata и связывает документ с новой версией предложения.

Получение файла также выполняется через backend. Интерфейс не получает storage path или storage key, а запрашивает файл по documentId. Backend проверяет доступ через metadata, версию предложения, договорный обмен и заявку.

---

## 14. Repo-check

```text
- есть ли экран agreement exchange;
- где он находится в routes;
- как UI открывает exchange после approved request;
- есть ли proposal history;
- какие поля proposal version показываются;
- есть ли senderSide;
- есть ли originalFileName;
- есть ли documentId;
- есть ли upload UI;
- есть ли download UI;
- есть ли accept/counter actions;
- как показываются availableActions;
- как UI обновляет history после upload;
- как UI обрабатывает file errors;
- раскрывается ли storage path/key в UI;
- есть ли direct public file URL.
```

---

## 15. Итоговая формула

```text
UI договорного обмена появляется после одобрения заявки;

он показывает exchange,
историю версий,
отправителя,
дату,
file metadata
и доступные действия;

новая версия отправляется через command API;

frontend выбирает файл,
но backend создаёт proposal version,
metadata
и сохраняет physical file;

download идёт через backend по documentId;

UI не показывает storage path,
storage key
или public uploads URL;

availableActions помогают показать кнопки,
но backend проверяет действие повторно;

новый файл после отправки должен отображаться как новая версия,
а не как перезапись старого документа.
```

Остался последний быстрый UI-драфт:

```text
planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/04-ui-states-validation-and-errors.topic.md
```
