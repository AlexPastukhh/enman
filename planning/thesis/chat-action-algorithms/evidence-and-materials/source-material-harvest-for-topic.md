# Algorithm: source material harvest for topic drafts and full text

Run when a topic draft or full-text version needs material from project sources.

## Default source priority

Use sources in this order, unless the user explicitly instructs otherwise:

```text
1. Active topic drafts and confirmed roadmap decisions.
2. Current chapter roadmap and raw notes as cautious source.
3. Scenarios and subject/process rules.
4. Research materials.
5. Visual materials.
6. Existing section drafts.
7. Existing chapter drafts.
8. Legacy chaotic drafts.
```

## Why old drafts are late

Old drafts / ready chapters / legacy drafts are useful but can be outdated, chaotic or based on older workflow.

Use them after stronger current sources, mainly to:

- recover useful ideas;
- compare whether a semantic point was lost;
- reuse good structure or wording after rewriting;
- identify candidate section/full-text paragraphs;
- check whether the new draft is missing something.

## Exception: user points to previous draft as example

If the user explicitly points to a previous draft as an example, include it in source-pass as:

```text
draft example source
```

It may guide structure and depth, but not override topic-specific desired outcomes.

## Research

Research is stored in:

```text
planning/thesis/vkr-topic-workbench/00-research-materials/
```

Always consult:

```text
planning/thesis/vkr-topic-workbench/00-research-materials/research-index.md
```

before using research files.

## Required source-pass table

```markdown
| Source | Priority | What was checked | What was found | How it affects draft/text |
|---|---:|---|---|---|
```

## Required question table

```markdown
| Question | Where searched | What was found | Status | What to do |
|---|---|---|---|---|
```

Statuses:

```text
answered;
partial;
ask user;
research needed;
scenario/domain check needed;
repo-check needed;
visual decision needed;
defer.
```
