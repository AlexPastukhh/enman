# Исследовательский отчёт по источникам для списка литературы и теоретической части ВКР

## Рамки отбора и принцип использования отчёта

Отчёт собран под тему ВКР: **«Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“»**. В отбор включались прежде всего официальные стандарты, спецификации, учебники и монографии, диссертации, обзорные публикации и официальная документация по стеку, если тема зависит от текущего состояния платформ и инструментов. Это особенно важно здесь, потому что на момент проверки официальный сайт OWASP уже указывает **Top Ten 2025** как текущий релиз, NIST перевёл цифровую идентификацию на финальную серию **SP 800‑63‑4** в 2025 году, а OpenAPI Initiative публикует релизы **OAS 3.1.1** и **OAS 3.2.0** как актуальные точки опоры для API-контрактов. citeturn32search7turn34search9turn34search3turn6search2turn6search14

В колонке **«Ссылка»** даны кликабельные ссылки в виде цитат. Колонка **«Статус»** нужна для практического отбора:  
**основной** — хороший кандидат в теоретическую часть и в итоговый список литературы;  
**нормативный** — стандарт, спецификация или отраслевой baseline, который хорошо подходит для разделов про требования, архитектуру, безопасность, API-контракт и правила взаимодействия;  
**справочный** — официальный продуктовый материал, tutorial или practitioner-источник, который лучше использовать для описания реализации, а не как каркас всей теоретической главы.

## Ключевые выводы по литературной базе

Для раздела о **документообороте и обработке заявок** наиболее сильная база получается из стандартов по records management и BPM-подхода: ISO 15489, ISO 30301/30302, BPMN 2.0.2 и классические работы по workflow/BPM. Это даёт не просто «описание автоматизации», а формальную основу для жизненного цикла документов, ролей, маршрутов согласования и описания сценариев прохождения заявки. citeturn0search0turn30search1turn30search19turn0search2turn2search17turn4search4

Для разделов про **клиент‑серверную архитектуру и web‑приложения** самая надёжная связка — Fielding, W3C Web Architecture, RFC 9110 и книги по enterprise/software architecture. Она позволяет корректно обосновать, почему приложение строится как разделённая система с HTTP‑взаимодействием, ресурсной моделью, уровнями ответственности и предсказуемыми архитектурными решениями. citeturn35search0turn3search2turn8search0turn2search1turn3search0

Для **технологического стека** — ASP.NET Core, React, TypeScript, EF Core, SQL Server — официальная документация Microsoft, React и TypeScript действительно ценна, но в ВКР её лучше использовать как **опору для точности реализации**, а не как замену книгам и стандартам. Это особенно верно для разделов о middleware, DI, Web API, конфигурации ORM, миграциях, индексах и транзакционной изоляции. citeturn11search17turn25search17turn11search1turn24search11turn24search9turn12search5turn12search18turn7search0turn6search1

Для **API-контракта и безопасности** лучше всего опираться на нормативные документы: OpenAPI, JSON Schema, RFC 9457, OAuth 2.0, JWT/JWT BCP, PKCE, NIST SP 800‑63B‑4 и OWASP ASVS. Такая комбинация даёт одновременно формальную и практическую основу: как описывать API, как стандартизовать ошибки, как проектировать аутентификацию и как проверять безопасность приложения. citeturn6search2turn26search0turn9search1turn8search2turn10search3turn32search1turn33search1turn34search9turn27search0

Для **Use Case, функциональных требований и спецификации сценариев** наиболее сильный набор — ISO/IEC/IEEE 29148, UML 2.5.1, Cockburn, Wiegers & Beatty, Robertson и BABOK. Они закрывают и формальную сторону требований, и практику сценариев, и шаблоны спецификаций. citeturn15search0turn14search1turn17search0turn16search5turn16search7turn16search6

## Таблица кандидатов для библиографии и теоретической части

**Часть A**

