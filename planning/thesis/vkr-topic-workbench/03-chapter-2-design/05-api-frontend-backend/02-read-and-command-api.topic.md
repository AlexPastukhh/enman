# Тема: Read API и command API

**Статус:** полный topic-драфт v1 / второй драфт блока 2.5 / продолжение `01-api-contract-overview.topic.md` / нужен repo-check по read endpoints, command endpoints, DTO, availableActions, approve/reject/start-review/start-exchange/send-proposal, pagination/filter/sort, command result, stale DTO и business-state errors / готов к text generation и visual bridge

---

## 0. Служебное назначение драфта

Этот файл является **рабочей карточкой темы**, а не финальным текстом ВКР.

В финальный текст ВКР можно переносить очищенный материал из разделов:

```text id="encspt"
5. Сценарная основа read/command API
6. Содержание темы
7. Read API
8. Command API
9. Сравнение read и command операций
10. Available actions и устаревание read DTO
11. Command result и обновление UI
12. Конкурентные действия и повторные команды
13. Что требует обоснования
14. Визуальные материалы
15. Черновые фрагменты будущего текста
```

Служебные разделы не переносить напрямую:

```text id="sxou9q"
repo-check;
матрица вопросов;
visual bridge;
что проверить по repo;
что отправить визуальному чату;
точные endpoint names без проверки;
точные DTO fields без проверки;
старые use-case labels L1/L2/L3;
устаревшие ContractDraft / ContractDraftSent без нормализации.
```

---

## 1. Карта размещения

```text id="ut94ev"
ВКР
├─ Глава 2. Проектирование web-приложения
│  ├─ 2.1 Требования, роли и основные сценарии системы
│  ├─ 2.2 Проектирование предметной модели и жизненных циклов
│  ├─ 2.3 Проектирование архитектуры web-приложения
│  ├─ 2.4 Проектирование хранения данных и файлов
│  ├─ 2.5 Проектирование API и взаимодействия frontend/backend
│  │  ├─ 2.5.1 API-контракт и взаимодействие frontend/backend
│  │  ├─ 2.5.2 Текущая тема: Read API и command API
│  │  ├─ 2.5.3 Контракт валидации и ошибок
│  │  └─ 2.5.4 API загрузки и получения файлов
│  └─ 2.6 Проектирование пользовательского интерфейса
└─ Глава 3. Реализация и тестирование web-приложения
```

Рабочая папка:

