# Existing Solutions Analysis

Status: draft  
Scope: preliminary material for Chapter 1 / Chapter 2

This file contains a working analysis of alternatives. It is not a final literature-backed section yet. Before final submission, product descriptions and citations must be checked against actual sources.

## 1. Purpose Of This Section

The purpose of analyzing existing solutions is to justify why a custom web application is reasonable for the ООО «ЗСК» VKR topic.

For this project, the comparison should focus on:

```text
client requests
document flow
employee review
request status tracking
document/agreement preparation
email or system notifications
role-based access
```

## 2. Baseline Alternative: Email, Phone, Excel And Manual Documents

The simplest alternative is to process client requests through email, phone calls, office documents and spreadsheets.

### Advantages

```text
- no need to develop a separate system;
- employees can start using familiar tools immediately;
- low initial technical cost.
```

### Disadvantages

```text
- requests can be lost or duplicated;
- status tracking is manual;
- it is difficult to connect applicant data, request data and documents;
- access rights are hard to enforce;
- it is difficult to analyze request history;
- client does not have a transparent personal account;
- document preparation depends on manual control.
```

### Conclusion

This option can work for a small number of requests, but it does not solve the problem of controlled request lifecycle and document-flow traceability.

## 3. Universal CRM / Collaboration Systems

Examples of this class:

```text
Bitrix24
other CRM / collaboration platforms
```

### Advantages

```text
- ready user management and task tracking;
- configurable pipelines;
- notifications and comments;
- quick start compared to custom development.
```

### Disadvantages

```text
- business process must be adapted to the platform;
- domain-specific applicant/request/document rules may be hard to express cleanly;
- integration and customization may require additional configuration or paid modules;
- data model can be excessive for a focused educational/custom application;
- generated contract/API and custom domain model are not under full project control.
```

### Relevance For ООО «ЗСК»

Universal CRM tools may help organize communications but do not directly model the target process:

```text
client account -> applicant party -> client request -> employee decision -> document draft -> notification.
```

## 4. Enterprise Document Flow Systems

Examples of this class:

```text
1C:Документооборот
Directum
ELMA365
other ECM/BPM systems
```

### Advantages

```text
- strong document management capabilities;
- approval routes;
- role-based permissions;
- integration potential with enterprise infrastructure;
- support for document lifecycle.
```

### Disadvantages

```text
- high complexity for a focused VKR-scale project;
- implementation and customization may require significant administrative setup;
- user scenarios can become too heavy for simple request submission;
- custom frontend/backend architecture is not the main result if an off-the-shelf platform is used;
- educational goal of developing a web application would be weakened.
```

### Relevance For ООО «ЗСК»

Enterprise document flow systems are strong when the main task is organization-wide document routing. In this VKR, the central task is different: to design and implement a web application around client request processing and its connection to future document preparation.

## 5. Specialized Request / Helpdesk Systems

Examples of this class:

```text
helpdesk systems;
technical support ticket systems;
request-tracking systems.
```

### Advantages

```text
- ready ticket lifecycle;
- status tracking;
- comments;
- employee assignment;
- dashboards and filtering.
```

### Disadvantages

```text
- ticket model may not match applicant/request/document domain exactly;
- document draft creation after approval is not necessarily part of the core model;
- custom applicant-party validation and network-company-specific request data may require adaptation;
- integration with custom educational codebase is not the goal of using a finished product.
```

### Relevance For ООО «ЗСК»

This class is the closest conceptual alternative because it models requests and statuses. However, the VKR requires a custom system where the domain model and implementation are designed around the target process.

## 6. Custom Web Application

The selected solution is a custom client-server web application.

### Advantages

```text
- domain model can directly represent client account, applicant party, connection request and review decision;
- frontend can be designed around the required user scenarios;
- backend API can enforce request/applicant ownership and validation rules;
- database structure can be kept focused and understandable;
- OpenAPI and generated constants can be used to synchronize frontend/backend contracts;
- testing can be organized by layers: domain, API/integration, client and E2E;
- project result directly matches the VKR topic: development of a web application.
```

### Disadvantages

```text
- more development effort compared to using a ready platform;
- requires implementation of authentication, UI, API, data storage and testing;
- requires maintenance of custom code;
- advanced document-flow features may need future development.
```

### Conclusion

A custom web application is justified because it provides a focused implementation of the target business process and allows the VKR to demonstrate analysis, design, implementation and testing of a full-stack system.

## 7. Comparison Table

| Alternative | Request tracking | Document-flow support | Custom domain model | Development value for VKR | Main limitation |
|---|---:|---:|---:|---:|---|
| Email/Excel/manual documents | low | low | low | low | no controlled lifecycle |
| Universal CRM | medium | medium | medium | medium | process adapts to platform |
| Enterprise document-flow system | medium | high | medium | medium | excessive complexity |
| Helpdesk/ticket system | high | low/medium | medium | medium | ticket model may not match documents |
| Custom web application | high | planned/extendable | high | high | requires implementation |

## 8. Text Fragment For Chapter 1

Анализ возможных вариантов решения показывает, что обработка заявок через электронную почту, телефонные обращения и отдельные таблицы не обеспечивает достаточного контроля жизненного цикла заявки. При таком подходе сотрудник должен вручную отслеживать статус обращения, связь с заявителем и наличие связанных документов. Это увеличивает вероятность ошибок и усложняет последующую проверку действий.

Готовые CRM- и helpdesk-системы позволяют организовать учёт обращений, однако их модель не всегда соответствует предметной области сетевой компании. Для рассматриваемой работы важно явно связать клиентский аккаунт, заявителя, заявку, результат проверки и последующее формирование документа. Использование готовой платформы также снижает значимость разработки собственного web-приложения как результата ВКР.

Системы электронного документооборота обладают развитой поддержкой документов и маршрутов согласования, но для базового сценария подачи и рассмотрения клиентской заявки могут оказаться избыточными. В рамках данной работы требуется более компактное программное решение, ориентированное на конкретный процесс и позволяющее постепенно расширять функциональность.

Поэтому в качестве основного варианта выбран подход разработки собственного web-приложения с клиент-серверной архитектурой. Такой вариант позволяет спроектировать предметную модель под процесс ООО «ЗСК», реализовать необходимые пользовательские сценарии и подготовить основу для дальнейшего развития документооборота.

## 9. Sources To Add Before Final Text

Before finalizing this section, add checked sources for:

```text
- document management systems;
- CRM/helpdesk systems;
- web application architecture;
- requirements engineering / Use Case modeling;
- official documentation of selected technologies.
```

Do not cite unchecked product descriptions.
