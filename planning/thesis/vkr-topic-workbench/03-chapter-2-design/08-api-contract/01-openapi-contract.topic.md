# Topic: OpenAPI контракт

Status: material-harvest-needed

## 1. Зачем эта тема нужна

API-контракт показывает, как согласуется серверная и клиентская части web-приложения. Это сильная инженерная тема проекта, потому что она связывает ASP.NET Core API, OpenAPI, TypeScript-типы и клиентские формы.

## 2. Куда входит

ПЗ:
- 2.8 API-контракт приложения
- 3.5 Реализация API-контракта

Отчёт по ПП:
- архитектура и API frontend/backend

Презентация:
- слайд “API-контракт”

Доклад:
- тезис о снижении рассинхронизации frontend/backend.

## 3. Что нужно добиться

Преподаватель должен понять:
- сервер публикует контракт API;
- frontend использует generated types/constants;
- это снижает риск различий между backend и frontend;
- ProblemDetails помогает унифицировать ошибки/валидацию.

Нужно обязательно раскрыть:
- ASP.NET Core controllers;
- OpenAPI;
- generated TypeScript types;
- typed API wrappers;
- ProblemDetails.

Нужно показать:
- API contract flow diagram;
- fragment/table of endpoint groups.

## 4. План раскрытия темы

Flow:
1. Объяснить проблему frontend/backend drift.
2. Показать OpenAPI как контракт.
3. Показать generated TypeScript types/constants.
4. Показать typed wrappers and forms.
5. Указать роль ProblemDetails.

## 5. Запланированная реализация раскрытия

Берём:
- api-contract-and-client-server-sync;
- Shared/openapi.json;
- generated frontend files;
- API wrapper files;
- tests/checks if present.

Подаём так:
- не как абстрактную теорию REST;
- как конкретный механизм согласования частей проекта.

Обосновываем так:
- typed contract reduces manual duplication and mismatch.

Вставляем визуалы:
- controller -> OpenAPI -> generated types -> wrappers -> UI.

Ссылки/источники:
- official OpenAPI / Microsoft docs if needed.

Repo-check:
- verify current generation scripts and generated artifacts.

## 6. Материалы для harvest

Planning docs:
- planning/thesis/vkr-clean/api-contract-and-client-server-sync.md

Repo evidence:
- Shared/openapi.json;
- generated TypeScript types;
- fetchJson/API wrappers;
- package scripts.

Research:
- OpenAPI specification / official docs.

Other chats:
- author discussion about API contract and frontend/backend sync.

## 7. Визуалы

Main:
- API contract flow.

Optional:
- table endpoint group -> frontend feature.

Appendix:
- OpenAPI fragment if needed.

## 8. Вопросы и решения

| Вопрос | Текущий ответ | Статус |
|---|---|---|
| Вставлять ли код OpenAPI? | Нет, лучше схема и таблица; код/фрагменты только в приложении при необходимости. | accepted |

## 9. Assumptions

- API contract is project-specific evidence, not generic web theory.

## 10. Coverage

| Что нужно показать | Чем покрываем | Статус |
|---|---|---|
| Contract links frontend/backend | flow diagram | planned |
| Generated artifacts exist | repo evidence | planned |
| Validation errors are standardized | ProblemDetails explanation | planned |

## 11. Риски

| Риск | Как избежать |
|---|---|
| Уйти в общую теорию REST | Писать через конкретный OpenAPI flow проекта |

## 12. Заготовки удачных формулировок

- API-контракт используется как техническая граница между серверной и клиентской частями: сервер формирует OpenAPI-описание, а клиентская часть использует сгенерированные типы и обёртки для обращения к API.
