# New chat onboarding for VKR work

Read this before working on VKR materials.

## 1. Main files

1. `planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md`
2. `planning/thesis/README.md`
3. `planning/thesis/chat-action-algorithms/README.md`
4. `planning/thesis/vkr-topic-workbench/README.md`
5. `planning/thesis/vkr-clean/README.md`

## 2. What not to do

Do not write the thesis directly from memory. Do not copy planning files directly into the thesis. Do not mention AI, prompts, chats or agent workflow in VKR text.

Do not use L1/L2 as thesis language.

Do not overclaim implementation.

## 3. What to do on typical commands

| User says | Run |
|---|---|
| `дай драфт`, `давай драфт`, `обнови драфт` | `chat-action-algorithms/drafting/topic-draft-default-flow.md` |
| `уточни`, `перепроверь` | `chat-action-algorithms/drafting/topic-clarify-and-recheck-flow.md` |
| `дай section draft` | `chat-action-algorithms/drafting/section-draft-generation.md` |
| user sends diagrams | `chat-action-algorithms/evidence-and-materials/visual-material-review.md` |
| implementation/chapter 3 text | `chat-action-algorithms/evidence-and-materials/repo-check-before-implementation-text.md` |
| archive request | `chat-action-algorithms/archive-generation-and-navigation-update.md` |
| `тчт` | `chat-action-algorithms/tcht-command.md` |

## 4. Drafting principle

A topic draft is the semantic base. A section draft is text being accumulated by section blocks.

Never create a section draft as one uncontrolled wall of text. First create blocks, questions and a plan of disclosure.

## 5. Navigation rule

Whenever a file is created, changed, moved or deleted, check:

```text
chat-action-algorithms/navigation-impact-check.md
```

The chat should ask/check: which navigation files must be updated because of this change?

## 6. Core guardrails

- Account activation is not a central implemented VKR flow.
- Mock check is a demonstration / extension point, not real external verification.
- Contract is not generated automatically.
- Agreement stage is started by an employee after approval.
- Agreement stage is exchange of ready document versions.
- Document reference / metadata is not a full ECM/EDO/file storage system.
- Do not claim email is fully implemented without repo-check.
