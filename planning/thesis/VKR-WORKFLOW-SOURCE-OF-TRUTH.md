# VKR workflow source of truth

This file is the main reference for how VKR materials are created, checked and turned into section drafts.

## 1. Main workflow formula

```text
raw notes
→ VKR-DRAFTING-ROADMAP.md
→ chapter roadmap
→ desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ semantic point discovery
→ topic draft
↔ mandatory chat algorithms
↔ source materials
↔ questions / default answers / variants
↔ research / repo / visual / domain / slices / existing section candidates
↔ section draft blocks
→ section draft v1
→ reviewer pass
→ clean VKR text
```

Topic draft and section draft develop in parallel. A topic draft is not a final text. It is a semantic base that feeds section draft blocks.

## 2. Desired-result coverage rule

Topic-level desired outcome defines which semantic points are needed.

Do not create one huge coverage table for all desired topic results.

Detailed coverage belongs inside each semantic point card:

```text
semantic point
→ desired result of this point
→ coverage table for this point
→ questions/checks/plan
→ section draft block
```

This keeps user ideas and source materials attached to the exact point they help раскрыть.

## 3. Capture-first roadmap rule

If a user message contains cross-topic ideas, future notes, disputed decisions or material that may belong to several future drafts, do not force it into the current topic draft immediately.

First capture the shared picture in:

```text
planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
```

Then distribute the material into:

```text
chapter roadmap
→ topic draft
→ section draft block
```

This means topic drafts do not need to carry all future notes. They should stay focused on the current topic and reference roadmap notes only when needed.

Use:

```text
planning/thesis/chat-action-algorithms/drafting/raw-notes-capture-and-distribution.md
```

## 4. Central role of chat action algorithms

```text
planning/thesis/chat-action-algorithms/
```

This folder is a central workflow layer, not an optional note collection. If the user gives a typical command, the chat must run the matching algorithm.

| User signal | Required algorithm |
|---|---|
| raw notes / many future ideas | `drafting/raw-notes-capture-and-distribution.md` |
| semantic point discovery | `drafting/semantic-point-discovery.md` |
| `дай драфт`, `давай драфт`, `обнови драфт` | `drafting/topic-draft-default-flow.md` |
| `уточни`, `перепроверь`, `проверь всё` | `drafting/topic-clarify-and-recheck-flow.md` |
| `дай section draft` | `drafting/section-draft-generation.md` |
| visual PDF / diagrams / screenshots | `evidence-and-materials/visual-material-review.md` |
| research insertion | `evidence-and-materials/research-bridge.md` |
| implementation text / chapter 3 | `evidence-and-materials/repo-check-before-implementation-text.md` |
| existing chapter / old section draft | `cleanup-and-legacy/existing-chapter-draft-review.md` |
| existing section candidate reverse engineering | `cleanup-and-legacy/existing-section-draft-reverse-engineering.md` |
| archive creation | `archive-generation-and-navigation-update.md` |
| new chat lost context | `new-chat-context-recovery.md` |
| `тчт` | `tcht-command.md` |

## 5. Storage map

For a full resource map, read:

```text
planning/thesis/VKR-RESOURCE-MAP.md
```

## 6. Guardrails

- Do not include AI, ChatGPT, prompts, agent workflow or chat process in VKR text.
- Do not use L1/L2 as VKR language.
- Account activation must not be described as a realised VKR user flow without repo-check.
- Mock data check is a demonstration / extension point, not a real external integration.
- The agreement stage is started by the employee after approval. The system does not automatically generate a contract.
- Document reference / metadata is not a full ECM/EDO/storage solution.
- Do not claim complete email notification implementation without repo/evidence check.

## 7. Archive rule

Before giving an archive link, the chat must open and verify the zip contents, check MANIFEST/APPLY and confirm navigation impact check was performed.
