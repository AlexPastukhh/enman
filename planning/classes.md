
Ниже — **целевая модель классов по слоям** с исправленной логикой вокруг `ApplicantParty`.

Главная поправка:  
**ФЛ / ИП / ЮЛ — это не разные “аккаунты”, а разные типы заявителя, то есть `ApplicantParty`.**

Аккаунт отвечает за вход в систему.  
Заявитель отвечает за юридическую/контактную сторону заявки.

То есть лучше разделить:

```text
Account — кто входит в систему
ApplicantParty — от чьего имени подается заявка
Employee — кто обрабатывает заявку
Request — сама заявка
ContractDraft — проект договора по заявке
```

---

# 1. Общая идея модели

## Почему не делать ФЛ/ИП/ЮЛ наследниками аккаунта

Можно было бы сделать так:

```text
Account
 ├── IndividualAccount
 ├── EntrepreneurAccount
 ├── LegalEntityAccount
 └── EmployeeAccount
```

Но это быстро ломает модель.

Проблема в том, что **аккаунт — это техническая сущность входа**, а ФЛ/ИП/ЮЛ — это **юридический статус заявителя**.

Один и тот же пользовательский аккаунт теоретически может:

- подать заявку как физическое лицо;
    
- подать заявку как ИП;
    
- представлять юридическое лицо;
    
- иметь несколько заявителей/организаций.
    

Поэтому правильнее:

```text
Account
 ├── ClientAccount
 └── EmployeeAccount

ApplicantParty
 ├── IndividualApplicantParty
 ├── EntrepreneurApplicantParty
 └── LegalEntityApplicantParty
```

`ClientAccount` может быть связан с одним или несколькими `ApplicantParty`.

---

# 2. Количество слоев

Я бы зафиксировал 4 уровня:

|Уровень|Назначение|
|---|---|
|**Слой 0**|Приведение проекта в порядок, переименование, фиксация архитектуры|
|**Слой 1**|Минимально полноценное приложение для защиты|
|**Слой 2**|Хороший диплом: документы, договоры, ошибки, проверки|
|**Слой 3**|Production-like расширение: безопасность, аудит, outbox, Windows-auth, кэширование|

Слой 0 нужен не в диплом как функциональность, а как рабочий этап.

---

# 3. Слой 1 — обязательное ядро

## 3.1. Что должен уметь слой 1

```text
Клиент регистрируется
→ входит в систему
→ создает заявку
→ сотрудник видит все / необработанные / обработанные заявки
→ сотрудник вручную проверяет заявку
→ одобряет или отклоняет
→ система отправляет email
→ при одобрении создается простой проект договора
```

## 3.2. Сущности слоя 1

### Identity

```text
Account
 ├── ClientAccount
 └── EmployeeAccount
```

### Applicant

```text
ApplicantParty
 └── IndividualApplicantParty
```

В первом слое можно поддержать только физическое лицо как заявителя.

### Requests

```text
ClientRequest
 ├── ConnectionRequest
 └── MeteringDeviceRequest
```

### Reviews

```text
RequestReview
ReviewDecision
```

### Contracts

```text
ContractDraft
```

### Notifications

```text
NotificationMessage
EmailNotification
```

---

# 4. Слой 2 — хороший диплом

## 4.1. Что добавляет слой 2

```text
ФЛ / ИП / ЮЛ
расширенные данные заявителя
документы по заявке
история изменений
статус "требует уточнения"
mock межведомственной проверки
шаблоны обратной связи
договорные шаблоны
версии проекта договора
PDF договора
обработка ошибок
```

## 4.2. Новые сущности слоя 2

### Applicant

```text
ApplicantParty
 ├── IndividualApplicantParty
 ├── EntrepreneurApplicantParty
 └── LegalEntityApplicantParty
```

### Request documents

```text
RequestDocument
DocumentType
GeneratedDocument
```

### Request history

```text
RequestHistoryEntry
RequestComment
```

