# New chat onboarding for VKR work

Read this before working on VKR materials.

## 1. Main files

1. `planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md`
2. `planning/thesis/VKR-RESOURCE-MAP.md`
3. `planning/thesis/README.md`
4. `planning/thesis/chat-action-algorithms/README.md`
5. `planning/thesis/vkr-topic-workbench/README.md`
6. `planning/thesis/vkr-clean/README.md`

## 2. Roadmap first

If the user gives many future notes, cross-topic thoughts or uncertain ideas, capture them first in:

```text
planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
```

Raw notes may also go to:

```text
planning/thesis/vkr-topic-workbench/00-inbox/raw-notes/
```

Do not force every note into the current topic draft.

Use:

```text
planning/thesis/chat-action-algorithms/drafting/raw-notes-capture-and-distribution.md
```

## 3. Desired-result coverage

Do not create one huge desired-result coverage table for the entire topic.

Use this rule:

```text
theme desired outcome
→ semantic points
→ each semantic point has its own desired result
→ each semantic point has its own coverage table
```

So when the user gives an idea, the chat should show:

- which desired result inside the semantic point it covers;
- whether it stays in the current topic;
- whether it goes to roadmap;
- whether it needs research/repo/visual check.

## 4. What not to do

Do not write the thesis directly from memory. Do not copy planning files directly into the thesis. Do not mention AI, prompts, chats or agent workflow in VKR text.

Do not use L1/L2 as thesis language.

Do not overclaim implementation.

## 5. What to do on typical commands

| User says | Run |
|---|---|
| raw notes / many future ideas | `chat-action-algorithms/drafting/raw-notes-capture-and-distribution.md` |
| `дай драфт`, `давай драфт`, `обнови драфт` | `chat-action-algorithms/drafting/topic-draft-default-flow.md` |
| semantic point discovery | `chat-action-algorithms/drafting/semantic-point-discovery.md` |
| `уточни`, `перепроверь` | `chat-action-algorithms/drafting/topic-clarify-and-recheck-flow.md` |
| `дай section draft` | `chat-action-algorithms/drafting/section-draft-generation.md` |
| user sends diagrams | `chat-action-algorithms/evidence-and-materials/visual-material-review.md` |
| implementation/chapter 3 text | `chat-action-algorithms/evidence-and-materials/repo-check-before-implementation-text.md` |
| existing chapter / old section draft | `chat-action-algorithms/cleanup-and-legacy/existing-chapter-draft-review.md` |
| existing section candidate reverse engineering | `chat-action-algorithms/cleanup-and-legacy/existing-section-draft-reverse-engineering.md` |
| archive request | `chat-action-algorithms/archive-generation-and-navigation-update.md` |
| `тчт` | `chat-action-algorithms/tcht-command.md` |

## 6. Core guardrails

- Account activation is not a central implemented VKR flow.
- Mock check is a demonstration / extension point, not real external verification.
- Contract is not generated automatically.
- Agreement stage is started by an employee after approval.
- Agreement stage is exchange of ready document versions.
- Document reference / metadata is not a full ECM/EDO/file storage system.
- Do not claim email is fully implemented without repo-check.
