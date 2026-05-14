# Architecture Decision Records Index

Status: current ADR planning entry point  
Scope: ADR candidates and future architecture decision records

## 1. Purpose

This folder collects architecture decisions and ADR candidates discovered during domain drafting, slice drafting and implementation planning.

ADR documentation is useful for this project because planning and architectural reasoning are part of the project value and can support diploma explanation.

## 2. Current ADR Workflow

Start with candidates, not full ADRs.

Use:

```text
planning/adr/adr-candidates.md
```

Promote a candidate to a full ADR only when the decision is stable enough and important enough.

Expected future files may look like:

```text
planning/adr/ADR-0001-domain-foundation-before-slices.md
planning/adr/ADR-0002-applicant-party-versioning-policy.md
planning/adr/ADR-0003-read-projections-vs-write-aggregate-duplication.md
```

Do not create numbered ADRs until explicitly requested.

## 3. When To Add An ADR Candidate

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
- changes testing strategy or delivery workflow.
```

## 4. Candidate Format

Use this format:

```markdown
| ADR candidate | Decision needed | Context | Options | Current direction | Affected scenarios/slices | Urgency | Notes |
|---|---|---|---|---|---|---|---|
```

## 5. Full ADR Format Later

When promoted, a full ADR should include:

```text
Title
Status
Context
Decision
Options considered
Consequences
Affected scenarios/slices
Links to planning artifacts
Open follow-up questions
```

## 6. Relationship To Slice Planning

Slice planning should collect ADR candidates whenever a slice reveals a cross-cutting decision.

Examples:

```text
external verification provider as plugin slice
read projection instead of duplicating ClientAccountId in write aggregate
final agreement refusal requiring request-level outcome
auth activation guard placement
```

## 7. Agent Rules

Agents should:

```text
- not write full ADRs unless explicitly asked;
- add candidates when a decision affects multiple slices or architecture boundaries;
- avoid turning every minor naming choice into an ADR;
- link candidates to scenarios/slices when possible;
- keep ADR candidates readable for diploma explanation.
```
