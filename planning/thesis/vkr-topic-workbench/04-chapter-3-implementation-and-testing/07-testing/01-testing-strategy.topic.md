# Topic: Стратегия тестирования

Status: material-harvest-needed

## 1. Зачем эта тема нужна

Тестирование показывает, что разработка не ограничилась описанием экранов. Нужно связать сценарии, доменные правила, API и пользовательские проверки.

## 2. Куда входит

ПЗ:
- 3.6 Проверка пользовательских сценариев
- 3.7 Тестирование доменной логики и API

Отчёт по ПП:
- тестирование и проверка

Презентация:
- слайд “Тестирование”

Доклад:
- тезис о проверке основных сценариев и доменных правил.

## 3. Что нужно добиться

Преподаватель должен понять:
- какие уровни тестирования используются;
- какие сценарии проверяются;
- почему домен тестируется отдельно;
- как API/integration tests и E2E дополняют друг друга.

Нужно обязательно раскрыть:
- domain tests;
- API/integration tests;
- frontend/component tests;
- Playwright E2E;
- contract checks if used.

Нужно показать:
- матрицу тестирования.

## 4. План раскрытия темы

Flow:
1. Связать тестирование со сценариями.
2. Показать уровни: domain → API/integration → frontend/E2E.
3. Объяснить, что проверяется на каждом уровне.
4. Привести таблицу сценарий → тип проверки.
5. Сделать вывод о достаточности проверки для предзащитного среза.

## 5. Запланированная реализация раскрытия

Берём:
- clean-testing;
- tests in repo;
- scenario specs;
- Playwright tests.

Подаём так:
- не перечислять файлы тестов без смысла;
- связать каждый уровень с поведением.

Обосновываем так:
- разные уровни тестов покрывают разные риски.

Вставляем визуалы:
- testing matrix;
- maybe small testing pyramid.

Ссылки/источники:
- testing references if needed.

Repo-check:
- verify current tests before final claims.

## 6. Материалы для harvest

Planning docs:
- clean-testing;
- test-related slice docs.

Repo evidence:
- domain tests;
- integration tests;
- frontend tests;
- Playwright tests.

Research:
- software testing references.

Other chats:
- author comments about testing and vertical slices.

## 7. Визуалы

Main:
- testing matrix.

Optional:
- testing pyramid.

Appendix:
- detailed test list.

## 8. Вопросы и решения

| Вопрос | Текущий ответ | Статус |
|---|---|---|
| Нужно ли связывать тесты со срезами? | Да, это персонализирует раздел и показывает инженерную логику. | accepted |

## 9. Assumptions

- Test claims require repo-check before final submission.

## 10. Coverage

| Что нужно показать | Чем покрываем | Статус |
|---|---|---|
| Domain rules are tested | domain tests evidence | planned |
| API scenarios are tested | integration tests evidence | planned |
| User flows can be demonstrated | Playwright/screenshots | planned |

## 11. Риски

| Риск | Как избежать |
|---|---|
| Просто перечислить тесты | Use scenario-to-test matrix |
| Overclaiming coverage | Check repo before final text |

## 12. Заготовки удачных формулировок

- Тестирование организовано вокруг тех же сценариев, которые использовались при проектировании: доменные правила проверяются отдельно, API-сценарии — интеграционными тестами, а пользовательский путь может быть подтверждён через E2E-проверки и скриншоты.
