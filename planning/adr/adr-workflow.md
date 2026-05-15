# ADR Workflow

Status: current ADR workflow  
Scope: ADR candidates, accepted architecture decision notes and future full ADRs

## 1. Purpose

This file defines how architecture decisions are captured without creating full numbered ADRs too early.

ADR documentation is important because architectural reasoning is part of the project value and can later be reused in diploma text.

## 2. Decision Documentation Levels

Use three levels:

```text
1. Architecture decision note
   Current accepted direction that guides planning and implementation.

2. ADR candidate
   Promotion backlog: a decision may later become a full numbered ADR.

3. Full numbered ADR
   Formal architecture decision record, created only when explicitly requested.
```

## 3. Source Priority

When deciding what direction to follow:

```text
1. Full numbered ADR, if it exists.
2. architecture-decision-notes.md.
3. Local slice/domain/client decision section if not yet promoted.
4. adr-candidates.md as promotion backlog / warning list.
```

`adr-candidates.md` is not the primary guiding source.

If `adr-candidates.md` and `architecture-decision-notes.md` disagree, stop and resolve before continuing implementation planning.

## 4. Architecture Decision Notes

File:

```text
planning/adr/architecture-decision-notes.md
```

Use it for decisions that are already accepted/current enough to guide planning.

A note should capture:

```text
- decision;
- context;
- options considered;
- accepted direction;
- rationale/trade-off;
- affected artifacts;
- diploma value;
- full ADR promotion likelihood.
```

## 5. ADR Candidates

File:

```text
planning/adr/adr-candidates.md
```

Use it for decisions that may become full numbered ADRs later.

A candidate should capture:

```text
- decision area;
- why a formal ADR may be useful;
- current accepted note IDs if direction is already accepted;
- promotion priority;
- open questions.
```

## 6. Full Numbered ADRs

Path pattern:

```text
planning/adr/ADR-0001-title.md
```

Do not create full numbered ADRs unless explicitly requested.

Full ADR template:

```markdown
# ADR-XXXX — Title

Status:
Date:
Decision owner/context:

## Context

## Decision

## Options Considered

| Option | Pros | Cons | Why not chosen / why chosen |
|---|---|---|---|

## Consequences

## Affected Scenarios / Slices / Files

## Tests / Verification

## Related Planning Artifacts

## Follow-Up Questions
```

## 7. When To Add Or Update A Decision Note

Add/update `architecture-decision-notes.md` when:

```text
- a question was discussed and accepted;
- the direction now guides planning or implementation;
- the decision has meaningful rationale/trade-off;
- future agents should not reopen it accidentally;
- the decision is useful for diploma explanation.
```

## 8. When To Add Or Update An ADR Candidate

Add/update `adr-candidates.md` when a decision:

```text
- affects multiple slices;
- changes layer boundaries;
- has meaningful alternatives;
- is useful for diploma architecture explanation;
- is expensive to change later;
- introduces a plugin/external provider boundary;
- decides read model vs write model duplication;
- decides transaction boundaries between aggregates;
- changes auth/framework placement;
- changes testing strategy or delivery workflow;
- creates/changes a cross-cutting client/server convention;
- creates planned extension/change/extension-pressure strategy.
```

## 9. Agent ADR Capture Rule

When planning or implementation reveals an architectural decision, the agent must classify ADR impact:

```text
no ADR relevance
add/update architecture decision note
add/update ADR candidate
propose full ADR promotion
```

The agent should not create full numbered ADRs unless explicitly asked.

## 10. Archive / Prompt Rule

Every planning archive or implementation prompt should include:

```text
ADR impact:
- no ADR update needed
- decision note added/updated
- candidate added/updated
- full ADR promotion proposed
```

## 11. Do Not

```text
- Do not turn every naming or file-placement choice into ADR.
- Do not lose accepted disputed decisions in chat only.
- Do not use adr-candidates as the sole guiding source.
- Do not create numbered ADRs without explicit request.
- Do not duplicate local slice questions as ADRs unless they affect architecture boundaries.
- Do not document a decision as accepted if it is still open.
```
