# Clean Database Design

Status: draft / repo-inspection sync

## Реализованная структура L1

В реализованном L1-срезе используются таблицы для клиентских аккаунтов, заявителей, заявок, сотрудников, результатов рассмотрения и договорно-документного обмена.

Core tables / table groups:

```text
- L1Accounts;
- L1ApplicantParties;
- L1ClientRequests;
- request review owned data / review columns;
- L1AgreementProposalExchanges;
- L1AgreementProposals;
- owned value objects for full name, address, email, document refs and proposal authors.
```

Exact table/column names should be rechecked against the latest EF migration/model snapshot before final chapter text.

## Таблица L1Accounts

Назначение: хранение аккаунтов пользователей системы.

Основные поля/данные:

- `Id`;
- `AccountType` / discriminator;
- `Email`;
- `PasswordHash`;
- `Role`;
- `IsActive`;
- `CreatedAt`.

В текущей модели через аккаунт/роль представляются клиенты и сотрудники. Для финального текста нужно уточнить, какие поля сотрудника вынесены в owned object / subtype.

## Таблица L1ApplicantParties

Назначение: хранение заявителей.

Основные поля/данные:

- `Id`;
- `ClientAccountId`;
- `ApplicantPartyType`;
- `Email`;
- `PhoneNumber`;
- `FullName_FirstName`;
- `FullName_MiddleName`;
- `FullName_LastName`;
- `VerificationStatus`;
- `IsCurrentDefault`;
- `CreatedAt`.

## Таблица L1ClientRequests

Назначение: хранение клиентских заявок.

Основные поля/данные:

- `Id`;
- `ClientAccountId`;
- `ApplicantPartyId`;
- `ClientRequestDiscriminator`;
- `RequestType`;
- `Status`;
- `Details`;
- `CreatedAt`;
- поля адреса объекта;
- данные review state / review result, depending on EF mapping.

## Таблицы / данные рассмотрения заявки

Назначение: хранение факта начала и результата проверки заявки сотрудником.

Данные:

```text
- StartedByEmployeeId;
- StartedAt;
- CompletedByEmployeeId;
- CompletedAt / decided time;
- ReviewStatus;
- Decision;
- rejection feedback/reason when rejected.
```

Depending on EF mapping, these данные may be stored as owned data inside request table or separate related structure. Final ERD should be generated from the current migration/model snapshot.

## Таблицы договорно-документного обмена

### L1AgreementProposalExchanges

Назначение: хранение exchange aggregate root.

Основные данные:

```text
- Id;
- RequestId;
- ClientAccountId;
- Status;
- ActiveProposalVersion;
- CreatedAt;
- FinalRefusedByEmployeeId;
- FinalRefusedAt;
- FinalRefusalReason.
```

### L1AgreementProposals

Назначение: хранение версий договорного предложения.

Основные данные:

```text
- AgreementProposalExchangeId;
- Version;
- State;
- Author/Sender;
- Document reference;
- Comment;
- CreatedAt.
```

The agreement document is represented as a document reference in the proposal. Do not describe this as full file storage unless final repo-check confirms binary/file persistence.

## Связи

- заявитель связан с клиентским аккаунтом через `ClientAccountId`;
- заявка связана с клиентским аккаунтом and applicant party;
- review data is linked to the request and employee;
- agreement exchange is linked to an approved request and client account;
- agreement proposals belong to agreement exchange;
- employee/client authorship is stored in proposal author data;
- deletion of historical data should be restricted to preserve request/review/document traceability.

## Still Planned / Future Tables

For the full production version, additional structures may be needed:

```text
- EmailNotifications / notification outbox;
- RequestStatusHistory;
- DocumentFiles / file storage metadata;
- AuditLog;
- ElectronicSignature records;
- Deployment/operation logs.
```

## Замечание по статусам

В коде используются implementation status names. В ПЗ можно сначала объяснять статусы по смыслу на русском языке, а точные enum names дать в таблице или приложении после final repo-check.
