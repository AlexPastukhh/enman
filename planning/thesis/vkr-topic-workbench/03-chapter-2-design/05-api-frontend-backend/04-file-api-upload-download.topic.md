# Тема: API загрузки и получения файлов

**Статус:** быстрый topic-драфт v1 / блок 2.5.4 / закрывает файловую часть API / нужен repo-check по upload/download endpoints, document metadata, storage key, file access, error contract, static file access.

---

## 1. Зачем тема нужна

Эта тема объясняет, как через API выполняется загрузка и получение файлов договорных предложений.

Главная мысль:

> Файлы договорных предложений не должны передаваться как прямые public static files. Frontend загружает и запрашивает файл через backend API. Backend проверяет доступ, связывает файл с версией договорного предложения через metadata и только после этого сохраняет или возвращает physical file.

---

## 2. Связь с предыдущими темами

Из 2.4:

```text
proposal version → document metadata → physical file
```

Из 2.5.1–2.5.3:

```text
frontend работает через API;
upload — command API;
download — read-like file operation;
ошибки должны быть структурированными;
storage path/storage key не раскрываются frontend.
```

---

## 3. Upload API

Upload относится к command API, потому что создаёт новые данные:

```text
physical file;
document metadata;
proposal version или связь с proposal version;
запись в истории договорного обмена.
```

Общий flow:

```text
1. Пользователь выбирает файл во frontend.
2. Frontend отправляет файл через backend API.
3. Backend проверяет авторизацию.
4. Backend проверяет доступ к договорному обмену.
5. Application layer проверяет сценарий.
6. Domain layer проверяет допустимость новой версии.
7. Backend сохраняет physical file.
8. Backend создаёт metadata.
9. Metadata связывается с proposal version.
10. Frontend получает command result.
```

Рабочая формулировка:

> Загрузка файла договорного предложения является командным сценарием. Она не сводится к сохранению файла в директорию: backend должен проверить доступ, сохранить physical file, создать metadata и связать документ с версией договорного предложения.

---

## 4. Что frontend отправляет при upload

Frontend может отправлять:

```text
файл;
идентификатор договорного обмена;
комментарий, если есть;
дополнительные данные версии, если они есть в модели.
```

Frontend не должен отправлять как доверенные данные:

```text
storage path;
storage key;
sender role;
final status;
createdAt;
uploadedBy;
internal file name.
```

Тезис:

> Frontend передаёт файл и параметры пользовательского действия, но backend сам определяет storage key, отправителя, время загрузки и связь с текущим пользователем.

---

## 5. Metadata при upload

После успешной загрузки backend должен создать metadata.

Metadata может включать:

```text
documentId;
proposalVersionId;
originalFileName;
storageKey / storageFileName;
contentType, если сохраняется;
sizeBytes, если сохраняется;
uploadedAt;
uploadedBy, если сохраняется;
senderSide, если хранится на этом уровне.
```

Осторожно:

```text
точные поля проверить по repo;
не писать checksum/hash без подтверждения;
не писать antivirus scan без подтверждения;
не писать audit trail без подтверждения.
```

---

## 6. Command result после upload

Upload не обязан возвращать полную domain model.

Возможный result:

```json
{
  "proposalVersionId": 7,
  "documentId": 22,
  "originalFileName": "agreement.pdf"
}
```

После upload frontend может:

```text
обновить историю версий;
повторно запросить exchange details;
показать имя загруженного файла;
показать ошибку, если upload не удался.
```

Тезис:

> Результат upload должен быть достаточным для обновления интерфейса, но не должен раскрывать внутреннюю модель backend или storage path.

---

## 7. Download API

Download похож на read operation, но возвращает не DTO, а physical file.

Общий flow:

```text
1. Frontend запрашивает файл по documentId.
2. Backend проверяет авторизацию.
3. Backend находит document metadata.
4. Backend определяет proposal version.
5. Backend определяет exchange и request.
6. Backend проверяет доступ пользователя.
7. Backend читает physical file по storage key.
8. Backend возвращает файл или safe error.
```

Рабочая формулировка:

> Получение файла должно начинаться с metadata и проверки доступа. Backend сначала определяет бизнес-контекст документа, а затем получает physical file из файлового хранилища.

---

## 8. Почему download не должен идти по storage path

Плохо:

```text
Frontend → /uploads/agreement-files/file.pdf
```

Потому что:

```text
нет проверки доступа;
можно раскрыть чужой файл;
storage path становится публичным контрактом;
сложно скрыть факт существования документа;
невозможно нормально обработать 403/404.
```

Правильно:

```text
Frontend → Backend API → metadata → access check → file storage → file response
```

Тезис:

> Договорные файлы не должны раздаваться как public static files. Доступ к ним зависит от заявки, договорного обмена, версии предложения и текущего пользователя.

---

## 9. Access check при download

Доступ проверяется не по имени файла, а по цепочке:

```text
document metadata
→ proposal version
→ agreement exchange
→ connection request
→ applicant / client account
```

Для клиента:

```text
файл доступен, если он относится к его заявке.
```

Для сотрудника:

```text
файл доступен, если сотрудник имеет право работать с заявкой или договорным обменом.
```

