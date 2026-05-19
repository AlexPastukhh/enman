# Algorithm: topic-to-section block drafting

Use this when converting topic-draft content into section-draft structure.

## Main principle

```text
topic-драфт задаёт смысловые блоки;
section-драфт создаёт похожие блоки;
текст постепенно собирается внутри этих блоков.
```

## Semantic point

A semantic point is the smallest meaningful unit of topic disclosure.

Each semantic point must explain:

```text
what it says;
why it is needed;
where it stands in the disclosure order;
what goes before it;
what goes after it;
which questions it raises;
which sources support it;
how it can become VKR text.
```

## Required process from topic draft

1. Extract semantic blocks / semantic points from the topic draft.
2. For each point, identify questions.
3. For each point, identify source materials.
4. For each point, define plan of disclosure.
5. Create matching headings/subheadings in section draft.
6. Generate text only inside those blocks.
7. Keep topic draft alive: when new questions/answers appear, update it.

## Required process from existing section draft

If an existing chapter/subsection already exists, do not paste it into topic draft.

Run reverse engineering:

```text
existing text block
→ semantic point
→ purpose/order/questions/sources/risks
→ topic draft update
→ section draft block correction
```

Use:

```text
planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-section-draft-reverse-engineering.md
```

## Do not

Do not use a standalone fragment bank as mandatory workflow. Do not paste random paragraphs into a section draft without block placement. Do not paste whole existing chapter text into a topic draft.