### Verification

```text
VerificationRequest
VerificationResult
VerificationCheckResult
VerificationStatus
```

### Contract

```text
ContractTemplate
ContractDraftVersion
ContractEvent
ContractAcknowledgement
```

### Feedback

```text
FeedbackTemplate
```

---

# 5. Слой 3 — production-like расширение

## 5.1. Что добавляет слой 3

```text
анонимные заявки
Windows-аутентификация через Negotiate
rate limiting
временная блокировка по email + IP
аудит
security events
outbox для надежной отправки email
повторные попытки отправки
кэширование справочников
health checks
production-настройки
```

## 5.2. Новые сущности слоя 3

```text
AnonymousApplicantParty
WindowsIdentityLink
LoginAttempt
AccountLock
SecurityEvent
AuditLogEntry
OutboxMessage
EmailDeliveryAttempt
RateLimitRule
ReferenceDataCacheEntry
SystemSetting
```

ЭП, реальные СМЭВ-интеграции и платежи лучше оставить **за пределами реализации**.

---

# 6. Итоговая модель классов

## 6.1. Account

### `Account`

Базовый класс для всех пользователей, которые могут входить в систему.

```csharp
public abstract class Account
{
    // [L1]
    public Guid Id { get; protected set; }
    public Email Email { get; protected set; }
    public PasswordHash PasswordHash { get; protected set; }
    public AccountRole Role { get; protected set; }
    public bool IsActive { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }

    // [L2]
    public bool EmailConfirmed { get; protected set; }
    public string? EmailConfirmationToken { get; protected set; }
    public string? PasswordResetToken { get; protected set; }
    public DateTimeOffset? PasswordResetTokenExpiresAt { get; protected set; }

    // [L3]
    public DateTimeOffset? LastLoginAt { get; protected set; }
    public DateTimeOffset? LockoutUntil { get; protected set; }
    public AuthProvider AuthProvider { get; protected set; }

    // [L1]
    public void Activate() { }
    public void Deactivate() { }

    // [L2]
    public void ConfirmEmail(string token) { }
    public void ChangePassword(PasswordHash newPasswordHash) { }
    public void CreatePasswordResetToken() { }
    public void ResetPassword(string token, PasswordHash newPasswordHash) { }

    // [L3]
    public bool CanLogin(DateTimeOffset now) { return true; }
    public void LockUntil(DateTimeOffset dateTime) { }
    public void MarkSuccessfulLogin(DateTimeOffset now) { }
}
```

### Слойность

|Элемент|Слой|
|---|---|
|`Id`, `Email`, `PasswordHash`, `Role`, `IsActive`|1|
|`EmailConfirmed`, password reset|2|
|`LockoutUntil`, `LastLoginAt`, `AuthProvider`|3|

---

## 6.2. `ClientAccount`

Аккаунт клиента.

```csharp
public sealed class ClientAccount : Account
{
    // [L1]
    private readonly List<ApplicantParty> _applicantParties = new();
    public IReadOnlyCollection<ApplicantParty> ApplicantParties => _applicantParties.AsReadOnly();

    // [L1]
    public static ClientAccount Register(Email email, PasswordHash passwordHash)
    {
        throw new NotImplementedException();
    }

    // [L1]
    public void AddApplicantParty(ApplicantParty applicantParty)
    {
    }

    // [L3]
    public void RemoveApplicantParty(Guid applicantPartyId)
    {
    }
}
```

### Почему так

Клиентский аккаунт — это вход в систему.  
А `ApplicantParty` — это тот, от чьего имени подается заявка.

На первом слое у клиента может быть один `IndividualApplicantParty`.  
На втором — можно добавить ИП и ЮЛ.  
На третьем — можно разрешить несколько организаций/заявителей на один аккаунт.

---

## 6.3. `EmployeeAccount`

Аккаунт сотрудника.