```text id="pj4ds6"
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

Эта тема уточняет разделение API на операции чтения и операции изменения состояния.

Предыдущая тема 2.5.1 отвечала на вопрос:

```text id="edp30i"
что такое API-контракт;
почему API — это граница frontend/backend;
почему API выражает сценарии, а не CRUD поверх таблиц;
почему DTO не равен domain model;
почему frontend не должен передавать новый status напрямую.
```

Текущая тема отвечает на вопрос:

```text id="odjabt"
какие API-операции нужны только для отображения данных;
какие API-операции запускают изменение состояния;
почему read DTO может быть screen-specific;
почему command API должен проходить через application/domain layer;
как availableActions связаны с командами;
что делать, если read DTO устарел;
как command result помогает frontend обновить UI.
```

Главная мысль:

> Read API и command API решают разные задачи. Read API подготавливает данные для экранов: списки, карточки, статусы, историю договорного обмена и доступные действия. Command API запускает сценарии изменения состояния: создание заявки, начало рассмотрения, одобрение, отклонение, начало договорного обмена и отправку версии предложения. Read DTO помогает frontend показать интерфейс, но не гарантирует успешность команды: command API всегда заново проверяет доступ, текущее состояние и бизнес-правила.

---

## 3. Важное уточнение: read ≠ command

Read API не должен изменять состояние процесса.

Command API не должен быть просто “update записи”.

### Read API

```text id="d1fehp"
получить данные;
подготовить экран;
показать статус;
показать available actions;
показать историю;
показать file metadata;
не менять состояние.
```

### Command API

```text id="eie8m9"
получить intention от frontend;
проверить request DTO;
проверить доступ;
загрузить предметный объект;
проверить domain rules;
изменить состояние;
сохранить результат;
вернуть success или structured error.
```

Безопасная формулировка:

> Операции чтения и изменения состояния имеют разную архитектурную роль. Read API формирует представление для frontend, а command API выполняет сценарий и проходит через application layer и domain layer.

---

## 4. Принятые уточнения

1. Read API не должен менять состояние.
2. Command API меняет состояние только после серверной проверки.
3. Read API может возвращать screen-specific DTO.
4. DTO списка, карточки заявки и договорного обмена могут отличаться.
5. Read DTO может содержать `availableActions`.
6. `availableActions` помогают frontend показать кнопки.
7. `availableActions` не заменяют command validation.
8. Read DTO может устареть.
9. Command API всегда заново проверяет состояние.
10. Command API принимает намерение пользователя, а не новый статус.
11. Frontend не должен отправлять произвольный `status`.
12. Статус — результат выполнения команды.
13. Route id и body команды лучше разделять.
14. Command result DTO не обязан возвращать полную domain model.
15. После command frontend может обновить часть экрана или повторно запросить read DTO.
16. Повторные команды и двойные клики нужно учитывать.
17. Конкурентные действия должны возвращать business-state error, а не generic server error.
18. Read API для списков желательно проектировать с pagination/filter/sort.
19. Списки заявок клиента и сотрудника могут иметь разные read DTO.
20. Договорный обмен может иметь отдельный read DTO для истории версий.
21. File metadata может быть частью read DTO договорного обмена.
22. File download не является обычным read DTO, потому что возвращает physical file и требует access check.
23. File upload является command-сценарием.
24. Mock-проверка данных является command/query-like сценарием: запускается действием, но не принимает решение автоматически.
25. Точные endpoint names нужно проверить по repo.
26. Точные поля DTO нужно проверить по repo.
27. Не переносить в текст старые `ContractDraft`, `ContractDraftSent`, email-flow и PDF generation без проверки.

---

## 5. Сценарная основа read/command API

### 5.1 Клиентские read-сценарии

```text id="o6clwz"
получить текущего пользователя;
получить список заявителей;
получить список своих заявок;
открыть карточку своей заявки;
увидеть статус заявки;
увидеть результат рассмотрения;
увидеть причину отказа, если она есть;
увидеть договорный обмен после одобрения;
увидеть версии договорного предложения;
увидеть metadata файла версии.
```

### 5.2 Клиентские command-сценарии

```text id="o74zo2"
создать заявителя;
создать заявку;
отправить встречную версию предложения, если подтверждено моделью;
загрузить файл версии, если это действие доступно;
принять предложение, если подтверждено моделью.
```

### 5.3 Сотруднические read-сценарии

```text id="ejvisd"
получить список заявок;
открыть карточку заявки;
увидеть данные заявителя;
увидеть состояние рассмотрения;
увидеть результат проверки данных;
увидеть доступные действия;
увидеть договорный обмен;
увидеть историю версий;
увидеть metadata файлов.
```

### 5.4 Сотруднические command-сценарии

```text id="yghcfr"
начать рассмотрение;
запустить проверку данных;
одобрить заявку;
отклонить заявку;
начать договорный обмен;
отправить версию договорного предложения;
загрузить файл версии.
```

Рабочая формулировка:

> Разделение read и command API следует выводить из сценариев. Всё, что нужно для отображения состояния, относится к read API. Всё, что изменяет состояние заявки, рассмотрения, договорного обмена или версии предложения, относится к command API.

---

## 6. Зачем эта тема нужна

### 6.1 На уровне темы

Тема нужна, чтобы не смешать отображение данных и изменение состояния.

Без разделения read/command могут появиться проблемы:

```text id="f8cctu"
read endpoint неожиданно меняет состояние;
frontend передаёт новый status напрямую;
списки заявок возвращают слишком тяжёлую domain model;
карточка заявки и dashboard используют один неудобный DTO;
availableActions воспринимаются как гарантия;
устаревший UI пытается выполнить недоступную команду;
двойной клик создаёт повторное действие;
конкурентные действия превращаются в generic 500 error.
```

Разделение помогает:

```text id="m3krb2"
делать DTO удобными для экранов;
держать business rules на backend;
явно проектировать команды;
обрабатывать stale data;
возвращать понятные business errors;
связывать UI с API без протекания domain model.
```

### 6.2 На уровне пункта 2.5

Пункт 2.5 должен раскрыть:

```text id="kfrm0n"
API-контракт;
read API и command API;
DTO;
валидацию;
ошибки;
file API;
frontend/backend interaction flow.
```

Текущий драфт закрывает вторую часть:

```text id="oj1uws"
read API и command API
```

### 6.3 На уровне всей главы 2

Тема связывает:

```text id="rv17vs"
2.3 разделение frontend/backend/application/domain
→ 2.5 API-contract
→ 2.6 UI states/buttons/forms
→ глава 3 controllers/handlers/tests
```

---

# 7. Содержание темы

## 7.1 Read API как модель отображения

Read API нужен для получения данных, которые frontend должен показать пользователю.

Read API отвечает за:

```text id="jr5xzz"
получение списков;
получение карточек;
получение истории;
получение статусов;
получение labels;
получение availableActions;
получение file metadata;
получение данных для конкретного экрана.
```

Read API не должен:

```text id="s7162b"
менять статус заявки;
начинать рассмотрение;
фиксировать решение;
создавать договорный обмен;
создавать версию предложения;
сохранять файл;
запускать domain transition.
```

Рабочая формулировка:

> Read API формирует данные для отображения, но не выполняет изменение состояния. Благодаря этому frontend может безопасно запрашивать списки, карточки и историю договорного обмена без риска случайно изменить процесс.

---

## 7.2 Screen-specific read DTO

Read DTO не обязан повторять domain entity.

Примеры read DTO:

```text id="nyxwjc"
ClientRequestListItemDto;
EmployeeRequestListItemDto;
RequestDetailsDto;
RequestReviewDetailsDto;
AgreementExchangeDetailsDto;
AgreementProposalVersionDto;
FileMetadataDto.
```

Dashboard сотрудника может требовать:

```text id="j87a9g"
requestId;
applicantName;
createdAt;
status;
statusLabel;
reviewState;
availableActions.
```

Карточка заявки может требовать:

```text id="oh9tth"
requestId;
applicant data;
request data;
review data;
decision;
rejectionReason;
availableActions.
```

История договорного обмена может требовать:

```text id="wyw3e5"
exchangeId;
proposalVersions;
senderSide;
sentAt;
originalFileName;
documentId;
availableActions.
```

Рабочая формулировка:

> Read DTO проектируется под потребности конкретного экрана. Поэтому DTO списка заявок может быть компактным, а DTO карточки заявки — более подробным. Это не нарушение архитектуры, а способ не раскрывать frontend лишнюю внутреннюю модель.

---

## 7.3 Read API для списков

Списочные read API нужны для:

```text id="digtad"
списка заявителей клиента;
списка заявок клиента;
списка заявок сотрудника;
истории версий договорного предложения.
```

Даже если данных немного, проектно лучше предусмотреть:

```text id="r8o8dc"
pagination;
filter by status;
filter by review state;
sort by created date;
sort by updated date;
search, если подтверждено моделью.
```

Осторожность:

```text id="om4rc0"
не утверждать реальные query parameters без repo-check;
не писать сложный search, если его нет;
не писать server-side pagination как реализованную, если repo не подтверждает.
```

Рабочая формулировка:

> Списочные read API следует проектировать как управляемую выдачу данных. Даже в учебном проекте полезно отделить получение списка от получения полной карточки объекта и предусмотреть возможность сортировки, фильтрации или пагинации.

---

## 7.4 Read API для карточки заявки

Карточка заявки должна собрать данные из нескольких частей процесса:

```text id="r2fvyr"
данные заявки;
данные заявителя;
текущий статус;
состояние рассмотрения;
результат проверки, если сохраняется;
решение;
причина отказа;
договорный обмен, если он начат;
availableActions.
```

Она не обязана раскрывать:

```text id="wpofst"
все поля domain entity;
внутренние enum details;
структуру БД;
служебные поля ORM;
storage path файлов.
```

Рабочая формулировка:

> Read API карточки заявки должен возвращать frontend целостное представление текущего состояния процесса, но не обязан повторять внутреннюю domain model. Его задача — подготовить данные для экрана.

---

## 7.5 Read API договорного обмена

Договорный обмен требует отдельного read DTO.

Он может включать:

```text id="qj6ky9"
exchangeId;
requestId;
exchangeStatus;
proposalVersions;
senderSide;
sentAt;
comment, если есть;
document metadata;
originalFileName;
documentId;
availableActions.
```

Важно:

```text id="fw4qsm"
история версий — read data;
отправка новой версии — command;
скачивание файла — отдельный file download API;
storage key не возвращается frontend.
```

Рабочая формулировка:

> Read API договорного обмена должен возвращать историю версий предложения и metadata документов. При этом physical file не должен отдаваться как часть read DTO: получение файла выполняется отдельным download-сценарием через backend.

---

## 7.6 Available actions в read DTO

Read DTO может содержать доступные действия.

Например:

```json id="wlmtsf"
{
  "requestId": 15,
  "status": "InReview",
  "availableActions": ["Approve", "Reject"],
  "canApprove": true,
  "canReject": true
}
```

Frontend использует это для:

```text id="e9nnpv"
показа кнопок;
скрытия недоступных действий;
подсказок пользователю;
уменьшения лишних запросов.
```

Но:

```text id="ogb06e"
availableActions не гарантируют успех команды;
между read и command состояние может измениться;
backend проверяет команду повторно.
```

Рабочая формулировка:

> Available actions являются частью модели чтения и помогают frontend отобразить интерфейс. Однако они не являются заменой проверки на backend, потому что read DTO может устареть.

---

## 7.7 Устаревание read DTO

Read DTO показывает состояние на момент получения.

Пример:

```text id="hl16gv"
сотрудник открыл карточку заявки;
frontend получил canApprove = true;
другой сотрудник уже отклонил заявку;
первый сотрудник нажал “Одобрить”;
backend должен вернуть business-state error.
```

Возможные коды:

```text id="vfuz0i"
ActionNoLongerAvailable;
RequestAlreadyDecided;
ReviewAlreadyStarted;
ExchangeAlreadyStarted;
ProposalVersionIsNoLongerActive.
```

Рабочая формулировка:

> Read DTO может устареть, поэтому command API обязан повторно проверять актуальное состояние объекта. Если действие уже недоступно, backend должен вернуть структурированную ошибку бизнес-состояния.

---

## 7.8 Command API как запуск сценария

Command API принимает намерение пользователя выполнить действие.

Примеры command API:

```text id="e89nef"
create applicant;
create request;
start review;
run data check;
approve request;
reject request;
start agreement exchange;
send proposal version;
upload proposal file.
```

Command API должен:

```text id="v0ixkf"
проверить пользователя;
проверить request DTO;
проверить доступ;
загрузить нужный объект;
вызвать application/domain logic;
сохранить изменения;
вернуть результат или ошибку.
```

Рабочая формулировка:

> Command API запускает сценарий изменения состояния. Он не должен сводиться к записи переданных frontend полей в БД, потому что изменение состояния требует проверки доступа и бизнес-правил.

---

## 7.9 Command API не принимает status напрямую

Frontend не должен отправлять:

```json id="hmw02a"
{
  "status": "Approved"
}
```

Вместо этого frontend вызывает команду:

```text id="thqro3"
ApproveRequest
```

или endpoint по смыслу действия:

```http id="du6e4r"
POST /requests/{requestId}/approve
```

Смысл:

```text id="jg3lpm"
frontend сообщает намерение;
backend определяет текущего пользователя;
backend проверяет роль и доступ;
domain проверяет переход;
status сохраняется как результат.
```

Рабочая формулировка:

> Статус заявки или договорного обмена должен быть результатом выполнения команды, а не свободным входным значением от frontend. Это защищает lifecycle от некорректных переходов.

---

## 7.10 Route id и body команды

Для command API полезно отделять объект действия от параметров действия.

Пример:

```http id="hxrid7"
POST /requests/{requestId}/reject
```

Body:

```json id="kjd5la"
{
  "reason": "..."
}
```

Что в route:

```text id="e6g2z6"
какая заявка;
какой договорный обмен;
какая версия;
какой документ.
```

Что в body:

```text id="v6swl3"
причина отказа;
комментарий;
параметры действия;
файл, если upload;
дополнительные данные формы.
```

Рабочая формулировка:

> Идентификатор объекта удобно передавать в route, а параметры действия — в body. Это делает команду понятнее и снижает риск противоречия между идентификатором в URL и идентификатором в теле запроса.

---

## 7.11 Command result DTO

После команды frontend должен понять результат.

Command result может содержать:

```text id="swkjgz"
id созданного объекта;
новый status;
documentId;
proposalVersionId;
нужный redirect target;
минимальный summary;
message или code;
next available action, если подтверждено моделью.
```

Но command result не обязан возвращать:

```text id="f399t3"
полную domain entity;
все связи;
полную карточку заявки;
storage path;
внутренние поля.
```

Возможные стратегии frontend после команды:

```text id="t2j7la"
обновить локальное состояние по command result;
повторно запросить карточку заявки;
повторно запросить список;
перейти на новый экран;
показать structured error.
```

Рабочая формулировка:

> Command result DTO должен быть достаточным для реакции интерфейса, но не обязан возвращать полную модель объекта. После команды frontend может повторно запросить read DTO, если экрану нужно актуальное состояние.

---

## 7.12 Команды создания объектов

Команды создания объектов относятся к command API.

Примеры:

```text id="i7mt5g"
создать заявителя;
создать заявку;
создать версию договорного предложения;
создать metadata документа через upload-сценарий.
```

Создание заявки должно проходить через backend, потому что backend проверяет:

```text id="t8mysj"
текущего клиента;
доступ к заявителю;
валидность request DTO;
business constraints;
связь с applicant party.
```

Рабочая формулировка:

> Создание объектов также относится к command API. Даже если объект новый, backend должен проверить пользователя, входные данные и связь создаваемого объекта с текущим процессом.

---

## 7.13 Команды изменения lifecycle

Lifecycle-команды особенно важны.

Примеры:

```text id="cgef21"
StartReview;
ApproveRequest;
RejectRequest;
StartAgreementExchange;
SendAgreementProposalVersion.
```

Они должны проходить через domain/application layer, потому что проверяют:

```text id="tqprua"
текущее состояние;
роль пользователя;
допустимый переход;
наличие review;
наличие decision;
связь exchange с approved request;
правила отправителя версии.
```

Рабочая формулировка:

> Команды жизненного цикла заявки и договорного обмена должны быть отдельными сценарными операциями. Они не должны реализовываться как универсальное изменение статуса, потому что каждый переход имеет собственные правила.

---

## 7.14 Mock-проверка данных как особый command/query scenario

Проверка данных заявки может выглядеть как команда, потому что пользователь нажимает кнопку и backend выполняет действие.

Но она не должна автоматически менять решение по заявке.

Проверка данных:

```text id="c1zouw"
запускается сотрудником;
проверяет доступ;
выполняет mock/service operation;
возвращает результат;
может сохранять результат, если это есть в модели;
не одобряет и не отклоняет заявку автоматически.
```

Рабочая формулировка:

> Проверка данных является вспомогательным серверным сценарием. Она может запускаться через command-like API, но её результат не заменяет отдельное решение сотрудника об одобрении или отклонении заявки.

---

## 7.15 Повторные команды и двойной клик

Frontend может отправить команду повторно.

Причины:

```text id="pk4srm"
двойной клик;
повторная отправка формы;
сетевой retry;
обновление страницы;
медленный ответ backend.
```

Frontend может:

```text id="dd03ow"
блокировать кнопку;
показывать loading state;
не отправлять форму повторно;
показывать результат.
```

Backend должен:

```text id="p2v291"
проверять текущее состояние;
не позволять повторный недопустимый transition;
возвращать понятную business-state error;
не полагаться только на UI blocking.
```

Рабочая формулировка:

> Защита от повторных команд не должна находиться только во frontend. Интерфейс может блокировать кнопку, но backend должен повторно проверять состояние объекта и не допускать некорректных повторных переходов.

---

## 7.16 Конкурентные действия пользователей

Несколько пользователей могут действовать с одним объектом.

Примеры:

```text id="nhjyff"
два сотрудника пытаются начать рассмотрение одной заявки;
один сотрудник одобряет заявку, другой отклоняет;
клиент отправляет версию, пока сотрудник отправил новую;
пользователь скачивает файл, когда metadata стала недоступна.
```

Это не всегда техническая ошибка. Часто это:

```text id="qmujtr"
business-state conflict;
stale read DTO;
action no longer available.
```

Рабочая формулировка:

> Конкурентные действия должны обрабатываться как ошибки бизнес-состояния, если конфликт связан с изменением lifecycle. Это позволяет frontend показать понятное сообщение и обновить данные.

---

## 7.17 Read после command

После успешной команды frontend должен получить актуальное состояние.

Варианты:

```text id="h0lhtv"
command result содержит достаточные данные;
frontend повторно вызывает details read API;
frontend обновляет список;
frontend invalidates cache, если используется;
frontend переходит на другой экран.
```

Пример:

```text id="hu7e9i"
ApproveRequest success
→ frontend повторно запрашивает RequestDetails
→ видит Approved и action StartAgreementExchange.
```

Рабочая формулировка:

> После выполнения command API frontend должен синхронизировать отображение с серверным состоянием. Это можно сделать через command result DTO или через повторный read-запрос.

---

## 7.18 Read API и file metadata

Read API может возвращать metadata файла, но не сам physical file.

Read DTO может содержать:

```text id="sv31jg"
documentId;
originalFileName;
uploadedAt;
senderSide;
fileSize, если используется;
contentType, если используется;
downloadAvailable.
```

Read DTO не должен содержать:

```text id="y2a9zc"
storage path;
internal storage key;
physical directory;
public static file path;
file bytes.
```

Рабочая формулировка:

> В read DTO договорного обмена можно вернуть metadata документа, необходимую для отображения файла. Сам physical file должен получаться отдельным download-сценарием через backend.

---

## 7.19 File upload как command API

Upload файла меняет состояние системы:

```text id="nxux9t"
создаёт physical file;
создаёт metadata;
создаёт или связывает proposal version;
изменяет историю договорного обмена.
```

Поэтому upload — это command, а не обычный read.

Он должен проверять:

```text id="lssids"
пользователя;
доступ к exchange;
допустимость новой версии;
наличие файла;
ограничения файла, если есть;
согласованность metadata/file.
```

Рабочая формулировка:

> Загрузка файла договорного предложения относится к command API, потому что она создаёт новую информацию в системе и связывает physical file с версией предложения.

---

## 7.20 File download как read-like operation с access check

Download похож на read, но отличается от обычного read DTO.

Он:

```text id="rjodjv"
не изменяет бизнес-состояние;
возвращает physical file;
требует metadata lookup;
требует access check;
не должен раскрывать storage path.
```

Рабочая формулировка:

> Получение файла можно рассматривать как read-like операцию, но она требует отдельного сценария, потому что возвращает physical file и должна проверять доступ через metadata, договорный обмен и заявку.

---

# 8. Read API

| Read-сценарий              | Что возвращает                       | Для какого UI           |
| -------------------------- | ------------------------------------ | ----------------------- |
| Current user               | роль, базовый контекст               | навигация, layout       |
| List applicants            | список заявителей                    | личный кабинет клиента  |
| List client requests       | компактные заявки клиента            | список моих заявок      |
| List employee requests     | компактные заявки для сотрудника     | рабочая зона сотрудника |
| Request details            | полная карточка заявки               | просмотр заявки         |
| Review details             | состояние рассмотрения               | сотрудническая карточка |
| Agreement exchange details | exchange + версии                    | договорный этап         |
| Proposal history           | версии предложения                   | история обмена          |
| File metadata              | originalFileName, uploadedAt, sender | отображение файла       |

Подпись:

```text id="cy2leb"
Таблица 2.x — Read API web-приложения
```

---

# 9. Command API

| Command-сценарий      | Что делает                | Какие проверки нужны                      |
| --------------------- | ------------------------- | ----------------------------------------- |
| Create applicant      | создаёт заявителя         | user, request DTO                         |
| Create request        | создаёт заявку            | client, applicant ownership, validation   |
| Start review          | начинает рассмотрение     | employee role, state                      |
| Run data check        | запускает проверку        | employee role, request state              |
| Approve request       | одобряет заявку           | employee role, review state, domain rules |
| Reject request        | отклоняет заявку          | employee role, reason, domain rules       |
| Start exchange        | начинает договорный обмен | approved request, access                  |
| Send proposal version | отправляет версию         | exchange state, sender                    |
| Upload proposal file  | сохраняет файл и metadata | access, file, consistency                 |
| Download document     | отдаёт файл               | metadata, access, file exists             |

Подпись:

```text id="c6xvmf"
Таблица 2.x — Command API и проверяемые условия
```

---

# 10. Сравнение read и command операций

| Критерий           | Read API                 | Command API                      |
| ------------------ | ------------------------ | -------------------------------- |
| Основная цель      | показать данные          | изменить состояние               |
| Изменяет состояние | нет                      | да                               |
| DTO                | screen-specific read DTO | command request/result DTO       |
| Валидация          | query/filter/access      | request/access/domain            |
| Domain rules       | не применяет переход     | применяет переход                |
| Ошибки             | not found/access         | validation/access/business-state |
| Available actions  | может возвращать         | проверяет заново                 |
| Stale data         | возможно                 | должен обработать                |
| Файлы              | metadata                 | upload/download scenario         |
| Пример             | request details          | approve request                  |

Подпись:

```text id="i13r42"
Таблица 2.x — Различие read API и command API
```

---

# 11. Available actions и устаревание read DTO

| Ситуация                        | Что видит frontend   | Что должен сделать backend              |
| ------------------------------- | -------------------- | --------------------------------------- |
| DTO актуален                    | canApprove = true    | выполнить command после проверки        |
| DTO устарел                     | кнопка ещё видна     | вернуть business-state error            |
| Действие скрыто                 | canApprove = false   | всё равно отклонить прямой API-запрос   |
| Другой сотрудник изменил заявку | UI не знает          | вернуть ActionNoLongerAvailable         |
| Заявка уже решена               | old DTO              | вернуть RequestAlreadyDecided           |
| Exchange закрыт/изменён         | old proposal actions | вернуть ProposalVersionIsNoLongerActive |

Подпись:

```text id="h23gb9"
Таблица 2.x — Available actions и повторная проверка команды
```

---

# 12. Command result и обновление UI

| Command               | Возможный result              | Что делает frontend                      |
| --------------------- | ----------------------------- | ---------------------------------------- |
| Create request        | requestId, status             | перейти к карточке или обновить список   |
| Start review          | requestId, reviewState        | обновить карточку                        |
| Approve request       | requestId, status             | запросить details / показать next action |
| Reject request        | requestId, status, reason     | обновить карточку                        |
| Start exchange        | exchangeId                    | перейти к договорному обмену             |
| Send proposal version | proposalVersionId             | обновить history                         |
| Upload file           | documentId, proposalVersionId | показать файл в версии                   |
| Download file         | physical file                 | скачать/открыть файл                     |

Подпись:

```text id="a02np6"
Таблица 2.x — Результаты command API и обновление интерфейса
```

---

# 13. Конкурентные действия и повторные команды

| Риск                  | Пример                                | Проектный ответ                           |
| --------------------- | ------------------------------------- | ----------------------------------------- |
| Двойной клик          | approve отправлен дважды              | backend повторно проверяет state          |
| Stale DTO             | canApprove устарел                    | business-state error                      |
| Два сотрудника        | оба начинают review                   | один success, второй structured error     |
| Повторный upload      | один файл отправлен дважды            | проверить сценарий и UI blocking          |
| Повторное решение     | approved → reject                     | domain запрещает                          |
| Старый exchange state | отправка версии в изменённый exchange | ActionNoLongerAvailable                   |
| Сетевой retry         | команда повторилась                   | state check / possible idempotency future |

Подпись:

```text id="hjeww2"
Таблица 2.x — Риски повторных и конкурентных команд
```

---

# 14. Что требует обоснования

### 14.1 Почему read API не должен менять состояние

Будущая формулировка:

> Read API предназначен для получения данных и не должен изменять состояние процесса. Это делает чтение безопасным для повторного вызова и упрощает работу frontend с обновлением экранов.

---

### 14.2 Почему command API должен быть сценарным

Будущая формулировка:

> Command API должен выражать пользовательское действие: начать рассмотрение, одобрить заявку, отклонить заявку, отправить версию предложения. Это позволяет связать API с application layer и domain layer.

---

### 14.3 Почему frontend не отправляет status напрямую

Будущая формулировка:

> Статус является результатом выполнения команды на backend. Если frontend передаёт новый статус напрямую, бизнес-переход оказывается на клиенте и может обойти domain rules.

---

### 14.4 Почему read DTO может быть screen-specific

Будущая формулировка:

> Read DTO подготавливается под конкретный экран. Это позволяет не отдавать frontend полную domain model и не перегружать список данными, нужными только карточке.

---

### 14.5 Почему availableActions не являются гарантией

Будущая формулировка:

> Available actions отражают состояние на момент чтения. Так как состояние может измениться до отправки команды, backend обязан повторно проверить действие.

---

### 14.6 Почему command result не обязан быть полной model

Будущая формулировка:

> Результат команды должен дать frontend достаточно информации для реакции, но не обязан возвращать полную domain model. При необходимости frontend может запросить актуальный read DTO.

---

### 14.7 Почему нужно учитывать повторные команды

Будущая формулировка:

> Пользователь может отправить команду повторно из-за двойного клика или сетевого повтора. Frontend может блокировать кнопку, но backend всё равно должен проверять текущее состояние.

---

### 14.8 Почему конкурентные действия — это business-state error

Будущая формулировка:

> Если другой пользователь уже изменил объект, команда может стать недоступной. Это не обязательно технический сбой, а ошибка бизнес-состояния, которую нужно вернуть frontend в структурированном виде.

---

### 14.9 Почему upload — command, а download — read-like operation

Будущая формулировка:

> Upload создаёт файл, metadata и новую информацию в договорном обмене, поэтому относится к command API. Download не меняет бизнес-состояние, но требует access check и metadata lookup, поэтому выделяется в отдельный file-сценарий.

---

### 14.10 Почему списки требуют pagination/filter/sort

Будущая формулировка:

> Списки заявок и версий могут расти. Поэтому read API для списков желательно проектировать как управляемую выдачу данных с возможностью сортировки, фильтрации и пагинации.

---

# 15. Визуальные материалы

### 15.1 Основной набор для 2.5.2

```text id="bmyej0"
Обязательно:
1. Рисунок 2.x — Read flow и command flow.
2. Таблица 2.x — Read API web-приложения.
3. Таблица 2.x — Command API и проверяемые условия.
4. Таблица 2.x — Различие read API и command API.
5. Рисунок 2.x — Available actions не заменяют command validation.

