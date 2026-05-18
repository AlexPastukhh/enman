# Topic: Document reference вместо полноценного файлового хранилища

Status: material-harvest-needed / wording-critical

## 1. Зачем эта тема нужна

Эта тема нужна, чтобы корректно описать документную часть проекта и не завысить статус реализации. В системе реализован договорно-документный обмен, где версии предложений содержат ссылку на документ и его метаданные. Это не равно полноценному бинарному файловому хранилищу.

## 2. Куда входит

ПЗ:
- 1.2 Документы и договоры в процессе обработки заявки
- 2.3 Жизненный цикл документа и договора
- 2.5 Доменная модель
- 2.6 Структура базы данных
- 3.4 Реализация хранения данных
- 3.8 Ограничения и дальнейшее развитие

Отчёт по ПП:
- раздел о разработке договорно-документного обмена

Презентация:
- слайд “Договорно-документный обмен”

Доклад:
- осторожный тезис о хранении ссылок и метаданных документа.

## 3. Что нужно добиться

Преподаватель должен понять:
- договорная линия в проекте есть;
- документ представлен через ссылку и метаданные;
- физическое хранение файла и загрузка/скачивание выделены как дальнейшее развитие.

Нужно обязательно раскрыть:
- AgreementDocumentRef;
- StorageKey;
- OriginalFileName;
- ContentType;
- SizeBytes;
- связь с AgreementProposal.

Нужно показать:
- диаграмму связи proposal → document reference;
- таблицу “что реализовано / что отложено”.

## 4. План раскрытия темы

Flow:
1. Объяснить, почему после одобрения заявки нужен договорный документ.
2. Показать, что в текущем срезе документ представлен ссылкой и метаданными.
3. Связать документную ссылку с версией договорного предложения.
4. Отдельно назвать deferred части: upload/download/storage adapter/secure serving.
5. Сделать вывод о корректном статусе реализации.

## 5. Запланированная реализация раскрытия

Берём:
- agreement exchange planning docs;
- repo/domain classes around AgreementDocumentRef;
- OpenAPI/API DTOs;
- UI screens for agreement exchange details if available.

Подаём так:
- не использовать термин “полноценное документохранилище”;
- использовать “ссылка на документ и метаданные”;
- показать как часть договорно-документного обмена.

Обосновываем так:
- для среза ВКР важно сохранить связь между заявкой, договорным предложением и документной ссылкой;
- физическое хранение файлов является самостоятельной инфраструктурной задачей.

Вставляем визуалы:
- схема AgreementProposal → AgreementDocumentRef;
- таблица implemented/future.

Ссылки/источники:
- при необходимости источник по электронному документообороту / хранению электронных документов.

Repo-check:
- проверить фактические поля AgreementDocumentRef перед финальным текстом.

## 6. Материалы для harvest

Planning docs:
- agreement exchange docs;
- clean-domain-model;
- clean-database-design;
- clean-results-and-future-work.

Repo evidence:
- AgreementDocumentRef;
- AgreementProposal;
- AgreementProposalExchange;
- agreement exchange API/UI.

Research:
- справочно: электронный документооборот.

Other chats:
- user messages about not overclaiming document storage.

## 7. Визуалы

Main:
- AgreementProposal → AgreementDocumentRef.

Optional:
- lifecycle of agreement document proposal.

Appendix:
- screenshots of agreement exchange details with document reference.

## 8. Вопросы и решения

| Вопрос | Текущий ответ | Статус |
|---|---|---|
| Можно ли писать “реализовано документохранилище”? | Нет. Писать “реализована ссылка на документ и метаданные в рамках договорно-документного обмена”. | accepted |
| Что относится к future work? | binary upload/download, storage adapter, secure file serving. | accepted |

## 9. Assumptions

- AgreementDocumentRef is metadata reference, not file bytes.
- Full file storage is a later infrastructure slice.

## 10. Coverage

| Что нужно показать | Чем покрываем | Статус |
|---|---|---|
| Документная линия реализована не только в тексте | domain/API/UI evidence | planned |
| Нет overclaiming файлового хранилища | wording + limitations table | planned |
| Связь документа с договорным предложением | diagram | planned |

## 11. Риски

| Риск | Как избежать |
|---|---|
| Назвать это полноценным хранилищем | Использовать термин document reference / ссылка и метаданные |
| Потерять значимость документной линии | Связать с заявкой, решением и agreement exchange |

## 12. Заготовки удачных формулировок

- В текущем срезе система хранит не файл как бинарный объект, а ссылку на документ и его метаданные, связанные с версией договорного предложения.