```csharp
public sealed class EmployeeAccount : Account
{
    // [L1]
    public EmployeeProfile Profile { get; private set; }

    // [L2]
    public string? Department { get; private set; }
    public string? Position { get; private set; }

    // [L3]
    public WindowsIdentityLink? WindowsIdentityLink { get; private set; }

    // [L1]
    public static EmployeeAccount Create(Email email, PasswordHash passwordHash, EmployeeProfile profile)
    {
        throw new NotImplementedException();
    }

    // [L3]
    public void LinkWindowsIdentity(WindowsIdentityLink link)
    {
    }
}
```

Сотрудники не являются заявителями, поэтому они не должны наследоваться от `ApplicantParty`.

---

# 7. ApplicantParty

## 7.1. `ApplicantParty`

Базовый класс заявителя.

```csharp
public abstract class ApplicantParty
{
    // [L1]
    public Guid Id { get; protected set; }
    public Guid? ClientAccountId { get; protected set; }
    public ApplicantPartyType Type { get; protected set; }
    public Email Email { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }

    // [L2]
    public Address? ActualAddress { get; protected set; }

    // [L3]
    public bool IsAnonymous => ClientAccountId is null;

    // [L1]
    public abstract string GetDisplayName();

    // [L2]
    public abstract ApplicantSnapshot CreateSnapshot();

    // [L2]
    public virtual void UpdateContactData(Email email, PhoneNumber phoneNumber)
    {
    }
}
```

## Главное решение

`ClientAccountId` nullable.

|Сценарий|`ClientAccountId`|
|---|---|
|Авторизованный клиент|заполнен|
|Анонимная заявка в слое 3|`null`|

Так мы заранее не ломаем архитектуру под будущие анонимные заявки.

---

## 7.2. `IndividualApplicantParty`

Физическое лицо.

```csharp
public sealed class IndividualApplicantParty : ApplicantParty
{
    // [L1]
    public FullName FullName { get; private set; }

    // [L2]
    public PassportData? PassportData { get; private set; }

    // [L1]
    public static IndividualApplicantParty Create(
        Guid clientAccountId,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public void ProvidePassportData(PassportData passportData)
    {
    }

    // [L1]
    public override string GetDisplayName()
    {
        return FullName.ToString();
    }

    // [L2]
    public override ApplicantSnapshot CreateSnapshot()
    {
        throw new NotImplementedException();
    }
}
```

---

## 7.3. `EntrepreneurApplicantParty`

Индивидуальный предприниматель.

Появляется во втором слое.

```csharp
public sealed class EntrepreneurApplicantParty : ApplicantParty
{
    // [L2]
    public FullName FullName { get; private set; }
    public Inn Inn { get; private set; }
    public Ogrnip Ogrnip { get; private set; }
    public Address RegistrationAddress { get; private set; }

    // [L2]
    public static EntrepreneurApplicantParty Create(
        Guid clientAccountId,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Inn inn,
        Ogrnip ogrnip,
        Address registrationAddress)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public override string GetDisplayName()
    {
        return FullName.ToString();
    }

    // [L2]
    public override ApplicantSnapshot CreateSnapshot()
    {
        throw new NotImplementedException();
    }
}
```

---

## 7.4. `LegalEntityApplicantParty`

Юридическое лицо.

Появляется во втором слое.

```csharp
public sealed class LegalEntityApplicantParty : ApplicantParty
{
    // [L2]
    public string OrganizationName { get; private set; }
    public Inn Inn { get; private set; }
    public Ogrn Ogrn { get; private set; }
    public Address LegalAddress { get; private set; }

    // [L2]
    public static LegalEntityApplicantParty Create(
        Guid clientAccountId,
        string organizationName,
        Email email,
        PhoneNumber phoneNumber,
        Inn inn,
        Ogrn ogrn,
        Address legalAddress)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public override string GetDisplayName()
    {
        return OrganizationName;
    }

    // [L2]
    public override ApplicantSnapshot CreateSnapshot()
    {
        throw new NotImplementedException();
    }
}
```

