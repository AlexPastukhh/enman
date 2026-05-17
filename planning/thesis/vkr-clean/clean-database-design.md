# Clean Database Design

Status: draft

## Реализованная структура L1

В реализованном L1-срезе используются три основные таблицы:

- `L1Accounts`;
- `L1ApplicantParties`;
- `L1ClientRequests`.

## Таблица L1Accounts

Назначение: хранение клиентских аккаунтов.

Основные поля:

- `Id`;
- `AccountType`;
- `Email`;
- `PasswordHash`;
- `Role`;
- `IsActive`;
- `CreatedAt`.

## Таблица L1ApplicantParties

Назначение: хранение заявителей.

Основные поля:

- `Id`;
- `ClientAccountId`;
- `ApplicantPartyType`;
- `Email`;
- `PhoneNumber`;
- `FullName_FirstName`;
- `FullName_MiddleName`;
- `FullName_LastName`;
- `CreatedAt`.

## Таблица L1ClientRequests

Назначение: хранение клиентских заявок.

Основные поля:

- `Id`;
- `ApplicantPartyId`;
- `ClientRequestDiscriminator`;
- `RequestType`;
- `Status`;
- `Details`;
- `CreatedAt`;
- поля адреса объекта.

## Связи

- заявитель связан с клиентским аккаунтом через `ClientAccountId`;
- заявка связана с заявителем через `ApplicantPartyId`;
- удаление связанных данных должно быть ограничено, чтобы не нарушать целостность истории заявок.

## Целевое расширение БД

Для полной версии системы могут потребоваться дополнительные таблицы:

- `Employees` — сотрудники сетевой компании;
- `RequestProcessing` или `RequestReviews` — результаты ручной проверки;
- `DocumentProjects` — проекты договоров и документов;
- `DocumentFiles` — сведения о файлах документов;
- `EmailNotifications` — история email-уведомлений;
- `RequestStatusHistory` — история изменения статусов заявки.

## Замечание по статусам

В коде L1 начальный статус заявки может быть представлен как `Submitted`. В проектных материалах обработки заявок используется статус ожидания рассмотрения. Перед финальной сдачей нужно привести терминологию к одному варианту.
