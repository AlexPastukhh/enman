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

## 2. Why there are several roadmap files

The general roadmap keeps the whole VKR picture and cross-chapter notes.

Chapter roadmaps keep notes that already belong mainly to one chapter.

```text
VKR-DRAFTING-ROADMAP.md
→ общая картина, сквозные решения, сырые заметки, спорные мысли

CHAPTER-X-ROADMAP.md
→ план конкретной главы, желаемый итог главы, заметки на перенос внутри главы
```

This prevents topic drafts from carrying future notes.

## 3. Current cross-cutting drafting model

```text
desired outcome of parent element
→ role of current element
→ desired outcome of current element
→ discovery questions
→ semantic points
→ order of disclosure
→ section draft blocks
```

## 4. Cross-chapter logic

| Chapter | Main role | Notes |
|---|---|---|
| Chapter 1 | domain process → problems → automation options → requirements | Should not claim that own app is universally best |
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
| Own app is not presented as universally best | Honest analysis of alternatives and trade-offs | 1.4/1.5 |
| Mock-check is not a Chapter 1 point | It is implementation/demo evidence | Chapter 3 |
| Use "user of web app" and "applicant" carefully | Avoids mixing account and person data | 1.1 / Chapter 2 |

## 7. Captured raw notes

| Date | Source | Summary | Raw file | Status |
|---|---|---|---|---|
| 2026-05-19 | user discussion | Chapter 1 notes about own app trade-offs, request state, allowed actions, user/applicant distinction, mock-check transfer and future chapter plans | `00-inbox/raw-notes/2026-05-19-chapter1-request-process-automation-options-notes.txt` | captured / needs distribution |

## 8. Distribution plan for current captured notes

| Note | Possible places | Why important | Risk | Status |
|---|---|---|---|---|
| Chapter 1 should not prove own app is always best | 1.4, 1.5 | Keeps argument honest | artificial justification | captured |
| Explain conditions for choosing alternatives | 1.4 | Makes automation analysis stronger | too broad if not controlled | captured |
| Own app in VKR is considered because the topic requires development and because it can model specific process logic | 1.5 | Gives acceptable academic framing | sounding like "because forced" | captured |
| Request as linking process object | 1.1, 1.3, Chapter 2 | Stronger domain explanation | too technical if DDD terms used | captured |
| Request state and allowed actions | 1.1, 1.3, Chapter 2 | Shows automation as consistency support | needs careful examples/repo-check | captured |
| User web account vs applicant | 1.1, Chapter 2 | Clarifies model | overexplaining account | captured |
| Mock-check only in Chapter 3 | Chapter 3 | Avoids overclaim in Chapter 1 | losing useful implementation note | decided |
| Architecture as way to support process rules | Chapter 2 | Creates bridge from Chapter 1 to design | too abstract if not tied to requirements | captured |

## 9. Unsorted notes area

Use this section for future notes that do not yet have a clear place.

Each note should eventually be moved to a chapter roadmap, topic draft or section draft block.