---

## 7.5. `AnonymousApplicantParty`

Появляется только в третьем слое.

```csharp
public sealed class AnonymousApplicantParty : ApplicantParty
{
    // [L3]
    public string DisplayName { get; private set; }

    // [L3]
    public static AnonymousApplicantParty Create(
        string displayName,
        Email email,
        PhoneNumber phoneNumber)
    {
        throw new NotImplementedException();
    }

    // [L3]
    public override string GetDisplayName()
    {
        return DisplayName;
    }

    // [L3]
    public override ApplicantSnapshot CreateSnapshot()
    {
        throw new NotImplementedException();
    }
}
```

---

# 8. ApplicantSnapshot

Снимок данных заявителя на момент подачи заявки.

```csharp
public sealed class ApplicantSnapshot
{
    // [L2]
    public ApplicantPartyType ApplicantType { get; private set; }
    public string DisplayName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    // [L2]
    public string? PassportSeries { get; private set; }
    public string? PassportNumber { get; private set; }

    // [L2]
    public string? Inn { get; private set; }
    public string? Ogrn { get; private set; }
    public string? Ogrnip { get; private set; }
    public string? OrganizationName { get; private set; }
    public string? LegalAddress { get; private set; }

    // [L2]
    public static ApplicantSnapshot From(ApplicantParty party)
    {
        throw new NotImplementedException();
    }
}
```

На первом слое можно не хранить полный snapshot, но лучше заложить место под него.

---

# 9. Requests

## 9.1. `ClientRequest`

Базовый класс заявки.

```csharp
public abstract class ClientRequest
{
    // [L1]
    public Guid Id { get; protected set; }
    public RequestNumber Number { get; protected set; }
    public Guid ApplicantPartyId { get; protected set; }
    public RequestType Type { get; protected set; }
    public RequestStatus Status { get; protected set; }
    public string Details { get; protected set; }
    public Address ObjectAddress { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public Guid? AssignedEmployeeId { get; protected set; }

    // [L2]
    public ApplicantSnapshot? ApplicantSnapshot { get; protected set; }
    public VerificationResult? VerificationResult { get; protected set; }

    // [L1]
    private readonly List<RequestReview> _reviews = new();
    public IReadOnlyCollection<RequestReview> Reviews => _reviews.AsReadOnly();

    // [L2]
    private readonly List<RequestDocument> _documents = new();
    public IReadOnlyCollection<RequestDocument> Documents => _documents.AsReadOnly();

    private readonly List<RequestHistoryEntry> _history = new();
    public IReadOnlyCollection<RequestHistoryEntry> History => _history.AsReadOnly();

    // [L1]
    public void TakeForReview(Guid employeeId)
    {
    }

    // [L1]
    public void Approve(Guid employeeId, string comment)
    {
    }

    // [L1]
    public void Reject(Guid employeeId, string reason)
    {
    }

    // [L1]
    public void AttachContractDraft(Guid contractDraftId)
    {
    }

    // [L1]
    public void MarkContractDraftSent()
    {
    }

    // [L2]
    public void RequireClarification(Guid employeeId, string comment)
    {
    }

    // [L2]
    public void AttachDocument(RequestDocument document)
    {
    }

    // [L2]
    public void ApplyVerificationResult(VerificationResult result)
    {
    }

    // [L2]
    public void AddHistory(RequestHistoryEntry entry)
    {
    }

    // [L3]
    public void Archive()
    {
    }
}
```

---

## 9.2. `ConnectionRequest`

Заявка на технологическое присоединение.

```csharp
public sealed class ConnectionRequest : ClientRequest
{
    // [L1]
    public static ConnectionRequest Create(
        Guid applicantPartyId,
        Address objectAddress,
        string details)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public decimal? RequestedPowerKw { get; private set; }

    // [L2]
    public void SpecifyRequestedPower(decimal powerKw)
    {
    }
}
```

---

## 9.3. `MeteringDeviceRequest`

