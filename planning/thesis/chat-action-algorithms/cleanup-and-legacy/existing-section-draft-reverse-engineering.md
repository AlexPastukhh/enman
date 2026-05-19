# Algorithm: existing section draft reverse engineering

Run when the user works with an already written chapter, subsection or section draft candidate and wants to turn it into a controlled topic/section workflow.

## Main idea

An existing chapter or subsection is not copied into a topic draft. It is treated as a **section draft candidate**.

The chat must reverse-engineer it into **semantic points**.

```text
existing chapter / subsection
→ section draft candidate
→ semantic points
→ questions
→ source checks
→ plan of disclosure
→ updated topic draft
→ corrected section draft blocks
```

## Key terms

### Existing chapter draft

A previously written chapter or subsection. It may contain useful structure, wording, tables and explanations, but it is not final clean VKR text.

### Section draft candidate

A text candidate for a future VKR section. In the current workflow, existing chapters may temporarily play this role.

### Semantic point

A semantic point is the smallest meaningful unit of topic disclosure.

A semantic point must have:

```text
name;
purpose in disclosure of the topic;
place in the disclosure order;
what goes before it;
what goes after it;
why it belongs here;
questions;
default answers / assumptions;
sources to check;
plan of disclosure;
example of conversion into VKR text;
decision for section draft text.
```

## Required process

1. Identify what existing text is being reviewed.
2. Determine its VKR place and related topic draft.
3. Do not paste the full text into the topic draft.
4. Split the text into semantic points.
5. For each semantic point, decide:
   - whether it is needed;
   - whether it fits this topic;
   - whether it should be moved;
   - whether it requires research;
   - whether it requires repo/evidence check;
   - whether it creates overclaim;
   - whether the existing wording can be reused.
6. Create or update the topic draft with semantic points, questions and disclosure plans.
7. Keep the existing text in the section draft candidate.
8. Update the section draft only after semantic-point review.

## Output format

```markdown
# Reverse engineering of existing section draft: <title>

## 0. Material

- Source:
- VKR place:
- Related topic draft:
- Status: section draft candidate

## 1. Overall assessment

- What is already good:
- What can be reused:
- What is risky:
- What needs sources:
- What needs repo/evidence check:
- What should be moved or removed:

## 2. Semantic points extracted from existing text

| № | Semantic point | Purpose | Place in disclosure | Existing text status | Action |
|---|---|---|---|---|---|

## 3. Semantic point cards

### Semantic point <n>: <name>

**Purpose:**
...

**Place in disclosure:**
- after:
- before:
- why here:

**Current section draft material:**
- source fragment:
- how well it fits:

**Questions:**
| Question | Why needed | Priority | Default answer | Influence |
|---|---|---|---|---|

**Source checks:**
- research:
- scenarios:
- DATA:
- domain:
- slices:
- repo/evidence:
- visuals:

**Plan of disclosure:**
1. ...
2. ...
3. ...

**Example VKR wording:**
...

**Decision for section draft:**
keep / compress / expand / rewrite / move / remove / add nearby block
```

## How to use existing text

Correct:

```text
existing paragraph
→ identify semantic point
→ check purpose/order/questions/sources
→ keep or rewrite in section draft block
```

Incorrect:

```text
existing paragraph
→ paste into topic draft as if it were the topic draft
```

## Main rule

Existing chapter text can be a section draft candidate, but the topic draft must contain the semantic analysis of that text, not the raw text itself.
