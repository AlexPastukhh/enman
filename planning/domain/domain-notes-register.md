# Domain Notes Register

Status: current domain notes register  
Scope: domain modeling notes that should not be lost but are not yet placed into a specific aggregate, value-object or decision file

## 1. Purpose

Use this register for:

```text
possible future aggregate notes;
possible value object extraction notes;
unresolved invariant notes;
implementation-adjacent domain notes;
notes discovered during scenario/domain/slice work.
```

Do not use it as a dumping ground.

If a note clearly belongs to an aggregate, value object or decision, move it there.

## 2. Entry Format

```text
ID:
Status:
Area:
Note:
Source:
Likely owner:
Target file:
Trigger:
Decision needed:
Do when:
Do not do before:
```

## 3. Current Entries

### DN-001 — Align domain workflows after source/version/cascade model

```text
ID: DN-001
Status: future review
Area: source/version/cascade alignment
Note: After source/version/cascade model is introduced, align domain-discovery-workflow.md, aggregate-drafting-workflow.md, value-object-drafting-workflow.md, scenario-to-aggregate-map.md and templates with source metadata conventions.
Source: domain layer conversion planning
Likely owner: planning/domain/ workflows and templates
Target file: planning/planning-maintenance-register.md may track the durable follow-up
Trigger: source/version/cascade model becomes current
Decision needed: exact source metadata fields for aggregate/value-object drafts
Do when: after source/version/cascade pilot stabilizes
Do not do before: before domain scaffold and source/version model are both stable
```

### DN-002 — AgreementProposalExchange pilot extraction

```text
ID: DN-002
Status: candidate
Area: aggregate extraction
Note: AgreementProposalExchange is preferred as the first aggregate extraction pilot because it has child entities, value object candidates and cross-aggregate coordination with Request.
Source: domain layer conversion planning and existing L2 agreement/domain sources
Likely owner: planning/domain/aggregates/agreement-proposal-exchange.md
Target file: planning/domain/aggregates/agreement-proposal-exchange.md
Trigger: first aggregate extraction batch
Decision needed: exact source set for pilot extraction
Do when: after scaffold and scenario-to-aggregate map exist
Do not do before: before aggregate template/workflow exist
```