Заявка по приборам учета.

```csharp
public sealed class MeteringDeviceRequest : ClientRequest
{
    // [L1]
    public static MeteringDeviceRequest Create(
        Guid applicantPartyId,
        Address objectAddress,
        string details)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public MeteringDeviceWorkType? WorkType { get; private set; }

    // [L2]
    public void SpecifyWorkType(MeteringDeviceWorkType workType)
    {
    }
}
```

---

## 9.4. `RequestStatus`

```csharp
public enum RequestStatus
{
    // [L1]
    Submitted,
    InReview,
    Approved,
    Rejected,
    ContractDraftSent,

    // [L2]
    NeedClarification,
    VerificationInProgress,
    VerificationFailed,
    ContractDraftPrepared,
    CorrectionRequested,
    Completed,

    // [L3]
    Archived
}
```

---

## 9.5. `RequestType`

```csharp
public enum RequestType
{
    // [L1]
    Connection,
    MeteringDevice,

    // [L3]
    Other
}
```

---

# 10. RequestReview

```csharp
public sealed class RequestReview
{
    // [L1]
    public Guid Id { get; private set; }
    public Guid RequestId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public ReviewDecision Decision { get; private set; }
    public string Comment { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    // [L2]
    public Guid? FeedbackTemplateId { get; private set; }
    public Guid? VerificationResultId { get; private set; }

    // [L1]
    public static RequestReview Approve(Guid requestId, Guid employeeId, string comment)
    {
        throw new NotImplementedException();
    }

    // [L1]
    public static RequestReview Reject(Guid requestId, Guid employeeId, string reason)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public static RequestReview NeedClarification(Guid requestId, Guid employeeId, string comment)
    {
        throw new NotImplementedException();
    }
}
```

```csharp
public enum ReviewDecision
{
    // [L1]
    Approved,
    Rejected,

    // [L2]
    NeedClarification,
    ManualReviewRequired
}
```

---

# 11. Contracts

## 11.1. `ContractDraft`

```csharp
public abstract class ContractDraft
{
    // [L1]
    public Guid Id { get; protected set; }
    public Guid RequestId { get; protected set; }
    public ContractNumber ContractNumber { get; protected set; }
    public ContractDraftStatus Status { get; protected set; }
    public string Text { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public Guid CreatedByEmployeeId { get; protected set; }
    public string? PdfFilePath { get; protected set; }

    // [L2]
    public Guid? TemplateId { get; protected set; }
    public Guid? CurrentVersionId { get; protected set; }
    public DateTimeOffset? SentAt { get; protected set; }
    public DateTimeOffset? AcknowledgedAt { get; protected set; }

    // [L1]
    public void MarkSent()
    {
    }

    // [L1]
    public void AttachPdf(string filePath)
    {
    }

    // [L2]
    public void AddVersion(ContractDraftVersion version)
    {
    }

    // [L2]
    public void Acknowledge(Guid userId)
    {
    }

    // [L2]
    public void RequestCorrection(string comment)
    {
    }

    // [L2]
    public void AcceptForOfflineSigning()
    {
    }

    // [L3]
    public void Cancel(string reason)
    {
    }
}
```

---

## 11.2. `ConnectionContractDraft`

```csharp
public sealed class ConnectionContractDraft : ContractDraft
{
    // [L1]
    public static ConnectionContractDraft CreateSimple(
        Guid requestId,
        Guid employeeId,
        string text)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public static ConnectionContractDraft CreateFromTemplate(
        Guid requestId,
        Guid employeeId,
        ContractTemplate template)
    {
        throw new NotImplementedException();
    }
}
```

---

## 11.3. `MeteringServiceContractDraft`

```csharp
public sealed class MeteringServiceContractDraft : ContractDraft
{
    // [L1]
    public static MeteringServiceContractDraft CreateSimple(
        Guid requestId,
        Guid employeeId,
        string text)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public static MeteringServiceContractDraft CreateFromTemplate(
        Guid requestId,
        Guid employeeId,
        ContractTemplate template)
    {
        throw new NotImplementedException();
    }
}
```

