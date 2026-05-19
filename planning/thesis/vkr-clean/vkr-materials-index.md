# VKR Materials Index

Статус: navigation / clean VKR materials / block-based workflow

## 1. Entry Points

| File | Purpose |
|---|---|
| `../README.md` | Главный вход в `planning/thesis/` |
| `../VKR-WORKFLOW-SOURCE-OF-TRUTH.md` | Главный source of truth по workflow |
| `../NEW-CHAT-ONBOARDING.md` | Памятка для новых чатов |
| `README.md` | Назначение `vkr-clean/` |
| `vkr-materials-index.md` | Навигация по clean VKR materials |
| `vkr-outline.md` | Рабочая структура ВКР |
| `evidence-map.md` | Source/evidence mapping для утверждений |
| `terminology.md` | Предметная и техническая терминология |

## 2. Section Draft Workflow

| File / material | Purpose |
|---|---|
| `section-drafts/README.md` | Entry point for section draft workflow |
| `section-drafts/section-draft-register.md` | Register of subsection draft statuses and review state |
| `section-drafts/reviewer-workflow.md` | Reviewer process and feedback consolidation |
| `section-drafts/reviewer-prompts.md` | Prompts for content, structure and style/originality reviewers |
| `section-drafts/full-draft-template.md` | Full draft attempt template |
| `section-drafts/full-draft-review-checklist.md` | Checklist for reviewing full draft attempts |

Active section drafting is block-based:

```text
topic draft
↔ questions / research / repo-check / visual bridge
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

`section-drafts/fragment-bank.md` is deprecated/support and not an active workflow dependency.

## 3. Topic Workbench Link

Topic-драфты хранятся здесь:

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Главный переход от topic-драфта к section-драфту:

```text
planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
```

## 4. Writing Protocol

| File | Purpose |
|---|---|
| `writing-protocol/README.md` | Entry point for controlled VKR text assembly |
| `writing-protocol/source-provenance-protocol.md` | Rules for using project artifacts, implementation evidence, research and analysis |
| `writing-protocol/section-card-template.md` | Legacy/adjacent section card template |
| `writing-protocol/research-usage-rules.md` | Rules for using research without compilation-style text |
| `writing-protocol/chapter-section-question-map.md` | Project-centered questions for future subsections |

## 5. Legacy chaotic drafts

| Path | Purpose |
|---|---|
| `legacy-chaotic-drafts/` | Weak/chaotic chapter drafts created before the workflow; use only for analysis/harvest |

## 6. Clean Source Files

| File | Purpose |
|---|---|
| `clean-requirements.md` | Functional and non-functional requirement summary |
| `functional-specification.md` | Behavior specification: actors, preconditions, main flow, alternatives, postconditions |
| `clean-use-cases.md` | Use-case/scenario summary |
| `clean-data-requirements.md` | Input, visible and stored data requirements |
| `clean-domain-model.md` | Domain model and lifecycle explanations |
| `clean-architecture.md` | Client-server architecture and design approach |
| `api-contract-and-client-server-sync.md` | API contract and frontend/backend synchronization |
| `clean-database-design.md` | Database design and persistence description |
| `clean-ui-description.md` | User interface description |
| `clean-testing.md` | Testing strategy and verification |
| `clean-results-and-future-work.md` | Results and further development |

## 7. Navigation rule

If a file or folder is added, moved, deprecated or marked active/support/legacy, update this index and the relevant README in the same change.