Желательно:
6. Рисунок 2.x — Screen-specific read DTO.
7. Рисунок 2.x — Command API принимает intention, а не status.
8. Таблица 2.x — Command result и обновление UI.
9. Таблица 2.x — Риски повторных и конкурентных команд.
10. Рисунок 2.x — Stale read DTO → business-state error.
11. Рисунок 2.x — Read after command.

Опционально:
12. Схема pagination/filter/sort.
13. Схема file metadata in read DTO + file download as separate flow.
14. Схема mock-check as command-like scenario.
```

---

### 15.2 Рисунок 2.x — Read flow и command flow

```text id="qhwk70"
READ FLOW
Frontend
  ↓ request screen data
Backend API
  ↓ read DTO
Frontend
  ↓ render status / actions / file metadata

COMMAND FLOW
Frontend
  ↓ user intention
Backend API
  ↓ request validation
Application layer
  ↓ access / ownership / scenario
Domain layer
  ↓ business rule
Storage
  ↓ save result
Frontend
  ↓ command result or structured error
```

Подпись:

```text id="goceqn"
Рисунок 2.x — Разделение read flow и command flow
```

---

### 15.3 Рисунок 2.x — Command принимает действие, а не status

```text id="vicqm7"
Bad:
PATCH /requests/{id}
{
  "status": "Approved"
}