---

## 11.4. `ContractDraftStatus`

```csharp
public enum ContractDraftStatus
{
    // [L1]
    DraftCreated,
    SentToApplicant,

    // [L2]
    PdfGenerated,
    ApplicantViewed,
    CorrectionRequested,
    AcceptedForOfflineSigning,

    // [L3]
    Cancelled
}
```

---

## 11.5. `ContractTemplate`

```csharp
public sealed class ContractTemplate
{
    // [L2]
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public RequestType RequestType { get; private set; }
    public ApplicantPartyType ApplicantPartyType { get; private set; }
    public string TemplateBody { get; private set; }
    public int Version { get; private set; }
    public bool IsActive { get; private set; }

    // [L2]
    public string Render(object data)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public void Deactivate()
    {
    }
}
```

---

## 11.6. `ContractDraftVersion`

```csharp
public sealed class ContractDraftVersion
{
    // [L2]
    public Guid Id { get; private set; }
    public Guid ContractDraftId { get; private set; }
    public int VersionNumber { get; private set; }
    public string Text { get; private set; }
    public string? PdfFilePath { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public string? Comment { get; private set; }
}
```

---

# 12. Notifications

## 12.1. `NotificationMessage`

```csharp
public abstract class NotificationMessage
{
    // [L1]
    public Guid Id { get; protected set; }
    public Guid? RequestId { get; protected set; }
    public Guid? RecipientUserId { get; protected set; }
    public string Recipient { get; protected set; }
    public NotificationChannel Channel { get; protected set; }
    public string Subject { get; protected set; }
    public string Body { get; protected set; }
    public NotificationStatus Status { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? SentAt { get; protected set; }

    // [L2]
    public string? ErrorMessage { get; protected set; }

    // [L1]
    public void MarkSent()
    {
    }

    // [L2]
    public void MarkFailed(string error)
    {
    }

    // [L3]
    public void ScheduleRetry()
    {
    }
}
```

---

## 12.2. `EmailNotification`

```csharp
public sealed class EmailNotification : NotificationMessage
{
    // [L1]
    public static EmailNotification Create(
        Guid requestId,
        string recipientEmail,
        string subject,
        string body)
    {
        throw new NotImplementedException();
    }
}
```

---

## 12.3. `NotificationChannel`

```csharp
public enum NotificationChannel
{
    // [L1]
    Email,

    // [L3]
    Sms,
    InternalMessage
}
```

---

## 12.4. `FeedbackTemplate`

```csharp
public sealed class FeedbackTemplate
{
    // [L2]
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public RequestType RequestType { get; private set; }
    public ApplicantPartyType? ApplicantPartyType { get; private set; }
    public ReviewDecision Decision { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public bool IsActive { get; private set; }

    // [L2]
    public string Render(object data)
    {
        throw new NotImplementedException();
    }
}
```

---

# 13. Documents

## `RequestDocument`

```csharp
public sealed class RequestDocument
{
    // [L2]
    public Guid Id { get; private set; }
    public Guid RequestId { get; private set; }
    public DocumentType DocumentType { get; private set; }
    public string FileName { get; private set; }
    public string FilePath { get; private set; }
    public string ContentType { get; private set; }
    public long SizeBytes { get; private set; }
    public Guid UploadedByUserId { get; private set; }
    public DateTimeOffset UploadedAt { get; private set; }
    public bool IsGenerated { get; private set; }

    // [L3]
    public DateTimeOffset? DeletedAt { get; private set; }

    // [L2]
    public static RequestDocument CreateUploaded(...)
    {
        throw new NotImplementedException();
    }

    // [L2]
    public static RequestDocument CreateGenerated(...)
    {
        throw new NotImplementedException();
    }

    // [L3]
    public void MarkDeleted()
    {
    }
}
```

---

