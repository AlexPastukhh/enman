# Тема: API-контракт и взаимодействие frontend/backend

**Статус:** полный topic-драфт v2 / первый драфт блока 2.5 / обновлено: добавлены scenario-based API contract, API ≠ CRUD, command intention вместо status update, route id vs body, screen-specific read DTO, command result DTO, stale availableActions, repeated commands, concurrency conflicts, stable codes/labels, pagination/filter/sort, API contract ownership, controlled API changes, API anti-patterns / нужен repo-check по OpenAPI, generated types/constants, DTO, ProblemDetails, error codes, read/command API, upload/download endpoints, validation errors, access responses и фактическим API-flow / готов к text generation и visual bridge

---

## 0. Служебное назначение драфта

Этот файл является **рабочей карточкой темы**, а не финальным текстом ВКР.

В финальный текст ВКР можно переносить очищенный материал из разделов:

```text id="7s12ty"
5. Сценарная основа API
6. Содержание темы
7. Группы API
8. Read API и command API
9. Граница решений frontend/backend
10. DTO и domain model
11. Типы DTO в API
12. Контракт ошибок и валидации
13. API для файлов
14. Нежелательные решения API
15. Что требует обоснования
16. Визуальные материалы
17. Черновые фрагменты будущего текста
```

Служебные разделы не переносить напрямую:

```text id="npwux4"
repo-check;
матрица вопросов;
visual bridge;
что проверить по repo;
что отправить визуальному чату;
точные endpoint names без проверки;
точные DTO fields без проверки;
формулировки “не доделано”;
старые use-case labels L1/L2/L3;
устаревшие ContractDraft / ContractDraftSent без нормализации.
```

---

## 1. Карта размещения

```text id="d7wjf9"
ВКР
├─ Глава 2. Проектирование web-приложения
│  ├─ 2.1 Требования, роли и основные сценарии системы
│  ├─ 2.2 Проектирование предметной модели и жизненных циклов
│  ├─ 2.3 Проектирование архитектуры web-приложения
│  ├─ 2.4 Проектирование хранения данных и файлов
│  ├─ 2.5 Проектирование API и взаимодействия frontend/backend
│  │  ├─ 2.5.1 Текущая тема: API-контракт и взаимодействие frontend/backend
│  │  ├─ 2.5.2 Read API и command API
│  │  ├─ 2.5.3 Контракт валидации и ошибок
│  │  └─ 2.5.4 API загрузки и получения файлов
│  └─ 2.6 Проектирование пользовательского интерфейса
└─ Глава 3. Реализация и тестирование web-приложения
```

Рабочая папка:

```text id="je4zie"
planning/thesis/vkr-topic-workbench/
└─ 03-chapter-2-design/
   └─ 05-api-frontend-backend/
      ├─ 01-api-contract-overview.topic.md
      ├─ 02-read-and-command-api.topic.md
      ├─ 03-validation-and-error-contract.topic.md
      └─ 04-file-api-upload-download.topic.md
```

---

## 2. Что это за тема

Эта тема открывает блок **2.5 “Проектирование API и взаимодействия frontend/backend”** и объясняет, как frontend и backend договариваются о запросах, ответах, ошибках, действиях и файловых операциях.

Предыдущие блоки уже определили:

```text id="l53xpo"
2.1 — роли и пользовательские сценарии;
2.2 — предметную модель и жизненные циклы;
2.3 — архитектуру frontend/backend/application/domain/storage;
2.4 — хранение structured data, metadata и physical files.
```

Текущая тема отвечает на вопрос:

```text id="tmdymp"
как frontend должен вызывать backend;
какие данные backend должен возвращать;
как отличать чтение от изменения состояния;
как обрабатывать ошибки;
как передавать файлы;
как не раскрывать frontend внутреннюю domain model;
как не превращать API в CRUD-обёртку над таблицами;
как выразить через API пользовательские сценарии.
```

Главная мысль:

> API-контракт является сценарной границей между frontend и backend. Frontend не работает напрямую с БД, domain layer и файловой системой, а обращается к backend через согласованные read и command операции. Read API готовит данные для экранов, command API принимает намерение пользователя выполнить действие, DTO не раскрывают внутреннюю domain model, а ошибки и validation results возвращаются в едином структурированном формате.

---

## 3. Важное уточнение: API-контракт — это не просто список endpoints

API-контракт не должен описываться только как набор URL.

Он включает:

```text id="8xf7ao"
структуру запросов;
структуру ответов;
DTO для чтения;
DTO для команд;
результаты команд;
правила именования полей;
коды и типы ошибок;
validation errors;
auth/access responses;
available actions;
file upload/download rules;
семантику read и command operations;
правила изменения состояния;
стабильные codes и display labels;
границы сокрытия domain/storage деталей.
```

Неправильно:

```text id="t5elvn"
API — это просто список endpoints;
frontend знает внутреннюю domain model;
frontend сам решает, можно ли выполнить команду;
frontend отправляет новый status как свободное значение;
ошибка — это просто текст;
файл можно получить напрямую по пути;
DTO копирует entity из backend;
read endpoint неожиданно меняет состояние.
```

Правильно:

```text id="pmmflv"
API задаёт устойчивую границу frontend/backend;
frontend получает DTO, удобные для сценария;
backend скрывает domain/internal storage details;
command выражает намерение пользователя;
command повторно проверяет доступ и domain rules;
ошибки структурированы;
файлы проходят через backend и access check.
```

Рабочая формулировка:

> API-контракт фиксирует не только технические адреса запросов, но и смысл взаимодействия frontend/backend: какие операции читают данные, какие изменяют состояние, какие ошибки возможны и какие данные frontend может безопасно использовать.

---

## 4. Важное уточнение: API как сценарный контракт, а не CRUD

API не должен проектироваться только как CRUD поверх таблиц.

Нежелательный подход:

```text id="eh76zp"
GET /requests
POST /requests
PUT /requests/{id}
DELETE /requests/{id}
```

Для системы важнее сценарные действия:

```text id="1w2zzb"
создать заявку;
начать рассмотрение;
одобрить заявку;
отклонить заявку;
начать договорный обмен;
отправить версию предложения;
получить файл версии.
```

Почему это важно:

```text id="w9c7ie"
пользователь выполняет сценарий, а не редактирует таблицу;
статус является результатом бизнес-действия;
application layer должен проверить доступ и контекст;
domain layer должен проверить допустимость перехода;
frontend не должен сам решать, какой state поставить.
```

Рабочая формулировка:

> API-контракт проектируется вокруг пользовательских сценариев, а не как прямое отражение таблиц базы данных. Команды должны выражать смысл действия пользователя: одобрить заявку, начать рассмотрение, отправить версию предложения, а не просто изменить поле статуса.

---

## 5. Принятые уточнения

1. API является границей между frontend и backend.
2. API выражает пользовательские сценарии, а не только CRUD-операции.
3. Frontend не обращается напрямую к БД.
4. Frontend не обращается напрямую к файловой системе.
5. Frontend не должен знать внутреннюю domain model.
6. DTO ответа не равен domain model.
7. Read API и command API имеют разную ответственность.
8. Read API готовит данные для UI.
9. Command API запускает изменение состояния.
10. Command API принимает намерение пользователя, а не готовое новое состояние объекта.
11. Frontend не должен отправлять новый статус заявки как свободное поле.
12. Статус является результатом выполнения команды на backend.
13. Command API должен проходить через application layer и domain layer.
14. В command API идентификатор объекта можно передавать в route, а параметры действия — в body.
15. Read DTO может быть screen-specific.
16. Список заявок, карточка заявки и история договорного обмена могут использовать разные DTO.
17. Command result DTO не обязан возвращать полную domain model.
18. После команды frontend может обновить экран локально или повторно запросить read DTO.
19. Available actions могут передаваться frontend в read DTO.
20. Available actions не заменяют backend/domain validation.
21. Read DTO может устареть между чтением и командой.
22. Command API обязан повторно проверять состояние объекта.
23. Повторная отправка команды должна обрабатываться безопасно.
24. Конфликт конкурентных действий должен возвращать business-state error, а не generic server error.
25. Frontend validation является подсказкой, а не бизнес-гарантией.
26. Backend request validation проверяет форму входных данных.
27. Application layer проверяет доступ, ownership и сценарий.
28. Domain layer проверяет бизнес-инварианты и допустимость переходов.
29. Ошибки должны быть структурированными.
30. Нужно различать validation errors, access errors, not found errors и business-state errors.
31. Стабильные machine-readable codes нужно отделять от display labels/messages.
32. Frontend-логика должна опираться на стабильные коды, а не на текст сообщения.
33. API не должен раскрывать infrastructure details: storage path, internal storage key, ORM errors, stack trace.
34. Нужно различать 401, 403 и 404.
35. Для защищённых объектов иногда безопаснее не раскрывать факт существования чужого ресурса.
36. File upload и file download — разные API-сценарии.
37. Download файла должен идти через metadata и access check.
38. Public static file access для договорных файлов не является основной моделью.
39. API не должен раскрывать internal storage path.
40. Read API для списков желательно проектировать с учётом pagination/filter/sort.
41. API-контракт должен быть единым источником согласования статусов, ошибок, available actions и file metadata.
42. Изменение API-контракта должно быть контролируемым.
43. Можно упоминать ProblemDetails как механизм единого формата ошибок, если repo-check подтвердит.
44. Можно упоминать OpenAPI как способ описания API-контракта, если repo-check подтвердит.
45. Можно упоминать generated types/constants как способ синхронизации frontend/backend, если repo-check подтвердит.
46. Старые use-cases используются только как сценарная карта.
47. Не переносить в API старые термины `ContractDraft`, `ContractDraftSent`, email-flow и PDF generation без repo-check.
48. Точные endpoint names, routes, DTO поля и response codes нужно проверить по repo.