Good:
POST /requests/{id}/approve
{}

Backend:
1. Проверяет пользователя
2. Проверяет доступ
3. Загружает заявку
4. Проверяет domain rule
5. Сохраняет Approved как результат
```

Подпись:

```text id="d6j6ae"
Рисунок 2.x — Command API принимает действие пользователя, а не новый статус
```

---

### 15.4 Рисунок 2.x — Available actions не заменяют command validation

```text id="ua050r"
Read DTO:
canApprove = true

      ↓ время прошло
      ↓ объект изменился

Command:
approve request

Backend проверяет снова:
success
или
ActionNoLongerAvailable
```

Подпись:

```text id="x7glcw"
Рисунок 2.x — Повторная проверка команды после получения available actions
```

---

### 15.5 Рисунок 2.x — Screen-specific read DTO

```text id="xgv6vy"
Domain model
  ├─ Request
  ├─ Applicant
  ├─ Review
  ├─ Decision
  └─ Exchange

        ↓ projections / mapping

Read DTOs
  ├─ RequestListItemDto
  ├─ RequestDetailsDto
  └─ AgreementExchangeDetailsDto
```

Подпись:

```text id="xn1c6s"
Рисунок 2.x — Экранно-ориентированные read DTO
```

---

### 15.6 Рисунок 2.x — Read after command

```text id="ncid1j"
Frontend sends command:
ApproveRequest

