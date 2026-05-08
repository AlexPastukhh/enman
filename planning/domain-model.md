# Domain Model

## Главная архитектурная идея

ФЛ / ИП / ЮЛ — это не разные аккаунты, а разные типы заявителя.

```text
Account        — кто входит в систему
ApplicantParty — от чьего имени подается заявка
Employee       — кто обрабатывает заявку
ClientRequest  — сама заявка
ContractDraft  — проект договора по заявке
```

## Важное правило реализации L1

Этот файл описывает целевую модель по всем слоям, но при реализации L1 запрещено добавлять L2/L3 поля и методы в код, если они не нужны текущему L1-сценарию.

L2/L3 элементы — это план расширения, а не задача L1.

## L1 implementation cut

В L1 реализуются только:

```text
Account
ClientAccount
EmployeeAccount
ApplicantParty
IndividualApplicantParty
ClientRequest
ConnectionRequest
MeteringDeviceRequest
RequestStatus
RequestReview
ReviewDecision
ContractDraft
EmailNotification
```

## Identity / Accounts

### `Account` — L1

Базовая учетная запись пользователя.

L1 поля:

- `Id`;
- `Email`;
- `PasswordHash`;
- `Role`;
- `IsActive`;
- `CreatedAt`.

L2 расширения:

- `EmailConfirmed`;
- `EmailConfirmationToken`;
- `PasswordResetToken`;
- `PasswordResetTokenExpiresAt`.

L3 расширения:

- `LastLoginAt`;
- `LockoutUntil`;
- `AuthProvider`.

Наследники:

- `ClientAccount` — L1;
- `EmployeeAccount` — L1.

### `ClientAccount` — L1

Аккаунт клиента. Отвечает только за вход клиента в систему.

Связан с одним или несколькими `ApplicantParty`.

В L1 обычно один `IndividualApplicantParty`.

### `EmployeeAccount` — L1

Аккаунт сотрудника, который обрабатывает заявки.

L3 может иметь `WindowsIdentityLink` для Negotiate-аутентификации.

## Applicant Parties

### `ApplicantParty` — L1

Базовый класс заявителя.

L1 поля:

- `Id`;
- `ClientAccountId`;
- `ApplicantPartyType`;
- `Email`;
- `PhoneNumber`;
- `CreatedAt`.

L1 методы:

- `GetDisplayName()`.

Не делать в L1:

- `CreateSnapshot()`;
- паспортные данные;
- ИНН;
- ОГРН;
- ОГРНИП;
- анонимного заявителя.

Snapshot лучше создавать в L2 через отдельный сервис/фабрику, чтобы L1-домен не зависел от L2-типа.

### `IndividualApplicantParty` — L1

Физическое лицо.

L1 поля:

- `FullName`;
- `Email`;
- `PhoneNumber`.

L2 расширения:

- `PassportData`;
- `ActualAddress`.

### `EntrepreneurApplicantParty` — L2

Индивидуальный предприниматель.

Поля:

- `FullName`;
- `Inn`;
- `Ogrnip`;
- `RegistrationAddress`.

### `LegalEntityApplicantParty` — L2

Юридическое лицо.

Поля:

- `OrganizationName`;
- `Inn`;
- `Ogrn`;
- `LegalAddress`.

### `AnonymousApplicantParty` — L3

Анонимный заявитель без `ClientAccountId`.

Используется только для L3 anonymous requests.

### `ApplicantSnapshot` — L2

Снимок данных заявителя на момент подачи заявки.

Нужен, чтобы изменение профиля пользователя не меняло исторические заявки.

## Requests

### `ClientRequest` — L1

Базовый aggregate заявки.

L1 поля:

- `Id`;
- `Number`;
- `ApplicantPartyId`;
- `RequestType`;
- `Status`;
- `Details`;
- `ObjectAddress`;
- `CreatedAt`;
- `AssignedEmployeeId`;
- `Reviews`.

L1 методы:

- `Create(...)`;
- `TakeForReview(employeeId)`;
- `Approve(employeeId, comment)`;
- `Reject(employeeId, reason)`;
- `AttachContractDraft(contractDraftId)`;
- `MarkContractDraftSent()`.

L2 расширения:

- `ApplicantSnapshot`;
- `Documents`;
- `History`;
- `VerificationResult`;
- `Comments`.

L3 расширения:

- `Archive()`.

### `ConnectionRequest` — L1

Заявка на технологическое присоединение.

L2 может добавить:

- `RequestedPowerKw`.

### `MeteringDeviceRequest` — L1

Заявка по приборам учета.

L2 может добавить:

- `MeteringDeviceWorkType`.

### `RequestStatus`

L1:

- `Submitted`;
- `InReview`;
- `Approved`;
- `Rejected`;
- `ContractDraftSent`.

L2:

- `NeedClarification`;
- `VerificationInProgress`;
- `VerificationFailed`;
- `ContractDraftPrepared`;
- `CorrectionRequested`;
- `Completed`.

L3:

- `Archived`.

### `RequestReview` — L1

Решение сотрудника по заявке.

L1 поля:

- `Id`;
- `RequestId`;
- `EmployeeId`;
- `Decision`;
- `Comment`;
- `CreatedAt`.

Важно: не использовать `bool IsApproved`. Использовать `ReviewDecision`.

### `ReviewDecision`

L1:

- `Approved`;
- `Rejected`.

L2:

- `NeedClarification`;
- `ManualReviewRequired`.

## Contracts

### `ContractDraft` — L1

Простой проект договора/документа, создаваемый после одобрения заявки.

Рекомендация: в L1 сделать один `ContractDraft` без наследования.

L1 поля:

- `Id`;
- `RequestId`;
- `ContractNumber`;
- `RequestType`;
- `Status`;
- `Text`;
- `CreatedAt`;
- `CreatedByEmployeeId`;
- `PdfFilePath` optional.

L2 расширения:

- `TemplateId`;
- `CurrentVersionId`;
- `SentAt`;
- `AcknowledgedAt`;
- `ContractDraftVersion`;
- `ContractTemplate`;
- `ContractEvent`;
- `ContractAcknowledgement`.

Не делать в L1:

- наследников `ConnectionContractDraft` / `MeteringServiceContractDraft`, если нет реальных отличий;
- юридическое подписание;
- электронную подпись.

### `ContractTemplate` — L2

Шаблон договора.

### `ContractDraftVersion` — L2

Версия проекта договора.

### `ContractAcknowledgement` — L2

Подтверждение ознакомления клиента с проектом договора.

## Documents

### `RequestDocument` — L2

Документ, прикрепленный к заявке.

Типы:

- паспорт;
- доверенность;
- правоустанавливающий документ;
- проект договора;
- технические условия;
- письмо об отказе;
- прочее.

### `GeneratedDocument` — L2

Документ, созданный системой, например PDF договора.

## Notifications

### `NotificationMessage` — L1

Базовое уведомление.

L1 канал:

- `Email`.

L3 каналы:

- `Sms`;
- `InternalMessage`.

### `EmailNotification` — L1

Email-уведомление клиенту.

### `FeedbackTemplate` — L2

Шаблон обратной связи.

### `OutboxMessage` — L3

Надежная отправка уведомлений.

## Verification

L2-only:

- `VerificationRequest`;
- `VerificationResult`;
- `VerificationCheckResult`;
- `ExternalVerificationService`.

Реальные внешние интеграции не делать.

## Security / Audit

L3-only:

- `LoginAttempt`;
- `AccountLock`;
- `SecurityEvent`;
- `AuditLogEntry`;
- `WindowsIdentityLink`;
- `RateLimitRule`;
- `EmailDeliveryAttempt`.

## EF Core mapping strategy

| Hierarchy | Recommended mapping |
|---|---|
| `Account` → `ClientAccount` / `EmployeeAccount` | TPH |
| `ApplicantParty` → ФЛ / ИП / ЮЛ / Anonymous | TPH |
| `ClientRequest` → `ConnectionRequest` / `MeteringDeviceRequest` | TPH |
| `NotificationMessage` → Email / SMS / Internal | TPH |
| `ContractDraft` | One table in L1; type enum or optional TPH in L2 |

Не использовать наследование для:

- `RequestDocument`;
- `ContractTemplate`;
- `FeedbackTemplate`;
- `VerificationResult`;
- `AuditLogEntry`;
- `OutboxMessage`.
