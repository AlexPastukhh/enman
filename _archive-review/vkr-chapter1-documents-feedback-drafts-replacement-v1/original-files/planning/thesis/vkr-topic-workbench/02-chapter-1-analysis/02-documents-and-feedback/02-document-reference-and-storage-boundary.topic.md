# Topic: Document reference и граница файлового хранилища

Status: material-harvest-needed

## 1. Зачем эта тема нужна

This topic supports Chapter 1 point 1.2 and prevents the VKR from looking like a simple request tracker. It keeps the document/contract line visible.

## 2. Куда входит

ПЗ:
- 1.2 Договорно-документный этап и обратная связь с клиентом.

## 3. Что нужно добиться

Преподаватель должен понять:
- положительное решение по заявке запускает договорно-документный этап;
- документ должен сохранять связь с заявкой;
- client feedback/notification is part of the automated process;
- physical file storage is not the same as document metadata reference.

## 4. План раскрытия темы

Flow:
1. Start from request decision.
2. Show why a document/contract stage appears.
3. Explain client feedback.
4. Clarify implementation boundary if document reference is discussed.

## 5. Запланированная реализация раскрытия

Use domain/project language. Do not describe controllers/endpoints here.

## 6. Материалы для harvest

Planning docs:
- agreement exchange docs;
- clean-results-and-future-work;
- document storage wording note.

Repo evidence:
- AgreementProposalExchange;
- AgreementProposal;
- AgreementDocumentRef.

## 7. Визуалы

Main:
- request → decision → document stage.

Optional:
- implemented vs future document storage boundary.

## 8. Вопросы и решения

| Вопрос | Текущий ответ | Статус |
|---|---|---|
| Можно ли писать “полноценное документохранилище”? | Нет. Писать про ссылку на документ и метаданные. | accepted |

## 9. Assumptions

- Email notifications are planned/needs repo-check unless implementation is confirmed.

## 10. Coverage

| Что нужно показать | Чем покрываем | Статус |
|---|---|---|
| Document stage is part of the process | text + diagram | planned |
| Document reference boundary | text + table | planned |

## 11. Риски

| Риск | Как избежать |
|---|---|
| Overclaiming storage | Use “document reference and metadata”, not “full storage”. |

## 12. Заготовки удачных формулировок

Договорно-документный этап рассматривается как продолжение положительного решения по заявке, а не как отдельная несвязанная операция.