Backend returns:
{ requestId, status: Approved }

Frontend strategy:
A. Update local state
or
B. Request fresh RequestDetailsDto
```

Подпись:

```text id="eqpl1c"
Рисунок 2.x — Обновление интерфейса после command API
```

---

# 16. Карта будущего текста темы 2.5.2

```text id="n5vzk5"
[Абзац 1]
Связать с 2.5.1: API-контракт задаёт границу frontend/backend, теперь нужно разделить read и command.

[Абзац 2]
Объяснить read != command.

[Абзац 3]
Раскрыть read API как модель отображения.

[Абзац 4]
Раскрыть screen-specific read DTO.

[Абзац 5]
Раскрыть read API списков: applicants, client requests, employee requests, proposal history.

[Абзац 6]
Раскрыть карточку заявки как read DTO, собирающий состояние процесса.

[Абзац 7]
Раскрыть read API договорного обмена и file metadata.

[Рисунок 2.x]
Screen-specific read DTO.

[Абзац 8]
Раскрыть availableActions.

[Абзац 9]
Раскрыть stale read DTO.

[Рисунок 2.x]
Available actions не заменяют command validation.

[Абзац 10]
Раскрыть command API как запуск сценария.

[Абзац 11]
Раскрыть command intention вместо status update.

[Рисунок 2.x]
Command принимает action, а не status.

