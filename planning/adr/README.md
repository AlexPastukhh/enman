# Architecture Decision Records Index

Status: current ADR planning entry point  
Scope: ADR candidates, accepted architecture decision notes and future architecture decision records

## 1. Purpose

This folder collects architecture decisions and ADR candidates discovered during domain drafting, slice drafting, client planning and implementation planning.

ADR documentation is useful because planning and architectural reasoning are part of the project value and can support diploma explanation.

## 2. Current ADR Files

```text
planning/adr/README.md
planning/adr/adr-workflow.md
planning/adr/adr-candidates.md
planning/adr/architecture-decision-notes.md
```

## 3. Current ADR Workflow

Use three levels:

```text
1. ADR candidate
   A decision may matter enough to document formally later.

2. Architecture decision note
   A decision is accepted/current enough to guide planning,
   but is not yet a full numbered ADR.

3. Full numbered ADR
   A stable important decision is documented formally.
```

Start with candidates and decision notes.

Do not create numbered ADRs until explicitly requested.

## 4. When To Add An ADR Candidate

Add a candidate when a decision:

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
- changes client-wide UI/testing conventions;
- introduces change/extension/extension-pressure strategy.
```

## 5. When To Add A Decision Note

Add/update a note in:

```text
planning/adr/architecture-decision-notes.md
```

when a decision:

```text
- was discussed and accepted;
- is already used by planning;
- has rationale/trade-off worth preserving;
- should be visible for future agents;
- may be useful in diploma text.
```

## 6. Full ADR Format Later

When promoted, a full ADR should include:

```text
Title
Status
Context
Decision
Options considered
Consequences
Affected scenarios/slices
Tests / verification
Links to planning artifacts
Open follow-up questions
```

## 7. Relationship To Slice Planning

Slice planning should collect ADR candidates and decision notes whenever a slice reveals a cross-cutting decision.

Examples:

```text
external verification provider as plugin slice
read projection instead of duplicating ClientAccountId in write aggregate
final agreement refusal requiring request-level outcome
auth activation guard placement
client sidecar architecture mapping
extension pressure / anti-coupling decision
```

## 8. Agent Rules

Agents should:

```text
- not write full numbered ADRs unless explicitly asked;
- add/update candidates when a decision affects multiple slices or architecture boundaries;
- add/update decision notes when a discussed decision becomes current direction;
- avoid turning every minor naming choice into an ADR;
- link candidates/notes to scenarios/slices when possible;
- keep ADR candidates and notes readable for diploma explanation;
- include ADR impact in archive summaries and implementation prompts when relevant.
```
