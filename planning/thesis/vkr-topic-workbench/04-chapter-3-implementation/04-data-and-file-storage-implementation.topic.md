# Тема: Реализация хранения данных и файлов

**Статус:** быстрый topic-драфт v1 / глава 3 / нужен repo-check по БД, entities, migrations, metadata, file storage service, upload/download, access check, storage path/key, tests.

1. Зачем тема нужна

Эта тема показывает, как в реализации устроено хранение структурированных данных, metadata документов и физических файлов.

Главная мысль:

Реализация хранения разделяет состояние бизнес-процесса и физические файлы. БД хранит заявки, рассмотрения, решения, договорный обмен, версии предложений и metadata документов. Physical files хранятся отдельно, а backend связывает их с процессом через document metadata и контролирует доступ при upload/download.

2. Что нужно доказать

Для допуска нужно быстро показать:

1. Есть БД / persistence layer.
2. Есть сущности заявки, review, decision, exchange, proposal version.
3. Есть metadata документа.
4. Файл хранится отдельно от БД.
5. Metadata связывает proposal version и physical file.
6. Backend не отдаёт файл по прямому public path.
7. Download идёт через metadata и access check.
8. Есть хотя бы manual/API/demo-проверка upload/download.
3. Реализация хранения structured data

В БД должны сохраняться:

- пользователи / аккаунты / роли;
- заявители;
- заявки;
- статусы заявок;
- рассмотрение;
- результат проверки, если сохраняется;
- решение;
- причина отказа;
- договорный exchange;
- версии договорного предложения;
- отправитель версии;
- metadata документа.

Рабочий текст:

Структурированные данные процесса сохраняются в базе данных. Это позволяет backend восстанавливать состояние заявки, определять текущий этап жизненного цикла, проверять доступ пользователя и отображать актуальные данные во frontend.

4. Реализация document metadata

Metadata нужна для связи версии предложения с физическим файлом.

Возможные поля:

documentId;
proposalVersionId;
originalFileName;
storageKey / storageFileName;
contentType, если есть;
sizeBytes, если есть;
uploadedAt;
uploadedBy, если есть.

Осторожно:

точные поля проверить по repo;
не писать checksum/hash, если его нет;
не писать antivirus scan, если его нет;
не писать audit trail, если его нет.

Рабочий текст:

В БД хранится не сам файл, а metadata-запись. Она связывает физический файл с конкретной версией договорного предложения и содержит сведения, необходимые backend для поиска файла и отображения его во frontend.

5. Реализация физического хранения файлов

Physical file хранится отдельно.

Для текущего scope:

- файл сохраняется в файловой директории;
- backend формирует внутреннее имя или storage key;
- originalFileName используется для отображения;
- storageKey/path используется только backend;
- frontend не получает physical path.

Рабочий текст:

Физическое содержимое документа сохраняется в файловом хранилище. В рамках текущей реализации это может быть простая директория. При этом frontend не работает с файловой системой напрямую, а получает файл только через backend API.

6. Upload flow в реализации

Flow:

frontend отправляет файл
→ backend принимает upload request
→ backend проверяет пользователя и доступ к exchange
→ application/domain проверяет допустимость новой версии
→ file storage сохраняет physical file
→ persistence сохраняет metadata
→ proposal version связывается с document metadata
→ frontend получает result DTO

Рабочий текст:

При загрузке файла backend выполняет не только сохранение binary content, но и связывает файл с договорным обменом. Успешный upload должен создавать и physical file, и metadata, связанную с версией предложения.

7. Download flow в реализации

Flow:

frontend запрашивает documentId
→ backend находит metadata
→ backend определяет proposal version
→ backend определяет exchange и request
→ backend проверяет доступ пользователя
→ backend читает physical file по storage key
→ backend возвращает file или safe error

Рабочий текст:

Получение файла реализуется через backend. Сервер сначала получает metadata и проверяет бизнес-контекст документа, а затем читает physical file из хранилища. Это предотвращает прямой доступ к договорным файлам по пути файловой системы.

8. Metadata/file consistency

Риски:

metadata есть, file отсутствует;
file есть, metadata отсутствует;
upload завершился частично;
metadata указывает на устаревший storage key.

Проектный ответ:

успешный upload только после сохранения file + metadata;
file без metadata не считается документом процесса;
metadata без file возвращает safe file error;
сложный cleanup orphan files можно указать как future work.