# 14. Verification

## `VerificationResult`

```csharp
public sealed class VerificationResult
{
    // [L2]
    public Guid Id { get; private set; }
    public Guid RequestId { get; private set; }
    public VerificationStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public string? ErrorMessage { get; private set; }

    private readonly List<VerificationCheckResult> _checks = new();
    public IReadOnlyCollection<VerificationCheckResult> Checks => _checks.AsReadOnly();

    // [L2]
    public bool HasBlockingErrors()
    {
        throw new NotImplementedException();
    }
}
```

---

# 15. Security / Production-like

## `LoginAttempt`

```csharp
public sealed class LoginAttempt
{
    // [L3]
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string IpAddress { get; private set; }
    public string UserAgent { get; private set; }
    public bool IsSuccessful { get; private set; }
    public string? FailureReason { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}
```

## `AccountLock`

```csharp
public sealed class AccountLock
{
    // [L3]
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string IpAddress { get; private set; }
    public DateTimeOffset LockedUntil { get; private set; }
    public string Reason { get; private set; }

    // [L3]
    public bool IsActive(DateTimeOffset now)
    {
        return now < LockedUntil;
    }
}
```

## `OutboxMessage`

```csharp
public sealed class OutboxMessage
{
    // [L3]
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Payload { get; private set; }
    public OutboxStatus Status { get; private set; }
    public int RetryCount { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? ProcessedAt { get; private set; }
}
```

---

# 16. EF Core mapping strategies

Теперь про маппинг.

## 16.1. Account inheritance

```text
Account
 ├── ClientAccount
 └── EmployeeAccount
```

### Рекомендация: TPH

Одна таблица:

```text
Accounts
```

С колонкой:

```text
AccountType
```

Почему TPH:

- проще;
    
- быстрее;
    
- меньше joins;
    
- легче для диплома;
    
- удобно хранить всех пользователей в одной таблице.
    

---

## 16.2. ApplicantParty inheritance

```text
ApplicantParty
 ├── IndividualApplicantParty
 ├── EntrepreneurApplicantParty
 ├── LegalEntityApplicantParty
 └── AnonymousApplicantParty
```

### Рекомендация: TPH

Одна таблица:

```text
ApplicantParties
```

С колонкой:

```text
ApplicantPartyType
```

Почему:

- заявки часто нужно показывать общим списком;
    
- сотруднику не важно, ФЛ это или ИП, сначала ему нужен общий список;
    
- разные поля можно хранить nullable;
    
- для диплома проще объяснить.
    

Если nullable-колонок станет слишком много, позже можно перейти на TPT, но для текущего проекта TPH лучше.

---

## 16.3. ClientRequest inheritance

```text
ClientRequest
 ├── ConnectionRequest
 └── MeteringDeviceRequest
```

### Рекомендация: TPH

Одна таблица:

```text
Requests
```

С колонкой:

```text
RequestType
```

Почему:

- общий жизненный цикл одинаковый;
    
- общий список заявок нужен всегда;
    
- различия между типами заявок пока небольшие;
    
- проще фильтровать.
    

---

## 16.4. ContractDraft inheritance

```text
ContractDraft
 ├── ConnectionContractDraft
 └── MeteringServiceContractDraft
```

### Рекомендация: TPH или без наследования

Для первого слоя можно вообще сделать один `ContractDraft`.

Во втором слое, если различий станет больше, можно добавить наследников.

Рекомендация:

- L1: один `ContractDraft`;
    
- L2: можно добавить `ContractDraftType`;
    
- L3: наследование только если реально появились разные правила.
    

То есть здесь наследование не обязательно.

---

## 16.5. Notification inheritance

```text
NotificationMessage
 ├── EmailNotification
 ├── SmsNotification
 └── InternalNotification
```

### Рекомендация: TPH

Одна таблица:

```text
Notifications
```

С колонкой:

```text
Channel
```

---

# 17. Что лучше НЕ делать через наследование

