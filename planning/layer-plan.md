# Layer Plan

## Назначение

Проект развивается по слоям. Каждый следующий слой должен расширять систему, а не требовать переписывания предыдущего.

## L0 — подготовка проекта

Не является пользовательским функционалом.

### Цель

Привести проект к состоянию, в котором можно безопасно реализовывать L1.

### Входит

- cleanup generated/template artifacts;
- организация planning;
- фиксация слоев;
- фиксация доменной модели;
- фиксация API;
- фиксация тестовой стратегии;
- удаление/архивация устаревших планов;
- подготовка repository handoff для AI-агентов.

## L1 — обязательный минимум

### Цель

Получить минимально полноценное приложение для защиты диплома.

### Входит

#### Клиент

- регистрация;
- авторизация;
- просмотр базового профиля;
- создание заявителя типа ФЛ;
- подача заявки;
- просмотр своих заявок;
- просмотр статуса и результата обработки.

#### Сотрудник

- авторизация;
- просмотр всех заявок;
- просмотр необработанных заявок;
- просмотр обработанных заявок;
- открытие карточки заявки;
- взятие заявки в обработку;
- ручная проверка заявки;
- одобрение заявки;
- отклонение заявки;
- ввод комментария / причины отказа.

#### Система

- сохранение заявки в БД;
- смена статусов;
- создание результата рассмотрения;
- создание простого проекта договора при одобрении;
- отправка email-уведомления клиенту.

### Типы заявок L1

- `Connection` — технологическое присоединение;
- `MeteringDevice` — установка, замена или эксплуатация приборов учета.

### Статусы заявки L1

- `Submitted`;
- `InReview`;
- `Approved`;
- `Rejected`;
- `ContractDraftSent`.

### Не входит в L1

- ИП/ЮЛ;
- документы;
- PDF;
- mock-проверки;
- восстановление пароля;
- подтверждение email;
- подтверждение телефона;
- rate limiting;
- Windows auth;
- анонимные заявки;
- SMS;
- личные сообщения.

## L2 — хороший диплом

### Цель

Сделать систему похожей на полноценную систему электронного документооборота.

### Добавляется

- расширенный профиль заявителя;
- поддержка ФЛ / ИП / ЮЛ;
- snapshot данных заявителя в заявке;
- прикрепление документов к заявке;
- типы документов;
- история изменений заявки;
- комментарии;
- статус `NeedClarification`;
- запрос уточняющих данных;
- расширенный поиск и фильтрация заявок;
- mock-сервис межведомственной проверки данных;
- шаблоны обратной связи;
- шаблоны договоров;
- версионирование проектов договоров;
- генерация PDF договора;
- подтверждение email;
- восстановление пароля;
- обработка ошибок через ProblemDetails.

## L3 — production-like расширение

### Цель

Приблизить проект к промышленной эксплуатации.

### Добавляется

- анонимная заявка;
- Windows-аутентификация сотрудников через Negotiate;
- rate limiting;
- временная блокировка входа по email + IP;
- учет попыток входа;
- email о подозрительной активности;
- outbox для надежной отправки уведомлений;
- повторные попытки отправки email;
- аудит действий пользователей;
- security events;
- кэширование справочников;
- health checks;
- production-настройки;
- Docker / docker-compose;
- SMS-уведомления;
- личные сообщения в системе.

## За пределами реализации

- полноценная электронная подпись;
- юридически значимый ЭДО;
- реальная СМЭВ-интеграция;
- реальная проверка паспортов / ФНС;
- платежный контур;
- полноценное подписание договора онлайн.

Эти элементы можно описывать только как перспективы развития.

## Current Migration Context

The project is moving from the old domain/application model to the new L1 model gradually.

At the current stage the old implementation may continue to exist, but new L1 use cases must be built as a separate flow:

- old domain/application flow remains until replaced;
- new L1 domain/application/persistence flow is added in parallel.

Do not mix old domain classes and L1 domain classes inside the same command handler.

For L1, create separate commands, handlers, repositories, DbContext mapping and tests. Do not partially use new L1 entities inside an old handler.

Allowed temporary coexistence:

```text
Old controllers/commands/handlers/domain/persistence
L1 controllers/commands/handlers/domain/persistence
```

Forbidden hybrid:

```text
old command handler + L1 domain entity + old repository + old DbContext navigation mapping
```

This avoids creating an implicit third model during migration.