Рабочий текст:

Так как metadata и physical file находятся в разных хранилищах, backend должен контролировать их согласованность на уровне сценариев upload/download.

9. Access check для файлов

Доступ проверяется по цепочке:

document metadata
→ proposal version
→ agreement exchange
→ connection request
→ applicant/client account

Тезис:

Пользователь не получает файл только по знанию имени или пути. Backend должен проверить, что document metadata относится к заявке или договорному обмену, доступному текущему пользователю.

10. Что не надо overclaim

Не писать без repo-check:

- используется полноценная СЭД;
- есть ЭДО;
- есть электронная подпись;
- есть object storage;
- есть signed URLs;
- есть antivirus scan;
- есть checksum/hash;
- есть полный аудит скачиваний;
- есть архивная политика;
- есть distributed transaction для file + metadata.

Писать безопасно:

- текущая модель достаточна для scope;
- object storage и ЭП/ЭДО — направления развития;
- физический файл хранится отдельно;
- metadata связывает файл с процессом;
- backend контролирует доступ.
11. Таблица для ВКР
Объект	Где хранится	Назначение
Заявитель	БД	данные клиента для подачи заявки
Заявка	БД	центральный объект процесса
Review	БД	состояние рассмотрения
Decision	БД	результат одобрения/отклонения
Rejection reason	БД	причина отказа
Agreement exchange	БД	договорный этап после одобрения
Proposal version	БД	версия договорного предложения
Document metadata	БД	связь версии с файлом
OriginalFileName	БД metadata	отображение пользователю
StorageKey/path	БД metadata / backend internal	поиск physical file
Physical file	file storage	содержимое документа

Подпись:

Таблица 3.x — Реализация хранения данных и файлов
12. Demo / evidence

Что показать на преддипломной:

1. БД с заявками / review / exchange / proposal.
2. Metadata документа в БД.
3. Физический файл в директории.
4. UI показывает originalFileName.
5. Download идёт через backend.
6. Storage path/key не виден во frontend.
7. Upload создаёт новую version/history row.
8. Ошибка missing/forbidden file возвращается safe.
13. Repo-check
- какая БД используется;
- где connection string / database config;
- где persistence layer;
- какие entity classes есть;
- ConnectionRequest;
- RequestReview;
- AgreementProposalExchange;
- AgreementProposal / ProposalVersion;
- AgreementDocumentRef / DocumentMetadata;
- поля metadata;
- originalFileName;
- storageKey/storagePath;
- где physical files реально сохраняются;
- есть ли file storage service;
- upload endpoint/service;
- download endpoint/service;
- access check при download;
- раскрывается ли storage path/key frontend;
- есть ли tests upload/download;
- есть ли tests metadata/file consistency;
- есть ли tests forbidden download.
14. Черновой текст

В реализации структурированные данные процесса сохраняются в базе данных. К ним относятся заявители, заявки, состояние рассмотрения, решения, договорные обмены и версии договорных предложений. Такое хранение позволяет backend восстанавливать актуальное состояние процесса и проверять доступ пользователя.

Файлы договорных предложений хранятся отдельно от структурированных данных. Для связи файла с процессом используется metadata-запись, которая сохраняется в БД и связывает physical file с конкретной версией договорного предложения. Metadata может содержать исходное имя файла, внутренний ключ хранения и дату загрузки.

Загрузка файла реализуется как командный сценарий. Backend проверяет доступ пользователя к договорному обмену, сохраняет physical file, создаёт metadata и связывает её с версией предложения. Получение файла также выполняется через backend: сервер сначала находит metadata, проверяет доступ через договорный обмен и заявку, а затем возвращает physical file.

Такой подход подтверждает проектное решение главы 2: domain logic не зависит от файловой системы, frontend не получает storage path, а доступ к договорным документам контролируется backend-ом.

15. Итоговая формула
реализация хранения разделяет:
structured data,
document metadata
и physical file;

БД хранит состояние процесса:
заявки,
review,
decision,
exchange,
proposal versions
и metadata;

файловое хранилище хранит physical file;

metadata связывает proposal version с файлом;

upload создаёт physical file + metadata;

download начинается с metadata
и проходит через access check;

frontend не получает storage path/storage key;

backend контролирует доступ
и consistency metadata/file;

эта реализация подтверждает storage boundary,
спроектированную в главе 2.
