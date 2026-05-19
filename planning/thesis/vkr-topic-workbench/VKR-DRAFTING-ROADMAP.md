# VKR drafting roadmap

This is the high-level planning file for VKR drafting.

It stores the shared picture, cross-chapter notes, future ideas, disputed decisions and material that should not be lost before it is distributed into concrete topic drafts or section drafts.

## 1. Capture-first rule

```text
raw notes / thoughts / disputed decisions
→ this roadmap
→ chapter roadmap
→ topic draft
→ section draft block
```

When a note may belong to several future sections, do not force it into the current topic draft.

First capture it here. Later distribute it to concrete chapter roadmaps, topic drafts and section draft blocks.

## 2. Current cross-cutting drafting model

```text
desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ discovery questions
→ semantic points
→ semantic point desired-result coverage
→ order of disclosure
→ section draft blocks
```

## 3. Important workflow decision

Do not create one huge desired-result coverage table for the whole topic.

The topic has a concise desired outcome and discovery table.

The detailed coverage table belongs inside each semantic point card.

```text
theme desired outcome
→ semantic points
→ semantic point desired result
→ coverage table inside semantic point
```

## 4. Cross-chapter logic

| Chapter | Main role | Notes |
|---|---|---|
| Chapter 1 | domain process → problems → automation options → requirements | Should not claim own app is universally best |
| Chapter 2 | requirements → design → model → architecture → DB/API/UI | Should use conclusions from Chapter 1 |
| Chapter 3 | implementation → evidence → testing → result | Should be grounded in repo, slices, tests and screenshots |

## 5. Cross-cutting themes

| Theme | Where it may be used | Notes |
|---|---|---|
| Request as a linking business-process object | 1.1, 1.3, Chapter 2 | Avoid heavy DDD terminology in Chapter 1 |
| Request state and allowed actions | 1.1, 1.3, Chapter 2 | Helps explain automation as process consistency, not only less manual input |
| User account vs applicant | 1.1, Chapter 2 | Account gives access/actions; applicant holds person/application data |
| Agreement/document exchange | 1.2, Chapter 2, Chapter 3 | Do not call it full ECM/EDO |
| Own web application and trade-offs | 1.4, 1.5 | Explain conditions and trade-offs, not universal superiority |
| Mock-check | Chapter 3 only | Do not use as Chapter 1 semantic point |

## 6. Decisions

| Decision | Reason | Where to apply |
|---|---|---|
| First capture raw notes in roadmap | Prevents loss and avoids overloading topic drafts | all drafting |
| Topic drafts should not carry all future notes | Topic drafts stay focused on current topic | topic drafts |
| Existing chapters are section draft candidates | They can be useful but are not final | section drafts/topic drafts |
| Semantic points derive from desired outcomes | Prevents random blocks | all topic drafts |
| Detailed desired-result coverage lives inside semantic point cards | Keeps coverage tied to the current point | all topic drafts |
| Own app is not presented as universally best | Honest analysis of alternatives and trade-offs | 1.4/1.5 |
| Mock-check is not a Chapter 1 point | It is implementation/demo evidence | Chapter 3 |
| Use "user of web app" and "applicant" carefully | Avoids mixing account and person data | 1.1 / Chapter 2 |

## 7. Captured raw notes

| Date | Source | Summary | Raw file | Status |
|---|---|---|---|---|
| 2026-05-19 | user discussion | Chapter 1 topic v5 and workflow correction: desired-result coverage should live inside semantic point cards; automation-option choice needs research; organization intro should be short; 1.1 should track which semantic point covers which result | `00-inbox/raw-notes/2026-05-19-chapter1-topic-v5-semantic-point-coverage-notes.txt` | captured / needs distribution |

## 8. Distribution plan for current captured notes

| Note | Possible places | Why important | Risk | Status |
|---|---|---|---|---|
| Desired-result coverage table belongs inside semantic point cards | topic-card-template, semantic-point-discovery, 1.1 topic draft | Prevents huge unclear topic-level coverage tables | missing user's ideas if not attached to points | captured / workflow update |
| Conditions for choosing automation options require research | 1.4, 1.5 | Needs stronger evidence than memory | weak justification of own app | research needed |
| License/implementation costs can matter when considering ready solutions | 1.4, 1.5 | User explicitly raised cost/payment | may require current sources | research needed |
| Specific business logic can justify custom app in some cases | 1.4, 1.5, Chapter 2 | Connects domain rules to implementation direction | needs careful wording | captured |
| Typical processes may fit ready solutions better | 1.4 | Avoids fake argument that own app is always best | may weaken if not balanced | captured |
| Short organization context should lead quickly to request process | 1.1 | Avoids starting too abruptly or too broadly | generic organization description | captured |
| "Automation is broader than a form" belongs inside semantic point coverage | 1.1 semantic point "state and allowed actions" | Shows exactly what this idea explains | can be lost in roadmap | move to topic draft |
