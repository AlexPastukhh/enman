# Algorithm: existing chapter draft review

Run when the user provides existing chapter drafts or old section drafts that are not final but may contain useful material.

## Storage

Store or reference them under:

```text
planning/thesis/vkr-clean/existing-chapter-drafts/
```

Do not confuse with:

```text
planning/thesis/vkr-clean/legacy-chaotic-drafts/
```

`existing-chapter-drafts` may be useful. `legacy-chaotic-drafts` are mostly cautionary / cleanup material.

## Important interpretation

Existing chapters are currently treated as **section draft candidates**.

That means:

```text
existing chapter text stays on the section-draft side;
topic draft receives semantic analysis of that text;
raw chapter paragraphs are not pasted into topic draft as topic content.
```

If the task is to break an existing subsection into topic/section workflow, also run:

```text
planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-section-draft-reverse-engineering.md
```

## Required behavior

1. Do not treat the existing chapter as final clean VKR text.
2. Determine which VKR chapters, subsections and topic drafts it relates to.
3. Review it by categories:
   - what is already good;
   - what can be reused;
   - what is too generic;
   - what is inaccurate;
   - what overclaims implementation;
   - what needs research sources;
   - what needs repo/evidence check;
   - what has formatting/numbering/visual issues.
4. Extract useful material as candidates:
   - structure candidates;
   - semantic point candidates;
   - section block candidates;
   - wording candidates;
   - table candidates;
   - problem notes;
   - questions.
5. Reverse-engineer useful text into semantic points before updating topic drafts.
6. Move ideas into topic drafts as material candidates and semantic-point cards.
7. Move usable text only into corresponding section draft blocks, marked as requiring review.
8. Run topic-draft workflow for reused material:
   - questions;
   - source support;
   - research/repo/visual checks;
   - plan of disclosure;
   - rewrite/edit.
9. Do not copy the chapter wholesale into final VKR text.

## Output format

```markdown
## Existing chapter draft review

### What is good
...

### What can be reused
| Material | Type | Where to use | Required checks |
|---|---|---|---|

### What is dangerous
...

### Questions generated
| Question | Priority | Why it matters | Where it goes |
|---|---|---|---|

### Semantic point candidates
| Semantic point | Related topic draft | Current section text | Checks | Action |
|---|---|---|---|---|

### Section block candidates
| Candidate block | Related topic draft | Reuse mode | Checks |
|---|---|---|---|
```

## Main rule

Existing chapter draft can feed topic draft and section draft, but it does not replace topic-draft workflow. The topic draft should contain semantic points, questions and plans; the section draft contains candidate text.
