# Drafting Rule — Current Project State Source

Status: active drafting/documentation rule  
Applies to: all status/inventory answers and all docs that reconcile implementation state

## Rule

When asked about the current project state, implementation status, existing code, endpoints, routes, generated contracts or tests, the answer must be based on the current repository state in GitHub.

```text
Use GitHub/repo connector as source of truth for current project state.

Do not infer current implementation status from handoff archives,
draft archives, pasted patches or old generated zip files.
```

Archives and uploaded handoff packages are useful as input for proposed changes, but they are not proof that the repo currently contains those changes.

## Practical meaning

```text
If the user asks:
  "что сейчас имплементировано?"
  "какие слайсы не имплементированы?"
  "есть ли endpoint?"
  "какие files сейчас есть?"
  "какой текущий diff/status?"

Then:
  inspect GitHub/current branch first.
```

Use archives only to understand proposed updates or to create a new handoff archive.

## Reason

Handoff archives can be:

```text
- not applied;
- partially applied;
- superseded by later archives;
- generated from earlier docs;
- locally modified outside the repo connector state.
```

Therefore, they must not be treated as the current project state.
