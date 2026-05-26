# Agent Scope Boundaries And Prompt Safety

Status: current agent-scope rule / scope-boundary and GitHub line-link workflow synchronized  
Scope: implementation prompts, slice prompts, documentation prompts and any chat that prepares work for another agent

## 1. Purpose

This project uses specialized chats/agents. A prompt for another chat must not silently grant permission to change areas that the user did not explicitly put in scope.

Reading docs is allowed and required. Changing docs, domain code, generated artifacts or scenario sources is not allowed unless the task explicitly says so.

## 2. Core Rule

A chat may read broadly, but it may change narrowly.

```text
Read current repo/docs for context.
Change only the files and responsibility area explicitly requested.
Do not infer write permission from useful context.
```

## 3. Prompt Generation Rule

When creating a prompt for another chat, do not include permission to change:

```text
- Domain.EnergyManagement/**
- planning/** docs
- generated artifacts
- scenario text/DATA/UI/behavior items
- ADR files
- cross-cutting workflow docs
```

unless the user explicitly asked for those changes.

A prompt may and should tell the next chat to read these files.

A prompt must not say or imply:

```text
"update domain if needed"
"adjust planning docs if needed"
"fix scenarios if needed"
"regenerate artifacts if convenient"
```

unless that is the explicit assignment.

## 4. Repo Evidence Link Rule For Prompts

When creating a prompt for another chat, include this rule whenever the task involves code, docs, status reconciliation, implementation review or problem analysis:

```text
When explaining current repo code/docs or reporting problems, provide GitHub Markdown links to exact lines/ranges. Prefer commit SHA links. If commit SHA is unavailable, use branch links and say they may drift.
```

Read:

```text
planning/repo-grounded-github-line-links-workflow.md
```

The prompt should not instruct the next chat to use whole-file links as evidence for specific implementation claims.

## 5. Implementation Agent Rule

Implementation agents must receive:

```text
- repository and branch;
- exact implementation scope;
- files/docs to read;
- current slice docs and sidecars;
- planned implementation direction that is already known;
- explicit out-of-scope list;
- explicit related-slice / future-slice ownership for excluded responsibilities;
- explicit docs/domain/generated-artifact mutation rules;
- repo-grounded GitHub line-link rule for explanations and handoff notes.
```

Implementation agents may read planning docs, but must not change planning docs unless the prompt explicitly says documentation update is part of the task.

Implementation agents must not change domain model code unless the prompt explicitly includes domain implementation in scope.

If implementation exposes doc drift, the agent should report it as a handoff note and include exact GitHub line links where practical:

```text
Docs drift found:
- ...
Recommended target role: Documentation Keeper / Status Reconciliation Chat
Evidence links:
- [short path, lines X-Y](...#LX-LY)
```

## 6. Slice Scope Preservation Rule

When a prompt is generated from a slice draft, preserve the slice boundary sections:

```text
Scope
Out of scope
Related slices / owners
Future extension points
```

Do not let an implementation prompt expand a read slice into:

```text
- command implementation;
- client UI;
- default/current switching;
- delete/archive lifecycle;
- domain field rename cleanup;
- generated artifact changes beyond explicitly requested contract checks;
- unrelated scenario/source cleanup.
```

If the implementation task intentionally crosses one of those boundaries, state that the user explicitly expanded the scope.

If the implementation discovers a need outside the slice scope, use a handoff note instead of silently implementing it.

## 7. Documentation Agent Rule

Documentation agents may update planning/docs only within the requested documentation scope.

They must not implement code, change generated artifacts or modify runtime behavior.

If a documentation task reveals code/domain work, the agent should record it as planned/future or hand it off to the implementation role.

## 8. Domain Mutation Rule

Domain code is high-impact.

A chat may change domain code only if the user explicitly asks for domain implementation or the assigned implementation slice clearly includes domain changes.

Examples that are not enough by themselves:

```text
"implement client UI"
"add a page"
"fix docs"
"create prompt"
"wire API wrapper"
```

These do not permit changing domain code.

## 9. Documentation Mutation Rule

Planning docs should be changed only by:

```text
- Documentation Keeper / Status Reconciliation Chat;
- Scenario Draft Chat for scenario artifacts in scope;
- Slice Draft Chat when the explicit task is to create/update slice docs;
- Diagram Prompt/Workflow doc updates when explicitly requested.
```

Implementation chats may propose documentation follow-ups but should not edit docs unless asked.

## 10. Handoff Instead Of Scope Creep

When a chat discovers needed work outside its scope, it should stop at a handoff note.

Format:

```text
Boundary reached:
Target role:
Reason:
Current evidence:
Evidence links:
Out-of-scope files:
Open questions:
Recommended next action:
```

`Evidence links` should use GitHub line links when the evidence is in repo files.

## 11. Read Context Checklist For Prompt Creators

When writing a prompt for a slice implementation chat, include the docs the agent must read:

```text
planning/README.md
planning/planning-agent-protocol.md
planning/agent-scope-boundaries-and-prompt-safety.md
planning/repo-grounded-github-line-links-workflow.md
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
relevant parent slice file
relevant .client.md sidecar, if client work
relevant scenario text/DATA/UI/behavior item sources
relevant API/testing/cross-cutting docs
```

If planned implementation details are known but not yet fully captured in docs, include them explicitly under:

```text
Known implementation direction from current planning discussion:
```

Do not hide them or assume the next chat can infer them.