---

## 6. Сценарная основа API

API должен поддерживать пользовательские сценарии, а не существовать как технический слой сам по себе.

Основные группы сценариев:

```text id="c6x9j8"
1. Авторизация и определение роли пользователя.
2. Работа клиента с заявителями.
3. Создание и просмотр клиентских заявок.
4. Просмотр заявок сотрудником.
5. Начало рассмотрения заявки.
6. Проверка данных заявки.
7. Одобрение или отклонение заявки.
8. Начало договорного обмена.
9. Просмотр истории версий предложения.
10. Отправка версии договорного предложения.
11. Загрузка файла договорного предложения.
12. Получение файла через backend.
```

Для API это означает:

```text id="uec0lu"
нужны read endpoints для списков и карточек;
нужны command endpoints для изменения состояния;
нужны DTO для frontend-экранов;
нужны command result DTO;
нужны error contracts;
нужны validation contracts;
нужны upload/download сценарии;
нужна проверка access/ownership на backend;
нужны стабильные codes для статусов, действий и ошибок.
```

Рабочая формулировка:

> API проектируется от сценариев системы. Для отображения экранов frontend запрашивает данные через read API, а для изменения состояния заявки, рассмотрения или договорного обмена запускает command API.

---

## 7. Зачем эта тема нужна

### 7.1 На уровне темы

Тема нужна, чтобы связать архитектуру и UI через устойчивую границу.

Без явного API-контракта возникают проблемы:

```text id="es2m9z"
frontend начинает зависеть от внутренней domain model;
frontend отправляет новый status напрямую;
ошибки обрабатываются по тексту;
разные экраны по-разному понимают статусы;
available actions вычисляются хаотично;
read и command flows смешиваются;
validation errors трудно показать в форме;
файлы могут начать отдаваться небезопасно;
storage path может попасть во frontend;
изменение backend ломает frontend.
```

API-контракт решает это:

```text id="ltzpp5"
фиксирует запросы и ответы;
отделяет DTO от domain model;
выражает сценарные команды;
задаёт единый формат ошибок;
задаёт единый формат validation errors;
позволяет generated types/constants;
позволяет frontend показывать available actions;
оставляет command validation на backend;
защищает file access через backend;
скрывает storage/domain implementation details.
```

### 7.2 На уровне пункта 2.5

Пункт 2.5 должен раскрыть:

```text id="shbd2e"
общую роль API-контракта;
API как сценарную boundary;
read API и command API;
command intention вместо прямого status update;
DTO и отличие от domain model;
валидацию и ошибки;
access responses;
file upload/download API;
связь API с UI-flow;
связь API с application/domain layer;
управляемость изменений API.
```

Текущий драфт закрывает первую часть:

```text id="v4lasg"
общий API-контракт и взаимодействие frontend/backend
```

Следующие драфты 2.5 могут раскрыть:

```text id="6czjch"
02-read-and-command-api.topic.md
03-validation-and-error-contract.topic.md
04-file-api-upload-download.topic.md
```

### 7.3 На уровне всей главы 2

API связывает:

```text id="376ajm"
2.1 сценарии
→ 2.2 domain model
→ 2.3 architecture
→ 2.4 storage
→ 2.5 API
→ 2.6 UI
→ глава 3 implementation/testing
```

---

# 8. Содержание темы

## 8.1 API как граница frontend/backend

Frontend не должен знать внутреннее устройство backend.

Frontend работает с:

```text id="ubm56u"
URL/route или client method;
request DTO;
response DTO;
command result DTO;
error response;
status code;
available actions;
file response, если это download.
```

Backend скрывает:

```text id="b7yga8"
domain entities;
internal state transitions;
database schema;
storage path;
storage key;
file directory;
implementation services;
ORM details;
internal exception messages.
```

Рабочая формулировка:

> API является границей между пользовательским интерфейсом и серверной частью. Frontend получает только те данные, которые нужны для конкретного сценария, а внутренняя domain model, схема БД и файловое хранилище остаются деталями backend.

---

## 8.2 API как сценарный контракт, а не CRUD

API должен выражать действия пользователя.

Сценарные команды:

```text id="u2lirf"
CreateConnectionRequest;
StartReview;
RunDataCheck;
ApproveRequest;
RejectRequest;
StartAgreementExchange;
SendAgreementProposalVersion;
DownloadAgreementDocument.
```

Эти команды лучше, чем универсальная операция:

```text id="xw7c4b"
UpdateRequestStatus
```

Почему:

```text id="om8hqm"
название команды выражает бизнес-смысл;
application layer понимает сценарий;
domain layer проверяет конкретный переход;
frontend не выбирает произвольное состояние;
ошибки становятся понятнее.
```

Рабочая формулировка:

> В проектируемой системе API должен выражать сценарии, а не только операции изменения записей. Например, “одобрить заявку” является отдельным действием, потому что оно имеет собственные правила доступа, проверки состояния и результат.

---

## 8.3 Read API

Read API нужен для получения данных, которые frontend отображает пользователю.

Примеры read operations:

```text id="g0hqaq"
получить список заявителей;
получить список заявок клиента;
получить список заявок сотрудника;
получить карточку заявки;
получить результат рассмотрения;
получить договорный обмен;
получить список версий предложения;
получить metadata файла для отображения.
```

Read API может возвращать:

```text id="42htvr"
status;
statusLabel;
reviewState;
decision;
rejectionReason;
availableActions;
canApprove;
canReject;
canStartAgreementExchange;
proposalVersions;
originalFileName;
uploadedAt;
senderSide.
```

Рабочая формулировка:

> Read API ориентирован на отображение данных. Он может возвращать frontend не полную domain model, а DTO, подготовленные для списка, карточки или истории договорного обмена.

---

## 8.4 Screen-specific read DTO

Read DTO может быть спроектирован под конкретный экран, а не под внутреннюю сущность.

Для employee dashboard DTO может содержать:

```text id="cmh1dj"
requestId;
applicantName;
requestStatus;
reviewState;
startedByCurrentEmployee;
availableActions;
createdAt.
```

Для request details DTO может содержать:

```text id="zh3j4r"
requestId;
full applicant data;
object address;
request details;
review info;
decision info;
availableActions.
```

Для agreement exchange DTO может содержать:

```text id="q4e5tk"
exchangeId;
requestId;
exchangeStatus;
proposalVersions;
senderSide;
originalFileName;
uploadedAt;
availableActions.
```

Рабочая формулировка:

> Read DTO проектируется под потребности экрана. Список заявок, карточка заявки и история договорного обмена могут использовать разные DTO, потому что frontend на этих экранах решает разные задачи.

---

## 8.5 Command API

Command API запускает сценарии изменения состояния.

Примеры command operations:

```text id="l6mz65"
создать заявителя;
создать заявку;
начать рассмотрение;
запустить проверку данных;
одобрить заявку;
отклонить заявку;
начать договорный обмен;
отправить версию предложения;
загрузить файл версии;
принять предложение, если это подтверждено моделью.
```

Command API должен:

```text id="ogqdu2"
проверить авторизацию;
проверить request DTO;
передать действие в application layer;
проверить access/ownership;
вызвать domain layer;
сохранить результат;
вернуть success или structured error.
```

Рабочая формулировка:

> Command API не должен просто записывать переданные frontend данные в БД. Он запускает серверный сценарий, в котором application layer проверяет доступ и вызывает domain layer для применения бизнес-правил.

---

## 8.6 Command API не принимает состояние напрямую

Frontend не должен отправлять backend-у новый статус заявки или договорного обмена как свободное значение.

Плохо:

```json
{
  "requestId": 15,
  "status": "Approved"
}
```

Лучше:

```http
POST /requests/{requestId}/approve
```

Body:

```json
{}
```

Почему:

```text id="fe0ymc"
frontend не должен решать, можно ли перейти в Approved;
backend сам знает текущего пользователя;
application layer проверяет доступ и ownership;
domain layer проверяет допустимость перехода;
статус является результатом команды, а не входным параметром.
```

Рабочая формулировка:

> Command API должен принимать намерение пользователя, а не готовое новое состояние объекта. Например, frontend запускает команду “одобрить заявку”, а итоговый статус определяется backend после проверки доступа и бизнес-правил.

---

## 8.7 Route id и body команды

Для command API полезно разделять идентификатор объекта и данные действия.

Пример approve:

```http
POST /requests/{requestId}/approve
```

Body:

```json
{}
```

Пример reject:

```http
POST /requests/{requestId}/reject
```

Body:

```json
{
  "reason": "..."
}
```

Смысл:

```text id="gn53yv"
route указывает, над каким объектом выполняется действие;
body содержит данные самого действия;
frontend не дублирует id в нескольких местах;
меньше риска несоответствия route id и body id;
контракт легче читать.
```

Рабочая формулировка:

> В API целесообразно разделять идентификатор объекта и параметры действия. Идентификатор заявки или договорного обмена может находиться в route, а тело запроса содержит только данные, необходимые для выполнения команды.