Тезис:

> Доступ к файлу определяется предметными связями, а не знанием имени файла или storage key.

---

## 10. Ошибки upload

Возможные ошибки:

```text
файл не передан;
файл слишком большой, если есть ограничение;
тип файла недопустим, если есть ограничение;
договорный обмен не найден;
нет доступа к обмену;
нельзя отправить новую версию;
ошибка сохранения physical file;
ошибка создания metadata.
```

Пример safe error:

```json
{
  "errorCode": "UploadFailed",
  "message": "Не удалось загрузить файл."
}
```

---

## 11. Ошибки download

Возможные ошибки:

```text
document metadata не найдена;
файл не найден в storage;
нет доступа;
документ относится к чужой заявке;
storage key повреждён или устарел;
physical file недоступен.
```

Не возвращать frontend:

```text
storage path;
storage key;
server directory;
stack trace;
детали чужого документа.
```

Пример:

```json
{
  "errorCode": "FileUnavailable",
  "message": "Файл недоступен."
}
```

---

## 12. Metadata/file consistency

Upload должен создавать согласованную пару:

```text
metadata + physical file
```

Риски:

```text
file сохранён, metadata не создана;
metadata создана, file не сохранён;
metadata указывает на отсутствующий file;
file есть, но не связан с process metadata.
```

Проектный ответ:

```text
успешный upload возвращается только после создания file и metadata;
file без metadata не считается документом процесса;
metadata без file приводит к safe file error;
сложный cleanup orphan files можно оставить как future work.
```

Тезис:

> Так как metadata и physical file находятся в разных хранилищах, backend должен контролировать их согласованность на уровне upload/download сценария.

---

## 13. Таблица для ВКР

| Сценарий       | Что делает frontend         | Что делает backend                                 |
| -------------- | --------------------------- | -------------------------------------------------- |
| Upload         | выбирает и отправляет файл  | проверяет доступ, сохраняет file, создаёт metadata |
| Show file      | показывает originalFileName | отдаёт metadata в read DTO                         |
| Download       | запрашивает documentId      | проверяет metadata/access и возвращает file        |
| Upload error   | показывает сообщение        | возвращает structured error                        |
| Download error | показывает safe error       | не раскрывает storage path                         |
| Stale exchange | отправляет старое действие  | возвращает business-state error                    |

Подпись:

```text
Таблица 2.x — API-сценарии загрузки и получения файлов
```

---

## 14. Визуалы

```text
1. Рисунок — Upload flow:
   frontend → backend → application/domain → file storage + metadata DB.

2. Рисунок — Download flow:
   frontend → backend → metadata → access check → file storage.

3. Рисунок — Почему не public static files:
   bad direct URL vs good backend API.

4. Таблица — Upload/download errors.

5. Схема — metadata/file consistency.
```

---

## 15. Черновой текст

Файловые операции выделяются в отдельную часть API, потому что они связаны не только с передачей binary content, но и с бизнес-контекстом договорного обмена. Файл договорного предложения должен быть связан с конкретной версией предложения через metadata-запись.

Загрузка файла относится к command API. При upload frontend передаёт файл через API, а backend проверяет доступ к договорному обмену, сохраняет physical file, создаёт metadata и связывает документ с версией предложения. Frontend не должен передавать storage path или storage key: эти значения являются внутренними деталями backend.

Получение файла выполняется через отдельный download API. Backend сначала находит metadata, затем определяет связанную версию предложения, договорный обмен и заявку, проверяет доступ пользователя и только после этого возвращает physical file.

Договорные файлы не должны раздаваться как public static files, потому что прямой путь к файлу обходил бы проверку доступа. Кроме того, API не должен раскрывать frontend внутренние пути файловой системы, storage key или сведения о чужих документах.

---

## 16. Repo-check

```text
- есть ли upload endpoint;
- есть ли download endpoint;
- по какому id выполняется download;
- создаёт ли upload proposal version;
- создаёт ли upload metadata;
- какие поля metadata есть;
- хранится ли originalFileName;
- хранится ли storageKey/storagePath;
- раскрывается ли storageKey frontend;
- есть ли static file serving;
- проверяется ли access при download;
- что возвращается при missing metadata;
- что возвращается при missing physical file;
- что возвращается при forbidden file;
- есть ли ограничения размера/типа;
- есть ли tests upload;
- есть ли tests download;
- есть ли tests forbidden download.
```

---

## 17. Итоговая формула

```text
upload файла — это command scenario;

download файла — это read-like file scenario
с обязательной проверкой доступа;

frontend отправляет файл
и запрашивает documentId,
но не работает со storage path;

backend сохраняет physical file,
создаёт metadata
и связывает document с proposal version;

download начинается с metadata,
проходит через proposal/exchange/request access check
и только потом читает physical file;

договорные файлы не должны раздаваться как public static files;

ошибки upload/download должны быть structured и safe;

API не раскрывает storage key,
internal path,
server directory
и сведения о чужих документах.
```

Блок **2.5 закрыт быстрыми драфтами**. Следующий блок:

```text
planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/
└─ 01-ui-structure-and-navigation.topic.md
```
