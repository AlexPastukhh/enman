# Evidence Map

Status: draft

Файл фиксирует связь между проектными источниками, реализацией и чистыми материалами ВКР. Он нужен для контроля происхождения материалов и не является литературным текстом диплома.

| Internal source | Clean output | Extracted material | Status | Notes |
|---|---|---|---|---|
| `planning/README.md` | `vkr-outline.md`, `vkr-materials-index.md` | Общая навигация по проектным источникам | draft | Использовать только как карту источников |
| `planning/planning-workflow-current.md` | `evidence-map.md`, `vkr-outline.md` | Текущий статус сценариев, DATA и domain draft | draft | Не переносить служебные формулировки |
| `planning/diagrams/scenario-text-specs/` | `clean-use-cases.md`, `clean-requirements.md` | Сценарии регистрации, входа, создания заявки, рассмотрения заявки, договорных предложений | draft | Переформулировано как пользовательские сценарии |
| `planning/diagrams/scenario-data/` | `clean-data-requirements.md`, `clean-ui-description.md` | Входные, видимые, выбираемые и прикрепляемые данные | draft | Использовать как требования к данным, не как готовую БД |
| `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md` | `clean-use-cases.md`, `clean-domain-model.md` | Создание заявки клиентом | implemented/designed | Код L1 подтверждает создание заявки, статус в коде может отличаться от проектного статуса InReview |
| `planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md` | `clean-use-cases.md`, `clean-domain-model.md` | Рассмотрение заявки сотрудником, Approved/Rejected | designed | Целевая функциональность полной версии |
| `planning/diagrams/scenario-text-specs/SC-13B-agreement-proposal-details-response.md` | `clean-use-cases.md`, `clean-domain-model.md`, `clean-ui-description.md` | Ответ клиента на договорное предложение | designed | Целевая функциональность документооборота |
| `planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md` | `clean-use-cases.md`, `clean-domain-model.md` | Создание проекта договора сотрудником | designed/planned | В теме ВКР описывается как часть документооборота |
| `planning/tables/pre-domain-variants-input.md` | `clean-requirements.md`, `clean-domain-model.md` | Ограничения поведения, жизненные циклы, инварианты | draft | ID можно оставить только для внутреннего traceability |
| `planning/ui/test-site-ui-plan.md` | `clean-ui-description.md`, `presentation/slide-outline.md` | Страницы и действия пользовательского интерфейса | draft | UI-план использовать как основу описания интерфейса |
| `planning/ui/ui-questions-register.md` | `clean-results-and-future-work.md`, `clean-ui-description.md` | Открытые UI-решения | draft | Учитывать как вопросы будущей доработки |
| `Domain.EnergyManagement/L1/L1Domain.cs` | `clean-domain-model.md`, `chapter-3-implementation.md` | Клиентский аккаунт, заявитель, заявка | implemented | Подтверждает реализованный L1-срез |
| `EnergyManagement.Server/Controllers/AuthController.cs` | `chapter-3-implementation.md`, `clean-architecture.md` | Регистрация, вход, получение пользователя | implemented/legacy | Использовать аккуратно: есть основной AuthController и L1 API |
| `EnergyManagement.Server/L1/Controllers/L1Controller.cs` | `clean-architecture.md`, `chapter-3-implementation.md` | L1 API: регистрация клиента, создание заявителя, создание заявки | implemented | Подтверждает реализованные endpoints |
| `EnergyManagement.Server/L1/Persistence/L1DbContext.cs` | `clean-database-design.md`, `chapter-3-implementation.md` | Таблицы L1 и EF Core mapping | implemented | Подтверждает структуру хранения |
| `Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs` | `clean-testing.md`, `chapter-3-implementation.md` | Интеграционные тесты L1-среза | implemented | Использовать для описания проверки |
| Uploaded presentation examples | `presentation/slide-outline.md`, `presentation/speech-draft.md` | Типовая структура защиты: тема, цель, задачи, архитектура, БД, интерфейс, результаты | reference | Использовать только как ориентир структуры защиты |
