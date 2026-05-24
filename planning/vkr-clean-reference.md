# VKR Clean Reference

Status: current clean wording guide  
Purpose: provide clean terminology and evidence mapping for VKR/thesis text, presentation and defense speech.

## 1. Purpose

This file is the clean reference layer for VKR-facing materials.

Use it when preparing:

```text
VKR chapters
presentation slides
defense speech
practice report wording
source/evidence explanations
```

Internal planning labels, dirty draft wording and implementation handoff language must be translated into clean domain wording before they appear in VKR-facing materials.

## 2. Allowed VKR Terminology

Use these terms in VKR, presentation and defense speech:

```text
клиентский аккаунт
заявитель
заявка
рассмотрение заявки
сотрудник
решение по заявке
договорный обмен
версия договорного предложения
ссылка на документ
метаданные документа
клиентская часть
серверная часть
API
прикладной сценарий
доменная модель
слой хранения данных
проверка данных
проверка работоспособности
```

## 3. Terms Forbidden in VKR-Facing Materials

Do not use these terms in VKR, presentation, defense speech or practice report:

```text
L1
L2
current planning status
dirty draft
agent
prompt
implementation archive
САПР
Worker
EmployeeRef
ReviewerRef
DocumentFileRef
ContractDraft
ProposalAttachment
```

`ResponsibleEmployeeId` must not be mentioned unless it is actually implemented and relevant to the described behavior.

## 4. Internal-to-Clean Mapping

| Internal/planning term | Clean VKR wording |
|---|---|
| L1 | клиентский контур: аккаунт, заявитель, создание заявки, мои заявки |
| L2 | служебная обработка и договорный обмен |
| L1 backend | серверные сценарии клиентского контура |
| L1 client | клиентская часть приложения |
| Employee Review | рассмотрение заявки сотрудником |
| Agreement Exchange | договорный обмен |
| Agreement Proposal | договорное предложение |
| AgreementDocumentRef | ссылка и метаданные документа |
| current/default ApplicantParty | текущий заявитель / заявитель по умолчанию |
| slice | сценарий реализации / часть реализации |
| implementation archive | внутренний handoff-материал |
| dirty draft | черновая заметка / материал восстановления контекста |

## 5. Evidence Rules

VKR-facing claims must be supported by at least one concrete evidence type:

```text
code path
API endpoint
database table
migration
test
runtime screenshot
generated API contract
```

Planning documents may explain intent, but they are not enough to prove that a feature is implemented.

## 6. Evidence Map

### Claim: client can create a request with an existing or new applicant

Clean wording:

```text
Клиент может создать заявку, выбрав существующего заявителя или введя данные нового заявителя.
```

Evidence:

```text
request creation screen
request creation API
applicant selection / new applicant form
applicant_parties table
connection_requests table
```

### Claim: applicant is separate from client account

Clean wording:

```text
Аккаунт клиента отвечает за доступ к системе, а заявитель хранит данные стороны, от имени которой подаётся заявка.
```

Evidence:

```text
client_accounts table
applicant_parties table with client_account_id
personal account / applicant UI screen
domain model explanation
```

### Claim: changing current/default applicant does not mutate old requests

Clean wording:

```text
Текущий заявитель используется как значение по умолчанию для новых заявок и не изменяет ранее созданные заявки.
```

Evidence:

```text
ApplicantParty current/default behavior
connection_requests linked to applicant_party_id
request creation scenario
domain/application tests if available
```

### Claim: employee reviews request

Clean wording:

```text
Сотрудник открывает заявку, начинает рассмотрение и фиксирует решение.
```

Evidence:

```text
employee request UI screen
EmployeeRequestsController
request_reviews table
review application/domain behavior
```

### Claim: request approval does not automatically create agreement exchange

Clean wording:

```text
Одобрение заявки только допускает договорный этап; договорный обмен начинается отдельным действием сотрудника.
```

Evidence:

```text
process diagram
agreement exchange start action
AgreementExchangesController
agreement_proposal_exchanges table
domain/application behavior
```

### Claim: agreement exchange stores proposal versions

Clean wording:

```text
Договорный обмен хранит историю версий договорных предложений.
```

Evidence:

```text
agreement_proposal_exchanges table
agreement_proposals table
agreement exchange UI screen
domain/application behavior for proposal versions
```

### Claim: client version is a counter-proposal, not ordinary rejection

Clean wording:

```text
Версия документа, отправленная клиентом, рассматривается как контрпредложение, а не как отказ от договорного обмена.
```

Evidence:

```text
agreement proposal state model
agreement_proposals table
agreement exchange scenario
domain/application behavior
```

### Claim: document is stored as reference and metadata

Clean wording:

```text
Документ договорного предложения представлен ссылкой и метаданными: ключом хранения, именем файла, типом содержимого и размером.
```

Evidence:

```text
agreement_document_refs table
AgreementDocumentRef model
document metadata fields
document upload/send proposal scenario
```

### Claim: validation is split between request validation, application orchestration and domain rules

Clean wording:

```text
Проверка формы запроса, прикладная проверка доступа и доменные инварианты разделены по зонам ответственности.
```

Evidence:

```text
FluentValidation validators
application handlers/services
domain methods returning Result/domain errors
API error handling
tests if available
```

## 7. Presentation / Defense Wording Rules

Use short clean wording:

```text
заявка → рассмотрение → договорный обмен
клиентский аккаунт ≠ заявитель
документ хранится как ссылка и метаданные
сервер остаётся источником истины для правил процесса
```

Avoid internal wording:

```text
L1/L2 cut
agent workflow
dirty draft
implementation archive
current status doc
```

## 8. Verification Reminder

Before saying that something is implemented, verify it through the current branch or runtime evidence.

Do not infer implementation state from:

```text
old planning snapshots
dirty drafts
archives
presentation text
unverified notes
```