---

## 8.8 Command result DTO

Command API не всегда должен возвращать полную обновлённую domain model.

Возможные варианты результата:

```text id="n8hq2s"
вернуть только id созданного объекта;
вернуть новый статус;
вернуть compact DTO;
вернуть ссылку на read endpoint;
вернуть structured error.
```

Пример approve result:

```json
{
  "requestId": 15,
  "status": "Approved"
}
```

Пример proposal upload result:

```json
{
  "proposalVersionId": 7,
  "documentId": 22
}
```

Рабочая формулировка:

> Результат команды должен быть достаточным для обновления интерфейса, но не обязан раскрывать внутреннюю domain model. После успешной команды frontend может либо обновить локальный экран, либо повторно запросить read DTO.

---

## 8.9 Available actions в read DTO

Frontend может получать доступные действия от backend.

Пример:

```json
{
  "requestId": "15",
  "status": "InReview",
  "availableActions": ["Approve", "Reject"],
  "canApprove": true,
  "canReject": true,
  "canStartAgreementExchange": false
}
```

Зачем это нужно:

```text id="9gtyt1"
frontend показывает нужные кнопки;
frontend скрывает недоступные действия;
UI становится понятнее;
меньше лишних запросов;
логика отображения согласуется с backend.
```

Но важно:

```text id="yqoy5d"
availableActions не является окончательной проверкой;
command всё равно должен заново проверить действие;
DTO может устареть между чтением и командой.
```

Рабочая формулировка:

> Доступные действия могут передаваться frontend в read DTO, чтобы интерфейс мог корректно показать кнопки. Однако выполнение команды всё равно должно проверяться на backend, потому что состояние объекта могло измениться после получения DTO.

---

## 8.10 Устаревание read DTO и available actions

Read DTO отражает состояние объекта на момент чтения. Между получением DTO и отправкой команды состояние может измениться.

Пример:

```text id="jh4tnq"
сотрудник A открыл заявку;
frontend показал кнопку “Начать рассмотрение”;
сотрудник B начал рассмотрение раньше;
сотрудник A нажал кнопку;
command API должен вернуть business-state error.
```

Возможные ошибки:

```text id="m34rq0"
ActionNoLongerAvailable;
ReviewAlreadyStarted;
RequestAlreadyDecided;
ProposalVersionIsNoLongerActive.
```

Рабочая формулировка:

> Read DTO и available actions помогают frontend отобразить интерфейс, но они могут устареть. Поэтому command API обязан повторно проверить состояние объекта и вернуть структурированную ошибку, если действие уже недоступно.

---

## 8.11 Повторная отправка команд

Некоторые команды могут быть отправлены повторно из-за сетевых ошибок, двойного клика или повторной отправки формы.

Примеры риска:

```text id="z6bo1z"
создать заявку два раза;
отправить одну и ту же версию предложения два раза;
загрузить файл повторно;
повторно одобрить заявку.
```

Для текущего scope не обязательно проектировать сложный idempotency-key, но важно зафиксировать принцип:

```text id="ndheky"
финальные действия должны проверять текущее состояние;
повtext id="ndheторное approve уже approved заявки должно давать понятную ошибку;
создание новых объектов может требовать защиты от повторной отправки;
frontend должен блокировать кнопку во время отправки;
backend всё равно проверяет состояние.
```

Рабочая формулировка:

> API должен учитывать возможность повторной отправки команды. Frontend может блокировать кнопку во время выполнения запроса, но backend всё равно должен защищать бизнес-состояние от повторных или конфликтующих действий.

---

## 8.12 Конфликты конкурентных действий

В системе возможны конфликтующие действия нескольких пользователей.

Примеры:

```text id="oyjgcv"
два сотрудника одновременно пытаются начать рассмотрение одной заявки;
один сотрудник одобряет заявку, другой пытается отклонить;
клиент открывает старое состояние договорного обмена и отправляет устаревшую версию;
одна сторона отправляет новую версию, пока другая работает со старой.
```

API должен возвращать не generic server error, а понятную ошибку состояния:

```text id="cvoisp"
ReviewAlreadyStarted;
RequestAlreadyDecided;
ProposalVersionIsNoLongerActive;
ActionNoLongerAvailable.
```

Рабочая формулировка:

> Конфликт конкурентных действий следует обрабатывать как ошибку бизнес-состояния, а не как технический сбой. Это позволяет frontend показать пользователю понятное сообщение и обновить данные экрана.

---

## 8.13 DTO не равен domain model

DTO создаётся для границы API, а domain model — для бизнес-логики.

DTO может быть:

```text id="yjfsxd"
коротким;
экранно-ориентированным;
содержащим label;
содержащим available actions;
объединяющим данные нескольких domain objects;
не содержащим внутренних domain details.
```

Domain model содержит:

```text id="xpy8cc"
инварианты;
переходы состояний;
предметные методы;
внутренние связи;
правила допустимости действий.
```

Рабочая формулировка:

> DTO ответа не является копией domain model. API должен возвращать данные, необходимые frontend для конкретного сценария, не раскрывая внутреннюю структуру предметной модели.

---

## 8.14 Request DTO и command input

Request DTO должен передавать данные, нужные для действия.

Например, для отклонения заявки:

```text id="pnryvi"
reason, если причина указывается;
служебные поля, если нужны.
```

Но не должен передавать:

```text id="zujv5p"
новый статус как свободную строку от frontend;
роль пользователя;
sender side, если backend может определить её сам;
client id, если он берётся из текущего пользователя;
internal domain fields;
storage path;
storage key.
```

Рабочая формулировка:

> Command DTO должен содержать входные данные действия, но не должен позволять frontend напрямую задавать внутреннее состояние предметного объекта. Например, frontend запускает команду “одобрить заявку”, а не передаёт произвольный новый статус.

---

## 8.15 Validation boundary

Валидация делится на уровни:

```text id="sda8ve"
frontend validation;
backend request validation;
application validation;
domain validation.
```

Frontend validation:

```text id="f6wijg"
помогает заполнить форму;
показывает обязательные поля;
уменьшает очевидные ошибки.
```

Backend request validation:

```text id="fhy9lo"
проверяет структуру запроса;
проверяет required fields;
проверяет формат;
возвращает fieldErrors.
```

Application/domain validation:

```text id="odfzva"
проверяет ownership;
проверяет role;
проверяет состояние объекта;
проверяет business invariant.
```

Рабочая формулировка:

> API-контракт должен учитывать разные уровни валидации. Ошибка заполнения поля, ошибка доступа и ошибка недопустимого состояния заявки имеют разную природу и должны возвращаться frontend в понятном виде.

---

## 8.16 Контракт ошибок

Ошибка должна быть структурированной.

Возможная структура:

```text id="c8qs6n"
status;
type;
title/message;
errorCode;
fieldErrors;
traceId, если используется;
extensions, если используются.
```

Типы ошибок:

```text id="epxomv"
validation error;
unauthorized;
forbidden;
not found;
business state error;
conflict/concurrency error;
file error;
unexpected error.
```

Рабочая формулировка:

> Frontend не должен зависеть от текста ошибки как от единственного признака. API должен возвращать структурированную ошибку, в которой можно отличить ошибку валидации от ошибки доступа или бизнес-состояния.

---

## 8.17 Stable codes и display labels

В API полезно различать:

```text id="d7xnuu"
machine-readable code;
human-readable label/message.
```

Пример статуса:

```json
{
  "status": "InReview",
  "statusLabel": "На рассмотрении"
}
```

Пример ошибки:

```json
{
  "errorCode": "ReviewAlreadyStarted",
  "message": "Заявка уже находится на рассмотрении."
}
```

Смысл:

```text id="a0pqv1"
frontend-логика опирается на stable code;
пользователь видит label/message;
текст можно менять без переписывания логики;
локализация не ломает условные проверки.
```

Рабочая формулировка:

> Для согласованности frontend/backend API может возвращать стабильные коды и отдельные пользовательские подписи. Код используется логикой интерфейса, а текст — для отображения пользователю.

---

## 8.18 401 / 403 / 404

API должен различать:

```text id="z0ga0s"
401 Unauthorized:
пользователь не вошёл в систему;

403 Forbidden:
пользователь вошёл, но не имеет права выполнить действие;

404 Not Found:
объект не найден или не должен быть раскрыт текущему пользователю.
```

Особенно важно для:

```text id="ptvvqo"
чужих заявок;
чужих договорных обменов;
чужих файлов;
metadata документа;
скачивания файла.
```

Рабочая формулировка:

> Для защищённых данных API должен осторожно выбирать ответ. Если пользователь пытается получить чужую заявку или файл, backend может не раскрывать факт существования объекта и возвращать безопасный ответ.

---

## 8.19 Business-state errors

Business-state error возникает, когда запрос технически корректен, но действие нельзя выполнить из-за состояния предметного объекта.

Примеры:

```text id="v32tub"
нельзя одобрить заявку, которая не находится на рассмотрении;
нельзя начать договорный обмен до одобрения заявки;
нельзя отправить версию в закрытом обмене;
нельзя скачать файл, который не связан с доступной версией;
нельзя повторно принять уже принятую версию.
```

Рабочая формулировка:

> Ошибка бизнес-состояния отличается от ошибки формата запроса. Она означает, что данные запроса корректны, но действие недопустимо для текущего состояния предметного объекта.

