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

## L1 aggregate boundaries

The current parallel L1 model is a migration target. EF/API still use the old model until an explicit migration step.

### Current implemented aggregate roots

Current implemented L1 aggregate roots:

- `ClientAccount` owns account/auth lifecycle.
- `IndividualApplicantParty` owns applicant party/profile data and references the owning client account.
- `ConnectionRequest` owns request number, details, object address and status, and references the applicant party.

The current implemented L1 subset is intentionally limited to behavior already represented by the old project: registration/login-related account data, physical-person applicant data, request creation, and `RequestStatus.Submitted`.

### Aggregate root creation

- Aggregate roots are created through their own static `Create` methods or through application services/factories dedicated to that aggregate.
- One aggregate root must not create another aggregate root.
- Aggregate root constructors should stay private/protected where possible.
- Application services may coordinate multiple aggregates, load required state, check cross-aggregate rules, and then call the target aggregate `Create` method or domain method.

### Child entity creation

- Child entities are created only by their owning aggregate root.
- Child entities should not have independent repositories.
- Child entity `Create`/factory methods should be internal/private if they exist.
- External code should not create child entities and then attach them manually.
- Actor is not owner. Future example: `EmployeeAccount` is the reviewer/actor, but the request aggregate owns `RequestReview`.

### Future aggregate candidates and child entities

These future concepts or future responsibilities are preliminary and are not part of the current implemented L1 subset unless explicitly listed above:

- `EmployeeAccount`: future aggregate root when employee flow is implemented.
- `ClientRequest` / `ConnectionRequest` workflow expansion: the current request aggregate root is limited to submitted connection request creation. Later it should own workflow/status transitions and child `RequestReview` entities.
- `RequestReview`: future child entity of `ClientRequest`, not a separate aggregate.
- `RequestDocument`: likely future child entity of `ClientRequest` if documents are submitted/validated as part of request processing.
- `ApplicantProfileDocument`: possible future child entity of `ApplicantParty` only if profile-level reusable applicant documents are introduced.
- `ContractDraft`: undecided. In simple L1 it may be child of `ClientRequest`; in L2 with versions/PDF/templates/separate lifecycle it may become a separate aggregate.
- `EmailNotification` / `OutboxMessage`: not part of the current implemented L1 subset. Later likely application/infrastructure/outbox concern, not necessarily a core aggregate.

### Inter-aggregate references

- Use scalar `long` IDs for inter-aggregate references for now.
- Do not introduce typed IDs yet unless explicitly requested later.
- Examples:
  - `IndividualApplicantParty.ClientAccountId: long`;
  - `ConnectionRequest.ApplicantPartyId: long`.
- Aggregate factories and domain methods should prefer IDs or small role/snapshot value objects over fully loaded aggregate navigation objects.
- Domain behavior must not rely on traversing public navigation graphs across aggregate boundaries.

### EF relationship and navigation rule

- For one-to-many relationships between aggregate roots, model the relationship from the many side with an FK.
- Do not add a collection navigation on the one/principal side just to express one-to-many.
- Example:
  - `ApplicantParty` has `ClientAccountId`;
  - `ClientAccount` does not need an `ApplicantParties` collection as a domain navigation;
  - EF mapping can use `HasOne<ClientAccount>().WithMany().HasForeignKey(x => x.ClientAccountId)`.
- This is a unidirectional relationship / relationship without principal collection navigation, not an absent relationship.
- Optional EF navigation properties between aggregate roots are allowed only as persistence/read convenience, not as domain ownership.
- Domain methods/factories should not require those navigation properties to be loaded.

### Primitive collections

- Do not model aggregate relationships as primitive collections like `List<long> ApplicantPartyIds` or `List<long> DocumentIds`.
- This avoids EF primitive collection tracking/value comparer complexity and keeps ownership clear.
- If the application needs "all applicant parties for account", use a repository/query by `ClientAccountId`.
- If an aggregate needs a collection inside it, it should usually be a child entity collection owned by that aggregate, not a primitive ID collection.

### Cross-aggregate invariants

- If a command needs data from another aggregate, the application service loads that aggregate and checks the rule before calling the target aggregate.
- Example:
  - load `ApplicantParty` by `applicantPartyId`;
  - check `ApplicantParty.ClientAccountId == currentClientAccountId`;
  - call `ConnectionRequest.Create(applicantPartyId, details, address)`.
- Do not push this check into `ConnectionRequest` by requiring the full `ApplicantParty` aggregate object.

## L1 implementation cut

Current implemented parallel L1 subset:

```text
Account
ClientAccount
ApplicantParty
IndividualApplicantParty
ClientRequest
ConnectionRequest
RequestStatus.Submitted
```

Employee/review/contract/email workflow concepts are future candidates until their old-project behavior and tests are intentionally migrated.

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
