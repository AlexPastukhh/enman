# Тема: Структура решения и организация реализации

**Статус:** быстрый topic-драфт v1 / глава 3 / нужен repo-check по фактической структуре solution, проектам, слоям backend, frontend, persistence, file storage, tests, tools и запуску приложения.

---

## 1. Зачем тема нужна

Эта тема открывает главу 3 и показывает, как проектная архитектура из главы 2 разложена в реальной структуре решения.

Главная мысль:

> Реализация web-приложения организована как клиент-серверное решение: frontend отвечает за пользовательский интерфейс, backend — за API и выполнение сценариев, application/domain layer — за бизнес-правила, persistence/infrastructure — за хранение данных и файлов, а тестовые проекты и инструменты подтверждают проверяемость реализации.

---

## 2. Что нужно показать в разделе

Для преддипломной и последующей генерации текста нужно зафиксировать:

```text
1. Какие проекты входят в solution.
2. Где расположен frontend.
3. Где расположен backend.
4. Где находятся domain classes.
5. Где находятся application commands/queries/services.
6. Где находятся controllers/endpoints.
7. Где находятся persistence/repositories/DbContext/migrations.
8. Где находится file storage implementation.
9. Где находятся tests/tools.
10. Как эта структура связана с проектными решениями главы 2.
```

Тезис:

> Раздел 3.1 должен не повторять теорию архитектуры, а показать, что выбранная архитектура реально реализована в структуре проекта.

---

## 3. Общая структура решения

Рабочая карта для repo-check:

```text
EnergyManagement.sln
├─ Domain.EnergyManagement/
├─ EnergyManagement.Server/
├─ energymanagement.client/
├─ EnergyManagement.Testing/
├─ EnergyManagement.Tools/
├─ EnergyManagement.Tools.Tests/
└─ tests / другие тестовые проекты, если есть
```

После repo-check нужно уточнить фактические названия проектов и папок.

Черновой текст:

Решение организовано как набор проектов, каждый из которых отвечает за отдельную часть приложения. Frontend-проект реализует пользовательский интерфейс, серверный проект предоставляет API и координирует выполнение сценариев, domain-проект содержит предметные сущности и правила, а infrastructure/persistence-часть отвечает за работу с БД и файловым хранилищем.

---

## 4. Backend как центр выполнения сценариев

Backend отвечает за:

```text
API endpoints;
authentication/authorization;
validation;
application commands;
application queries;
domain transitions;
persistence;
file upload/download;
error contract;
integration with frontend through DTO.
```

Тезис:

> Backend не является простым CRUD-слоем. Он принимает пользовательские команды, проверяет доступ и состояние процесса, применяет application/domain logic и сохраняет результат.

---

## 5. Application layer

Application layer можно раскрыть через:

```text
commands;
queries;
handlers;
application services;
repositories abstractions;
validated input;
read services.
```

Примеры сценариев:

```text
create request;
start review;
run applicant verification;
approve/reject request;
start agreement exchange;
send proposal version;
upload/download document.
```

Тезис:

> Application layer связывает API с предметной логикой и инфраструктурой. Он помогает не смешивать controller code, domain rules и persistence details.

---

## 6. Domain layer

Domain layer нужен для:

```text
request lifecycle;
review lifecycle;
decision rules;
agreement exchange lifecycle;
proposal version rules;
document reference as business link, not raw file path;
account/applicant/employee concepts.
```

Черновой текст:

Предметные сущности и переходы состояния вынесены в domain layer. Это позволяет описывать жизненный цикл заявки и договорного обмена независимо от конкретных экранов и API endpoints.

---

## 7. Persistence и file storage

Persistence часть отвечает за:

```text
DbContext;
repositories;
migrations;
entities mapping;
structured data;
document metadata.
```

File storage отвечает за:

```text
physical file saving;
storage key/path as internal detail;
file read for download;
safe error when file is unavailable.
```

Тезис:

> БД хранит состояние процесса и metadata документов, а физическое содержимое файлов хранится отдельно. Backend связывает эти части через metadata и не раскрывает frontend внутренний путь хранения.

---

## 8. Frontend structure