Не всё нужно наследовать.

## Не надо наследовать

```text
RequestDocument
ContractTemplate
FeedbackTemplate
VerificationResult
AuditLogEntry
OutboxMessage
```

Почему:

- это не разные “виды поведения”;
    
- проще использовать enum `DocumentType`, `TemplateType`, `Channel`;
    
- меньше EF-сложности;
    
- проще поддерживать.
    

---

# 18. Итоговая таблица: класс → слой → маппинг

|Класс|Слой появления|Наследование|Маппинг|
|---|--:|---|---|
|`Account`|L1|base|TPH|
|`ClientAccount`|L1|derived|TPH|
|`EmployeeAccount`|L1|derived|TPH|
|`WindowsIdentityLink`|L3|нет|отдельная таблица|
|`ApplicantParty`|L1|base|TPH|
|`IndividualApplicantParty`|L1|derived|TPH|
|`EntrepreneurApplicantParty`|L2|derived|TPH|
|`LegalEntityApplicantParty`|L2|derived|TPH|
|`AnonymousApplicantParty`|L3|derived|TPH|
|`ClientRequest`|L1|base|TPH|
|`ConnectionRequest`|L1|derived|TPH|
|`MeteringDeviceRequest`|L1|derived|TPH|
|`RequestReview`|L1|нет|отдельная таблица|
|`ContractDraft`|L1|optional base|одна таблица|
|`ConnectionContractDraft`|L2/optional|derived|TPH, если нужно|
|`MeteringServiceContractDraft`|L2/optional|derived|TPH, если нужно|
|`NotificationMessage`|L1|base|TPH|
|`EmailNotification`|L1|derived|TPH|
|`SmsNotification`|L3|derived|TPH|
|`RequestDocument`|L2|нет|отдельная таблица|
|`ContractTemplate`|L2|нет|отдельная таблица|
|`FeedbackTemplate`|L2|нет|отдельная таблица|
|`VerificationResult`|L2|нет|отдельная таблица|
|`LoginAttempt`|L3|нет|отдельная таблица|
|`AccountLock`|L3|нет|отдельная таблица|
|`OutboxMessage`|L3|нет|отдельная таблица|

---

# 19. Самое важное решение

## Делать наследование здесь:

```text
Account
ApplicantParty
ClientRequest
NotificationMessage
```

## Осторожно с наследованием здесь:

```text
ContractDraft
```

## Не делать наследование здесь:

```text
Document
Template
Verification
Audit
Outbox
```

---

# 20. Как расширять без переписывания

Главное правило:

## В слой 1 не добавлять поля слоя 2 и 3 напрямую в основные классы, если они не нужны

Например, не надо сразу пихать в `ClientRequest`:

```text
PassportData
Ogrn
Ogrnip
SmsStatus
LockoutReason
SignatureCertificate
```

Лучше:

- паспорт и ИНН — в `ApplicantParty`;
    
- копия данных — в `ApplicantSnapshot`;
    
- документы — в `RequestDocument`;
    
- проверки — в `VerificationResult`;
    
- договоры — в `ContractDraft`;
    
- уведомления — в `NotificationMessage`;
    
- аудит — в `AuditLogEntry`.
    

Тогда `ClientRequest` остается чистым aggregate root, а новые слои подключаются рядом.

---

# 21. Минимальный набор для начала разработки

Начинай с этого:

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

Потом слой 2:

```text
EntrepreneurApplicantParty
LegalEntityApplicantParty
ApplicantSnapshot
RequestDocument
VerificationResult
ContractTemplate
ContractDraftVersion
FeedbackTemplate
RequestHistoryEntry
```

Потом слой 3:

```text
AnonymousApplicantParty
WindowsIdentityLink
LoginAttempt
AccountLock
SecurityEvent
AuditLogEntry
OutboxMessage
EmailDeliveryAttempt
```

Это даст расширяемую структуру без необходимости заново проектировать домен на каждом этапе.