# VKR workflow source of truth

This file is the main reference for how VKR materials are created, checked and turned into section drafts.

## 1. Main workflow formula

```text
topic draft
↔ обязательные алгоритмы чатов
↔ источники материалов
↔ вопросы / дефолтные ответы / варианты
↔ research / repo / visual / domain / slices / existing chapter drafts
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Topic draft and section draft develop in parallel. A topic draft is not a final text. It is a semantic base that feeds section draft blocks.

## 2. Central role of chat action algorithms

```text
planning/thesis/chat-action-algorithms/
```

This folder is a central workflow layer, not an optional note collection. If the user gives a typical command, the chat must run the matching algorithm.

| User signal | Required algorithm |
|---|---|
| `дай драфт`, `давай драфт`, `обнови драфт` | `drafting/topic-draft-default-flow.md` |
| `уточни`, `перепроверь`, `проверь всё` | `drafting/topic-clarify-and-recheck-flow.md` |
| `дай section draft` | `drafting/section-draft-generation.md` |
| visual PDF / diagrams / screenshots | `evidence-and-materials/visual-material-review.md` |
| research insertion | `evidence-and-materials/research-bridge.md` |
| implementation text / chapter 3 | `evidence-and-materials/repo-check-before-implementation-text.md` |
| existing chapter / old section draft | `cleanup-and-legacy/existing-chapter-draft-review.md` |
| archive creation | `archive-generation-and-navigation-update.md` |
| new chat lost context | `new-chat-context-recovery.md` |
| `тчт` | `tcht-command.md` |

## 3. Storage map

For a full resource map, read:

```text
planning/thesis/VKR-RESOURCE-MAP.md
```

### Topic drafts

```text
planning/thesis/vkr-topic-workbench/**/*.topic.md
```

Topic drafts contain:

```text
placement in VKR;
meaning of the topic;
what changed since previous draft;
source materials;
useful material candidates;
questions with priority;
default answers and variants;
research bridge;
repo/evidence questions;
visual bridge;
boundaries and overclaim risks;
future section draft blocks;
plan of disclosure for each block.
```

### Section drafts

```text
planning/thesis/vkr-clean/section-drafts/
```

Section drafts are not generated from nowhere. They are built from section blocks, each connected to a topic-draft semantic block.

No required `fragment-bank.md`. If text is ready, it belongs inside the relevant section-draft block.

### Existing chapter drafts

```text
planning/thesis/vkr-clean/existing-chapter-drafts/
```

Existing chapter drafts are a secondary resource. They may be not bad and may contain useful wording, tables and structure, but they are not source of truth and are not final clean text. They can feed topic drafts and section drafts only after review.

Use:

```text
planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-chapter-draft-review.md
```

### Clean materials

```text
planning/thesis/vkr-clean/
```

Clean materials are VKR-safe engineering materials. Do not copy planning prompts, AI/chat workflow or internal generation notes into the final thesis.

### Legacy chaotic drafts

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/
```

Chaotic/weak chapter versions created outside the workflow are stored here for analysis only. They are not clean section drafts.

## 4. Chapter source priority

Use:

```text
planning/thesis/chat-action-algorithms/evidence-and-materials/chapter-source-priority-map.md
```

Short version:

- Chapter 1: topic drafts, existing chapter drafts, research, visuals, clean requirements; scenario/domain/repo only to check logic and avoid overclaiming.
- Chapter 2: topic drafts, existing chapter drafts, scenarios, DATA, domain, requirements, architecture, UI/API/database planning.
- Chapter 3: code, tests, slice drafts, scenarios, DATA, domain, ADR/questions/decisions, screenshots, repo/evidence checks.

## 5. Topic-to-section block workflow

```text
planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
```

Main rule:

```text
topic-драфт задаёт смысловые блоки;
section-драфт создаёт похожие блоки/подзаголовки;
для каждого блока задаются вопросы;
по вопросам собираются материалы;
для блока составляется план раскрытия;
текст постепенно пишется внутрь section-драфта.
```

Existing chapter drafts may provide candidate blocks or candidate text, but any reused material must pass through the same block workflow.

## 6. Navigation impact check

Whenever a file or folder changes, run:

```text
planning/thesis/chat-action-algorithms/navigation-impact-check.md
```

The chat must check:

```text
Какие навигационные файлы нужно обновить из-за этого изменения?
```

Always consider:

```text
planning/thesis/README.md
planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
planning/thesis/VKR-RESOURCE-MAP.md
planning/thesis/NEW-CHAT-ONBOARDING.md
planning/thesis/chat-action-algorithms/README.md
planning/thesis/vkr-topic-workbench/README.md
planning/thesis/vkr-clean/README.md
chapter README / topic-index when relevant
```

Do not maintain statuses in `topic-index.md`. Topic-index is navigation, not a task board.

## 7. Question priority

Questions are not just a large list. They must be prioritised:

```text
blocking — without answer unsafe/impossible to write;
strong — improves precision and project specificity;
research — requires external support;
repo/evidence — prevents overclaim;
visual — needed for schemes/screenshots;
style — useful later during editing.
```

Use:

```text
planning/thesis/chat-action-algorithms/drafting/question-priority.md
```

## 8. Guardrails

### AI / prompts / chats

Do not include AI, ChatGPT, prompts, agent workflow or chat process in VKR text.

### L1/L2

Do not use L1/L2 as VKR language. They may remain internal implementation labels only.

### Account activation

Account activation must not be described as a realised VKR user flow. Treat it as supporting auth detail / future or limited detail unless a dedicated repo-check proves otherwise.

### Mock check

Mock data check is a demonstration / extension point, not a real external integration.

### Agreement stage

The agreement stage is started by the employee after approval. The system does not automatically generate a contract. It supports exchange of ready document versions.

### Documents

Document reference / metadata is not a full ECM/EDO/storage solution. Do not claim e-signature, legal EDO, external integrations, industrial file storage or full audit without repo-check.

### Email

Do not claim complete email notification implementation without repo/evidence check.

## 9. Archive rule

Before giving an archive link, the chat must open and verify the zip contents, check MANIFEST/APPLY, check originals for replacements, and confirm navigation impact check was performed.
