# Use Case Diagrams Plan

Status: draft  
Scope: Use Case diagrams and explanatory text for VKR chapter 2

Use Case diagrams show who interacts with the system and what actions are available. They do not replace textual scenario specifications.

## 1. Diagram Set

```text
1. General system Use Case diagram.
2. Client Use Case diagram.
3. Employee Use Case diagram.
4. Document and notification Use Case diagram.
```

## 2. General Use Case Diagram

Actors:

```text
- Клиент
- Сотрудник
- Сервис email-уведомлений
```

Use cases:

```text
- Зарегистрироваться
- Войти в систему
- Выйти из системы
- Создать заявителя
- Подать заявку
- Просмотреть свои заявки
- Просмотреть детали заявки
- Просмотреть очередь заявок
- Рассмотреть заявку
- Одобрить заявку
- Отклонить заявку
- Подготовить проект договора/документа
- Отправить уведомление
```

Suggested PlantUML draft:

```plantuml
@startuml
left to right direction

actor "Клиент" as Client
actor "Сотрудник" as Employee
actor "Сервис email-уведомлений" as Email

rectangle "Web-приложение ООО «ЗСК»" {
  usecase "Зарегистрироваться" as UC_Register
  usecase "Войти в систему" as UC_Login
  usecase "Выйти из системы" as UC_Logout
  usecase "Создать заявителя" as UC_Applicant
  usecase "Подать заявку" as UC_Request
  usecase "Просмотреть свои заявки" as UC_MyRequests
  usecase "Просмотреть детали заявки" as UC_RequestDetails

  usecase "Просмотреть очередь заявок" as UC_Queue
  usecase "Рассмотреть заявку" as UC_Review
  usecase "Одобрить заявку" as UC_Approve
  usecase "Отклонить заявку" as UC_Reject
  usecase "Подготовить проект договора/документа" as UC_Document
  usecase "Отправить уведомление" as UC_Notify
}

Client --> UC_Register
Client --> UC_Login
Client --> UC_Logout
Client --> UC_Applicant
Client --> UC_Request
Client --> UC_MyRequests
Client --> UC_RequestDetails

Employee --> UC_Login
Employee --> UC_Logout
Employee --> UC_Queue
Employee --> UC_Review
UC_Review --> UC_Approve : <<extend>>
UC_Review --> UC_Reject : <<extend>>
UC_Approve --> UC_Document : <<extend>>

UC_Notify --> Email
UC_Approve --> UC_Notify : <<include>>
UC_Reject --> UC_Notify : <<include>>
UC_Document --> UC_Notify : <<include>>
@enduml
```

## 3. Explanatory Text Under The Diagram

```text
На диаграмме показаны основные акторы системы: клиент, сотрудник сетевой компании и сервис email-уведомлений. Клиент выполняет регистрацию, вход в систему, создание заявителя, подачу заявки и просмотр собственных заявок. Сотрудник работает с очередью заявок, рассматривает данные, принимает решение об одобрении или отклонении и при необходимости подготавливает проект договора или связанного документа. Сервис email-уведомлений используется для уведомления клиента о результате обработки или готовности документа.
```

## 4. Employee Diagram Note

Use the employee diagram to show that approval and document preparation are related but separate actions. This supports the architecture decision not to auto-create an agreement proposal during approval unless the final business rule explicitly requires it.
