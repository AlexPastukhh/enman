# Clean Domain Model

Status: draft / repo-inspection sync

## Общая модель

Доменная модель системы описывает основные сущности процесса обработки клиентских заявок и документооборота в сетевой компании ООО «ЗСК».

Core domain line:

```text
ClientAccount
-> ApplicantParty
-> ConnectionRequest
-> RequestReview
-> AgreementProposalExchange
-> AgreementProposal / AgreementDocumentRef
```

Email notification remains a planned/supporting part unless a sender/outbox implementation is added.

## Реализованные сущности L1

### Account / ClientAccount

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: учетная запись пользователя системы. Для клиента используется `ClientAccount`; сотрудник моделируется как subtype/account role participant in the same L1 account model.

Основные данные:

- email;
- хеш пароля;
- роль;
- признак активности;
- дата создания.

### Employee

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: сотрудник сетевой компании, который рассматривает заявки и участвует в договорно-документном обмене.

Основные действия:

- открыть очередь заявок;
- открыть детали заявки;
- начать рассмотрение;
- одобрить заявку;
- отклонить заявку с обратной связью;
- начать agreement exchange по одобренной заявке;
- отправить новую версию договорного предложения;
- финально отказаться от agreement exchange.

### IndividualApplicantParty

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: заявитель-физическое лицо, связанный с клиентским аккаунтом.

Основные данные:

- идентификатор клиентского аккаунта;
- ФИО;
- email;
- телефон;
- статус проверки / verification status;
- признак current/default для сценариев подачи заявки.

### ConnectionRequest

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: заявка клиента на подключение или связанную услугу.

Основные данные:

- клиентский аккаунт;
- заявитель;
- тип заявки;
- статус;
- описание;
- адрес объекта;
- дата создания;
- review state / review result through `RequestReview`.

### RequestReview

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: результат ручной проверки заявки сотрудником.

Смысл:

```text
employee starts review
-> employee approves or rejects
-> request status changes to approved/rejected
-> review record stores responsible employee and decision timing
```

Important invariant:

```text
A request should be explicitly started for review before approve/reject actions complete the review.
```

### RejectionFeedback

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: обратная связь при отклонении заявки.

### AgreementProposalExchange

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: обмен проектами договора / документа после одобрения заявки.

Смысл:

```text
approved request
-> employee starts exchange with first document proposal
-> client can accept or send own version
-> employee can respond with another version
-> exchange can be accepted or finally refused
```

### AgreementProposal

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: отдельная версия предложения в рамках agreement exchange.

Основные данные:

- version;
- author/sender;
- document reference;
- comment;
- state;
- creation time.

### AgreementDocumentRef

Статус: [IMPLEMENTED / REPO-OBSERVED]

Назначение: ссылка/описание проектного договорного документа в agreement proposal.

Diploma-safe formulation:

```text
В текущей реализации договорный этап представлен не как финальное юридическое подписание, а как обмен версиями проектного документа, связанного с одобренной заявкой.
```

### EmailNotification

Статус: [PLANNED / RECHECK REQUIRED]

Назначение: уведомление клиента о результате обработки заявки или появлении документа.

Current safe note:

```text
Email should be described as a planned notification channel unless repo evidence confirms a concrete sender/outbox/persistence implementation.
```

## Жизненный цикл заявки

Repo-observed/project lifecycle:

```text
Submitted / InReview
-> review started
-> Approved
   -> agreement exchange can be started
-> Rejected
```

Additional status observed in domain:

```text
AgreementExchangeFailed
```

Use exact implementation names only after final repo-check. In reader-facing VKR text it is acceptable to explain status semantics in Russian and put implementation names in parentheses if needed.

## Жизненный цикл договорно-документного обмена

Repo-observed agreement exchange lifecycle:

```text
AwaitingClientConfirmation
-> Accepted
```

Alternative/counter-proposal branch:

```text
AwaitingClientConfirmation
-> client sends own version
-> AwaitingEmployeeResponse
-> employee sends new version
-> AwaitingClientConfirmation
```

Final refusal branch:

```text
AwaitingClientConfirmation / AwaitingEmployeeResponse
-> FinallyRefused
```

## Инварианты

- Клиентская заявка должна иметь заявителя.
- Заявитель должен принадлежать клиентскому аккаунту.
- Клиент не должен получать доступ к чужим заявкам.
- Сотрудник должен иметь роль/права для review actions.
- Рассмотрение заявки должно быть начато перед одобрением или отклонением.
- Одобренная заявка может стать основанием для agreement exchange.
- Agreement exchange должен быть связан с одобренной заявкой и клиентским аккаунтом.
- Версии предложений в agreement exchange должны последовательно заменять активное предложение.
- Email-уведомление не должно отправляться до успешного сохранения соответствующего события, если notification slice будет реализован.
