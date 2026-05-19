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
   - section block candidates;
   - wording candidates;
   - table candidates;
   - problem notes;
   - questions.
5. Move ideas into topic drafts as material candidates.
6. Move usable text only into corresponding section draft blocks, marked as requiring review.
7. Run topic-draft workflow for reused material:
   - questions;
   - source support;
   - research/repo/visual checks;
   - plan of disclosure;
   - rewrite/edit.
8. Do not copy the chapter wholesale into final VKR text.

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

### Section block candidates
| Candidate block | Related topic draft | Reuse mode | Checks |
|---|---|---|---|
```

## Main rule

Existing chapter draft can feed topic draft and section draft, but it does not replace topic-draft workflow.
