# Algorithm: Repo Check Before Implementation Text

Когда нужно писать про реализацию, особенно главу 3:

```text
1. Не писать по памяти.
2. Проверить код / tests / screenshots / demo.
3. Проверить актуальные slice drafts.
4. Проверить scenarios, DATA, domain drafts, ADR/questions/decisions.
5. Отделить implemented / designed / planned / deferred / mock.
6. Не писать “реализовано” без evidence.
7. Сохранить findings в clean evidence/chapter-3 файлы, если работа системная.
```

Особые guardrails:

```text
- account activation не делать центральным реализованным flow без проверки;
- mock-проверка — demonstration/extension point;
- договор не генерируется автоматически;
- договорный этап начинает сотрудник;
- AgreementDocumentRef — metadata/reference, not full legal EDMS.
```