[Абзац 12]
Раскрыть route id и body.

[Абзац 13]
Раскрыть command result DTO.

[Таблица 2.x]
Read API и Command API.

[Абзац 14]
Раскрыть lifecycle commands.

[Абзац 15]
Раскрыть mock-проверку как command-like scenario.

[Абзац 16]
Раскрыть повторные команды и двойной клик.

[Абзац 17]
Раскрыть конкурентные действия как business-state errors.

[Таблица 2.x]
Риски повторных и конкурентных команд.

[Абзац 18]
Раскрыть read after command.

[Абзац 19]
Раскрыть file metadata в read DTO, upload как command и download как read-like operation.

[Переход]
Следующий драфт раскрывает validation и error contract.
```

---

# 17. Черновые фрагменты будущего текста

### Фрагмент 1 — ввод

После определения общего API-контракта необходимо разделить операции чтения и операции изменения состояния. Такое разделение позволяет frontend безопасно получать данные для экранов и отдельно запускать сценарии, изменяющие состояние заявки или договорного обмена.

### Фрагмент 2 — read API

Read API используется для получения данных, необходимых интерфейсу: списков, карточек, статусов, доступных действий и истории договорного обмена. Эти операции не должны изменять состояние процесса.

### Фрагмент 3 — screen DTO

Read DTO может быть спроектирован под конкретный экран. Например, список заявок требует компактных данных, карточка заявки — полного состояния процесса, а договорный обмен — истории версий и metadata документов.

### Фрагмент 4 — command API

Command API запускает пользовательское действие. Команда “одобрить заявку” должна пройти через backend, application layer и domain layer, а не сводиться к передаче нового статуса с frontend.

### Фрагмент 5 — status как результат

Frontend не должен отправлять новый статус заявки как свободное значение. Статус является результатом выполнения команды на backend после проверки доступа и бизнес-правил.

### Фрагмент 6 — availableActions

Available actions помогают frontend показать доступные кнопки, но не гарантируют успешность команды. Состояние объекта может измениться после получения DTO, поэтому backend обязан повторно проверить команду.

### Фрагмент 7 — stale DTO

Если read DTO устарел, command API должен вернуть ошибку бизнес-состояния. Например, если заявка уже была рассмотрена другим сотрудником, повторная команда должна завершиться понятной ошибкой, а не техническим сбоем.

### Фрагмент 8 — command result

Command result DTO должен быть достаточным для обновления интерфейса. После успешной команды frontend может обновить локальное состояние или повторно запросить актуальную карточку заявки.

### Фрагмент 9 — повторные команды

Повторная отправка команды возможна из-за двойного клика или сетевой ошибки. Frontend может блокировать кнопку, но backend всё равно должен проверять текущее состояние объекта.

### Фрагмент 10 — конкурентные действия

Конкурентные действия пользователей должны обрабатываться как ошибки бизнес-состояния. Это позволяет frontend показать понятное сообщение и обновить данные экрана.

### Фрагмент 11 — file metadata и download

Read API договорного обмена может возвращать metadata документов, но не сам physical file. Получение файла выполняется отдельным download-сценарием через backend и проверку доступа.

### Фрагмент 12 — upload как command

Загрузка файла относится к command API, потому что она создаёт physical file, metadata и новую информацию в договорном обмене.

---

# 18. Матрица вопросов для конкретизации темы

### 18.1 Вопросы по read API

| ID | Вопрос                                | Зачем нужен            | Текущее решение / предположение | Как отвечать |
| -- | ------------------------------------- | ---------------------- | ------------------------------- | ------------ |
| R1 | Какие read endpoints есть?            | Для точности           | проверить                       | repo-check   |
| R2 | Есть ли список заявок клиента?        | Для client UI          | проверить                       | repo-check   |
| R3 | Есть ли список заявок сотрудника?     | Для employee UI        | проверить                       | repo-check   |
| R4 | Есть ли details DTO заявки?           | Для карточки           | проверить                       | repo-check   |
| R5 | Есть ли exchange details/history DTO? | Для договорного обмена | проверить                       | repo-check   |
| R6 | Есть ли file metadata DTO?            | Для файлов             | проверить                       | repo-check   |
| R7 | Есть ли screen-specific DTO?          | Для архитектуры        | проверить                       | repo-check   |
| R8 | Есть ли pagination/filter/sort?       | Для списков            | проверить                       | repo-check   |

### 18.2 Вопросы по command API

| ID | Вопрос                         | Зачем нужен           | Текущее решение / предположение | Как отвечать |
| -- | ------------------------------ | --------------------- | ------------------------------- | ------------ |
| C1 | Какие command endpoints есть?  | Для структуры API     | проверить                       | repo-check   |
| C2 | Есть ли start review?          | Для lifecycle         | проверить                       | repo-check   |
| C3 | Есть ли approve/reject?        | Для decision          | проверить                       | repo-check   |
| C4 | Есть ли run data check?        | Для mock-check        | проверить                       | repo-check   |
| C5 | Есть ли start exchange?        | Для договорного этапа | проверить                       | repo-check   |
| C6 | Есть ли send proposal version? | Для exchange          | проверить                       | repo-check   |
| C7 | Есть ли upload proposal file?  | Для file command      | проверить                       | repo-check   |
| C8 | Есть ли command result DTO?    | Для UI update         | проверить                       | repo-check   |

### 18.3 Вопросы по status/intention

| ID  | Вопрос                                        | Зачем нужен      | Текущее решение / предположение | Как отвечать    |
| --- | --------------------------------------------- | ---------------- | ------------------------------- | --------------- |
| SI1 | Передаёт ли frontend status напрямую?         | Риск antipattern | не должен                       | repo-check      |
| SI2 | Есть ли command approve вместо update status? | Важно            | должен быть сценарный command   | repo-check      |
| SI3 | Где определяется итоговый status?             | Для domain       | backend/domain                  | repo-check/text |
| SI4 | Может ли frontend передать senderSide?        | Риск trust       | backend должен определять       | repo-check      |
| SI5 | Какие поля command DTO действительно нужны?   | Для контракта    | проверить                       | repo-check      |

### 18.4 Вопросы по availableActions

| ID | Вопрос                                  | Зачем нужен  | Текущее решение / предположение | Как отвечать    |
| -- | --------------------------------------- | ------------ | ------------------------------- | --------------- |
| A1 | Есть ли availableActions в read DTO?    | Для UI       | проверить                       | repo-check      |
| A2 | Это список строк или boolean flags?     | Для DTO      | проверить                       | repo-check      |
| A3 | Кто рассчитывает availableActions?      | Для boundary | backend/application             | repo-check      |
| A4 | Проверяет ли command действие повторно? | Центрально   | должен                          | repo-check      |
| A5 | Что при stale availableActions?         | Для errors   | business-state error            | repo-check/text |

### 18.5 Вопросы по repeated/concurrent commands

| ID  | Вопрос                                | Зачем нужен     | Текущее решение / предположение | Как отвечать     |
| --- | ------------------------------------- | --------------- | ------------------------------- | ---------------- |
| RC1 | Что при двойном approve?              | Для robustness  | business-state error            | repo-check       |
| RC2 | Что если два сотрудника start review? | Для concurrency | один success, второй error      | repo-check/text  |
| RC3 | Есть ли idempotency-key?              | Не overclaim    | не утверждать                   | repo-check       |
| RC4 | Frontend блокирует кнопку?            | Для UI 2.6      | later                           | repo-check/later |
| RC5 | Есть ли errorCode для conflict?       | Для 2.5.3       | проверить                       | repo-check       |

### 18.6 Вопросы по command result

| ID  | Вопрос                                         | Зачем нужен    | Текущее решение / предположение | Как отвечать |
| --- | ---------------------------------------------- | -------------- | ------------------------------- | ------------ |
| CR1 | Что возвращает create request?                 | Для UI         | проверить                       | repo-check   |
| CR2 | Что возвращает approve/reject?                 | Для UI         | проверить                       | repo-check   |
| CR3 | Что возвращает start exchange?                 | Для navigation | проверить                       | repo-check   |
| CR4 | Что возвращает send proposal/upload?           | Для history    | проверить                       | repo-check   |
| CR5 | UI повторно запрашивает details после command? | Для flow       | проверить                       | repo-check   |

### 18.7 Вопросы по file read/command

| ID | Вопрос                                  | Зачем нужен  | Текущее решение / предположение | Как отвечать |
| -- | --------------------------------------- | ------------ | ------------------------------- | ------------ |
| F1 | File metadata есть в exchange read DTO? | Для UI       | проверить                       | repo-check   |
| F2 | Download отдельный endpoint?            | Для file API | проверить                       | repo-check   |
| F3 | Upload создаёт proposal version?        | Для command  | проверить                       | repo-check   |
| F4 | Upload создаёт metadata?                | Для storage  | проверить                       | repo-check   |
| F5 | Download меняет состояние?              | Обычно нет   | не должен                       | repo-check   |
| F6 | Download проверяет access?              | Центрально   | должен                          | repo-check   |

---

# 19. Что проверить по repo

```text id="k5uwpm"
- список read endpoints;
- список command endpoints;
- есть ли client request list;
- есть ли employee request list;
- есть ли request details DTO;
- есть ли agreement exchange details/history DTO;
- есть ли proposal version DTO;
- есть ли file metadata DTO;
- есть ли availableActions/canApprove/canReject;
- как рассчитываются availableActions;
- есть ли pagination/filter/sort;
- есть ли screen-specific DTO;
- есть ли start review endpoint;
- есть ли run data check endpoint;
- есть ли approve/reject endpoints;
- есть ли update status endpoint;
- передаёт ли frontend status напрямую;
- route id и body разделены ли;
- command result DTO;
- что возвращают create/start/approve/reject/start exchange/send proposal;
- как frontend обновляет экран после command;
- что при повторной command;
- что при concurrent start review;
- какие business-state error codes есть;
- file upload как command;
- file download как read-like operation;
- download идёт через metadata/access check;
- frontend получает metadata, а не storage path;
- какие tests покрывают read endpoints;
- какие tests покрывают command endpoints;
- какие tests покрывают stale/concurrent scenarios.
```

---

# 20. Что отправить визуальному чату

```text id="aa5wmt"
Сделай визуальный разбор темы 2.5.2 “Read API и command API”.