| ID | Раздел | Источник | Автор/организация | Год | Тип | Статус | Почему полезен | Ссылка |
|---|---|---|---|---|---|---|---|---|
| S01 | Документооборот и заявки | ISO 15489-1:2016 Information and documentation — Records management — Part 1: Concepts and principles | ISO | 2016 | стандарт | основной | Базовые принципы управления документами и жизненного цикла записей | citeturn0search0 |
| S02 | Документооборот и заявки | ISO 30300:2020 Information and documentation — Records management — Core concepts and vocabulary | ISO | 2020 | стандарт | основной | Терминология и понятийная рамка для систем управления документами | citeturn0search1 |
| S03 | Документооборот и заявки | BPMN Version 2.0.2 | OMG | 2013 | спецификация | нормативный | Формальная нотация для описания бизнес‑процессов и маршрутов заявок | citeturn0search2 |
| S04 | Документооборот и заявки | Workflow Management: Models, Methods, and Systems | W.M.P. van der Aalst, K.M. van Hee | 2002 | монография | основной | Теория workflow‑систем, модели, маршрутизация и исполнение процессов | citeturn2search17 |
| S05 | Документооборот и заявки | Introduction to Business Process Management | M. Dumas, M. La Rosa, J. Mendling, H.A. Reijers | 2018 | глава книги | основной | Компактный ввод в BPM‑подход, полезен для теоретической главы | citeturn4search4 |
| S06 | Документооборот и заявки | ISO 30301:2019 Information and documentation — Management systems for records — Requirements | ISO | 2019 | стандарт | основной | Требования к системе управления документами на уровне организации | citeturn30search1 |
| S07 | Документооборот и заявки | ISO 30302:2022 Information and documentation — Management systems for records — Guidelines for implementation | ISO | 2022 | стандарт | основной | Пошаговая логика внедрения системы управления документами | citeturn30search19 |
| S08 | Документооборот и заявки | Workflow Management Coalition Terminology & Glossary | WfMC | 1999 | глоссарий/стандарт | справочный | Полезен для точного словаря терминов workflow и process management | citeturn31search1 |
| S09 | Клиент‑серверные web‑приложения | Architectural Styles and the Design of Network-based Software Architectures | Roy T. Fielding | 2000 | диссертация | основной | Фундамент для REST, client‑server и ограничений сетевой архитектуры | citeturn35search0 |
| S10 | Клиент‑серверные web‑приложения | Architecture of the World Wide Web, Volume One | W3C | 2004 | рекомендация W3C | нормативный | Принципы URI, ресурсов, представлений и общей web‑архитектуры | citeturn3search2 |
| S11 | Клиент‑серверные web‑приложения; OpenAPI и API contract | RFC 9110 HTTP Semantics | IETF / RFC Editor | 2022 | RFC/стандарт | нормативный | Нормативная база HTTP‑методов, кодов ответа и семантики запросов | citeturn8search0 |
| S12 | Клиент‑серверные web‑приложения | Patterns of Enterprise Application Architecture | Martin Fowler | 2002 | монография | основной | Паттерны серверной архитектуры, слои, repository, service, transaction | citeturn2search1 |
| S13 | Клиент‑серверные web‑приложения | Software Architecture in Practice, 4th Edition | Len Bass, Paul Clements, Rick Kazman | 2021 | монография | основной | Архитектурные решения, quality attributes и связь архитектуры с требованиями | citeturn3search0 |
| S14 | Клиент‑серверные web‑приложения; OpenAPI и API contract | RESTful Web APIs | Leonard Richardson, Mike Amundsen, Sam Ruby | 2013 | монография | основной | Практическая и теоретическая база для HTTP/REST‑ориентированного API | citeturn35search3 |
| S15 | Клиент‑серверные web‑приложения | Common web application architectures | Microsoft | 2023 | архитектурный гайд | справочный | Практические схемы слоёв и развёртывания web‑приложений | citeturn25search1 |
| S16 | ASP.NET Core и REST API | Overview of ASP.NET Core | Microsoft | 2025 | официальная документация | справочный | Кратко фиксирует свойства платформы и её основные возможности | citeturn11search17 |
| S17 | ASP.NET Core и REST API | ASP.NET Core fundamentals overview | Microsoft | 2025 | официальная документация | справочный | Middleware, DI, конфигурация и базовые строительные блоки платформы | citeturn25search17 |
| S18 | ASP.NET Core и REST API | Create web APIs with ASP.NET Core | Microsoft | 2026 | официальная документация | справочный | Официальный материал по построению Web API на контроллерах | citeturn11search1 |
| S19 | ASP.NET Core и REST API | Create a controller-based web API with ASP.NET Core | Microsoft | 2026 | tutorial | справочный | Показывает CRUD‑контур API и типовой pipeline обработки запросов | citeturn11search9 |
| S20 | ASP.NET Core и REST API | Architect modern web apps with ASP.NET Core and Azure | Microsoft | 2025 | eBook/guide | справочный | Полезен для обоснования выбранной архитектуры монолитного web‑приложения | citeturn25search7 |
| S21 | ASP.NET Core и REST API; Безопасность и аутентификация | Overview of ASP.NET Core Authentication | Microsoft | 2026 | официальная документация | справочный | Объясняет встроенную auth‑pipeline платформы и её extension points | citeturn11search2 |
| S22 | React и TypeScript | Quick Start | React Team / Meta | б/д | официальная документация | справочный | Быстрый вход в компоненты, props, state и событийную модель | citeturn7search0 |
| S23 | React и TypeScript | Thinking in React | React Team / Meta | б/д | tutorial | справочный | Хорошо подходит для описания компонентной декомпозиции интерфейса | citeturn7search2 |
| S24 | React и TypeScript | Managing State | React Team / Meta | б/д | официальная документация | справочный | Полезен для раздела о структуре состояния и передаче данных | citeturn7search3 |
| S25 | React и TypeScript | The TypeScript Handbook | Microsoft / TypeScript Team | б/д | handbook | справочный | Основной официальный учебный материал по типам и системе типов | citeturn6search1 |
| S26 | React и TypeScript | Effective TypeScript, 2nd Edition | Dan Vanderkam | 2024 | монография | основной | Практические приёмы безопасного проектирования типов и API | citeturn36search5 |
| S27 | React и TypeScript | Learning React, 2nd Edition | Alex Banks, Eve Porcello | 2020 | монография | основной | Структурированный учебный источник по современной React‑разработке | citeturn37search17 |
| S28 | React и TypeScript | Fluent React | Tejas Kumar | 2024 | монография | основной | Современные React‑паттерны и организация frontend‑кода | citeturn37search20 |
| S29 | EF Core и SQL Server | Overview of Entity Framework Core | Microsoft | 2024 | официальная документация | справочный | Фиксирует роль EF Core как ORM и его место в .NET‑стеке | citeturn24search11 |
| S30 | EF Core и SQL Server | Introduction to relationships | Microsoft | 2023 | официальная документация | справочный | Объясняет отображение отношений объектов в реляционную модель | citeturn24search1 |
| S31 | EF Core и SQL Server | Efficient Querying | Microsoft | 2023 | официальная документация | справочный | Ключевой материал по загрузке данных, индексам и форме запросов | citeturn24search9 |
| S32 | EF Core и SQL Server | Migrations Overview | Microsoft | 2023 | официальная документация | справочный | Подходит для описания эволюции схемы БД вместе с моделью | citeturn5search21 |
| S33 | EF Core и SQL Server | SQL Server internals and architecture guides | Microsoft | 2026 | официальная документация | справочный | Сводная точка входа в материалы по внутренней архитектуре SQL Server | citeturn12search5 |
| S34 | EF Core и SQL Server | Index Architecture and Design Guide | Microsoft | 2025 | официальная документация | справочный | Полезно для раздела о производительности и индексации | citeturn12search2 |

