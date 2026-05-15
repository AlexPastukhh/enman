# ADR Workflow

Status: current ADR workflow  
Scope: ADR candidates, accepted architecture decision notes and future full ADRs

## 1. Purpose

This file defines how architecture decisions should be captured.

ADR documentation is important for this project because architectural reasoning is part of the project value and can be reused in diploma text.

## 2. Three Levels Of Decision Documentation

Use three levels:

```text
1. ADR candidate
   A decision may matter enough to document formally later.

2. Architecture decision note
   A decision is already accepted enough to be used,
   but not yet promoted to a numbered full ADR.

3. Full numbered ADR
   A stable, important decision is documented formally with context,
   alternatives, decision and consequences.
```

## 3. Where To Put What

### ADR candidates

File:

```text
planning/adr/adr-candidates.md
```

Use for:

```text
- unresolved decisions;
- decisions that may need full ADR later;
- decisions with meaningful alternatives;
- decisions that may become diploma-relevant.
```

### Accepted architecture decision notes

File:

```text
planning/adr/architecture-decision-notes.md
```

Use for:

```text
- accepted/current directions already used by planning;
- decisions with useful rationale;
- decisions that affected workflow, slice boundaries, client/server boundaries or domain boundaries;
- decisions useful for diploma explanation;
- decisions not yet worth a full numbered ADR.
```

### Full ADRs

Path pattern:

```text
planning/adr/ADR-0001-title.md
```

Create only when explicitly requested or when the decision is stable and important enough.

## 4. ADR Candidate Trigger

Add/update an ADR candidate when a decision:

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
- creates a planned extension/change point;
- creates important extension pressure / anti-coupling decision.
```

## 5. Accepted Decision Note Trigger

Add/update an architecture decision note when:

```text
- a question was discussed and accepted;
- the current direction is now used by domain/slice/client planning;
- the decision has meaningful rationale or trade-off;
- future agents should not reopen it accidentally;
- the decision can be reused in diploma text.
```

## 6. Full ADR Promotion Trigger

Promote to a full numbered ADR when:

```text
- the decision is stable;
- the decision is architecturally significant;
- alternatives are clear;
- consequences are known enough;
- the decision affects implementation or diploma explanation;
- the user explicitly asks for full ADR.
```

## 7. Full ADR Template

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

## 8. Agent ADR Capture Rule

When planning or implementation reveals an architectural decision, the agent must decide:

```text
- no ADR relevance;
- add/update ADR candidate;
- add/update accepted architecture decision note;
- propose full ADR promotion.
```

The agent should not create full numbered ADRs unless explicitly asked.

The agent must include its assumption/preferred direction when raising ADR-related questions.

## 9. Next-Step Rule

Every archive or implementation prompt should mention ADR impact when relevant:

```text
ADR impact:
- no ADR update needed
- candidate added/updated
- decision note added/updated
- full ADR candidate proposed
```

## 10. Do Not

```text
- Do not turn every naming or file-placement choice into ADR.
- Do not lose accepted disputed decisions in chat only.
- Do not create numbered ADRs without explicit request.
- Do not duplicate local slice questions as ADRs unless they affect architecture boundaries.
- Do not document a decision as accepted if it is still open.
```
