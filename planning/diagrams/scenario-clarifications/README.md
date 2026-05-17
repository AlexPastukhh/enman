# Scenario Clarifications For Diagram Generation

Status: current diagram-generation clarification index / L2 validation cleanup synchronized  
Scope: scenario conflicts, terminology guards and non-blocking questions that must be checked before drawing diagrams

## 1. Purpose

This folder tracks scenario-level clarifications that should be resolved or explicitly marked before diagram generation.

It is intentionally separate from implementation slices.

Use it when:

```text
- a scenario/specification conflict may produce a wrong diagram;
- terminology has changed but older scenario files may still contain old wording;
- a diagram prompt must not silently choose the wrong interpretation;
- a question is not about implementation details, but about scenario meaning.
```

## 2. Current Clarifications

| File | Topic | Diagram impact |
|---|---|---|
| `AGR-001-agreement-proposal-replacement-terminology.md` | Agreement proposal replacement must not be drawn as ordinary rejection | lifecycle/state diagrams, sequence diagrams for SC-13B/SC-13D |
| `L2-validation-and-agreement-exchange-source-cleanup.md` | L2 validation source cleanup, stale global validation addendum guardrail, current agreement ownership/numbering/route decisions | L2 review/agreement diagrams, scenario cleanup, diagram prompts |
| `diagram-generation-readiness-guardrails.md` | General guardrails before generating diagrams | all diagrams |

## 3. Rule

If a scenario text file and a clarification file disagree, diagram generation must not silently pick the scenario text.

Instead:

```text
1. use the clarification as the current assumption if it has an accepted direction;
2. mark the diagram note with the assumption;
3. create/update scenario cleanup work item for later text-spec correction.
```

## 4. Not A Replacement For Scenario Specs

Clarification files are temporary guardrails.

They do not replace scenario text specs. Once the scenario specs are corrected, keep only a short historical note or remove the clarification if no longer needed.

## 5. Deprecated Source Warning

Old global validation addenda are not active L2 source of truth when they conflict with current L2 clarifications/slices/domain direction.

Do not use stale terms such as:

```text
DocumentFileRef
ReviewerRef
ProposalAttachment
EmployeeRef
```

for current L2 review/agreement diagrams.