---

## 8.20 Сокрытие infrastructure details в API

API не должен возвращать frontend:

```text id="w8gk9w"
путь к файлу на сервере;
storage key как основной публичный путь;
имена таблиц;
внутренние enum-значения, если они не являются контрактом;
stack trace;
ORM errors;
internal exception messages;
имя директории файлового хранилища.
```

Frontend должен получать:

```text id="t3rqs1"
documentId;
originalFileName;
download action через backend endpoint;
safe errorCode;
display label;
status code.
```

Рабочая формулировка:

> API должен скрывать инфраструктурные детали backend. Пользовательский интерфейс получает безопасные идентификаторы и DTO, а не пути файловой системы, схему БД или внутренние ошибки приложения.

---

## 8.21 Списки, фильтры и пагинация

Read API для списков заявок и истории договорного обмена должен учитывать, что данных может стать больше.

Для списков можно проектировать:

```text id="g05c1y"
pagination;
filter by status;
sort by date;
search, если нужно;
page size limit.
```

Примеры:

```text id="lnlh2b"
список заявок клиента;
дашборд сотрудника;
история версий договорного предложения.
```

Рабочая формулировка:

> Read API для списков должен поддерживать управляемую выдачу данных. Даже если в учебном scope объём данных небольшой, проектный API лучше рассматривать как список с возможной пагинацией, сортировкой и фильтрацией.

---

## 8.22 API для договорного обмена

API договорного обмена должен поддерживать:

```text id="p7tiqs"
получение текущего exchange;
получение истории версий;
начало exchange после одобрения;
отправку новой версии;
получение available actions;
получение metadata файла;
download файла через backend.
```

Не утверждать без repo-check:

```text id="mghm1f"
accept proposal endpoint;
counter proposal endpoint;
comment endpoint;
email notification endpoint;
PDF generation endpoint;
sign endpoint.
```

Рабочая формулировка:

> API договорного обмена должен отражать lifecycle exchange: получение истории, отправку версии, связь версии с document metadata и безопасное получение файла через backend.

---

## 8.23 API для файлов: upload

Upload API нужен для отправки файла версии договорного предложения.

Upload должен включать:

```text id="yhat8t"
авторизацию;
проверку доступа к exchange;
проверку допустимости новой версии;
приём файла;
сохранение physical file;
создание metadata;
связь metadata с proposal version;
возврат результата.
```

Frontend отправляет:

```text id="x8u0yj"
файл;
данные версии, если нужны;
комментарий, если есть;
идентификатор exchange/proposal context.
```

Backend не должен принимать от frontend:

```text id="flfahx"
storage path;
internal storage key;
sender role как доверенное поле;
final status версии как свободное поле.
```

Рабочая формулировка:

> При upload frontend передаёт файл и данные действия, а backend сам формирует storage key, сохраняет physical file, создаёт metadata и связывает документ с версией предложения.

---

## 8.24 API для файлов: download

Download API должен начинаться с document metadata и проверки доступа.

Flow:

```text id="7rq2fw"
frontend запрашивает файл;
backend проверяет пользователя;
backend находит metadata;
backend определяет proposal/exchange/request;
backend проверяет access;
backend читает physical file по storage key;
backend возвращает файл или safe error.
```

API не должен:

```text id="y782no"
раскрывать physical path;
отдавать файлы из public uploads folder;
разрешать download только по имени файла;
раскрывать существование чужого файла.
```

Рабочая формулировка:

> Download договорного файла должен выполняться через backend. Это нужно, чтобы перед выдачей physical file проверить связь документа с заявкой, договорным обменом и текущим пользователем.

---

## 8.25 API contract ownership

API-контракт должен быть единым источником согласования frontend и backend.

Нельзя, чтобы frontend и backend независимо придумывали:

```text id="qjk5bu"
названия статусов;
коды ошибок;
формат validation errors;
названия available actions;
file metadata fields;
значения enum/code;
формат результата команды.
```

Рабочая формулировка:

> API-контракт должен быть единым источником согласования frontend и backend. Это особенно важно для статусов, кодов ошибок, доступных действий и DTO файловых операций.

---

## 8.26 Изменение API-контракта

При изменении backend важно не ломать frontend неожиданно.

Опасные изменения:

```text id="sq0253"
переименование поля DTO;
изменение значения status code;
удаление errorCode;
изменение формата validation errors;
изменение download flow;
замена stable code без поддержки frontend;
переименование available action.
```

Не нужно подробно заявлять semantic versioning API, если его нет в repo. Достаточно принципа:

```text id="17qdgy"
изменения DTO должны быть согласованы;
ошибки и status codes должны оставаться стабильными;
frontend и backend должны обновляться синхронно;
breaking changes не должны быть случайными.
```

Рабочая формулировка:

> API-контракт должен изменяться контролируемо. Изменения структуры DTO, кодов ошибок и формата валидации влияют на frontend, поэтому такие изменения должны быть согласованы с клиентской частью.

---

## 8.27 OpenAPI и generated types/constants

Если repo-check подтверждает, можно писать:

```text id="b56dzn"
OpenAPI используется для описания API-контракта;
frontend может использовать generated types;
frontend может использовать generated constants;
это снижает рассинхронизацию;
это уменьшает ручное дублирование строковых кодов.
```

Не писать без проверки:

```text id="smfo6p"
OpenAPI полностью покрывает все endpoints;
все DTO генерируются автоматически;
frontend не содержит ручных типов;
все error codes генерируются.
```

Рабочая формулировка:

> Для согласования frontend и backend может использоваться формализованное описание API, например OpenAPI. На его основе могут генерироваться типы или константы, что снижает риск рассинхронизации между клиентской и серверной частями.

---

## 8.28 API и расширяемость

Хороший API-контракт помогает менять внутреннюю реализацию без переписывания UI.

Примеры:

```text id="9rv3ba"
заменить файловую директорию на object storage;
добавить новые error codes;
изменить внутреннюю domain model;
добавить новые проверки;
расширить договорный exchange;
добавить уведомления;
добавить ЭДО/ЭП как отдельный API block.
```

Главное:

```text id="e5nfk8"
frontend работает через стабильный контракт;
backend скрывает internal changes;
domain model не протекает во frontend;
storage details не протекают в DTO.
```

Рабочая формулировка:

> API-контракт снижает связанность frontend и backend. Если backend меняет внутреннюю реализацию хранения или бизнес-правил, frontend не должен переписываться полностью при сохранении согласованного контракта.

---

# 9. Группы API

| Группа API                 | Назначение                     | Примеры операций                       |
| -------------------------- | ------------------------------ | -------------------------------------- |
| Auth / user context        | определить пользователя и роль | login, current user                    |
| Applicants                 | работа с заявителями           | create applicant, list applicants      |
| Requests read              | чтение заявок                  | list my requests, request details      |
| Requests command           | изменение заявки               | create request, start review           |
| Review / decision          | рассмотрение и решение         | approve, reject, run check             |
| Agreement exchange read    | чтение договорного обмена      | exchange details, proposal history     |
| Agreement exchange command | изменение exchange             | start exchange, send proposal          |
| Files upload               | загрузка документа             | upload proposal document               |
| Files download             | получение документа            | download by document ref               |
| Errors/validation          | единый формат ответа           | validation error, forbidden, not found |

Подпись:

```text id="9f5a74"
Таблица 2.x — Основные группы API web-приложения
```

---

# 10. Read API и command API

| Тип API       | Назначение                       | Пример                   | Что важно                             |
| ------------- | -------------------------------- | ------------------------ | ------------------------------------- |
| Read API      | получить данные для UI           | список заявок            | не меняет состояние                   |
| Read API      | показать карточку                | request details          | может вернуть availableActions        |
| Read API      | показать exchange                | proposal history         | DTO удобен для экрана                 |
| Command API   | выполнить действие               | approve request          | проходит через application/domain     |
| Command API   | создать объект                   | create applicant/request | проверяет request DTO                 |
| Command API   | отправить версию                 | send proposal            | проверяет access и state              |
| File upload   | создать document metadata + file | upload proposal file     | не принимает storage path от frontend |
| File download | вернуть physical file            | download document        | проверяет metadata/access             |

Подпись:

```text id="pxo15x"
Таблица 2.x — Разделение read API и command API
```

---

# 11. Граница решений frontend/backend

| Вопрос                        | Frontend                            | Backend                                   |
| ----------------------------- | ----------------------------------- | ----------------------------------------- |
| Какую кнопку показать         | может использовать availableActions | рассчитывает или подтверждает доступность |
| Можно ли выполнить команду    | предварительно скрывает действие    | окончательно проверяет                    |
| Какой статус поставить заявке | не решает                           | решает через application/domain layer     |
| Какой storage key у файла     | не знает                            | генерирует и хранит                       |
| Как показать ошибку поля      | отображает fieldErrors              | формирует validation response             |
| Есть ли доступ к файлу        | не решает окончательно              | проверяет через metadata/request/exchange |
| Какой текст показать          | отображает message/label            | возвращает code/message или code/label    |
| Что делать при stale DTO      | обновляет экран после ошибки        | возвращает business-state error           |
| Что делать при двойном клике  | блокирует кнопку                    | повторно проверяет состояние              |

Подпись:

```text id="fy5qc3"
Таблица 2.x — Граница решений frontend и backend
```

