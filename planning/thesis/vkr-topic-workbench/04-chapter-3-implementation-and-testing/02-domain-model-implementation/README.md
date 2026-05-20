# 02-domain-model-implementation

Status: active / Chapter 3 section 3.2 domain model implementation workbench.

This folder stores topic drafts for section **3.2 Реализация модели предметной области**.

The purpose of this section is to explain how implemented domain objects support the application scenarios:
client account, applicant, connection request, employee review, agreement exchange, proposal versions,
document references, domain errors and domain-level tests.

## Important rule

The final VKR text must not use internal planning terms such as:

```text
L1 / L2
topic draft
source-pass
workflow
slice draft
legacy
```

These terms may be used internally in the topic drafts, but the final text must translate them into thesis-facing language:

```text
модель предметной области
сценарий работы системы
функциональный срез
серверная операция
прикладный обработчик
проверка правил модели
```

## Source-pass order for each 3.2 topic

```text
1. Scenario text specification
2. Behavior items / scenario data
3. Domain draft
4. Slice draft
5. Repo/code
```

## Topic files

```text
03-02-01-domain-project-purpose-and-scenario-role.topic.md
03-02-02-process-participants-account-applicant-employee.topic.md
03-02-03-connection-request-lifecycle.topic.md
03-02-04-employee-request-review.topic.md
03-02-05-agreement-exchange.topic.md
03-02-06-agreement-proposal-versions-and-documents.topic.md
03-02-07-domain-results-errors-and-impossible-states.topic.md
03-02-08-domain-rules-verification.topic.md
```

## Target VKR section structure

```text
3.2 Реализация модели предметной области

3.2.1 Назначение доменного проекта и его место в реализации сценариев
3.2.2 Реализация участников процесса: аккаунт, заявитель и сотрудник
3.2.3 Реализация заявки и её жизненного цикла
3.2.4 Реализация рассмотрения заявки сотрудником
3.2.5 Реализация договорного обмена
3.2.6 Реализация версий договорных предложений и документов
3.2.7 Доменные ограничения, результаты операций и невозможные состояния
3.2.8 Проверка правил предметной модели
```

## Final text rule

Use the `Clean text candidate` sections as candidate VKR text, but run a final repo-check before inserting the text into the thesis.
