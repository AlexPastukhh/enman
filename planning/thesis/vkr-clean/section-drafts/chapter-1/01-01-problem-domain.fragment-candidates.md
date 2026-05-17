# Fragment Candidates — 1.1 Problem Domain

Status: fragment candidates from full draft v1  
Target: Chapter 1 / Section 1.1  
Purpose: preserve successful fragments before future rewrites

## FR-CH1-001 — Scope of subject domain

Fragment type: opening / scope clarification  
Status: candidate  
Risk: low  
Needs: no external source required unless final text introduces general definition

> Предметная область работы не ограничивается простой регистрацией заявки. Клиентская заявка является входной точкой процесса, однако дальнейшая обработка включает несколько связанных этапов: идентификацию клиента, указание заявителя, регистрацию данных заявки, проверку сотрудником, принятие решения, подготовку договора или связанного документа и информирование клиента о результате.

Why keep: хорошо исправляет риск сужения темы только до заявки и связывает заявку, документы и уведомления.

## FR-CH1-002 — Client and applicant distinction

Fragment type: project-specific explanation  
Status: candidate  
Risk: low  
Needs: check terminology against current domain model before final chapter

> Для корректного описания процесса важно отделить клиента от заявителя. Клиент взаимодействует с системой через учётную запись: регистрируется, входит в приложение, создаёт или выбирает данные заявителя и подаёт заявку. Заявитель отражает лицо, от имени которого оформляется обращение.

Why keep: хороший проектный фрагмент, показывает не абстрактную теорию, а нашу модель.

## FR-CH1-003 — Request as connecting object

Fragment type: key thesis  
Status: reusable candidate  
Risk: low

> Заявка выступает связующим объектом между клиентской частью процесса и внутренней работой организации. Она содержит сведения, необходимые для рассмотрения: связь с заявителем, описание обращения, адрес или иной объект обслуживания, текущий статус и результат обработки.

Why keep: можно использовать в 1.1, 2.1 или при описании доменной модели.

## FR-CH1-004 — Document is not isolated

Fragment type: document flow explanation  
Status: candidate  
Risk: medium  
Needs: avoid overclaiming implementation; keep as target/process statement unless repo confirms

> Документ не должен существовать изолированно от исходной заявки: его смысл определяется тем, какое обращение рассматривалось, кто был заявителем, какое решение было принято и какие данные должны быть перенесены в документ.

Why keep: хороший фрагмент для связки заявки и документооборота.

## FR-CH1-005 — Email as feedback automation

Fragment type: communication/automation thesis  
Status: candidate  
Risk: medium  
Needs: repo check before implementation chapter; okay as subject-domain need

> Email-уведомление в проектируемом процессе рассматривается не как второстепенная функция, а как элемент автоматизации коммуникации между организацией и клиентом.

Why keep: удачно объясняет, почему email относится к автоматизации процесса, а не к случайной функции.

## FR-CH1-006 — Manual process problems

Fragment type: problem statement  
Status: reusable candidate  
Risk: low-to-medium  
Needs: can be strengthened by external source or example

> При ручной или разрозненной обработке заявок возникают несколько типовых проблем. Данные клиента, заявителя и заявки могут храниться отдельно друг от друга; статус обработки может быть неочевиден; результат проверки и связанный документ могут быть подготовлены без явной связи с исходной заявкой; информирование клиента может выполняться вручную и не иметь единого журнала.

Why keep: хорошее обобщение проблем, можно использовать в актуальности или анализе предметной области.

## FR-CH1-007 — Closing transition

Fragment type: transition to existing solutions analysis  
Status: candidate  
Risk: low

> Именно необходимость связать эти элементы в единую систему является основанием для дальнейшего анализа существующих решений и проектирования собственного web-приложения для ООО «ЗСК».

Why keep: хороший переход от 1.1 к 1.2.