---

# 12. DTO и domain model

| Объект                     | Где используется          | Назначение                                    |
| -------------------------- | ------------------------- | --------------------------------------------- |
| Domain entity              | backend domain layer      | бизнес-правила и инварианты                   |
| Request DTO                | API input                 | принять данные от frontend                    |
| Response DTO               | API output                | отдать данные для экрана                      |
| Read DTO                   | frontend display          | список, карточка, история                     |
| Command DTO                | command input             | данные действия                               |
| Command result DTO         | command output            | результат действия без раскрытия domain model |
| Error DTO / ProblemDetails | error response            | структурированная ошибка                      |
| File metadata DTO          | frontend display/download | имя файла, id, дата, отправитель              |

Подпись:

```text id="jamxfy"
Таблица 2.x — Различие API DTO и domain model
```

---

# 13. Типы DTO в API

| Тип DTO              | Назначение                    | Пример                            |
| -------------------- | ----------------------------- | --------------------------------- |
| Command request DTO  | входные данные действия       | reject reason, proposal comment   |
| Command result DTO   | результат изменения состояния | created id, new status            |
| Read list DTO        | строка списка                 | request row, proposal row         |
| Read details DTO     | карточка объекта              | request details, exchange details |
| File metadata DTO    | отображение документа         | file name, uploadedAt, sender     |
| Validation error DTO | ошибки формы                  | fieldErrors                       |
| Business error DTO   | ошибка состояния              | errorCode, message                |
| Auth/context DTO     | текущий пользователь          | role, account status              |

Подпись:

```text id="irh4l6"
Таблица 2.x — Типы DTO в API
```

---

# 14. Контракт ошибок и валидации

| Тип ошибки                 | Пример                          | Что должен получить frontend    |
| -------------------------- | ------------------------------- | ------------------------------- |
| Validation error           | не заполнено поле               | fieldErrors                     |
| Unauthorized               | пользователь не вошёл           | 401 / auth error                |
| Forbidden                  | нет права выполнить действие    | 403 или safe response           |
| Not found                  | заявка не найдена               | 404                             |
| Business-state error       | нельзя одобрить заявку          | errorCode + message             |
| Concurrency/conflict error | действие уже недоступно         | stable errorCode + refresh hint |
| File not found             | metadata есть, file отсутствует | safe file error                 |
| Upload failed              | файл не сохранён                | errorCode + message             |
| Unexpected error           | сбой сервера                    | safe generic message            |

Подпись:

```text id="q8th3x"
Таблица 2.x — Контракт ошибок API
```

---

# 15. API для файлов

| Сценарий             | Frontend отправляет              | Backend делает                           | Frontend получает                    |
| -------------------- | -------------------------------- | ---------------------------------------- | ------------------------------------ |
| Upload proposal file | file + exchange/proposal context | access check, save file, create metadata | новая версия / metadata / error      |
| Download file        | document id / metadata id        | metadata lookup, access check, file read | physical file / safe error           |
| Show file in UI      | request exchange details         | read metadata                            | originalFileName, uploadedAt, sender |
| File missing         | download request                 | checks metadata/file                     | safe error                           |
| Forbidden file       | download request                 | checks ownership/access                  | safe 403/404                         |

Подпись:

```text id="0yydnb"
Таблица 2.x — API-сценарии загрузки и получения файлов
```

---

# 16. Нежелательные решения API

| Антипаттерн                              | Почему плохо                                     |
| ---------------------------------------- | ------------------------------------------------ |
| Frontend отправляет новый статус         | бизнес-переход оказывается на клиенте            |
| DTO копирует domain entity               | frontend зависит от внутренней модели            |
| Ошибка — только строка текста            | невозможно надёжно обработать в UI               |
| Download по storage path                 | раскрывает внутреннее хранение                   |
| Public URL для договорного файла         | обход access check                               |
| Available actions без command validation | stale UI может выполнить недопустимое действие   |
| Один endpoint “update everything”        | смешивает сценарии и ломает инварианты           |
| Read API мутирует состояние              | нарушает ожидаемую семантику чтения              |
| Command API возвращает stack trace       | раскрывает внутренние детали                     |
| Frontend получает storage key            | infrastructure detail становится public contract |
| Status label используется как logic key  | изменение текста ломает frontend                 |
| Списки без pagination/filter/sort        | плохо масштабируется даже при росте данных       |

Подпись:

```text id="w3was2"
Таблица 2.x — Нежелательные решения API
```

---

# 17. Что требует обоснования

### 17.1 Почему API-контракт — это больше, чем endpoints

Будущая формулировка:

> API-контракт включает не только адреса запросов, но и структуру данных, правила обработки ошибок, формат валидации и смысл операций чтения и изменения состояния.

---

### 17.2 Почему API не должен быть CRUD поверх таблиц

Будущая формулировка:

> Пользовательские действия имеют предметный смысл. Поэтому API должен выражать сценарии: начать рассмотрение, одобрить заявку, отклонить заявку, отправить версию предложения, а не просто менять поля записи в БД.

---

### 17.3 Почему command API принимает intention, а не status

Будущая формулировка:

> Статус заявки или договорного обмена является результатом выполнения команды. Frontend должен отправлять намерение пользователя, а backend должен проверить доступ и бизнес-правила перед изменением состояния.

---

### 17.4 Почему route id и body команды лучше разделять

Будущая формулировка:

> Идентификатор ресурса в route показывает, над каким объектом выполняется действие, а body содержит параметры действия. Это снижает риск рассинхронизации и делает контракт понятнее.

---

### 17.5 Почему read API и command API разделяются

Будущая формулировка:

> Read API подготавливает данные для отображения, а command API запускает изменение состояния. Поэтому команды должны проходить через application layer и domain layer, даже если frontend уже получил available actions.

---

### 17.6 Почему read DTO может быть screen-specific

Будущая формулировка:

> Разные экраны решают разные задачи, поэтому список заявок, карточка заявки и история договорного обмена могут использовать разные DTO. Это позволяет не раскрывать frontend лишнюю внутреннюю модель.

---

### 17.7 Почему command result DTO не равен domain model

Будущая формулировка:

> Результат команды должен быть достаточным для обновления интерфейса, но не обязан возвращать всю domain model. При необходимости frontend может повторно запросить read DTO.

---

### 17.8 Почему available actions не заменяют command validation

Будущая формулировка:

> Available actions помогают frontend отобразить доступные кнопки, но состояние объекта может измениться после чтения. Поэтому command API обязан повторно проверить доступность действия.

---

### 17.9 Почему stale DTO и concurrency нужно учитывать

Будущая формулировка:

> Read DTO отражает состояние на момент чтения. Если другой пользователь изменил объект раньше, command API должен вернуть ошибку бизнес-состояния, а не технический сбой.

---

### 17.10 Почему нужен structured error contract

Будущая формулировка:

> Структурированный формат ошибок позволяет frontend различать ошибки валидации, доступа и бизнес-состояния без зависимости от текста сообщения.

---

### 17.11 Почему stable codes нужно отделять от display labels

Будущая формулировка:

> Frontend-логика должна опираться на стабильные коды, а не на отображаемые тексты. Это позволяет менять пользовательские подписи без поломки UI-логики.

---

### 17.12 Почему 401/403/404 важны

Будущая формулировка:

> Разные статусы доступа требуют разного поведения frontend. Кроме того, для защищённых объектов backend не должен раскрывать факт существования чужих данных.

---

### 17.13 Почему API скрывает infrastructure details

Будущая формулировка:

> API не должен раскрывать frontend пути файловой системы, storage key, ORM errors и stack trace. Эти сведения относятся к внутренней реализации backend и не должны становиться публичным контрактом.

---

### 17.14 Почему upload и download — разные API-сценарии

Будущая формулировка:

> Upload создаёт physical file и metadata, а download находит metadata, проверяет доступ и возвращает physical file. Эти сценарии имеют разные входные данные, ошибки и проверки.

---

### 17.15 Почему download должен идти через metadata

Будущая формулировка:

> Metadata связывает physical file с версией предложения и договорным обменом. Поэтому backend должен сначала определить бизнес-контекст документа, а затем выдать файл.

---

### 17.16 Почему списки лучше проектировать с pagination/filter/sort

Будущая формулировка:

> Даже если учебный объём данных небольшой, списочные API лучше проектировать как управляемую выдачу данных. Это упрощает развитие системы при росте количества заявок и версий.

---

### 17.17 Почему API-контракт должен быть единым источником согласования

Будущая формулировка:

> Статусы, error codes, available actions и file metadata должны быть согласованы между frontend и backend. Иначе разные части системы начнут по-разному понимать один и тот же сценарий.

---

### 17.18 Почему OpenAPI/generated types полезны

Будущая формулировка:

> Формальное описание API снижает риск рассинхронизации frontend/backend. Генерация типов и констант помогает не дублировать вручную имена DTO, коды ошибок и служебные значения.

---

# 18. Визуальные материалы

### 18.1 Основной набор для 2.5.1