**Часть B**

| ID | Раздел | Источник | Автор/организация | Год | Тип | Статус | Почему полезен | Ссылка |
|---|---|---|---|---|---|---|---|---|
| S35 | EF Core и SQL Server | SET TRANSACTION ISOLATION LEVEL (Transact-SQL) | Microsoft | 2025 | официальная документация | справочный | Нужен для обоснования транзакций, изоляции и конкурентного доступа | citeturn12search18 |
| S36 | EF Core и SQL Server | Fundamentals of Database Systems | Ramez Elmasri, Shamkant B. Navathe | 2021 | монография | основной | База по моделированию данных и реляционным принципам | citeturn13search5 |
| S37 | OpenAPI и API contract | OpenAPI Specification v3.1.1 | OpenAPI Initiative | 2024 | спецификация | нормативный | Нормативная форма описания HTTP API и их контрактов | citeturn6search2 |
| S38 | OpenAPI и API contract | OpenAPI Specification v3.2.0 | OpenAPI Initiative | 2025 | спецификация | нормативный | Актуальная линия спецификации на момент проверки отчёта | citeturn6search14 |
| S39 | OpenAPI и API contract | JSON Schema Draft 2020-12 | JSON Schema authors | 2020 | спецификация | нормативный | Основа для схем валидации данных и совместимости с OAS 3.1+ | citeturn26search0 |
| S40 | OpenAPI и API contract | JSON Schema Specification | JSON Schema authors | 2020-12/current | спецификация | нормативный | Подробная база по Core/Validation и композиции схем | citeturn26search8 |
| S41 | OpenAPI и API contract | RFC 9457 Problem Details for HTTP APIs | IETF / RFC Editor | 2023 | RFC/стандарт | нормативный | Стандартная машиночитаемая форма ошибок HTTP API | citeturn9search1 |
| S42 | OpenAPI и API contract | Consumer-Driven Contracts: A Service Evolution Pattern | Martin Fowler | 2006 | статья | справочный | Концептуальная база для согласования контрактов поставщика и потребителя | citeturn26search2 |
| S43 | OpenAPI и API contract; Тестирование web‑приложений | Contract Test | Martin Fowler | 2018 | статья | справочный | Кратко объясняет роль контрактных тестов в пайплайне разработки | citeturn26search6 |
| S44 | Тестирование web‑приложений | ISTQB Certified Tester Foundation Level Syllabus v4.0.1 | ISTQB | 2024 | syllabus/standard | нормативный | Канонический словарь и структура процесса тестирования ПО | citeturn18search0 |
| S45 | Тестирование web‑приложений; ASP.NET Core и REST API | Integration tests in ASP.NET Core | Microsoft | 2026 | официальная документация | справочный | Покрывает интеграционные тесты request pipeline и hosted app | citeturn11search3 |
| S46 | Тестирование web‑приложений | Playwright Introduction | Microsoft | б/д | официальная документация | справочный | Подходит для обоснования E2E‑тестов современного web‑интерфейса | citeturn23search3 |
| S47 | Тестирование web‑приложений | React Testing Library | Testing Library authors | 2024 | официальная документация | справочный | Полезен для компонентных тестов React с пользовательской перспективы | citeturn22search1 |
| S48 | Тестирование web‑приложений | A Survey on Web Application Testing: A Decade of Evolution | Tao Li, Rubing Huang, Chenhui Cui, Dave Towey, Lei Ma, Yuan-Fang Li, Wen Xia | 2024 | survey article | основной | Современный обзор методов и инструментов web application testing | citeturn21search3 |
| S49 | Тестирование web‑приложений | A Survey on Web Testing: On the Rise of AI and Applications in Industry | Iva Kertusha, Gebremariem Assress, Onur Duman, Andrea Arcuri | 2025 | survey article | основной | Показывает последние тренды, автоматизацию и влияние ИИ на web testing | citeturn21search7 |
| S50 | Тестирование web‑приложений | Challenges of End-to-End Testing with Selenium WebDriver and How to Face Them: A Survey | Maurizio Leotta, Boni García, Filippo Ricca, Jim Whitehead | 2023 | IEEE paper | основной | Систематизирует риски и ограничения end‑to‑end тестирования | citeturn21search2 |
| S51 | Безопасность и аутентификация | OWASP Top Ten Web Application Security Risks | OWASP | 2025 | отраслевой стандарт | нормативный | Актуальная карта основных рисков web‑безопасности | citeturn32search7 |
| S52 | Безопасность и аутентификация | OWASP Top 10:2021 | OWASP | 2021 | отраслевой стандарт | нормативный | Широко цитируемая версия, полезна для сопоставления с прежними работами | citeturn10search0 |
| S53 | Безопасность и аутентификация | OWASP Application Security Verification Standard | OWASP | б/д | verification standard | нормативный | Чек‑лист требований к техническим контролям безопасности web‑приложений | citeturn27search0 |
| S54 | Безопасность и аутентификация | Password Storage Cheat Sheet | OWASP | б/д | cheat sheet | справочный | Практика безопасного хранения паролей и выбора hashing‑схем | citeturn27search1 |
| S55 | Безопасность и аутентификация | Session Management Cheat Sheet | OWASP | б/д | cheat sheet | справочный | Рекомендации по таймаутам, инвалидации и защите сессий | citeturn27search2 |
| S56 | Безопасность и аутентификация | RFC 6749 The OAuth 2.0 Authorization Framework | IETF / RFC Editor | 2012 | RFC/стандарт | нормативный | Фундаментальная модель авторизации по токенам | citeturn8search2 |
| S57 | Безопасность и аутентификация | OpenID Connect Core 1.0 incorporating errata set 2 | OpenID Foundation | б/д | спецификация | нормативный | Базовая спецификация аутентификации поверх OAuth 2.0 | citeturn8search15 |
| S58 | Безопасность и аутентификация | RFC 7519 JSON Web Token | IETF / RFC Editor | 2015 | RFC/стандарт | нормативный | Стандарт формата JWT, часто используемого в web‑приложениях | citeturn10search3 |
| S59 | Безопасность и аутентификация | RFC 8725 JSON Web Token Best Current Practices | IETF / RFC Editor | 2020 | BCP/RFC | нормативный | Безопасные практики внедрения и валидации JWT | citeturn32search1 |
| S60 | Безопасность и аутентификация | RFC 7636 Proof Key for Code Exchange by OAuth Public Clients | IETF / RFC Editor | 2015 | RFC/стандарт | нормативный | Важен для безопасной code flow‑авторизации публичных клиентов | citeturn33search1 |
| S61 | Безопасность и аутентификация | NIST SP 800-63B-4 Digital Identity Guidelines: Authentication and Lifecycle Management | NIST | 2025 | guideline/standard | нормативный | Сильная нормативная база по аутентификации и жизненному циклу аутентификаторов | citeturn34search9 |
| S62 | Use Case и требования | ISO/IEC/IEEE 29148:2018 Systems and software engineering — Life cycle processes — Requirements engineering | ISO/IEC/IEEE | 2018 | стандарт | основной | Базовый международный стандарт по engineering‑подходу к требованиям | citeturn15search0 |
| S63 | Use Case и требования | Unified Modeling Language 2.5.1 | OMG | 2017 | спецификация | нормативный | Формальная основа UML‑диаграмм и use case‑моделирования | citeturn14search1 |
| S64 | Use Case и требования | Writing Effective Use Cases | Alistair Cockburn | 2000 | монография | основной | Классический источник по построению use case и сценариев | citeturn17search0 |
| S65 | Use Case и требования | Software Requirements, Third Edition | Karl Wiegers, Joy Beatty | 2013 | монография | основной | Сильный учебный источник по функциональным и нефункциональным требованиям | citeturn16search5 |
| S66 | Use Case и требования | BABOK Guide v3 | IIBA | 2015 | body of knowledge | основной | Полезен для техник выявления, анализа и трассировки требований | citeturn16search6 |
| S67 | Use Case и требования | Mastering the Requirements Process, 3rd edition | Suzanne Robertson, James Robertson | 2012 | монография | основной | Практика выявления и проверки требований, включая шаблоны спецификаций | citeturn16search7 |
| S68 | Use Case и требования | Patterns for Effective Use Cases | Steve Adolph, Paul Bramble, Alistair Cockburn, Andy Pols | 2002 | монография | основной | Полезен для углублённой проработки качества и шаблонов use case | citeturn17search8 |

