# Raw Author Message Log

Status: raw / not processed  
Scope: author messages captured before creating `enman-vkr-topic-workbench-v1.zip`

These entries are intentionally raw. They are not final thesis text and not polished rules.

## Entry 001 — Topic workbench instead of direct drafting

Date: 2026-05-18  
Source: chat message  
Related area: workflow / VKR topic workbench  
Status: raw / not-processed

Raw author message:
> ну отчет по ПП это очень малая версия диплома, а диплом то походу надо иметь написанный по крайней мере предварительный предзащитный, так что эту хрень лучше просто как производное из вкр сделать в конце 2 разделение тем это хорошо, я бы сделал папку, в корне будут все правила, воркфлоу и основные файлы а дальше будет стркутура по неизбежным пунктам вкр и в эти папки этих пунктов мы будем складывать во первых какие то общие файлы, индекс мб, что то для навигации и общего пояснения, и файлы тем(по сути подпунктов в требуемых пунктах) ...

Notes:
- Store as raw source for workflow reasoning.
- Do not use directly as final text.

## Entry 002 — What topic files should contain

Date: 2026-05-18  
Source: chat message  
Related area: topic card format  
Status: raw / not-processed

Raw author message:
> ... я бы тут как и в моих слайс драфтах ввел такую вещь как то чего мы хотим добиться в подпункте этой темы(что именно нужно упоминуть, описать, раскрыть, на какие именно вопросы ответить,что нужно показать) + отдельно я бы ввел запланированую имплементацию того как мы будем делать то что нужно сделать в этой теме (тут мы прямо описываем - берем такую то инфу, так то ее преподносим, такие то диаграммы, так то обосновываем) ... + вопросы к теме, открытые и нет, их ответы которые дали и базовые ассампшенс ... + таблица покрытия того что нужно показать в этой теме нашим импле флоу ...

Notes:
- Raw basis for `topic-card-template.md`.

## Entry 003 — Top folders should be required VKR points

Date: 2026-05-18  
Source: chat message  
Related area: workbench folder hierarchy  
Status: raw / not-processed

Raw author message:
> пример папок что ты дал хороший но верхние папки это же требуемые пункты scenarios-and-specification domain-model-and-ddd это точно не требуемые пункты диплома

Notes:
- Raw basis for top-level folder rule.

## Entry 004 — Mandatory base vs project-specific themes

Date: 2026-05-18  
Source: chat message  
Related area: workbench hierarchy  
Status: raw / not-processed

Raw author message:
> самые верхние это те без которых не обойтись, которые нас заставили иметь, а scenarios-and-specification, domain-model-and-ddd, architecture-and-slice это наши темы/подпункты, тут по сути тема может сама стать родительской папкой если она большеая и включает много тем, но самые верхние это именно база содержания вкр

Notes:
- Raw basis for hierarchy rule and topic-as-folder exception.

## Entry 005 — Store author messages separately without processing for now

Date: 2026-05-18  
Source: chat message  
Related area: author message capture  
Status: raw / not-processed

Raw author message:
> давай пока что прост в отдельное место складировать сообщения как ты сказал ., но пока без обработки. теперь перед созанием архива еще раз осознай что имнно нужно делать

Notes:
- Defines raw-only capture for v1.

## Entry 006 — Avoid L1/L2 as VKR concepts

Date: 2026-05-18  
Source: chat message  
Related area: terminology / implementation evidence  
Status: raw / not-processed

Raw author message:
> и еще желателно не использовать больше понятия л1 л2 , т к это нужно для имплементации, это можно будет указать только когда мы будем обосновывать архитектуру типо можно разделить процесс на фазы имплементации и удобно все делать вместо того чтобы все сразу, что то типо того, но л1 л2 мне не нравится как что то значимие для пунктов вкр

Notes:
- Raw basis for L1/L2 terminology rule.

## Entry 007 — Announce captured messages before archive creation

Date: 2026-05-18  
Source: chat message  
Related area: archive workflow / author message capture  
Status: raw / not-processed

Raw author message:
> давай еще ты будешь упоминать перед созданием архива какие мои сообщения будут добавлены в  то место где они хранятся чтобы не забывать про это(в воркфлоу это стоит упомянуть или где там лучше это сделать для подобных вещей)

Notes:
- Raw basis for archive generation checklist.

## Entry 008 — Do not overwrite fresh commit

Date: 2026-05-18  
Source: chat message  
Related area: archive generation safety  
Status: raw / not-processed

Raw author message:
> давай архив. там коммит щас был, не сотри ничего случайно + ничего не забудь

Notes:
- This archive adds new files under a new folder and avoids replacing existing repo files.