```text id="h359jj"
Обязательно:
1. Рисунок 2.x — API-контракт как граница frontend/backend.
2. Рисунок 2.x — API как сценарный контракт, а не CRUD.
3. Таблица 2.x — Основные группы API web-приложения.
4. Таблица 2.x — Разделение read API и command API.
5. Таблица 2.x — Граница решений frontend/backend.
6. Таблица 2.x — Контракт ошибок API.

Желательно:
7. Рисунок 2.x — Frontend intention → API command → Application → Domain → Result.
8. Рисунок 2.x — DTO не равен domain model.
9. Рисунок 2.x — Available actions не заменяют command validation.
10. Рисунок 2.x — Frontend must not send status.
11. Таблица 2.x — Типы DTO в API.
12. Таблица 2.x — API-сценарии загрузки и получения файлов.
13. Таблица 2.x — Нежелательные решения API.
14. Рисунок 2.x — Download файла через metadata и access check.

Опционально:
15. Схема OpenAPI → generated types/constants → frontend.
16. Схема 401 / 403 / 404.
17. Схема validation boundary.
18. Схема stale read DTO.
19. Схема pagination/filter/sort.
```

---

### 18.2 Рисунок 2.x — API-контракт как граница frontend/backend

```text id="a6j9ow"
┌────────────────────────────┐
│ Frontend                    │
│ UI, forms, pages, states    │
└─────────────┬──────────────┘
              │ API contract
              │ request DTO / response DTO
              │ errors / validation
              ▼
┌────────────────────────────┐
│ Backend API                 │
│ routes, auth, validation    │
└─────────────┬──────────────┘
              ▼
┌────────────────────────────┐
│ Application layer           │
│ сценарии и access checks    │
└─────────────┬──────────────┘
              ▼
┌────────────────────────────┐
│ Domain layer                │
│ business rules              │
└─────────────┬──────────────┘
              ▼
┌────────────────────────────┐
│ Storage                     │
│ БД + file storage           │
└────────────────────────────┘
```

Подпись:

```text id="3dgj7i"
Рисунок 2.x — API-контракт как граница frontend и backend
```

---

### 18.3 Рисунок 2.x — API как сценарный контракт

```text id="yq9q6e"
Не основная идея:
CRUD over tables
GET /requests
PUT /requests/{id}

Основная идея:
Scenario commands
POST /requests/{id}/start-review
POST /requests/{id}/approve
POST /requests/{id}/reject
POST /agreement-exchanges/{id}/proposal-versions
```

Подпись:

```text id="lbj3dk"
Рисунок 2.x — API как сценарный контракт, а не CRUD поверх таблиц
```

---

### 18.4 Рисунок 2.x — Read flow и command flow

```text id="it4yfm"
READ FLOW
Frontend
  ↓ запрос данных экрана
Backend API
  ↓ read DTO
Frontend
  ↓ отображает status / available actions

COMMAND FLOW
Frontend
  ↓ команда действия
Backend API
  ↓ request validation
Application layer
  ↓ access / ownership / scenario
Domain layer
  ↓ business rule
Storage
  ↓ save result
Frontend
  ↓ success / structured error
```

Подпись:

```text id="pp94ew"
Рисунок 2.x — Разделение операций чтения и изменения состояния
```

---

### 18.5 Рисунок 2.x — Frontend не отправляет status

```text id="ufkw92"
Bad:
PATCH /requests/{id}
{
  "status": "Approved"
}

Good:
POST /requests/{id}/approve
{}

Backend:
- checks user;
- checks access;
- loads request;
- calls domain rule;
- saves Approved if allowed.
```

Подпись:

```text id="l5hret"
Рисунок 2.x — Command API принимает действие, а не новый статус
```

---

### 18.6 Рисунок 2.x — Available actions are hints, not guarantees

```text id="x9fbux"
Read DTO:
canApprove = true

      ↓ time passes
      ↓ state changed by another user

Command:
POST /requests/{id}/approve

Backend validates again:
- success
or
- ActionNoLongerAvailable
```

Подпись:

```text id="h5m42d"
Рисунок 2.x — Available actions не заменяют проверку команды
```

---

### 18.7 Рисунок 2.x — DTO не равен domain model

```text id="5sbc5h"
Domain model:
- состояния;
- инварианты;
- методы переходов;
- внутренние связи.

        ↓ mapping / projection

API DTO:
- поля для экрана;
- labels;
- available actions;
- file metadata;
- safe error codes.

Frontend видит DTO, но не внутреннюю domain model.
```

Подпись:

```text id="1wecnn"
Рисунок 2.x — Разделение API DTO и domain model
```

---

### 18.8 Рисунок 2.x — Download файла через API

```text id="ivpaz6"
Frontend
  ↓ request document
Backend API
  ↓ auth
Document metadata
  ↓ proposal / exchange / request
Access check
  ↓ allowed?
File storage
  ↓ physical file by storage key
Backend
  ↓ file or safe error
Frontend
```

Подпись:

```text id="3h5zra"
Рисунок 2.x — Получение файла через metadata и проверку доступа
```

---

# 19. Карта будущего текста темы 2.5.1

```text id="pf9ot6"
[Абзац 1]
Связать с 2.3 и 2.4: frontend/backend разделены, теперь нужна граница API.

[Абзац 2]
Объяснить API-контракт как больше, чем endpoints.

[Абзац 3]
Раскрыть API как сценарный контракт, а не CRUD поверх таблиц.

[Абзац 4]
Раскрыть API как границу frontend/backend.

[Рисунок 2.x]
API-контракт как граница frontend/backend.

[Абзац 5]
Раскрыть read API.

[Абзац 6]
Раскрыть screen-specific read DTO.

[Абзац 7]
Раскрыть command API.

[Абзац 8]
Раскрыть command intention вместо прямой передачи status.

[Рисунок 2.x]
Frontend не отправляет status.

[Абзац 9]
Раскрыть route id и body команды.

[Абзац 10]
Раскрыть command result DTO.

[Абзац 11]
Раскрыть DTO и отличие от domain model.

[Таблица 2.x]
Типы DTO в API.

[Абзац 12]
Раскрыть available actions в read DTO.

[Абзац 13]
Раскрыть stale DTO и повторную command validation.

[Рисунок 2.x]
Available actions are hints, not guarantees.

[Абзац 14]
Раскрыть повторные команды и конкурентные конфликты.

[Абзац 15]
Раскрыть validation boundary.

[Абзац 16]
Раскрыть structured error contract.

[Абзац 17]
Раскрыть stable codes и display labels.

[Таблица 2.x]
Контракт ошибок API.

[Абзац 18]
Раскрыть 401/403/404.

[Абзац 19]
Раскрыть сокрытие infrastructure details.

[Абзац 20]
Раскрыть pagination/filter/sort для списков.

[Абзац 21]
Раскрыть API договорного обмена.

[Абзац 22]
Раскрыть file upload.

[Абзац 23]
Раскрыть file download через metadata/access check.

[Таблица 2.x]
API-сценарии файлов.

[Абзац 24]
Раскрыть API contract ownership и controlled changes.

[Таблица 2.x]
Нежелательные решения API.

[Абзац 25]
Упомянуть OpenAPI/generated types/constants как способ снизить рассинхронизацию, если подтверждено.

[Абзац 26]
Показать расширяемость API-контракта.

[Переход]
Следующий драфт подробнее раскрывает read API и command API.
```

---

# 20. Черновые фрагменты будущего текста

### Фрагмент 1 — ввод

После проектирования архитектуры и модели хранения необходимо определить API-контракт между frontend и backend. Он задаёт правила, по которым пользовательский интерфейс получает данные, запускает действия и обрабатывает ошибки.

### Фрагмент 2 — API как граница

API является границей между frontend и серверной частью. Frontend не обращается напрямую к БД, domain layer и файловому хранилищу, а работает через request DTO, response DTO и единый формат ошибок.

### Фрагмент 3 — API не CRUD

API-контракт в проектируемой системе следует рассматривать не как простой набор адресов, а как сценарную границу между frontend и backend. Пользовательский интерфейс не изменяет состояние предметных объектов напрямую, а отправляет backend-у намерение выполнить действие: создать заявку, начать рассмотрение, одобрить заявку, отклонить заявку или отправить версию договорного предложения.

### Фрагмент 4 — read API

Read API используется для получения данных, необходимых экрану: списка заявок, карточки заявки, истории договорного обмена и доступных действий.

### Фрагмент 5 — screen DTO

Read DTO проектируется под потребности конкретного экрана. DTO списка заявок, карточки заявки или истории договорного обмена могут отличаться от внутренней domain model и содержать только те поля, которые нужны конкретному экрану.

### Фрагмент 6 — command API

Command API используется для изменения состояния. Например, одобрение заявки или отправка версии предложения должны проходить через application layer и domain layer, а не выполняться как простая запись статуса в БД.

### Фрагмент 7 — command intention

Command API не должен принимать от frontend готовый новый статус объекта. Статус заявки или договорного обмена является результатом выполнения команды на сервере. Backend загружает необходимые данные, проверяет доступ, вызывает domain layer и только после этого сохраняет новое состояние.

### Фрагмент 8 — command result

Результат команды должен быть достаточным для обновления интерфейса, но не обязан раскрывать внутреннюю domain model. После успешной команды frontend может либо обновить экран, либо повторно запросить read DTO.

### Фрагмент 9 — available actions

Read DTO может содержать доступные действия, чтобы frontend мог показать нужные кнопки. Однако command API обязан повторно проверить действие, поскольку состояние объекта могло измениться после чтения.

### Фрагмент 10 — stale DTO

Read DTO отражает состояние на момент чтения. Если другой пользователь изменил объект раньше, command API должен вернуть ошибку бизнес-состояния, а frontend должен обновить данные экрана.