Нужны 3 версии для каждого визуала:

1. Рисунок “Read flow и command flow”.
2. Таблица “Read API web-приложения”.
3. Таблица “Command API и проверяемые условия”.
4. Таблица “Различие read API и command API”.
5. Рисунок “Screen-specific read DTO”.
6. Рисунок “Command API принимает intention, а не status”.
7. Рисунок “Available actions не заменяют command validation”.
8. Рисунок “Stale read DTO → business-state error”.
9. Таблица “Command result и обновление UI”.
10. Таблица “Риски повторных и конкурентных команд”.
11. Опционально — схема “Read after command”.
12. Опционально — схема “pagination/filter/sort”.
13. Опционально — схема “file metadata in read DTO + file download separately”.
14. Опционально — схема “mock-check as command-like scenario”.

Важно:
- показать read API и command API отдельно;
- read API рисовать как получение DTO для экрана;
- command API рисовать как user intention → backend → application → domain → storage;
- показать, что read не меняет состояние;
- показать, что command меняет состояние только после проверки;
- показать, что read DTO может быть screen-specific;
- показать, что availableActions помогают UI, но не гарантируют успех;
- показать stale DTO: canApprove=true, потом состояние изменилось, backend возвращает ActionNoLongerAvailable;
- показать bad/good:
  Bad: frontend sends status;
  Good: frontend sends approve command;