Frontend реализует:

```text
routes/pages;
client area;
employee area;
forms;
request lists;
request details;
review controls;
agreement exchange screen;
proposal history;
upload/download UI;
loading/error states;
API client.
```

Тезис:

> Frontend отображает состояние процесса и отправляет пользовательские действия через API, но не управляет бизнес-состоянием напрямую.

---

## 9. Tests and tools

В структуре реализации отдельно фиксируются:

```text
unit tests;
API/integration tests, если есть;
frontend tests, если есть;
tools for OpenAPI/client constants, если есть;
test database helpers, если есть.
```

Тезис:

> Наличие тестовых и инструментальных проектов показывает, что реализация не только написана, но и может быть проверена и синхронизирована с API-контрактом.

---

## 10. Таблица для ВКР

| Часть решения | Назначение | Что подтверждает |
|---|---|---|
| Domain project | предметные сущности и правила | lifecycle заявки и договорного обмена |
| Server/API project | backend API и сценарии | связь frontend/backend |
| Application layer | commands/queries/services | выполнение пользовательских сценариев |
| Persistence | БД, repositories, migrations | сохранение состояния процесса |
| File storage | физические файлы | upload/download договорных документов |
| Frontend project | UI клиента и сотрудника | пользовательские сценарии |
| Tests/tools | проверки и генерация артефактов | готовность к демонстрации и сопровождению |

Подпись:

```text
Таблица 3.x — Структура реализации web-приложения
```

---

## 11. Визуалы

```text
1. Схема — Solution structure: frontend → backend API → application/domain → persistence/file storage.
2. Схема — Backend layers: controllers → commands/queries → domain → repositories/storage.
3. Таблица — Проекты solution и их назначение.
4. Screenshot — структура solution в IDE.
5. Screenshot — структура backend L1/application/domain/persistence.
6. Screenshot — frontend routes/pages/components.
```

---

## 12. Demo / evidence

Для преддипломной можно показать:

```text
1. Solution в IDE.
2. Frontend project.
3. Server project.
4. Domain entities.
5. Application handlers.
6. Controllers/endpoints.
7. DbContext/repositories/migrations.
8. File storage directory/service.
9. Test projects / test run.
```

---

## 13. Repo-check

```text
- точные названия проектов solution;
- где расположен frontend;
- где расположен server;
- где расположен domain;
- есть ли отдельный application layer или он внутри server;
- где controllers;
- где commands/queries/handlers;
- где DTO;
- где validation;
- где repositories;
- где DbContext;
- какие migrations актуальны;
- где file storage service;
- где upload directory;
- какие test projects есть;
- какие tools есть;
- как запускается backend;
- как запускается frontend;
- какие команды сборки/тестов проходят.
```

---

## 14. Черновой текст

Реализация web-приложения организована как клиент-серверное решение. Frontend отвечает за отображение пользовательских сценариев клиента и сотрудника, а backend предоставляет API для чтения данных и выполнения команд. Такое разделение соответствует проектным решениям главы 2 и позволяет не смешивать пользовательский интерфейс с бизнес-логикой.

Серверная часть содержит API endpoints, validation, application commands и queries, а также инфраструктурные компоненты для работы с БД и файлами. Предметные правила вынесены в domain layer: там описываются основные сущности, состояния заявки, рассмотрение, решение и договорный обмен.

Хранение данных реализуется через persistence layer. Структурированные данные процесса сохраняются в БД, а файлы договорных предложений сохраняются отдельно. Связь между физическим файлом и договорным процессом выполняется через metadata, доступ к которой контролируется backend.

---

## 15. Итоговая формула

```text
глава 3 начинается с карты реализации;

solution показывает,
что проектные решения главы 2 разложены по реальным проектам и папкам;

frontend отвечает за UI;

backend отвечает за API и application scenarios;

domain layer хранит предметные правила;

persistence хранит structured data;

file storage хранит physical files;

metadata связывает файлы с договорным процессом;

tests/tools подтверждают проверяемость реализации;

раздел 3.1 является мостом
между проектной архитектурой главы 2
и реализованными slices главы 3.
```