## Что лучше использовать только как справочные источники

| Категория | ID | Как использовать |
|---|---|---|
| Официальная документация по фреймворкам и платформам | S15–S25, S29–S35, S45–S47 | Использовать для описания реализации, актуального API, middleware, ORM, SQL Server, тестовых инструментов и конкретных технических решений. Не делать из них основу всей теоретической главы. |
| Practitioner-материалы и короткие концептуальные статьи | S42–S43, S54–S55 | Хорошо подходят как уточняющие источники по contract testing и практикам защиты, но лучше сопровождать их стандартами, RFC, NIST, книгами и обзорами. |
| Отраслевые baseline-документы безопасности | S51–S53 | Их стоит включать в ВКР, но желательно не оставлять единственными источниками по безопасности: лучше сочетать с RFC, NIST и книгами/обзорами. |

## Стартовая выборка 45 источников и ограничения

| Раздел | Рекомендуемые 5 источников для итоговых 45 | Почему именно они |
|---|---|---|
| Документооборот и заявки | **S01, S03, S04, S06, S07** | Дают стандарты records management, формальную нотацию BPMN и классическую workflow/BPM-базу. |
| Клиент‑серверные web‑приложения | **S09, S10, S11, S12, S13** | Закрывают REST, web‑архитектуру, HTTP semantics и архитектурные паттерны. |
| ASP.NET Core и REST API | **S16, S17, S18, S19, S20** | Этого достаточно, чтобы технично и корректно обосновать backend‑стек и API‑слой. |
| React и TypeScript | **S22, S23, S25, S26, S27** | Хороший баланс между официальной моделью React/TS и более академичными/учебными книгами. |
| EF Core и SQL Server | **S29, S30, S31, S33, S36** | Достаточно для ORM, связей, запросов, внутренней архитектуры СУБД и общей базы по БД. |
| OpenAPI и API contract | **S37, S39, S41, S42, S43** | Закрывают формальный API‑контракт, схемы данных, ошибки API и идею contract testing. |
| Тестирование web‑приложений | **S44, S45, S46, S48, S50** | Дают терминологию тестирования, интеграционные/E2E подходы и обзорное состояние исследований. |
| Безопасность и аутентификация | **S51, S53, S56, S60, S61** | Набор покрывает риски, требования к контролям, OAuth, PKCE и современные нормы аутентификации. |
| Use Case и требования | **S62, S63, S64, S65, S67** | Это самый сильный каркас для требований, UML и сценариев использования. |

Ограничения отчёта: часть официальных web‑документов не показывает явный год публикации в доступном карточечном представлении поиска; такие позиции помечены как **«б/д»**. При финальном оформлении библиографии по ГОСТ или вузовской методичке их лучше оформлять как **электронные ресурсы организации** с датой обращения. Для более «академического» профиля итогового списка литературы можно ещё сильнее сместить баланс в сторону стандартов, монографий и обзорных работ, а продуктовую документацию оставить прежде всего для главы о проектировании и реализации.