### Фрагмент 11 — repeated commands

API должен учитывать повторную отправку команды. Frontend может блокировать кнопку во время выполнения запроса, но backend всё равно должен проверять состояние объекта, чтобы повторная команда не нарушила бизнес-инварианты.

### Фрагмент 12 — DTO

DTO ответа не должен быть прямым отражением domain model. Он передаёт frontend только те данные, которые нужны для конкретного сценария, и не раскрывает внутренние связи backend.

### Фрагмент 13 — валидация

Валидация делится на несколько уровней: frontend помогает пользователю заполнить форму, backend проверяет структуру запроса, application layer проверяет доступ, а domain layer — бизнес-инварианты.

### Фрагмент 14 — ошибки

Ошибки API должны возвращаться в структурированном формате. Это позволяет frontend отличать ошибку заполнения формы от ошибки доступа, отсутствия ресурса или недопустимого состояния заявки.

### Фрагмент 15 — codes и labels

Для frontend важно различать стабильные коды и отображаемые подписи. Логика интерфейса должна опираться на code, а пользовательский текст может отображаться отдельно.

### Фрагмент 16 — infrastructure details

API не должен раскрывать frontend внутренние пути файловой системы, storage key, stack trace, ORM errors и другие инфраструктурные детали backend.

### Фрагмент 17 — списки

Read API для списков лучше проектировать с учётом пагинации, сортировки и фильтрации. Это позволяет развивать систему при росте количества заявок и версий договорного предложения.

### Фрагмент 18 — файлы

Файловые операции также проходят через API. При upload backend сохраняет physical file и metadata, а при download сначала находит metadata, проверяет доступ к заявке и договорному обмену и только затем возвращает файл.

### Фрагмент 19 — OpenAPI

Для согласования frontend и backend может использоваться формализованное описание API. Это снижает риск рассинхронизации DTO, кодов ошибок и служебных значений.

### Фрагмент 20 — controlled changes

API-контракт должен изменяться контролируемо. Изменения структуры DTO, error codes и формата validation errors напрямую влияют на frontend, поэтому они должны быть согласованы с клиентской частью.

---

# 21. Матрица вопросов для конкретизации темы

### 21.1 Вопросы по API-контракту

| ID   | Вопрос                                           | Зачем нужен                   | Текущее решение / предположение | Как отвечать    |
| ---- | ------------------------------------------------ | ----------------------------- | ------------------------------- | --------------- |
| API1 | Есть ли OpenAPI?                                 | Для формального API-контракта | проверить                       | repo-check      |
| API2 | Генерируются ли frontend types?                  | Для sync frontend/backend     | проверить                       | repo-check      |
| API3 | Генерируются ли constants/error codes?           | Для semantic contract         | проверить                       | repo-check      |
| API4 | Есть ли единый API client во frontend?           | Для архитектуры               | проверить                       | repo-check      |
| API5 | Как frontend вызывает backend?                   | Для 2.5                       | проверить                       | repo-check      |
| API6 | Есть ли единый источник статусов/actions/errors? | Для contract ownership        | проверить                       | repo-check      |
| API7 | Как меняется API-контракт?                       | Для controlled changes        | не раскрывать без repo-check    | repo-check/text |

### 21.2 Вопросы по scenario API

| ID  | Вопрос                                      | Зачем нужен           | Текущее решение / предположение | Как отвечать |
| --- | ------------------------------------------- | --------------------- | ------------------------------- | ------------ |
| SC1 | API построен по сценариям или CRUD?         | Для ключевой мысли    | сценарный подход                | repo-check   |
| SC2 | Есть ли отдельные approve/reject endpoints? | Для command API       | проверить                       | repo-check   |
| SC3 | Есть ли endpoint start review?              | Для lifecycle         | проверить                       | repo-check   |
| SC4 | Есть ли endpoint start exchange?            | Для договорного этапа | проверить                       | repo-check   |
| SC5 | Есть ли один “update status” endpoint?      | Риск antipattern      | не утверждать                   | repo-check   |

### 21.3 Вопросы по read API

| ID | Вопрос                                         | Зачем нужен             | Текущее решение / предположение | Как отвечать |
| -- | ---------------------------------------------- | ----------------------- | ------------------------------- | ------------ |
| R1 | Какие read endpoints есть?                     | Для групп API           | проверить                       | repo-check   |
| R2 | Есть ли list my requests?                      | Для клиента             | проверить                       | repo-check   |
| R3 | Есть ли employee request list?                 | Для сотрудника          | проверить                       | repo-check   |
| R4 | Есть ли request details DTO?                   | Для карточки            | проверить                       | repo-check   |
| R5 | Есть ли exchange history DTO?                  | Для договорного обмена  | проверить                       | repo-check   |
| R6 | Есть ли availableActions/canApprove/canReject? | Для UI                  | проверить                       | repo-check   |
| R7 | Есть ли разные DTO для list/details?           | Для screen-specific DTO | проверить                       | repo-check   |
| R8 | Есть ли pagination/filter/sort?                | Для read API            | проверить                       | repo-check   |

### 21.4 Вопросы по command API

| ID | Вопрос                                | Зачем нужен           | Текущее решение / предположение | Как отвечать |
| -- | ------------------------------------- | --------------------- | ------------------------------- | ------------ |
| C1 | Какие command endpoints есть?         | Для 2.5               | проверить                       | repo-check   |
| C2 | Есть ли create applicant/request?     | Для клиента           | проверить                       | repo-check   |
| C3 | Есть ли start review?                 | Для lifecycle         | проверить                       | repo-check   |
| C4 | Есть ли approve/reject?               | Для decision          | проверить                       | repo-check   |
| C5 | Есть ли start exchange?               | Для договорного этапа | проверить                       | repo-check   |
| C6 | Есть ли send proposal version?        | Для exchange          | проверить                       | repo-check   |
| C7 | Передаёт ли frontend status напрямую? | Важно                 | не должен                       | repo-check   |
| C8 | Route id отделён от body?             | Для API contract      | проверить                       | repo-check   |
| C9 | Что возвращает command result?        | Для DTO               | проверить                       | repo-check   |

### 21.5 Вопросы по stale DTO / concurrency

| ID  | Вопрос                                   | Зачем нужен              | Текущее решение / предположение | Как отвечать    |
| --- | ---------------------------------------- | ------------------------ | ------------------------------- | --------------- |
| CC1 | Что если availableActions устарели?      | Для command validation   | business-state error            | repo-check/text |
| CC2 | Что если два сотрудника начинают review? | Для concurrency          | structured error                | repo-check/text |
| CC3 | Что если approve/reject повторили?       | Для idempotency/conflict | structured error                | repo-check      |
| CC4 | Есть ли error codes для conflict?        | Для UI                   | проверить                       | repo-check      |
| CC5 | Frontend блокирует кнопку при submit?    | UI, не API               | позже 2.6                       | later           |

### 21.6 Вопросы по DTO

| ID   | Вопрос                               | Зачем нужен      | Текущее решение / предположение | Как отвечать |
| ---- | ------------------------------------ | ---------------- | ------------------------------- | ------------ |
| DTO1 | DTO отделены от domain entities?     | Для architecture | должны быть                     | repo-check   |
| DTO2 | Какие request DTO есть?              | Для API text     | проверить                       | repo-check   |
| DTO3 | Какие response DTO есть?             | Для UI           | проверить                       | repo-check   |
| DTO4 | Есть ли DTO для file metadata?       | Для 2.5/2.4      | проверить                       | repo-check   |
| DTO5 | Есть ли DTO с labels/status display? | Для UI           | проверить                       | repo-check   |
| DTO6 | Есть ли риск domain model leakage?   | Для текста       | не утверждать без проверки      | repo-check   |
| DTO7 | Есть ли command result DTO?          | Для result flow  | проверить                       | repo-check   |

### 21.7 Вопросы по ошибкам

| ID | Вопрос                                 | Зачем нужен           | Текущее решение / предположение | Как отвечать |
| -- | -------------------------------------- | --------------------- | ------------------------------- | ------------ |
| E1 | Используется ли ProblemDetails?        | Для error contract    | проверить                       | repo-check   |
| E2 | Есть ли errorCode?                     | Для frontend behavior | проверить                       | repo-check   |
| E3 | Есть ли validation fieldErrors?        | Для форм              | проверить                       | repo-check   |
| E4 | Как различаются 401/403/404?           | Для access            | проверить                       | repo-check   |
| E5 | Как возвращается business-state error? | Для domain rules      | проверить                       | repo-check   |
| E6 | Раскрываются ли internal details?      | Нельзя                | проверить                       | repo-check   |
| E7 | Есть ли stable code + display message? | Для UI                | проверить                       | repo-check   |

### 21.8 Вопросы по validation

| ID | Вопрос                                      | Зачем нужен        | Текущее решение / предположение | Как отвечать |
| -- | ------------------------------------------- | ------------------ | ------------------------------- | ------------ |
| V1 | Где request validation?                     | Для boundary       | backend API                     | repo-check   |
| V2 | Есть ли FluentValidation?                   | Для реализации     | проверить                       | repo-check   |
| V3 | Как frontend показывает validation errors?  | Для UI             | подробнее 2.6                   | later        |
| V4 | Какие domain validation errors есть?        | Для business-state | проверить                       | repo-check   |
| V5 | Как не смешать validation и business rules? | Для текста         | разные уровни                   | text         |

