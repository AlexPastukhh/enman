# Clean VKR Materials

Status: draft / section-drafting workflow synchronized  
Scope: clean materials for VKR text, defense materials, section drafts and diagram planning

## Тема ВКР

«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“».

## Назначение папки

Папка `vkr-clean/` содержит материалы, которые можно разворачивать в текст ВКР:

```text
- структура ВКР;
- терминология;
- анализ предметной области;
- требования и функциональная спецификация;
- пользовательские сценарии и Use Case-диаграммы;
- архитектура web-приложения;
- API-контракт между backend и frontend;
- доменная модель;
- база данных;
- пользовательский интерфейс;
- тестирование;
- результаты и дальнейшее развитие;
- планы диаграмм и приложений;
- workflow подготовки subsection drafts и reviewer feedback.
```

## Граница между planning и clean VKR

Рабочие planning-файлы используются как источники, но не переносятся в диплом напрямую.

В `vkr-clean/` нужно писать:

```text
- академично;
- конкретно по ООО «ЗСК»;
- без внутренних слов вроде agent, prompt, chat в итоговом тексте ВКР;
- без длинных технических трекеров в основном тексте;
- с аккуратными TODO там, где раздел зависит от будущей реализации, источника, визуального материала или repo-check.
```

## Статусы реализации

Подробные статусы реализации фиксируются в служебных файлах:

```text
planning/vkr-work-context-current.md
vkr-clean/evidence-map.md
```

В чистовых файлах допустимы краткие пояснения и placeholders, если текст зависит от будущей проверки кода.

## Главная линия ВКР

```text
клиент регистрируется
-> входит в систему
-> создает заявителя-физическое лицо
-> подает заявку
-> сотрудник рассматривает заявку
-> заявка одобряется или отклоняется
-> после одобрения подготавливается проект договора/документа
-> клиент получает уведомление и продолжает документооборот
```

## Текущий акцент clean-документов

С учетом актуального planning, clean-документы должны отражать:

```text
- сценарную спецификацию поведения;
- разделение backend/frontend;
- API-контракт через OpenAPI;
- генерируемые семантические константы;
- typed client API wrappers;
- ProblemDetails / form error mapping;
- тестирование по слоям;
- диаграммы и визуальные материалы;
- reviewer workflow для проверки subsection drafts.
```

## Section Draft Workflow

Для подготовки подразделов ВКР используется отдельная зона:

```text
vkr-clean/section-drafts/
```

Она отвечает за:

```text
- short drafts, которые сначала обсуждаются в чате и обычно не сохраняются как файлы;
- full draft attempts, которые сохраняются как файлы и могут иметь версии v1/v2/v3;
- reviewer workflow для content / structure / style-originality review;
- fragment bank для удачных формулировок;
- section draft register для статусов подразделов.
```

Начинать работу с подразделом нужно с:

```text
vkr-clean/vkr-materials-index.md
vkr-clean/section-drafts/README.md
vkr-clean/section-drafts/vkr-section-drafting-workflow.md
vkr-clean/section-drafts/reviewer-workflow.md
```