- показать command result DTO как компактный результат;
- показать read after command как способ обновления UI;
- показать повторные команды и двойной клик;
- показать concurrent actions как business-state error;
- показать file metadata в read DTO, но physical file через отдельный download;
- не показывать frontend как прямой доступ к БД;
- не показывать frontend как источник business rules;
- не показывать storage path или storage key в read DTO.
```

---

# 21. Итоговая формула темы

```text id="ga78t3"
read API и command API решают разные задачи;

read API получает данные для отображения:
списки,
карточки,
статусы,
историю договорного обмена,
file metadata
и available actions;

read API не должен менять состояние процесса;

read DTO может быть screen-specific:
список заявок,
карточка заявки
и история договорного обмена
могут иметь разные DTO;

command API запускает пользовательское действие:
создать заявку,
начать рассмотрение,
одобрить,
отклонить,
начать договорный обмен,
отправить версию предложения,
загрузить файл;

command API принимает intention,
а не новый status;

статус является результатом выполнения команды
после проверки backend,
application layer
и domain layer;

route может задавать объект действия,
а body — параметры команды;

availableActions помогают frontend показать кнопки,
но не заменяют backend validation;

read DTO может устареть,
поэтому command API всегда повторно проверяет актуальное состояние;

повторные команды,
двойные клики
и конкурентные действия
должны обрабатываться через проверки состояния
и structured business-state errors;

command result DTO должен быть достаточным для обновления UI,
но не обязан возвращать полную domain model;

после command frontend может повторно запросить read DTO,
чтобы синхронизировать экран с backend;

file metadata может быть частью read DTO,
но physical file получается отдельным download API;

upload файла относится к command API,
потому что создаёт physical file,
metadata
и новую информацию в договорном обмене;

такое разделение делает API понятным,
сценарным,
безопасным
и не позволяет frontend напрямую управлять lifecycle состояния.
```

Следующий драфт:

```text id="lmco7v"
planning/thesis/vkr-topic-workbench/03-chapter-2-design/05-api-frontend-backend/03-validation-and-error-contract.topic.md
```
