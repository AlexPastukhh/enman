# Topic: Playwright screenshots

Status: visual-planned

## 1. Зачем эта тема нужна

Скриншоты через Playwright нужны, чтобы быстро и воспроизводимо подготовить визуальные доказательства реализации для ПЗ, отчёта ПП и презентации.

## 2. Куда входит

ПЗ:
- 3.3 Реализация frontend
- 3.6 Проверка пользовательских сценариев
- приложения со скриншотами

Отчёт по ПП:
- демонстрация выполненной разработки

Презентация:
- слайды UI и демонстрации результата

Доклад:
- тезис о проверяемом пользовательском сценарии.

## 3. Что нужно добиться

Преподаватель должен понять:
- реализованы не только backend/API, но и пользовательские экраны;
- есть связный клиентский и сотруднический поток;
- скриншоты воспроизводимы и могут быть обновлены после изменений.

Нужно обязательно раскрыть:
- какие экраны снимаются;
- зачем каждый скрин нужен;
- где скрин используется.

Нужно показать:
- минимум 8-12 экранов.

## 4. План раскрытия темы

Flow:
1. Выбрать сценарии для демонстрации.
2. Настроить стабильные test data / state.
3. Снять screenshots через Playwright.
4. Подписать каждый screenshot по смыслу.
5. Использовать часть в ПЗ/ПП, остальные в приложении.

## 5. Запланированная реализация раскрытия

Берём:
- current UI pages;
- Playwright config/tests;
- screenshot plan.

Подаём так:
- скриншот должен подтверждать конкретный тезис;
- не вставлять картинки без пояснения.

Обосновываем так:
- screenshots show implemented user-facing result.

Вставляем визуалы:
- registration/login;
- applicant/account;
- create request;
- my requests list/details;
- employee dashboard/details;
- agreement exchange list/details.

Ссылки/источники:
- не нужны, это проектные артефакты.

Repo-check:
- verify routes and current UI state before screenshot capture.

## 6. Материалы для harvest

Planning docs:
- playwright-screenshot-plan;
- preddiploma screenshot plan.

Repo evidence:
- Playwright tests;
- client routes;
- UI pages.

Research:
- not needed.

Other chats:
- messages about using screenshots for personalization.

## 7. Визуалы

Main:
- 4-6 screenshots in chapter 3.

Optional:
- 8-12 screenshots in appendix.

Appendix:
- full scenario screenshot set.

## 8. Вопросы и решения

| Вопрос | Текущий ответ | Статус |
|---|---|---|
| Использовать ручные скрины или Playwright? | Playwright preferred for reproducibility. | accepted |
| Вставлять все скрины в основной текст? | Нет, основной текст — ключевые, полный набор — приложение. | accepted |

## 9. Assumptions

- Screenshot set will be regenerated as UI changes.

## 10. Coverage

| Что нужно показать | Чем покрываем | Статус |
|---|---|---|
| Клиентский поток | screenshots 01-06 | planned |
| Сотруднический поток | screenshots 07-09 | planned |
| Договорный обмен | screenshots 10-12 | planned |

## 11. Риски

| Риск | Как избежать |
|---|---|
| Скрины устареют | Generate via Playwright near submission |
| Скрины будут декором | Каждому дать подпись и пояснение |

## 12. Заготовки удачных формулировок

- На рисунке показан экран, подтверждающий реализацию сценария: пользователь может перейти от данных заявителя к созданию заявки и дальнейшему просмотру результата.
