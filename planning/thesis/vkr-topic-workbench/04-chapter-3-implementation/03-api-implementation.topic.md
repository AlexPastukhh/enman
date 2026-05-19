# Тема: Реализация API и взаимодействия frontend/backend

**Статус:** быстрый topic-драфт v1 / глава 3 / нужен repo-check по фактическим endpoints, DTO, controllers/handlers, read/command API, validation/errors, upload/download.

1. Зачем тема нужна

Эта тема показывает, как API из главы 2 реализован в коде и как через него frontend взаимодействует с backend.

Главная мысль:

API реализует границу между frontend и backend. Frontend не обращается напрямую к БД, domain layer и файловой системе, а вызывает read endpoints для получения данных и command endpoints для выполнения пользовательских действий.

2. Что показать в разделе

Нужно быстро доказать:

1. Есть backend API.
2. API разделён на read и command операции.
3. Frontend получает DTO, а не domain entities.
4. Command API запускает сценарии, а не просто меняет status.
5. Validation/errors возвращаются структурированно.
6. File upload/download идут через backend.
7. API связан с реализованными slices.
3. Read API

Read API используется для экранов frontend:

- список заявителей;
- список заявок клиента;
- список заявок сотрудника;
- карточка заявки;
- данные review/decision;
- договорный exchange;
- история proposal versions;
- file metadata.

Рабочий текст:

Read API реализует операции получения данных для пользовательского интерфейса. Эти операции не изменяют состояние системы, а возвращают DTO, подготовленные для конкретных экранов: списков, карточек, истории договорного обмена и отображения файлов.

4. Command API

Command API запускает сценарии изменения состояния:

- create applicant;
- create request;
- start review;
- run data check;
- approve request;
- reject request;
- start agreement exchange;
- send proposal version;
- upload proposal file.

Рабочий текст:

Command API реализует пользовательские действия. Frontend отправляет намерение выполнить действие, например одобрить заявку или отправить версию предложения, а backend проверяет доступ, состояние объекта и domain-правила перед сохранением результата.

5. API не должен быть просто update status

Плохая модель:

frontend → передаёт status = Approved

Правильная модель:

frontend → вызывает approve command
backend → проверяет роль, доступ, состояние
domain → проверяет допустимость перехода
storage → сохраняет Approved как результат

Текст:

В реализации важно, что frontend не должен напрямую задавать новый статус заявки. Статус является результатом выполнения команды на backend. Это защищает жизненный цикл заявки от некорректных переходов.

6. DTO

В API используются DTO для отделения frontend от внутренней модели.

Типы DTO:

Request DTO — входные данные команды;
Read DTO — данные для экрана;
Command result DTO — результат действия;
Error DTO — структурированная ошибка;
File metadata DTO — сведения о документе.

Текст:

DTO позволяют не раскрывать frontend внутренние domain entities и структуру БД. Например, карточка заявки может получать агрегированный DTO, включающий статус, данные заявителя, результат рассмотрения и доступные действия.

7. Validation и errors

API должен возвращать разные типы ошибок:

validation error;
unauthorized;
forbidden;
not found;
business-state error;
file error;
unexpected error.

Примеры:

- не заполнено поле формы;
- пользователь не имеет доступа;
- заявка уже рассмотрена;
- действие устарело;
- файл недоступен.

Текст:

Реализация API должна различать ошибки входных данных, доступа и бизнес-состояния. Это позволяет frontend правильно отображать ошибку: подсветить поле формы, показать сообщение о недоступном действии или обновить карточку заявки.

8. File API

Файловые операции выделяются отдельно.

Upload:

frontend отправляет file;
backend проверяет access;
backend сохраняет physical file;
backend создаёт metadata;
metadata связывается с proposal version.

Download:

frontend запрашивает documentId;
backend ищет metadata;
backend проверяет access через proposal/exchange/request;
backend читает physical file;
backend возвращает file или safe error.

Текст:

API загрузки и получения файлов реализует storage-boundary из главы 2. Frontend не получает storage path или storage key, а работает через documentId и backend API.

9. Таблица API по сценариям
Сценарий	Тип API	Что возвращает
Список заявок клиента	read	list DTO
Список заявок сотрудника	read	employee list DTO
Карточка заявки	read	details DTO
Создание заявки	command	requestId/status
Начало review	command	review state
Проверка данных	command-like	check result
Одобрение	command	new status
Отклонение	command	new status + reason
Start exchange	command	exchangeId
Proposal history	read	versions DTO
Upload file	command	documentId/proposalVersionId
Download file	file read	physical file

Подпись:

Таблица 3.x — Реализованные API-операции по сценариям
10. Связь API с frontend

Frontend использует API для:

получения списков;
отображения карточки заявки;
показа availableActions;
отправки форм;
запуска команд;
загрузки файла;
скачивания файла;
обработки ошибок.

Текст:

Frontend не содержит бизнес-правил переходов заявки. Он отображает доступные действия и отправляет команды через API, а backend повторно проверяет возможность выполнения действия.

11. Что проверить по repo
- реальные controllers/endpoints/handlers;
- read endpoints;
- command endpoints;
- DTO classes/types;
- request/response models;
- availableActions/canApprove/canReject;
- validation mechanism;
- error format;
- ProblemDetails, если есть;
- 401/403/404 handling;
- business-state errors;
- upload endpoint;
- download endpoint;
- раскрывается ли storage path;
- frontend API client;
- generated types/OpenAPI, если есть;
- tests API endpoints.
12. Скриншоты / доказательства

Для преддипломной можно показать:

1. Swagger/OpenAPI page, если есть.
2. Backend endpoints/controllers в IDE.
3. DTO classes/types.
4. Frontend API client.
5. Network request из браузера.
6. Ошибка validation в UI.
7. Upload request.
8. Download через backend.
9. Запуск API tests.
13. Черновой текст

API реализует границу между frontend и backend. Пользовательский интерфейс не обращается напрямую к базе данных, domain layer или файловому хранилищу, а использует серверные endpoints.

Операции API разделены на read и command. Read API возвращает данные для экранов: списки заявок, карточку заявки, историю договорного обмена и metadata документов. Command API запускает действия пользователя: создание заявки, начало рассмотрения, одобрение, отклонение, начало договорного обмена и отправку версии предложения.

При выполнении command API frontend не передаёт новый статус как свободное значение. Вместо этого он отправляет намерение выполнить действие, а backend проверяет пользователя, доступ, текущее состояние объекта и domain-правила. Только после этого результат сохраняется в БД.

Файловые операции также проходят через API. При загрузке backend сохраняет physical file и metadata, а при скачивании сначала проверяет доступ через metadata, proposal version, exchange и request. Это позволяет не раскрывать frontend внутренний путь хранения файла.

14. Итоговая формула
API в главе 3 показывает,
как frontend реально связан с backend;

read API возвращает DTO для экранов;

command API запускает сценарии изменения состояния;

frontend не задаёт status напрямую;

backend проверяет access,
application scenario
и domain rules;

DTO отделяют frontend от domain model;

errors позволяют UI показать validation,
business-state,
access
и file problems;

file upload/download идут через backend,
metadata
и access check;

API подтверждает,
что архитектура главы 2 реализована в коде.
