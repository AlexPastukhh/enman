# Clean Results And Future Work

Status: draft / repo-inspection sync  
Scope: achieved results, limitations and future work for VKR conclusion and chapter 3

## Достигнутые результаты

В ходе разработки подготовлены проектные материалы и реализован программный срез web-приложения для автоматизации обработки клиентских заявок, рассмотрения заявок сотрудником и обмена проектами договорных документов.

### 1. Клиентский контур

[IMPLEMENTED / REPO-OBSERVED] Реализована регистрация клиента.

[IMPLEMENTED / REPO-OBSERVED] Реализован вход клиента в систему и загрузка текущего пользователя.

[IMPLEMENTED / REPO-OBSERVED] Реализован выход из системы на серверном уровне.

[IMPLEMENTED / REPO-OBSERVED] Реализовано создание заявителя-физического лица.

[IMPLEMENTED / REPO-OBSERVED] Реализовано получение списка заявителей аккаунта и чтение текущего заявителя.

[IMPLEMENTED / REPO-OBSERVED] Реализовано создание заявки на подключение / обслуживание.

[IMPLEMENTED / REPO-OBSERVED] Реализованы просмотр списка собственных заявок, фильтрация и просмотр деталей заявки.

### 2. Контур сотрудника

[IMPLEMENTED / REPO-OBSERVED] Реализована модель сотрудника как участника обработки заявки.

[IMPLEMENTED / REPO-OBSERVED] Реализованы API и UI для очереди заявок сотрудника.

[IMPLEMENTED / REPO-OBSERVED] Реализован просмотр деталей заявки сотрудником.

[IMPLEMENTED / REPO-OBSERVED] Реализованы операции начала рассмотрения заявки, одобрения и отклонения с обратной связью.

### 3. Договорно-документный контур

[IMPLEMENTED / REPO-OBSERVED] Реализована доменная модель обмена проектами договора / документа: `AgreementProposalExchange`, `AgreementProposal`, `AgreementDocumentRef`, версии предложений и статусы обмена.

[IMPLEMENTED / REPO-OBSERVED] Реализован запуск обмена проектом договора по одобренной заявке.

[IMPLEMENTED / REPO-OBSERVED] Реализованы операции отправки версии документа, просмотра обмена, принятия активного предложения клиентом и финального отказа сотрудника.

[IMPLEMENTED / REPO-OBSERVED] Реализованы клиентские и сотруднические страницы списка и деталей agreement exchange.

Diploma-safe formulation:

```text
В текущей версии реализован срез договорно-документного взаимодействия в форме обмена проектами документа. Этот срез не следует описывать как полноценное юридическое подписание договора; корректнее говорить о подготовке, передаче, согласовании и принятии версии проектного документа в рамках процесса обработки заявки.
```

### 4. Данные, API и контракт

[IMPLEMENTED / REPO-OBSERVED] Настроено хранение данных аккаунтов, заявителей, заявок, сотрудников, результатов рассмотрения и agreement exchange.

[IMPLEMENTED / REPO-OBSERVED] Поддерживается OpenAPI-артефакт `Shared/openapi.json`, generated TypeScript API types, generated constants/error artifacts and typed frontend wrappers.

[IMPLEMENTED / REPO-OBSERVED] Подготовлены server-side validation and ProblemDetails mapping patterns for API responses.

### 5. Тестирование

[IMPLEMENTED / REPO-OBSERVED] Подготовлены domain tests for accounts, applicants, requests, request reviews and agreement proposal exchange behavior.

[IMPLEMENTED / REPO-OBSERVED] Подготовлены integration tests for auth, applicant parties, client requests, employee requests, agreement exchanges and antiforgery behavior.

[IMPLEMENTED / REPO-OBSERVED] Подготовлены client API/model/component tests for forms, lists, filters, request details, employee review actions and agreement widgets.

[IMPLEMENTED / REPO-OBSERVED] Подготовлены Playwright E2E tests for core client flows: registration, login, applicant parties, request creation, my requests and request details.

## Ограничения текущей версии

Текущая версия подтверждает основной программный поток от клиента к заявке, рассмотрению сотрудником и договорно-документному обмену. При этом для финальной защиты необходимо аккуратно отделять реализованный программный срез от полного производственного внедрения.

Current limitations:

```text
- email sending is not yet confirmed as implemented through SMTP/provider/outbox;
- notification history is still better described as designed/planned;
- agreement exchange is implemented as project document exchange, not full legally significant signing;
- deployment/production operation should remain future work unless actually done;
- final screenshots should be regenerated after UI stabilizes;
- employee authentication/demo path should be checked before recording presentation/demo screenshots.
```

## Направления дальнейшего развития

- реализация надежной отправки email-уведомлений через отдельный sender/outbox mechanism;
- хранение истории уведомлений и статуса доставки;
- расширение журнала истории статусов заявки и agreement exchange;
- добавление электронного подписания или интеграции с внешним сервисом подписи;
- развитие шаблонов документов и генерации файлов договоров;
- расширение типов заявителей;
- расширение типов заявок;
- развитие рабочего места сотрудника и административных функций;
- подготовка production deployment configuration;
- аналитика по срокам обработки заявок и состоянию договорного обмена.

## Формулировка для заключения

В результате работы был разработан программный срез web-приложения для автоматизации обработки клиентских заявок в сетевой компании. Реализованная часть охватывает регистрацию и вход клиента, создание заявителя, подачу заявки, просмотр заявок, рассмотрение заявки сотрудником, принятие решения и запуск договорно-документного обмена по одобренной заявке. Для согласования frontend и backend используется контрактный подход на основе OpenAPI, generated TypeScript types and semantic constants. Корректность основных сценариев подтверждается доменными, интеграционными, клиентскими и E2E-тестами. Дальнейшее развитие связано с email-уведомлениями, расширением документооборота, production deployment and operational controls.