### 21.9 Вопросы по file API

| ID | Вопрос                              | Зачем нужен     | Текущее решение / предположение | Как отвечать |
| -- | ----------------------------------- | --------------- | ------------------------------- | ------------ |
| F1 | Есть ли upload endpoint?            | Для file API    | проверить                       | repo-check   |
| F2 | Есть ли download endpoint?          | Для file API    | проверить                       | repo-check   |
| F3 | Upload создаёт proposal + metadata? | Для flow        | проверить                       | repo-check   |
| F4 | Download идёт через metadata?       | Для security    | должен                          | repo-check   |
| F5 | Есть ли public static access?       | Риск            | не должен быть основным         | repo-check   |
| F6 | Возвращается ли originalFileName?   | Для UI/download | проверить                       | repo-check   |
| F7 | Раскрывается ли storage path?       | Нельзя          | проверить                       | repo-check   |
| F8 | Раскрывается ли storage key?        | Нельзя          | проверить                       | repo-check   |

### 21.10 Вопросы по договорному обмену

| ID  | Вопрос                        | Зачем нужен   | Текущее решение / предположение | Как отвечать |
| --- | ----------------------------- | ------------- | ------------------------------- | ------------ |
| EX1 | Есть ли API start exchange?   | Для lifecycle | проверить                       | repo-check   |
| EX2 | Есть ли API proposal history? | Для UI        | проверить                       | repo-check   |
| EX3 | Есть ли API send proposal?    | Для command   | проверить                       | repo-check   |
| EX4 | Есть ли API accept proposal?  | Не утверждать | проверить                       | repo-check   |
| EX5 | Есть ли API counter proposal? | Не утверждать | проверить                       | repo-check   |
| EX6 | Есть ли comments?             | Не утверждать | проверить                       | repo-check   |

### 21.11 Вопросы для генерации текста

| ID | Вопрос                              | Зачем нужен          | Текущее решение / предположение      | Как отвечать    |
| -- | ----------------------------------- | -------------------- | ------------------------------------ | --------------- |
| T1 | Как не уйти в реализацию endpoints? | Глава 2 проектная    | писать группы API и контракт         | text            |
| T2 | Как связать API с 2.3?              | Architecture         | API как frontend/backend boundary    | text            |
| T3 | Как связать API с 2.4?              | Files/storage        | upload/download через metadata       | text            |
| T4 | Как связать API с 2.6?              | UI                   | DTO/availableActions/errors          | text            |
| T5 | Как связать API с главой 3?         | Implementation       | concrete endpoints/tests там         | text            |
| T6 | Как не overclaim OpenAPI/generated? | Проверить repo       | писать осторожно                     | repo-check/text |
| T7 | Как объяснить intention command?    | Центрально           | command = намерение, status = result | text            |
| T8 | Как объяснить stale DTO?            | Для availableActions | read не guarantee                    | text            |

---

# 22. Что проверить по repo

```text id="yxheob"
- есть ли OpenAPI;
- где находится OpenAPI config;
- генерируются ли frontend types;
- генерируются ли constants;
- есть ли общий API client;
- структура backend controllers/endpoints;
- API больше scenario-based или CRUD-based;
- есть ли endpoints approve/reject/start-review;
- есть ли endpoint update status напрямую;
- структура request DTO;
- структура response DTO;
- структура command result DTO;
- отделены ли DTO от domain entities;
- есть ли read DTO для списка заявок;
- есть ли read DTO для карточки заявки;
- есть ли screen-specific DTO;
- есть ли availableActions/canApprove/canReject;
- есть ли pagination/filter/sort для списков;
- есть ли command endpoints create/start/approve/reject;
- передаёт ли frontend status напрямую;
- route id и body разделены или id дублируется;
- есть ли endpoints договорного обмена;
- есть ли upload endpoint;
- есть ли download endpoint;
- идёт ли download через metadata;
- раскрывается ли storage path frontend;
- раскрывается ли storage key frontend;
- есть ли ProblemDetails;
- есть ли errorCode;
- есть ли validation errors по полям;
- есть ли stable code + display label/message;
- как различаются 401/403/404;
- как возвращаются business-state errors;
- как возвращаются concurrency/conflict errors;
- как API обрабатывает forbidden file access;
- раскрываются ли stack trace / ORM errors;
- какие tests покрывают API;
- какие tests покрывают validation/errors;
- какие tests покрывают upload/download.
```

---

# 23. Что отправить визуальному чату

```text id="9go8it"
Сделай визуальный разбор темы 2.5.1 “API-контракт и взаимодействие frontend/backend”.

Нужны 3 версии для каждого визуала:

1. Рисунок “API-контракт как граница frontend/backend”.
2. Рисунок “API как сценарный контракт, а не CRUD”.
3. Таблица “Основные группы API web-приложения”.
4. Рисунок “Read flow и command flow”.
5. Таблица “Разделение read API и command API”.
6. Таблица “Граница решений frontend/backend”.
7. Рисунок “Frontend intention → API command → Application → Domain → Result”.
8. Рисунок “Frontend must not send status”.
9. Рисунок “Available actions не заменяют command validation”.
10. Рисунок “DTO не равен domain model”.
11. Таблица “Типы DTO в API”.
12. Таблица “Контракт ошибок API”.
13. Таблица “Нежелательные решения API”.
14. Рисунок “Download файла через metadata и access check”.
15. Опционально — схема “OpenAPI → generated types/constants → frontend”.
16. Опционально — схема “401 / 403 / 404”.
17. Опционально — схема “validation boundary”.
18. Опционально — схема “stale read DTO”.
19. Опционально — схема “pagination/filter/sort”.

Важно:
- показать frontend и backend как разные стороны API boundary;
- показать API не как список endpoints, а как сценарный контракт;
- показать request DTO / response DTO / command result DTO;
- показать, что frontend не знает domain model напрямую;
- показать read API отдельно от command API;
- показать, что command API принимает intention/action;
- показать, что frontend не отправляет status напрямую;
- показать bad/good пример:
  Bad: PATCH request { status: Approved }
  Good: POST /requests/{id}/approve;
- показать, что read DTO может содержать availableActions;
- показать, что command API всё равно идёт через application/domain;
- показать stale DTO:
  canApprove=true → время прошло → backend validates again;
- показать errors как structured contract;
- показать stable code отдельно от display label/message;
- показать validation errors отдельно от business-state errors;
- показать 401/403/404 как разные варианты access response;
- показать file upload/download через backend;
- download файла показывать через metadata → proposal/exchange/request → access check;
- не показывать frontend как прямой доступ к БД;
- не показывать frontend как прямой доступ к file storage;
- не показывать public static file URL для договорных документов;
- не показывать storage path или storage key как frontend contract;
- не перегружать конкретными endpoint names.
```

---

# 24. Итоговая формула темы

```text id="ko3z6r"
API-контракт задаёт границу frontend/backend;

API выражает не таблицы,
а пользовательские сценарии;

frontend не обращается напрямую к БД,
domain layer
или файловому хранилищу;

frontend работает через request DTO,
response DTO,
command result DTO,
file API
и structured errors;

read API готовит данные для экранов:
списки,
карточки,
статусы,
историю договорного обмена
и available actions;

read DTO может быть screen-specific;

command API запускает изменение состояния:
создание заявки,
начало рассмотрения,
одобрение,
отклонение,
начало договорного обмена,
отправку версии предложения;

frontend отправляет intention/command,
а не новый статус;

статус является результатом выполнения команды на backend;

command API должен проходить через application layer
и domain layer;

route может задавать объект действия,
а body — параметры действия;

command result DTO должен быть достаточным для UI,
но не обязан раскрывать domain model;

DTO не равен domain model:
frontend получает данные для сценария,
а не внутреннюю структуру backend;

available actions помогают frontend показать кнопки,
но не заменяют backend/domain validation;

read DTO может устареть,
поэтому command API всегда проверяет действие заново;

повторные команды,
двойные клики
и конкурентные действия
должны обрабатываться через проверки состояния
и structured business-state errors;

ошибки должны быть структурированы:
validation errors,
access errors,
not found,
business-state errors,
file errors;

stable code и status code важнее для frontend-логики,
чем текст сообщения;

display label/message нужен для пользователя,
но не должен быть единственной основой UI-логики;

401,
403
и 404
нужно различать по смыслу,
особенно для чужих заявок и файлов;

upload файла создаёт physical file и metadata;

download файла начинается с metadata,
проходит через access check
и только потом возвращает physical file;

API не должен раскрывать frontend internal storage path,
storage key,
stack trace,
ORM errors
и внутренние детали backend;

read API для списков следует проектировать с учётом
pagination,
filter
и sort;

API-контракт должен быть единым источником согласования
статусов,
ошибок,
available actions
и file metadata
между frontend и backend;

OpenAPI,
generated types
и generated constants
можно использовать как средство синхронизации frontend/backend,
если это подтверждено repo;

хороший API-контракт снижает связанность frontend/backend
и позволяет менять domain/storage implementation
без переписывания пользовательского интерфейса во множестве мест.
```

Следующий драфт после этого:

```text id="dpuxuh"
planning/thesis/vkr-topic-workbench/03-chapter-2-design/05-api-frontend-backend/02-read-and-command-api.topic.md
```
